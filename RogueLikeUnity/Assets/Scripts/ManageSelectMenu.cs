using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ManageSelectMenu : MonoBehaviour
    {
        public Sprite[] ItemSprite;

        private GameObject _itemSelectedPrefab;
        private sbyte _wait;
        private GameObject _itemMenu;
        private GameObject _scrollItemView;
        private GameObject _ItemContentView;
        private GameObject _ItemTabParent;
        private GameObject _ItemTabUnit;
        private GameObject _ItemDetail;
        private GameObject _PlayerStatusView;
        private GameObject _MenuItemAction;
        private GameObject _MenuItemActionPosition;
        private GameObject _MenuItemActionItem;
        private GameObject _ChoicesUnit;
        private GameObject _OptionDetailView;
        private GameObject _OptionDetailSelectUnit;
        private GameObject _OptionDetailViewContent;
        private GameObject _OptionDetaiDescription;
        private GameObject _UIBase;
        private SelectTurnState TempState;

        private Dictionary<BaseItem, GameObject> _ListViewItemUnits;
        private Dictionary<BaseOption, GameObject> _ListViewOptionUnits;
        private Dictionary<MenuItemActionType, GameObject> _ListViewItemSubUnits;
        private Dictionary<int, GameObject> _ItemTabs;
        private List<GameObject> ItemsStack;
        private List<GameObject> TabsStack;
        private List<GameObject> OptionStack;

        //アイテムメニュー系
        public int nowType;
        public int beforeType;
        public BaseItem beforeTarget;
        private const int _DriveItem = 100;
        private Dictionary<int, BaseItem> _LastTarget;
        private const float TabDefaultX = -420;
        private const float TabDefaultY = 240;
        private const float TabDefaultUnit = 64;
        private const float ItemSelectDefaultX = -353.1f;
        private const float ItemSelectDefaultY = 132.54f;
        private const float ItemSelectUnitY = -25;
        private const float ViewItemUnitHeight = 25.5f;

        //オプション
        private BaseOption optionTarget;
        private const float OptionUnit = 20;
        private List<BaseOption> _ViesOptions;

        //サブメニュー系
        public MenuItemActionType subSelected;
        private Dictionary<int, MenuItemActionType> _subItemMenu;
        private const float SubViewItemUnitHeight = -25;
        private const float SubMenuDefaultX = -410;
        private const float SubMenuDefaultY = 155;
        private const float SubMenuSelectDefaultX = -100;
        private const float SubMenuSelectDefaultY = 155;

        private List<BaseItem> TempItems;
        private List<BaseItem> TempActionList;


        private SelectTurnState TempMain;

        /// <summary>
        /// 初期画面の選択項目数
        /// </summary>
        private const sbyte ItemMenuTabsCount = 11;

        public void Start()
        {
            _ListViewItemUnits = new Dictionary<BaseItem, GameObject>(new BaseItemComparer());
            _ListViewItemSubUnits = new Dictionary<MenuItemActionType, GameObject>(new MenuItemActionTypeComparer());
            _ListViewOptionUnits = new Dictionary<BaseOption, GameObject>(new BaseOptionComparer());
            _LastTarget = new Dictionary<int, BaseItem>();
            _ItemTabs = new Dictionary<int, GameObject>();
            ItemsStack = new List<GameObject>();
            TabsStack = new List<GameObject>();
            OptionStack = new List<GameObject>();

            TempActionList = new List<BaseItem>();

            //アイテムメニュービューを取得
            _itemMenu = GameObject.Find("MenuItemPanel");
            _ItemTabParent = _itemMenu.transform.Find("Tabs").gameObject;
            _ItemTabUnit = _ItemTabParent.transform.Find("MenuTabUnit").gameObject;
            _ItemDetail = _itemMenu.transform.Find("ItemDetail").gameObject;
            _scrollItemView = _itemMenu.transform.Find("MenuItemView").gameObject;
            _itemSelectedPrefab = _scrollItemView.transform.Find("ViewItemUnit").gameObject;
            _ItemContentView = _scrollItemView.transform.Find("Viewport/MenuItemContent").gameObject;
            _PlayerStatusView = _itemMenu.transform.Find("PlayerStatus").gameObject;

            //オプションビューを取得
            _OptionDetailView = _ItemDetail.transform.Find("OptionDetail/OptionDetailPanel/OptionDetailView").gameObject;
            _OptionDetailSelectUnit = _ItemDetail.transform.Find("OptionDetail/OptionDetailPanel/OptionDetailSelectUnit").gameObject;
            _OptionDetailViewContent = _OptionDetailView.transform.Find("Viewport/OptionDetailViewContent").gameObject;
            _OptionDetaiDescription = _ItemDetail.transform.Find("OptionDetail/OptionDetailPanel/OptionDetaiDescription").gameObject;

            //アイテムサブメニューを取得
            _MenuItemAction = _itemMenu.transform.Find("MenuItemActionSelect").gameObject;
            _MenuItemActionPosition = _MenuItemAction.transform.Find("SubMenuPosition").gameObject;
            _MenuItemActionItem = _MenuItemAction.transform.Find("MenuItemActionItem").gameObject;

            //UI
            _UIBase = _itemMenu.transform.Find("MenuUIButton").gameObject;

            //アイテムの初期選択位置を更新する
            _LastTarget.Add((int)ItemType.All, null);
            _LastTarget.Add((int)ItemType.Weapon, null);
            _LastTarget.Add((int)ItemType.Shield, null);
            _LastTarget.Add((int)ItemType.Ring, null);
            _LastTarget.Add((int)ItemType.Food, null);
            _LastTarget.Add((int)ItemType.Candy, null);
            _LastTarget.Add((int)ItemType.Melody, null);
            _LastTarget.Add((int)ItemType.Ball, null);
            _LastTarget.Add((int)ItemType.Bag, null);
            _LastTarget.Add((int)ItemType.Other, null);
            _LastTarget.Add((int)ItemType.Material, null);
            _LastTarget.Add(_DriveItem, null);

            nowType = (int)ItemType.All;

            _ChoicesUnit.SetActive(false);
            _itemSelectedPrefab.SetActive(false);
            _itemMenu.SetActive(false);
            _MenuItemAction.SetActive(false);
            _MenuItemActionItem.SetActive(false);
            _ItemTabUnit.SetActive(false);
            _OptionDetailSelectUnit.SetActive(false);

        }

        public SelectTurnState UpdateDisplay(SelectTurnState nowState, PlayerCharacter player)
        {
            TempState = nowState;
            if (_wait > 0)
            {
                _wait--;
                return nowState;
            }

            switch (nowState)
            {
                case SelectTurnState.MenuWarehouseOutStart:
                    nowState = SelectTurnState.MenuWarehouseOut;
                    TempMain = nowState;
                    break;
                case SelectTurnState.MenuWarehouseOut:
                    nowState = UpdateItemMenu(nowState, player);
                    break;
                case SelectTurnState.MenuWarehouseInStart:
                    nowState = SelectTurnState.MenuWarehouseOut;
                    TempMain = nowState;
                    break;
                case SelectTurnState.MenuWarehouseIn:
                    nowState = UpdateItemMenu(nowState, player);
                    break;
                case SelectTurnState.FinishMenu:

                    FinishMenu();
                    nowState = SelectTurnState.DungeonSelect;
                    break;
                default:
                    break;
            }

            return nowState;
        }
        #region アイテムメニュー

        public void InitializeWarehouseIn()
        {

            TempItems = GameStateInformation.TempItemList;

            InitializeItemMenu();
        }
        public void InitializeWarehouseOut()
        {

            TempItems = GameStateInformation.WarehouseItems;

            InitializeItemMenu();
        }

        private void InitializeItemMenu(bool issort = false)
        {
            CommonFunction.SetActive(_itemMenu, true);

            switch (KeyControlInformation.Info.OpMode)
            {
                case OperationMode.KeyOnly:
                    CommonFunction.SetActive(_UIBase, false);

                    break;
                case OperationMode.UseMouse:
                    CommonFunction.SetActive(_UIBase, true);

                    break;
            }

            List<BaseItem> list = new List<BaseItem>();

            //ItemTypeをベースにタブ項目を作る
            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (ItemType val in CommonFunction.EnumItemTypes)
            {
                //メンバ名を取得する
                string eName = Enum.GetName(typeof(ItemType), val);
                names.Add((int)val, eName);
            }
            InitializeTabs(names);

            //Allならすべて、そうでなければ対象だけ
            if (nowType == (int)ItemType.All)
            {

                foreach (BaseItem i in TempItems)
                {
                    //ソートがオンなら
                    if (issort == true)
                    {
                        i.SortNo = i.ObjNo;
                    }

                    //格納オブジェクトの場合
                    if (i.IsDrive == true)
                    {
                    }
                    else
                    {
                        list.Add(i);
                    }
                }

                //ソート
                list = list.OrderBy(i => i.SortNo).ToList();
            }
            else
            {

                foreach (BaseItem i in TempItems)
                {
                    if ((int)i.IType != nowType)
                    {
                        continue;
                    }
                    //ソートがオンなら
                    if (issort == true)
                    {
                        i.SortNo = i.ObjNo;
                    }
                    //格納オブジェクトの場合
                    if (i.IsDrive == true)
                    {
                        //格納元が表示許可を出しているか調査
                        BagBase temp = i.InDrive;
                        if (temp.IsDisplayContents == true)
                        {
                            list.Add(i);
                        }
                    }
                    else
                    {
                        list.Add(i);
                    }
                }


                list = list.OrderBy(i => i.SortNo).ToList();
            }

            //アイテムリストを作成する
            InitializeItems(list);

            //タブの設定
            SetTabSelect(nowType);

            //ステータス情報の初期化
            InitializeStatus();
        }

        private void InitializeTabs(Dictionary<int, string> tabs)
        {
            //前回のタブを削除
            foreach (GameObject g in _ItemTabs.Values)
            {
                DestroyObject(g);
            }
            _ItemTabs.Clear();

            int i = 0;

            foreach (int k in tabs.Keys)
            {
                //コピーを作成

                GameObject obj = CommonFunction.GetObject(TabsStack, _ItemTabUnit, _ItemTabParent.transform);
                obj.transform.localPosition = new Vector3(TabDefaultX + i * TabDefaultUnit,
                    TabDefaultY,
                    CommonConst.SystemValue.UiVectorZ);
                //obj.SetActive(true);

                //イベントハンドラの設定
                CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickTab(e, k));

                obj.GetComponentInChildren<Text>().text = tabs[k];


                _ItemTabs.Add(k, obj);
                //obj.SetActive(true);
                SetTabUnSelect(k);

                i++;
            }
        }

        private void InitializeItems(List<BaseItem> list)
        {
            //前回の表示項目を削除
            foreach (GameObject g in _ListViewItemUnits.Values)
            {
                //UnityEngine.Object.Destroy(g);
                CommonFunction.DestroyObject(g, _scrollItemView.transform);
            }
            _ListViewItemUnits.Clear();

            int index = 0;

            // 表示対象のアイテムの種類の数だけノードを生成
            foreach (BaseItem item in list)
            {
                index++;

                //コピーを作成
                GameObject obj = CommonFunction.GetObject(ItemsStack, _itemSelectedPrefab, _ItemContentView.transform);
                SetItemValue(obj, item, index);
                CommonFunction.SetItemUnSelectBack(obj);

                //イベントハンドラの設定
                CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickItemMenu(e, item));
                CommonFunction.AddListenerMenu(obj, EventTriggerType.Drag, e => OnDragItemMenu(e), false);
                CommonFunction.AddListenerMenu(obj, EventTriggerType.Scroll, e => OnScrollItemMenu(e), false);

                _ListViewItemUnits.Add(item, obj);
                //obj.SetActive(true);
            }

            //コンテナ内の高さを決める
            _ItemContentView.transform.GetComponent<RectTransform>().sizeDelta
                = new Vector2(0, _ListViewItemUnits.Count * ViewItemUnitHeight + 25);

            //一件以上ある場合
            if (_ListViewItemUnits.Count > 0)
            {
                //選択対象が存在しない場合は初期値を入れる
                if (CommonFunction.IsNull(_LastTarget[nowType]) == true || _ListViewItemUnits.ContainsKey(_LastTarget[nowType]) == false)
                {
                    _LastTarget[nowType] = _ListViewItemUnits.Keys.First();
                }
                float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
                int indexx = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
                int n = _ListViewItemUnits.Count;
                CommonFunction.SetCenterViewItem(height, _scrollItemView, indexx, n);

                //選択位置の背景を設定
                CommonFunction.SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);

                //詳細の更新
                SetDetailDisplay(_LastTarget[nowType]);

            }
            //アイテムが全くない場合
            else
            {
                //名前
                _ItemDetail.transform.Find("DetailItemName").GetComponent<Text>().text = "";

                //攻撃力
                _ItemDetail.transform.Find("DetailItemAttackValue").GetComponent<Text>().text = "";

                //防御力
                _ItemDetail.transform.Find("DetailItemDefenseValue").GetComponent<Text>().text = "";

                SetOptionValues(null, false);

                //説明
                _ItemDetail.transform.Find("DetailItemDescription").GetComponent<Text>().text = "";
            }
        }

        private void FinishMenu()
        {
            //表示項目を削除
            foreach (GameObject g in _ListViewItemUnits.Values)
            {
                CommonFunction.DestroyObject(g, _scrollItemView.transform);
            }
            _ListViewItemUnits.Clear();

            TempActionList.Clear();

            _itemMenu.SetActive(false);
        }
        private void InitializeStatus()
        {
            //Lv
            _PlayerStatusView.transform.Find("MenuStLevelValue").GetComponent<Text>().text =
                DisplayInformation.Info.Level.ToString();

            //Power
            _PlayerStatusView.transform.Find("MenuStPowerValue").GetComponent<Text>().text =
                string.Format(CommonConst.Format.PerNumber, DisplayInformation.Info.PowerValue, DisplayInformation.Info.PowerMax);

            //Hp
            _PlayerStatusView.transform.Find("MenuStHpValue").GetComponent<Text>().text =
                string.Format(CommonConst.Format.PerNumber, DisplayInformation.Info.HpValue, DisplayInformation.Info.HpMax);

            //満腹
            _PlayerStatusView.transform.Find("MenuStSatValue").GetComponent<Text>().text =
                string.Format(CommonConst.Format.PerNumber, DisplayInformation.Info.SatietyValue, DisplayInformation.Info.SatietyMax);

            //状態
            _PlayerStatusView.transform.Find("MenuStStateValue").GetComponent<Text>().text =
                CommonFunction.GetStateNames(DisplayInformation.Info.State);

            //次のレベル
            _PlayerStatusView.transform.Find("MenuStNextLevelValue").GetComponent<Text>().text =
                string.Format(CommonConst.Format.PerNumber, DisplayInformation.Info.LvExpValue, DisplayInformation.Info.LvExpMax);

            //所持数
            _PlayerStatusView.transform.Find("MenuStItemCountValue").GetComponent<Text>().text =
                string.Format(CommonConst.Format.PerNumber, Mathf.FloorToInt(PlayerCharacter.ItemCount()), DisplayInformation.Info.ItemMaxCount);
        }
        private void SetDetailDisplay(BaseItem item)
        {
            //名前
            _ItemDetail.transform.Find("DetailItemName").GetComponent<Text>().text =
                item.GetName();

            //攻撃力
            _ItemDetail.transform.Find("DetailItemAttackValue").GetComponent<Text>().text =
                item.GetAttack();

            //防御力
            _ItemDetail.transform.Find("DetailItemDefenseValue").GetComponent<Text>().text =
                item.GetDefense();

            SetOptionValues(item.Options,item.IsAnalyse);

            //説明
            _ItemDetail.transform.Find("DetailItemDescription").GetComponent<Text>().text =
                item.GetDescription();
        }
        private SelectTurnState UpdateItemMenu(SelectTurnState nowState, PlayerCharacter player)
        {
            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                //表示リストが0だったら無視
                if (_ListViewItemUnits.Count == 0)
                {

                }
                //最初の項目だったら無視
                else if (_ListViewItemUnits.Keys.First() == _LastTarget[nowType])
                {

                }
                //それ以外だったら1つ上の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                    //選択中の番号を取得
                    int index = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
                    index--;
                    _LastTarget[nowType] = _ListViewItemUnits.Keys.ElementAt(index);
                    //詳細の更新
                    SetDetailDisplay(_LastTarget[nowType]);
                    CommonFunction.SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);


                    float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
                    int n = _ListViewItemUnits.Count;
                    CommonFunction.SetCenterViewItem(height, _scrollItemView, index, n);

                }
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                //表示リストが0だったら無視
                if (_ListViewItemUnits.Count == 0)
                {

                }
                //最後の項目だったら無視
                else if (_ListViewItemUnits.Keys.Last() == _LastTarget[nowType])
                {

                }
                //それ以外だったら1つ下の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                    //選択中の番号を取得
                    int index = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
                    index++;
                    _LastTarget[nowType] = _ListViewItemUnits.Keys.ElementAt(index);
                    //詳細の更新
                    SetDetailDisplay(_LastTarget[nowType]);
                    CommonFunction.SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);

                    float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
                    int n = _ListViewItemUnits.Count;
                    CommonFunction.SetCenterViewItem(height, _scrollItemView, index, n);
                }
            }
            //左
            else if (KeyControlInformation.Info.OnMoveLeft())
            {

                //ドライブを見るの場合無視
                if (nowState == SelectTurnState.LookDrive)
                {

                }
                //最初の項目だったら無視
                else if (nowType == 1)
                {

                }
                //それ以外だったら1つ左のタブに移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    SetTabUnSelect(nowType);
                    nowType--;
                    InitializeItemMenu();
                }
            }
            //右
            else if (KeyControlInformation.Info.OnMoveRight())
            {
                //ドライブを見るの場合無視
                if (nowState == SelectTurnState.LookDrive)
                {
                }
                //最後の項目だったら無視
                else if (nowType == ItemMenuTabsCount)
                {

                }
                //それ以外だったら1つ右のタブに移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    SetTabUnSelect(nowType);
                    nowType++;
                    InitializeItemMenu();
                }
            }
            //キャンセル
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
            {

                //ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                //ドライブを見るの場合
                if (nowState == SelectTurnState.LookDrive)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                    nowType = beforeType;
                    _LastTarget[nowType] = beforeTarget;
                    nowState = TempMain;
                    InitializeItemMenu();
                    FinishSub();
                }
                else
                {

                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                    nowState = SelectTurnState.FinishMenu;
                }
            }
            //オプション参照
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.LookOption))
            {
                if (CommonFunction.IsNull(_ViesOptions) == false
                    && _ViesOptions.Count >= 1)
                {

                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                    InitializeOption();
                    nowState = SelectTurnState.ItemOption;
                }
            }
            //ソート
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.ItemSort))
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                //ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                SetTabUnSelect(nowType);
                InitializeItemMenu(true);
            }
            //複数選択
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuMultiSelectOk))
            {
                //表示リストが0だったら無視
                if (_ListViewItemUnits.Count == 0)
                {
                }
                else if (nowState != SelectTurnState.LookDrive)
                {
                    SetItemValue(_ListViewItemUnits[_LastTarget[nowType]], _LastTarget[nowType], _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]));
                }
            }
            //決定
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
            {

                nowState = ExeItemMenu(nowState, player);
            }

            return nowState;
        }
        public void OnClickTab(BaseEventData eventData, int tab)
        {

            if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                SetTabUnSelect(tab);
                nowType = tab;
                InitializeItemMenu();
            }
        }

        public void OnClickItemMenu(BaseEventData eventData, BaseItem i)
        {
            if (TempState == SelectTurnState.LookDrive)
            {
                if (KeyControlInformation.Info.OnRightClick(eventData) == true)
                {
                    KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
                }
            }
            if (TempState != SelectTurnState.ItemSubMenu)
            {
                return;
            }
            if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
            {
                if (CommonFunction.IsDoubleClick())
                {
                    KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                }
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    CommonFunction.SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                    //選択中の番号を取得
                    _LastTarget[nowType] = i;
                    //詳細の更新
                    SetDetailDisplay(_LastTarget[nowType]);
                    CommonFunction.SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);

                    CommonFunction.SetDragScrollViewFirstPosition(_scrollItemView);
                }
            }
            else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
            }
        }

        public void OnDragItemMenu(BaseEventData eventData)
        {
            float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
            int n = _ListViewItemUnits.Count;

            CommonFunction.SetDragScrollViewItem(eventData, height, _scrollItemView, ViewItemUnitHeight, n);
        }
        public void OnScrollItemMenu(BaseEventData eventData)
        {
            float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
            int n = _ListViewItemUnits.Count;

            CommonFunction.SetScrollScrollViewItem(eventData, height, _scrollItemView, ViewItemUnitHeight, n);
        }

        private SelectTurnState ExeItemMenu(SelectTurnState nowState, PlayerCharacter player)
        {

            //ドライブを見るの場合無視
            if (nowState == SelectTurnState.LookDrive)
            {

            }
            //表示リストが0だったら無視
            else if (_ListViewItemUnits.Count == 0)
            {
            }
            else
            {

            }
            return nowState;
        }

        /// <summary>
        /// 表に表示する項目の設定
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="item"></param>
        private void SetItemValue(GameObject obj, BaseItem item, int index)
        {
            //背景　
            obj.transform.Find("SelectedBackOdd").gameObject.SetActive(index % 2 == 0);

            //タイプ
            int typenum = (int)item.IType;
            typenum--;
            obj.transform.Find("TypeImage").GetComponent<Image>().sprite = ItemSprite[typenum];
            //obj.transform.FindChild("TypeImage").GetComponent<RawImage>().texture = ItemTypeTexture[typenum];
            //obj.transform.Find("TypeName").GetComponent<Text>().text = item.GetTypeName();

            //名前
            //if (DeleteList.Contains(item) == true)
            {
                obj.transform.Find("ItemName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.SelectItem, _LastTarget[nowType].DisplayNameInItem);
            }
            //else
            {
                obj.transform.Find("ItemName").GetComponent<Text>().text = item.DisplayNameInItem;
            }

            //保存ドライブ
            obj.transform.Find("DriveName").GetComponent<Text>().text = item.GetDriveName();
            //装備中
            if (item.IsEquip == true)
            {
                obj.transform.Find("TextIsEquip").GetComponent<Text>().text = "装備中";
            }
            else if (PlayerCharacter.ItemList.Contains(item) == false)
            {
                obj.transform.Find("TextIsEquip").GetComponent<Text>().text = "足元";
            }
            else
            {
                obj.transform.Find("TextIsEquip").GetComponent<Text>().text = "";
            }
        }

        #region アイテムメニュー関連

        #region　オプション
        private void SetOptionValues(List<BaseOption> options,bool isAnalyse)
        {
            _ViesOptions = options;

            //前の情報を削除
            foreach (GameObject g in _ListViewOptionUnits.Values)
            {
                CommonFunction.DestroyObject(g, _OptionDetailView.transform);
            }
            _ListViewOptionUnits.Clear();

            _OptionDetaiDescription.GetComponent<Text>().text = "";

            //表示対象がなければ終了
            if (CommonFunction.IsNull(options) == true)
            {
                if (isAnalyse == true)
                {
                    _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text =
                        "-";
                }
                else
                {
                    _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text =
                        "?";
                }
                return;
            }

            if (isAnalyse == true)
            {
                _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text =
                options.Count().ToString();
            }
            else
            {
                _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text =
                    "?";
                return;
            }

            int index = 0;
            foreach (BaseOption bo in options)
            {
                GameObject copy = CommonFunction.GetObject(OptionStack, _OptionDetailSelectUnit, _OptionDetailViewContent.transform);
                //GameObject copy = GameObject.Instantiate(_OptionDetailSelectUnit, _OptionDetailViewContent.transform);
                CommonFunction.SetItemUnSelectBack(copy);
                //copy.SetActive(true);
                copy.GetComponentInChildren<Text>().text = bo.DisplayNameInItem;
                copy.transform.Find("OptionSelectedOdd").gameObject.SetActive(index % 2 == 0);


                //イベントハンドラの設定
                CommonFunction.AddListenerMenu(copy, EventTriggerType.PointerDown, e => OnClickOption(e, bo));
                CommonFunction.AddListenerMenu(copy, EventTriggerType.Drag, e => OnDragOption(e), false);
                CommonFunction.AddListenerMenu(copy, EventTriggerType.Scroll, e => OnScrollOption(e), false);

                _ListViewOptionUnits.Add(bo, copy);
                index++;
            }
        }
        private void InitializeOption()
        {
            if (CommonFunction.IsNull(optionTarget) || _ListViewOptionUnits.ContainsKey(optionTarget) == false)
            {
                optionTarget = _ViesOptions[0];
            }
            CommonFunction.SetItemSelectBack(_ListViewOptionUnits[optionTarget]);
            _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
        }
        private TurnState UpdateOption(TurnState nowState)
        {
            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                //表示リストが0だったら無視
                if (_ViesOptions.Count == 0)
                {

                }
                //最初の項目だったら無視
                else if (_ListViewOptionUnits.Keys.First() == optionTarget)
                {

                }
                //それ以外だったら1つ上の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                    //選択中の番号を取得
                    int index = _ListViewOptionUnits.Keys.ToList().IndexOf(optionTarget);
                    index--;
                    optionTarget = _ListViewOptionUnits.Keys.ElementAt(index);
                    //詳細の更新
                    _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
                    CommonFunction.SetItemSelectBack(_ListViewOptionUnits[optionTarget]);


                    //コンテナの高さを決める
                    float height = _ListViewOptionUnits.Count * OptionUnit;
                    int idx = index;
                    int n = _ListViewOptionUnits.Count;

                    CommonFunction.SetCenterViewItem(height, _OptionDetailView, idx, n);


                }
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                //表示リストが0だったら無視
                if (_ViesOptions.Count == 0)
                {

                }
                //最後の項目だったら無視
                else if (_ListViewOptionUnits.Keys.Last() == optionTarget)
                {

                }
                //それ以外だったら1つ下の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                    //選択中の番号を取得
                    int index = _ListViewOptionUnits.Keys.ToList().IndexOf(optionTarget);
                    index++;
                    optionTarget = _ListViewOptionUnits.Keys.ElementAt(index);
                    //詳細の更新
                    _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
                    CommonFunction.SetItemSelectBack(_ListViewOptionUnits[optionTarget]);


                    //コンテナの高さを決める
                    float height = _ListViewOptionUnits.Count * OptionUnit;
                    int idx = index;
                    int n = _ListViewOptionUnits.Count;

                    CommonFunction.SetCenterViewItem(height, _OptionDetailView, idx, n);

                    //SetCenterViewOption();
                }
            }
            //キャンセル
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                //ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                _OptionDetaiDescription.GetComponent<Text>().text = "";
                CommonFunction.SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                nowState = TurnState.ItemMenu;
            }
            return nowState;
        }


        public void OnClickOption(BaseEventData eventData, BaseOption i)
        {
            if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();
                if (CommonFunction.IsNull(optionTarget) == false && _ListViewOptionUnits.ContainsKey(optionTarget) == true)
                {
                    CommonFunction.SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                }
                //選択中の番号を取得
                optionTarget = i;
                //詳細の更新
                _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
                CommonFunction.SetItemSelectBack(_ListViewOptionUnits[optionTarget]);

                CommonFunction.SetDragScrollViewFirstPosition(_OptionDetailView);
            }
        }

        public void OnDragOption(BaseEventData eventData)
        {
            float height = _ListViewOptionUnits.Count * OptionUnit;
            int n = _ListViewOptionUnits.Count;

            CommonFunction.SetDragScrollViewItem(eventData, height, _OptionDetailView, OptionUnit, n);
        }
        public void OnScrollOption(BaseEventData eventData)
        {
            float height = _ListViewOptionUnits.Count * OptionUnit;
            int n = _ListViewOptionUnits.Count;

            CommonFunction.SetScrollScrollViewItem(eventData, height, _OptionDetailView, OptionUnit, n);
        }

        #endregion オプション

        #region アイテム使用メニュー

        private void InitializeItemSubMenu()
        {
            //前回の表示項目を削除
            foreach (GameObject g in _ListViewItemSubUnits.Values)
            {
                UnityEngine.Object.Destroy(g);
            }
            _ListViewItemSubUnits.Clear();

            _MenuItemAction.SetActive(true);
            _MenuItemAction.transform.localPosition = new Vector3(SubMenuDefaultX, SubMenuDefaultY, CommonConst.SystemValue.UiVectorZ);
            //選択されたアイテムを取得
            BaseItem item = _LastTarget[nowType];

            //一番上を選択状態にする
            subSelected = 0;

            //表示対象を取得
            _subItemMenu = item.GetItemAction();

            sbyte i = 0;
            foreach (MenuItemActionType target in _subItemMenu.Values)
            {
                //コピーを作成
                GameObject obj = CommonFunction.SetItemChild(_MenuItemActionItem, _MenuItemActionPosition);

                //行動名の設定
                obj.transform.Find("Text").GetComponent<Text>().text = CommonFunction.MenuItemActionTypeName[target];
                obj.transform.localPosition = new Vector3(0, SubViewItemUnitHeight * i, 0);
                CommonFunction.SetItemUnSelectBack(obj);
                _ListViewItemSubUnits.Add(target, obj);

                //イベントハンドラの設定
                CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickSubMenu(e, target));

                obj.SetActive(true);

                i++;
            }
            subSelected = _ListViewItemSubUnits.Keys.First();


            //一件目を選択状態にする
            CommonFunction.SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
        }

        private TurnState UpdateItemSubMenu(TurnState nowState, PlayerCharacter player)
        {
            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                //表示リストが0だったら無視
                if (_ListViewItemSubUnits.Count == 0)
                {

                }
                //最初の項目だったら無視
                else if (_ListViewItemSubUnits.Keys.First() == subSelected)
                {

                }
                //それ以外だったら1つ上の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                    //選択中の番号を取得
                    int index = _ListViewItemSubUnits.Keys.ToList().IndexOf(subSelected);
                    index--;
                    subSelected = _ListViewItemSubUnits.Keys.ElementAt(index);
                    //背景を更新
                    CommonFunction.SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
                }
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                //表示リストが0だったら無視
                if (_ListViewItemUnits.Count == 0)
                {

                }
                //最後の項目だったら無視
                else if (_ListViewItemSubUnits.Keys.Last() == subSelected)
                //else if (subSelected == _ListViewItemSubUnits.Count - 1)
                {

                }
                //それ以外だったら1つ下の項目に移動
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    ManageWait.Info.WaitSelect();
                    CommonFunction.SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                    //選択中の番号を取得
                    int index = _ListViewItemSubUnits.Keys.ToList().IndexOf(subSelected);
                    index++;
                    subSelected = _ListViewItemSubUnits.Keys.ElementAt(index);
                    //背景を更新
                    CommonFunction.SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
                }
            }

            //キャンセル
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
            //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                //ステートをメニューに返す
                nowState = TurnState.ItemMenu;
                nowType = beforeType;
                _LastTarget[nowType] = beforeTarget;
                InitializeItemMenu();
                FinishSub();
            }
            //決定
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
            //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
            {
                nowState = ExeSubMenu(nowState, player);
            }
            //else if (IsClick == true)
            //{
            //    nowState = TempState;
            //    IsClick = false;
            //}

            return nowState;
        }

        public void OnClickSubMenu(BaseEventData eventData, MenuItemActionType a)
        {

            if (TempState != SelectTurnState.ItemSubMenu)
            {
                return;
            }

            if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
            {
                if (CommonFunction.IsDoubleClick())
                {
                    KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                    //TempState = ExeSubMenu();
                    //IsClick = true;
                }
                else
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                    CommonFunction.SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                    subSelected = a;
                    CommonFunction.SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
                }
            }
            else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
            }
        }

        public TurnState ExeSubMenu(TurnState nowState, PlayerCharacter player)
        {


            if (nowState == TurnState.PlayerItemUse)
            {
                FinishAllMenu();
            }

            return nowState;
        }

        public void FinishSub()
        {

            //遊びを入れる
            ManageWait.Info.WaitSelect();

            //前回の項目を削除
            foreach (GameObject g in _ListViewItemSubUnits.Values)
            {
                UnityEngine.Object.Destroy(g);
            }
            _ListViewItemSubUnits.Clear();

            //サブメニューの閉じ処理
            _MenuItemAction.SetActive(false);

        }

        public void FinishAllMenu()
        {
            FinishSub();
            FinishMenu();

        }

        #endregion アイテム使用メニュー


        /// <summary>
        /// 対象位置のタブ背景を非選択状態にする
        /// </summary>
        private void SetTabUnSelect(int num)
        {
            GameObject obj = GetTab(num);
            Image col = obj.transform.GetComponent<Image>();
            col.color = Color.white;
        }

        /// <summary>
        /// 対象位置のタブ背景を選択状態にする
        /// </summary>
        private void SetTabSelect(int num)
        {
            GameObject obj = GetTab(num);
            Image col = obj.transform.GetComponent<Image>();
            col.color = Color.yellow;
        }

        private GameObject GetTab(int num)
        {
            return _ItemTabs[num];
        }
        #endregion アイテムメニュー関連

        #endregion アイテムメニュー


    }
}

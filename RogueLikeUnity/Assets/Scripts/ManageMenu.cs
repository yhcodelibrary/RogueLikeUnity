using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageMenu : MonoBehaviour
{
    private GameObject _itemSelectedPrefab;
    private GameObject _itemMenu;
    private GameObject _firstMenu;
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
    private Dictionary<FirstMenuType ,GameObject> _ListViewFirstUnits;
    private Dictionary<BaseItem, GameObject> _ListViewItemUnits;
    private Dictionary<BaseOption, GameObject> _ListViewOptionUnits;
    private Dictionary<MenuItemActionType, GameObject> _ListViewItemSubUnits;
    private Dictionary<int, GameObject> _ItemTabs;
    private List<GameObject> ItemsStack;
    private List<GameObject> TabsStack;
    private List<GameObject> OptionStack;

    //public Material[] ItemTypeMaterials;
    //public Texture[] ItemTypeTexture;
    public Sprite[] ItemSprite;

    //ファーストメニュー系
    public FirstMenuType firstSelected;
    private const float FirstSelectDefaultX = 10f;
    private const float FirstSelectDefaultY = -10;
    private const float FirstSelectUnitY = -25;

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
    private List<BaseItem> DeleteList;

    //オプション
    private BaseOption optionTarget;
    private const float OptionUnit = 20;
    private List<BaseOption> _ViesOptions;

    private TurnState TempState;
    private TurnState BeforeState;


    //サブメニュー系
    public MenuItemActionType subSelected;
    private Dictionary<int, MenuItemActionType> _subItemMenu;
    private const float SubViewItemUnitHeight = -25;
    private const float SubMenuDefaultX = -410;
    private const float SubMenuDefaultY = 155;
    private const float SubMenuSelectDefaultX = -100;
    private const float SubMenuSelectDefaultY = 155;
    public BaseItem FirstSectTemp;

    /// <summary>
    /// 初期画面の選択項目数
    /// </summary>
    private const sbyte ItemMenuTabsCount = 11;


    public void Start()
    {
        _ListViewFirstUnits = new Dictionary<FirstMenuType, GameObject>(new FirstMenuTypeComparer());
        _ListViewItemUnits = new Dictionary<BaseItem, GameObject>(new BaseItemComparer());
        _ListViewItemSubUnits = new Dictionary<MenuItemActionType, GameObject>(new MenuItemActionTypeComparer());
        _ListViewOptionUnits = new Dictionary<BaseOption, GameObject>(new BaseOptionComparer());
        _LastTarget = new Dictionary<int, BaseItem>();
        _ItemTabs = new Dictionary<int, GameObject>();
        DeleteList = new List<BaseItem>();
        ItemsStack = new List<GameObject>();
        TabsStack = new List<GameObject>();
        OptionStack = new List<GameObject>();

        //ファーストメニューのオブジェクト追加
        _firstMenu = GameObject.Find("MenuFirstPanel");
        _ChoicesUnit = _firstMenu.transform.Find("ChoicesUnit").gameObject;
        //_ListViewFirstUnits.Add(_firstMenu.transform.FindChild("FirstMenuItem").gameObject);
        //_ListViewFirstUnits.Add(_firstMenu.transform.FindChild("FirstMenuFoot").gameObject);
        //_ListViewFirstUnits.Add(_firstMenu.transform.FindChild("FirstMenuSystem").gameObject);

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

        //詳細メニューの言語更新
        _ItemDetail.transform.Find("DetailItemAttack").GetComponent<Text>().text = CommonConst.Message.ATK;
        _ItemDetail.transform.Find("DetailItemDefense").GetComponent<Text>().text = CommonConst.Message.DEF;
        _ItemDetail.transform.Find("DetailItemOptionCount").GetComponent<Text>().text = CommonConst.Message.Options;

        //プレイヤーステータスの言語更新
        _PlayerStatusView.transform.Find("MenuStSat").GetComponent<Text>().text = CommonConst.Message.Satiety;
        _PlayerStatusView.transform.Find("MenuStState").GetComponent<Text>().text = CommonConst.Message.State; 
        _PlayerStatusView.transform.Find("MenuStNextLevel").GetComponent<Text>().text = CommonConst.Message.NextLv;
        _PlayerStatusView.transform.Find("MenuStItemCount").GetComponent<Text>().text = CommonConst.Message.ItemWeight;

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
        _firstMenu.SetActive(false);
        _itemSelectedPrefab.SetActive(false);
        _itemMenu.SetActive(false);
        _MenuItemAction.SetActive(false);
        _MenuItemActionItem.SetActive(false);
        _ItemTabUnit.SetActive(false);
        _OptionDetailSelectUnit.SetActive(false);
        
    }

    public TurnState UpdateDisplay(TurnState nowState, PlayerCharacter player)
    {
        TempState = nowState;

        switch (nowState)
        {
            case TurnState.FirstMenu:
                nowState = UpdateFirstMenu(nowState);
                break;
            case TurnState.ItemMenu:
                nowState = UpdateItemMenu(nowState, player);
                //InitializeStatus();
                break;
            case TurnState.ItemOption:
                nowState = UpdateOption(nowState);
                break;
            case TurnState.ItemSubMenu:
                nowState = UpdateItemSubMenu(nowState,player);
                break;
            case TurnState.ItemInDrive:
                nowState = UpdateItemMenu(nowState, player);

                break;
            case TurnState.ItemInDriveFromDrive:
                nowState = UpdateItemMenu(nowState, player);
                break;
            case TurnState.LookDrive:
                nowState = UpdateItemMenu(nowState, player);
                break;
            case TurnState.ItemTargetSelect:
                nowState = UpdateItemMenu(nowState, player);
                break;
            case TurnState.ItemDelete:
                nowState = UpdateItemMenu(nowState, player);
                break;
            case TurnState.ItemAnalyse:
                nowState = UpdateItemMenu(nowState, player);
                break;
            default:
                break;
        }

        return nowState;
    }


    #region ファーストメニュー

    /// <summary>
    /// ファーストメニューの初期化
    /// </summary>
    public void InitializeAtelier()
    {
        _firstMenu.SetActive(true);
        firstSelected = FirstMenuType.Atelier;

        //前回のタブを削除
        foreach (GameObject g in _ListViewFirstUnits.Values)
        {
            UnityEngine.Object.Destroy(g);
        }
        _ListViewFirstUnits.Clear();

        int i = 0;

        GameObject obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Synthesis;
        _ListViewFirstUnits.Add(FirstMenuType.Atelier, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.Atelier));
        SetItemSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Strength;
        _ListViewFirstUnits.Add(FirstMenuType.AtelierStrength, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.AtelierStrength));
        SetItemUnSelectBack(obj);
        i++;

        if(DungeonInformation.Info.IsAnalyze == true)
        {
            obj = SetItemChild(_ChoicesUnit, _firstMenu);
            obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
                FirstSelectDefaultY + i * FirstSelectUnitY,
                CommonConst.SystemValue.UiVectorZ);
            obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Appraisal;
            _ListViewFirstUnits.Add(FirstMenuType.ItemAnalyse, obj);
            obj.SetActive(true);
            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.ItemAnalyse));
            SetItemUnSelectBack(obj);
            i++;
        }

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.DiscardItems;
        _ListViewFirstUnits.Add(FirstMenuType.ItemDelete, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.ItemDelete));
        SetItemUnSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Close;
        _ListViewFirstUnits.Add(FirstMenuType.NotStair, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.NotStair));
        SetItemUnSelectBack(obj);
        i++;
    }

    /// <summary>
    /// ファーストメニューの初期化
    /// </summary>
    public void InitializeStair()
    {
        _firstMenu.SetActive(true);
        firstSelected = FirstMenuType.GetStair;

        //前回のタブを削除
        foreach (GameObject g in _ListViewFirstUnits.Values)
        {
            UnityEngine.Object.Destroy(g);
        }
        _ListViewFirstUnits.Clear();

        int i = 0;

        GameObject obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.MoveOn;
        _ListViewFirstUnits.Add(FirstMenuType.GetStair, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.GetStair));
        SetItemSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.SaveAndStop;
        _ListViewFirstUnits.Add(FirstMenuType.Save, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.Save));
        SetItemUnSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Stay;
        _ListViewFirstUnits.Add(FirstMenuType.NotStair, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.NotStair));
        SetItemUnSelectBack(obj);
        i++;
    }
    /// <summary>
    /// ファーストメニューの初期化
    /// </summary>
    public void InitializeFirstMenu()
    {
        _firstMenu.SetActive(true);
        firstSelected = FirstMenuType.Item;
        
        //前回のタブを削除
        foreach (GameObject g in _ListViewFirstUnits.Values)
        {
            UnityEngine.Object.Destroy(g);
        }
        _ListViewFirstUnits.Clear();

        int i = 0;

        GameObject obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.ItemMenu;
        _ListViewFirstUnits.Add(FirstMenuType.Item, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListener(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.Item));
        SetItemSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.ExaminingFoot;
        _ListViewFirstUnits.Add(FirstMenuType.Foot, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListener(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.Foot));
        SetItemUnSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.System;
        _ListViewFirstUnits.Add(FirstMenuType.System, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListener(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.System));
        SetItemUnSelectBack(obj);
        i++;

        obj = SetItemChild(_ChoicesUnit, _firstMenu);
        obj.transform.localPosition = new Vector3(FirstSelectDefaultX,
            FirstSelectDefaultY + i * FirstSelectUnitY,
            CommonConst.SystemValue.UiVectorZ);
        obj.GetComponentsInChildren<Text>().Last().text = CommonConst.Message.Close;
        _ListViewFirstUnits.Add(FirstMenuType.NotStair, obj);
        obj.SetActive(true);
        //イベントハンドラの設定
        CommonFunction.AddListener(obj, EventTriggerType.PointerDown, e => OnClickFirstMenu(e, FirstMenuType.NotStair));
        SetItemUnSelectBack(obj);
        i++;
    }
    private TurnState UpdateFirstMenu(TurnState nowState)
    {
        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //最初の項目だったら無視
            if (_ListViewFirstUnits.Keys.First() == firstSelected)
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewFirstUnits[firstSelected]);
                //選択中の番号を取得
                int index = _ListViewFirstUnits.Keys.ToList().IndexOf(firstSelected);
                index--;
                firstSelected = _ListViewFirstUnits.Keys.ElementAt(index);
                //詳細の更新
                SetItemSelectBack(_ListViewFirstUnits[firstSelected]);
            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {
            //最後の項目だったら無視
            if (_ListViewFirstUnits.Keys.Last() == firstSelected)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewFirstUnits[firstSelected]);
                //選択中の番号を取得
                int index = _ListViewFirstUnits.Keys.ToList().IndexOf(firstSelected);
                index++;
                firstSelected = _ListViewFirstUnits.Keys.ElementAt(index);
                //詳細の更新
                SetItemSelectBack(_ListViewFirstUnits[firstSelected]);                
            }
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
        {
            nowState = ExeFirstMenu(nowState);
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);
            //ファーストメニューの閉じ処理
            _firstMenu.SetActive(false);

            //ステートをプレイヤーに返す
            nowState = TurnState.MenuExit;
        }
        //else if(IsClick == true)
        //{
        //    nowState = TempState;
        //    IsClick = false;
        //}

        return nowState;
    }
    public void OnClickFirstMenu(BaseEventData eventData, FirstMenuType t)
    {
        if(TempState != TurnState.FirstMenu)
        {
            return;
        }
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if (CommonFunction.IsDoubleClick())
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                //TempState = ExeFirstMenu();
                //IsClick = true;
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
                SetItemUnSelectBack(_ListViewFirstUnits[firstSelected]);
                //選択中の番号を取得
                firstSelected = t;
                //詳細の更新
                SetItemSelectBack(_ListViewFirstUnits[firstSelected]);
            }
        }
        else if(KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }

    public TurnState ExeFirstMenu(TurnState nowState)
    {
        //選択された画面を開く
        switch (firstSelected)
        {
            case FirstMenuType.Item:
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                InitializeItemMenu(TurnState.ItemMenu);
                
                nowState = TurnState.ItemMenu;
                break;
            case FirstMenuType.ItemAnalyse:
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                InitializeItemMenu(TurnState.ItemAnalyse);

                nowState = TurnState.ItemAnalyse;
                break;
            case FirstMenuType.ItemDelete:
                DeleteList.Clear();
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                InitializeItemMenu(TurnState.ItemDelete);
                
                nowState = TurnState.ItemDelete;
                break;
            case FirstMenuType.Foot:
                //足元にアイテムがある場合
                if (CommonFunction.IsNull(UseItemInformation.FootItem) == false)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                    //足元選択時の選択タイプをAllに
                    nowType = (int)ItemType.All;
                    //足元のアイテムを選択状態にしてメニューを開く
                    _LastTarget[nowType] = UseItemInformation.FootItem;
                    InitializeItemMenu(TurnState.ItemMenu);


                    nowState = TurnState.ItemMenu;
                }
                //階段だった場合
                else if (UseItemInformation.IsStair == true)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                    InitializeStair();
                }
                else
                {
                    //何もない場合
                    DisplayInformation.Info.AddMessage(CommonConst.Message.FootNoneObject);
                    ManageWait.Info.Wait(CommonConst.Wait.MenuSelect);
                }
                break;
            case FirstMenuType.System:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                nowState = TurnState.SystemMenuInit;
                break;
            case FirstMenuType.GetStair:
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Stair);
                //進む
                ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                //ファーストメニューの閉じ処理
                _firstMenu.SetActive(false);
                //ステートをプレイヤーに返す
                nowState = TurnState.NextFloor;
                break;
            case FirstMenuType.NotStair:
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);


                //ファーストメニューの閉じ処理
                _firstMenu.SetActive(false);

                //ステートをプレイヤーに返す
                nowState = TurnState.MenuExit;
                break;
            case FirstMenuType.Atelier:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                nowState = TurnState.AtelierMainInit;
                break;
            case FirstMenuType.AtelierStrength:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                nowState = TurnState.AtelierStrengthInit;
                break;

            case FirstMenuType.Save:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                nowState = TurnState.SaveInit;
                break;

            default:
                nowState = TurnState.FirstMenu;
                break;
        }

        return nowState;
    }
    #endregion ファーストメニュー

    #region アイテムメニュー関連
    private void InitializeItemMenu(TurnState state,bool issort = false)
    {
        if(state == TurnState.ItemMenu || state == TurnState.ItemDelete)
        {
            InitializeItemMenuBody(issort);
        }
        else if(state == TurnState.ItemAnalyse)
        {
            InitializeItemAnalyseMenu(issort);
        }
        else
        {
            InitializeItemMenuBody(issort);
        }
    }
    private void InitializeItemMenuBody(bool issort)
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

        //Dictionary<Guid, BaseItem> list = new Dictionary<Guid, BaseItem>();
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

            foreach (BaseItem i in PlayerCharacter.ItemList)
            {
                //ソートがオンなら
                if (issort == true)
                {
                    i.SortNo = i.GetSortNo();
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
            //足元にアイテムがあれば追加
            if (CommonFunction.IsNull(UseItemInformation.FootItem) == false)
            {
                list.Add(UseItemInformation.FootItem);
                //ドライブで格納許可がでているならその中身も追加
                if (UseItemInformation.FootItem.IType == ItemType.Bag
                    && ((BagBase)UseItemInformation.FootItem).IsDisplayContents == true)
                {
                    foreach (BaseItem i in ((BagBase)UseItemInformation.FootItem).BagItems)
                    {
                        list.Add(i);
                    }
                }
            }

            //ソート
            list = list.OrderBy(i => i.SortNo).ToList();
        }
        else
        {

            foreach (BaseItem i in PlayerCharacter.ItemList)
            {
                if ((int)i.IType != nowType)
                {
                    continue;
                }
                //ソートがオンなら
                if (issort == true)
                {
                    i.SortNo = i.GetSortNo();
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

            //足元にアイテムがあれば追加
            if (CommonFunction.IsNull(UseItemInformation.FootItem) == false)
            {
                if((int)UseItemInformation.FootItem.IType == nowType)
                {
                    list.Add(UseItemInformation.FootItem);
                }
                //ドライブで格納許可がでているならその中身も追加
                if (UseItemInformation.FootItem.IType == ItemType.Bag
                        && ((BagBase)UseItemInformation.FootItem).IsDisplayContents == true)
                {
                    foreach (BaseItem i in ((BagBase)UseItemInformation.FootItem).BagItems)
                    {
                        if ((int)i.IType != nowType)
                        {
                            continue;
                        }
                        list.Add(i);
                    }
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

    private void InitializeItemAnalyseMenu(bool issort)
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

            foreach (BaseItem i in PlayerCharacter.ItemList)
            {
                //ソートがオンなら
                if (issort == true)
                {
                    i.SortNo = i.GetSortNo();
                }
                if(i.IsAnalyse == true)
                {
                    continue;
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

            //ソート
            list = list.OrderBy(i => i.SortNo).ToList();
        }
        else
        {

            foreach (BaseItem i in PlayerCharacter.ItemList)
            {
                if ((int)i.IType != nowType)
                {
                    continue;
                }
                //ソートがオンなら
                if (issort == true)
                {
                    i.SortNo = i.GetSortNo();
                }
                if (i.IsAnalyse == true)
                {
                    continue;
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

    private void InitializeTabs(Dictionary<int,string> tabs)
    {
        //前回のタブを削除
        foreach (GameObject g in _ItemTabs.Values)
        {
            //UnityEngine.Object.Destroy(g);
            DestroyObject(g);
        }
        _ItemTabs.Clear();

        int i = 0;

        foreach (int k in tabs.Keys)
        {
            //コピーを作成
            //GameObject obj = SetItemChild(_ItemTabUnit, _ItemTabParent);
            GameObject obj = GetObject(TabsStack, _ItemTabUnit, _ItemTabParent.transform);
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
            DestroyMessageObject(g, _scrollItemView.transform);
        }
        _ListViewItemUnits.Clear();

        int index = 0;

        // 表示対象のアイテムの種類の数だけノードを生成
        foreach (BaseItem item in list)
        {
            index++;
            
            //コピーを作成
            //GameObject obj = SetItemChild(_itemSelectedPrefab, _ItemContentView);
            GameObject obj = GetMessageObject(ItemsStack, _itemSelectedPrefab, _ItemContentView.transform);
            SetItemValue(obj, item, index);
            SetItemUnSelectBack(obj);

            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickItemMenu(e, item));
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Drag, e => OnDragItemMenu(e), false);
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Scroll, e => OnScrollItemMenu(e), false);
            
            _ListViewItemUnits.Add(item ,obj);
            //obj.SetActive(true);
        }

        //コンテナ内の高さを決める
        _ItemContentView.transform.GetComponent<RectTransform>().sizeDelta
            = new Vector2(0, _ListViewItemUnits.Count * ViewItemUnitHeight + 25);

        //一件以上ある場合
        if (_ListViewItemUnits.Count > 0)
        {
            //選択対象が存在しない場合は初期値を入れる
            if ( CommonFunction.IsNull(_LastTarget[nowType]) == true || _ListViewItemUnits.ContainsKey(_LastTarget[nowType]) == false)
            {
                _LastTarget[nowType] = _ListViewItemUnits.Keys.First();
            }
            //初期選択スクロール位置を設定
            SetCenterViewItem();

            //選択位置の背景を設定
            SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);

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

            ClearOptionValue();

            //説明
            _ItemDetail.transform.Find("DetailItemDescription").GetComponent<Text>().text = "";
        }
    }

    private void FinishMenu()
    {
        //表示項目を削除
        foreach (GameObject g in _ListViewItemUnits.Values)
        {
            CommonFunction.RemoveAllListeners(g, EventTriggerType.PointerDown);
            CommonFunction.RemoveAllListeners(g, EventTriggerType.Drag);
            CommonFunction.RemoveAllListeners(g, EventTriggerType.Scroll);
            DestroyMessageObject(g, _scrollItemView.transform);
        }
        _ListViewItemUnits.Clear();

        //beforeTarget = null;

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
        //BaseItem item = GetSelectItem(name);
        
        //名前
        _ItemDetail.transform.Find("DetailItemName").GetComponent<Text>().text =
            item.GetName();

        //攻撃力
        _ItemDetail.transform.Find("DetailItemAttackValue").GetComponent<Text>().text =
            item.GetAttack();

        //防御力
        _ItemDetail.transform.Find("DetailItemDefenseValue").GetComponent<Text>().text =
            item.GetDefense();

        //武器か盾の場合はオプションを更新する
        //if(item.IType == ItemType.Weapon)
        //{
        //    SetOptionValues(((WeaponBase)item).Options);
        //}
        //else if(item.IType == ItemType.Shield)
        //{
        //    SetOptionValues(((ShieldBase)item).Options);
        //}
        //else
        //{
        //    SetOptionValues(null);
        //}
        SetOptionValues(item);

        //説明
        _ItemDetail.transform.Find("DetailItemDescription").GetComponent<Text>().text =
            item.GetDescription();
    }
    #region　オプション
    //private void SetOptionValues(List<BaseOption> options

    private void ClearOptionValue()
    {
        //前の情報を削除
        foreach (GameObject g in _ListViewOptionUnits.Values)
        {
            DestroyMessageObject(g, _OptionDetailView.transform);
        }
        _ListViewOptionUnits.Clear();

        _ViesOptions = null;

        _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text = "-";
    }

    private void SetOptionValues(BaseItem item)
    {
        
        //前の情報を削除
        foreach (GameObject g in _ListViewOptionUnits.Values)
        {
            DestroyMessageObject(g, _OptionDetailView.transform);
        }
        _ListViewOptionUnits.Clear();

        _OptionDetaiDescription.GetComponent<Text>().text = "";

        string optcount;
        _ViesOptions = null;

        if (CommonFunction.IsNull(item.Options) == false)
        {
            if (item.IsAnalyse == true)
            {
                _ViesOptions = item.Options;
                optcount = _ViesOptions.Count().ToString();
            }
            else
            {
                optcount = "?";
            }
        }
        else
        {
            optcount = "-";
        }

        _ItemDetail.transform.Find("DetailItemOptionCountValue").GetComponent<Text>().text =
            optcount;
        //表示対象がなければ終了
        if (CommonFunction.IsNull(_ViesOptions) == true)
        {
            return;
        }

        int index = 0;
        foreach (BaseOption bo in _ViesOptions)
        {
            GameObject copy = GetMessageObject(OptionStack, _OptionDetailSelectUnit, _OptionDetailViewContent.transform);
            //GameObject copy = GameObject.Instantiate(_OptionDetailSelectUnit, _OptionDetailViewContent.transform);
            SetItemUnSelectBack(copy);
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
        if(CommonFunction.IsNull(optionTarget) || _ListViewOptionUnits.ContainsKey(optionTarget) == false)
        {
            optionTarget = _ViesOptions[0];
        }
        SetItemSelectBack(_ListViewOptionUnits[optionTarget]);
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

                SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                //選択中の番号を取得
                int index = _ListViewOptionUnits.Keys.ToList().IndexOf(optionTarget);
                index--;
                optionTarget = _ListViewOptionUnits.Keys.ElementAt(index);
                //詳細の更新
                _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
                SetItemSelectBack(_ListViewOptionUnits[optionTarget]);


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

                SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
                //選択中の番号を取得
                int index = _ListViewOptionUnits.Keys.ToList().IndexOf(optionTarget);
                index++;
                optionTarget = _ListViewOptionUnits.Keys.ElementAt(index);
                //詳細の更新
                _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
                SetItemSelectBack(_ListViewOptionUnits[optionTarget]);


                //コンテナの高さを決める
                float height = _ListViewOptionUnits.Count * OptionUnit;
                int idx = index;
                int n = _ListViewOptionUnits.Count;

                CommonFunction.SetCenterViewItem(height, _OptionDetailView, idx, n);
                
            }
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);
            
            FinishSub();
            _OptionDetaiDescription.GetComponent<Text>().text = "";
            SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
            nowState = BeforeState;
        }
        return nowState;
    }


    public void OnClickOption(BaseEventData eventData, BaseOption i)
    {
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
           
            if (CommonFunction.IsNull(optionTarget) == false && _ListViewOptionUnits.ContainsKey(optionTarget) == true)
            {
                SetItemUnSelectBack(_ListViewOptionUnits[optionTarget]);
            }
            //選択中の番号を取得
            optionTarget = i;
            //詳細の更新
            _OptionDetaiDescription.GetComponent<Text>().text = optionTarget.Description;
            SetItemSelectBack(_ListViewOptionUnits[optionTarget]);

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
    /// <summary>
    /// スクロール位置の設定
    /// </summary>
    //private void SetCenterViewOption()
    //{
    //    //以下スクロールの設定
    //    float height = _ViesOptions.Count * OptionUnit;
    //    float contentHeight = _OptionDetailView.GetComponent<RectTransform>().sizeDelta.y;
    //    // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
    //    if (contentHeight >= height)
    //    {
    //        return;
    //    }

    //    int index = _ViesOptions.Keys.ToList().IndexOf(optionTarget);
    //    int n = _ViesOptions.Count;
    //    float y = (index + 0.5f) / n;  // 要素の中心座標
    //    float scrollY = y - 0.5f * height / contentHeight;
    //    float ny = scrollY / (1 - height / contentHeight);  // ScrollRect用に正規化した座標

    //    ScrollRect scrollRect = _OptionDetailView.GetComponent<ScrollRect>();
    //    scrollRect.verticalNormalizedPosition = Mathf.Clamp(ny, 0, 1);
    //}
    #endregion オプション

    private TurnState UpdateItemMenu(TurnState nowState,PlayerCharacter player)
    {
        //TempState = nowState;
        //Player = player;

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

                SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                //選択中の番号を取得
                int index = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
                index--;
                _LastTarget[nowType] = _ListViewItemUnits.Keys.ElementAt(index);
                //詳細の更新
                SetDetailDisplay(_LastTarget[nowType]);
                SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                SetCenterViewItem();

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

                SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                //選択中の番号を取得
                int index = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
                index++;
                _LastTarget[nowType] = _ListViewItemUnits.Keys.ElementAt(index);
                //詳細の更新
                SetDetailDisplay(_LastTarget[nowType]);
                SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                SetCenterViewItem();
            }
        }
        //左
        else if (KeyControlInformation.Info.OnMoveLeft())
        {

            //ドライブを見るの場合無視
            if (nowState == TurnState.LookDrive)
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
                InitializeItemMenu(nowState);
            }
        }
        //右
        else if (KeyControlInformation.Info.OnMoveRight())
        {
            //ドライブを見るの場合無視
            if (nowState == TurnState.LookDrive)
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
                InitializeItemMenu(nowState);
            }
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        {

            //ドライブを見るの場合
            if (nowState == TurnState.LookDrive)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                nowType = beforeType;
                _LastTarget[nowType] = beforeTarget;
                nowState = TurnState.ItemMenu;
                InitializeItemMenu(nowState);
                FinishSub();
            }
            //入れるのキャンセル
            else if (nowState == TurnState.ItemInDriveFromDrive)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                _MenuItemAction.transform.localPosition = new Vector3(SubMenuDefaultX, SubMenuDefaultY);
                nowState = TurnState.ItemSubMenu;
            }
            //対象アイテム選択のキャンセル
            else if (nowState == TurnState.ItemTargetSelect)
            {

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                _MenuItemAction.transform.localPosition = new Vector3(SubMenuDefaultX, SubMenuDefaultY);
                nowState = TurnState.ItemSubMenu;
            }
            else
            {
                DeleteList.Clear();

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                FinishMenu();
                nowState = TurnState.FirstMenu;
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
                BeforeState = nowState;
                nowState = TurnState.ItemOption;
            }
        }
        //ソート
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.ItemSort))
        {
            if (nowState != TurnState.LookDrive)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                SetTabUnSelect(nowType);
                InitializeItemMenu(nowState, true);
            }
        }
        //複数選択
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuMultiSelectOk))
        {
            //表示リストが0だったら無視
            if (_ListViewItemUnits.Count == 0)
            {
            }
            else if (nowState == TurnState.ItemDelete)
            {
                //素材がすでに含まれているとき
                if (DeleteList.Contains(_LastTarget[nowType]) == true)
                {
                    //選択素材を減らす
                    DeleteList.Remove(_LastTarget[nowType]);
                }
                //選択されていないとき
                else
                {
                    //選択素材を増やす
                    DeleteList.Add(_LastTarget[nowType]);
                }
                SetItemValue(_ListViewItemUnits[_LastTarget[nowType]], _LastTarget[nowType], _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]));

            }
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        {
            nowState = ExeItemMenu(nowState,player);
        }
        //else if (IsClick == true)
        //{
        //    nowState = TempState;
        //    IsClick = false;
        //}


        if (nowState == TurnState.PlayerItemUse)
        {
            FinishAllMenu();
        }
        return nowState;
    }
    public void OnClickTab(BaseEventData eventData, int tab)
    {
        if (TempState != TurnState.ItemMenu && TempState != TurnState.ItemDelete
            && TempState != TurnState.ItemInDriveFromDrive)
        {
            return;
        }

        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
            
            SetTabUnSelect(tab);
            nowType = tab;
            InitializeItemMenu(TempState);
        }
    }

    public void OnClickItemMenu(BaseEventData eventData, BaseItem i)
    {
        if(TempState == TurnState.LookDrive)
        {
            if (KeyControlInformation.Info.OnRightClick(eventData) == true)
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
            }
        }
        if (TempState != TurnState.ItemMenu
            && TempState != TurnState.ItemDelete
            && TempState != TurnState.ItemTargetSelect
            && TempState != TurnState.ItemInDriveFromDrive)
        {
            return;
        }
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if (CommonFunction.IsDoubleClick())
            {
                if (TempState == TurnState.ItemDelete)
                {
                    KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuMultiSelectOk, true);
                }
                else
                {
                    KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                }
                //TempState = ExeItemMenu();
                //IsClick = true;
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
                
                SetItemUnSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);
                //選択中の番号を取得
                _LastTarget[nowType] = i;
                //詳細の更新
                SetDetailDisplay(_LastTarget[nowType]);
                SetItemSelectBack(_ListViewItemUnits[_LastTarget[nowType]]);

                CommonFunction.SetDragScrollViewFirstPosition(_scrollItemView);
                //SetCenterViewItem();
            }
        }
        else if(KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }

    public void OnDragItemMenu(BaseEventData eventData)
    {
        float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
        int n = _ListViewItemUnits.Count;

        CommonFunction.SetDragScrollViewItem(eventData,height,_scrollItemView, ViewItemUnitHeight,n);
    }
    public void OnScrollItemMenu(BaseEventData eventData)
    {
        float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
        int n = _ListViewItemUnits.Count;

        CommonFunction.SetScrollScrollViewItem(eventData, height, _scrollItemView, ViewItemUnitHeight, n);
    }

    private TurnState ExeItemMenu(TurnState nowState,PlayerCharacter player)
    {


        //ドライブを見るの場合無視
        if (nowState == TurnState.LookDrive)
        {

        }
        //表示リストが0だったら無視
        else if (_ListViewItemUnits.Count == 0)
        {
        }
        else
        {
            //入れる（アイテム側なら）
            if (nowState == TurnState.ItemInDrive)
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                UseItemInformation.ItemUseType = MenuItemActionType.Putin;
                UseItemInformation.UseItemMain = FirstSectTemp;
                BaseItem target = _LastTarget[nowType];

                //対象がドライブの場合
                if (typeof(BagBase) == target.GetType())
                {
                    //ドライブがバグっていた場合
                    if (CommonFunction.HasOptionType(target.Options, OptionType.Adhesive))
                    {
                        DisplayInformation.Info.AddMessage(CommonConst.Message.DriveBug);
                    }
                    else
                    {

                        //ドライブの空き容量を確認
                        if (((BagBase)target).IsPutinItem() == true)
                        {

                            UseItemInformation.ItemUseType = MenuItemActionType.Putin;
                            UseItemInformation.UseItemMain = FirstSectTemp;
                            UseItemInformation.ParentDrive = (BagBase)_LastTarget[nowType];
                            nowState = TurnState.PlayerItemUse;
                        }
                        //容量がなかったら
                        else
                        {
                            DisplayInformation.Info.AddMessage(CommonConst.Message.DriveFull);
                        }
                        UseItemInformation.ParentDrive = (BagBase)_LastTarget[nowType];
                    }

                }
                //ドライブを選ばなかった場合のメッセージを表示する
                else
                {
                    DisplayInformation.Info.AddMessage(CommonConst.Message.NotDrive);
                    
                }
            }
            //入れる（ドライブ側なら）
            else if (nowState == TurnState.ItemInDriveFromDrive)
            {

                BaseItem target = _LastTarget[nowType];

                //対象がドライブの場合
                //if (typeof(BagBase) == target.GetType())
                if (target.IsDriveProb == false)
                {
                    DisplayInformation.Info.AddMessage(CommonConst.Message.NotDrive);
                    
                }
                else
                {
                    UseItemInformation.ItemUseType = MenuItemActionType.Putin;
                    UseItemInformation.UseItemMain = _LastTarget[nowType];
                    UseItemInformation.ParentDrive = (BagBase)FirstSectTemp;
                    nowState = TurnState.PlayerItemUse;

                    _MenuItemAction.transform.localPosition = new Vector3(SubMenuSelectDefaultX, SubMenuSelectDefaultY);
                }
            }
            //アイテム効果対象を選ぶ
            else if (nowState == TurnState.ItemTargetSelect)
            {
                //効果対象を格納
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = beforeTarget;
                UseItemInformation.UseItemEffectTarget = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
            }
            //まとめて削除
            else if (nowState == TurnState.ItemDelete)
            {
                if (DeleteList.Count == 0)
                {
                    if(KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
                    {

                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.ItemDeleteNotSelectUseMouse, KeyControlModel.GetName(
                                KeyControlInformation.Info.MenuMultiSelectOk).Trim()));
                    }
                    else
                    {

                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.ItemDeleteNotSelect, KeyControlModel.GetName(
                                KeyControlInformation.Info.MenuMultiSelectOk).Trim()));
                    }
                }
                else
                {
                    foreach (BaseItem i in DeleteList)
                    {

                        if (i.IsEquip == true)
                        {
                            i.ForceRemoveEquip(player);
                        }
                        PlayerCharacter.RemoveItem(i);

                    }
                    DeleteList.Clear();

                    DisplayInformation.Info.AddMessage(
                        CommonConst.Message.ItemDelete);

                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.Break);

                    FinishAllMenu();

                    GameStateInformation.Info.IsRestCount = true;

                    nowState = TurnState.MenuExit;
                }
            }
            //鑑定
            else if(nowState == TurnState.ItemAnalyse)
            {
                if(  PlayerCharacter.HasItemPlayer(CommonConst.ObjNo.MaterialSandIron)== false)
                {
                    DisplayInformation.Info.AddMessage(CommonConst.Message.ItemLuckAnalyseMaterial);
                }
                else
                {
                    BaseItem mat = PlayerCharacter.ItemList.Find(i => i.ObjNo == CommonConst.ObjNo.MaterialSandIron);
                    PlayerCharacter.RemoveItem(mat);

                    string bname = _LastTarget[nowType].DisplayNameInMessage;
                    _LastTarget[nowType].ClearAnalyse();
                    string aname = _LastTarget[nowType].DisplayNameInMessage;

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.ItemAnalyse, bname, aname));

                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                    GameStateInformation.Info.IsRestCount = true;

                    FinishAllMenu();

                    nowState = TurnState.MenuExit;
                }
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                //メニューの通常選択
                //ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                beforeType = nowType;
                beforeTarget = _LastTarget[nowType];
                InitializeItemSubMenu();
                nowState = TurnState.ItemSubMenu;
            }
        }
        return nowState;
    }

    /// <summary>
    /// 表に表示する項目の設定
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="item"></param>
    private void SetItemValue(GameObject obj, BaseItem item , int index)
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
        if (DeleteList.Contains(item) == true)
        {
            obj.transform.Find("ItemName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.SelectItem, _LastTarget[nowType].DisplayNameInItem);
        }
        else
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
        else if(PlayerCharacter.ItemList.Contains(item) == false)
        {
            obj.transform.Find("TextIsEquip").GetComponent<Text>().text = "足元";
        }
        else
        {
            obj.transform.Find("TextIsEquip").GetComponent<Text>().text = "";
        }
    }

    /// <summary>
    /// スクロール位置の設定
    /// </summary>
    private void SetCenterViewItem()
    {

        float height = _ListViewItemUnits.Count * ViewItemUnitHeight;
        int index = _ListViewItemUnits.Keys.ToList().IndexOf(_LastTarget[nowType]);
        int n = _ListViewItemUnits.Count;
        CommonFunction.SetCenterViewItem(height, _scrollItemView, index, n);
    }

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
            GameObject obj = SetItemChild(_MenuItemActionItem, _MenuItemActionPosition);

            //行動名の設定
            obj.transform.Find("Text").GetComponent<Text>().text = CommonFunction.MenuItemActionTypeName[target];
            obj.transform.localPosition = new Vector3(0, SubViewItemUnitHeight * i, 0);
            SetItemUnSelectBack(obj);
            _ListViewItemSubUnits.Add(target, obj);

            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickSubMenu(e, target));

            obj.SetActive(true);

            i++;
        }
        subSelected = _ListViewItemSubUnits.Keys.First();


        //一件目を選択状態にする
        SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
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

                SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                //選択中の番号を取得
                int index = _ListViewItemSubUnits.Keys.ToList().IndexOf(subSelected);
                index--;
                subSelected = _ListViewItemSubUnits.Keys.ElementAt(index);
                //背景を更新
                SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
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
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                //選択中の番号を取得
                int index = _ListViewItemSubUnits.Keys.ToList().IndexOf(subSelected);
                index++;
                subSelected = _ListViewItemSubUnits.Keys.ElementAt(index);
                //背景を更新
                SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
            }
        }

        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            //ステートをメニューに返す
            nowState = TurnState.ItemMenu;
            nowType = beforeType;
            _LastTarget[nowType] = beforeTarget;
            InitializeItemMenu(nowState);
            FinishSub();
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
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

        if(TempState != TurnState.ItemSubMenu)
        {
            return;
        }

        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if(CommonFunction.IsDoubleClick())
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                //TempState = ExeSubMenu();
                //IsClick = true;
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                SetItemUnSelectBack(_ListViewItemSubUnits[subSelected]);
                subSelected = a;
                SetItemSelectBack(_ListViewItemSubUnits[subSelected]);
            }
        }
        else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }

    public TurnState ExeSubMenu(TurnState nowState,PlayerCharacter player)
    {
        //if (nowState == TurnState.None)
        //{
        //    nowState = TempState;
        //}

        //if (CommonFunction.IsNull(player) == true)
        //{
        //    player = Player;
        //}

        //入れる以外はアクション項目を渡して終了
        //選択した項目によって分岐
        switch (subSelected)
        {
            case MenuItemActionType.Eat:
                //食べる
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Equip:
                //装備
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Put:
                //置く
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Putin:
                //_LastTarget[nowType]
                FirstSectTemp = _LastTarget[nowType];
                //入れる
                nowState = TurnState.ItemInDrive;
                break;
            case MenuItemActionType.PutinParent:
                //選択中だったアイテムを格納する
                FirstSectTemp = _LastTarget[nowType];
                _MenuItemAction.transform.localPosition = new Vector3(SubMenuSelectDefaultX, SubMenuSelectDefaultY);
                //入れる
                nowState = TurnState.ItemInDriveFromDrive;
                break;
            case MenuItemActionType.RemoveEquip:
                //外す
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Throw:
                //投げる

                //肩こりだった場合は失敗終了
                if ((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.NotThrow, player.DisplayNameInMessage));
                    break;
                }
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Use:
                //使う
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Get:
                //拾う
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.Melody:
                //奏でる

                //かぜだった場合は失敗終了
                if ((DisplayInformation.Info.State & (int)StateAbnormal.Reticent) != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.NotSing, player.DisplayNameInMessage));
                    break;
                }
                UseItemInformation.ItemUseType = subSelected;
                UseItemInformation.UseItemMain = _LastTarget[nowType];
                nowState = TurnState.PlayerItemUse;
                break;
            case MenuItemActionType.LookOption:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                //オプション参照
                InitializeOption();
                BeforeState = TurnState.ItemMenu;
                nowState = TurnState.ItemOption;

                break;
            case MenuItemActionType.Look:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                //覗く
                _MenuItemAction.transform.localPosition = new Vector3(SubMenuSelectDefaultX, SubMenuSelectDefaultY);
                Dictionary<int, string> names = new Dictionary<int, string>();
                names.Add(_DriveItem, "Look");
                InitializeTabs(names);
                beforeType = nowType;
                nowType = _DriveItem;
                InitializeItems(((BagBase)_LastTarget[beforeType]).BagItems);

                SetTabSelect(_DriveItem);
                nowState = TurnState.LookDrive;
                break;
        }


        if (nowState == TurnState.PlayerItemUse)
        {
            FinishAllMenu();
        }

        return nowState;
    }

    public void FinishSub()
    {

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
        //ファーストメニューの閉じ処理
        _firstMenu.SetActive(false);
        
    }

    #endregion アイテム使用メニュー

    #region 全体の共通関数

    public void OnClickSort()
    {
        // 選択を解除
        EventSystem.current.SetSelectedGameObject(null);
        KeyControlInformation.Info.SetPushKeyOneTime(KeyType.ItemSort, true);
    }
    public void OnClickCansel()
    {
        // 選択を解除
        EventSystem.current.SetSelectedGameObject(null);
        KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
    }

    public void OnClickOk()
    {
        // 選択を解除
        EventSystem.current.SetSelectedGameObject(null);
        KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
    }

    private GameObject GetMessageObject(List<GameObject> list,GameObject origin,Transform tf)
    {
        GameObject gm = list.Find(i => i.activeSelf == false);
        if (CommonFunction.IsNull(gm) == false)
        {
            gm.SetActive(true);
            gm.transform.SetParent(tf.transform);
            return gm;
        }
        gm = GameObject.Instantiate(origin, tf.transform);
        gm.SetActive(true);
        list.Add(gm);
        return gm;
    }
    private GameObject GetObject(List<GameObject> list, GameObject origin, Transform tf)
    {
        GameObject gm = list.Find(i => i.activeSelf == false);
        if (CommonFunction.IsNull(gm) == false)
        {
            gm.SetActive(true);
            return gm;
        }
        gm = GameObject.Instantiate(origin, tf.transform);
        gm.SetActive(true);
        list.Add(gm);
        return gm;
    }

    private void DestroyMessageObject(GameObject gm,Transform parent)
    {
        gm.transform.SetParent(parent);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.PointerDown);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.Drag);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.Scroll);
        gm.SetActive(false);
    }
    private void DestroyObject(GameObject gm)
    {
        gm.SetActive(false);
    }

    /// <summary>
    /// 対象位置のイメージを消す
    /// </summary>
    private void SetItemUnSelectBack(GameObject obj)
    {
        Image cols = obj.transform.Find("SelectedBack").GetComponent<Image>();
        cols.color =
            new Color(cols.color.r, cols.color.g, cols.color.b, 0.0f);
    }

    /// <summary>
    /// 対象位置のイメージをつける
    /// </summary>
    private void SetItemSelectBack(GameObject obj)
    {
        Image cols = obj.transform.Find("SelectedBack").GetComponent<Image>();
        cols.color =
            new Color(cols.color.r, cols.color.g, cols.color.b, 1.0f);
    }


    /// <summary>
    /// アイテムスクロールビューに項目を設定
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject SetItemChild(GameObject child, GameObject parent)
    {
        // インスタンスを生成
        GameObject obj = GameObject.Instantiate(child, parent.transform);
        
        return obj;
    }
    #endregion 全体の共通関数
}
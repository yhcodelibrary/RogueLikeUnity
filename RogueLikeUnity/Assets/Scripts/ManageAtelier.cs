using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageAtelier : MonoBehaviour
{

    //public Material[] ItemTypeMaterials;
    //public Texture[] ItemTypeTexture;
    public Sprite[] ItemSprite;
    
    private GameObject menu;
    private GameObject itemSelectedUnit;
    /// <summary>
    /// レシピメイン
    /// </summary>
    private GameObject ScrollViewRecipe;
    private GameObject recipeViewContent;
    /// <summary>
    /// 必要素材
    /// </summary>
    private GameObject recipeMaterialUnit;
    private GameObject ScrollViewRecipeMaterial;
    private GameObject recipeMaterialViewContent;
    /// <summary>
    /// サブメニュー
    /// </summary>
    private GameObject subMenuMain;
    private GameObject subMenuUnit;
    private GameObject subMenuContent;
    /// <summary>
    /// 素材選択
    /// </summary>
    private GameObject ScrollViewSelectMaterial;
    private GameObject recipeSelectMaterialViewContent;
    /// <summary>
    /// オプション系
    /// </summary>
    private GameObject OptionSelectedUnit;
    private GameObject ScrollViewOption;
    private GameObject OptionViewContent;
    private GameObject OptionDetaiDescription;

    /// <summary>
    /// 作成画面系
    /// </summary>
    private GameObject AtelierCreatePanel;
    private GameObject AtelierOptionScrolllView;
    private GameObject AtelierCreateOptionContent;

    private GameObject _UIBase;

    private Dictionary<Guid, RecipeInformation> Recipes;
    private Dictionary<Guid, float> Weights;
    private float TotalMaterialWeight;
    private Guid SelectRecipeNo;
    private List<AtelierMenuActionType> SubMenus;
    private AtelierMenuActionType SelectSubMenuNo;
    /// <summary>
    /// 表示中の全アイテム
    /// </summary>
    private Dictionary<Guid,BaseItem> Items;
    /// <summary>
    /// 選択したアイテム
    /// </summary>
    private List<BaseItem> ItemSelected;
    private Dictionary<long, int> NowSelectItemCount;
    private BaseItem SelectMaterialNo;
    private List<BaseOption> Options;
    private BaseOption SelectOptionNo;
    
    //private Dictionary<Guid, BaseOption> CreateOptions;
    private BaseOption SelectCreateOptionNo;

    public BaseItem CreateItem;

    private sbyte SelectNowBtn;

    private TurnState TempState;

    private List<GameObject> RecipeStack;
    private List<GameObject> RecipeMaterialStack;
    private List<GameObject> MaterialStack;
    private List<GameObject> OptionStack;
    private List<GameObject> CreateOptionStack;

    private const float ItemUnitSize = 25;
    private const float OptionUnit = 20;

    public static bool IsSuccess;

    private static List<AtelierMenuActionType> _SubMenuList;
    private static List<AtelierMenuActionType> SubMenuList
    {
        get
        {
            if(CommonFunction.IsNull(_SubMenuList) == false)
            {
                return _SubMenuList;
            }
            _SubMenuList = new List<AtelierMenuActionType>();
            _SubMenuList.Add(AtelierMenuActionType.Auto);
            _SubMenuList.Add(AtelierMenuActionType.Manual);
            return _SubMenuList;

        }
    }
    private Dictionary<Guid, GameObject> _ListViewRecipeUnits;
    private Dictionary<AtelierMenuActionType, GameObject> _ListViewSubMenu;
    private Dictionary<long, GameObject> _ListViewRecipeMterialUnits;
    private Dictionary<BaseItem, GameObject> _ListViewUseMterialUnits;
    private Dictionary<BaseOption, GameObject> _ListViewOptionUnits;
    private Dictionary<BaseOption, GameObject> _ListViewCreateOptionUnits;

    public void Start()
    {
        menu = GameObject.Find("AtelierMenuPanel");
        itemSelectedUnit = menu.transform.Find("ItemSelectedUnit").gameObject;
        recipeMaterialUnit = menu.transform.Find("RecipeMaterialUnit").gameObject;
        OptionSelectedUnit = menu.transform.Find("AtelierOptionDetailSelectUnit").gameObject;
        subMenuUnit = menu.transform.Find("AtelierSubSelectedUnit").gameObject;
        //レシピビュー系
        ScrollViewRecipe = menu.transform.Find("ScrollViewRecipe").gameObject;
        recipeViewContent = ScrollViewRecipe.transform.Find("Viewport/RecipeViewContent").gameObject;
        //必要素材系
        ScrollViewRecipeMaterial = menu.transform.Find("ScrollViewRecipeMaterial").gameObject;
        recipeMaterialViewContent = menu.transform.Find("ScrollViewRecipeMaterial/Viewport/RecipeMaterialViewContent").gameObject;
        //素材ビュー系
        ScrollViewSelectMaterial = menu.transform.Find("ScrollViewSelectMaterial").gameObject;
        recipeSelectMaterialViewContent = ScrollViewSelectMaterial.transform.Find("Viewport/RecipeSelectMaterialViewContent").gameObject;
        //オプション系
        ScrollViewOption = menu.transform.Find("AtelierOptionDetail/AtelierOptionDetailPanel/AtelierOptionDetailView").gameObject;
        OptionViewContent = ScrollViewOption.transform.Find("Viewport/OptionDetailViewContent").gameObject;
        OptionDetaiDescription = menu.transform.Find("AtelierOptionDetail/AtelierOptionDetailPanel/AtelierOptionDetaiDescription").gameObject;
        //サブメニュー系
        subMenuMain = menu.transform.Find("AtelierSubMenuPanel").gameObject;
        subMenuContent = subMenuMain.transform.Find("AtelierSubScrollView/Viewport/AtelierSubMenuContent").gameObject;

        //UI
        _UIBase = menu.transform.Find("MenuUIButton").gameObject;

        //作成系
        AtelierCreatePanel = menu.transform.Find("AtelierCreatePanel").gameObject;
        AtelierOptionScrolllView = AtelierCreatePanel.transform.Find("AtelierOptionScrolllView").gameObject;
        AtelierCreateOptionContent = AtelierOptionScrolllView.transform.Find("Viewport/AtelierCreateOptionContent").gameObject;


        menu.transform.Find("RecipeHeader").GetComponent<Text>().text = CommonConst.Message.FormulationRecipe;
        menu.transform.Find("RecipeMaterialHeader").GetComponent<Text>().text = CommonConst.Message.RequiredMaterial;
        menu.transform.Find("AtelierOptionlHeader").GetComponent<Text>().text = CommonConst.Message.MaterialOption;

        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemAttack").GetComponent<Text>().text = CommonConst.Message.ATK;
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemDefense").GetComponent<Text>().text = CommonConst.Message.DEF;
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemOptionCount").GetComponent<Text>().text = CommonConst.Message.Options;


        _ListViewRecipeUnits = new Dictionary<Guid, GameObject>();
        _ListViewSubMenu = new Dictionary<AtelierMenuActionType, GameObject>();
        _ListViewRecipeMterialUnits = new Dictionary<long, GameObject>();
        _ListViewUseMterialUnits = new Dictionary<BaseItem, GameObject>(new BaseItemComparer());
        _ListViewOptionUnits = new Dictionary<BaseOption, GameObject>(new BaseOptionComparer());
        _ListViewCreateOptionUnits = new Dictionary<BaseOption, GameObject>(new BaseOptionComparer());
        NowSelectItemCount = new Dictionary<long, int>();
        SubMenus = new List<AtelierMenuActionType>();
        Recipes = new Dictionary<Guid, RecipeInformation>();
        Weights = new Dictionary<Guid, float>();
        Items = new Dictionary<Guid, BaseItem>();
        ItemSelected = new List<BaseItem>();
        RecipeStack = new List<GameObject>();
        MaterialStack = new List<GameObject>();
        OptionStack = new List<GameObject>();
        CreateOptionStack = new List<GameObject>();
        RecipeMaterialStack = new List<GameObject>();

        IsSuccess = false;

        subMenuMain.SetActive(false);
        recipeMaterialUnit.SetActive(false);
        itemSelectedUnit.SetActive(false);
        OptionSelectedUnit.SetActive(false);
        subMenuUnit.SetActive(false);
        AtelierCreatePanel.SetActive(false);
        menu.SetActive(false);
    }

    public TurnState UpdateMainMenu(TurnState state)
    {
        TempState = state;

        switch (state)
        {
            case TurnState.AtelierMain:
                state = UpdateAtelierMenu(state);
                break;
            case TurnState.AtelierSubAction:
                state = UpdateItemSubMenu(state);
                break;
            case TurnState.AtelierMatelialSelect:
                state = UpdateAtelierMaterialSelect(state);
                break;
            case TurnState.AtelierMatelialOption:
                state = UpdateAtelierOptionSelect(state);
                break;
            case TurnState.AtelierCreate:
                state = UpdateCreate(state);
                break;
        }

        return state;
    }

    public void InitializeRecipeMenu()
    {

        menu.SetActive(true);

        //Dictionary<Guid, BaseItem> list = new Dictionary<Guid, BaseItem>();

        ////レシピを取得
        //Dictionary<long, RecipeInformation> recipes = new Dictionary<long, RecipeInformation>();

        //RecipeInformation rec = new RecipeInformation();
        //rec.RecipeTargetNo = 30009;
        //rec.RecipeTargetType = ItemType.Material;
        //rec.SetRecipeValue(30003, ItemType.Material, 2);
        //recipes.Add(rec.RecipeTargetNo, rec);

        //rec = new RecipeInformation();
        //rec.RecipeTargetNo = 20001;
        //rec.RecipeTargetType = ItemType.Weapon;
        //rec.SetRecipeValue(30004, ItemType.Material, 1);
        //rec.SetRecipeValue(30005, ItemType.Material, 1);
        //rec.SetRecipeValue(30009, ItemType.Material, 1);
        //recipes.Add(rec.RecipeTargetNo, rec);

        //rec = new RecipeInformation();
        //rec.RecipeTargetNo = 25010;
        //rec.RecipeTargetType = ItemType.Ball;
        //rec.SetRecipeValue(30001, ItemType.Material, 1);
        //rec.SetRecipeValue(30002, ItemType.Material, 1);
        //rec.SetRecipeValue(30003, ItemType.Material, 1);
        //rec.SetRecipeValue(30005, ItemType.Material, 1);
        //recipes.Add(rec.RecipeTargetNo, rec);

        //Recipeリストを作成する
        InitializeRecipes(TableRecipe.GetValue(RecipeType.Normal));

    }
    public void InitializeStrengthMenu()
    {

        menu.SetActive(true);
        
        //Recipeリストを作成する
        InitializeRecipes(TableStrength.GetValue(PlayerCharacter.ItemList));

    }

    private void InitializeRecipes(Dictionary<Guid, RecipeInformation> list)
    {
        switch (KeyControlInformation.Info.OpMode)
        {
            case OperationMode.KeyOnly:
                _UIBase.SetActive(false);
                break;
            case OperationMode.UseMouse:
                _UIBase.SetActive(true);
                break;
        }

        //前回の表示項目を削除
        foreach (GameObject g in _ListViewRecipeUnits.Values)
        {
            DestroyStackObject(g, ScrollViewRecipe.transform);
        }
        _ListViewRecipeUnits.Clear();
        Weights.Clear();

        Recipes = list;

        int index = 0;

        // 表示対象のアイテムの種類の数だけノードを生成
        foreach (RecipeInformation rec in list.Values)
        {

            //生成対象アイテムを取得
            //BaseItem item = TableItemIncidence.GetItemObjNo(rec.RecipeTargetType, rec.RecipeTargetNo, false);

            //アイテムの重さを保存
            Weights.Add(rec.Name, rec.Weight);

            //コピーを作成
            GameObject obj = GetStackObject(RecipeStack, itemSelectedUnit, recipeViewContent.transform);

            //タイプ
            int typenum = (int)rec.RecipeTargetType;
            typenum--;
            //obj.transform.FindChild("TypeImage").GetComponent<RawImage>().texture = ItemTypeTexture[typenum];
            obj.transform.Find("TypeImage").GetComponent<Image>().sprite = ItemSprite[typenum];

            //名前 作成できない場合は赤で表示
            if (CanCreate(rec) == true)
            {
                obj.transform.Find("SelectName").GetComponent<Text>().text = rec.RecipeTargetName;
            }
            else
            {
                obj.transform.Find("SelectName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.ItemBad, rec.RecipeTargetName);
            }

            //奇数の背景を変える
            obj.transform.Find("SelectedOdd").gameObject.SetActive(index % 2 == 0);
            index++;
            
            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickAtelier(e, rec.Name));
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Drag, e => OnDragAtelier(e), false);
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Scroll, e => OnScrollAtelier(e), false);

            //選択背景を不活性化
            SetItemUnSelectBack(obj);

            //リストに追加
            _ListViewRecipeUnits.Add(rec.Name, obj);
        }

        //オプションの説明を初期化
        OptionDetaiDescription.GetComponent<Text>().text = "";

        //一件目を選択状態にする
        if(_ListViewRecipeUnits.Count > 0)
        {
            if(_ListViewRecipeUnits.ContainsKey(SelectRecipeNo) == false)
            {
                SelectRecipeNo = _ListViewRecipeUnits.Keys.First();
            }
            SetItemSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);

            //一件目の素材情報を反映
            SetRecipeMaterial(Recipes[SelectRecipeNo]);
        }

        //コンテナの高さを決める
        float height = _ListViewRecipeUnits.Count * 25f;
        int idx = _ListViewRecipeUnits.Keys.ToList().IndexOf(SelectRecipeNo);
        int n = _ListViewRecipeUnits.Count;

        CommonFunction.SetCenterViewItem(height, ScrollViewRecipe, idx, n, 0.9f);

    }

    private TurnState UpdateAtelierMenu(TurnState state)
    {

        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //表示リストが0だったら無視
            if (_ListViewRecipeUnits.Count == 0)
            {

            }
            //最初の項目だったら無視
            else if (_ListViewRecipeUnits.Keys.First() ==  SelectRecipeNo)
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);
                //選択中の番号を取得
                int index = _ListViewRecipeUnits.Keys.ToList().IndexOf(SelectRecipeNo);
                index--;
                SelectRecipeNo = _ListViewRecipeUnits.Keys.ElementAt(index);
                //Recipe詳細の更新
                SetRecipeMaterial(Recipes[SelectRecipeNo]);
                //背景色を変更
                SetItemSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);

                //コンテナの高さを決める
                float height = _ListViewRecipeUnits.Count * 25f;
                //float height = recipeViewContent.GetComponent<RectTransform>().sizeDelta.y;
                int idx = index;
                int n = _ListViewRecipeUnits.Count;

                CommonFunction.SetCenterViewItem(height, ScrollViewRecipe, idx, n,0.9f);

            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {

            //表示リストが0だったら無視
            if (_ListViewRecipeUnits.Count == 0)
            {

            }
            //最後の項目だったら無視
            else if (_ListViewRecipeUnits.Keys.Last() == SelectRecipeNo)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);
                //選択中の番号を取得
                int index = _ListViewRecipeUnits.Keys.ToList().IndexOf(SelectRecipeNo);
                index++;
                SelectRecipeNo = _ListViewRecipeUnits.Keys.ElementAt(index);
                //Recipe詳細の更新
                SetRecipeMaterial(Recipes[SelectRecipeNo]);
                //背景色を変更
                SetItemSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);

                //コンテナの高さを決める
                //float height = recipeViewContent.GetComponent<RectTransform>().sizeDelta.y;
                float height = _ListViewRecipeUnits.Count * 25f;
                int idx = index;
                int n = _ListViewRecipeUnits.Count;


                CommonFunction.SetCenterViewItem(height, ScrollViewRecipe, idx, n,0.9f);

            }
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
        {
            //表示リストが0だったら無視
            if (_ListViewRecipeUnits.Count == 0)
            {

            }
            //作成できる場合は次の選択しに
            else if (CanCreate(Recipes[SelectRecipeNo]) == true)
            {
                //現在の重さ + 調合アイテムの重さ - 調合に使う素材の重さ
                if (((PlayerCharacter.ItemCount() + Weights[SelectRecipeNo]) - TotalMaterialWeight <= PlayerCharacter.ItemMaxCount))
                {

                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                    //アクション選択を初期化
                    InitializeItemSubMenu(SubMenuList);

                    //ステートを選択しメニューに
                    state = TurnState.AtelierSubAction;
                }
                else
                {
                    DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierMaxItem);
                    
                }
            }
            else
            {
                DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierNotCreate);
            }
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        {

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            //素材選択を初期化
            FinishAtelierMenu();

            ObjectClear();

            //ステートをファーストメニューに返す
            state = TurnState.FirstMenu;
        }

        return state;
    }

    public void OnClickAtelier(BaseEventData eventData, Guid g)
    {

        if (TempState != TurnState.AtelierMain)
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
                
                SetItemUnSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);
                //選択中の番号を取得
                SelectRecipeNo = g;
                //Recipe詳細の更新
                SetRecipeMaterial(Recipes[SelectRecipeNo]);
                //背景色を変更
                SetItemSelectBack(_ListViewRecipeUnits[SelectRecipeNo]);

                CommonFunction.SetDragScrollViewFirstPosition(ScrollViewRecipe);
                //SetCenterViewItem();
            }
        }
        else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }
    public void OnDragAtelier(BaseEventData eventData)
    {
        float height = _ListViewRecipeUnits.Count * ItemUnitSize;
        int n = _ListViewRecipeUnits.Count;

        CommonFunction.SetDragScrollViewItem(eventData, height, ScrollViewRecipe, ItemUnitSize, n);
    }
    public void OnScrollAtelier(BaseEventData eventData)
    {
        float height = _ListViewRecipeUnits.Count * ItemUnitSize;
        int n = _ListViewRecipeUnits.Count;

        CommonFunction.SetScrollScrollViewItem(eventData, height, ScrollViewRecipe, ItemUnitSize, n);
    }

    private void FinishAtelierMenu()
    {
        //前回の表示項目を削除
        foreach (GameObject g in _ListViewRecipeUnits.Values)
        {
            DestroyStackObject(g, ScrollViewRecipe.transform);
        }
        _ListViewRecipeUnits.Clear();

        //前回の表示項目を削除
        foreach (GameObject g in _ListViewRecipeMterialUnits.Values)
        {
            DestroyStackObject(g, ScrollViewRecipeMaterial.transform);
        }
        _ListViewRecipeMterialUnits.Clear();

        menu.SetActive(false);

        Recipes = null;
    }

    private void InitializeItemSubMenu(List<AtelierMenuActionType> list)
    {
        //前回の表示項目を削除
        foreach (GameObject g in _ListViewSubMenu.Values)
        {
            UnityEngine.Object.Destroy(g);
        }
        _ListViewSubMenu.Clear();

        SubMenus = list;

        subMenuMain.SetActive(true);

        int i = 0;
        foreach (AtelierMenuActionType target in SubMenus)
        {
            //コピーを作成
            GameObject obj = SetItemChild(subMenuUnit, subMenuContent);

            //行動名の設定
            obj.transform.Find("SelectName").GetComponent<Text>().text = CommonFunction.AtelierMenuActionTypeName[target];
            SetItemUnSelectBack(obj);
            _ListViewSubMenu.Add(target, obj);
            obj.SetActive(true);

            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickSubMenu(e, target));

            i++;
        }
        SelectSubMenuNo = SubMenus[0];

        //一件目を選択状態にする
        SetItemSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
    }


    private TurnState UpdateItemSubMenu(TurnState nowState)
    {
        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //表示リストが0だったら無視
            if (_ListViewSubMenu.Count == 0)
            {

            }
            //最初の項目だったら無視
            else if (SelectSubMenuNo == _ListViewSubMenu.Keys.First())
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
                //選択中の番号を取得
                int index = _ListViewSubMenu.Keys.ToList().IndexOf(SelectSubMenuNo);
                index--;
                SelectSubMenuNo = _ListViewSubMenu.Keys.ElementAt(index);
                SetItemSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {
            //表示リストが0だったら無視
            if (_ListViewSubMenu.Count == 0)
            {

            }
            //最後の項目だったら無視
            else if (_ListViewSubMenu.Keys.Last() == SelectSubMenuNo)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
                //選択中の番号を取得
                int index = _ListViewSubMenu.Keys.ToList().IndexOf(SelectSubMenuNo);
                index++;
                SelectSubMenuNo = _ListViewSubMenu.Keys.ElementAt(index);
                SetItemSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
            }
        }

        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        {

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            FinishSubMenu();

            //ステートをメニューに返す
            nowState = TurnState.AtelierMain;
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
        {
            
            NowSelectItemCount.Clear();
            //選択した項目によって分岐
            switch (SelectSubMenuNo)
            {
                case AtelierMenuActionType.Auto:
                    //素材選択を初期化
                    InitializeMaterialSelect(Recipes[SelectRecipeNo], true);
                    
                    //作成を初期化
                    InitializeCreate(Recipes[SelectRecipeNo]);

                    nowState = TurnState.AtelierCreate;
                    break;
                case AtelierMenuActionType.Manual:
                    //手動選択
                    //素材選択を初期化
                    InitializeMaterialSelect(Recipes[SelectRecipeNo], false);
                    nowState = TurnState.AtelierMatelialSelect;
                    break;
            }

            FinishSubMenu();
            
        }

        return nowState;
    }
    public void OnClickSubMenu(BaseEventData eventData, AtelierMenuActionType a)
    {

        if (TempState != TurnState.AtelierSubAction)
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

                SetItemUnSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
                SelectSubMenuNo = a;
                SetItemSelectBack(_ListViewSubMenu[SelectSubMenuNo]);
            }
        }
        else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }

    private void FinishSubMenu()
    {
        subMenuMain.SetActive(false);
    }

    public void InitializeMaterialSelect(RecipeInformation rec,bool isAutoSelect)
    {
        //前回の表示項目を削除
        foreach (GameObject g in _ListViewUseMterialUnits.Values)
        {
            DestroyStackObject(g, ScrollViewSelectMaterial.transform);

        }
        _ListViewUseMterialUnits.Clear();

        //選択対象となる素材リストを作成
        NowSelectItemCount.Clear();
        ItemSelected.Clear();
        Items.Clear();
        foreach(long no in rec.RecipeMaterialsType.Keys)
        {
            NowSelectItemCount.Add(no, 0);
            //持ち物の中からターゲット素材を抽出
            List< BaseItem> items;
            //強化の場合は強化対象装備のみを抽出
            if(rec.IsStrength == true)
            {
                if(rec.RecipeTargetType == rec.RecipeMaterialsType[no])
                {
                    items = new List<BaseItem>();
                    items.Add(rec.TargetStrength);
                }
                else
                {
                    items = PlayerCharacter.ItemList.FindAll(i => i.ObjNo == no 
                        && i.IsDrive == false
                        && i.StrengthValue >= rec.RecipeMaterialsPlus[no]);
                }
            }
            else
            {
                items = PlayerCharacter.ItemList.FindAll(i => i.ObjNo == no
                    && i.IsDrive == false
                    && i.StrengthValue == rec.RecipeMaterialsPlus[no]);
            }
            for (int i = 0; i < items.Count; i++)
            {
                //自動選択が正なら上から順に素材を選択
                if (isAutoSelect == true && i < rec.RecipeMaterialsCount[no])
                {
                    ItemSelected.Add(items[i]);
                    NowSelectItemCount[no]++;
                }
                Items.Add(items[i].Name, items[i]);
            }
        }

        int index = 0;

        // 表示対象のアイテムの種類の数だけノードを生成
        foreach (BaseItem item in Items.Values)
        {

            //コピーを作成
            GameObject obj = GetStackObject(MaterialStack, itemSelectedUnit, recipeSelectMaterialViewContent.transform);

            //タイプ
            int typenum = (int)item.IType;
            typenum--;
            //obj.transform.FindChild("TypeImage").GetComponent<RawImage>().texture = ItemTypeTexture[typenum];
            obj.transform.Find("TypeImage").GetComponent<Image>().sprite = ItemSprite[typenum];

            //名前
            if (ItemSelected.Contains(item))
            {
                obj.transform.Find("SelectName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.SelectItem, item.DisplayNameInItem);
            }
            else
            {
                obj.transform.Find("SelectName").GetComponent<Text>().text = item.DisplayNameInItem;
            }

            //奇数の背景を変える
            obj.transform.Find("SelectedOdd").gameObject.SetActive(index % 2 == 0);
            index++;
            
            //選択背景を不活性化
            SetItemUnSelectBack(obj);
            
            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(obj, EventTriggerType.PointerDown, e => OnClickMaterialSelect(e, item));
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Drag, e => OnDragMaterialSelect(e), false);
            CommonFunction.AddListenerMenu(obj, EventTriggerType.Scroll, e => OnScrollMaterialSelect(e), false);
            //obj.SetActive(true);

            //リストに追加
            _ListViewUseMterialUnits.Add(item, obj);
        }

        foreach (long no in rec.RecipeMaterialsType.Keys)
        {
            //素材選択数を更新
            SetMaterialSelect(no);

            //_ListViewRecipeMterialUnits[no].transform.FindChild("MatSelectCount").GetComponent<Text>().text = NowSelectItemCount[no].ToString();
        }

        //コンテナの高さを決める
        float height = _ListViewUseMterialUnits.Count * 25f;
        int idx = 0;
        int n = _ListViewUseMterialUnits.Count;

        CommonFunction.SetCenterViewItem(height, ScrollViewSelectMaterial, idx, n);

        //一件目を選択状態にする
        SelectMaterialNo = _ListViewUseMterialUnits.Keys.First();
        SetItemSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);

        //素材オプションの更新
        SetMaterialOptions(SelectMaterialNo);
    }

    private TurnState UpdateAtelierMaterialSelect(TurnState state)
    {
        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //表示リストが0だったら無視
            if (_ListViewUseMterialUnits.Count == 0)
            {

            }
            //最初の項目だったら無視
            else if (_ListViewUseMterialUnits.Keys.First() == SelectMaterialNo)
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);
                //選択中の番号を取得
                int index = _ListViewUseMterialUnits.Keys.ToList().IndexOf(SelectMaterialNo);
                index--;
                SelectMaterialNo = _ListViewUseMterialUnits.Keys.ElementAt(index);
                //素材オプションの更新
                SetMaterialOptions(SelectMaterialNo);
                //背景色を変更
                SetItemSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);

                //コンテナの高さを決める
                float height = _ListViewUseMterialUnits.Count * 25f;
                int idx = index;
                int n = _ListViewUseMterialUnits.Count;

                CommonFunction.SetCenterViewItem(height, ScrollViewSelectMaterial, idx, n);

            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {

            ManageWait.Info.WaitSelect();

            //表示リストが0だったら無視
            if (_ListViewUseMterialUnits.Count == 0)
            {

            }
            //最後の項目だったら無視
            else if (_ListViewUseMterialUnits.Keys.Last() == SelectMaterialNo)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);
                //選択中の番号を取得
                int index = _ListViewUseMterialUnits.Keys.ToList().IndexOf(SelectMaterialNo);
                index++;
                SelectMaterialNo = _ListViewUseMterialUnits.Keys.ElementAt(index);
                //素材オプションの更新
                SetMaterialOptions(SelectMaterialNo);
                //背景色を変更
                SetItemSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);

                //コンテナの高さを決める
                float height = _ListViewUseMterialUnits.Count * 25f;
                int idx = index;
                int n = _ListViewUseMterialUnits.Count;

                CommonFunction.SetCenterViewItem(height, ScrollViewSelectMaterial, idx, n);
            }
        }
        //素材選択
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuMultiSelectOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuMultiSelectOk))
        {
            //素材がすでに含まれているとき
            if(ItemSelected.Contains(SelectMaterialNo) == true)
            {
                //選択素材を減らす
                ItemSelected.Remove(SelectMaterialNo);
                NowSelectItemCount[SelectMaterialNo.ObjNo]--;

                _ListViewUseMterialUnits[SelectMaterialNo].transform.Find("SelectName").GetComponent<Text>().text = SelectMaterialNo.DisplayNameInItem;

                //選択数を更新
                SetMaterialSelect(SelectMaterialNo.ObjNo);
                
            }
            //選択されていないとき
            else
            {
                //素材が選択上限に達していなければ選択、そうでなければキャンセル
                if(NowSelectItemCount[SelectMaterialNo.ObjNo] < Recipes[SelectRecipeNo].RecipeMaterialsCount[SelectMaterialNo.ObjNo])
                {
                    //選択素材を増やす
                    ItemSelected.Add(SelectMaterialNo);
                    NowSelectItemCount[SelectMaterialNo.ObjNo]++;

                    //名前の選択色を更新
                    _ListViewUseMterialUnits[SelectMaterialNo].transform.Find("SelectName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.SelectItem, SelectMaterialNo.DisplayNameInItem);

                    //選択数を更新
                    SetMaterialSelect(SelectMaterialNo.ObjNo);

                }
                else
                {
                    DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierMaxMaterial);
                }
            }

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

            ////ステートをレシピ選択に返す
            //state = TurnState.AtelierMain;
        }
        //素材決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
        {
            bool check = false;
            bool adhesive = false;
            string adhesiveName = "";
            //カウントチェック
            foreach(long no in Recipes[SelectRecipeNo].RecipeMaterialsCount.Keys)
            {
                if(Recipes[SelectRecipeNo].RecipeMaterialsCount[no] 
                    != NowSelectItemCount[no])
                {
                    check = true;
                }
            }
            //接着チェック
            foreach(BaseItem i in ItemSelected)
            {
                if(i.IsEquip && CommonFunction.HasOptionType(i.Options,OptionType.Adhesive))
                {
                    adhesiveName = i.DisplayNameInItem;
                    adhesive = true;
                }
            }

            if(check == true)
            {
                DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierMaterialNotRes1);
                if (KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.AtelierMaterialNotRes2UseMouse, KeyControlModel.GetName(
                            KeyControlInformation.Info.MenuMultiSelectOk).Trim()));
                }
                else
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.AtelierMaterialNotRes2, KeyControlModel.GetName(
                            KeyControlInformation.Info.MenuMultiSelectOk).Trim()));
                }
            }
            else
            {
                if (adhesive == true)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.ItemAdhesiveNotRemove, adhesiveName));
                }
                else
                {
                    //チェックが通っていれば作成画面へ
                    InitializeCreate(Recipes[SelectRecipeNo]);

                    state = TurnState.AtelierCreate;
                }
            }
        }
        //オプション参照
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.LookOption))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.LookOption))
        {

            if (CommonFunction.IsNull(Options) == false
                && Options.Count >= 1)
            {
                SelectOptionNo = Options[0];
                //素材オプションの更新
                SetOption(SelectOptionNo);

                //背景色を変更
                SetItemSelectBack(_ListViewOptionUnits[SelectOptionNo]);

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                //ステートを素材選択に返す
                state = TurnState.AtelierMatelialOption;
            }
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        {

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            //素材選択を初期化
            FinishMaterialSelect();

            //ステートをレシピ選択に返す
            state = TurnState.AtelierMain;
        }

        return state;
    }

    public void OnClickMaterialSelect(BaseEventData eventData, BaseItem a)
    {

        if (TempState != TurnState.AtelierMatelialSelect)
        {
            return;
        }

        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if (CommonFunction.IsDoubleClick())
            {
                KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuMultiSelectOk, true);
                //KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
                //TempState = ExeSubMenu();
                //IsClick = true;
            }
            else
            {

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
                
                SetItemUnSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);
                //選択中の番号を取得
                SelectMaterialNo = a;
                //素材オプションの更新
                SetMaterialOptions(SelectMaterialNo);
                //背景色を変更
                SetItemSelectBack(_ListViewUseMterialUnits[SelectMaterialNo]);

                CommonFunction.SetDragScrollViewFirstPosition(ScrollViewSelectMaterial);
            }
        }
        else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuCancel, true);
        }
    }

    public void OnDragMaterialSelect(BaseEventData eventData)
    {
        float height = _ListViewUseMterialUnits.Count * ItemUnitSize;
        int n = _ListViewUseMterialUnits.Count;

        CommonFunction.SetDragScrollViewItem(eventData, height, ScrollViewSelectMaterial, ItemUnitSize, n);
    }
    public void OnScrollMaterialSelect(BaseEventData eventData)
    {
        float height = _ListViewUseMterialUnits.Count * ItemUnitSize;
        int n = _ListViewUseMterialUnits.Count;

        CommonFunction.SetScrollScrollViewItem(eventData, height, ScrollViewSelectMaterial, ItemUnitSize, n);
    }

    private void SetMaterialSelect(long objno)
    {
        //素材選択数を更新
        //選択数に満たないとき
        if(NowSelectItemCount[objno] < Recipes[SelectRecipeNo].RecipeMaterialsCount[objno])
        {
            _ListViewRecipeMterialUnits[objno].transform.Find("MatSelectCount").GetComponent<Text>().text =
                string.Format("<color={0}>{1}</color>", CommonConst.Color.ItemBad, NowSelectItemCount[objno].ToString());
        }
        //選択数に足りたとき
        else
        {
            _ListViewRecipeMterialUnits[objno].transform.Find("MatSelectCount").GetComponent<Text>().text =
                    NowSelectItemCount[objno].ToString();
        }
    }

    private void FinishMaterialSelect()
    {
        //オプション表示を初期化
        foreach (GameObject g in _ListViewOptionUnits.Values)
        {
            DestroyStackObject(g, ScrollViewOption.transform);
        }
        _ListViewOptionUnits.Clear();

        //素材オプションの更新
        SetOption(null);
        Options = null;

        //素材項目を初期化
        foreach (GameObject g in _ListViewUseMterialUnits.Values)
        {
            DestroyStackObject(g, ScrollViewSelectMaterial.transform);
        }
        _ListViewUseMterialUnits.Clear();

        FinishSubMenu();

        Items.Clear();

    }
    private TurnState UpdateAtelierOptionSelect(TurnState state)
    {

        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //表示リストが0だったら無視
            if (_ListViewOptionUnits.Count == 0)
            {

            }
            //最初の項目だったら無視
            else if (_ListViewOptionUnits.Keys.First() == SelectOptionNo)
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewOptionUnits[SelectOptionNo]);
                //選択中の番号を取得
                int index = _ListViewOptionUnits.Keys.ToList().IndexOf(SelectOptionNo);
                index--;
                SelectOptionNo = _ListViewOptionUnits.Keys.ElementAt(index);
                //素材オプションの更新
                SetOption(SelectOptionNo);
                //背景色を変更
                SetItemSelectBack(_ListViewOptionUnits[SelectOptionNo]);

                //コンテナの高さを決める
                float height = _ListViewOptionUnits.Count * 25f;
                int idx = index;
                int n = _ListViewOptionUnits.Count;

                CommonFunction.SetCenterViewItem(height, ScrollViewOption, idx, n);

            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {
            //表示リストが0だったら無視
            if (_ListViewRecipeUnits.Count == 0)
            {

            }
            //最後の項目だったら無視
            else if (_ListViewOptionUnits.Keys.Last() == SelectOptionNo)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();

                SetItemUnSelectBack(_ListViewOptionUnits[SelectOptionNo]);
                //選択中の番号を取得
                int index = _ListViewOptionUnits.Keys.ToList().IndexOf(SelectOptionNo);
                index++;
                SelectOptionNo = _ListViewOptionUnits.Keys.ElementAt(index);
                //素材オプションの更新
                SetOption(SelectOptionNo);
                //背景色を変更
                SetItemSelectBack(_ListViewOptionUnits[SelectOptionNo]);

                //コンテナの高さを決める
                float height = _ListViewOptionUnits.Count * 25f;
                int idx = index;
                int n = _ListViewOptionUnits.Count;

                CommonFunction.SetCenterViewItem(height, ScrollViewOption, idx, n);
            }
        }
        ////オプション参照
        //else if (Input.GetKeyDown(KeyControlInformation.Info.LookOption))
        //{

        //    if (CommonFunction.IsNull(Options) == false
        //        && Options.Count >= 1)
        //    {
        //        //サウンドを鳴らす
        //        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

        //        //ステートを素材選択に返す
        //        state = TurnState.AtelierMatelialOption;
        //    }
        //}
        //キャンセル
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        {
            ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            //背景をもどす
            SetItemUnSelectBack(_ListViewOptionUnits[SelectOptionNo]);

            //素材オプションの更新
            SetOption(null);

            //ステートを素材選択に返す
            state = TurnState.AtelierMatelialSelect;
        }

        return state;
    }

    public void OnClickOption(BaseEventData eventData, BaseOption i)
    {
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
            
            if (CommonFunction.IsNull(SelectOptionNo) == false && _ListViewOptionUnits.ContainsKey(SelectOptionNo) == true)
            {
                SetItemUnSelectBack(_ListViewOptionUnits[SelectOptionNo]);
            }
            //選択中の番号を取得
            SelectOptionNo = i;
            //素材オプションの更新
            SetOption(SelectOptionNo);

            SetItemSelectBack(_ListViewOptionUnits[SelectOptionNo]);


            CommonFunction.SetDragScrollViewFirstPosition(ScrollViewOption);
        }
    }

    public void OnDragOption(BaseEventData eventData)
    {
        float height = _ListViewOptionUnits.Count * OptionUnit;
        int n = _ListViewOptionUnits.Count;

        CommonFunction.SetDragScrollViewItem(eventData, height, ScrollViewOption, OptionUnit, n);
    }
    public void OnScrollOption(BaseEventData eventData)
    {
        float height = _ListViewOptionUnits.Count * OptionUnit;
        int n = _ListViewOptionUnits.Count;

        CommonFunction.SetScrollScrollViewItem(eventData, height, ScrollViewOption, OptionUnit, n);
    }


    /// <summary>
    /// 対象のレシピが生成可能か調査
    /// </summary>
    /// <param name="rec"></param>
    /// <returns></returns>
    private bool CanCreate(RecipeInformation rec)
    {

        foreach (long no in rec.RecipeMaterialsType.Keys)
        {
            //対象素材の所持数をカウント
            int count = rec.CountHaveItem(no);

            //所持数が足りない場合は終了
            if (count < rec.RecipeMaterialsCount[no])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// レシピに対応した必要素材を更新
    /// </summary>
    /// <param name="rec"></param>
    private void SetRecipeMaterial(RecipeInformation rec)
    {

        //前回の表示項目を削除
        foreach (GameObject g in _ListViewRecipeMterialUnits.Values)
        {
            DestroyStackObject(g, ScrollViewRecipeMaterial.transform);
        }
        _ListViewRecipeMterialUnits.Clear();

        int index = 0;

        //レシピ素材の総重量を更新
        TotalMaterialWeight = 0;

        // 表示対象のアイテムの種類の数だけノードを生成
        foreach (long no in rec.RecipeMaterialsType.Keys)
        {
            //必要な素材を取得
            BaseItem mat = TableItemIncidence.GetItemObjNo(rec.RecipeMaterialsType[no], no, false);
            mat.StrengthValue = rec.RecipeMaterialsPlus[no];

            //対象素材の所持数をカウント
            int count = rec.CountHaveItem(no);

            //選択状態のコピーを取得
            GameObject copy = GetStackObject(RecipeMaterialStack, recipeMaterialUnit, recipeMaterialViewContent.transform);
            //GameObject copy = SetItemChild(recipeMaterialUnit, recipeMaterialViewContent);

            //名前 必要数が足りない場合は赤で表示
            if(count < rec.RecipeMaterialsCount[no])
            {
                copy.transform.Find("MatName").GetComponent<Text>().text = string.Format("<color={0}>{1}</color>", CommonConst.Color.ItemBad, mat.DisplayNameInItem);
            }
            else
            {
                copy.transform.Find("MatName").GetComponent<Text>().text = mat.DisplayNameInItem;
            }


            //タイプ
            int typenum = (int)mat.IType;
            typenum--;
            copy.transform.Find("TypeImage").GetComponent<Image>().sprite = ItemSprite[typenum];
            //copy.transform.FindChild("TypeImage").GetComponent<RawImage>().texture = ItemTypeTexture[typenum];

            //素材（必要数/所持数） 必要数が足りない場合は赤で表示
            if (count < rec.RecipeMaterialsCount[no])
            {
                copy.transform.Find("MatCount").GetComponent<Text>().text = string.Format("<color={0}>{2}</color>/{1}", CommonConst.Color.ItemBad, rec.RecipeMaterialsCount[no], count);
            }
            else
            {
                copy.transform.Find("MatCount").GetComponent<Text>().text = string.Format("{1}/{0}", rec.RecipeMaterialsCount[no], count);
            }

            //奇数の背景を変える
            copy.transform.Find("SelectedOdd").gameObject.SetActive(index % 2 == 0);
            index++;

            SetItemUnSelectBack(copy);

            copy.SetActive(true);

            TotalMaterialWeight += mat.Weight;

            //作った素材をリストに追加
            _ListViewRecipeMterialUnits.Add(no, copy);
        }
    }

    /// <summary>
    /// 素材オプションを更新
    /// </summary>
    /// <param name="rec"></param>
    private void SetMaterialOptions(BaseItem item)
    {
        //前回の表示項目を削除
        foreach (GameObject g in _ListViewOptionUnits.Values)
        {
            DestroyStackObject(g, ScrollViewOption.transform);
        }
        _ListViewOptionUnits.Clear();

        OptionDetaiDescription.GetComponent<Text>().text = "";


        if (CommonFunction.IsNull(item) == false && item.IsAnalyse == true)
        {
            Options = item.Options;
        }
        else
        {
            Options = null;
        }

        //表示対象がなければ終了
        if (CommonFunction.IsNull(Options) == true || Options.Count == 0)
        {
            return;
        }

        int index = 0;

        //オプションの数だけノードを作成
        foreach (BaseOption bo in Options)
        {
            //コピーを作成
            GameObject copy = GetStackObject(OptionStack, OptionSelectedUnit, OptionViewContent.transform);

            //GameObject copy = GameObject.Instantiate(OptionSelectedUnit, OptionViewContent.transform);
            SetItemUnSelectBack(copy);

            copy.GetComponentInChildren<Text>().text = bo.DisplayNameInItem;
            copy.transform.Find("SelectedOdd").gameObject.SetActive(index % 2 == 0);
            
            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(copy, EventTriggerType.PointerDown, e => OnClickOption(e, bo));
            CommonFunction.AddListenerMenu(copy, EventTriggerType.Drag, e => OnDragOption(e), false);
            CommonFunction.AddListenerMenu(copy, EventTriggerType.Scroll, e => OnScrollOption(e), false);
            
            _ListViewOptionUnits.Add(bo, copy);
            index++;
        }
        
    }

    private void SetOption(BaseOption op)
    {
        if(CommonFunction.IsNull(op) == false)
        {
            OptionDetaiDescription.GetComponent<Text>().text = op.Description;
        }
        else
        {
            OptionDetaiDescription.GetComponent<Text>().text = "";
        }
    }


    #region　作成

    private void InitializeCreate(RecipeInformation rec)
    {
        AtelierCreatePanel.SetActive(true);

        //調合アイテムを作成
        CreateItem = rec.AtelierExecute(ItemSelected);

        //名前
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemName").GetComponent<Text>().text =
            CreateItem.DisplayNameNormal;

        //攻撃力
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemAttackValue").GetComponent<Text>().text =
            CreateItem.GetAttack();

        //防御力
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemDefenseValue").GetComponent<Text>().text =
            CreateItem.GetDefense();

        //武器か盾の場合はオプションを更新する
        SetOptionValues(CreateItem);

        //説明
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemDescription").GetComponent<Text>().text =
            CreateItem.GetDescription();

        //ボタンを色付け
        SetTabSelect(1);
        SetTabUnSelect(2);
        SelectNowBtn = 1;
    }

    private void SetOptionValues(BaseItem item)
    {
        //Options = options;

        //前の情報を削除
        foreach (GameObject g in _ListViewCreateOptionUnits.Values)
        {
            DestroyStackObject(g, AtelierOptionScrolllView.transform);

        }
        _ListViewCreateOptionUnits.Clear();

        //説明
        AtelierCreatePanel.transform.Find("AtelierOptionDescription").GetComponent<Text>().text =
            "";

        //表示対象がなければ終了
        if (CommonFunction.IsNull(item.Options) == true || item.Options.Count == 0)
        {

            AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemOptionCountValue").GetComponent<Text>().text =
                    "";

            return;
        }
        AtelierCreatePanel.transform.Find("AtelierItemDetail/AtelierItemOptionCountValue").GetComponent<Text>().text =
            item.Options.Count().ToString();

        int index = 0;
        foreach (BaseOption bo in item.Options)
        {
            //GameObject copy = GameObject.Instantiate(OptionSelectedUnit, AtelierCreateOptionContent.transform);
            //コピーを作成
            GameObject copy = GetStackObject(CreateOptionStack, OptionSelectedUnit, AtelierCreateOptionContent.transform);

            //copy.SetActive(true);
            SetItemUnSelectBack(copy);
            copy.GetComponentInChildren<Text>().text = bo.DisplayNameInItem;
            copy.transform.Find("SelectedOdd").gameObject.SetActive(index % 2 == 0);
            _ListViewCreateOptionUnits.Add(bo, copy);
            
            //イベントハンドラの設定
            CommonFunction.AddListenerMenu(copy, EventTriggerType.PointerDown, e => OnClickCreateOption(e, bo));
            CommonFunction.AddListenerMenu(copy, EventTriggerType.Drag, e => OnDragCreateOption(e), false);
            CommonFunction.AddListenerMenu(copy, EventTriggerType.Scroll, e => OnScrollCreateOption(e), false);

            index++;
        }

        SelectCreateOptionNo = item.Options[0];
        SetItemSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
        //SetOption(options[SelectCreateOptionNo]);
        AtelierCreatePanel.transform.Find("AtelierOptionDescription").GetComponent<Text>().text =
            SelectCreateOptionNo.Description;
    }

    private TurnState UpdateCreate(TurnState nowState)
    {
        //上
        if (KeyControlInformation.Info.OnMoveUp())
        {
            //表示リストが0だったら無視
            if (_ListViewCreateOptionUnits.Count == 0)
            {

            }
            //最初の項目だったら無視
            else if (_ListViewCreateOptionUnits.Keys.First() == SelectCreateOptionNo)
            {

            }
            //それ以外だったら1つ上の項目に移動
            else
            {
                //遊び
                ManageWait.Info.WaitSelect();

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
                
                SetItemUnSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
                //選択中の番号を取得
                int index = _ListViewCreateOptionUnits.Keys.ToList().IndexOf(SelectCreateOptionNo);
                index--;
                SelectCreateOptionNo = _ListViewCreateOptionUnits.Keys.ElementAt(index);
                //詳細の更新
                AtelierCreatePanel.transform.Find("AtelierOptionDescription").GetComponent<Text>().text =
                    SelectCreateOptionNo.Description;
                SetItemSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
                
                //コンテナの高さを決める
                float height = _ListViewCreateOptionUnits.Count * 25f;
                int idx = index;
                int n = _ListViewCreateOptionUnits.Count;

                CommonFunction.SetCenterViewItem(height, AtelierOptionScrolllView, idx, n);
            }
        }
        //下
        else if (KeyControlInformation.Info.OnMoveDown())
        {
            //表示リストが0だったら無視
            if (_ListViewCreateOptionUnits.Count == 0)
            {

            }
            //最後の項目だったら無視
            else if (_ListViewCreateOptionUnits.Keys.Last() == SelectCreateOptionNo)
            {

            }
            //それ以外だったら1つ下の項目に移動
            else
            {
                //遊び
                ManageWait.Info.WaitSelect();

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
                
                SetItemUnSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
                //選択中の番号を取得
                int index = _ListViewCreateOptionUnits.Keys.ToList().IndexOf(SelectCreateOptionNo);
                index++;
                SelectCreateOptionNo = _ListViewCreateOptionUnits.Keys.ElementAt(index);
                //詳細の更新
                AtelierCreatePanel.transform.Find("AtelierOptionDescription").GetComponent<Text>().text =
                    SelectCreateOptionNo.Description;
                SetItemSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);

                //コンテナの高さを決める
                float height = _ListViewCreateOptionUnits.Count * 25f;
                int idx = index;
                int n = _ListViewCreateOptionUnits.Count;

                CommonFunction.SetCenterViewItem(height, AtelierOptionScrolllView, idx, n);
            }
        }
        //左
        else if (KeyControlInformation.Info.OnMoveLeft())
        {

            //最初の項目だったら無視
            if (SelectNowBtn == 1)
            {

            }
            //それ以外だったら1つ左のタブに移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();
                SetTabUnSelect(SelectNowBtn);
                SelectNowBtn--;
                SetTabSelect(SelectNowBtn);
            }
        }
        //右
        else if (KeyControlInformation.Info.OnMoveRight())
        {
            //最後の項目だったら無視
            if (SelectNowBtn == 2)
            {

            }
            //それ以外だったら1つ右のタブに移動
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                ManageWait.Info.WaitSelect();
                SetTabUnSelect(SelectNowBtn);
                SelectNowBtn++;
                SetTabSelect(SelectNowBtn);
            }
        }
        //決定
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
        {
            if (SelectNowBtn == 1)
            {
                FinishCreate();
                FinishMaterialSelect();
                FinishSubMenu();
                FinishAtelierMenu();

                nowState = TurnState.AtelierCreateExecute;
            }
            else if(SelectNowBtn == 2)
            {

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

                FinishCreate();

                nowState = TurnState.AtelierMatelialSelect;
            }
        }
        //キャンセル
        else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
        //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

            FinishCreate();

            nowState = TurnState.AtelierMatelialSelect;
        }
        return nowState;
    }


    public void OnClickCreateOption(BaseEventData eventData, BaseOption i)
    {
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);
            
            if (CommonFunction.IsNull(SelectCreateOptionNo) == false && _ListViewCreateOptionUnits.ContainsKey(SelectCreateOptionNo) == true)
            {
                SetItemUnSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
            }

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

            SetItemUnSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);
            //選択中の番号を取得
            SelectCreateOptionNo = i;
            //詳細の更新
            AtelierCreatePanel.transform.Find("AtelierOptionDescription").GetComponent<Text>().text =
                SelectCreateOptionNo.Description;
            SetItemSelectBack(_ListViewCreateOptionUnits[SelectCreateOptionNo]);


            CommonFunction.SetDragScrollViewFirstPosition(AtelierOptionScrolllView);
        }
    }

    public void OnDragCreateOption(BaseEventData eventData)
    {
        float height = _ListViewCreateOptionUnits.Count * OptionUnit;
        int n = _ListViewCreateOptionUnits.Count;

        CommonFunction.SetDragScrollViewItem(eventData, height, AtelierOptionScrolllView, OptionUnit, n);
    }
    public void OnScrollCreateOption(BaseEventData eventData)
    {
        float height = _ListViewCreateOptionUnits.Count * OptionUnit;
        int n = _ListViewCreateOptionUnits.Count;

        CommonFunction.SetScrollScrollViewItem(eventData, height, AtelierOptionScrolllView, OptionUnit, n);
    }


    public void CreateExecute(PlayerCharacter player, KilnObject kiln)
    {
        //使用する素材をアイテムから削る
        foreach(BaseItem i in ItemSelected)
        {
            if(i.IsEquip == true)
            {
                i.ForceRemoveEquip(player);
            }
            PlayerCharacter.RemoveItem(i);
        }
        ItemSelected.Clear();

        //成功
        if (IsSuccess == true ||
            CommonFunction.IsRandom(PlayerInformation.Info.AlcReasonableFloat) == true)
        //if(false)
        {
            IsSuccess = false;

            EffectFlareCore s = EffectFlareCore.CreateObject(kiln, EffectFlareCore.FLareCoreType.Yellow);
            s.Play();

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.Levelup);

            DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierSuccess);
            
        }
        //失敗
        else
        {
            EffectSmoke s = EffectSmoke.CreateObject(kiln);
            s.SetColor(Color.gray);
            s.Play();

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.Bomb);

            DisplayInformation.Info.AddMessage(CommonConst.Message.AtelierFail);

            CreateItem = null;
        }
    }

    private void FinishCreate()
    {

        //前の情報を削除
        foreach (GameObject g in _ListViewCreateOptionUnits.Values)
        {
            DestroyStackObject(g, AtelierOptionScrolllView.transform);
        }
        _ListViewCreateOptionUnits.Clear();
        
        AtelierCreatePanel.SetActive(false);
    }
    #endregion

    public void OnClickCreate()
    {
        // 選択を解除
        EventSystem.current.SetSelectedGameObject(null);
        KeyControlInformation.Info.SetPushKeyOneTime(KeyType.MenuOk, true);
    }

    public void OnClickCancel()
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

    public void ObjectClear()
    {
        CreateItem = null;
        ItemSelected.Clear();
    }

    /// <summary>
    /// 対象位置のタブ背景を非選択状態にする
    /// </summary>
    private void SetTabUnSelect(sbyte num)
    {
        GameObject obj = null;
        switch(num)
        {
            case 1:
                obj = AtelierCreatePanel.transform.Find("AtelierCreateButton").gameObject;
                break;
            case 2:
                obj = AtelierCreatePanel.transform.Find("AtelierCancelButton").gameObject;
                break;
        }
        Image col = obj.transform.GetComponent<Image>();
        col.color = Color.white;
    }

    /// <summary>
    /// 対象位置のタブ背景を選択状態にする
    /// </summary>
    private void SetTabSelect(sbyte num)
    {
        GameObject obj = null;
        switch (num)
        {
            case 1:
                obj = AtelierCreatePanel.transform.Find("AtelierCreateButton").gameObject;
                break;
            case 2:
                obj = AtelierCreatePanel.transform.Find("AtelierCancelButton").gameObject;
                break;
        }
        Image col = obj.transform.GetComponent<Image>();
        col.color = Color.yellow;
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



    private GameObject GetStackObject(List<GameObject> list, GameObject origin, Transform tf)
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
    private void DestroyStackObject(GameObject gm, Transform parent)
    {
        gm.transform.SetParent(parent);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.PointerDown);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.Drag);
        CommonFunction.RemoveAllListeners(gm, EventTriggerType.Scroll);
        gm.SetActive(false);
    }
}
using Assets.Scripts;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

public class ManageGame : MonoBehaviour
{
    
    /// <summary>
    /// フェード中の透明度
    /// </summary>
    private float fadeAlpha = 0;

    /// <summary>
    /// フェード中かどうか
    /// </summary>
    private bool isFading = false;

    public GameObject Camera;
    //public GameObject Player;
    //public GameObject StellaPlayer;
    private GameObject _arrows;
    private GameObject _arrowT;
    private GameObject _arrowTL;
    private GameObject _arrowTR;
    private GameObject _arrowL;
    private GameObject _arrowR;
    private GameObject _arrowBL;
    private GameObject _arrowBR;
    private GameObject _arrowB;
    private GameObject _keyDisp;
    private GameObject _messageLog;
    private GameObject _messageLogText;
    private GameObject _messageLogTextContent;
    private GameObject _messageLogTextScrollView;
    private GameObject _messageLogCloseButton;
    private TurnState _state;
    public PlayerCharacter _player;
    private ManageDungeon _model;
    private ManageMenu _menu;
    private ManageSystemMenu _systemMenu;
    private ManageAtelier _atelierMenu;
    
    private ushort _turn;
    //private bool IsWait;
    private ushort nowFloor;
    private bool IsMessagelog;
    private bool IsStair;
    private long PreviouslyTimestamp;
    private DateTime GameStartTimestamp;
    private DateTime FloorTimestamp;
    private ManageFade _manageFade;
    private bool isRTS;
    private ManageRTS mwRTS;

    private List<BaseTrap> NewTraps;
    private List<BaseItem> destList;
    private List<BaseItem> WaterInItem;
    private List<BaseItem> addList;
    private List<GameObject> Messages;
    private Dictionary<CharacterDirection, bool> ArrowState;

    private void Awake()
    {
        //Application.targetFrameRate = 30; //30FPSに設定
        ResourceInformation.DungeonInit();

        if (CommonFunction.IsNull(ResourceInformation.SaveInfo) == true)
        {
            ResourceInformation.SaveInfo = new SavePlayingInformation();
        }
#if UNITY_EDITOR
        if (false)
        {
            ResourceInformation.SaveInfo.f = 5;
            ResourceInformation.SaveInfo.lv = 40;
            ResourceInformation.SaveInfo.hv = 60;
        }

#endif
    }


    // Use this for initialization


    void Start()
    {

#if UNITY_EDITOR
        try
        {
            
            StartMain();

            //StartCoroutine(Tester());
            //DateTime dt = DateTime.Now;
            //for (int i = 0; i < 1000000; i++)
            //{
            //    string tes = CommonConst.Message.AddAcceleration;
            //}
            //CommonFunction.OutLog(DateTime.Now - dt);
            //dt = DateTime.Now;
            //for (int i = 0; i < 1000000; i++)
            //{
            //    string tes = CommonConst.Message.DriveFull;
            //}
            //CommonFunction.OutLog(DateTime.Now - dt);

        }
        catch (Exception e)
        {
            StartCoroutine(ErrorInformation.Info.SendLogCorutine(e));
            throw e;
        }

#else

        try
        {
            StartMain();
        }
        catch(Exception e)
        {
            StartCoroutine(ErrorInformation.Info.SendLogCorutine(e));
        }
#endif
    }

#if UNITY_EDITOR

    private IEnumerator Tester()
    {

        DateTime dt = DateTime.Now;
        CommonFunction.OutLog((System.GC.GetTotalMemory(false) / 1024).ToString("#,0"));
        float test = 0;
        while (test < 1)
        {
            test += CommonFunction.GetDelta(1);
            yield return null;
        }
        CommonFunction.OutLog((System.GC.GetTotalMemory(false) / 1024).ToString("#,0"));
        CommonFunction.OutLog(DateTime.Now - dt);
        dt = DateTime.Now;
        test = Time.time + 1;
        CommonFunction.OutLog(Time.time);
        while (test > Time.time)
        {
            yield return null;
        }
        CommonFunction.OutLog((System.GC.GetTotalMemory(false) / 1024).ToString("#,0"));
        CommonFunction.OutLog(DateTime.Now - dt);
        yield return null;
    }
#endif

    void StartMain()
    {
        ErrorInformation.Info.Initialize();

        try
        {

            SavePlayingInformation save = ResourceInformation.SaveInfo;

            DisplayInformation.InfoClone.Initialize();

            switch (PlayerInformation.Info.PType)
            {
                case PlayerType.UnityChan:
                    _player = new PlayerCharacter();
                    _player.Initialize();
                    Camera.GetComponent<AutoCamCst>().SetTarget(_player.ThisDisplayObject.transform);
                    _player.EquipLeft = GameObject.Find("UnityEquipLeft");
                    _player.EquipRight = GameObject.Find("UnityEquipRight");
                    break;
                case PlayerType.OricharChan:
                    _player = new PlayerCharacter();
                    _player.Initialize();
                    Camera.GetComponent<AutoCamCst>().SetTarget(_player.ThisDisplayObject.transform);
                    _player.EquipLeft = GameObject.Find("StellaEquipLeft");
                    _player.EquipRight = GameObject.Find("StellaEquipRight");
                    _player.StellaGuns = new GameObject[2];
                    _player.StellaGuns[0] = GameObject.Find("STLSD_obj_gunM");
                    _player.StellaGuns[0].SetActive(false);
                    _player.StellaGuns[1] = GameObject.Find("STLSD_obj_gunS");
                    _player.StellaGuns[1].SetActive(false);

                    break;
            }

            GameStateInformation.Info.PlayerName = _player.Name;

            _arrows = GameObject.Find("SlashMoveArrows");
            _arrowT = _arrows.transform.Find("ArrowT").gameObject;
            _arrowTR = _arrows.transform.Find("ArrowTR").gameObject;
            _arrowTL = _arrows.transform.Find("ArrowTL").gameObject;
            _arrowL = _arrows.transform.Find("ArrowL").gameObject;
            _arrowR = _arrows.transform.Find("ArrowR").gameObject;
            _arrowBR = _arrows.transform.Find("ArrowBR").gameObject;
            _arrowBL = _arrows.transform.Find("ArrowBL").gameObject;
            _arrowB = _arrows.transform.Find("ArrowB").gameObject;
            //ArrowFalse();
            _keyDisp = GameObject.Find("KeyDisplayPanel");
            _keyDisp.transform.Find("KeyDisplayTitle").GetComponent<Text>().text = CommonConst.Message.KeySetting;
            _keyDisp.SetActive(false);
            _messageLog = GameObject.Find("MessageLogArea");
            _messageLogText = _messageLog.transform.Find("Panel/MessageLogText").gameObject;
            _messageLogTextScrollView = _messageLog.transform.Find("Panel/MessageLogTextScrollView").gameObject;
            _messageLogTextContent = _messageLogTextScrollView.transform.Find("Viewport/MessageLogTextContent").gameObject;
            _messageLogCloseButton = _messageLog.transform.Find("Panel/MessageLogClose").gameObject;

            NewTraps = new List<BaseTrap>();
            destList = new List<BaseItem>();
            WaterInItem = new List<BaseItem>();
            addList = new List<BaseItem>();
            ArrowState = new Dictionary<CharacterDirection, bool>(new CharacterDirectionComparer());
            ArrowState.Add(CharacterDirection.Top, false);
            ArrowState.Add(CharacterDirection.TopLeft, false);
            ArrowState.Add(CharacterDirection.Left, false);
            ArrowState.Add(CharacterDirection.BottomLeft, false);
            ArrowState.Add(CharacterDirection.Bottom, false);
            ArrowState.Add(CharacterDirection.BottomRight, false);
            ArrowState.Add(CharacterDirection.Right, false);
            ArrowState.Add(CharacterDirection.TopRight, false);
            ArrowUpdate();

            Messages = new List<GameObject>();

            GameStateInformation.Info.Initialize();
            if (save.IsLoadSave == true)
            {
                GameStateInformation.Info.SetupSave(save);
            }
            else
            {
                GameStateInformation.Info.SetupAnalyseFirst(DungeonInformation.Info.IsAnalyze);
            }

            isRTS = DungeonInformation.Info.IsTimer;
            if (isRTS == true)
            {
                GameObject gm3 = new GameObject("RtsManager");
                mwRTS = gm3.AddComponent<ManageRTS>();
            }

            _messageLog.SetActive(false);

            
            //保存されていたら保存データを読み込み
            //if(save.IsLoadSave == true)
            //{
            //    //アイテムデータをロード
            //    SaveItemData[] items = SaveDataInformation.LoadItemValue();

            //    //アイテムデータを反映
            //    PlayerCharacter.ItemList = SaveItemInformation.ToListBaseItem(items);

            //    //セーブデータを削除
            //    SaveDataInformation.RemoveSaveData();
            //}

            MusicInformation.Music.Setup(DungeonInformation.Info.MType);


            _model = GetComponent<ManageDungeon>();
            _menu = GetComponent<ManageMenu>();
            _systemMenu = GetComponent<ManageSystemMenu>();
            _atelierMenu = GetComponent<ManageAtelier>();
            

            //セーブデータの読み込み
            _player.SetFirstStatus(save);
            //_player.Initialize();

            //セーブデータをCharacterInfoとDungeonInfoに反映

            DungeonHistoryInformation.InitializeDungeonHistoryInformation();
            DungeonHistoryInformation.Info.vcPlayerName = _player.Name.ToString("D");

            _model.DungeonObjNo = DungeonInformation.Info.DungeonObjNo;

            GameObject gm = new GameObject("FloorChanger");
            _manageFade = gm.AddComponent<ManageFade>();
            _manageFade.SetupFade(DungeonInformation.Info.Name);
            _manageFade.PlayFadeOut(save.f, true);
            
            ScoreInformation.InitializeScoreInformation();
            ScoreInformation.Info.DungeonName = DungeonInformation.Info.Name;
            ScoreInformation.Info.iDungeonId = (int)DungeonInformation.Info.DungeonObjNo;

            //開始時間
            PreviouslyTimestamp = save.pt;
            GameStartTimestamp = DateTime.Now;
            FloorTimestamp = DateTime.Now;

            _model.Setup(DungeonInformation.Info.X, DungeonInformation.Info.Y);
            _model.CommonProb = DungeonInformation.Info.Prob;
            _model.Kiln = new KilnObject();
            nowFloor = save.f;
            SetupFloor(save.f);

            ResourceInformation.SaveInfo = new SavePlayingInformation();

            //暗闇の時
            if ((_player.CharacterAbnormalState & (int)StateAbnormal.Dark) != 0)
            {
                //世界を消す
                _model.OnDark();
                CommonFunction.SetActive(ResourceInformation.MapPanel, false);
                //ResourceInformation.MapPanel.SetActive(false);
            }
            //暗闇ではないとき
            else if ((_player.CharacterAbnormalState & (int)StateAbnormal.Dark) == 0)
            {
                //世界をつける
                _model.OffDark();
                CommonFunction.SetActive(ResourceInformation.MapPanel, true);
                //ResourceInformation.MapPanel.SetActive(true);
            }

#if !UNITY_EDITOR
        GameObject.Find("PanelTurnCount").SetActive(false);
#endif

            //for (uint i = 1; i <= uint.MaxValue ;i++)
            //{
            //    if (TableTrapIncidence.GetTrapTest(1, i) == false)
            //    {
            //        UnityEngine.Debug.Log(i);
            //    }
            //}

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //for (int i = 0; i < 200000; i++)
            //{
            //    sb.AppendLine(CommonFunction.ConvergenceRandom(1, 0.4f, 5f).ToString());
            //    //sb.AppendLine(CommonFunction.IsRandom(0.96f).ToString());
            //}
            //CommonFunction.Dump(sb);
            //test = new Dictionary<Guid, string>(new GuidComparer());
            //test.Add(Guid.NewGuid(), "testes1");
            //test.Add(Guid.NewGuid(), "testes2");
            //test.Add(Guid.NewGuid(), "testes3");
            //test.Add(Guid.NewGuid(), "testes4");
        }
        catch(Exception e)
        {
            StartCoroutine(ErrorInformation.Info.SendLogCorutine(e));
            throw e;
        }
    }

    void Update()
    {

#if UNITY_EDITOR
        try
        {
            UpdateMain();
        }
        catch (Exception e)
        {
            StartCoroutine(
                ErrorInformation.Info.SendLogCorutine(e, 
                    CommonFunction.CammaString(_state,
                        GameStateInformation.Info.Seed,
                        _model.CreateErrorLog())
                    ));
            _state = TurnState.TurnFinish;
            throw e;
        }

#else
        
        try
        {
            UpdateMain();
        }
        catch(Exception e)
        {
            StartCoroutine(
                ErrorInformation.Info.SendLogCorutine(e, 
                    CommonFunction.CammaString(_state,
                        GameStateInformation.Info.Seed,
                        _model.CreateErrorLog())
                    ));
            _state = TurnState.TurnFinish;
        }
#endif
    }

    // Update is called once per frame
    void UpdateMain()
    {
        //KeyControlInformation.Info.SetPushUpClear();

        if (ManageWait.Info.IsWait == true)
        {
            return;
        }
#region フロア移動
        if (_state == TurnState.NextFloor)
        {
            ArrowFalse();
            ArrowUpdate();

            _player.OffAllAnimation();

            //51階でクリア
            if (nowFloor > DungeonInformation.Info.DisruptFloor)
            {
                ScoreInformation.Info.isDisrupt = true;
                //レベル50を基準に低いほど高得点
                int level = DungeonInformation.Info.DisruptFloor - _player.Level;
                ScoreInformation.Info.AddScore(level * 1000);
                TransitionScoreScene();
                _manageFade.Play(FadeState.FadeIn, true);
                _state = TurnState.PlayerDeath;
                return;
            }

            if (_manageFade.IsFadeChange == true)
            {
                nowFloor++;
                _manageFade.Play(FadeState.FadeIn, nowFloor, true);
            }
            //画面が暗くなったら再作成を開始する
            if (_manageFade.IsFadeinEnd == true)
            {
                SetupFloor(nowFloor);
                //_player.ChangeDirection(CharacterDirection.Bottom);
                _state = TurnState.TurnFinish;
                _model.IsRemap = true;
            }
        }
#endregion フロア移動


        if(_manageFade.FadeState != FadeState.None)
        {
            return;
        }

#region 入力
        if (_keyDisp.activeSelf == true)
        {
            _keyDisp.SetActive(false);
        }


        #region メニュー終了
        if (_state == TurnState.MenuExit)
        {
            ManageWait.Info.Wait(CommonConst.Wait.MenuMove);
            
            if (isRTS == true)
            {
                mwRTS.Wait();
            }
            if (GameStateInformation.Info.IsRestCount == true)
            {
                GameStateInformation.Info.IsRestCount = false;
                _player.RestActionTurn--;
                _state = TurnState.EnemyInit;
            }
            else
            {
                _state = TurnState.Player;
            }
        }
        #endregion メニュー終了


        //キー表示の場合はキー表示を優先する
        if (Input.GetKey(KeyControlInformation.Info.KeyDisplay) && _state != TurnState.SystemMenu)
        {
            SetKeyDisplay();
        }
        else if(IsMessagelog == true)
        {
            SetMessageLogScroll();
            //if (Input.GetKey(KeyControlInformation.Info.MessageLog)
            //    || Input.GetKey(KeyControlInformation.Info.MenuCancel))
            if(KeyControlInformation.Info.OnKeyDownField(KeyType.MessageLog)
                || KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuCancel))
            {
                IsMessagelog = false;
                _messageLog.SetActive(false);
                ManageWait.Info.Wait(CommonConst.Wait.MenuMove);
                _state = TurnState.MenuExit;
            }
        }
        else
        {
            if(IsStair == true && _state == TurnState.Player && _player.RestActionTurn >= 1)
            {
                _menu.InitializeStair();
                _state = TurnState.FirstMenu;
                IsStair = false;
            }
            //ターン消費が回復しきっていなければ行動順を飛ばす
            if(_state == TurnState.Player && _player.RestActionTurn < 1)
            {

                //foreach (BaseCharacter c in _model.Characters.Values)
                //{
                //    c.Acc();
                //}
                _state = TurnState.EnemyInit;
            }
            //睡眠状態だったら行動順を飛ばす
            if (_state == TurnState.Player && (_player.CharacterAbnormalState & (int)StateAbnormal.Sleep) != 0)
            {
                ManageWait.Info.Wait(CommonConst.Wait.Sleep);

                _player.RestActionTurn--;
                _state = TurnState.EnemyInit;
                return;
            }

            switch (_state)
            {
                //メニュー系のステートの時はManageMenuに処理を任せる
                case TurnState.FirstMenu:

                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemMenu:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemSubMenu:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemOption:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;

                case TurnState.ItemAnalyse:
                    _state = _menu.UpdateDisplay(_state, _player);

                    break;
                case TurnState.ItemInDrive:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemInDriveFromDrive:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.LookDrive:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemTargetSelect:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.ItemDelete:
                    _state = _menu.UpdateDisplay(_state, _player);
                    break;
                case TurnState.SystemMenuInit:

                    //遊びを入れる
                    ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                    _systemMenu.Initialize(_player);
                    _state = TurnState.SystemMenu;
                    break;
                case TurnState.SystemMenu:
                    _state = _systemMenu.UpdateSystemMenu(_state);
                    break;
                case TurnState.AtelierMainInit:

                    //遊びを入れる
                    ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                    _atelierMenu.InitializeRecipeMenu();
                    _state = TurnState.AtelierMain;
                    break;
                case TurnState.AtelierStrengthInit:

                    //遊びを入れる
                    ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

                    _atelierMenu.InitializeStrengthMenu();
                    _state = TurnState.AtelierMain;
                    break;
                case TurnState.AtelierMain:
                    _state = _atelierMenu.UpdateMainMenu(_state);
                    break;
                case TurnState.AtelierSubAction:
                    _state = _atelierMenu.UpdateMainMenu(_state);
                    break;
                case TurnState.AtelierMatelialSelect:
                    _state = _atelierMenu.UpdateMainMenu(_state);
                    break;
                case TurnState.AtelierMatelialOption:
                    _state = _atelierMenu.UpdateMainMenu(_state);
                    break;
                case TurnState.AtelierCreate:
                    _state = _atelierMenu.UpdateMainMenu(_state);
                    break;
                case TurnState.Player:
                    ArrowFalse();

                    var enumerator = _model.Characters.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        BaseCharacter c = enumerator.Current;
                        c.UpdateIdleStatus();
                    }
                    


                    if (isRTS == true)
                    {
                        if(mwRTS.IsWait == false)
                        {
                            _player.RestActionTurn--;
                            _state = TurnState.EnemyInit;
                        }
                    }

#region　フィールド操作

                    //メッセージログ
                    if (KeyControlInformation.Info.OnKeyDownField(KeyType.MessageLog))
                    //    if (Input.GetKey(KeyControlInformation.Info.MessageLog))
                    {
                        IsMessagelog = true;
                        _messageLog.SetActive(true);
                        SetMessageLog();
                        ManageWait.Info.Wait(CommonConst.Wait.MenuMove);
                    }
#if UNITY_EDITOR
                    //メニュー
                    else if (Input.GetKeyDown(KeyCode.Y))
                    {
                        //サウンドを鳴らす
                        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                        _menu.InitializeAtelier();
                        _state = TurnState.FirstMenu;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        throw new InvalidOperationException();
                    }
#endif
                    //メニュー
                    //else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOpen))
                    else if (KeyControlInformation.Info.OnKeyDownField(KeyType.MenuOpen))
                    {
                        //サウンドを鳴らす
                        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                        _menu.InitializeFirstMenu();
                        _state = TurnState.FirstMenu;
                    }
                    //足踏み
                    //else if (Input.GetKey(KeyControlInformation.Info.Idle))
                    else if (KeyControlInformation.Info.OnKeyField(KeyType.Idle))
                    {
                        _player.Idle();
                        _state = TurnState.EnemyInit;
                        ManageWait.Info.Wait(CommonConst.Wait.Idle);

                    }
                    else
                    //攻撃
                    //if (Input.GetKey(KeyControlInformation.Info.Attack))
                    if (KeyControlInformation.Info.OnKeyField(KeyType.Attack))
                    {
                        //トラップが前にあったら可視化
                        MapPoint mp = _player.CurrentPoint.Add(CommonFunction.CharacterDirectionVector[_player.Direction]);
                        _model.SetUpTrapMap();
                        BaseTrap tr = _model.TrapMap.Get(mp);
                        if (CommonFunction.IsNull(tr) == false)
                        {
                            tr.IsVisible = true;
                        }
                        //窯が前にあったら調合メニュー
                        _model.SetUpCharacterMap();
                        BaseCharacter kiln = _model.CharacterMap.Get(mp);
                        if (CommonFunction.IsNull(kiln) == false && kiln.Type == ObjectType.Kiln)
                        {
                            //サウンドを鳴らす
                            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);
                            _menu.InitializeAtelier();
                            _state = TurnState.FirstMenu;
                        }
                        else
                        {
                            //攻撃判定
                            BaseCharacter target = _player.Attack(_model);
                            //if(CommonFunction.IsNull(target) == false)
                            //{
                            //    _model.RemoveCharacter(target);
                            //}
                            _state = TurnState.EnemyInit;
                        }
                    }
                    //必殺技
                    //else if(Input.GetKey(KeyControlInformation.Info.DeathBlow))
                    else if (KeyControlInformation.Info.OnKeyDownField(KeyType.DeathBlow))
                    {
                        bool res = _player.DeathBlow(_model);
                        if (res == true)
                        {
                            _state = TurnState.EnemyInit;
                        }
                    }
                    //向きの変更
                    //斜めの場合は遊びを入れる
                    //else if (Input.GetKey(KeyControlInformation.Info.ChangeDirection))
                    else if (KeyControlInformation.Info.OnKeyField(KeyType.ChangeDirection))
                    {
                        //左上
                        if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            ChangeDirectionPlayer(CharacterDirection.TopLeft);
                            ManageWait.Info.Wait(CommonConst.Wait.ChangeDirection);
                        }
                        //左下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            ChangeDirectionPlayer(CharacterDirection.BottomLeft);
                            ManageWait.Info.Wait(CommonConst.Wait.ChangeDirection);
                        }
                        //右上
                        else if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveRight())
                        {
                            ChangeDirectionPlayer(CharacterDirection.TopRight);
                            ManageWait.Info.Wait(CommonConst.Wait.ChangeDirection);
                        }
                        //右下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveRight())
                        {
                            ChangeDirectionPlayer(CharacterDirection.BottomRight);
                            ManageWait.Info.Wait(CommonConst.Wait.ChangeDirection);
                        }
                        //上
                        else if (KeyControlInformation.Info.OnMoveUp())
                        {
                            ChangeDirectionPlayer(CharacterDirection.Top);
                        }
                        //左
                        else if (KeyControlInformation.Info.OnMoveLeft())
                        {
                            ChangeDirectionPlayer(CharacterDirection.Left);
                        }
                        //下
                        else if (KeyControlInformation.Info.OnMoveDown())
                        {
                            ChangeDirectionPlayer(CharacterDirection.Bottom);
                        }
                        //右
                        else if (KeyControlInformation.Info.OnMoveRight())
                        {
                            ChangeDirectionPlayer(CharacterDirection.Right);
                        }
                        SetArrowActive(_player.Direction);
                        //switch (_player.Direction)
                        //{
                        //    case CharacterDirection.Top:
                        //        CommonFunction.SetActive(_arrowT, true);

                        //        break;
                        //    case CharacterDirection.TopLeft:
                        //        CommonFunction.SetActive(_arrowTL, true);
                        //        break;
                        //    case CharacterDirection.TopRight:
                        //        CommonFunction.SetActive(_arrowTR, true);
                        //        break;
                        //    case CharacterDirection.Left:
                        //        CommonFunction.SetActive(_arrowL, true);
                        //        break;
                        //    case CharacterDirection.Right:
                        //        CommonFunction.SetActive(_arrowR, true);
                        //        break;
                        //    case CharacterDirection.BottomLeft:
                        //        CommonFunction.SetActive(_arrowBL, true);
                        //        break;
                        //    case CharacterDirection.BottomRight:
                        //        CommonFunction.SetActive(_arrowBR, true);
                        //        break;
                        //    case CharacterDirection.Bottom:
                        //        CommonFunction.SetActive(_arrowB, true);
                        //        break;
                        //}
                    }
                    //移動
                    //Xが押されているときは斜め移動のみ
                    else if (Input.GetKey(KeyControlInformation.Info.XMove))
                    {
                        SetArrowActive(CharacterDirection.TopLeft);
                        SetArrowActive(CharacterDirection.TopRight);
                        SetArrowActive(CharacterDirection.BottomLeft);
                        SetArrowActive(CharacterDirection.BottomRight);
                        //CommonFunction.SetActive(_arrowTL, true);
                        //CommonFunction.SetActive(_arrowTR, true);
                        //CommonFunction.SetActive(_arrowBL, true);
                        //CommonFunction.SetActive(_arrowBR, true);

                        //左上
                        if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            CheckMovePlayer(CharacterDirection.TopLeft);
                        }
                        //左下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            CheckMovePlayer(CharacterDirection.BottomLeft);
                        }
                        //右上
                        else if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveRight())
                        {
                            CheckMovePlayer(CharacterDirection.TopRight);
                        }
                        //右下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveRight())
                        {
                            CheckMovePlayer(CharacterDirection.BottomRight);
                        }
                    }
                    else
                    {
                        //左上
                        if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            CheckMovePlayer(CharacterDirection.TopLeft);
                        }
                        //左下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveLeft())
                        {
                            CheckMovePlayer(CharacterDirection.BottomLeft);
                        }
                        //右上
                        else if (KeyControlInformation.Info.OnMoveUp() && KeyControlInformation.Info.OnMoveRight())
                        {
                            CheckMovePlayer(CharacterDirection.TopRight);
                        }
                        //右下
                        else if (KeyControlInformation.Info.OnMoveDown() && KeyControlInformation.Info.OnMoveRight())
                        {
                            CheckMovePlayer(CharacterDirection.BottomRight);
                        }
                        //上
                        else if (KeyControlInformation.Info.OnMoveUp())
                        {
                            CheckMovePlayer(CharacterDirection.Top);
                        }
                        //左
                        else if (KeyControlInformation.Info.OnMoveLeft())
                        {
                            CheckMovePlayer(CharacterDirection.Left);
                        }
                        //下
                        else if (KeyControlInformation.Info.OnMoveDown())
                        {
                            CheckMovePlayer(CharacterDirection.Bottom);
                        }
                        //右
                        else if (KeyControlInformation.Info.OnMoveRight())
                        {
                            CheckMovePlayer(CharacterDirection.Right);
                        }
                    }

                    break;
#endregion フィールド操作
                default:
                    break;
            }
        }
        ArrowUpdate();
#endregion 入力


#region 調合処理の実行
        if (_state == TurnState.AtelierCreateExecute)
        {
            //遊びを入れる
            ManageWait.Info.Wait(CommonConst.Wait.MenuMove);

            _menu.FinishAllMenu();
            _atelierMenu.CreateExecute(_player, _model.Kiln);
            //調合に成功していたら調合アイテムを持ち物に追加
            if(CommonFunction.IsNull(_atelierMenu.CreateItem) == false)
            {
                _player.AddItem(_atelierMenu.CreateItem, GetNowTimeTicks());
                _atelierMenu.CreateItem = null;
            }

            _atelierMenu.ObjectClear();

            //ターン消費
            _player.RestActionTurn--;
            _state = TurnState.EnemyInit;
        }
#endregion 調合処理の実行

#region アイテム処理系
        if (_state == TurnState.PlayerItemUse)
        {

            if (UseItemInformation.ItemUseType == MenuItemActionType.Throw)
            {
                _player.ThrowItem();
            }
            else if(UseItemInformation.ItemUseType == MenuItemActionType.Melody)
            {
                _player.SingItem();
            }
            else
            {
                _player.UseItem();
            }

            UseItem();
            ManageWait.Info.Wait(CommonConst.Wait.ItemUse);
        }
#endregion アイテム処理系

#region アイテム動作系
        if (_state == TurnState.ItemAction)
        {
            //すべてのオブジェクトの移動処理が終了したらステート移動処理に移る
            bool isMove = false;
            isMove = BlowItemAction(isMove);

            //移動処理が終了したらステートを移動する
            if (isMove == false)
            {
                _state = TurnState.EnemyItemEffect;
            }
        }
#endregion アイテム動作系

#region アイテム効果による敵の移動系
        if (_state == TurnState.EnemyItemEffect)
        {
            //すべてのオブジェクトの移動処理が終了したらステート移動処理に移る
            bool isMove = false;

            //キャラクタ
            foreach (BaseCharacter i in _model.Characters)
            {
                //キャラクタ
                if (i.Blow(_model) == true)
                {
                    isMove = true;
                }
            }

            //移動処理が終了したらステートを移動する
            if (isMove == false)
            {
                _state = TurnState.EnemyInit;
            }
        }
#endregion アイテム効果による敵の移動系

#region 移動先でのアイテム取得,トラップ
        if (_state == TurnState.MoveAfterCheck)
        {
            //移動先にアイテムがあった場合
            _model.SetUpItemMap();
            if (CommonFunction.IsNull(_model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y)) == false)
            {
                BaseItem item = _model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y);

                //対象が階段だった場合
                if (_model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y).Type == ObjectType.Stair)
                {
                    IsStair = true;
                }
                //ダッシュキーが押下されているとき
                else
                {
                    if (KeyControlInformation.Info.OnKeyField(KeyType.Dash))
                    //    if (Input.GetKey(KeyControlInformation.Info.Dash))
                    {
                        //ダッシュをキャンセル
                        _player.DashCancel();
                        //乗る
                        item.OnItem();

                        //遊びを入れる
                        ManageWait.Info.Wait(CommonConst.Wait.OnItem);
                    }
                    else
                    {
                        //取得できるか判定する
                        bool result = item.GetOnItem();
                        if (result == true)
                        {
                            long ticks = GetNowTimeTicks();
                            //プレイヤー管理アイテムに追加
                            _player.AddItem(item, ticks);

                            //マップオブジェクトからは削除
                            _model.RemoveItem(item);
                        }
                        else
                        {
                            //失敗は乗っている状態
                            //遊びを入れる
                            ManageWait.Info.Wait(CommonConst.Wait.OnItem);
                        }
                    }
                }
            }

            //移動先にトラップがあった場合
            _model.SetUpTrapMap();
            if (CommonFunction.IsNull(_model.TrapMap.Get(_player.CurrentPoint)) == false)
            {
                BaseTrap trap = _model.TrapMap.Get(_player.CurrentPoint);

                trap.Startup(_model, _player);

                //遊びを入れる
                ManageWait.Info.Wait(CommonConst.Wait.TrapStart);
            }
            _state = TurnState.EnemyInit;
        }
#endregion 移動先でのアイテム取得,トラップ

#region 敵の行動準備
        if (_state == TurnState.EnemyInit)
        {
            //トラップの発動判定
            _model.SetUpTrapMap();
            if (CommonFunction.IsNull(_model.TrapMap.Get(_player.CurrentPoint)) == false)
            {
                BaseTrap trap = _model.TrapMap.Get(_player.CurrentPoint);

                if (trap.CanStartup(_player) == true)
                {
                    trap.Startup(_model, _player);
                }
            }
            _state = TurnState.Enemy;
        }
#endregion 敵の行動準備

#region 敵の行動
        if (_state == TurnState.Enemy)
        {
            //List<BaseCharacter> deathList = new List<BaseCharacter>();

            //UnityEngine.UI.Text cnt = GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>();
            //cnt.text = "";
            var enumerator = _model.Characters.GetEnumerator();

            while (enumerator.MoveNext())
            {
                BaseCharacter c = enumerator.Current;
                _model.SetUpCharacterMap();
                if (c.Type == ObjectType.Enemy)
                {
                    //敵の行動
                    BaseCharacter deathchar = ((BaseEnemyCharacter)c).EnemyTurn(_model, _player);

                    //敵のトラップ発動
                    if (CommonFunction.IsNull(_model.TrapMap.Get(c.CurrentPoint)) == false)
                    {
                        BaseTrap trap = _model.TrapMap.Get(c.CurrentPoint);
                        if (trap.CanStartup(c) == true)
                        {
                            trap.Startup(_model, c);
                            //遊びを入れる
                            //_wait = CommonConst.Wait.TrapStart;
                        }
                    }
                }
            }


            foreach(BaseCharacter c in _model.AddCharacters)
            {
                _model.AddNewCharacter(c);
            }
            _model.AddCharacters.Clear();

            //死亡キャラの削除処理
            //foreach (BaseCharacter c in deathList)
            //{
            //    _model.RemoveCharacter(c);
            //}

            _state = TurnState.MovingSetup;
        }
#endregion 敵の行動

#region 移動準備

        if (_state == TurnState.MovingSetup)
        {
            //ダッシュ
            //ダッシュキーが押下されているとき または足踏み
            if (KeyControlInformation.Info.OnKeyField(KeyType.Dash)
                || KeyControlInformation.Info.OnKeyField(KeyType.Idle))
                //if (Input.GetKey(KeyControlInformation.Info.Dash)
                //|| Input.GetKey(KeyControlInformation.Info.Idle))
            {
                var enumerator = _model.Characters.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    BaseCharacter c = enumerator.Current;
                    c.Dash();
                }
                //    foreach (BaseCharacter c in _model.Characters.Values)
                //{
                //    c.Dash();
                //}
            }
            
            //マップの暗さを設定
            RoomInformation visible = _model.GetVisibility(_player.CurrentPoint);
            SpotLightMove.Instance.SetVisibleLight(visible, _model, _player);
            
            //敵の可視化を更新
            _model.SetEnemyVisibleContainsBefore(visible);

            _state = TurnState.Moving;
        }
#endregion 移動準備


#region 特殊移動

        if (_state == TurnState.SpecialMoving)
        {
            //すべてのオブジェクトの移動処理が終了したらステート移動処理に移る
            bool isMove = false;

            //キャラクタ
            var enumerator = _model.Characters.GetEnumerator();

            while (enumerator.MoveNext())
            {
                BaseCharacter c = enumerator.Current;

                //キャラクタ
                if (c.Blow(_model) == true)
                {
                    isMove = true;
                }
            }

            if (isMove == false)
            {
                //アイテム
                isMove = BlowItemAction(isMove);
            }
            
            //移動処理が終了したらステートを移動する
            if (isMove == false)
            {
                _state = TurnState.SpecialMovingTrap;
            }
        }
#endregion 特殊移動

#region 特殊移動後のトラップ発動

        if (_state == TurnState.SpecialMovingTrap)
        {
            //移動先にトラップがあった場合
            _model.SetUpTrapMap();
            if (CommonFunction.IsNull(_model.TrapMap.Get(_player.CurrentPoint)) == false)
            {
                BaseTrap trap = _model.TrapMap.Get(_player.CurrentPoint);
                trap.Startup(_model, _player);

                //遊びを入れる
                ManageWait.Info.Wait(CommonConst.Wait.TrapStart);
            }

            //移動処理が終了したらステートを移動する
            _state = TurnState.Moving;
        }
#endregion 特殊移動後のトラップ発動

#region 移動
        if (_state == TurnState.Moving)
        {
            //すべてのオブジェクトの移動処理が終了したらステート移動処理に移る
            bool isMove = false;
            bool isNextAttack = false;
            bool isSpecial = false;
            //プレイヤー
            if (_player.Move(_model, true,out isSpecial,_player.CurrentPoint) == true)
            {
                isMove = true;
            }
            if (_player.IsNextAttack == true)
            {
                isNextAttack = true;
            }

            //その他キャラ
            //if(isMove == false || _player.ActType == ActionType.Move)
            {

                var enumerator = _model.Characters.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BaseCharacter c = enumerator.Current;

                    if (c.Type != ObjectType.Player)
                    {
                        if (c.Move(_model, isNextAttack, out isSpecial, _player.CurrentPoint) == true)
                        //if (_player.IsMove == true)
                        {
                            isMove = true;
                        }
                        if (c.ActType == ActionType.Attack && c.IsNextAttack == true)
                        {
                            isNextAttack = true;
                        }
                        if (isSpecial == true)
                        {
                            _state = TurnState.SpecialMoving;
                            return;
                        }
                    }
                }
                //foreach (BaseCharacter c in _model.Characters.Values)
                //{
                //    if (c.Type != ObjectType.Player)
                //    {
                //        if (c.Move(_model, isNextAttack, out isSpecial, _player.CurrentPoint) == true)
                //        //if (_player.IsMove == true)
                //        {
                //            isMove = true;
                //        }
                //        if (c.ActType == ActionType.Attack && c.IsNextAttack == true)
                //        {
                //            isNextAttack = true;
                //        }
                //        if(isSpecial == true)
                //        {
                //            _state = TurnState.SpecialMoving;
                //            return;
                //        }
                //    }
                //}
            }

            //移動処理が終了したらステートを移動する
            if (isMove == false)
            {
                _model.DeathUpdate();

                //敵の可視化を更新
                RoomInformation visible = _model.GetVisibility(_player.CurrentPoint);
                _model.SetEnemyVisible(visible);

                //マップの影を更新
                _model.SetUpFieldShadow(_player.CurrentPoint);

                //ステータス情報の更新
                DisplayInformation.Info.SetPlayerInformation(_player);

                //プレイヤーが死んだら死亡処理に移る
                //if (_player.ActType == ActionType.Death)
                if (_player.IsDeath == true)
                {
                    _state = TurnState.PlayerDeathInit;
                    ManageWait.Info.Wait(CommonConst.Wait.Death);

                    MusicInformation.Music.IsFadeout = true;
                    return;
                }

                //プレイヤーのマップ視界を更新する
                _model.UpdateWriteTarget(_player.CurrentPoint);
                _model.IsRemap = true;

                _state = TurnState.Trap;
            }
        }
        else
        {
            bool isSpecial = false;
            _player.Move(_model,true, out isSpecial, _player.CurrentPoint);
        }
#endregion 移動

#region 罠の発動
        if (_state == TurnState.Trap)
        {

            var enumerator = _model.Traps.GetEnumerator();
            if (CommonFunction.IsNull(enumerator) == false)
            {
                while (enumerator.MoveNext())
                {
                    BaseTrap t = enumerator.Current;

                    //トラップの発動が成功した場合
                    if (t.Invocate(_model) == true)
                    {
                        //上階に強制移動の場合
                        if (t.TType == TrapType.Cyclone)
                        {
                            ManageWait.Info.Wait(CommonConst.Wait.Cyclone);

                            _state = TurnState.NextFloor;
                            return;
                        }
                    }
                    BaseTrap[] newt = t.TurnAction(_model);
                    if (CommonFunction.IsNull(newt) == false)
                    {
                        NewTraps.AddRange(newt);
                    }
                }
            }


            for(int i = 0; i < NewTraps.Count; i++)
            {
                _model.AddNewTrap(NewTraps[i]);
            }
            NewTraps.Clear();

            _model.DeathUpdate();


            if (isRTS == true)
            {
                mwRTS.Wait();
            }

            //残処理のキャラがいればプレイヤーに
            _state = TurnState.Player;

            //すべてのオブジェクトのターン処理が終了したらターン更新をする
            bool isWaitFinish = false;

            var enumerator2 = _model.Characters.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                BaseCharacter c = enumerator2.Current;
                if (c.RestActionTurn >= 1)
                {
                    isWaitFinish = true;
                }
            }
            //foreach (BaseCharacter c in _model.Characters.Values)
            //{
            //    if (c.RestActionTurn >= 1)
            //    {
            //        isWaitFinish = true;
            //    }
            //}

            //ステータス情報の更新
            DisplayInformation.Info.SetPlayerInformation(_player);


            //処理が終了したらステートを移動する
            if (isWaitFinish == false)
            {
                _state = TurnState.TurnFinish;
            }

            //暗闇の時
            //if ((_player.CharacterAbnormalState & (int)StateAbnormal.Dark) != 0)
            //{
            //    //世界を消す
            //    _model.OnDark();
            //    ResourceInformation.MapPanel.SetActive(false);
            //}

        }
#endregion 罠の発動

#region ターン初期化
        if (_state == TurnState.TurnFinish)
        {
            //アイテムマップを最新化
            _model.SetUpItemMap();

            UseItemInformation.IsStair = false;
            //足元のアイテムを一時に追加
            if (CommonFunction.IsNull(_model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y)) == false)
            {
                if (_model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y).Type == ObjectType.Stair)
                {
                    UseItemInformation.IsStair = true;
                }
                else
                {
                    UseItemInformation.FootItem = _model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y);
                }
            }
            else
            {
                UseItemInformation.FootItem = null;
            }


            //各オブジェクトのターン初期化
            var enumerator = _model.Characters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                BaseCharacter c = enumerator.Current;
                c.FinishTurn(_model);

            }

            
            _model.DeathUpdate();

            //暗闇の時
            if ((_player.CharacterAbnormalState & (int)StateAbnormal.Dark) != 0)
            {
                //世界を消す
                _model.OnDark();
                CommonFunction.SetActive(ResourceInformation.MapPanel, false);
                //ResourceInformation.MapPanel.SetActive(false);
            }
            //暗闇ではないとき
            else if ((_player.CharacterAbnormalState & (int)StateAbnormal.Dark) == 0)
            {
                //世界をつける
                _model.OffDark();
                CommonFunction.SetActive(ResourceInformation.MapPanel, true);
                //ResourceInformation.MapPanel.SetActive(true);
            }

            if (_model.Characters.Count < 6)
            {
                //敵が一定以下になったら「一定確率でリスポンする
                if(CommonFunction.IsRandom(0.01f) == true)
                {

                    _model.SetUpCharacterMap();
                    _model.SetUpItemMap();
                    int enemytype = TableEnemyMap.GetValue(_model.DungeonObjNo, DisplayInformation.Info.Floor);
                    uint rand = CommonFunction.GetRandomUInt32();
                    BaseEnemyCharacter enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, nowFloor);

                    int stopper = 0;
                    bool addenemy = true;
                    while(CommonFunction.IsNull(enemy) == true
                        || enemy.ObjNo == GameStateInformation.Info.EmeryTarget)
                    {
                        if(stopper > 30)
                        {
                            addenemy = false;
                            break;
                        }
                        enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, nowFloor);
                    }

                    if (addenemy == true)
                    {
                        //ランダムで配置場所を取得
                        int x;
                        int y;
                        _model.GetEnemyPosition(_player.CurrentPoint, out x, out y);

                        enemy.SetCharacterDisplayObject(x, y);
                        _model.AddNewCharacter(enemy);
                    }
                }
            }
            
            //ステータス情報の更新
            DisplayInformation.Info.SetPlayerInformation(_player);

            //プレイヤーが死んだら死亡処理に移る
            //if (_player.ActType == ActionType.Death)
            if(_player.IsDeath == true)
            {
                _state = TurnState.PlayerDeathInit;
                ManageWait.Info.Wait(CommonConst.Wait.Death);

                MusicInformation.Music.IsFadeout = true;
                return;
            }

            _turn++;

#if UNITY_EDITOR
            //GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
            //    GameObject.Find("MapobjectPoint").transform.childCount.ToString();

#endif
            //cnt.text = _turn.ToString();

            _state = TurnState.Player;
        }

#endregion ターン初期化

#region ゲームオーバー
        if(_state == TurnState.PlayerDeathInit)
        {
            TransitionScoreScene();
            _manageFade.Play(FadeState.FadeIn, true);
            _state = TurnState.PlayerDeath;
        }
#endregion ゲームオーバー

#region セーブ
        if(_state == TurnState.SaveInit)
        {

            _manageFade.Play(FadeState.FadeIn, true);

            //セーブデータの作成
            SavePlayingInformation save = new SavePlayingInformation();
            save.SetPlayer(_player);
            save.SetDungeon(DungeonInformation.Info, (ushort)(nowFloor + 1));
            save.SetAnalyse();

            save.pt = (DateTime.Now - GameStartTimestamp).Ticks + PreviouslyTimestamp;

            string saveplay = save.GetJson();

            //アイテムデータの作成
            string saveitem = SaveItemInformation.GetJson(PlayerCharacter.ItemList);

            //保存
            SaveDataInformation.SavePlayingValue(saveplay);
            SaveDataInformation.SaveItemValue(saveitem);


            TransitionTitleScene();

            _state = TurnState.PlayerDeath;
        }
#endregion セーブ

        //マップ描画
        _model.UpdateMap(_player);

#if UNITY_EDITOR
        //GameObject.Find("TurnCount").GetComponent<UnityEngine.UI.Text>().text =
        //    GameObject.Find("MapobjectPoint").transform.childCount.ToString();


#endif
    }

    Dictionary<Guid, MapPoint> addItemMap = new Dictionary<Guid, MapPoint>();
    private bool BlowItemAction(bool isMove)
    {

        //すべてのオブジェクトの移動処理が終了したらステート移動処理に移る
        //消去対象キャラクタ
        //消去対象アイテム
        //destList.Clear();
        //追加対象アイテムとその位置
        //addList.Clear();
        addItemMap.Clear();

        //アイテム
        var enumerator = _model.Items.GetEnumerator();
        while (enumerator.MoveNext())
        {
            BaseItem i = enumerator.Current;

            if (i.Move(_player) == true)
            {
                isMove = true;
            }
            //移動が終わったら
            else
            {
                //投擲終了処理を行うかどうか
                if (i.IsThrowAfterAction == true)
                {
                    if(CommonFunction.IsNull(i.ThrowAfterActionTarget) == false)
                    {
                        //誰かいた場合はそのキャラにアイテム投擲判定を与える
                        if (i.ThrowAction(_model, _player, i.ThrowAfterActionTarget) == true)
                        {
                            //投擲判定が成功した場合
                            //マップから削除する
                            destList.Add(i);
                        }

                    }
                    else
                    {
                        //誰もいなかったら投擲終了処理
                        if (i.ThrowDrop(_model, _player))
                        {
                            //投擲判定が成功した場合
                            //マップから削除する
                            destList.Add(i);
                        }
                    }
                    isMove = true;
                    i.ThrowAfterActionTarget = null;
                    i.IsThrowAfterAction = false;
                    //投擲後高さが変わるので、位置を再設定
                    i.ResetObjectPosition();
                    //投擲後の視認性を再設定
                    i.SetObjectActive(_model.GetVisibility(_player.CurrentPoint), _model.IsVisible, false);
                }

                //移動が終了し、ブレイクが立っていれば消す
                if (i.IsThrowBreak == true)
                {
                    //ドライブの場合はその場にアイテムを散らす
                    if (i.IType == ItemType.Bag)
                    {
                        List<BaseItem> list = new List<BaseItem>();
                        foreach (BaseItem di in ((BagBase)i).BagItems)
                        {
                            addList.Add(di);
                            addItemMap.Add(di.Name, i.CurrentPoint);
                            list.Add(di);
                        }
                        //中のアイテムを全部削除
                        foreach (BaseItem di in list)
                        {
                            ((BagBase)i).PutoutItem(di);
                        }
                    }
                    destList.Add(i);
                }
            }

        }


        //消去対象を一気に消去
        foreach (BaseItem i in destList)
        {
            _model.RemoveItem(i);
            i.Dispose();
        }
        destList.Clear();
        //追加対象を一気に追加
        for (int i = 0; i < addList.Count; i++)
        {
            _model.BreakoutItem(addList[i], addItemMap[addList[i].Name], addItemMap[addList[i].Name]);
            //位置を補正
            addList[i].Move(_player);
            //水の上だった場合
            if (_model.Dungeon.DungeonMap.Get(addList[i].CurrentPoint).State == LoadStatus.Water)
            {
                WaterInItem.Add(addList[i]);
            }
            addList[i] = null;
        }
        addList.Clear();

        //水の上に落ちたアイテムを全削除
        for (int i = 0; i < WaterInItem.Count; i++)
        {
            _model.RemoveItem(WaterInItem[i]);
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.WaterInItem, WaterInItem[i].DisplayNameInMessage));
            WaterInItem[i].Dispose();
        }
        WaterInItem.Clear();

        _model.DeathUpdate();
        //消去対象を一気に消去
        //foreach (BaseCharacter i in killList)
        //{
        //    _model.RemoveCharacter(i);
        //}

        return isMove;
    }

    private void TransitionScoreScene()
    {
        ScoreInformation.Info.TotalTime = (DateTime.Now - GameStartTimestamp).Ticks + PreviouslyTimestamp;

        ScoreInformation.Info.PlayerName = _player.DisplayNameNormal;
        ScoreInformation.Info.PlayerNameBase = _player.DisplayName;
        StartCoroutine(TransScene("Scenes/Gameover", 1));

    }

    private void TransitionTitleScene()
    {
        StartCoroutine(TransScene("Scenes/Title", 1));

    }

    private void ChangeDirectionPlayer(CharacterDirection dir)
    {
        _player.ChangeDirection(dir);
    }
    private void CheckMovePlayer(CharacterDirection dir)
    {

        //混乱していた場合
        if ((_player.CharacterAbnormalState & (int)StateAbnormal.Confusion) != 0)
        {
            //方向を適当に変更
            dir = CommonFunction.ReverseDirection.Keys.ElementAt(UnityEngine.Random.Range(0, 8));
        }

        if (_player.CheckMove(_model, dir) == true)
        {
            _state = TurnState.MoveAfterCheck;
        }
    }

    private void UseItem()
    {
        BaseItem item;
        bool result;

        //選択した項目によって分岐
        switch (UseItemInformation.ItemUseType)
        {
            case MenuItemActionType.Equip:
                //装備
                item = GetItem();

                //装備済みのものがあったら外す
                BaseItem eqitem = PlayerCharacter.GetEquip(item.IType);

                if (CommonFunction.IsNull(eqitem) == false)
                {
                    //外すに失敗したら処理終了
                    if (eqitem.RemoveEquip(_player) == false)
                    {
                        break;
                    }
                }

                bool isEquip = false;

                //対象が持ち物の中だったら
                if (PlayerCharacter.HasItemPlayer(item))
                {
                    //ドライブに入っていなかったら
                    if (item.IsDrive == false)
                    {
                        isEquip = true;
                    }
                    else
                    {
                        //ドライブに入っていて、入手可能だったら
                        if (PlayerCharacter.IsGetItem(item) == true)
                        {
                            //ドライブから取り出す
                            item.InDrive.PutoutItem(item);

                            isEquip = true;
                        }
                    }
                }
                else
                {
                    //対象が足元だったら

                    //アイテムを拾えるか判定する
                    if (PlayerCharacter.IsGetItem(item) == true)
                    {
                        //取得可能なら取得して装備する
                        long ticks = GetNowTimeTicks();

                        //プレイヤー管理アイテムに追加
                        _player.AddItem(item, ticks);

                        //ドライブに入っていたら
                        if (item.IsDrive == true)
                        {
                            //ドライブから取り出す
                            item.InDrive.PutoutItem(item);

                        }
                        else
                        {
                            //マップオブジェクトから削除
                            _model.RemoveItem(item);
                        }

                        isEquip = true;
                    }
                }
                if(isEquip == true)
                {
                    //装備する
                    item.Equip(_player);
                }
                    else
                    {
                        item.FailGetItem();
                    }

                break;
            case MenuItemActionType.Put:
                //置く
                //対象を取得
                item = UseItemInformation.UseItemMain;

                //装備中だったら外す
                if(item.IsEquip == true)
                {
                    //外すに失敗したら処理終了
                    if (item.RemoveEquip(_player) == false)
                    {
                        break;
                    }
                }

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Put);

                //プレイヤーから削除
                PlayerCharacter.RemoveItem(UseItemInformation.UseItemMain);

                //別のドライブに入っていたら取り出す
                if (UseItemInformation.UseItemMain.IsDrive == true)
                {
                    UseItemInformation.UseItemMain.InDrive.PutoutItem(UseItemInformation.UseItemMain);
                }

                item.SetInstanceName();

                //対象の位置に置く
                _model.PutItem(item, _player.CurrentPoint, _player.CurrentPoint);

                break;
            case MenuItemActionType.Putin:
                //選択中だったアイテムを格納する
                //対象取得
                BagBase drive = UseItemInformation.ParentDrive;
                bool driveHasPlayer = PlayerCharacter.HasItemPlayer(drive);

                item = GetItem();

                //装備中だったら解除する
                if (item.IsEquip == true)
                {
                    //外すに失敗したら処理終了
                    if (item.RemoveEquip(_player,false) == false)
                    {
                        break;
                    }
                }

                //対象のアイテムを管理から削除
                bool itemHasPlayer = PlayerCharacter.HasItemPlayer(item);
                if (itemHasPlayer == true)
                {
                    //プレイヤーから削除
                    PlayerCharacter.RemoveItem(item);
                }
                else
                {
                    //マップオブジェクトから削除
                    _model.RemoveItem(item);
                }

                //別のドライブに入っていたら取り出す
                if(item.IsDrive == true)
                {
                    item.InDrive.PutoutItem(item);
                }

                //アイテムを格納して変動結果を取得
                int itemtype = TableItemMap.GetValue(_model.DungeonObjNo, DisplayInformation.Info.Floor);
                BaseItem inItem = drive.PutinItem(itemtype, item, _player);

                if(driveHasPlayer == true)
                {
                    if (CommonFunction.IsNull(inItem) == false)
                    {
                        long ticks = GetNowTimeTicks();

                        //プレイヤー管理アイテムに追加
                        _player.AddItem(inItem, ticks);
                    }
                }
                else
                {
                    //ドライブに追加するのでマップオブジェクトへの追加はなし
                }


                break;
            case MenuItemActionType.RemoveEquip:
                //外す

                //対象を取得
                item = GetItem();

                //対象を外す
                item.RemoveEquip(_player);
                break;
            case MenuItemActionType.Throw:
                //投げる
                //対象を取得
                item = GetThrowItem();

                //装備済みだったら外す
                if (item.IsEquip == true)
                {
                    //外すに失敗したら処理終了
                    if (item.RemoveEquip(_player,false) == false)
                    {
                        break;
                    }
                }

                //混乱していた場合
                if ((_player.CharacterAbnormalState & (int)StateAbnormal.Confusion) != 0)
                {
                    //方向を適当に変更
                    CharacterDirection dir = CommonFunction.ReverseDirection.Keys.ElementAt(UnityEngine.Random.Range(0, 8));
                    _player.ChangeDirection(dir);
                }

                //敵のマッピングを最新化
                _model.SetUpCharacterMap();

                //投げた先のアイテムが落ちるポイントを取得
                bool isVanish;
                MapPoint point = _model.GetHitItemThrow(_player.CurrentPoint, _player.Direction, _player.ThrowRange,
                    _player.EquipRing.CheckTunnel(), out isVanish);
                
                item.SetThrowInstanceName();

                item.ThrowAfterActionTarget = null;
                //投擲対象に誰かいた場合
                BaseCharacter tar = _model.CharacterMap.Get(point);
                if (CommonFunction.IsNull(tar) == false
                    && tar.Type != ObjectType.Player)
                {
                    item.ThrowAfterActionTarget = tar;

                    if (item.ThrowStartSpecial(_player, tar) == true)
                    {
                        break;
                    }
                }

                //対象のアイテムが消えれば
                if (isVanish == true)
                {
                    item.IsThrowVanish = true;
                }
                //対象ポイントにアイテムを置く
                _model.ThrowItem(item, point, _player.CurrentPoint, _player.Direction);

                //投げる処理開始
                result = item.ThrowStart(_player);

                //使用に成功したら対象をアイテムから削除
                if (result == true)
                {
                    if (PlayerCharacter.HasItemPlayer(UseItemInformation.UseItemMain) == true)
                    {
                        PlayerCharacter.RemoveItem(UseItemInformation.UseItemMain);
                    }
                    
                    //別のドライブに入っていたら取り出す
                    if (UseItemInformation.UseItemMain.IsDrive == true)
                    {
                        UseItemInformation.UseItemMain.InDrive.PutoutItem(item);
                    }
                    //else
                    //{
                    //    _model.RemoveItem(UseItemInformation.UseItemMain);
                    //}
                }
                break;

            case MenuItemActionType.Melody:
                //奏でる
                result = GetItem().Melody(_model,_player);

                //使用に成功したら対象をアイテムから削除
                if (result == true)
                {
                    RemoveUseItem();
                }
                break;
            case MenuItemActionType.Use:
                //使う
                result = GetItem().Use(_model, _player);

                //使用に成功したら対象をアイテムから削除
                if (result == true)
                {
                    RemoveUseItem();
                }
                break;

            case MenuItemActionType.Eat:
                //食べる
                result = GetItem().Eat(_model, _player);

                //使用に成功したら対象をアイテムから削除
                if (result == true)
                {
                    RemoveUseItem();
                }


                break;
            case MenuItemActionType.Get:
                //拾う

                item = UseItemInformation.UseItemMain;
                //アイテムを拾えるか判定する
                if (PlayerCharacter.IsGetItem(item) == true)
                {
                    //取得可能なら取得
                    long ticks = GetNowTimeTicks();

                    //ドライブに入っていたら取り出す
                    if(item.IsDrive == true)
                    {
                        item.InDrive.PutoutItem(item);
                    }

                    //プレイヤー管理アイテムに追加
                    _player.AddItem(item, ticks);
                    //マップオブジェクトから削除
                    _model.RemoveItem(item);
                    item.GetItem();
                }
                else
                {
                    item.FailGetItem();
                }

                break;
        }

        _state = TurnState.ItemAction;
    }
    private void RemoveUseItem()
    {
        //プレイヤーが持っていたらプレイヤーから削除
        if (PlayerCharacter.HasItemPlayer(UseItemInformation.UseItemMain))
        {
            PlayerCharacter.RemoveItem(UseItemInformation.UseItemMain);
        }
        //持っていなければダンジョンから排除
        else
        {
            _model.RemoveItem(UseItemInformation.UseItemMain);
        }

        //別のドライブに入っていたら取り出す
        if (UseItemInformation.UseItemMain.IsDrive == true)
        {
            UseItemInformation.UseItemMain.InDrive.PutoutItem(UseItemInformation.UseItemMain);
        }

        UseItemInformation.UseItemMain.Dispose();
        UseItemInformation.UseItemMain = null;

    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
        //だんだん暗く
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.smoothDeltaTime;
            yield return 0;
        }

        //シーン切替
        SceneManager.LoadScene(scene);
        
    }


    private void SetKeyDisplay()
    {
        _keyDisp.SetActive(true);
        _keyDisp.transform.Find("KeyDisplayText").GetComponent<Text>().text = KeyControlInformation.Info.GetKeyValueText();
    }

    private void SetMessageLog()
    {
        //foreach(Transform t in _messageLogTextContent.transform)
        //{
        //    Destroy(t.gameObject);
        //}
        //foreach(string s in DisplayInformation.Info.DisplayMessagesHistory)
        //{
        //    GameObject gm = GameObject.Instantiate(_messageLogText, _messageLogTextContent.transform);
        //    gm.GetComponent<Text>().text = s;
        //}
        for(int i = 0; i < DisplayInformation.Info.DisplayMessagesHistory.Count; i++)
        {
            GameObject gm = GetMessageObject(i);
            gm.GetComponent<Text>().text = DisplayInformation.Info.DisplayMessagesHistory[i];
        }
        //スクロールバーを最下層に設定
        ScrollRect scrollRect = _messageLogTextScrollView.GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 0;

        //マウスの時だけボタンを出す
        if(KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
        {
            _messageLogCloseButton.SetActive(true);
        }
        else
        {
            _messageLogCloseButton.SetActive(false);
        }

        //_messageLog.transform.FindChild("Panel/MessageLogText").GetComponent<Text>().text
        //    = DisplayInformation.Info.GetAllMessage();
    }

    private GameObject GetMessageObject(int i)
    {
        if(Messages.Count > i)
        {
            return Messages[i];
        }
        GameObject gm = GameObject.Instantiate(_messageLogText, _messageLogTextContent.transform);
        Messages.Add(gm);
        return gm;
    }

    private void SetMessageLogScroll()
    {
        //以下スクロールの設定
        float height = DisplayInformation.Info.DisplayMessagesHistory.Count * 25;
        float contentHeight = _messageLogTextContent.GetComponent<RectTransform>().sizeDelta.y;
        // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
        if (contentHeight <= height)
        {
            return;
        }

        int n = DisplayInformation.Info.DisplayMessagesHistory.Count;

        //現在の高さ総計
        float loc = n * 30f;
        float unit = 8f / loc;
        if (KeyControlInformation.Info.OnMoveUp())
        {
            unit = -unit;
        }
        else if (KeyControlInformation.Info.OnMoveDown())
        {
        }
        else
        {
            return;
        }

        _messageLogTextScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = Mathf.Clamp(
            _messageLogTextScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition - unit, 0, 1);
    }

    private void SetupFloor(ushort floor)
    {
        //シードの更新
        int seed = CommonFunction.GetSeed();
        GameStateInformation.Info.Seed = seed;
        CommonFunction.SetSeed(seed);

        //ステータス情報の更新
        DisplayInformation.Info.SetFloorInformation(floor);
        DisplayInformation.Info.SetPlayerInformation(_player);

        //スコアの更新
        ScoreInformation.Info.AddScore(100 * floor);
        ScoreInformation.Info.Floor = floor;

        //ゲームステートの初期化
        GameStateInformation.Info.FloorStateInitialize();

        //履歴の登録
        DungeonHistoryInformation.Info.iDungeonId = (int)_model.DungeonObjNo;
        DungeonHistoryInformation.Info.iFloor = floor;
        DungeonHistoryInformation.Info.iWeaponDamage = _player.EquipWeapon.TotalDamage;
        DungeonHistoryInformation.Info.iCurrentLevel = _player.Level;
        DungeonHistoryInformation.Info.iTurn = _turn;
        DungeonHistoryInformation.Info.iCurrentHp = Mathf.CeilToInt(_player.CurrentHp);
        DungeonHistoryInformation.Info.vcCharacterName = _player.DisplayNameNormal;
        MainThreadDispatcher.StartUpdateMicroCoroutine(DungeonHistoryInformation.Info.SendDungeonHistoryCorutine(DungeonHistoryInformation.Info.GetJson()));

        bool isBoss = false;
        if(DungeonInformation.Info.DisruptFloor == floor)
        {
            if (string.IsNullOrEmpty(DungeonInformation.Info.BossObjNo) == false)
            {
                isBoss = true;
            }
        }
        _model.Create(isBoss);

        _state = TurnState.TurnFinish;
        int x = _model.GetRandomX();
        int y = _model.GetRandomY();

        while (_model.Dungeon.DungeonMap.Get(x, y).State != LoadStatus.Room)
        {
            x = _model.GetRandomX();
            y = _model.GetRandomY();
        }
#if UNITY_EDITOR
        //bool notnearwall = false;

        //while (notnearwall == false
        //    && (_model.Dungeon.DungeonMap.Get(x+1, y).State != LoadStatus.Room
        //    || _model.Dungeon.DungeonMap.Get(x-1, y).State != LoadStatus.Room
        //    || _model.Dungeon.DungeonMap.Get(x, y+1).State != LoadStatus.Room
        //    || _model.Dungeon.DungeonMap.Get(x, y-1).State != LoadStatus.Room))
        //{
        //    x = _model.GetRandomX();
        //    y = _model.GetRandomY();
        //}
#endif

        //プレイヤーのセットアップ
        _player.SetPosition(x, y);
        _player.RestActionTurn = 0;
        _player.ChangeDirection(CharacterDirection.Bottom);
        _model.AddNewCharacter(_player);
        _turn = 1;
        //////////////////////////////////////
#if UNITY_EDITOR

        if (false)
        {
            while (_model.Dungeon.DungeonMap.Get(x + 1, y).State != LoadStatus.Load
                || _model.Dungeon.DungeonMap.Get(x - 1, y).State != LoadStatus.Load)
            {
                x = _model.GetRandomX();
                y = _model.GetRandomY();
            }
            MapPoint mp = MapPoint.Get(x, y);
            BaseEnemyCharacter tenemy = TableEnemy.GetEnemy(10007, 1, 1, 1, 1, 1, 1, 1);
            tenemy.BaseAttack = 0;
            tenemy.CurrentHp = 200;
            EnemyTester(tenemy);
            //EnemyTester(tenemy, mp);
            //tenemy = TableEnemy.GetEnemy(10007, 1, 1, 1, 1, 1, 1, 1);
            //tenemy.BaseAttack = 0;
            //EnemyTester(tenemy);
            ////EnemyTester(tenemy, mp);
            ////EnemyTester(6, mp, 10004);

            //tenemy = TableEnemy.GetEnemy(10002, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10023, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10007, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10008, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10009, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10010, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10011, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10017, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10018, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10019, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10020, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
            //tenemy = TableEnemy.GetEnemy(10021, 1, 1, 1, 1, 1, 1, 1);
            //EnemyTester(tenemy);
        }
#endif
        //////////////////////////////////////
        //マップ描画
        _model.WriteMap();

        ////////////////////任意の位置に階段を配置
        if(isBoss == false)
        {
            x = _model.GetRandomX();
            y = _model.GetRandomY();
            while (_model.IsRoom(x, y) == false)
            {
                x = _model.GetRandomX();
                y = _model.GetRandomY();
            }
            ResourceInformation.Stair.transform.localPosition = new Vector3(x * 1,
                2.9f,
                y * 1);
            _model.SetStair(ResourceInformation.Stair, x, y);
        }
        //ボス部屋だった場合
        else
        {

            DungeonInformation.Info.MType = MusicInformation.MusicType.Boss;

            MusicInformation.Music.Setup(MusicInformation.MusicType.Boss);

            List<BaseEnemyCharacter> stairCand = new List<BaseEnemyCharacter>();

            string[] stArrayData = DungeonInformation.Info.BossObjNo.Split(',');
            foreach (string tarval in stArrayData)
            {
                string[] vals = tarval.Split(':');
                int count = int.Parse(vals[0]);
                long objno = long.Parse(vals[1]);

                for (int i = 0; i < count; i++)
                {
                    BaseEnemyCharacter boss = TableEnemy.GetEnemy(objno,
                        floor,
                        DungeonInformation.Info.EnemyHpProb,
                        DungeonInformation.Info.EnemyAtkProb,
                        DungeonInformation.Info.EnemyExpProb,
                        DungeonInformation.Info.StartProbHp,
                        DungeonInformation.Info.StartProbAtk,
                        DungeonInformation.Info.StartProbExp);

                    _model.SetUpCharacterMap();
                    _model.GetEnemyPosition(_player.CurrentPoint, out x, out y);

                    boss.SetCharacterDisplayObject(x, y);
                    _model.AddNewCharacter(boss);

                    stairCand.Add(boss);
                }
            }


            //ダミー位置に階段を作成
            _model.CreateStair(ResourceInformation.Stair, 999, 999);
            ResourceInformation.Stair.transform.localPosition = CommonConst.SystemValue.VecterDammy;

            //出現位置を設定
            _model.GetEnemyPosition(1, out x, out y);

            //階段を持たせる
            stairCand[UnityEngine.Random.Range(0, stairCand.Count)].TempEnemyUseItem = _model.stair;

            stairCand.Clear();
            stairCand = null;

        }

        RingBase ring = _player.EquipRing;

        //床落ちアイテムを作成する
        //int itemcount = CommonFunction.ConvergenceRandom(5, 0.9f, 1.1f, 30);
        float con = DungeonInformation.Info.ItemC;
        con = ring.GetItemConvergence(con);
        //5-30個
        int itemcount = CommonFunction.ConvergenceRandom(
            DungeonInformation.Info.ItemS,
            DungeonInformation.Info.ItemP, 
            con,
            DungeonInformation.Info.ItemM);

        int itemtype = TableItemMap.GetValue(_model.DungeonObjNo, floor);
        //対象の個数作成
        for (int i = 0; i <= itemcount ;i++)
        {
            _model.SetUpItemMap();
            //uintの乱数を取得
            uint rand = CommonFunction.GetRandomUInt32();
            //対象のアイテムを取得
            BaseItem item = TableItemIncidence.GetItem(itemtype, rand, true);
            for (int j = 0; j <= 10; j++)
            {
                if (CommonFunction.IsNull(item) == false)
                {
                    item.IsAnalyse = DungeonInformation.Info.IsAnalyze == false;
                    break;
                }
                rand = CommonFunction.GetRandomUInt32();
                item = TableItemIncidence.GetItem(itemtype, rand, true);
            }
            if (CommonFunction.IsNull(item) == true)
            {
                continue;
            }
            //ランダムで配置場所を取得
            x = _model.GetRandomX();
            y = _model.GetRandomY();
            while (_model.IsRoom(x, y) == false
                || _player.CurrentPoint.Equal(x,y) == true
                || CommonFunction.IsNull(_model.ItemMap.Get(x,y)) == false)
            {
                x = _model.GetRandomX();
                y = _model.GetRandomY();
            }

            //マップにアイテムを追加
            _model.AddNewItem(item);
            item.SetThisDisplayObject(x, y);
        }

        //敵を作成する
        con = DungeonInformation.Info.EnemyC;
        con = ring.GetEnemyConvergence(con);
        //5-20個
        int enemycount = CommonFunction.ConvergenceRandom(
            DungeonInformation.Info.EnemyS,
            DungeonInformation.Info.EnemyP,
            con,
            DungeonInformation.Info.EnemyM);

        int enemytype = TableEnemyMap.GetValue(_model.DungeonObjNo, floor);

        for (int i = 0; i <= enemycount; i++)
        {
            _model.SetUpCharacterMap();
            //uintの乱数を取得
            uint rand = CommonFunction.GetRandomUInt32();
            BaseEnemyCharacter enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, nowFloor);
            for (int j = 0; j <= 10; j++)
            {
                if (CommonFunction.IsNull(enemy) == false)
                {
                    break;
                }
                rand = CommonFunction.GetRandomUInt32();
                enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, nowFloor);
            }
            if (CommonFunction.IsNull(enemy) == true)
            {
                continue;
            }

            //ランダムで配置場所を取得
            _model.GetEnemyPosition(enemycount,out x,out y);

            enemy.SetCharacterDisplayObject(x, y);
            _model.AddNewCharacter(enemy);
        }

        //罠を作成する
        con = DungeonInformation.Info.TrapC;
        con = ring.GetTrapConvergence(con);
        int trapcount = CommonFunction.ConvergenceRandom(DungeonInformation.Info.TrapS,
            DungeonInformation.Info.TrapP,
            con,
            DungeonInformation.Info.TrapM);

        int traptype = TableTrapMap.GetValue(_model.DungeonObjNo, floor);

        _model.SetUpItemMap();
        _model.SetUpCharacterMap();
        for (int i = 0; i <= trapcount; i++)
        {
            _model.SetUpTrapMap();
            //uintの乱数を取得
            uint rand = CommonFunction.GetRandomUInt32();
            BaseTrap trap = TableTrapIncidence.GetTrap(traptype, rand);
            for (int j = 0; j <= 10; j++)
            {
                if(CommonFunction.IsNull(trap) == false)
                {
                    break;
                }
                rand = CommonFunction.GetRandomUInt32();
                trap = TableTrapIncidence.GetTrap(traptype, rand);
            }
            if (CommonFunction.IsNull(trap) == true)
            {
                continue;
            }
            //ランダムで配置場所を取得
            x = _model.GetRandomX();
            y = _model.GetRandomY();
            while (_model.IsRoom(x, y) == false
                || CommonFunction.IsNull(_model.CharacterMap.Get(x, y)) == false
                || CommonFunction.IsNull(_model.ItemMap.Get(x, y)) == false
                || CommonFunction.IsNull(_model.TrapMap.Get(x, y)) == false)
            {
                x = _model.GetRandomX();
                y = _model.GetRandomY();
            }
            trap.SetThisDisplayTrap(x, y);
            _model.AddNewTrap(trap);
        }


        //任意の位置に窯を配置
        _model.Kiln.Initialize();
        if (CommonFunction.IsRandom(DungeonInformation.Info.KilnProb) == true)
        //if(floor % 2 == 0)
        {
            _model.SetUpTrapMap();
            x = _model.GetRandomX();
            y = _model.GetRandomY();
            while (_model.IsRoom(x, y) == false
                || CommonFunction.IsNull(_model.CharacterMap.Get(x, y)) == false
                || CommonFunction.IsNull(_model.ItemMap.Get(x, y)) == false
                || CommonFunction.IsNull(_model.TrapMap.Get(x, y)) == false)
            {
                x = _model.GetRandomX();
                y = _model.GetRandomY();
            }
            _model.Kiln.SetPosition(x, y);
            _model.AddKiln(_model.Kiln);
        }
        else
        {
            _model.Kiln.IsActive = false;
            if(CommonFunction.IsNull(_model.Kiln.MapUnit) == false)
            {
                _model.Kiln.MapUnit.OffActive();
            }
        }

        //プレイヤー配置の部屋マップを明るくする
        _model.UpdateWriteTarget(_player.CurrentPoint);

        _model.IsRemap = true;
        _model.UpdateMap(_player);
        IsMessagelog = false;

        //時間によるタイムボーナス
        if (floor > 1)
        {
            if ((DateTime.Now - FloorTimestamp).TotalSeconds < 60)
            {
                ScoreInformation.Info.AddScore(floor * 180);
            }
            else if ((DateTime.Now - FloorTimestamp).TotalSeconds < 120)
            {
                ScoreInformation.Info.AddScore(floor * 120);
            }
            else if ((DateTime.Now - FloorTimestamp).TotalSeconds < 300)
            {
                ScoreInformation.Info.AddScore(floor * 20);
            }
        }
        FloorTimestamp = DateTime.Now;
        
        //マップの暗さを設定
        RoomInformation visible = _model.GetVisibility(_player.CurrentPoint);
        SpotLightMove.Instance.SetInitial(DungeonInformation.Info.IsBadVisible);
        SpotLightMove.Instance.SetVisibleLight(visible, _model, _player);

        //敵の可視化を更新
        _model.SetEnemyVisible(visible);

        //マップの影を更新
        _model.SetUpFieldShadow(_player.CurrentPoint);

        Resources.UnloadUnusedAssets();
        GC.Collect();
        
    }

    private void SetArrowActive(CharacterDirection d)
    {
        ArrowState[d] = true;
    }

    private void ArrowUpdate()
    {
        CommonFunction.SetActive(_arrowT, ArrowState[CharacterDirection.Top]);
        CommonFunction.SetActive(_arrowTL, ArrowState[CharacterDirection.TopLeft]);
        CommonFunction.SetActive(_arrowL, ArrowState[CharacterDirection.Left]);
        CommonFunction.SetActive(_arrowBL, ArrowState[CharacterDirection.BottomLeft]);
        CommonFunction.SetActive(_arrowB, ArrowState[CharacterDirection.Bottom]);
        CommonFunction.SetActive(_arrowBR, ArrowState[CharacterDirection.BottomRight]);
        CommonFunction.SetActive(_arrowR, ArrowState[CharacterDirection.Right]);
        CommonFunction.SetActive(_arrowTR, ArrowState[CharacterDirection.TopRight]);
    }

    private void ArrowFalse()
    {
        ArrowState[CharacterDirection.Top] = false;
        ArrowState[CharacterDirection.TopLeft] = false;
        ArrowState[CharacterDirection.Left] = false;
        ArrowState[CharacterDirection.BottomLeft] = false;
        ArrowState[CharacterDirection.Bottom] = false;
        ArrowState[CharacterDirection.BottomRight] = false;
        ArrowState[CharacterDirection.Right] = false;
        ArrowState[CharacterDirection.TopRight] = false;

        //CheckFalse(_arrowT);
        //CheckFalse(_arrowTR);
        //CheckFalse(_arrowTL);
        //CheckFalse(_arrowL);
        //CheckFalse(_arrowR);
        //CheckFalse(_arrowBR);
        //CheckFalse(_arrowBL);
        //CheckFalse(_arrowB);
    }
    private void CheckFalse(GameObject obj)
    {
        if (obj.activeSelf == true)
        {
            obj.SetActive(false);
        }
    }

    private BaseItem GetThrowItem()
    {
        BaseItem item = UseItemInformation.UseItemMain;
        //ボールの場合はコピーを用意
        if(item.IType == ItemType.Ball)
        {
            BallBase b = (BallBase)item;
            b.RestCount--;
            if (b.RestCount == 0)
            {
                return b;
            }
            b = b.Copy();
            return b;
        }
        else
        {
            return item;
        }
    }

    private BaseItem GetItem()
    {
        return UseItemInformation.UseItemMain;
    }

    //private BaseItem GetItem(Guid gui)
    //{
    //    if (PlayerCharacter.HasItemPlayer(gui) == true)
    //    {
    //        return PlayerCharacter.ItemList[gui];
    //    }
    //    else
    //    {
    //        _model.SetUpItemMap();
    //        return _model.ItemMap.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y);
    //    }
    //}
    private long GetNowTimeTicks()
    {
        return (PreviouslyTimestamp + (DateTime.Now - GameStartTimestamp).Ticks);
    }

#if UNITY_EDITOR

    private void EnemyTester(int dist,MapPoint mp,long objno)
    {
        for (int i = 0; i < dist; i++)
        {
            for (int j = 0; j < dist; j++)
            {
                BaseEnemyCharacter tenemy = TableEnemy.GetEnemy(10003, 1, 1, 1, 1, 1, 1, 1);
                EnemyTesterSub(tenemy, MapPoint.Get(mp.X + i, mp.Y + j));
            }
        }
    }

    private void EnemyTester(BaseEnemyCharacter tenemy)
    {
        EnemyTester(tenemy, _player.CurrentPoint);
    }
    private void EnemyTester(BaseEnemyCharacter tenemy,MapPoint mp)
    {
        _model.SetUpCharacterMap();
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y + 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y - 2))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y + 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X, mp.Y - 1))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 2, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 2, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X + 1, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y))) return;
        if (EnemyTesterSub(tenemy, MapPoint.Get(mp.X - 1, mp.Y))) return;
    }

    private bool EnemyTesterSub(BaseEnemyCharacter tenemy,MapPoint mp)
    {
        if(CommonFunction.IsNull(_model.Dungeon.DungeonMap.Get(mp)) == true)
        {
            return false;
        }
        if ((_model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.Room
                || _model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.RoomEntrance
                || _model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.RoomExit
                || _model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.Load)
            && CommonFunction.IsNull(_model.CharacterMap.Get(mp)) == true)
        {
            tenemy.SetCharacterDisplayObject(mp.X, mp.Y);
            _model.AddNewCharacter(tenemy);
            return true;
        }
        return false;
    }

    private MapPoint MapEmpty()
    {
        _model.SetUpCharacterMap();
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 2))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 2))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 2);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 1))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y + 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 1))) return MapPoint.Get(_player.CurrentPoint.X, _player.CurrentPoint.Y - 1);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X + 2, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X - 2, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X + 1, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y);
        if (IsMapEmpty(MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y))) return MapPoint.Get(_player.CurrentPoint.X - 1, _player.CurrentPoint.Y);
        return null;
    }
    private bool IsMapEmpty(MapPoint mp)
    {

        if ((_model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.Room
                || _model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.RoomEntrance
                || _model.Dungeon.DungeonMap.Get(mp).State == LoadStatus.RoomExit))
        {
            return true;
        }
        return false;
    }
#endif
}

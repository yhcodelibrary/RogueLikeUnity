using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

public class ManageGameSelect : MonoBehaviour
{
    public GameObject Camera;
    public GameObject[] CameraTargets;
    public Dictionary<long, GameObject> GameObjectMapping;
    public int CurrentTarget = 0;
    private GameObject ScrollViewSelected;
    private GameObject ScrollViewSelectedUnit;
    private GameObject ScrollViewSelectedContents;

    private GameObject CharacterStatusPanel;
    private GameObject DungeonStatusPanel;


    /// <summary>
    /// フェード中の透明度
    /// </summary>
    private float fadeAlpha = 1;

    /// <summary>
    /// フェード中かどうか
    /// </summary>
    private bool isFading = false;

    private GameObject TargetPlayCharacter;
    private GameObject _keyDisp;

    public Dictionary<long, DungeonInformation> Dungeons;
    public Dictionary<long, PlayerInformation> Characters;

    public Dictionary<long, GameObject> SelectedList;
    public long SelectCharacter;
    public long SelectDungeon;

    public Texture2D BlackTexture;

    private ManageFade _manageFade;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    

    private SelectTurnState _state;



    private void Awake()
    {
        //Application.targetFrameRate = 30; //30FPSに設定

    }

    void Start()
    {

#if UNITY_EDITOR
        try
        {
            StartMain();
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
    private void StartMain()
    {

        GameObject gm = new GameObject("FloorChanger");
        _manageFade = gm.AddComponent<ManageFade>();
        _manageFade.SetupFade();
        _manageFade.Wait = 0.01f;
        _manageFade.PlayFadeOut(false);

        GameObject.Find("OnBGMCheck").GetComponent<Toggle>().isOn =
            MusicInformation.Music.IsMusicOn;

        GameObject.Find("OnSoundCheck").GetComponent<Toggle>().isOn =
            SoundInformation.Sound.IsPlay;

        GameObject.Find("OnVoiceCheck").GetComponent<Toggle>().isOn =
            VoiceInformation.Voice.IsPlay;

        if (KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
        {
            GameObject.Find("UseMouse").GetComponent<Toggle>().isOn =
                true;
        }

        if (ScoreInformation.Info.PlayerNameBase !=CommonConst.Message.UnityChanName && ScoreInformation.Info.PlayerNameBase != CommonConst.Message.StellaChanName)
        {
            GameObject.Find("NameInputField").GetComponent<InputField>().text = ScoreInformation.Info.PlayerNameBase;
        }
        else
        {
            GameObject.Find("NameInputField").GetComponent<InputField>().text = "";
        }

        //MusicInformation.Music.Setup(MusicInformation.MusicType.CharacterSelect);

        ScrollViewSelected = GameObject.Find("SelectTargetScrollView");
        ScrollViewSelectedUnit = GameObject.Find("SelectUnit");
        ScrollViewSelectedContents = GameObject.Find("SelectTargetContent");

        _keyDisp = GameObject.Find("KeyDisplayPanel");

        CharacterStatusPanel = GameObject.Find("CharacterStatusPanel");
        DungeonStatusPanel = GameObject.Find("DungeonStatusPanel");

        ScrollViewSelectedUnit.SetActive(false);
        DungeonStatusPanel.SetActive(false);

        SelectedList = new Dictionary<long, GameObject>();

        _state = SelectTurnState.ManageStart;

        _keyDisp.SetActive(false);
        

        //ダンジョンデータを読み込み
        Dungeons = TableDungeonMaster.GetAllValue();
        //キャラクターデータを読み込み
        Characters = TablePlayer.GetAllValue();
        //初期選択を格納
        SelectCharacter = Characters.First().Value.ObjNo;
        InitialzeContent(Characters, SelectCharacter);

        MainThreadDispatcher.StartUpdateMicroCoroutine(TransSceneStart(1f));
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
            StartCoroutine(ErrorInformation.Info.SendLogCorutine(e));
            throw e;
        }

#else
        
        try
        {
            UpdateMain();
        }
        catch(Exception e)
        {
            StartCoroutine(ErrorInformation.Info.SendLogCorutine(e));
        }
#endif
    }

    private void UpdateMain()
    {
        if (ManageWait.Info.IsWait == true)
        {
            return;
        }
        if (isFading == true)
        {
            return;
        }


        _keyDisp.SetActive(false);
        //キー表示の場合はキー表示を優先する
        if (Input.GetKey(KeyControlInformation.Info.KeyDisplay))
        {
            SetKeyDisplay();
        }
        //キャラクター選択の時
        else if (_state == SelectTurnState.CharacterSelect)
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("NameInputField"))
            {
                return;
            }

            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                SelectCharacter = SelectUp(SelectedList, SelectCharacter);

                //詳細の更新
                SetCharacterDetail(SelectCharacter);
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                SelectCharacter = SelectDown(SelectedList, SelectCharacter);

                //詳細の更新
                SetCharacterDetail(SelectCharacter);
            }
            else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
            {
                DecisionCharacter();
            }
        }
        //アクション選択の時
        else if(_state == SelectTurnState.ActionSelectStart)
        {

        }
        //キャラクターアニメーションの時
        else if (_state == SelectTurnState.CharacterAnimation)
        {

            Animator anim = TargetPlayCharacter.GetComponent<Animator>();

            // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

            if (currentBaseState.fullPathHash != idleState)
            {
                anim.SetBool("IsStart", false);
            }
        }
        //ダンジョン選択前処理
        else if (_state == SelectTurnState.DungeonSelectStart)
        {
            InitialzeContent(Dungeons, Dungeons.First().Value.DungeonObjNo);
            _state = SelectTurnState.DungeonSelect;
        }
        else if (_state == SelectTurnState.DungeonSelect)
        {

            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("NameInputField"))
            {
                return;
            }

            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                SelectDungeon = SelectUp(SelectedList, SelectDungeon);

                //詳細の更新
                SetDungeonDetail(SelectDungeon);
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                SelectDungeon = SelectDown(SelectedList, SelectDungeon);

                //詳細の更新
                SetDungeonDetail(SelectDungeon);

            }
            //ダンジョン決定　シーン移動
            else if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk))
            {
                DecisionDungeon();

            }
            if (Input.GetKeyDown(KeyControlInformation.Info.MenuCancel))
            {
                CancelDungeon();
            }
        }
    }

    private void DecisionCharacter()
    {
        //キャラクタ情報を格納
        PlayerInformation.Info = Characters[SelectCharacter];

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

        //ボイスを鳴らす
        VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.VoiceType.Start);

        Animator anim = TargetPlayCharacter.GetComponent<Animator>();
        anim.SetBool("IsStart", true);

        StartCoroutine(TransAnimationEnd(4f));

        _state = SelectTurnState.CharacterAnimation;
    }
    private void DecisionDungeon()
    {

        string name = GameObject.Find("NameInputField").GetComponent<InputField>().text;

        ScoreInformation.Info.PlayerName = name;

        //ダンジョン情報を格納
        DungeonInformation.Info = Dungeons[SelectDungeon];

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.GameStart);

        MusicInformation.Music.IsFadeout = true;

        _manageFade.SetWaitDefault();
        _manageFade.PlayFadeIn(true);


        MainThreadDispatcher.StartUpdateMicroCoroutine(TransSceneEnd(1));
        
    }
    private void CancelDungeon()
    {
        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.MenuCancel);

        InitialzeContent(Characters, SelectCharacter);
        _state = SelectTurnState.CharacterSelect;
    }

    private long SelectUp(Dictionary<long, GameObject> List, long objNo)
    {

        //表示リストが0だったら無視
        if (List.Count == 0)
        {

        }
        //最初の項目だったら無視
        else if (List.Keys.First() == objNo)
        {

        }
        //それ以外だったら1つ上の項目に移動
        else
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

            ManageWait.Info.WaitSelect();

            SetItemUnSelectBack(List[objNo]);
            //選択中の番号を取得
            int index = List.Keys.ToList().IndexOf(objNo);
            index--;
            objNo = List.Keys.ElementAt(index);
            //詳細の更新
            SetItemSelectBack(List[objNo]);

            float height = List.Count * 25f;
            int idx = List.Keys.ToList().IndexOf(objNo);
            int n = List.Count;

            CommonFunction.SetCenterViewItem(height, ScrollViewSelected, idx, n);
        }
        return objNo;
    }
    private long SelectDown(Dictionary<long, GameObject> List, long objNo)
    {

        //表示リストが0だったら無視
        if (List.Count == 0)
        {
        }
        //最後の項目だったら無視
        else if (List.Keys.Last() == objNo)
        {
        }
        //それ以外だったら1つ下の項目に移動
        else
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

            ManageWait.Info.WaitSelect();

            SetItemUnSelectBack(List[objNo]);
            //選択中の番号を取得
            int index = List.Keys.ToList().IndexOf(objNo);
            index++;
            objNo = List.Keys.ElementAt(index);

            SetItemSelectBack(List[objNo]);

            float height = List.Count * 25f;
            int idx = List.Keys.ToList().IndexOf(objNo);
            int n = List.Count;

            CommonFunction.SetCenterViewItem(height, ScrollViewSelected, idx, n);
        }
        return objNo;
    }
    private void InitialzeContent(Dictionary<long, DungeonInformation> inf, long objNo)
    {
        GameObject.Find("ActionText").GetComponent<Text>().text = CommonConst.Message.SelectDungeon;
        CharacterStatusPanel.SetActive(false);
        DungeonStatusPanel.SetActive(true);

        foreach (GameObject t in SelectedList.Values)
        {
            Destroy(t.gameObject);
            
        }
        SelectedList.Clear();

        int index = 0;
        foreach (DungeonInformation d in inf.Values)
        {
            GameObject copy = GameObject.Instantiate(ScrollViewSelectedUnit, ScrollViewSelectedContents.transform);
            copy.SetActive(true);
            copy.transform.Find("OddImage").gameObject.SetActive(index % 2 == 0);
            index++;
            GameObject ray = copy.transform.Find("Text").gameObject;
            ray.GetComponent<Text>().text = d.Name;

            //イベントハンドラの設定
            CommonFunction.AddListener(ray, EventTriggerType.PointerDown, e => OnClickSelectDungeon(e, d.DungeonObjNo));

            SetItemUnSelectBack(copy);

            SelectedList.Add(d.DungeonObjNo, copy);
        }

        SetItemSelectBack(SelectedList[objNo]);
        SetDungeonDetail(objNo);
        SelectDungeon = objNo;
    }
    private void InitialzeContent(Dictionary<long, PlayerInformation> inf, long objNo)
    {
        GameObject.Find("ActionText").GetComponent<Text>().text = CommonConst.Message.SelectCharacter;

        CharacterStatusPanel.SetActive(true);
        DungeonStatusPanel.SetActive(false);

        foreach (GameObject t in SelectedList.Values)
        {
            Destroy(t.gameObject);
        }
        SelectedList.Clear();

        int index = 0;
        foreach (PlayerInformation d in inf.Values)
        {
            GameObject copy = GameObject.Instantiate(ScrollViewSelectedUnit, ScrollViewSelectedContents.transform);
            copy.SetActive(true);

            copy.transform.Find("OddImage").gameObject.SetActive(index % 2 == 0);
            index++;
            GameObject ray = copy.transform.Find("Text").gameObject;
            ray.GetComponent<Text>().text = d.DefaultName;

            //イベントハンドラの設定
            CommonFunction.AddListener(ray, EventTriggerType.PointerDown, e => OnClickSelectCharacter(e, d.ObjNo));

            SetItemUnSelectBack(copy);

            SelectedList.Add(d.ObjNo, copy);
        }

        SetItemSelectBack(SelectedList[objNo]);
        SetCharacterDetail(objNo);
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransAnimationEnd(float interval)
    {

        Animator anim = TargetPlayCharacter.GetComponent<Animator>();

        yield return new WaitForSeconds(0.3f);

        // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
        AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        while (currentBaseState.fullPathHash != idleState)
        {
            if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk)
                || KeyControlInformation.Info.OnLeftClick())
            {
                break;
            }

            yield return null;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        }

        //yield return new WaitForSeconds(0.5f);

        _state = SelectTurnState.DungeonSelectStart;
    }


    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransSceneStart(float interval)
    {
        //CanvasGroup cv = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        //cv.alpha = 0;
        //だんだん暗く
        this.isFading = true;

        float time = 0;
        //0.5秒待つ
        while (time <= 0.5f)
        {
            time += CommonFunction.GetDelta(1);
            yield return null;
        }

        _state = SelectTurnState.CharacterSelect;
        this.isFading = false;
        

        ////だんだん明るく
        //time = 0;
        //while (time <= interval)
        //{
        //    time += CommonFunction.GetDelta(1);
        //    yield return null;
        //}
        

        //float cva = 0.02f;

        //while (cv.alpha != 1)
        //{
        //    cv.alpha = Mathf.Clamp(cv.alpha + cva, 0f, 1f);
        //    time += Time.deltaTime;
        //    yield return 0;
        //}
        

    }


    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransSceneEnd(float interval)
    {
        //だんだん暗く
        float time = 0;
        while (time <= interval)
        {
            time += CommonFunction.GetDelta(1);
            yield return 0;
        }
        System.GC.Collect();

        //シーン切替
        SceneManager.LoadScene("Scenes/rouge1");
    }

    /// <summary>
    /// キャラクターの詳細
    /// </summary>
    /// <param name="objno"></param>
    private void SetCharacterDetail(long objno)
    {
        TargetPlayCharacter = GameObject.Find(Characters[objno].InstanceName);

        Transform tar = null;
        foreach (GameObject g in CameraTargets)
        {
            if (g.name == Characters[objno].CameraName)
            {
                tar = g.transform;
            }
        }
        Camera.GetComponent<CharacterSelectCam>().SetTarget(tar);

        
        CharacterStatusPanel.transform.Find("NameHeader").GetComponent<Text>().text
            = CommonConst.Message.CharacterName;
        CharacterStatusPanel.transform.Find("NameText").GetComponent<Text>().text
            = Characters[objno].DefaultName;
        CharacterStatusPanel.transform.Find("ItemHaveHeader").GetComponent<Text>().text
            = CommonConst.Message.ItemMaximumPossession;
        CharacterStatusPanel.transform.Find("ItemHaveText").GetComponent<Text>().text
            = Characters[objno].ItemMaxCount.ToString();
        CharacterStatusPanel.transform.Find("ItemHaveHeader").GetComponent<Text>().text
            = CommonConst.Message.ItemMaximumPossession;
        CharacterStatusPanel.transform.Find("DeathblowHeader").GetComponent<Text>().text
            = CommonConst.Message.Finisher;
        CharacterStatusPanel.transform.Find("DeathblowText").GetComponent<Text>().text
            = Characters[objno].DeathblowName;
        CharacterStatusPanel.transform.Find("DeathblowDetailText").GetComponent<Text>().text
            = Characters[objno].DeathblowDescription;
        CharacterStatusPanel.transform.Find("DeathblowDetailText2").GetComponent<Text>().text
            = Characters[objno].DeathblowDescription2;
        CharacterStatusPanel.transform.Find("AlcHeader").GetComponent<Text>().text
            = CommonConst.Message.FormulationTalent;
        CharacterStatusPanel.transform.Find("AlcText").GetComponent<Text>().text
            = Characters[objno].AlcReasonable;
        
        VoiceInformation.Voice.PType = Characters[objno].PType;
    }

    /// <summary>
    /// ダンジョンの詳細
    /// </summary>
    /// <param name="objno"></param>
    private void SetDungeonDetail(long objno)
    {
        Transform tar = null;
        foreach (GameObject g in CameraTargets)
        {
            if (g.name == Dungeons[objno].CameraName)
            {
                tar = g.transform;
                break;
            }
        }
        Camera.GetComponent<CharacterSelectCam>().SetTarget(tar);

        DungeonStatusPanel.transform.Find("NameHeader").GetComponent<Text>().text
            = CommonConst.Message.DungeonName;
        DungeonStatusPanel.transform.Find("NameText").GetComponent<Text>().text
            = Dungeons[objno].Name;
        DungeonStatusPanel.transform.Find("FloorHeader").GetComponent<Text>().text
            = CommonConst.Message.DungeonDepth;
        DungeonStatusPanel.transform.Find("FloorText").GetComponent<Text>().text
            = Dungeons[objno].DisruptFloor.ToString();
        DungeonStatusPanel.transform.Find("AnalyzeHeader").GetComponent<Text>().text
            = CommonConst.Message.Appraisal; 
        DungeonStatusPanel.transform.Find("AnalyzeText").GetComponent<Text>().text
            = Dungeons[objno].IsAnalyze ? CommonConst.Message.Exist : CommonConst.Message.None;
        DungeonStatusPanel.transform.Find("TimeText").GetComponent<Text>().text
            = Dungeons[objno].IsTimer ? CommonConst.Message.Exist : CommonConst.Message.None;
        DungeonStatusPanel.transform.Find("BringHeader").GetComponent<Text>().text
            = CommonConst.Message.Bringing; 
        DungeonStatusPanel.transform.Find("BringText").GetComponent<Text>().text
            = Dungeons[objno].IsBringing ? CommonConst.Message.Exist : CommonConst.Message.None;
        DungeonStatusPanel.transform.Find("VisibleHeader").GetComponent<Text>().text
            = CommonConst.Message.Visibility;
        DungeonStatusPanel.transform.Find("VisibleText").GetComponent<Text>().text
            = Dungeons[objno].IsBadVisible ? CommonConst.Message.Bad : CommonConst.Message.Good;
        DungeonStatusPanel.transform.Find("DungeonDetailText").GetComponent<Text>().text
            = Dungeons[objno].Description;
        DungeonStatusPanel.transform.Find("TimeHeader").GetComponent<Text>().text
            = CommonConst.Message.TurnHaveTime;
    }
    

    /// <summary>
    /// 対象位置のイメージを消す
    /// </summary>
    private void SetItemUnSelectBack(GameObject obj)
    {
        Image[] cols = obj.transform.GetComponentsInChildren<Image>();
        cols[cols.Length - 1].color =
            new Color(cols[cols.Length - 1].color.r, cols[cols.Length - 1].color.g, cols[cols.Length - 1].color.b, 0.0f);
    }

    /// <summary>
    /// 対象位置のイメージをつける
    /// </summary>
    private void SetItemSelectBack(GameObject obj)
    {
        Image[] cols = obj.transform.GetComponentsInChildren<Image>();
        cols[cols.Length - 1].color =
            new Color(cols[cols.Length - 1].color.r, cols[cols.Length - 1].color.g, cols[cols.Length - 1].color.b, 1.0f);
    }

    private void SetKeyDisplay()
    {
        _keyDisp.SetActive(true);
        _keyDisp.transform.Find("KeyDisplayText").GetComponent<Text>().text = KeyControlInformation.Info.GetKeyValueText();
    }

    public void OnClickSelectCharacter(BaseEventData eventData, long no)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;

        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if (CommonFunction.IsDoubleClick())
            {
                DecisionCharacter();
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                SetItemUnSelectBack(SelectedList[SelectCharacter]);
                SelectCharacter = no;

                SetItemSelectBack(SelectedList[SelectCharacter]);

                //詳細の更新
                SetCharacterDetail(SelectCharacter);
            }
            return;
        }
    }
    public void OnClickSelectDungeon(BaseEventData eventData, long no)
    {
        if (KeyControlInformation.Info.OnLeftClick(eventData) == true)
        {
            if (CommonFunction.IsDoubleClick())
            {
                DecisionDungeon();
            }
            else
            {
                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.MenuMove);

                SetItemUnSelectBack(SelectedList[SelectDungeon]);
                SelectDungeon = no;

                SetItemSelectBack(SelectedList[SelectDungeon]);

                //詳細の更新
                SetDungeonDetail(SelectDungeon);
            }
        }
        else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        {
            CancelDungeon();
        }
    }
}

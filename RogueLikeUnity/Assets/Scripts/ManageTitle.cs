using Assets.Scripts;
using Assets.Scripts.Extend;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageTitle : MonoBehaviour
{
    private GameObject SelectTargetPanel;
    private GameObject ScrollViewSelected;
    private GameObject ScrollViewSelectedUnit;
    private GameObject ScrollViewSelectedContents;
    
    /// <summary>
    /// フェード中の透明度
    /// </summary>
    //private float fadeAlpha = 0;

    private bool IsStart;

    private ManageFade _manageFade;

    private Dictionary<int,GameObject> SelectedList;
    private int Selected;

    private string TargetScene;

    private TitleState state;

    private GameObject TitleJp;
    private GameObject TitleEn;

    public enum TitleState
    {
        Input,
        ActionSelectInit,
        ActionSelect,
        FadeInit,
        FadeInitLoadSave,
        Fade
    }
    public enum TitleSelectAction
    {
        NewGame,
        Continue
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
        //選択肢を作成
        //Dictionary<TitleSelectAction,>
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        state = TitleState.Input;

        GameStateInformation.IsEnglish = false;

#if UNITY_EDITOR
        const string UrlRes = @"http://localhost:60860/Unity/Resource/lng";
#else
        const string UrlRes = @"http://custom-sb.azurewebsites.net/Unity/Resource/lng";
#endif

        //OTPを取得
        //WWW www = new WWW(UrlRes);
        yield return null;
        //while (www.isDone == false || www.progress != 1)
        //{
        //    yield return null;
        //}

        //yield return www;

        //if (CommonFunction.IsNull(www.error) == false)
        //{
        //    www.Dispose();
        //}
        //else
        //{
        //    if (www.text == "en")
        //    {
        //        GameStateInformation.IsEnglish = true;
        //    }
        //}

        TitleJp = GameObject.Find("TitleImage");
        TitleEn = GameObject.Find("TitleImageEng");

        if (GameStateInformation.IsEnglish == true)
        {
            GameObject.Find("OnEnglish").GetComponent<Toggle>().isOn = true;
            CommonConst.Message.SetEn();
            CommonConst.DeathMessage.SetEn();
            CommonFunction.SetActive(TitleEn, true);
            CommonFunction.SetActive(TitleJp, false);
        }
        else
        {
            GameObject.Find("OnEnglish").GetComponent<Toggle>().isOn = false;
            CommonConst.Message.SetJp();
            CommonConst.DeathMessage.SetJp();
            CommonFunction.SetActive(TitleEn, false);
            CommonFunction.SetActive(TitleJp, true);
        }

        //バナー表示
        //AdMobExt.bannerView.Show();
        //StartCoroutine(CheckStartup());

        ManageNews.Setup();

        GameObject.Find("TitleSubText").GetComponent<Text>().text = CommonConst.Message.SubTitle;

        GameObject gm = new GameObject("FloorChanger");
        _manageFade = gm.AddComponent<ManageFade>();
        _manageFade.SetupFade();
        _manageFade.Wait = 0.01f;
        _manageFade.PlayFadeOut(false, 0.5f);

        SelectedList = new Dictionary<int, GameObject>();

        GameObject.Find("OnBGMCheck").GetComponent<Toggle>().isOn =
            MusicInformation.Music.IsMusicOn;

        GameObject.Find("OnSoundCheck").GetComponent<Toggle>().isOn =
            SoundInformation.Sound.IsPlay;

        GameObject.Find("OnVoiceCheck").GetComponent<Toggle>().isOn =
            VoiceInformation.Voice.IsPlay;

        GameObject.Find("NameInputField").GetComponent<InputField>().text = ScoreInformation.Info.PlayerNameBase;

        SelectTargetPanel = GameObject.Find("SelectTargetPanel");
        ScrollViewSelected = GameObject.Find("SelectTargetScrollView");
        ScrollViewSelectedUnit = GameObject.Find("SelectUnit");
        ScrollViewSelectedContents = GameObject.Find("SelectTargetContent");

        MusicInformation.Music.Setup(MusicInformation.MusicType.Title);

        StringBuilder sb = new StringBuilder();
        //キー情報の読み込み
        KeyControlModel kcm = SaveDataInformation.LoadKeyControl();
        if (CommonFunction.IsNull(kcm) == false)
        {
            KeyControlInformation.Info = kcm;
            sb.AppendLine(CommonConst.Message.KeyConfigSuccess);
        }
        KeyControlInformation.Info.OpMode = OperationMode.KeyOnly;

        GameObject.Find("PushEnter").GetComponent<Text>().text = string.Format("Push {0}", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());

        //システム情報の読み込み
        SystemInformation si = SaveDataInformation.LoadSystemInformation();
        if (CommonFunction.IsNull(si) == false)
        {
            if (CommonFunction.IsNullOrWhiteSpace(GameObject.Find("NameInputField").GetComponent<InputField>().text) == true)
            {
                GameObject.Find("NameInputField").GetComponent<InputField>().text = si.CharacterName;
            }
            MusicInformation.Music.Volume = CommonFunction.NumberToPercent(si.BGMVolume);
            SoundInformation.Sound.Volume = CommonFunction.NumberToPercent(si.SoundVolume);
            VoiceInformation.Voice.Volume = CommonFunction.NumberToPercent(si.VoiceVolume);
            sb.AppendLine(CommonConst.Message.SystemSettingSuccess);
        }
        GameObject.Find("SystemText").GetComponent<Text>().text = sb.ToString();


        //セーブデータを読み込み
        SavePlayingInformation save = SaveDataInformation.LoadPlayingValue();
        if (CommonFunction.IsNull(save) == false)
        {
            save.IsLoadSave = true;
            ResourceInformation.SaveInfo = save;
        }

        CommonFunction.SetActive(ScrollViewSelectedUnit, false);
        CommonFunction.SetActive(SelectTargetPanel, false);

        IsStart = true;

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
        if(IsStart == false)
        {
            return;
        }

        if (ManageWait.Info.IsWait == true)
        {
            return;
        }

        if (state == TitleState.Input)
        {
            if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk)
                || (KeyControlInformation.Info.OnLeftClick()
                    && CommonFunction.IsDoubleClick()))
            {
                if (CommonFunction.IsNull(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject) == false
                    && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == "NameInputField")
                {
                    return;
                }

                if (CommonFunction.IsNull(ResourceInformation.SaveInfo) == false 
                    && ResourceInformation.SaveInfo.IsLoadSave == true)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.MenuOk);

                    state = TitleState.ActionSelectInit;
                }
                else
                {
                    TargetScene = "Scenes/GameSelect";
                    state = TitleState.FadeInit;
                }
            }
        }
        else
        if (state == TitleState.ActionSelectInit)
        {
            InitialzeContent();
            
            state = TitleState.ActionSelect;
        }
        else
        if (state == TitleState.ActionSelect)
        {


            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == GameObject.Find("NameInputField"))
            {
                return;
            }

            //上
            if (KeyControlInformation.Info.OnMoveUp())
            {
                Selected = SelectUp(SelectedList, Selected);
            }
            //下
            else if (KeyControlInformation.Info.OnMoveDown())
            {
                Selected = SelectDown(SelectedList, Selected);

            }
            //ダンジョン決定　シーン移動
            else if (KeyControlInformation.Info.OnKeyDownMenu(KeyType.MenuOk))
            {
                if(Selected == (int)TitleSelectAction.Continue)
                {


                    TargetScene = "Scenes/rouge1";
                    state = TitleState.FadeInitLoadSave;
                }
                else if (Selected == (int)TitleSelectAction.NewGame)
                {
                    ResourceInformation.SaveInfo = new SavePlayingInformation();
                    SaveDataInformation.RemoveSaveData();
                    TargetScene = "Scenes/GameSelect";
                    state = TitleState.FadeInit;
                }

            }

        }
        else
        if (state == TitleState.FadeInitLoadSave)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.GameStart);

            string name = GameObject.Find("NameText").GetComponent<Text>().text;

            ScoreInformation.Info.PlayerName = name;

            _manageFade.PlayFadeIn(true, 0.5f);
            

            SavePlayingInformation save = ResourceInformation.SaveInfo;

            //保存されていたら保存データを読み込み
            if (save.IsLoadSave == true)
            {
                DungeonInformation.Info = TableDungeonMaster.GetValue(save.don);
                PlayerInformation.Info = TablePlayer.GetValue(save.pon);

                //アイテムデータをロード
                SaveItemData[] items = SaveDataInformation.LoadItemValue();

                //アイテムデータを反映
                GameStateInformation.TempItemList = SaveItemInformation.ToListBaseItem(items);

                //セーブデータを削除
                SaveDataInformation.RemoveSaveData();
            }
            
            StartCoroutine(TransScene(TargetScene, 0.5f));

            state = TitleState.Fade;
        }
        else
        if (state == TitleState.FadeInit)
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.GameStart);

            string name = GameObject.Find("NameText").GetComponent<Text>().text;

            ScoreInformation.Info.PlayerName = name;
            
            _manageFade.PlayFadeIn(true, 0.5f);

            StartCoroutine(TransScene(TargetScene, 0.5f));

            state = TitleState.Fade;
        }
    }


    public void Awake()
    {
        //Application.targetFrameRate = 30; //30FPSに設定
        //if (this != Instance)
        //{
        //    Destroy(this);
        //    return;
        //}

        //DontDestroyOnLoad(this.gameObject);

    }

    private void InitialzeContent()//List<TitleSelectAction> inf, int objNo)
    {
        CommonFunction.SetActive(SelectTargetPanel, true);


        foreach (GameObject t in SelectedList.Values)
        {
            Destroy(t.gameObject);
        }
        SelectedList.Clear();

        int index = 0;
        GameObject copy = GameObject.Instantiate(ScrollViewSelectedUnit, ScrollViewSelectedContents.transform);
        copy.SetActive(true);
        copy.transform.Find("OddImage").gameObject.SetActive(index % 2 == 0);
        GameObject ray = copy.transform.Find("Text").gameObject;
        ray.GetComponent<Text>().text = CommonConst.Message.StartContinue;

        //イベントハンドラの設定
        CommonFunction.AddListener(copy, EventTriggerType.PointerDown, e => OnClickSelectDungeon(e, (int)TitleSelectAction.Continue));

        SelectedList.Add((int)TitleSelectAction.Continue, copy);

        index++;
        copy = GameObject.Instantiate(ScrollViewSelectedUnit, ScrollViewSelectedContents.transform);
        copy.SetActive(true);
        copy.transform.Find("OddImage").gameObject.SetActive(index % 2 == 0);
        ray = copy.transform.Find("Text").gameObject;
        ray.GetComponent<Text>().text = CommonConst.Message.StartFirst;

        //イベントハンドラの設定
        CommonFunction.AddListener(copy, EventTriggerType.PointerDown, e => OnClickSelectDungeon(e, (int)TitleSelectAction.NewGame));

        SelectedList.Add((int)TitleSelectAction.NewGame, copy);


        SetItemUnSelectBack(copy);

        Selected = (int)TitleSelectAction.Continue;

        SetItemSelectBack(SelectedList[Selected]);
    }

    private int SelectUp(Dictionary<int, GameObject> List, int objNo)
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
    private int SelectDown(Dictionary<int, GameObject> List, int objNo)
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
    public void OnClickSelectDungeon(BaseEventData eventData, int no)
    {
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
                
                SetItemUnSelectBack(SelectedList[Selected]);
                //選択中の番号を取得
                Selected = no;

                SetItemSelectBack(SelectedList[Selected]);
                
            }
        }
        //else if (KeyControlInformation.Info.OnRightClick(eventData) == true)
        //{
        //    CancelDungeon();
        //}
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

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
        yield return new WaitForSeconds(interval);

        //シーン切替
        SceneManager.LoadScene(scene);

        //だんだん明るく
        //time = 0;
        //while (time <= interval)
        //{
        //    this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
        //    time += Time.deltaTime;
        //    yield return 0;
        //}

        //this.isFading = false;
    }

    public void OnEnglish(GameObject g)
    {
        GameStateInformation.IsEnglish = g.GetComponent<Toggle>().isOn;

        if (GameStateInformation.IsEnglish == true)
        {
            CommonConst.Message.SetEn();
            CommonConst.DeathMessage.SetEn();
            CommonFunction.SetActive(TitleEn, true);
            CommonFunction.SetActive(TitleJp, false);
        }
        else
        {
            CommonConst.Message.SetJp();
            CommonConst.DeathMessage.SetJp();
            CommonFunction.SetActive(TitleEn, false);
            CommonFunction.SetActive(TitleJp, true);
        }

        GameObject.Find("TitleSubText").GetComponent<Text>().text = CommonConst.Message.SubTitle;

    }


    private IEnumerator CheckStartup()
    {
        

#if UNITY_EDITOR
        const string UrlRes = @"http://localhost:60860/Resource/Start";
        const string UrlOTP = @"http://localhost:60860/Resource/IssueStart";
#else
        const string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/Start";
        const string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueStart";
#endif

        //OTPを取得
        WWW www = new WWW(UrlOTP);

        while (www.isDone == false && www.progress != 1)
        {
            yield return null;
        }
        yield return www;

        if (CommonFunction.IsNull(www.error) == false)
        {
            www.Dispose();
            yield break;
        }


        string rowotp = www.text;

        string sessionid = null;
#if UNITY_EDITOR
        sessionid = www.responseHeaders["SET-COOKIE"];
#endif
        www.Dispose();

        //OTPを復号化
        string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.StartOTP);

        //OTPを使って実行キーを暗号化
        string ecjson = CryptInformation.EncryptString(CommonConst.Key.StartKey, otp);
        
        Dictionary<string, string> headers = new Dictionary<string, string>();

#if UNITY_EDITOR
        sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
        Regex rgx = new Regex(";.+");
        sessionid = rgx.Replace(sessionid, "");
        headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));
#endif

        WWWForm form = new WWWForm();
        form.AddField("input", ecjson);

#if UNITY_EDITOR
        www = new WWW(UrlRes, form.data, headers);
#else
        headers = form.headers;
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept");
        headers.Add("Access-Control-Allow-Methods", "POST");
        headers.Add("Access-Control-Allow-Origin", "*");
        www = new WWW(UrlRes, form.data , headers);
#endif

        while (www.isDone == false && www.progress != 1)
        {
            yield return null;
        }

        yield return www;

        string dec = CryptInformation.DecryptString(www.text, CommonConst.CryptKey.StartCrypt);

        if(dec == CommonConst.Key.StartExeKey)
        {
            IsStart = true;
        }

        www.Dispose();
    }
}

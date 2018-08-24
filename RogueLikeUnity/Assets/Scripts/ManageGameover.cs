using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using SocialConnector;
using UniRx;

public class ManageGameover : MonoBehaviour
{

    /// <summary>
    /// フェード中の透明度
    /// </summary>
    private float fadeAlpha = 0;

    /// <summary>
    /// フェード中かどうか
    /// </summary>
    private bool isFading = false;

    private ManageFade _manageFade;

    public void Awake()
    {
        //Application.targetFrameRate = 30; //30FPSに設定
        ResourceInformation.DungeonInit();

    }
    

    private GameObject ActionObject;

    private void Start()
    {

        GameObject gm = new GameObject("FloorChanger");
        _manageFade = gm.AddComponent<ManageFade>();
        _manageFade.SetupFade();
        _manageFade.Wait = 0.01f;
        _manageFade.PlayFadeOut(false,2);

#if UNITY_EDITOR
        //ScoreInformation.Info.isDisrupt = true;
        //PlayerInformation.Info.PType = PlayerType.OricharChan;
#endif
        if (ScoreInformation.Info.isDisrupt == true)
        {
            GameObject.Find("TouhaFail").SetActive(false);
            GameObject.Find("TouhaSuccess").GetComponent<Text>().text = CommonConst.Message.Success;
            MusicInformation.Music.Setup(MusicInformation.MusicType.Gameclear);
            switch (PlayerInformation.Info.PType)
            {
                case PlayerType.UnityChan:
                    GameObject.Find("Character").SetActive(false);
                    GameObject.Find("SD_Stellachan").SetActive(false);
                    GameObject.Find("SD_StellachanClear").SetActive(false);
                    ActionObject = GameObject.Find("CharacterClear");
                    break;
                case PlayerType.OricharChan:                    
                    GameObject.Find("Character").SetActive(false);
                    GameObject.Find("CharacterClear").SetActive(false);
                    GameObject.Find("SD_Stellachan").SetActive(false);
                    ActionObject = GameObject.Find("SD_StellachanClear");
                    break;
            }
        }
        else
        {
            GameObject.Find("TouhaSuccess").SetActive(false);
            GameObject.Find("TouhaFail").GetComponent<Text>().text = CommonConst.Message.Failure;
            MusicInformation.Music.Setup(MusicInformation.MusicType.Gameover);
            switch (PlayerInformation.Info.PType)
            {
                case PlayerType.UnityChan:
                    GameObject.Find("CharacterClear").SetActive(false);
                    GameObject.Find("SD_Stellachan").SetActive(false);
                    GameObject.Find("SD_StellachanClear").SetActive(false);
                    ActionObject = GameObject.Find("Character");
                    break;
                case PlayerType.OricharChan:
                    GameObject.Find("Character").SetActive(false);
                    GameObject.Find("CharacterClear").SetActive(false);
                    GameObject.Find("SD_StellachanClear").SetActive(false);
                    ActionObject = GameObject.Find("SD_Stellachan");
                    break;
            }
        }

        fadeAlpha = 1f;

        GameObject.Find("Name").GetComponent<Text>().text = CommonConst.Message.Name;
        GameObject.Find("Floor").GetComponent<Text>().text = CommonConst.Message.SearchFloor;
        GameObject.Find("DungeonName").GetComponent<Text>().text = CommonConst.Message.DungeonName;
        GameObject.Find("PlayTime").GetComponent<Text>().text = CommonConst.Message.PlayTime;
        GameObject.Find("Level").GetComponent<Text>().text = CommonConst.Message.Level; 
        GameObject.Find("Cause").GetComponent<Text>().text = CommonConst.Message.CauseOfFailure; 
        GameObject.Find("EquipWeapon").GetComponent<Text>().text = CommonConst.Message.TheMostUsedWeapon;
        GameObject.Find("EquipShield").GetComponent<Text>().text = CommonConst.Message.TheMostUsedShield;
        GameObject.Find("EquipRing").GetComponent<Text>().text = CommonConst.Message.TheMostUsedRing;


        GameObject.Find("NameValue").GetComponent<Text>().text =
            ScoreInformation.Info.PlayerName;

        GameObject.Find("DungeonNameValue").GetComponent<Text>().text =
            ScoreInformation.Info.DungeonName;

        GameObject.Find("FloorValue").GetComponent<Text>().text =
            string.Format("{0} F", ScoreInformation.Info.Floor);

        TimeSpan t = new TimeSpan(ScoreInformation.Info.TotalTime);
        GameObject.Find("PlayTimeValue").GetComponent<Text>().text =
            string.Format("{0:0}h {1}m {2}s", Math.Floor(t.TotalHours), t.Minutes, t.Seconds);

        GameObject.Find("ScoreValue").GetComponent<Text>().text =
            string.Format("{0:#,0}", ScoreInformation.Info.Score);

        GameObject.Find("LevelValue").GetComponent<Text>().text =
            string.Format("{0}", ScoreInformation.Info.iLevel);
        
        GameObject.Find("EquipWeaponValue").GetComponent<Text>().text =
            ScoreInformation.Info.MostUseWeaponName;

        GameObject.Find("EquipShieldValue").GetComponent<Text>().text =
            ScoreInformation.Info.MostUseSieldName;

        GameObject.Find("EquipRingValue").GetComponent<Text>().text =
            ScoreInformation.Info.MostUseRingName;

        GameObject.Find("CauseValue").GetComponent<Text>().text =
            ScoreInformation.Info.CauseDeath;

        if (KeyControlInformation.Info.OpMode == OperationMode.UseMouse)
        {
            GameObject.Find("PushStart").GetComponent<Text>().text = string.Format("Push {0} or Mouse Double Click", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());
        }
        else
        {
            GameObject.Find("PushStart").GetComponent<Text>().text = string.Format("Push {0}", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());
        }


        StartCoroutine(ScoreInformation.Info.SendScoreCorutine(ScoreInformation.Info.GetJson()));
        StartCoroutine(TransScene(1f));
        //OnPushShare();
    }
    private void Update()
    {
        if(this.isFading == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyControlInformation.Info.MenuOk)
            || (KeyControlInformation.Info.OnLeftClick()
                && CommonFunction.IsDoubleClick()))
        {
            TableItemIncidence.Destroy();

            _manageFade.Wait = 0.6f;
            _manageFade.PlayFadeIn(true, 0.5f);

            StartCoroutine(TransSceneToNext("Scenes/GameSelect"));
        }
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransSceneToNext(string scene)
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Scenes/GameSelect");
    }
    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(float interval)
    {
        CanvasGroup cv = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        cv.alpha = 0;
        //だんだん暗く
        this.isFading = true;
        float time = 0;

        yield return new WaitForSeconds(1);

        //だんだん明るく
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.smoothDeltaTime;
            yield return 0;
        }
        float cva = 0.02f;


        Animator taranim = ActionObject.GetComponent<Animator>();
        taranim.SetBool("IsStart", true);

        while (cv.alpha < 0.3)
        {
            cv.alpha = Mathf.Clamp(cv.alpha + cva, 0f, 1f);
            time += Time.smoothDeltaTime;
            yield return 0;
        }

        if (ScoreInformation.Info.isDisrupt == true)
        {
            //音を鳴らす
            VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.VoiceType.Gameclear);

        }
        else
        {
            //音を鳴らす
            VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.VoiceType.Gameover);
        }
        while (cv.alpha < 1)
        {
            cv.alpha = Mathf.Clamp(cv.alpha + cva, 0f, 1f);
            time += Time.smoothDeltaTime;
            yield return 0;
        }

        yield return new WaitForEndOfFrame();

        //var cameraObject = GameObject.FindWithTag("MainCamera");
        //WebPlayerTweetScript tw = cameraObject.transform.GetComponent<WebPlayerTweetScript>();
        //・tw.test();
        //OnPushShare();

        this.isFading = false;
    }


    // シェア
    public void OnPushShare()
    {
        //メインカメラを取得する
        //var cameraObject = GameObject.FindWithTag("MainCamera");
        //WebPlayerTweetScript tw = cameraObject.transform.GetComponent<WebPlayerTweetScript>();
        //tw.ShareTweet();
        StartCoroutine(Share());
    }

    // シェア処理
    private IEnumerator Share()
    {
        // 画面をキャプチャ
        Application.CaptureScreenshot("screenShot.png");

        // キャプチャを保存するので１フレーム待つ
        yield return new WaitForEndOfFrame();

        // シェアテキスト設定
        string text = "シェアする内容";
        string url = "http://google.com/";

        // キャプチャの保存先を指定
        string texture_url = Application.persistentDataPath + "/screenShot.png";

        // iOS側の処理を呼び出す
        //SocialConnector.SocialConnector.Share(text, url);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("consumer_key", "IrwkW1kQMD9ok0PeGP1l3mKCU");
        headers.Add("consumer_secret", "lvEYsOeypdi4FzpuWi13r7Q9iRadR5Czk5PvF5lCLeJol6Jgfu");
        headers.Add("token", "832955370649759744-IX8KrW26yA9tzoPauUPb0hgpKvllP4I");
        headers.Add("secret", "	YpljKtgeEmJyXCkcCCshL1KrumOJs2CZOZzr0tsjCSsGH");



        WWWForm form = new WWWForm();
        form.AddBinaryData("media_data", MakeScreenShot());

        var www = ObservableWWW.Post("https://upload.twitter.com/1.1/media/upload.json", form.data, headers).ToYieldInstruction();
        while (!(www.HasResult || www.IsCanceled || www.HasError)) // 3つもプロパティ並べるのダルいので次回アップデートでIsDoneを追加します予定
        {
            yield return null;
        }

        string result = "";
        if (www.HasResult == true && CommonFunction.IsNullOrWhiteSpace(www.Result) == false)
        {
            result = www.Result;
        }
        Debug.Log(www.Error);


        //string xml = Account.oAuthMediaRequest(oAuthTwitter.Method.POST, "https://upload.twitter.com/1.1/media/upload.json", "");

    }


    byte[] MakeScreenShot()
    {

        var texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        return texture.EncodeToPNG();
        //var texture = Camera.main.targetTexture;
        //var tex2d = new Texture2D(texture.width, texture.height);
        //RenderTexture.active = texture;
        //tex2d.ReadPixels(new Rect(0, 0, tex2d.width, tex2d.height), 0, 0);
        //tex2d.Apply();
        //return tex2d.EncodeToPNG();
    }
}


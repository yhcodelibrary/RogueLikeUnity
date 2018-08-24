//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Text.RegularExpressions;
//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.Assertions;
//using UnityEngine.Networking;
//using System;

//public class MusicInformation : MonoBehaviour
//{

//#if UNITY_EDITOR
//    //private string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/TypeA";
//    //private string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/Issue";
//    private string UrlRes = @"http://localhost:60860/Resource/TypeA";
//    private string UrlOTP = @"http://localhost:60860/Resource/Issue";
//#else
//    private string UrlParent = "";
//    private string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/TypeA";
//    private string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/Issue";
//    //private string UrlRes = @"http://localhost:60860/Resource/TypeA";
//    //private string UrlOTP = @"http://localhost:60860/Resource/Issue";
//#endif
    
//    public bool start = false;

//    public MusicType Type;
//    private static WebGLStreamingAudioSourceInterop AudioTitle;
//    private static WebGLStreamingAudioSourceInterop AudioRogue1;
//    private static WebGLStreamingAudioSourceInterop AudioGameover;
//    private static WebGLStreamingAudioSourceInterop AudioGameclear;
//    private static WebGLStreamingAudioSourceInterop AudioCharacterSelect;

//    private static bool isMusicOn;
//    public bool IsMusicOn
//    {
//        get { return isMusicOn; }
//        set
//        {
//            if(value == false)
//            {
//                StopBGM();
//                Type = MusicType.None;
//                //if (CommonFunction.IsNull(audio_background) == false)
//                //{
//                //    audio_background.Stop();
//                //    audio_background.clip = null;
//                //}
//            }
//            isMusicOn = value;
//        }
//    }
//    private float _Volume;
//    public float Volume
//    {
//        get
//        {
//            return _Volume;
//        }
//        set
//        {
//            _Volume = Mathf.Clamp(value, 0, 1);
//            CommonFunction.SetVolume(AudioTitle, _Volume);
//            CommonFunction.SetVolume(AudioRogue1, _Volume);
//            CommonFunction.SetVolume(AudioGameover, _Volume);
//            CommonFunction.SetVolume(AudioGameclear, _Volume);
//            CommonFunction.SetVolume(AudioCharacterSelect, _Volume);
//        }
//    }

    
//    public bool IsFadeout;

//    //public void Awake()
//    //{
//    //    if (this != Instance)
//    //    {
//    //        Destroy(this);
//    //        return;
//    //    }

//    //    DontDestroyOnLoad(this.gameObject);
//    //}

//    public static bool IsTitleSetup
//    {
//        get;private set;
//    }
//    public static bool IsRogue1Setup
//    {
//        get; private set;
//    }
//    public static bool IsGameoverSetup
//    {
//        get; private set;
//    }
//    public static bool IsGameclearSetup
//    {
//        get; private set;
//    }
//    public static bool IsCharacterSelectSetup
//    {
//        get; private set;
//    }
//    private static MusicInformation _musicInformation;
//    public static MusicInformation Music
//    {
//        get
//        {
//            if (CommonFunction.IsNull(_musicInformation) == false)
//            {
//                return _musicInformation;
//            }
//            GameObject m = new GameObject("MusicManager");
//            _musicInformation = m.AddComponent<MusicInformation>();
//            _musicInformation.IsMusicOn = false;
//            _musicInformation.Volume = 0.7f;
//            IsTitleSetup = false;
//            IsRogue1Setup = false;
//            IsGameclearSetup = false;
//            IsGameoverSetup = false;
//            return _musicInformation;
//        }
//    }

//    void Awake()
//    {
//        // 自分自身だったり
//        DontDestroyOnLoad(this);
//    }
//    public MusicInformation()
//    {
//    }
//    void Start()
//    {
//        SetupAttach();
//    }

//    public void Setup(MusicType tp)
//    {
//        IsFadeout = false;
//        if(Type != tp)
//        {
//            StopBGM();
//        }

//        Type = tp;
//        if (IsMusicOn == false)
//        {
//            return;
//        }

//        switch (tp)
//        {
//            case MusicType.Title:
//                StartCoroutine("SetupTitleData");
//                break;
//            case MusicType.Rogue1Alpha:
//                StartCoroutine("SetupRogue1Data");
//                break;
//            case MusicType.Gameover:
//                StartCoroutine("SetupGameoverData");
//                break;
//            case MusicType.Gameclear:
//                StartCoroutine("SetupGameclearData");
//                break;
//            case MusicType.CharacterSelect:
//                StartCoroutine("SetupCharacterSelectData");
//                break;
//            default:
//                Type = MusicType.None;
//                break;
//        }
//    }

//    private void SetupAttach()
//    {
//        //audio_background = (AudioSource)gameObject.AddComponent<AudioSource>();
//    }

//    private IEnumerator SetupTitleData()
//    {
//        if (IsTitleSetup == true)
//        {
//            yield break;
//        }
//        List<WebGLStreamingAudioSourceInterop> temp = new List<WebGLStreamingAudioSourceInterop>();
//        //okaetudukumichi.ogg
//        yield return GetAudioClip("2CB988993E92472DA6BAAAAD54005BE2",
//            temp, 106000);
//        if (temp.Count > 0 && CommonFunction.IsNull(temp[0]) == false)
//        {
//            if (CommonFunction.IsNull(AudioTitle) == true)
//            {
//                AudioTitle = temp[0];
//            }
//            IsTitleSetup = true;
//        }
//    }

//    private IEnumerator SetupRogue1Data()
//    {
//        if(IsRogue1Setup == true)
//        {
//            yield break;
//        }
//        List<WebGLStreamingAudioSourceInterop> temp = new List<WebGLStreamingAudioSourceInterop>();
//        //koganemushi.ogg
//        yield return GetAudioClip("6DB1B387FE8441A5801DA8C64FAAAD4A",
//            temp, 128000);
//        if (temp.Count > 0 && CommonFunction.IsNull(temp[0]) == false)
//        {
//            if (CommonFunction.IsNull(AudioRogue1) == true)
//            {
//                AudioRogue1 = temp[0];
//            }
//            IsRogue1Setup = true;
//        }
//    }
//    private IEnumerator SetupGameoverData()
//    {
//        if (IsGameoverSetup == true)
//        {
//            yield break;
//        }
//        List<WebGLStreamingAudioSourceInterop> temp = new List<WebGLStreamingAudioSourceInterop>();
//        //hinokageri.ogg
//        yield return GetAudioClip("CB8EF7D677064788A356E478468D298F",
//            temp, 98000);
//        if (temp.Count > 0 && CommonFunction.IsNull(temp[0]) == false)
//        {
//            if (CommonFunction.IsNull(AudioGameover) == true)
//            {
//                AudioGameover = temp[0];
//            }
//            IsGameoverSetup = true;
//        }
//    }
//    private IEnumerator SetupGameclearData()
//    {
//        if (IsGameclearSetup == true)
//        {
//            yield break;
//        }
//        List<WebGLStreamingAudioSourceInterop> temp = new List<WebGLStreamingAudioSourceInterop>();
//        //hinokageri.ogg
//        yield return GetAudioClip("533FB1F3A33D4A6AAFF8D88CD1893705",
//            temp, 67000);
//        if (temp.Count > 0 && CommonFunction.IsNull(temp[0]) == false)
//        {
//            if (CommonFunction.IsNull(AudioGameclear) == true)
//            {
//                AudioGameclear = temp[0];
//            }
//            IsGameclearSetup = true;
//        }
//    }

//    private IEnumerator SetupCharacterSelectData()
//    {
//        if (IsCharacterSelectSetup == true)
//        {
//            yield break;
//        }
//        List<WebGLStreamingAudioSourceInterop> temp = new List<WebGLStreamingAudioSourceInterop>();
//        //hinokageri.ogg
//        yield return GetAudioClip("E33D2F636AD34CF59ADCD376A3D15917",
//            temp, 121000);
//        if (temp.Count > 0 && CommonFunction.IsNull(temp[0]) == false)
//        {
//            if(CommonFunction.IsNull(AudioCharacterSelect) == true)
//            {
//                AudioCharacterSelect = temp[0];
//            }
//            IsCharacterSelectSetup = true;
//        }
//    }

//    public enum MusicType
//    {
//        None,
//        Title,
//        CharacterSelect,
//        Rogue1Alpha,
//        Gameover,
//        Gameclear
//    }


//    public string GetOTP(out string sessionid)
//    {
//        WWW www = new WWW(UrlOTP);

//        while (www.isDone == false && www.progress != 1)
//        {
           
//        }
//        string otp = www.text;
//        sessionid = www.responseHeaders["SET-COOKIE"];
//        www.Dispose();
//        return otp;

//    }

//    public delegate void WebGLStreamingAudioDidFinish(int audio);
    
//    public IEnumerator GetAudioClip(string resName, List<WebGLStreamingAudioSourceInterop> list,double stoptime)
//    {
//        string sessionid = null;

//        //OTPを取得
//        WWW www = new WWW(UrlOTP);

//        while (www.isDone == false && www.progress != 1)
//        {
//            yield return null;
//        }

//        if (CommonFunction.IsNullOrWhiteSpace(www.error) == false)
//        {
//            www.Dispose();
//            yield break;
//        }

//        yield return www;
//        string rowotp = www.text;

//#if UNITY_EDITOR
//        sessionid = www.responseHeaders["SET-COOKIE"];
//#endif
//        www.Dispose();

//        //string rowotp = GetOTP(out sessionid);

//        //セッションIDを抽出
////        Dictionary<string, string> headers = new Dictionary<string, string>();
////#if UNITY_EDITOR
////        sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
////        Regex rgx = new Regex(";.+");
////        sessionid = rgx.Replace(sessionid, "");
////        headers.Add("Cookie",string.Format("ASP.NET_SessionId={0};", sessionid));
////#endif

//        //OTPを復号化
//        string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.OTP);

//        //OTPを使ってリソース名を暗号化
//        string resname = CryptInformation.EncryptString(resName, otp);

//        //リソース名をエンコード
//        resname = Uri.EscapeDataString(resname);
//        WebGLStreamingAudioSourceInterop clip = (new WebGLStreamingAudioSourceInterop(
//            string.Format("{0}?input={1}&a.ogg", UrlRes, resname), GameObject.Find("MusicManager"), sessionid, stoptime ));

//        list.Add(clip);
//    }

//    WebGLStreamingAudioSourceInterop SpawnAudio(WebGLStreamingAudioSourceInterop audio)
//    {
//        audio.invalidated += (sender) => {

//            //RefreshPanel();
//        };

//        //m_audioList.Add(audio);

//        audio.Play();

//        //RefreshPanel();
//        return audio;
//    }
    
//    private void StopBGM()
//    {

//        if (CommonFunction.IsNull(AudioTitle) == false)
//        {
//            AudioTitle.Stop();
//        }
//        if (CommonFunction.IsNull(AudioRogue1) == false)
//        {
//            AudioRogue1.Stop();
//        }
//        if (CommonFunction.IsNull(AudioGameover) == false)
//        {
//            AudioGameover.Stop();
//        }
//        if (CommonFunction.IsNull(AudioGameclear) == false)
//        {
//            AudioGameclear.Stop();
//        }
//        if (CommonFunction.IsNull(AudioCharacterSelect) == false)
//        {
//            AudioCharacterSelect.Stop();
//        }
//    }

//    public IEnumerator GetAudioClip(string url,AudioClip clp)
//    {
//        WWW www = new WWW(url);

//        while (www.isDone == false && www.progress != 1)
//        {
//            yield return null;
//        }

//        clp = www.GetAudioClip(false, true, AudioType.OGGVORBIS);
        
//        www.Dispose();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(IsMusicOn == false)
//        {
//            return;
//        }

//        switch (Type)
//        {
//            case MusicType.Title:
//                if (IsTitleSetup == true)
//                {
//                    PlayAudio(AudioTitle);
//                }
//                break;
//            case MusicType.Rogue1Alpha:
//                if (IsRogue1Setup == true)
//                {
//                    PlayAudio(AudioRogue1);
//                }
//                break;
//            case MusicType.Gameover:
//                if (IsGameoverSetup == true)
//                {
//                    PlayAudio(AudioGameover);
//                }
//                break;
//            case MusicType.Gameclear:
//                if (IsGameclearSetup == true)
//                {
//                    PlayAudio(AudioGameclear);
//                }
//                break;
//            case MusicType.CharacterSelect:
//                if (IsCharacterSelectSetup == true)
//                {
//                    PlayAudio(AudioCharacterSelect);
//                }
//                break;
//        }
        
//    }

//    private void PlayAudio(WebGLStreamingAudioSourceInterop audio)
//    {

//        audio.Timespan();
//        if (audio.IsPlaying == false)
//        {
//            StopBGM();
//            audio.Volume = Volume;
//            audio.Play();
//        }
//        else if (IsFadeout == true)
//        {
//            float vol = Mathf.Clamp(audio.Volume - 0.02f, 0, 1);
//            if (vol == 0)
//            {
//                audio.Stop();
//            }
//            else
//            {
//                audio.Volume = Mathf.Clamp(audio.Volume - 0.02f, 0, 1);
//            }
//        }
//    }
    
//}


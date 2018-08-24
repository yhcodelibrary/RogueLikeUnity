using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public class VoiceInformation : MonoBehaviour
{
#if UNITY_EDITOR
    //private string UrlRes = @"http://localhost:60860/Resource/TypeABV?t=VI";
    //private string UrlOTP = @"http://localhost:60860/Resource/IssueAB";
    //private string UrlSet = @"http://localhost:60860/Resource/SetAB";
#else
    //private string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/TypeABV?t=VI";
    //private string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueAB";
    //private string UrlSet = @"http://custom-sb.azurewebsites.net/Resource/SetAB";
#endif
    public PlayerType PType;
    public bool IsSetup;
    public bool IsSetupNow;
    public bool IsPlay;

    private const string InstanceName = "VoiceManager";

    private static VoiceInformation _VoiceInformation;
    public static VoiceInformation Voice
    {
        get
        {
            if (CommonFunction.IsNull(_VoiceInformation) == false)
            {
                GameObject gm = GameObject.Find(InstanceName);
                if (CommonFunction.IsNull(gm) == true)
                {
                    GameObject m2 = new GameObject(InstanceName);
                    _VoiceInformation = m2.AddComponent<VoiceInformation>();
                }
                return _VoiceInformation;
            }
            GameObject m = new GameObject(InstanceName);
            _VoiceInformation = m.AddComponent<VoiceInformation>();

            _VoiceInformation.VoicesList = new Dictionary<PlayerType, Dictionary<VoiceType, AudioSource>>(new PlayerTypeComparer());
            _VoiceInformation.VoicesList.Add(PlayerType.UnityChan, new Dictionary<VoiceType, AudioSource>(new VoiceTypeComparer()));
            _VoiceInformation.VoicesList.Add(PlayerType.OricharChan, new Dictionary<VoiceType, AudioSource>(new VoiceTypeComparer()));
            _VoiceInformation.IsSetup = false;
            _VoiceInformation.IsSetupNow = false;
            _VoiceInformation.Volume = 0.7f;
            _VoiceInformation.IsPlay = false;
            _VoiceInformation.PType = PlayerType.UnityChan;
            return _VoiceInformation;
        }
    }

    private float _Volume;
    public float Volume
    {
        get
        {
            return _Volume;
        }
        set
        {
            _Volume = Mathf.Clamp(value, 0, 1);
            foreach (AudioSource s in VoicesList[PlayerType.UnityChan].Values)
            {
                s.volume = _Volume;
            }
            foreach (AudioSource s in VoicesList[PlayerType.OricharChan].Values)
            {
                s.volume = _Volume;
            }
        }
    }
    
    public Dictionary<PlayerType, Dictionary<VoiceType, AudioSource>> VoicesList;
    //public Dictionary<PlayerType, Dictionary<VoiceType, WebGLStreamingAudioSourceInterop>> VoiceList;

    public enum VoiceType
    {
        None,
        Attack1,
        Attack2,
        Start,
        Levelup,
        Gameover,
        Gameclear,
        Defence1,
        Defence2,
        Death,
        Sing,
        Deathblow,
        Test
    }

    void Awake()
    {
        // 自分自身だったり
        DontDestroyOnLoad(this);
    }
    

    public void Setup()
    {
        if (IsPlay == false)
        {
            return;
        }
        StartCoroutine(SetupData());
    }

    public VoiceType PlayRandomAttack()
    {
        if (CommonFunction.IsRandom(0.5f) == true)
        {
            return VoiceType.Attack1;
        }
        else
        {
            return VoiceType.Attack2;
        }
    }

    public VoiceType PlayRandomDefence()
    {
        if (CommonFunction.IsRandom(0.5f) == true)
        {
            return VoiceType.Defence1;
        }
        else
        {
            return VoiceType.Defence2;
        }
    }

    public void Play(PlayerType p,VoiceType t)
    {
        if (IsPlay == false)
        {
            return;
        }
        if (IsSetup == false)
        {
            return;
        }
        if(t == VoiceType.None)
        {
            return;
        }

        if(VoicesList[p].ContainsKey(t) == true)
        {
            VoicesList[p][t].Play();
        }
    }


    private IEnumerator SetupData()
    {
        if (IsSetup == true)
        {
            yield break;
        }
        if (IsSetupNow == true)
        {
            yield break;
        }
        IsSetupNow = true;


//        string sessionid = null;

//        string rowotp;
//        //OTPを取得
//        using (WWW www = new WWW(UrlOTP))
//        {
//            while (www.isDone == false && www.progress != 1)
//            {
//                yield return null;
//            }

//            if (CommonFunction.IsNullOrWhiteSpace(www.error) == false)
//            {
//                www.Dispose();
//                yield break;
//            }
//            yield return www;
//            rowotp = www.text;

//#if UNITY_EDITOR
//            sessionid = www.responseHeaders["SET-COOKIE"];
//#endif
//        }

//        //取得タイプを設定
//        //OTPを復号化
//        string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.ABOTP);

//        //OTPを使ってリソース名を暗号化
//        string resname = CryptInformation.EncryptString("2C67CB7EB8D54A7BAE64522D2EC424BE", otp);

//        //リソース名をエンコード
//        resname = Uri.EscapeDataString(resname);

//        Dictionary<string, string> headers = new Dictionary<string, string>();

//#if UNITY_EDITOR
//        sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
//        Regex rgx = new Regex(";.+");
//        sessionid = rgx.Replace(sessionid, "");
//        headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));

//#endif


        //アセットのロード
        using (AssetBundleInformation ab = new AssetBundleInformation())
        {
            //アセットのダウンロード
            //yield return StartCoroutine(ab.Setup(string.Format("{0}&input={1}", UrlRes, resname), 2));
            yield return StartCoroutine(ab.Setup(
                string.Format(CommonConst.SystemValue.UrlResBase, "625FCBFC962C42C189CB2A9A008C6AD0.datagz"), 4));

            #region ユニティちゃん

            //Unity攻撃1
            AddAudioSource(PlayerType.UnityChan, VoiceType.Attack1, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityAttack1.ogg"));

            //Unity攻撃2
            AddAudioSource(PlayerType.UnityChan, VoiceType.Attack2, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityAttack2.ogg"));

            //Unity開始
            AddAudioSource(PlayerType.UnityChan, VoiceType.Start, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityStart.ogg"));

            //Unityレベル
            AddAudioSource(PlayerType.UnityChan, VoiceType.Levelup, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityLevelup.ogg"));

            //Unityゲームオーバー
            AddAudioSource(PlayerType.UnityChan, VoiceType.Gameover, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityGameover.ogg"));

            //Unity踏破
            AddAudioSource(PlayerType.UnityChan, VoiceType.Gameclear, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityGameclear.ogg"));

            //Unity防御
            AddAudioSource(PlayerType.UnityChan, VoiceType.Defence1, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityDefence1.ogg"));

            //Unity防御2
            AddAudioSource(PlayerType.UnityChan, VoiceType.Defence2, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityDefence2.ogg"));

            //Unity死亡
            AddAudioSource(PlayerType.UnityChan, VoiceType.Death, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityDeath.ogg"));

            //Unity必殺技
            AddAudioSource(PlayerType.UnityChan, VoiceType.Deathblow, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityDeathblow.ogg"));

            //Unity奏でる
            AddAudioSource(PlayerType.UnityChan, VoiceType.Sing, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnitySing.ogg"));

            //Unityテスト
            AddAudioSource(PlayerType.UnityChan, VoiceType.Test, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/UnityTest.ogg"));

            #endregion ユニティちゃん

            #region オリキャラちゃん

            //Ori攻撃1
            AddAudioSource(PlayerType.OricharChan, VoiceType.Attack1, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriAttack1.ogg"));

            //Ori攻撃2
            AddAudioSource(PlayerType.OricharChan, VoiceType.Attack2, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriAttack2.ogg"));

            //Ori開始
            AddAudioSource(PlayerType.OricharChan, VoiceType.Start, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriStart.ogg"));

            //Oriレベル
            AddAudioSource(PlayerType.OricharChan, VoiceType.Levelup, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriLevelup.ogg"));

            //Oriゲームオーバー
            AddAudioSource(PlayerType.OricharChan, VoiceType.Gameover, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriGameover.ogg"));

            //Ori踏破
            AddAudioSource(PlayerType.OricharChan, VoiceType.Gameclear, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriGameclear.ogg"));

            //Ori防御
            AddAudioSource(PlayerType.OricharChan, VoiceType.Defence1, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriDefence1.ogg"));

            //Ori防御2
            AddAudioSource(PlayerType.OricharChan, VoiceType.Defence2, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriDefence2.ogg"));

            //Ori死亡
            AddAudioSource(PlayerType.OricharChan, VoiceType.Death, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriDeath.ogg"));

            //Ori必殺技
            AddAudioSource(PlayerType.OricharChan, VoiceType.Deathblow, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriDeathblow.ogg"));

            //Ori奏でる
            AddAudioSource(PlayerType.OricharChan, VoiceType.Sing, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriSing.ogg"));

            //Oriテスト
            AddAudioSource(PlayerType.OricharChan, VoiceType.Test, ab.DownloadAssetBundle<AudioClip>("Assets/Voices/OriTest.ogg"));


            #endregion オリキャラちゃん
        }

        IsSetupNow = false;
        IsSetup = true;
    }
    private void AddAudioSource(PlayerType pt, VoiceType vt, AudioClip ac)
    {
        if (CommonFunction.IsNull(ac) == false)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = ac;
            VoicesList[pt].Add(vt, a);
        }
    }
}

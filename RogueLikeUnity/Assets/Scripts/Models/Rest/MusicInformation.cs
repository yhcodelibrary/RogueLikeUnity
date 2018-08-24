using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using System;

public class MusicInformation : MonoBehaviour
{

#if UNITY_EDITOR
    //private string UrlResBase = "https://custom-sbjp.firebaseapp.com/RLUT/AssetBundle/{0}";
    //private string UrlResA = @"http://localhost:60860/Resource/TypeAA?t=MI";
    //private string UrlResB = @"http://localhost:60860/Resource/TypeAB?t=MI";
    //private string UrlResC = @"http://localhost:60860/Resource/TypeAC?t=MI";
    //private string UrlResD = @"http://localhost:60860/Resource/TypeAD?t=MI";
    //private string UrlResE = @"http://localhost:60860/Resource/TypeAE?t=MI";
    //private string UrlResF = @"http://localhost:60860/Resource/TypeAF?t=MI";
    //private string UrlResG = @"http://localhost:60860/Resource/TypeAG?t=MI";
    //private string UrlResH = @"http://localhost:60860/Resource/TypeAH?t=MI";
    //private string UrlResI = @"http://localhost:60860/Resource/TypeAI?t=MI";
    //private string UrlResJ = @"http://localhost:60860/Resource/TypeAJ?t=MI";
    //private string UrlOTP = @"http://localhost:60860/Resource/Issue";
#else
    //private string UrlParent = "";
    //private string UrlResA = @"http://custom-sb.azurewebsites.net/Resource/TypeAA?t=MI";
    //private string UrlResB = @"http://custom-sb.azurewebsites.net/Resource/TypeAB?t=MI";
    //private string UrlResC = @"http://custom-sb.azurewebsites.net/Resource/TypeAC?t=MI";
    //private string UrlResD = @"http://custom-sb.azurewebsites.net/Resource/TypeAD?t=MI";
    //private string UrlResE = @"http://custom-sb.azurewebsites.net/Resource/TypeAE?t=MI";
    //private string UrlResF = @"http://custom-sb.azurewebsites.net/Resource/TypeAF?t=MI";
    //private string UrlResG = @"http://custom-sb.azurewebsites.net/Resource/TypeAG?t=MI";
    //private string UrlResH = @"http://custom-sb.azurewebsites.net/Resource/TypeAH?t=MI";
    //private string UrlResI = @"http://custom-sb.azurewebsites.net/Resource/TypeAI?t=MI";
    //private string UrlResJ = @"http://custom-sb.azurewebsites.net/Resource/TypeAJ?t=MI";
    //private string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/Issue";

#endif

    public bool start = false;

    public MusicType Type;
    //private AudioSource AudioTitle;
    //private AudioSource AudioRogue1;
    //private AudioSource AudioGameover;
    //private AudioSource AudioGameclear;
    //private AudioSource AudioCharacterSelect;
    //private AudioSource AudioWaterSide;
    //private AudioSource AudioOldTower;
    //private Dictionary<MusicType, AudioSource> BGMs;
    private Dictionary<MusicType, AudioClip> BGMs;
    private AudioSource MainAudio;
    private Dictionary<MusicType, bool> BGMSetups;
    private MusicType PlayingType;

    private static bool isMusicOn;
    public bool IsMusicOn
    {
        get { return isMusicOn; }
        set
        {
            if(value == false)
            {
                StopBGM();
                Type = MusicType.None;
                //if (CommonFunction.IsNull(audio_background) == false)
                //{
                //    audio_background.Stop();
                //    audio_background.clip = null;
                //}
            }
            isMusicOn = value;
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
            MainAudio.volume = _Volume;
            //foreach(AudioSource s in BGMs.Values)
            //{
            //    s.volume = _Volume;
            //}
        }
    }

    
    public bool IsFadeout;
    
    private static MusicInformation _musicInformation;
    public static MusicInformation Music
    {
        get
        {
            if (CommonFunction.IsNull(_musicInformation) == false)
            {
                return _musicInformation;
            }
            GameObject m = new GameObject("MusicManager");
            _musicInformation = m.AddComponent<MusicInformation>();
            _musicInformation.BGMs = new Dictionary<MusicType, AudioClip>(new MusicTypeComparer());
            _musicInformation.MainAudio = _musicInformation.gameObject.AddComponent<AudioSource>();
            _musicInformation.MainAudio.loop = true;
            _musicInformation.BGMSetups = new Dictionary<MusicType, bool>(new MusicTypeComparer());
            foreach (MusicType val in Enum.GetValues(typeof(MusicType)))
            {
                _musicInformation.BGMSetups.Add(val, false);
            }
            _musicInformation.IsMusicOn = false;
            _musicInformation.Volume = 0.7f;
            //    IsTitleSetup = false;
            //IsRogue1Setup = false;
            //IsGameclearSetup = false;
            //IsGameoverSetup = false;
            //IsWaterSideSetup = false;
            return _musicInformation;
        }
    }

    void Awake()
    {
        // 自分自身だったり
        DontDestroyOnLoad(this);
    }

    public MusicInformation()
    {
    }
    void Start()
    {
        SetupAttach();
    }

    public void Setup(MusicType tp)
    {
        IsFadeout = false;
        if(Type != tp)
        {
            StopBGM();
        }

        Type = tp;
        if (IsMusicOn == false)
        {
            return;
        }

        switch (tp)
        {
            case MusicType.Title:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "2CB988993E92472DA6BAAAAD54005BE2.datagz"), "2CB988993E92472DA6BAAAAD54005BE2", "Assets/Musics/okaetudukumichi.ogg"));
                break;
            case MusicType.Rogue1Alpha:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "6DB1B387FE8441A5801DA8C64FAAAD4A.datagz"), "6DB1B387FE8441A5801DA8C64FAAAD4A", "Assets/Musics/koganemushi.ogg"));
                break;
            case MusicType.Gameover:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "CB8EF7D677064788A356E478468D298F.datagz"), "CB8EF7D677064788A356E478468D298F", "Assets/Musics/hinokageri.ogg"));
                break;
            case MusicType.Gameclear:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "533FB1F3A33D4A6AAFF8D88CD1893705.datagz"), "533FB1F3A33D4A6AAFF8D88CD1893705", "Assets/Musics/keijokyoku.ogg"));
                break;
            case MusicType.CharacterSelect:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "E33D2F636AD34CF59ADCD376A3D15917.datagz"), "E33D2F636AD34CF59ADCD376A3D15917", "Assets/Musics/hajimetenookashidukuri.ogg"));
                break;
            case MusicType.WaterSide:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "F29CCFDDC8684A0899FC66DBF018D49A.datagz"), "F29CCFDDC8684A0899FC66DBF018D49A", "Assets/Musics/doukeshitoshoujo.ogg"));
                break;
            case MusicType.OldTower:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "772B5690321C4C7BB25A4B643F4E1C70.datagz"), "772B5690321C4C7BB25A4B643F4E1C70", "Assets/Musics/tsukiyonofukurou.ogg"));
                break;
            case MusicType.Atelier:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "9295BAB45DC340E8AB9BA8AB8BC7FD5E.datagz"), "9295BAB45DC340E8AB9BA8AB8BC7FD5E", "Assets/Musics/akachannokoushin.ogg"));
                break;
            case MusicType.Anarchy:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "83B8A49488A441B5A3D91DA947CF3F9D.datagz"), "83B8A49488A441B5A3D91DA947CF3F9D", "Assets/Musics/noranekonosouretsu.ogg"));
                break;
            case MusicType.RTS:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "FBB4AD60D89B4F479AE5BCABD08A81B0.datagz"), "FBB4AD60D89B4F479AE5BCABD08A81B0", "Assets/Musics/yonwanousaginoodori.ogg"));
                break;
            case MusicType.FON:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "66B4EF4C09F0446F94A723D09CB7BDA2.datagz"), "66B4EF4C09F0446F94A723D09CB7BDA2", "Assets/Musics/iroasetayugure.ogg"));
                break;
            case MusicType.Boss:
                StartCoroutine(SetupDataCommon(tp, string.Format(CommonConst.SystemValue.UrlResBase, "F34E11D2EBF2469CA077C59D97641470.datagz"), "F34E11D2EBF2469CA077C59D97641470", "Assets/Musics/totsugeki.ogg"));
                break;
            default:
                
            Type = MusicType.None;
                break;
        }
    }

    private void SetupAttach()
    {
        //audio_background = (AudioSource)gameObject.AddComponent<AudioSource>();
    }
    private IEnumerator SetupDataCommon(MusicType t,string resUrl,string guid,string path)
    {
        if (BGMSetups[t] == true || BGMs.ContainsKey(t) == true)
        {
            yield break;
        }

        //BGMs.Add(t, Music.gameObject.AddComponent<AudioSource>());
        //BGMs[t].loop = true;

        //okaetudukumichi.ogg
        yield return GetAudioClip(resUrl, guid, path, t);

        BGMSetups[t] = true;

    }


    public enum MusicType
    {
        None,
        Title,
        CharacterSelect,
        Rogue1Alpha,
        Gameover,
        Gameclear,
        WaterSide,
        OldTower,
        Atelier,
        Anarchy,
        RTS,
        FON,
        Boss,
        EST
    }


    public delegate void WebGLStreamingAudioDidFinish(string baseUrl, int audio);
    
    public IEnumerator GetAudioClip(string baseUrl, string resName,string targetPath, MusicType t)
    {
        
        //アセットのロード
        using (AssetBundleInformation ab = new AssetBundleInformation())
        {
            //アセットのダウンロード
            //yield return StartCoroutine(ab.Setup(string.Format("{0}&input={1}", baseUrl, resname), 6));
            yield return StartCoroutine(ab.Setup(baseUrl, 7));

            //Music
            AudioClip ac = ab.DownloadAssetBundle<AudioClip>(targetPath);
            if (CommonFunction.IsNull(ac) == false)
            {
                BGMs.Add(t, ac);
                //source.clip = ac;                
            }
        }
    }

    private void StopBGM()
    {
        PlayingType = MusicType.None;

        MainAudio.Stop();
        //foreach (AudioSource s in BGMs.Values)
        //{
        //    s.Stop();
        //}
    }
    

    // Update is called once per frame
    void Update()
    {
        if(IsMusicOn == false)
        {
            return;
        }

        if(Type != MusicType.None)
        {
            if (PlayingType == MusicType.None)
            {
                if (BGMSetups[Type] == true && MainAudio.isPlaying == false)
                {
                    StopBGM();
                    MainAudio.clip = BGMs[Type];
                    MainAudio.Play();
                    PlayingType = Type;
                }
            }
        }
        
        
    }
    
    
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public class SoundInformation : MonoBehaviour
{
#if UNITY_EDITOR
    //private string UrlRes = @"http://localhost:60860/Resource/TypeABS?t=SI";
    //private string UrlOTP = @"http://localhost:60860/Resource/IssueAB";
    //private string UrlSet = @"http://localhost:60860/Resource/SetAB";
#else
    //private string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/TypeABS?t=SI";
    //private string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueAB";
    //private string UrlSet = @"http://custom-sb.azurewebsites.net/Resource/SetAB";
#endif
    public bool IsSetup;
    public bool IsSetupNow;
    public bool IsPlay;

    private const string InstanceName = "SoundManager";
    
    private static SoundInformation _SoundInformation;
    public static SoundInformation Sound
    {
        get
        {
            if (CommonFunction.IsNull(_SoundInformation) == false)
            {
                return _SoundInformation;
            }
            GameObject m = new GameObject(InstanceName);
            _SoundInformation = m.AddComponent<SoundInformation>();

            _SoundInformation.SoundsList = new Dictionary<SoundType, AudioSource>(new SoundTypeComparer());
            //_SoundInformation.SoundList = new Dictionary<SoundType, WebGLStreamingAudioSourceInterop>();
            _SoundInformation.IsSetup = false;
            _SoundInformation.IsSetupNow = false;
            _SoundInformation.IsPlay = false;
            _SoundInformation.Volume = 0.7f;
            return _SoundInformation;
        }
    }

    //private static GameObject _Instance;
    //public static GameObject Instance
    //{
    //    get
    //    {
    //        if (CommonFunction.IsNull(_Instance) == false)
    //        {
    //            return _Instance;
    //        }
    //        _Instance = new GameObject("Sound");
    //        Values = _Instance.AddComponent<SoundInformation>();
    //        Values.SoundsList = new Dictionary<SoundType, AudioSource>();
    //        DontDestroyOnLoad(_Instance);
    //        return _Instance;
    //    }
    //}

    //public static SoundInformation Values;

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
            foreach(AudioSource s in SoundsList.Values)
            {
                s.volume = _Volume;
            }
            //foreach (WebGLStreamingAudioSourceInterop s in SoundList.Values)
            //{
            //    CommonFunction.SetVolume(s, _Volume);
            //}
        }
    }

    //public Dictionary<SoundType, WebGLStreamingAudioSourceInterop> SoundList;
    public Dictionary<SoundType, AudioSource> SoundsList;

    public enum SoundType
    {
        None,
        AttackHit,
        AttackMiss,
        MenuOk,
        MenuMove,
        MenuCancel,
        Sing,
        Bomb,
        BucketFall,
        Throw,
        Cyclone,
        Smoke,
        Summon,
        Machinegun,
        ElectricTrap,
        ElectricMelody,
        Recover,
        Stair,
        Put,
        Putin,
        Rotation,
        GameStart,
        Levelup,
        Break,
        Equip,
        Shed,
        Shelling,
        Gun,
        Meteor,
        Alarm,
        Howling

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

    public void Play(SoundType t)
    {
        if(IsPlay == false)
        {
            return;
        }
        if(IsSetup == false)
        {
            return;
        }
        if(SoundsList.ContainsKey(t) == true)
        {
            SoundsList[t].Play();
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
        
        //取得タイプを設定
        //OTPを復号化
        //string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.ABOTP);

        ////OTPを使ってリソース名を暗号化
        //string resname = CryptInformation.EncryptString("1292D265E56E462C89D11AD2EA8AC5A6", otp);

        ////リソース名をエンコード
        //resname = Uri.EscapeDataString(resname);

        //Dictionary<string, string> headers = new Dictionary<string, string>();

#if UNITY_EDITOR
        //sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
        //Regex rgx = new Regex(";.+");
        //sessionid = rgx.Replace(sessionid, "");
        //headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));

#endif

        //アセットのロード
        using (AssetBundleInformation ab = new AssetBundleInformation())
        {
            //アセットのダウンロード
            //yield return StartCoroutine(ab.Setup(string.Format("{0}&input={1}", UrlRes, resname), 3));
            yield return StartCoroutine(ab.Setup(
                string.Format(CommonConst.SystemValue.UrlResBase, "848A7C981E414BE0BB6A4E1B7BE57F25.datagz"), 5));

            //素振り
            AddAudioSource(SoundType.AttackMiss, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/punch-swing1.ogg"));

            //攻撃ヒット
            AddAudioSource(SoundType.AttackHit, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/punch-middle2.ogg"));

            //メニュー決定
            AddAudioSource(SoundType.MenuOk, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/cursor1.ogg"));

            //メニュー移動
            AddAudioSource(SoundType.MenuMove, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/cursor4.ogg"));
     
            //メニューキャンセル
            AddAudioSource(SoundType.MenuCancel, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/cancel1.ogg"));

            //歌う
            AddAudioSource(SoundType.Sing, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/shine1.ogg"));

            //地雷
            AddAudioSource(SoundType.Bomb, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/bomb1.ogg"));

            //バケツ落下
            AddAudioSource(SoundType.BucketFall, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/hyun1.ogg"));

            //投げる
            AddAudioSource(SoundType.Throw, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/nyu1.ogg"));

            //竜巻
            AddAudioSource(SoundType.Cyclone, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-wind2.ogg"));

            //煙
            AddAudioSource(SoundType.Smoke, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-chant1.ogg"));

            //召喚陣
            AddAudioSource(SoundType.Summon, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-gravity1.ogg"));

            //銃撃
            AddAudioSource(SoundType.Machinegun, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/machinegun2.ogg"));

            //電流走る
            AddAudioSource(SoundType.ElectricTrap, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/eye-shine1.ogg"));

            //回復
            AddAudioSource(SoundType.Recover, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-status-cure1.ogg"));

            //放電の旋律
            AddAudioSource(SoundType.ElectricMelody, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-electron2.ogg"));

            //階段
            AddAudioSource(SoundType.Stair, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/dash-staircase-home-climb1.ogg"));

            //置く
            AddAudioSource(SoundType.Put, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/puyon1.ogg"));

            //入れる
            AddAudioSource(SoundType.Putin, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/touch1.ogg"));

            //回転
            AddAudioSource(SoundType.Rotation, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/whip-gesture3.ogg"));

            //ゲームスタート
            AddAudioSource(SoundType.GameStart, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/decision8.ogg"));

            //レベルアップ
            AddAudioSource(SoundType.Levelup, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-cure4.ogg"));

            //壊れる
            AddAudioSource(SoundType.Break, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/glass-crack1.ogg"));

            //装備
            AddAudioSource(SoundType.Equip, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/nyu2.ogg"));

            //弾く
            AddAudioSource(SoundType.Shed, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/katana-clash4.ogg"));

            //砲撃
            AddAudioSource(SoundType.Shelling, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/cannon2.ogg"));

            //銃
            AddAudioSource(SoundType.Gun, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/gun1.ogg"));

            //メテオストーム
            AddAudioSource(SoundType.Meteor, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/magic-flame2.ogg"));

            //アラーム
            AddAudioSource(SoundType.Alarm, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/laser1.ogg"));

            //遠吠え
            AddAudioSource(SoundType.Howling, ab.DownloadAssetBundle<AudioClip>("Assets/Sounds/lion1.ogg"));
        }

        //素振り
        //GetAudioSource(SoundType.AttackMiss, "Assets/Sounds/punch-swing1.ogg");

        ////攻撃ヒット
        //GetAudioSource(SoundType.AttackHit, "Assets/Sounds/punch-middle2.ogg");

        ////メニュー決定
        //GetAudioSource(SoundType.MenuOk, "Assets/Sounds/cursor1.ogg");

        ////メニュー移動
        //GetAudioSource(SoundType.MenuMove, "Assets/Sounds/cursor4.ogg");

        ////メニューキャンセル
        //GetAudioSource(SoundType.MenuCancel, "Assets/Sounds/cancel1.ogg");

        ////歌う
        //GetAudioSource(SoundType.Sing, "Assets/Sounds/shine1.ogg");

        ////地雷
        //GetAudioSource(SoundType.Bomb, "Assets/Sounds/bomb1.ogg");

        ////バケツ落下
        //GetAudioSource(SoundType.BucketFall, "Assets/Sounds/hyun1.ogg");

        ////投げる
        //GetAudioSource(SoundType.Throw, "Assets/Sounds/nyu1.ogg");

        ////竜巻
        //GetAudioSource(SoundType.Cyclone, "Assets/Sounds/magic-wind2.ogg");

        ////煙
        //GetAudioSource(SoundType.Smoke, "Assets/Sounds/magic-chant1.ogg");

        ////召喚陣
        //GetAudioSource(SoundType.Summon, "Assets/Sounds/magic-gravity1.ogg");

        ////銃撃
        //GetAudioSource(SoundType.Machinegun, "Assets/Sounds/machinegun2.ogg");

        ////電流走る
        //GetAudioSource(SoundType.ElectricTrap, "Assets/Sounds/eye-shine1.ogg");

        ////回復
        //GetAudioSource(SoundType.Recover, "Assets/Sounds/magic-status-cure1.ogg");

        ////放電の旋律
        //GetAudioSource(SoundType.ElectricMelody, "Assets/Sounds/magic-electron2.ogg");

        ////階段
        //GetAudioSource(SoundType.Stair, "Assets/Sounds/dash-staircase-home-climb1.ogg");

        ////置く
        //GetAudioSource(SoundType.Put, "Assets/Sounds/puyon1.ogg");

        ////入れる
        //GetAudioSource(SoundType.Putin, "Assets/Sounds/touch1.ogg");

        ////回転
        //GetAudioSource(SoundType.Rotation, "Assets/Sounds/whip-gesture3.ogg");

        ////ゲームスタート
        //GetAudioSource(SoundType.GameStart, "Assets/Sounds/decision8.ogg");

        ////レベルアップ
        //GetAudioSource(SoundType.Levelup, "Assets/Sounds/magic-cure4.ogg");

        ////壊れる
        //GetAudioSource(SoundType.Break, "Assets/Sounds/glass-crack1.ogg");

        ////装備
        //GetAudioSource(SoundType.Equip, "Assets/Sounds/nyu2.ogg");

        ////弾く
        //GetAudioSource(SoundType.Shed, "Assets/Sounds/katana-clash4.ogg");

        ////砲撃
        //GetAudioSource(SoundType.Shelling, "Assets/Sounds/cannon2.ogg", true);

        ////素振り
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.AttackMiss, "8071DAF865FF4A5DAF6E010DFBC94EBD"));

        ////攻撃ヒット
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.AttackHit, "C266E14F63384EAEBFE692CEE7652B5E"));

        ////メニュー決定
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.MenuOk, "6654602AA9B9431D9D391C30D6113D58"));

        ////メニュー移動
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.MenuMove, "83294277A60B46BEBE38A1F8740D88F2"));

        ////メニューキャンセル
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.MenuCancel, "AC950E8AB2E145F1A0292445AEC13BA3"));

        ////歌う
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Sing, "F74E532DF8184E4A931DCD1C7C303732"));

        ////地雷
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Bomb, "8C0E7B05D23A4197B833D1CBBDD7622F"));

        ////バケツ落下
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.BucketFall, "9B217080B6684BBE9E373AA970953CA9"));

        ////投げる
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Throw, "AD8593BA882B4116945B8DE99245FDD2"));

        ////竜巻
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Cyclone, "5A525DD10B2F40B5AA6B3876DD90BC2E"));

        ////煙
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Smoke, "9FA4DE1386574D75B103F5C112B7CFDC"));

        ////召喚陣
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Summon, "4342BEA80D3B4646A7B2B4353485172E"));

        ////銃撃
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Machinegun, "700D2C1A40E44A26AABEAF961FBAFB15"));

        ////電流走る
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.ElectricTrap, "BC08AC204069402F89F144FAA18D9D9B"));

        ////回復
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Recover, "002AA253073E403B9E764A0BFE0487E9"));

        ////放電の旋律
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.ElectricMelody, "A719879FF8BA4C878DF5AD04FFC63EEC"));

        ////階段
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Stair, "EBA8FA599A034B9EB593B3EA6BE4A345"));

        ////置く
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Put, "D9A2314DE2E1458F9CD9BB272D06938B"));

        ////入れる
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Putin, "16A3CA8E37F24289AF70093539004D24"));

        ////回転
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Rotation, "24B4D0AB19A84530B37D403AF7AB991D"));

        ////ゲームスタート
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.GameStart, "F9509CCD4EDB42C1AFF0D5D724AD5180"));

        ////レベルアップ
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Levelup, "D4AF353957504294A800C63834A1E5CA"));

        ////壊れる
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Break, "C75832CE0E5A4E24912131E704DBF996"));

        ////装備
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Equip, "26C1277CDD4F458DA44C80C9B3A6295C"));

        ////弾く
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Shed, "7C323455939C482FB87B0F35F7F65B6A"));

        ////砲撃
        //MainThreadDispatcher.StartUpdateMicroCoroutine(GetAudioClip(SoundType.Shelling, "CC6333031580488A836DC6B8E487C371"));

        //素振り
        //yield return StartCoroutine(GetAudioClip(SoundType.AttackMiss, "8071DAF865FF4A5DAF6E010DFBC94EBD"));

        ////攻撃ヒット
        //yield return StartCoroutine(GetAudioClip(SoundType.AttackHit, "C266E14F63384EAEBFE692CEE7652B5E"));

        ////メニュー決定
        //yield return StartCoroutine(GetAudioClip(SoundType.MenuOk, "6654602AA9B9431D9D391C30D6113D58"));

        ////メニュー移動
        //yield return StartCoroutine(GetAudioClip(SoundType.MenuMove, "83294277A60B46BEBE38A1F8740D88F2"));

        ////メニューキャンセル
        //yield return StartCoroutine(GetAudioClip(SoundType.MenuCancel, "AC950E8AB2E145F1A0292445AEC13BA3"));

        ////歌う
        //yield return StartCoroutine(GetAudioClip(SoundType.Sing, "F74E532DF8184E4A931DCD1C7C303732"));

        ////地雷
        //yield return StartCoroutine(GetAudioClip(SoundType.Bomb, "8C0E7B05D23A4197B833D1CBBDD7622F"));

        ////バケツ落下
        //yield return StartCoroutine(GetAudioClip(SoundType.BucketFall, "9B217080B6684BBE9E373AA970953CA9"));

        ////投げる
        //yield return StartCoroutine(GetAudioClip(SoundType.Throw, "AD8593BA882B4116945B8DE99245FDD2"));

        ////竜巻
        //yield return StartCoroutine(GetAudioClip(SoundType.Cyclone, "5A525DD10B2F40B5AA6B3876DD90BC2E"));

        ////煙
        //yield return StartCoroutine(GetAudioClip(SoundType.Smoke, "9FA4DE1386574D75B103F5C112B7CFDC"));

        ////召喚陣
        //yield return StartCoroutine(GetAudioClip(SoundType.Summon, "4342BEA80D3B4646A7B2B4353485172E"));

        ////銃撃
        //yield return StartCoroutine(GetAudioClip(SoundType.Machinegun, "700D2C1A40E44A26AABEAF961FBAFB15"));

        ////電流走る
        //yield return StartCoroutine(GetAudioClip(SoundType.ElectricTrap, "BC08AC204069402F89F144FAA18D9D9B"));

        ////回復
        //yield return StartCoroutine(GetAudioClip(SoundType.Recover, "002AA253073E403B9E764A0BFE0487E9"));

        ////放電の旋律
        //yield return StartCoroutine(GetAudioClip(SoundType.ElectricMelody, "A719879FF8BA4C878DF5AD04FFC63EEC"));

        ////階段
        //yield return StartCoroutine(GetAudioClip(SoundType.Stair, "EBA8FA599A034B9EB593B3EA6BE4A345"));

        ////置く
        //yield return StartCoroutine(GetAudioClip(SoundType.Put, "D9A2314DE2E1458F9CD9BB272D06938B"));

        ////入れる
        //yield return StartCoroutine(GetAudioClip(SoundType.Putin, "16A3CA8E37F24289AF70093539004D24"));

        ////回転
        //yield return StartCoroutine(GetAudioClip(SoundType.Rotation, "24B4D0AB19A84530B37D403AF7AB991D"));

        ////ゲームスタート
        //yield return StartCoroutine(GetAudioClip(SoundType.GameStart, "F9509CCD4EDB42C1AFF0D5D724AD5180"));

        ////レベルアップ
        //yield return StartCoroutine(GetAudioClip(SoundType.Levelup, "D4AF353957504294A800C63834A1E5CA"));

        ////壊れる
        //yield return StartCoroutine(GetAudioClip(SoundType.Break, "C75832CE0E5A4E24912131E704DBF996"));

        ////装備
        //yield return StartCoroutine(GetAudioClip(SoundType.Equip, "26C1277CDD4F458DA44C80C9B3A6295C"));

        ////弾く
        //yield return StartCoroutine(GetAudioClip(SoundType.Shed, "7C323455939C482FB87B0F35F7F65B6A"));

        ////砲撃
        //yield return StartCoroutine(GetAudioClip(SoundType.Shelling, "CC6333031580488A836DC6B8E487C371"));

        IsSetupNow = false;
        IsSetup = true;

    }

    //private void GetAudioSource(SoundType st, string resName, bool isUnload = false)
    //{
    //    AssetBundleInformation<AudioClip> ab = new AssetBundleInformation<AudioClip>();

    //    Observable.FromMicroCoroutine(_ => ab.DownloadAssetBundle(resName, UrlRes, 10, isUnload))
    //        .Subscribe(_ =>
    //        {
    //            AddAudioSource(st, ab.Obj);
    //        });
    //}
    private void AddAudioSource(SoundType st,AudioClip ac)
    {
        if (CommonFunction.IsNull(ac) == false)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = ac;
            SoundsList.Add(st, a);
        }
    }

    //private void GetAudioClip(SoundType st,string resName,string sessionId ,string otp)
    //{

    //    if (SoundList.ContainsKey(st) == false)
    //    {
    //        //OTPを使ってリソース名を暗号化
    //        string resname = CryptInformation.EncryptString(resName, otp);

    //        //リソース名をエンコード
    //        resname = Uri.EscapeDataString(resname);

    //        GetAudioClip(SoundList, st, resname, 1000,sessionId);
    //        //yield return StartCoroutine(GetAudioClip(resName, temp, 1000));
    //    }
    //}

    //public void GetAudioClip(Dictionary<SoundType, WebGLStreamingAudioSourceInterop> list, SoundType st, string resname, double stoptime, string sessionid)
    //{

    //    //string rowotp = GetOTP(out sessionid);

    //    //セッションIDを抽出
    //    //        Dictionary<string, string> headers = new Dictionary<string, string>();
    //    //#if UNITY_EDITOR
    //    //        sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
    //    //        Regex rgx = new Regex(";.+");
    //    //        sessionid = rgx.Replace(sessionid, "");
    //    //        headers.Add("Cookie",string.Format("ASP.NET_SessionId={0};", sessionid));
    //    ////#endif

    //    ////OTPを復号化
    //    //string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.SDOTP);

    //    //OTPを使ってリソース名を暗号化
    //    //string resname = CryptInformation.EncryptString(resName, otp);

    //    WebGLStreamingAudioSourceInterop clip = (new WebGLStreamingAudioSourceInterop(
    //        string.Format("{0}?input={1}&a.ogg", UrlRes, resname), GameObject.Find(InstanceName), sessionid, stoptime));

    //    if (CommonFunction.IsNull(clip) == false
    //        && list.ContainsKey(st) == false)
    //    {
    //        clip.Volume = Volume;
    //        list.Add(st, clip);
    //    }
    //}
}

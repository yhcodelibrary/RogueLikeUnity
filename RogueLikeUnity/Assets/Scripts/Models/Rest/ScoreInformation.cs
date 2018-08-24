using Assets.Scripts.Extend;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScoreInformation
{
    public static void InitializeScoreInformation()
    {
        Info.Score = 0;

        Info.Floor = 1;
        Info.iLevel = 1;

        Info.isDisrupt = false;
        info.CauseDeath = "-";
        Info.CauseDeathType = DeathCouseType.None;
        Info.EnemyObjNo = null;
        Info.MostUseWeaponDamage = 0;
        Info.MostUseWeaponName = CommonConst.Message.BareHands;
        Info.MostUseSieldDamage = 0; ;
        Info.MostUseSieldName = "-";
        Info.MostUseRingDamage = 0; ;
        Info.MostUseRingName = "-";
    }
    private static ScoreInformation info;
    public static ScoreInformation Info
    {
        get
        {
            if (CommonFunction.IsNull(info) == false)
            {
                return info;
            }
            info = new ScoreInformation();
            return info;
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        if(Score < 0)
        {
            Score = 0;
        }
    }

    public bool isDisrupt;
    public long TotalTime;
    public string PlayerName;
    public string PlayerNameBase;
    public string DungeonName;
    public ushort Floor;
    public int iLevel;
    public int iDungeonId;

    /// <summary>
    /// 与えたダメージ
    /// 階層移動　階層*100
    /// 状態異常を発生 +10
    /// アイテムを拾う +1
    /// </summary>
    public float Score { get; private set; }

    public string CauseDeath;
    public DeathCouseType CauseDeathType;
    public Nullable<long> EnemyObjNo;

    public int MostUseWeaponDamage;
    public string MostUseWeaponName;
    public int MostUseSieldDamage;
    public string MostUseSieldName;
    public int MostUseRingDamage;
    public string MostUseRingName;

    public IEnumerator SendScoreCorutine(string json)
    {

#if UNITY_EDITOR
        const string UrlRes = @"http://localhost:60860/Resource/SCR";
        const string UrlOTP = @"http://localhost:60860/Resource/IssueSC";
        //const string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/SCR";
        //const string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueSC";
#else
    //const string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/SCR";
    //const string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueSC";
    const string UrlRes = @"http://custom-sb-rc.azurewebsites.net/Resource/SCR";
    const string UrlOTP = @"http://custom-sb-rc.azurewebsites.net/Resource/IssueSC";
#endif

        //OTPを取得
        //        WWW www = new WWW(UrlOTP);

        //        while (www.isDone == false && www.progress != 1)
        //        {
        //            yield return null;
        //        }

        //        if (CommonFunction.IsNull(www.error) == false)
        //        {
        //            www.Dispose();
        //            yield break;
        //        }

        //        string rowotp = www.text;

        //        string sessionid = null;
        //#if UNITY_EDITOR
        //        sessionid = www.responseHeaders["SET-COOKIE"];
        //#endif
        //        www.Dispose();

        //        //OTPを復号化
        //        string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.SCOTP);

        //        //OTPを使ってリソース名を暗号化
        //        string ecjson = CryptInformation.EncryptString(json, otp);
        string ecjson = CryptInformation.EncryptString(json, CommonConst.CryptKey.SCOTP);

        Dictionary<string, string> headers = new Dictionary<string, string>();

#if UNITY_EDITOR
        //sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
        //Regex rgx = new Regex(";.+");
        //sessionid = rgx.Replace(sessionid, "");
        //headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));
#endif

        WWWForm form = new WWWForm();
        form.AddField("input", ecjson);

#if UNITY_EDITOR
        WWW www = new WWW(UrlRes, form.data , headers);
#else
        headers = form.headers;
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept");
        headers.Add("Access-Control-Allow-Methods", "POST");
        headers.Add("Access-Control-Allow-Origin", "*");
        WWW www = new WWW(UrlRes, form.data , headers);
#endif

        while (www.isDone == false && www.progress != 1)
        {
            yield return null;
        }
        www.Dispose();
    }

    public string GetJson()
    {
        T_RLU_ClearScore i = new T_RLU_ClearScore();
        i.vcVersion = CommonConst.SystemValue.CurrentVersion;

        i.vcGameId = GameStateInformation.GameId.ToString("D");
        i.isDisrupt = isDisrupt;
        i.vcCharacterName = PlayerName;
        i.iScore = (long)Score;
        i.iTotalTime = TotalTime;
        i.vcCouse = CauseDeath;
        i.iCouse = (int)CauseDeathType ;
        i.iEnemyObjNo = EnemyObjNo;
        i.vcDungeonName = DungeonName;
        i.iPlayerObjNo = PlayerInformation.Info.ObjNo;
        i.iFloor = Floor;
        i.iLevel = iLevel;
        i.iDungeonId = iDungeonId;
        i.vcWeaponName = MostUseWeaponName;
        i.vcShieldName = MostUseSieldName;
        i.vcRingName = MostUseRingName;

        string json = JsonMapper.ToJson(i);

        return json;
    }

    private partial class T_RLU_ClearScore
    {
        public string vcGameId { get; set; }
        public string vcVersion { get; set; }
        public string vcCharacterName { get; set; }
        public bool isDisrupt { get; set; }
        public long iTotalTime { get; set; }
        public long iScore { get; set; }
        public int iFloor { get; set; }
        public int iLevel { get; set; }
        public int iDungeonId { get; set; }
        public string vcDungeonName { get; set; }
        public string vcCouse { get; set; }
        public string vcWeaponName { get; set; }
        public string vcShieldName { get; set; }
        public string vcRingName { get; set; }
        public Nullable<int> iCouse { get; set; }
        public Nullable<long> iPlayerObjNo { get; set; }
        public Nullable<long> iEnemyObjNo { get; set; }
    }
}

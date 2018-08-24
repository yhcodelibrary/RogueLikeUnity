using Assets.Scripts.Extend;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class DungeonHistoryInformation
{

    public static void InitializeDungeonHistoryInformation()
    {

        Info.iDungeonId = 1;
        Info.iFloor = 1;

        Info.vcPlayerName = "";
        info.iWeaponDamage = 0;
        Info.iEnemyBastardCount = 0;
        Info.iTrapInvokeCount = 0;
        Info.iCurrentLevel = 0;
        Info.iCurrentHp = 0;
        Info.iTurn = 0;
    }
    private static DungeonHistoryInformation info;
    public static DungeonHistoryInformation Info
    {
        get
        {
            if (CommonFunction.IsNull(info) == false)
            {
                return info;
            }
            info = new DungeonHistoryInformation();
            return info;
        }
    }

    public int iDungeonId;
    public int iFloor;
    public string vcPlayerName;
    public int iWeaponDamage;
    public int iEnemyBastardCount;
    public int iTrapInvokeCount;
    public int iCurrentLevel;
    public int iCurrentHp;
    public int iTurn;
    public string vcCharacterName;

    public IEnumerator SendDungeonHistoryCorutine(string json)
    {

#if UNITY_EDITOR
        const string UrlRes = @"http://custom-sb-rc.azurewebsites.net/Resource/DHR";
        const string UrlOTP = @"http://custom-sb-rc.azurewebsites.net/Resource/IssueDH";
        //const string UrlRes = @"http://localhost:60860/Resource/DHR";
        //const string UrlOTP = @"http://localhost:60860/Resource/IssueDH";
#else
    //const string UrlRes = @"http://custom-sb.azurewebsites.net/Resource/DHR";
    //const string UrlOTP = @"http://custom-sb.azurewebsites.net/Resource/IssueDH";
    const string UrlRes = @"http://custom-sb-rc.azurewebsites.net/Resource/DHR";
    const string UrlOTP = @"http://custom-sb-rc.azurewebsites.net/Resource/IssueDH";
#endif

        Dictionary<string, string> headers = new Dictionary<string, string>();
        //        headers.Add("Access-Control-Allow-Credentials", "true");
        //        //OTPを取得
        //        WWW www = new WWW(UrlOTP, null, headers);

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

        //        //foreach (string s in www.responseHeaders.Keys)
        //        //{
        //        //    DisplayInformation.Info.AddMessage(string.Format("{0}:{1}", s, www.responseHeaders[s]));
        //        //}
        //#if UNITY_EDITOR
        //        sessionid = www.responseHeaders["SET-COOKIE"];
        //#endif
        //        www.Dispose();

        //        //OTPを復号化
        //        string otp = CryptInformation.DecryptString(rowotp, CommonConst.CryptKey.DHOTP);

        //        //OTPを使ってリソース名を暗号化
        //        string ecjson = CryptInformation.EncryptString(json, otp);
        string ecjson = CryptInformation.EncryptString(json, CommonConst.CryptKey.DHOTP);


#if UNITY_EDITOR
        //sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
        //Regex rgx = new Regex(";.+");
        //sessionid = rgx.Replace(sessionid, "");
        //headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));
#endif

        WWWForm form = new WWWForm();
        form.AddField("input", ecjson);

#if UNITY_EDITOR
        WWW www = new WWW(UrlRes, form.data, headers);
#else
        headers = form.headers;
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept");
        headers.Add("Access-Control-Allow-Methods", "POST");
        headers.Add("Access-Control-Allow-Origin", "*");
        WWW www = new WWW(UrlRes, form.data, headers);
#endif

        while (www.isDone == false && www.progress != 1)
        {
            yield return null;
        }
        www.Dispose();
    }
    
    public string GetJson()
    {
        T_RLU_DungeonSearchHistory i = new T_RLU_DungeonSearchHistory();
        i.vcVersion = CommonConst.SystemValue.CurrentVersion;

        i.vcGameId = GameStateInformation.GameId.ToString("D");
        i.iCurrentLevel = DisplayInformation.Info.Level;
        i.iCurrentHp = Mathf.CeilToInt(DisplayInformation.Info.HpValue);
        i.vcPlayerName = vcPlayerName;
        i.iDungeonId = iDungeonId;
        i.iFloor = iFloor;
        i.iPlayerObjNo = PlayerInformation.Info.ObjNo;
        i.iWeaponDamage = iWeaponDamage;
        i.iEnemyBastardCount = iEnemyBastardCount;
        i.iTrapInvokeCount = iTrapInvokeCount;
        i.iTurn = iTurn;
        i.vcCharacterName = vcCharacterName;

        string json = JsonMapper.ToJson(i);

        return json;
    }
    


    public partial class T_RLU_DungeonSearchHistory
    {
        public string vcGameId { get; set; }
        public string vcVersion { get; set; }
        public int iDungeonId { get; set; }
        public int iFloor { get; set; }
        public string vcPlayerName { get; set; }
        public int iWeaponDamage { get; set; }
        public int iEnemyBastardCount { get; set; }
        public int iTrapInvokeCount { get; set; }
        public int iCurrentLevel { get; set; }
        public int iCurrentHp { get; set; }
        public int iTurn { get; set; }
        public Nullable<int> iCouse { get; set; }
        public Nullable<long> iPlayerObjNo { get; set; }

        public string vcCharacterName { get; set; }
    }
}

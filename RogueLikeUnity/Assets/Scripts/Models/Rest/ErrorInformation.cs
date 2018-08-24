using Assets.Scripts.Extend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ErrorInformation
{
#if UNITY_EDITOR
    const string UrlRes = @"http://localhost:60860/Unity/Resource/EL";
#else

    const string UrlRes = @"http://custom-sb-rc.azurewebsites.net/Unity/Resource/EL";
#endif

    private static ErrorInformation info;
    public static ErrorInformation Info
    {
        get
        {
            if (CommonFunction.IsNull(info) == false)
            {
                return info;
            }
            info = new ErrorInformation();
            return info;
        }
    }

    private bool IsOneTime;
    public void Initialize()
    {
        IsOneTime = false;
    }


    public IEnumerator SendLogCorutine(Exception e)
    {
        yield return SendLogCorutine(e, "");
    }
    public IEnumerator SendLogCorutine(Exception e,string state)
    {
        if(IsOneTime == true)
        {
            yield break;
        }

        IsOneTime = true;


        string otp = CommonConst.CryptKey.ErrorLog;

        T_RLU_ErrorLog el = new T_RLU_ErrorLog();
#if UNITY_EDITOR
        el.vcVersion = "0.01";
#else
        el.vcVersion = CommonConst.SystemValue.CurrentVersion;
#endif
        el.vcException = CommonFunction.CutString(e.GetType().ToString(), 100);
        el.vcMessage = CommonFunction.CutString(e.Message, 200);
        el.vcStackTrace = CommonFunction.CutString(state + e.StackTrace, 4000);
        

        string json = JsonMapper.ToJson(el);

        //OTPを使ってリソース名を暗号化
        string ecjson = CryptInformation.EncryptString(json, otp);
        
        Dictionary<string, string> headers = new Dictionary<string, string>();

//#if UNITY_EDITOR
//        sessionid = sessionid.Replace("ASP.NET_SessionId=", "");
//        Regex rgx = new Regex(";.+");
//        sessionid = rgx.Replace(sessionid, "");
//        headers.Add("Cookie", string.Format("ASP.NET_SessionId={0};", sessionid));
//#endif

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


    public partial class T_RLU_ErrorLog
    {
        public string vcVersion { get; set; }
        public string vcException { get; set; }
        public string vcMessage { get; set; }
        public string vcStackTrace { get; set; }
        //public Nullable<System.DateTime> dtRgstTime { get; set; }
        //public string vcRgstUser { get; set; }
        //public string vcRgstPgm { get; set; }
        //public Nullable<System.DateTime> dtLastUpdTime { get; set; }
        //public string vcLastUpdUser { get; set; }
        //public string vcLastUpdPgm { get; set; }
        //public bool isEffectiveFlg { get; set; }
        //public byte[] tsUpdate { get; set; }
    }
}

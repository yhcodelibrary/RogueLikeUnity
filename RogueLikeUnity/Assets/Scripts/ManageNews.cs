using Assets.Scripts.Extend;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ManageNews : MonoBehaviour
    {

#if UNITY_EDITOR
        const string UrlNews = @"http://localhost:60860/Resource/NR";
        //const string UrlNews = @"http://custom-sb-rc.azurewebsites.net/Resource/NR";
#else
        const string UrlNews = @"http://custom-sb-rc.azurewebsites.net/Resource/NR";
#endif
        public static ManageNews News;
        private static CanvasGroup cvGroup;
        private static Text NewsText;

        public static void Setup()
        {
            if (CommonFunction.IsNull(News) == true)
            {
                GameObject cv = GameObject.Find("NewsCanvas");
                if (CommonFunction.IsNull(cv) == true)
                {
                    return;
                }
                cvGroup = cv.GetComponent<CanvasGroup>();
                News = cv.gameObject.AddComponent<ManageNews>();
                NewsText = cv.transform.Find("NewsMessageArea/NewsMessageText").GetComponent<Text>();

                DontDestroyOnLoad(cv);
            }
            NewsText.text = "";

            News.IsFirst = true;
            News.IsRealtime = true;
        }

        public static void Setdown()
        {
            News.IsRealtime = false;
        }

        public bool IsRealtime;
        public IDisposable NewsCheckCoroutine;
        public int NewsNumber;
        public bool IsFirst;
        private float waitframe;


        private void Update()
        {
            if (IsRealtime == false)
            {
                return;
            }
            //waitframe += CommonFunction.GetDelta(1);

            //30秒に一回起動
            //if (waitframe > 30 && CommonFunction.IsNull(NewsCheckCoroutine) == true)
            if (waitframe < Time.time && CommonFunction.IsNull(NewsCheckCoroutine) == true)
            {

                MainThreadDispatcher.StartUpdateMicroCoroutine(CheckNews());
                //waitframe = 0;
                waitframe = Time.time + 30;
            }
        }
        
        private IEnumerator CheckNews()
        {

            T_RLU_NewsHistory input = new T_RLU_NewsHistory();
            input.iSequence = NewsNumber;
            input.vcVersion = CommonConst.SystemValue.CurrentVersion;
            input.vcGameId = GameStateInformation.GameId.ToString("D");

            string json = JsonMapper.ToJson(input);
            //jsonを暗号化
            string ecjson = CryptInformation.EncryptString(json, CommonConst.CryptKey.GetNews);

            //ニュースを取得
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

#else
        headers = form.headers;
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept");
        headers.Add("Access-Control-Allow-Methods", "POST");
        headers.Add("Access-Control-Allow-Origin", "*");
#endif

            //WWW www = new WWW(UrlNews, form.data, headers);

            //while (www.isDone == false && www.progress != 1)
            //{
            //    yield return null;
            //}
            //yield return www;

            var www = ObservableWWW.Post(UrlNews, form.data, headers).ToYieldInstruction();
            while (!(www.HasResult || www.IsCanceled || www.HasError)) // 3つもプロパティ並べるのダルいので次回アップデートでIsDoneを追加します予定
            {
                yield return null;
            }

            string result = "";
            if (www.HasResult == true && CommonFunction.IsNullOrWhiteSpace(www.Result) == false)
            {
                result = www.Result;
            }
            
            //if(CommonFunction.IsNullOrWhiteSpace(www.error) == true)
            //{
            //    result = www.text;
            //}

            www.Dispose();

            yield return null;

            //yield return Observable.FromMicroCoroutine(() => DisplayNews(result)).Subscribe();
            MainThreadDispatcher.StartUpdateMicroCoroutine(DisplayNews(result));
            //yield return  StartCoroutine(DisplayNews(result));

            NewsCheckCoroutine = null;
        }

        private IEnumerator DisplayNews(string result)
        {
            const float interval = 1f;

            if(string.IsNullOrEmpty(result) == true)
            {
                yield break;
            }

            //jsonを復号化
            string decjson = CryptInformation.DecryptString(result, CommonConst.CryptKey.DecNews);

            T_RLU_NewsHistory model = JsonMapper.ToObject<T_RLU_NewsHistory>(decjson);
            if(CommonFunction.IsNull(model) == true)
            {
                yield break;
            }

            NewsNumber = model.iSequence;

            if(IsFirst == true)
            {
                IsFirst = false;
                yield break;
            }

            NewsText.text = CreateNews(model);
            
            float time = 0;
            
            //だんだん明るく
            time = 0;
            while (time <= interval)
            {
                cvGroup.alpha = Mathf.Clamp((time / interval), 0f, 1f);

                time += CommonFunction.GetDelta(1);
                yield return null;
            }

            time = 0;
            //5秒待つ
            while (time <= 5)
            {
                time += CommonFunction.GetDelta(1);
                yield return null;
            }

            //だんだん暗く
            time = 0;
            while (time <= interval)
            {
                cvGroup.alpha = Mathf.Clamp(1 - (time / interval), 0f, 1f);
                time += CommonFunction.GetDelta(1);
                yield return null;
            }

            cvGroup.alpha = 0;
        }

        private string CreateNews(T_RLU_NewsHistory model)
        {
            string result;

            DungeonInformation duninfo = TableDungeonMaster.GetValue(model.iDungeonObjNo);

            switch (model.iActionNo)
            {
                //ダンジョン挑戦
                case NewsType.DungeonStart:
                    result = string.Format("news:<color={0}>{1}</color>が<color={2}>{3}</color>に挑戦開始",
                        CommonConst.Color.Player,
                        model.vcCharacterName,
                        CommonConst.Color.Dungeon,
                        duninfo.Name);
                    break;
                //ダンジョン進行
                case NewsType.DungeonNext:
                    result = string.Format("news:<color={0}>{1}</color>が<color={2}>{3}</color>{4}Fに到達",
                        CommonConst.Color.Player,
                        model.vcCharacterName,
                        CommonConst.Color.Dungeon,
                        duninfo.Name,
                        model.iFloor);
                    break;
                //踏破失敗
                case NewsType.DisruptFail:

                    result = string.Format("news:<color={0}>{1}</color>が<color={5}>{6}</color>にて<color={2}>{3}</color>{4}Fで踏破失敗",
                        CommonConst.Color.Player,
                        model.vcCharacterName,
                        CommonConst.Color.Dungeon,
                        duninfo.Name,
                        model.iFloor,
                        CommonConst.Color.DeathCause,
                        model.vcCouse);
                    break;

                //踏破成功
                case NewsType.DisruptSuccess:
                    result = string.Format("news:<color={0}>{1}</color>が<color={2}>{3}</color>の踏破成功",
                        CommonConst.Color.Player,
                        model.vcCharacterName,
                        CommonConst.Color.Dungeon,
                        duninfo.Name,
                        model.iFloor);

                    break;

                default:
                    result = "";
                    break;
            }

            return result;
        }
    }

    public class T_RLU_NewsHistory
    {
        public int iSequence;
        public string vcVersion;
        public NewsType iActionNo;
        public Nullable<int> iActionSubNo;
        public string vcCouse;
        public string vcCharacterName;
        public string vcGameId;
        public int iDungeonObjNo;
        public int iFloor;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ManageDisplay : MonoBehaviour
{
    //public GUIStyle StyleTitle = new GUIStyle();
    //public GUIStyle StyleTitleBack = new GUIStyle();
    //public bool VerticleHealthBar;
    //public Texture HealthBubbleTexture;
    //public Texture HealthTexture;
    //public float HealthBubbleTextureRotation;
    //public Rect HealthBarDimens;
    //private HealthSystem health_bar;
    private Text floorText;
    private Text levelText;
    private GameObject levelExBar;
    private GameObject hpBar;
    private Text hpText;
    private GameObject satietyBar;
    private Text satietyText;
    private GameObject powerBar;
    private Text powerText;
    private GameObject messageArea;
    private GameObject messageTextScrollView;
    private GameObject messageTextContent;
    private GameObject messageText;
    private List<GameObject> Messages;
    private ScrollRect MessageScrollRect;

    private float MessageAreaHeight;

    private const float levelBarMax = 125;
    private const float levelBarHeight = 16;
    private const float hpBarMax = 250;
    private const float hpBarHeight = 18;
    private const float satietyBarMax = 250;
    private const float satietyBarHeight = 18;
    private const float powerBarMax = 125;
    private const float powerBarHeight = 18;
    // public static DisplayInformation info = new DisplayInformation(); 

    public void Start()
    {
        Messages = new List<GameObject>();
        floorText = GameObject.Find("FloorValue").GetComponent<Text>();
        levelText = GameObject.Find("LevelValue").GetComponent<Text>();
        levelExBar = GameObject.Find("LvExpBar");
        hpBar = GameObject.Find("HpBar");
        hpText = GameObject.Find("HpValue").GetComponent<Text>();
        satietyBar = GameObject.Find("SatietyBar");
        satietyText = GameObject.Find("SatietyValue").GetComponent<Text>();
        powerBar = GameObject.Find("PowerBar");
        powerText = GameObject.Find("PowerValue").GetComponent<Text>();
        messageText = GameObject.Find("MessageText");
        messageArea = GameObject.Find("MessageArea");
        messageTextContent = GameObject.Find("MessageTextContent");
        messageTextScrollView = GameObject.Find("MessageTextScrollView");
        MessageScrollRect =  messageTextScrollView.GetComponent<ScrollRect>();

        MessageAreaHeight = messageTextScrollView.GetComponent<RectTransform>().sizeDelta.y;

        GameObject.Find("FloorUnit").GetComponent<Text>().text = CommonConst.Message.Floor;
        GameObject.Find("SatietyHeader").GetComponent<Text>().text = CommonConst.Message.Sat;

        messageArea.SetActive(false);


    }


    public void Update()
    {
        //メッセージが表示中なら
        if(DisplayInformation.Info.IsNowDisplayMessage == true)
        {

            SetMessageLogScroll();
            //TimeSpan span = DisplayInformation.Info.MessageUpdateTimestamp.Elapsed;
            DisplayInformation.Info.MessageUpdateTimestamp += CommonFunction.GetDelta(1);
            //表示終了時間に達したら
            //if (span.TotalMilliseconds > CommonConst.Message.DisplayTime)
            if (DisplayInformation.Info.MessageUpdateTimestamp > CommonConst.Message.DisplayTime)
            {
                //時間計測を止めてメッセージ欄を消す
                DisplayInformation.Info.EndMessageDisplay();

                int cnt = messageTextContent.transform.childCount;
                for (int i = 0; i < cnt; ++i)
                {
                    DestroyMessageObject(messageTextContent.transform.GetChild(0).gameObject);
                }
                //foreach (Transform child in messageTextContent.transform)
                //{
                //    DestroyMessageObject(child.gameObject);
                //    //Destroy(child.gameObject);
                //}
                //if (messageTextContent.transform.childCount > 10)
                //{
                //    int i = 0;
                //    foreach (Transform child in messageTextContent.transform)
                //    {
                //        Destroy(child.gameObject);
                //        // gameObjectの子オブジェクト(child)を操作できる
                //        // 子だけで、孫(子の子)は含まれない
                //        //if (i < messageTextContent.transform.childCount - 4)
                //        //{
                //        //    Destroy(child.gameObject);
                //        //    //child.gameObject.SetActive(false);
                //        //}
                //        //i++;
                //    }
                //}

                messageArea.SetActive(false);
            }
        }

        //表示更新フラグが設定されていなければ更新しない
        if (DisplayInformation.Info.IsUpdate == false)
        {
            return;
        }

        //フロアの更新
        if (DisplayInformation.Info.IsChangeFloor(DisplayInformation.InfoClone.Floor))
        {
            floorText.text = DisplayInformation.Info.Floor.ToString();
        }

        //LVの更新
        if (DisplayInformation.Info.IsChangeLevel(DisplayInformation.InfoClone.Level))
        {
            levelText.text = DisplayInformation.Info.Level.ToString();
        }

        //経験値の更新
        if (DisplayInformation.Info.IsChangeExp(DisplayInformation.InfoClone.LvExpMax, DisplayInformation.InfoClone.LvExpValue))
        {
            float exp = (float)DisplayInformation.Info.LvExpValue / DisplayInformation.Info.LvExpMax;
            levelExBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Ceil(levelBarMax * (exp >= 1 ? 1 : exp)), levelBarHeight);
            
        }

        //HPの更新
        if (DisplayInformation.Info.IsChangeHp(DisplayInformation.InfoClone.HpMax, DisplayInformation.InfoClone.HpValue))
        {
            hpBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(hpBarMax * (Mathf.Ceil(DisplayInformation.Info.HpValue) / DisplayInformation.Info.HpMax), hpBarHeight);
            hpText.text = string.Format(CommonConst.Format.PerNumber, Mathf.Ceil(DisplayInformation.Info.HpValue), DisplayInformation.Info.HpMax);
            
        }

        //満腹度の更新
        if (DisplayInformation.Info.IsChangeSat(DisplayInformation.InfoClone.SatietyMax, DisplayInformation.InfoClone.SatietyValue))
        {
            satietyBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(satietyBarMax * (Mathf.Ceil(DisplayInformation.Info.SatietyValue) / DisplayInformation.Info.SatietyMax), satietyBarHeight);
            satietyText.text = string.Format(CommonConst.Format.PerNumber, Mathf.Ceil(DisplayInformation.Info.SatietyValue), DisplayInformation.Info.SatietyMax);
        }
        //力の更新
        if (DisplayInformation.Info.IsChangePower(DisplayInformation.InfoClone.PowerMax, DisplayInformation.InfoClone.PowerValue))
        {
            powerBar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(powerBarMax * (Mathf.Ceil(DisplayInformation.Info.PowerValue) / DisplayInformation.Info.PowerMax), powerBarHeight);
            powerText.text = string.Format(CommonConst.Format.PerNumber, Mathf.Ceil(DisplayInformation.Info.PowerValue), DisplayInformation.Info.PowerMax);
        }

        DisplayInformation.InfoClone.SetCloneValue(DisplayInformation.Info);

        //メッセージ更新フラグが立っているとき
        if (DisplayInformation.Info.IsUpdateMessage == true)
        {
            //メッセージにたまっている最後の部分を表示する
            //StringBuilder sb = new StringBuilder();


            for (int i = 0; i < DisplayInformation.Info.DisplayMessages.Count; i++)
            {
                DisplayInformation.Info.DisplayMessagesHistory.Add(DisplayInformation.Info.DisplayMessages[i]);
                //sb.AppendLine(DisplayInformation.Info.DisplayMessages[i]);
                //GameObject gm = GameObject.Instantiate(messageText, messageTextContent.transform);
                GameObject gm = GetMessageObject();
                gm.GetComponent<Text>().text = DisplayInformation.Info.DisplayMessages[i];
            }

            DisplayInformation.Info.DisplayMessages.Clear();

            //履歴が一定数を超えたら削除
            if(DisplayInformation.Info.DisplayMessagesHistory.Count > 200)
            {
                int index = 0;
                for (int i = 200; i < DisplayInformation.Info.DisplayMessagesHistory.Count - 1; i++)
                {
                    DisplayInformation.Info.DisplayMessagesHistory.RemoveAt(index);
                }
            }

            messageArea.SetActive(true);
            //messageText.text = sb.ToString();

            DisplayInformation.Info.StartMessageDisplay();
        }
        //更新フラグを切る
        DisplayInformation.Info.OffUpdate();

        //health_bar.Update();
    }

    private GameObject GetMessageObject()
    {
        GameObject gm = Messages.Find(i => i.activeSelf == false);
        if(CommonFunction.IsNull(gm)==false)
        {
            gm.SetActive(true);
            gm.transform.SetParent(messageTextContent.transform);
            return gm;
        }
        gm = GameObject.Instantiate(messageText, messageTextContent.transform);
        Messages.Add(gm);
        return gm;
    }
    private void DestroyMessageObject(GameObject gm)
    {
        gm.transform.SetParent(messageTextScrollView.transform);
        gm.SetActive(false);
    }

    private void SetMessageLogScroll()
    {
        CommonFunction.SetActive(messageArea, true);

        //以下スクロールの設定
        float contentHeight = messageTextContent.GetComponent<RectTransform>().sizeDelta.y;
        // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
        if (contentHeight <= MessageAreaHeight)
        {
            DisplayInformation.Info.StartTimestamp();
            //if (DisplayInformation.Info.MessageUpdateTimestamp.IsRunning == false)
            //{
            //    DisplayInformation.Info.MessageUpdateTimestamp.Reset();
            //    DisplayInformation.Info.MessageUpdateTimestamp.Start();
            //}
            return;
        }
        
        int n = messageTextContent.transform.childCount;
        
        //現在の高さ総計
        float loc = n * 30f;
        float unit = CommonFunction.GetDelta(120f / loc);

        MessageScrollRect.verticalNormalizedPosition = Mathf.Clamp(
            MessageScrollRect.verticalNormalizedPosition - unit, 0, 1);
        if(MessageScrollRect.verticalNormalizedPosition >= unit)
        {
            DisplayInformation.Info.ResetTimestamp();
            //DisplayInformation.Info.MessageUpdateTimestamp.Reset();
            //DisplayInformation.Info.MessageUpdateTimestamp.Start();

        }
        else
        {

            if (n > 100)
            {
                for (int i = 0; i < n - 5; ++i)
                {
                    DestroyMessageObject(messageTextContent.transform.GetChild(0).gameObject);
                }
                MessageScrollRect.SetLayoutVertical();
                MessageScrollRect.verticalNormalizedPosition = 0;
            }
        }

    }

}


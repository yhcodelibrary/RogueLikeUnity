using Rei.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommonFunction {

    private static MersenneTwister _rnd { get; set; }

    public static MersenneTwister Rnd
    {
        get
        {
            if (_rnd == null)
            {
                _rnd = new MersenneTwister((int)(DateTime.Now.Ticks % Int32.MaxValue));
            }
            return _rnd;
        }
    }

    public static int GetSeed()
    {
        return (int)(DateTime.Now.Ticks % Int32.MaxValue);
    }

    public static void SetSeed(int seed)
    {
        _rnd = new MersenneTwister(seed);
        UnityEngine.Random.InitState(seed);
    }

    /// <summary>
    /// 0-1までの数値を入れてそれを超えるかどうか判定
    /// 失敗はfalse
    /// </summary>
    /// <param name="prob"></param>
    /// <returns></returns>
    public static bool IsRandom(float prob)
    {
        
        //百分率を出す
        float result = (float)Rnd.NextUInt32() / uint.MaxValue;

        //結果を判定
        if (result > prob)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// startからend-1までの整数を返す
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int Range(int start ,int end)
    {
        //0-endの整数をだす 0-6
        int result = (int)(Rnd.NextUInt32() % end);

        //startを入れる
        result = start + result;

        //endで再計算
        return result % end;
    }

    /// <summary>
    /// 0-1の乱数を得る
    /// </summary>
    /// <returns></returns>
    public static uint GetRandomUInt32()
    {
        return Rnd.NextUInt32();
    }

    /// <summary>
    /// 0-1の乱数を得る
    /// </summary>
    /// <returns></returns>
    public static uint GetRandomUInt32(uint max)
    {
        return Rnd.NextUInt32() % max;
    }

    /// <summary>
    /// 0-1の乱数を得る
    /// </summary>
    /// <returns></returns>
    public static float GetRandomValue()
    {
        return (float)Rnd.NextUInt32() / uint.MaxValue;
    }

    /// <summary>
    /// 重みと初期確率から収束した数値を受け取る
    /// </summary>
    /// <param name="start"></param>
    /// <param name="firstprob"></param>
    /// <param name="con"></param>
    /// <returns></returns>
    public static int ConvergenceRandom(int start,float firstprob,float con, int max = 20)
    {
        if(IsRandom(firstprob) == true && start < max)
        {
            return ConvergenceRandom(start + 1, firstprob / con, con, max);
        }
        return start;
    }

    /// <summary>
    /// 指定された文字列が null または空であるか、空白文字だけで構成されているかどうかを返します
    /// </summary>
    public static bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim() == "";
    }

    /// <summary>
    /// 重みと初期確率から収束した確率での結果を取得する
    /// </summary>
    /// <param name="start"></param>
    /// <param name="firstprob"></param>
    /// <param name="con"></param>
    /// <returns></returns>
    public static bool IsConvergenceRandom(int start, float firstprob, float con, int max = 20)
    {
        if(start < 1)
        {
            return true;
        }
        float plob = firstprob;
        for (int i = 1; i < start; i++)
        {
            plob = plob / con;
        }

        return IsRandom(plob);
    }

    public static float BCA(float v,float g,float x)
    {
        return Mathf.Pow(v, 4) / (Mathf.Pow(g, 2) * Mathf.Pow(x, 2));
    }
    public static float BCB(float v, float g, float x, float y)
    {
        return (2 * Mathf.Pow(v, 2) * y) / (g * Mathf.Pow(x, 2));
    }
    public static float BCC(float v, float g, float x)
    {
        return Mathf.Pow(v, 2) / (g * x);
    }
    public static float BallisticCalculation(float v, float g, float x, float y)
    {
        return Mathf.Atan(BCC(v, g, x) + Mathf.Sqrt(BCA(v, g, x) - 1 - BCB(v, g, x, y)));
    }

    public static int CalcDamage(float baseattack, float weaponattack,int power, float enemydefence,float critical=1)
    {
        return Mathf.RoundToInt(CalcDamageBase(baseattack, weaponattack, power, enemydefence, critical));
    }
    public static float CalcDamageBase(float baseattack, float weaponattack, int power, float enemydefence, float critical = 1)
    {
        //=G3+((G3*(I3-O3/2+F3-8)/16),0)
        float damage = baseattack + ((baseattack * (weaponattack * critical - enemydefence / 2 + power - 8) / 16));
        //5%誤差を取り出す
        float noise = UnityEngine.Random.Range(-damage * 0.05f, damage * 0.05f);
        damage += noise;
        if (damage < 0)
        {
            damage = 0;
        }
        return damage;
    }


    /// <summary>
    /// スクロール位置の設定
    /// </summary>
    public static void SetCenterViewItem(float height,GameObject scrollView,int index,int n,float dic = 1)
    {
        //以下スクロールの設定
        float contentHeight = scrollView.GetComponent<RectTransform>().sizeDelta.y;
        // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
        if (contentHeight >= height)
        {
            return;
        }
        //height *= dic;
        //float y = (index + 0.5f) / n;  // 要素の中心座標
        //float scrollY = y - 0.5f * height / contentHeight;
        //float ny = (scrollY ) / (1 - height / contentHeight);  // ScrollRect用に正規化した座標
        float ny = index / (float)(n - 1);
        ny = 1 - ny; 
        ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = Mathf.Clamp(ny, 0, 1);
    }

    public static float DragScrollViewFirstPosition;

    public static void SetDragScrollViewFirstPosition(GameObject scrollView)
    {
        ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
        DragScrollViewFirstPosition = scrollRect.verticalNormalizedPosition;
    }

    public static void SetActive(GameObject g, bool b)
    {
        if(g.activeSelf != b)
        {
            g.SetActive(b);
        }
    }

    /// <summary>
    /// スクロール位置の設定
    /// </summary>
    public static void SetDragScrollViewItem(BaseEventData eventData,float height, GameObject scrollView, float unit,int n)
    {

        PointerEventData pointerEventData = eventData as PointerEventData;
        if (IsNull(pointerEventData) == true)
        {
            return;

        }
        //以下スクロールの設定
        float contentHeight = scrollView.GetComponent<RectTransform>().sizeDelta.y;
        // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
        if (contentHeight >= height)
        {
            return;
        }

        //移動量を計算
        float y = pointerEventData.pressPosition.y - pointerEventData.position.y;

        //移動量単位
        float move = height / contentHeight / unit / n * 2;
        //float move = height  / n / unit;

        float ny = DragScrollViewFirstPosition + (move * y);
        ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = Mathf.Clamp(ny, 0, 1);
    }

    public static sbyte GetDeltaWait(sbyte time)
    {
        int res = Mathf.CeilToInt(time * (60 * Time.smoothDeltaTime));
        
        res = Mathf.Clamp(res, 0, time * 3);

        return (sbyte)res;
    }
    public static float GetDelta(float perFlameCount)
    {
        //float res = perFlameCount * (60 * Time.deltaTime);
        float res = perFlameCount * Time.deltaTime;

        res = Mathf.Clamp(res, 0, perFlameCount * 3);

        return res;
    }

    /// <summary>
    /// スクロール位置の設定
    /// </summary>
    public static void SetScrollScrollViewItem(BaseEventData eventData, float height, GameObject scrollView,float unit, int n)
    {

        PointerEventData pointerEventData = eventData as PointerEventData;
        if (IsNull(pointerEventData) == true)
        {
            return;

        }
        //以下スクロールの設定
        float contentHeight = scrollView.GetComponent<RectTransform>().sizeDelta.y;
        // コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
        if (contentHeight >= height)
        {
            return;
        }

        //移動量を計算
        float y = pointerEventData.scrollDelta.y;

        //移動量単位
        float move = height / contentHeight  / n;
        //float move = height / n / unit;

        ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();

        float ny = scrollRect.verticalNormalizedPosition + (y * move);

        scrollRect.verticalNormalizedPosition = Mathf.Clamp(ny, 0, 1);
    }

    private static long _beforeClickStamp = 0;
    public static bool IsDoubleClick()
    {
        //Debug.Log(DateTime.Now.Ticks - _beforeClickStamp);
        //if (data.button == PointerEventData.InputButton.Left)
        {
            //前回のクリックから0.25秒以内か
            TimeSpan sp = new TimeSpan(DateTime.Now.Ticks - _beforeClickStamp);


            if (sp.TotalMilliseconds < 250)
            {
                return true;
            }
           
            _beforeClickStamp = DateTime.Now.Ticks;
        }

        return false;
    }

    //public static void AddListener(GameObject uiBehaviour, EventTriggerType eventID, UnityAction<BaseEventData> callback)
    //{
    //    var entry = new EventTrigger.Entry();
    //    entry.eventID = eventID;
    //    entry.callback.AddListener(callback);

    //    var eventTriggers = (uiBehaviour.GetComponent<EventTrigger>() ?? uiBehaviour.gameObject.AddComponent<EventTrigger>()).triggers;
    //    eventTriggers.Add(entry);
    //}

    public static void AddListenerMenu(GameObject uiBehaviour, EventTriggerType eventID, UnityAction<BaseEventData> callback, bool isRemake = true)
    {
        if (KeyControlInformation.Info.OpMode != OperationMode.UseMouse)
        {

            EventTrigger eventTrigger = uiBehaviour.GetComponent<EventTrigger>();
            if(IsNull(eventTrigger) == true)
            {
                return;
            }
            List<EventTrigger.Entry> eventTriggers = eventTrigger.triggers;

            EventTrigger.Entry origin = eventTriggers.Find(e => e.eventID == eventID);
            if (CommonFunction.IsNull(origin) == false)
            {
                eventTriggers.Remove(origin);
            }
            return;
        }
        AddListener(uiBehaviour, eventID, callback, isRemake);
    }
    public static void AddListener(GameObject uiBehaviour, EventTriggerType eventID, UnityAction<BaseEventData> callback,bool isRemake = true)
    {
        var eventTriggers = (uiBehaviour.GetComponent<EventTrigger>() ?? uiBehaviour.gameObject.AddComponent<EventTrigger>()).triggers;

        EventTrigger.Entry origin = eventTriggers.Find(e => e.eventID == eventID);
        if (CommonFunction.IsNull(origin) == false)
        {
            if(isRemake == false)
            {
                return;
            }
            eventTriggers.Remove(origin);
        }

        var entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener(callback);

        eventTriggers.Add(entry);
        
    }

    public static void RemoveAllListeners(GameObject uiBehaviour, EventTriggerType eventID)
    {
        var eventTrigger = uiBehaviour.GetComponent<EventTrigger>();

        if (eventTrigger == null)
            return;

        eventTrigger.triggers.RemoveAll(listener => listener.eventID == eventID);
    }

    /// <summary>
    /// オプション配列のクローンを作る
    /// </summary>
    public static List<BaseOption> CloneOptions(List<BaseOption> opts)
    {
        List<BaseOption> newopts = new List<BaseOption>();
        if (CommonFunction.IsNull(opts) == false)
        {
            foreach (BaseOption o in opts)
            {
                BaseOption c = o.Clone();
                newopts.Add(c);
            }
        }
        return newopts;
    }

    public static bool IsReverceDamage(Dictionary<Guid, BaseOption> options)
    {
        if (CommonFunction.IsNull(options) == false)
        {
            foreach (BaseOption o in options.Values)
            {
                if (o.OType == OptionType.ReverceDamage)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool HasOptionType(List<BaseOption> options,OptionType opt)
    {
        if (CommonFunction.IsNull(options) == false)
        {
            foreach (BaseOption o in options)
            {
                if (o.OType == opt)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static int GetDefenceAbnormals(List<BaseOption> options)
    {
        int result = 0;
        if (CommonFunction.IsNull(options) == false)
        {
            foreach (BaseOption o in options)
            {
                if (o.OType == OptionType.DefenceAbnormal)
                {
                    result += o.AbnormalStateTarget;
                }
            }
        }
        return result;
    }

    public static bool HasOptionType(List<BaseOption> options, OptionType opt, out int plus,out float commonf)
    {
        plus = 0;
        commonf = 0;
        if (CommonFunction.IsNull(options) == false)
        {
            foreach (BaseOption o in options)
            {
                if (o.OType == opt)
                {
                    plus = o.Plus;
                    commonf = o.CommonFloat;
                    return true;
                }
            }
        }
        return false;
    }
    

    private static StateAbnormal[] _BadAbnormals;
    public static StateAbnormal[] BadAbnormals
    {
        get
        {
            if(IsNull(_BadAbnormals) == false)
            {
                return _BadAbnormals;
            }

            _BadAbnormals = new StateAbnormal[] {
                StateAbnormal.Dark,
                StateAbnormal.Confusion,
                StateAbnormal.Charmed,
                StateAbnormal.Decoy,
                StateAbnormal.DeadlyPoison,
                StateAbnormal.Palalysis,
                StateAbnormal.Poison,
                StateAbnormal.Reticent,
                StateAbnormal.Sleep,
                StateAbnormal.Slow,
                StateAbnormal.StiffShoulder
            };
            return _BadAbnormals;
        }
    }

    public static bool IsBadAbnormal(int state)
    {
        bool r = false;
        foreach(StateAbnormal s in BadAbnormals)
        {
            if((state & ((int)s)) != 0)
            {
                r = true;
            }
        }
        return r;
    }
    //悪い状態異常をランダムで一つ取得
    public static StateAbnormal GetRandomAbnormal()
    {
        return BadAbnormals[UnityEngine.Random.Range(0, BadAbnormals.Length)];
    }

    /// <summary>
    /// UnityのNull比較オペレータには不具合があるっぽい？
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNull(object obj)
    {
        
        return object.Equals(null, obj);
    }

    public static T GetSingleton<T>(ref T target) where T : new()
    {
        if(IsNull(target) == false)
        {
            return target;
        }
        target = new T();
        return target;
    }

    public static Dictionary<Key, Val> GetDictionarySingleton<Key,Val,Comp>(ref Dictionary<Key, Val> target) where Comp : IEqualityComparer<Key>,new ()
    {
        if (IsNull(target) == false)
        {
            return target;
        }

        target = new Dictionary<Key, Val>(new Comp());
        return target;
    }

    public static string CutString(string val, int cnt)
    {
        if (CommonFunction.IsNull(val) == false && val.Length > cnt)
        {
            val = val.Substring(0, cnt);
        }
        return val;
    }
    
    public static Vector3 GetVelocity(Vector3 velocity,float speed)
    {
        return (velocity * (speed * (60 * Time.smoothDeltaTime)));
    }

    /// <summary>
    /// UnityのNull比較オペレータには不具合があるっぽい？
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullUnity(GameObject obj)
    {
        return UnityEngine.Object.Equals(null, obj) == true && UnityEngine.Object.Equals(null, ((System.Object)obj)) == false;
        //return UnityEngine.Object.Equals(null, obj);
    }
    #region UIオブジェクト系
    public static GameObject GetObject(List<GameObject> list, GameObject origin, Transform tf)
    {
        GameObject gm = list.Find(i => i.activeSelf == false);
        if (CommonFunction.IsNull(gm) == false)
        {
            gm.SetActive(true);
            return gm;
        }
        gm = GameObject.Instantiate(origin, tf.transform);
        gm.SetActive(true);
        list.Add(gm);
        return gm;
    }

    public static void DestroyObject(GameObject gm, Transform parent)
    {
        gm.transform.SetParent(parent);
        gm.SetActive(false);
    }
    public static void DestroyObject(GameObject gm)
    {
        gm.SetActive(false);
    }

    /// <summary>
    /// アイテムスクロールビューに項目を設定
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject SetItemChild(GameObject child, GameObject parent)
    {
        // インスタンスを生成
        GameObject obj = GameObject.Instantiate(child, parent.transform);

        return obj;
    }

    private static Dictionary<int, Color> _UnColorPool;
    private static Dictionary<int, Color> UnColorPool
    {
        get
        {
            if(IsNull(_UnColorPool) == false)
            {
                return _UnColorPool;
            }
            _UnColorPool = new Dictionary<int, Color>();
            return _UnColorPool;
        }
    }

    /// <summary>
    /// 対象位置のイメージを消す
    /// </summary>
    public static void SetItemUnSelectBack(GameObject obj)
    {
        Image cols = obj.transform.Find("SelectedBack").GetComponent<Image>();
        int rgb = Mathf.CeilToInt(cols.color.r + (cols.color.g * 1000) + (cols.color.b * 1000000));

        Color newc;
        if(UnColorPool.ContainsKey(rgb) == false)
        {
            newc = new Color(cols.color.r, cols.color.g, cols.color.b, 0.0f);
            UnColorPool.Add(rgb, newc);
        }
        else
        {
            newc = UnColorPool[rgb];
        }

        cols.color = newc;
    }

    private static Dictionary<int, Color> _ColorPool;
    private static Dictionary<int, Color> ColorPool
    {
        get
        {
            if (IsNull(_ColorPool) == false)
            {
                return _ColorPool;
            }
            _ColorPool = new Dictionary<int, Color>();
            return _ColorPool;
        }
    }

    /// <summary>
    /// 対象位置のイメージをつける
    /// </summary>
    public static void SetItemSelectBack(GameObject obj)
    {
        Image cols = obj.transform.Find("SelectedBack").GetComponent<Image>();
        int rgb = Mathf.CeilToInt(cols.color.r + (cols.color.g * 1000) + (cols.color.b * 1000000));

        Color newc;
        if (ColorPool.ContainsKey(rgb) == false)
        {
            newc = new Color(cols.color.r, cols.color.g, cols.color.b, 1.0f);
            ColorPool.Add(rgb, newc);
        }
        else
        {
            newc = ColorPool[rgb];
        }

        cols.color = newc;

    }
    #endregion UIオブジェクト系

    public static AttackState AddDamage(AttackInformation atinf,BaseCharacter attacker, BaseCharacter target, int damage)
    {

        atinf.AddDamage(target.Name, damage);

        //ヒットメッセージ
        atinf.AddMessage(
            target.GetMessageAttackHit(attacker.DisplayNameInMessage, damage));

        //ダメージ判定
        AttackState atState = target.AddDamage(damage);

        return atState;
    }

    public static string GetHierarchyPath(string ThisGameObjectName,Transform self)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(self.gameObject.name);
        string path = self.gameObject.name;
        Transform parent = self.parent;
        while (parent != null)
        {
            if(ThisGameObjectName == parent.name)
            {
                break;
            }
            sb.Insert(0, "/");
            sb.Insert(0, parent.name);
            parent = parent.parent;
        }
        return sb.ToString();
    }
    
    private static Dictionary<CharacterDirection, Vector3> _EulerAngles;
    public static Dictionary<CharacterDirection, Vector3> EulerAngles
    {
        get
        {
            if (_EulerAngles != null)
            {
                return _EulerAngles;
            }
            _EulerAngles = new Dictionary<CharacterDirection, Vector3>(new CharacterDirectionComparer());

            _EulerAngles.Add(CharacterDirection.Top, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.Top, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.Bottom, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.Bottom, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.Right, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.Right, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.Left, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.Left, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.TopLeft, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.TopLeft, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.TopRight, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.TopRight, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.BottomLeft, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.BottomLeft, CommonConst.Rotation.None));
            _EulerAngles.Add(CharacterDirection.BottomRight, new Vector3(CommonConst.Rotation.None, CommonConst.Rotation.BottomRight, CommonConst.Rotation.None));
            //_directionVector.Add(CharacterDirection.Match, new Vector3(0, 0, 0));
            return _EulerAngles;
        }
    }

    private static Dictionary<CharacterDirection, Vector3> _directionVector;
    public static Dictionary<CharacterDirection, Vector3> DirectionVector
    {
        get
        {
            if(_directionVector != null)
            {
                return _directionVector;
            }
            _directionVector = new Dictionary<CharacterDirection, Vector3>(new CharacterDirectionComparer());

            _directionVector.Add(CharacterDirection.Top, new Vector3(0, 0, 1));
            _directionVector.Add(CharacterDirection.Bottom, new Vector3(0, 0, -1));
            _directionVector.Add(CharacterDirection.Right, new Vector3(1, 0, 0));
            _directionVector.Add(CharacterDirection.Left, new Vector3(-1, 0, 0));
            _directionVector.Add(CharacterDirection.TopLeft, new Vector3(-0.7f, 0, 0.7f));
            _directionVector.Add(CharacterDirection.TopRight, new Vector3(0.7f, 0, 0.7f));
            _directionVector.Add(CharacterDirection.BottomLeft, new Vector3(-0.7f, 0, -0.7f));
            _directionVector.Add(CharacterDirection.BottomRight, new Vector3(0.7f, 0, -0.7f));
            //_directionVector.Add(CharacterDirection.Match, new Vector3(0, 0, 0));
            return _directionVector;
        }
    }

    private static Dictionary<CharacterDirection, Vector2> _directionVector2;
    public static Dictionary<CharacterDirection, Vector2> DirectionVector2
    {
        get
        {
            if (_directionVector2 != null)
            {
                return _directionVector2;
            }
            _directionVector2 = new Dictionary<CharacterDirection, Vector2>(new CharacterDirectionComparer());

            _directionVector2.Add(CharacterDirection.Top, new Vector2(0, 1));
            _directionVector2.Add(CharacterDirection.Bottom, new Vector2(0,  -1));
            _directionVector2.Add(CharacterDirection.Right, new Vector2(1, 0));
            _directionVector2.Add(CharacterDirection.Left, new Vector2(-1,  0));
            _directionVector2.Add(CharacterDirection.TopLeft, new Vector2(-0.7f, 0.7f));
            _directionVector2.Add(CharacterDirection.TopRight, new Vector2(0.7f, 0.7f));
            _directionVector2.Add(CharacterDirection.BottomLeft, new Vector2(-0.7f,  -0.7f));
            _directionVector2.Add(CharacterDirection.BottomRight, new Vector2(0.7f,  -0.7f));
            //_directionVector.Add(CharacterDirection.Match, new Vector3(0, 0, 0));
            return _directionVector2;
        }
    }

    /// <summary>
    /// 左前の相対位置
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _RelativeFrontLeft;
    public static Dictionary<CharacterDirection, CharacterDirection> RelativeFrontLeft
    {
        get
        {
            if (IsNull(_RelativeFrontLeft) == false)
            {
                return _RelativeFrontLeft;
            }
            _RelativeFrontLeft = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _RelativeFrontLeft.Add(CharacterDirection.Top, CharacterDirection.TopLeft);
            _RelativeFrontLeft.Add(CharacterDirection.TopLeft, CharacterDirection.Left);
            _RelativeFrontLeft.Add(CharacterDirection.Left, CharacterDirection.BottomLeft);
            _RelativeFrontLeft.Add(CharacterDirection.BottomLeft, CharacterDirection.Bottom);
            _RelativeFrontLeft.Add(CharacterDirection.Bottom, CharacterDirection.BottomRight);
            _RelativeFrontLeft.Add(CharacterDirection.BottomRight, CharacterDirection.Right);
            _RelativeFrontLeft.Add(CharacterDirection.Right, CharacterDirection.TopRight);
            _RelativeFrontLeft.Add(CharacterDirection.TopRight, CharacterDirection.Top);
            return _RelativeFrontLeft;
        }
    }
    /// <summary>
    /// 右前の相対位置
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _RelativeFrontRight;
    public static Dictionary<CharacterDirection, CharacterDirection> RelativeFrontRight
    {
        get
        {
            if (IsNull(_RelativeFrontRight) == false)
            {
                return _RelativeFrontRight;
            }
            _RelativeFrontRight = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _RelativeFrontRight.Add(CharacterDirection.Top, CharacterDirection.TopRight);
            _RelativeFrontRight.Add(CharacterDirection.TopLeft, CharacterDirection.Top);
            _RelativeFrontRight.Add(CharacterDirection.Left, CharacterDirection.TopLeft);
            _RelativeFrontRight.Add(CharacterDirection.BottomLeft, CharacterDirection.Left);
            _RelativeFrontRight.Add(CharacterDirection.Bottom, CharacterDirection.BottomLeft);
            _RelativeFrontRight.Add(CharacterDirection.BottomRight, CharacterDirection.Bottom);
            _RelativeFrontRight.Add(CharacterDirection.Right, CharacterDirection.BottomRight);
            _RelativeFrontRight.Add(CharacterDirection.TopRight, CharacterDirection.Right);
            return _RelativeFrontRight;
        }
    }
    /// <summary>
    /// 左の相対位置
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _RelativeLeft;
    public static Dictionary<CharacterDirection, CharacterDirection> RelativeLeft
    {
        get
        {
            if (IsNull(_RelativeLeft) == false)
            {
                return _RelativeLeft;
            }
            _RelativeLeft = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _RelativeLeft.Add(CharacterDirection.Top, CharacterDirection.Left);
            _RelativeLeft.Add(CharacterDirection.TopLeft, CharacterDirection.BottomLeft);
            _RelativeLeft.Add(CharacterDirection.Left, CharacterDirection.Bottom);
            _RelativeLeft.Add(CharacterDirection.BottomLeft, CharacterDirection.BottomRight);
            _RelativeLeft.Add(CharacterDirection.Bottom, CharacterDirection.Right);
            _RelativeLeft.Add(CharacterDirection.BottomRight, CharacterDirection.TopRight);
            _RelativeLeft.Add(CharacterDirection.Right, CharacterDirection.Top);
            _RelativeLeft.Add(CharacterDirection.TopRight, CharacterDirection.TopLeft);
            return _RelativeLeft;
        }
    }
    /// <summary>
    /// 右の相対位置
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _RelativeRight;
    public static Dictionary<CharacterDirection, CharacterDirection> RelativeRight
    {
        get
        {
            if (IsNull(_RelativeRight) == false)
            {
                return _RelativeRight;
            }
            _RelativeRight = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _RelativeRight.Add(CharacterDirection.Top, CharacterDirection.Right);
            _RelativeRight.Add(CharacterDirection.TopLeft, CharacterDirection.TopRight);
            _RelativeRight.Add(CharacterDirection.Left, CharacterDirection.Top);
            _RelativeRight.Add(CharacterDirection.BottomLeft, CharacterDirection.TopLeft);
            _RelativeRight.Add(CharacterDirection.Bottom, CharacterDirection.Left);
            _RelativeRight.Add(CharacterDirection.BottomRight, CharacterDirection.BottomLeft);
            _RelativeRight.Add(CharacterDirection.Right, CharacterDirection.Bottom);
            _RelativeRight.Add(CharacterDirection.TopRight, CharacterDirection.BottomRight);
            return _RelativeRight;
        }
    }

    private static Dictionary<CharacterDirection, CharacterDirection> _ReversedirectionVector;
    public static Dictionary<CharacterDirection, CharacterDirection> ReverseDirection
    {
        get
        {
            if (_ReversedirectionVector != null)
            {
                return _ReversedirectionVector;
            }
            _ReversedirectionVector = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _ReversedirectionVector.Add(CharacterDirection.Top, CharacterDirection.Bottom);
            _ReversedirectionVector.Add(CharacterDirection.TopLeft, CharacterDirection.BottomRight);
            _ReversedirectionVector.Add(CharacterDirection.TopRight, CharacterDirection.BottomLeft);
            _ReversedirectionVector.Add(CharacterDirection.Left, CharacterDirection.Right);
            _ReversedirectionVector.Add(CharacterDirection.Right, CharacterDirection.Left);
            _ReversedirectionVector.Add(CharacterDirection.BottomLeft, CharacterDirection.TopRight);
            _ReversedirectionVector.Add(CharacterDirection.BottomRight, CharacterDirection.TopLeft);
            _ReversedirectionVector.Add(CharacterDirection.Bottom, CharacterDirection.Top);
            //_ReversedirectionVector.Add(CharacterDirection.Match, CharacterDirection.Match);
            return _ReversedirectionVector;
        }
    }

    /// <summary>
    /// 左後ろ
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _ReverseLeftDirectionVector;
    public static Dictionary<CharacterDirection, CharacterDirection> ReverseLeftDirectionVector
    {
        get
        {
            if (_ReverseLeftDirectionVector != null)
            {
                return _ReverseLeftDirectionVector;
            }
            _ReverseLeftDirectionVector = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _ReverseLeftDirectionVector.Add(CharacterDirection.Top, CharacterDirection.BottomLeft);
            _ReverseLeftDirectionVector.Add(CharacterDirection.TopLeft, CharacterDirection.Bottom);
            _ReverseLeftDirectionVector.Add(CharacterDirection.TopRight, CharacterDirection.Left);
            _ReverseLeftDirectionVector.Add(CharacterDirection.Left, CharacterDirection.BottomRight);
            _ReverseLeftDirectionVector.Add(CharacterDirection.Right, CharacterDirection.TopLeft);
            _ReverseLeftDirectionVector.Add(CharacterDirection.BottomLeft, CharacterDirection.Right);
            _ReverseLeftDirectionVector.Add(CharacterDirection.BottomRight, CharacterDirection.Top);
            _ReverseLeftDirectionVector.Add(CharacterDirection.Bottom, CharacterDirection.TopRight);
            //_ReversedirectionVector.Add(CharacterDirection.Match, CharacterDirection.Match);
            return _ReverseLeftDirectionVector;
        }
    }

    /// <summary>
    /// 右後ろ
    /// </summary>
    private static Dictionary<CharacterDirection, CharacterDirection> _ReverseRightDirectionVector;
    public static Dictionary<CharacterDirection, CharacterDirection> ReverseRightDirectionVector
    {
        get
        {
            if (_ReverseRightDirectionVector != null)
            {
                return _ReverseRightDirectionVector;
            }
            _ReverseRightDirectionVector = new Dictionary<CharacterDirection, CharacterDirection>(new CharacterDirectionComparer());
            _ReverseRightDirectionVector.Add(CharacterDirection.Top, CharacterDirection.BottomRight);
            _ReverseRightDirectionVector.Add(CharacterDirection.TopLeft, CharacterDirection.Right);
            _ReverseRightDirectionVector.Add(CharacterDirection.Left, CharacterDirection.TopRight);
            _ReverseRightDirectionVector.Add(CharacterDirection.BottomLeft, CharacterDirection.Top);
            _ReverseRightDirectionVector.Add(CharacterDirection.Bottom, CharacterDirection.TopLeft);
            _ReverseRightDirectionVector.Add(CharacterDirection.BottomRight, CharacterDirection.Left);
            _ReverseRightDirectionVector.Add(CharacterDirection.Right, CharacterDirection.BottomLeft);
            _ReverseRightDirectionVector.Add(CharacterDirection.TopRight, CharacterDirection.Bottom);
            //_ReversedirectionVector.Add(CharacterDirection.Match, CharacterDirection.Match);
            return _ReverseRightDirectionVector;
        }
    }
    /// <summary>
    /// 方向から単位ベクトルを取得し、
    /// 2個のベクトルの距離を比較して距離が1以上（向きが逆になった）あればfalseを返す
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static bool CheckDirectionVector(CharacterDirection dir,Vector3 v2)
    {
        //if(Vector3.Distance(DirectionVector[dir],v2) >= 1)
        if (Vector3.SqrMagnitude(DirectionVector[dir] - v2) >= 1)
        {
            return false;
        }
        //単位ベクトルが0になっても終了
        else if (v2 == Vector3.zero)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// 方向から単位ベクトルを取得し、
    /// 2個のベクトルの距離を比較して距離が1以上（向きが逆になった）あればfalseを返す
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static bool CheckDirectionVector(CharacterDirection dir, Vector2 v2)
    {
        if (Vector3.Distance(DirectionVector[dir], v2) >= 1)
        {
            return false;
        }
        //単位ベクトルが0になっても終了
        else if (v2 == Vector2.zero)
        {
            return false;
        }
        return true;
    }


    private static Dictionary<StateAbnormal, string> _StateAddMessage;
    public static Dictionary<StateAbnormal, string> StateAddMessage
    {
        get
        {
            if (_StateAddMessage != null)
            {
                return _StateAddMessage;
            }
            _StateAddMessage = new Dictionary<StateAbnormal, string>(new StateAbnormalComparer());
            _StateAddMessage.Add(StateAbnormal.Normal, "正常");
            _StateAddMessage.Add(StateAbnormal.Dark, CommonConst.Message.AddDark);
            _StateAddMessage.Add(StateAbnormal.Sleep, CommonConst.Message.AddSleep);
            _StateAddMessage.Add(StateAbnormal.Poison, CommonConst.Message.AddState);
            _StateAddMessage.Add(StateAbnormal.DeadlyPoison, CommonConst.Message.AddState);
            _StateAddMessage.Add(StateAbnormal.Palalysis, CommonConst.Message.AddPalalysis);
            _StateAddMessage.Add(StateAbnormal.Confusion, CommonConst.Message.AddConf);
            _StateAddMessage.Add(StateAbnormal.Charmed, CommonConst.Message.AddState);
            _StateAddMessage.Add(StateAbnormal.Decoy, CommonConst.Message.AddDecoy);
            _StateAddMessage.Add(StateAbnormal.Slow, CommonConst.Message.AddSlow);
            _StateAddMessage.Add(StateAbnormal.Reticent, CommonConst.Message.AddReticent);
            _StateAddMessage.Add(StateAbnormal.StiffShoulder, CommonConst.Message.AddShoulder);
            _StateAddMessage.Add(StateAbnormal.Acceleration, CommonConst.Message.AddAcceleration);

            return _StateAddMessage;
        }
    }
    public static string GetAbnormalMessage(StateAbnormal st,BaseCharacter tar)
    {
        return string.Format(StateAddMessage[st], tar.DisplayNameInMessage, StateNames[st]);
    }

    private static Dictionary<StateAbnormal, string> _StateNames;
    public static Dictionary<StateAbnormal, string> StateNames
    {
        get
        {
            if (_StateNames != null)
            {
                return _StateNames;
            }
            _StateNames = new Dictionary<StateAbnormal, string>(new StateAbnormalComparer());
            _StateNames.Add(StateAbnormal.Normal, CommonConst.Message.StateAbnormalNormal);
            _StateNames.Add(StateAbnormal.Dark, CommonConst.Message.StateAbnormalDark);
            _StateNames.Add(StateAbnormal.Sleep, CommonConst.Message.StateAbnormalSleep);
            _StateNames.Add(StateAbnormal.Poison, CommonConst.Message.StateAbnormalPoison);
            _StateNames.Add(StateAbnormal.DeadlyPoison, CommonConst.Message.StateAbnormalDeadlyPoison);
            _StateNames.Add(StateAbnormal.Palalysis, CommonConst.Message.StateAbnormalPalalysis);
            _StateNames.Add(StateAbnormal.Confusion, CommonConst.Message.StateAbnormalConfusion);
            _StateNames.Add(StateAbnormal.Charmed, CommonConst.Message.StateAbnormalCharmed);
            _StateNames.Add(StateAbnormal.Decoy, CommonConst.Message.StateAbnormalDecoy);
            _StateNames.Add(StateAbnormal.Slow, CommonConst.Message.StateAbnormalSlow);
            _StateNames.Add(StateAbnormal.Reticent, CommonConst.Message.StateAbnormalReticent);
            _StateNames.Add(StateAbnormal.StiffShoulder, CommonConst.Message.StateAbnormalStiffShoulder);
            _StateNames.Add(StateAbnormal.Acceleration, CommonConst.Message.StateAbnormalAcceleration);

            return _StateNames;
        }
    }

    private static Array _EnumItemTypes;
    public static Array EnumItemTypes
    {
        get
        {
            if (IsNull(_EnumItemTypes) == false)
            {
                return _EnumItemTypes;
            }
            _EnumItemTypes = Enum.GetValues(typeof(ItemType));
            return _EnumItemTypes;
        }
    }

    private static Array _EnumObjectTypes;
    public static Array EnumObjectTypes
    {
        get
        {
            if (IsNull(_EnumObjectTypes) == false)
            {
                return _EnumObjectTypes;
            }
            _EnumObjectTypes = Enum.GetValues(typeof(ObjectType));
            return _EnumObjectTypes;
        }
    }


    private static Array _AnimationTypes;
    public static Array AnimationTypes
    {
        get
        {
            if(IsNull(_AnimationTypes) == false)
            {
                return _AnimationTypes;
            }
            _AnimationTypes = Enum.GetValues(typeof(AnimationType));
            return _AnimationTypes;
        }
    }

    private static Array _StateAbnormals;
    public static Array StateAbnormals
    {
        get
        {
            if (IsNull(_StateAbnormals) == false)
            {
                return _StateAbnormals;
            }
            _StateAbnormals = Enum.GetValues(typeof(StateAbnormal));
            return _StateAbnormals;
        }
    }
    public static string GetStateNames(int state)
    {
        StringBuilder sb = new StringBuilder();

        foreach(StateAbnormal sa in StateNames.Keys)
        {
            if(sa == StateAbnormal.Normal)
            {
                continue;
            }
            int targetState = state & (int)sa;
            if (targetState != 0)
            {
                sb.Append(StateNames[(StateAbnormal)targetState]);
            }
        }

        if(sb.Length == 0)
        {
            sb.Append(StateNames[StateAbnormal.Normal]);
        }

        return sb.ToString();
    }

    private static Dictionary<MenuItemActionType, string> _MenuItemActionTypeName;

    public static Dictionary<MenuItemActionType, string> MenuItemActionTypeName
    {
        get
        {
            if (_MenuItemActionTypeName != null)
            {
                return _MenuItemActionTypeName;
            }
            _MenuItemActionTypeName = new Dictionary<MenuItemActionType, string>();
            _MenuItemActionTypeName.Add(MenuItemActionType.Get, CommonConst.Message.ActionTypeGet);
            _MenuItemActionTypeName.Add(MenuItemActionType.Eat, CommonConst.Message.ActionTypeEat);
            _MenuItemActionTypeName.Add(MenuItemActionType.Equip, CommonConst.Message.ActionTypeEquip);
            _MenuItemActionTypeName.Add(MenuItemActionType.Put, CommonConst.Message.ActionTypePut);
            _MenuItemActionTypeName.Add(MenuItemActionType.Look, CommonConst.Message.ActionTypeLook);
            _MenuItemActionTypeName.Add(MenuItemActionType.Melody, CommonConst.Message.ActionTypeMelody);
            _MenuItemActionTypeName.Add(MenuItemActionType.Putin, CommonConst.Message.ActionTypePutin);
            _MenuItemActionTypeName.Add(MenuItemActionType.PutinParent, CommonConst.Message.ActionTypePutinParent);
            _MenuItemActionTypeName.Add(MenuItemActionType.Putout, CommonConst.Message.ActionTypePutout);
            _MenuItemActionTypeName.Add(MenuItemActionType.RemoveEquip, CommonConst.Message.ActionTypeRemoveEquip);
            _MenuItemActionTypeName.Add(MenuItemActionType.Throw, CommonConst.Message.ActionTypeThrow);
            _MenuItemActionTypeName.Add(MenuItemActionType.Use, CommonConst.Message.ActionTypeUse);
            _MenuItemActionTypeName.Add(MenuItemActionType.LookOption, CommonConst.Message.ActionTypeLookOption);

            return _MenuItemActionTypeName;
        }
    }

    private static Dictionary<AtelierMenuActionType, string> _AtelierMenuActionTypeName;

    public static Dictionary<AtelierMenuActionType, string> AtelierMenuActionTypeName
    {
        get
        {
            if (IsNull(_AtelierMenuActionTypeName) == false)
            {
                return _AtelierMenuActionTypeName;
            }
            _AtelierMenuActionTypeName = new Dictionary<AtelierMenuActionType, string>();
            _AtelierMenuActionTypeName.Add(AtelierMenuActionType.Auto, CommonConst.Message.SelectAuto);
            _AtelierMenuActionTypeName.Add(AtelierMenuActionType.Manual, CommonConst.Message.SelectManual);

            return _AtelierMenuActionTypeName;
        }
    }

    private static Dictionary<CharacterDirection, MapPoint> _CharacterDirectionVector;

    public static Dictionary<CharacterDirection, MapPoint> CharacterDirectionVector
    {
        get
        {
            if (_CharacterDirectionVector != null)
            {
                return _CharacterDirectionVector;
            }
            _CharacterDirectionVector = new Dictionary<CharacterDirection, MapPoint>(new CharacterDirectionComparer());
            //_CharacterDirectionVector.Add(CharacterDirection.Match, new MapPoint(0, 0));
            _CharacterDirectionVector.Add(CharacterDirection.Top, MapPoint.Get(0, 1));
            _CharacterDirectionVector.Add(CharacterDirection.TopLeft, MapPoint.Get(-1, 1));
            _CharacterDirectionVector.Add(CharacterDirection.Left, MapPoint.Get(-1, 0));
            _CharacterDirectionVector.Add(CharacterDirection.BottomLeft, MapPoint.Get(-1, -1));
            _CharacterDirectionVector.Add(CharacterDirection.Bottom, MapPoint.Get(0, -1));
            _CharacterDirectionVector.Add(CharacterDirection.BottomRight, MapPoint.Get(1, -1));
            _CharacterDirectionVector.Add(CharacterDirection.Right, MapPoint.Get(1, 0));
            _CharacterDirectionVector.Add(CharacterDirection.TopRight, MapPoint.Get(1, 1));

            return _CharacterDirectionVector;
        }
    }

    public static Dictionary<CharacterDirection, Dictionary<bool, List<CharacterDirection>>> _DirectionPriority;

    /// <summary>
    /// 方向に対応した優先順位のリストを管理
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Dictionary<CharacterDirection, Dictionary<bool, List<CharacterDirection>>> DirectionPriority
    {
        get
        {
            if (_DirectionPriority != null)
            {
                return _DirectionPriority;
            }

            _DirectionPriority = new Dictionary<CharacterDirection, Dictionary<bool, List<CharacterDirection>>>(new CharacterDirectionComparer());
            //上
            Dictionary<bool, List<CharacterDirection>> parent = new Dictionary<bool, List<CharacterDirection>>();
            List<CharacterDirection> child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Bottom);
            ////child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Bottom);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.Top, parent);

            //↓
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Top);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);
            
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Top);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.Bottom, parent);

            //右上
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.BottomLeft);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomLeft);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.TopRight, parent);

            //左上
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomRight);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.BottomRight);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.TopLeft, parent);

            //右
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Left);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);


            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Left);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.Right, parent);

            //左
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Right);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Right);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.Left, parent);

            //左下
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.TopRight);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.TopLeft);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.TopRight);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.BottomLeft, parent);

            //右下
            parent = new Dictionary<bool, List<CharacterDirection>>();
            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.TopLeft);
            //child.Add(CharacterDirection.Match);
            parent.Add(true, child);

            child = new List<CharacterDirection>();
            child.Add(CharacterDirection.BottomRight);
            child.Add(CharacterDirection.Right);
            child.Add(CharacterDirection.Bottom);
            child.Add(CharacterDirection.BottomLeft);
            child.Add(CharacterDirection.TopRight);
            child.Add(CharacterDirection.Left);
            child.Add(CharacterDirection.Top);
            child.Add(CharacterDirection.TopLeft);
            //child.Add(CharacterDirection.Match);
            parent.Add(false, child);
            _DirectionPriority.Add(CharacterDirection.BottomRight, parent);

            //一致
            //parent = new Dictionary<bool, Dictionary<CharacterDirection, Slash>>();
            //child = new Dictionary<CharacterDirection, Slash>(new CharacterDirectionComparer());
            //child.Add(CharacterDirection.Match);
            //parent.Add(true, child);
            //parent.Add(false, child);
            //_DirectionPriority.Add(CharacterDirection.Match, parent);

            return _DirectionPriority;
        }
    }

    public static void SetVolume(WebGLStreamingAudioSourceInterop audio, float value)
    {
        if (CommonFunction.IsNull(audio) == false)
        {
            audio.Volume = value;
        }
    }

    public static float PercentToNumber(float val)
    {
        return Mathf.Clamp(val * 100, 0, 100);
    }

    public static int PercentToInt(float val)
    {
        return Mathf.RoundToInt(Mathf.Clamp(val * 100, 0, 100));
    }

    public static float NumberToPercent(float val)
    {
        return Mathf.Clamp(val / 100, 0, 1);
    }
    public static string CammaString(params object[] args)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object t in args)
        {
            sb.Append(t);
            sb.Append(",");
        }
        return sb.ToString();
    }

#if UNITY_EDITOR
    public static void OutLog(params object[] args)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object t in args)
        {
            sb.Append(t);
        }
        OutLog(sb.ToString());
    }

    public static void OutLog(string[] str)
    {
        foreach(string s in str)
        {
            OutLog(s);
        }
    }

    public static void OutLog(object str)
    {
        if(IsNull( str))
        {
            Debug.Log(null  );
        }
        else
        {
            Debug.Log(str.ToString());
        }
    }
    public static void Dump(StringBuilder sb , string text = @"C:\Unity\test.txt")
    {
        Debug.Log(sb.Length);
        System.IO.StreamWriter writer =
            new System.IO.StreamWriter(text, true, System.Text.Encoding.UTF8);
        writer.WriteLine(sb.ToString());
        writer.Close();
    }
#endif

}

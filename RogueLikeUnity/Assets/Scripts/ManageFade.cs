using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ManageFade : MonoBehaviour
{

    //フェード対象
    private CanvasGroup _fadeTarget;
    private float _duration;
    private bool _ignoreTimeScale = true;
    public FadeState FadeState;
    private bool isWait;
    public bool IsFadeinEnd;
    public bool IsFadeChange;

    public float Wait = CommonConst.Wait.FloorChangeSeconds;

    public void SetupFade(string dungeonName)
    {
        _fadeTarget = GameObject.Find("NextFloorPanel").GetComponent<CanvasGroup>();
        _fadeTarget.transform.Find("DungeonNameText").GetComponent<Text>().text
            = string.Format("{0}", dungeonName);
    }
    public void SetupFade()
    {
        _fadeTarget = GameObject.Find("NextFloorPanel").GetComponent<CanvasGroup>();
    }
    public void SetWaitDefault()
    {
        Wait = CommonConst.Wait.FloorChangeSeconds;
    }


    /// <summary>
    /// フェードを開始する
    /// </summary>
    public void Play(FadeState state, ushort floor, bool isWait = false, float duration = 1, bool ignoreTimeScale = true)
    {
        if (state == FadeState.FadeOut)
        {
            _fadeTarget.alpha = 1;
        }
        else
        {
            _fadeTarget.alpha = 0;
        }
        _fadeTarget.transform.Find("DungeonFloorText").GetComponent<Text>().text
            = string.Format("{0} F", floor);
        FadeState = state;
        _duration = duration;
        isWait = false;
        IsFadeinEnd = false;
        IsFadeChange = false;
        _ignoreTimeScale = ignoreTimeScale;
    }


    /// <summary>
    /// フェードを開始する
    /// </summary>
    public void Play(FadeState state, bool isWait = false, float duration = 1, bool ignoreTimeScale = true)
    {
        if (state == FadeState.FadeOut)
        {
            _fadeTarget.alpha = 1;
        }
        else
        {
            _fadeTarget.alpha = 0;
        }
        _fadeTarget.transform.Find("DungeonFloorText").GetComponent<Text>().text
            = "";
        _fadeTarget.transform.Find("DungeonNameText").GetComponent<Text>().text
            = "";
        FadeState = state;
        _duration = duration;
        isWait = false;
        IsFadeinEnd = false;
        IsFadeChange = false;
        _ignoreTimeScale = ignoreTimeScale;
    }
    /// <summary>
    /// フェードを開始する
    /// </summary>
    public void PlayFadeOut(ushort floor, bool isWait = false, float duration = 1, bool ignoreTimeScale = true)
    {
        _fadeTarget.alpha = 0.999f;
        _fadeTarget.transform.Find("DungeonFloorText").GetComponent<Text>().text
            = string.Format("{0} F", floor);
        FadeState = FadeState.FadeIn;
        _duration = duration;
        isWait = false;
        IsFadeinEnd = false;
        IsFadeChange = false;
        _ignoreTimeScale = ignoreTimeScale;
    }
    /// <summary>
    /// フェードを開始する
    /// </summary>
    public void PlayFadeOut( bool isWait = false, float duration = 1, bool ignoreTimeScale = true)
    {
        _fadeTarget.alpha = 0.999f;
        FadeState = FadeState.FadeIn;
        _duration = duration;
        isWait = false;
        IsFadeinEnd = false;
        IsFadeChange = false;
        _ignoreTimeScale = ignoreTimeScale;
    }
    /// <summary>
    /// フェードを開始する
    /// </summary>
    public void PlayFadeIn(bool isWait = false, float duration = 1, bool ignoreTimeScale = true)
    {
        _fadeTarget.alpha = 0f;
        FadeState = FadeState.FadeIn;
        _duration = duration;
        isWait = false;
        IsFadeinEnd = false;
        IsFadeChange = false;
        _ignoreTimeScale = ignoreTimeScale;
    }


    private void Update()
    {
        Update2();
    }
    private void Update2()
    {
        if (FadeState == FadeState.None
            || isWait == true)
        {
            return;
        }
        float fadeSpeed = 1f / _duration;
        if (_ignoreTimeScale)
        {
            fadeSpeed *= Time.unscaledDeltaTime;
        }
        else
        {
            fadeSpeed *= Time.smoothDeltaTime;
        }

        _fadeTarget.alpha += fadeSpeed * (FadeState == FadeState.FadeIn ? 1f : -1f);
        
        //フェード終了判定
        if (_fadeTarget.alpha > 0 && _fadeTarget.alpha < 1)
        {
            return;
        }

        //フェードインが終了したらフェードアウトに切り替え
        if (FadeState == FadeState.FadeIn)
        {
            isWait = true;
            IsFadeinEnd = true;
            FadeState = FadeState.FadeOut;
            MainThreadDispatcher.StartUpdateMicroCoroutine(WaitCorutine());
            //StartCoroutine("WaitCorutine");
        }
        //フェードアウトが終了したら終了フラグを出す
        else if (FadeState == FadeState.FadeOut && isWait == false)
        {
            IsFadeChange = true;
            FadeState = FadeState.None;
        }
    }
    IEnumerator WaitCorutine()
    {
        //3秒表示
        //yield return new WaitForSeconds(Wait);

        float waitcount = 0;

        while (waitcount < Wait)
        {
            waitcount += CommonFunction.GetDelta(1);
            yield return null;
        }

        isWait = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StellaBrink : MonoBehaviour
{
    public Material Default;
    public Material Brink1;
    public Material Brink2;

    public bool isActive = true;				//オート目パチ有効
    //public float timeBlink = 0.4f;              //目パチの時間
    //private float timeRemining = 0.0f;          //タイマー残り時間
    private bool isChange = false;               //目パチ管理用
    //private bool timerStarted = false;			//タイマースタート管理用

    //float _wait = 0;
    //float _waitt = 90;

    bool isWait;


    enum Status
    {
        Close,
        HalfClose,
        Open    //目パチの状態
    }
    private Status eyeStatus;	//現在の目パチステータス

    // Use this for initialization
    void Start()
    {
        eyeStatus = Status.Open;
        isWait = false;
        isChange = true;
        // ランダム判定用関数をスタートする
        //StartCoroutine ("RandomChange");
    }

    // Update is called once per frame
    void Update()
    {
        if(isWait == true)
        {
            return;
        }
        isWait = true;

        switch (eyeStatus)
        {
            case Status.Open:
                //0.5秒から5秒
                StartCoroutine(Wait(UnityEngine.Random.Range(0.5f, 5f), Status.HalfClose));
                break;
            case Status.HalfClose:
                //0.01秒から0.1秒
                StartCoroutine(Wait(UnityEngine.Random.Range(0.01f, 0.1f), Status.Close));
                break;
            case Status.Close:
                //0.1秒から0.5秒
                StartCoroutine(Wait(UnityEngine.Random.Range(0.05f, 0.2f), Status.Open));
                break;
        }

    }

    private IEnumerator Wait(float time, Status st)
    {
        yield return new WaitForSeconds(time);
        eyeStatus = st;
        isChange = true;
        isWait = false;
    }

    void LateUpdate()
    {
        if (isActive)
        {
            if (isChange)
            {
                switch (eyeStatus)
                {
                    case Status.Close:
                        this.GetComponent<Renderer>().material = Brink2;
                        break;
                    case Status.HalfClose:
                        this.GetComponent<Renderer>().material = Brink1;
                        break;
                    case Status.Open:
                        this.GetComponent<Renderer>().material = Default;
                        break;
                }
                isChange = false;
                //Debug.Log(eyeStatus);
            }
        }
    }
}
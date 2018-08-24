using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{

    private float nextTime;
    public float interval = 0.8f; //点滅周期
    public float nowalpha = 0.8f; //点滅周期
    public float tranceapl; //点滅周期
    public float tranceapldef = 0.01f; //点滅周期

    // Use this for initialization
    void Start()
    {

        nextTime = Time.time;
        tranceapl = tranceapldef;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowalpha == 0)
        {
            tranceapl = tranceapldef;
        }
        else if(nowalpha == 1)
        {
            tranceapl = -tranceapldef * (60 * Time.smoothDeltaTime);
        }

        nowalpha = Mathf.Clamp(nowalpha + tranceapl, 0, 1);

        Color c = GetComponent<Text>().color;
        GetComponent<Text>().color = new Color(c.r, c.g, c.b, nowalpha);

    }
}

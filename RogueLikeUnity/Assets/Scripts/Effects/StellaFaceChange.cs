using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StellaFaceChange : MonoBehaviour
{
    public Material Default;
    public Material Angry1;
    public Material Angry2;
    public GameObject Face;

    //アニメーションEvents側につける表情切り替え用イベントコール
    public void OnCallChangeFace(string str)
    {
        switch(str)
        {
            case "Default":
                Face.GetComponent<Renderer>().material = Default;
                Face.GetComponent<StellaBrink>().isActive = true;
                break;
            case "Angry1":
                Face.GetComponent<Renderer>().material = Angry1;
                Face.GetComponent<StellaBrink>().isActive = false;
                break;
            case "Angry2":
                Face.GetComponent<Renderer>().material = Angry2;
                Face.GetComponent<StellaBrink>().isActive = false;
                break;
        }
    }
}

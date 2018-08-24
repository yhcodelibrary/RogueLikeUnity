using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TitleToggleEvent : MonoBehaviour
{

    public void OnBGM()
    {

        Toggle t = GetComponent<Toggle>();
        if(t.isOn == true)
        {
            MusicInformation.Music.IsMusicOn = true;
            MusicInformation.Music.Setup(MusicInformation.MusicType.Title);
        }
        else
        {
            MusicInformation.Music.IsMusicOn = false;
        }
    }
    public void OnBGMCS()
    {

        Toggle t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            MusicInformation.Music.IsMusicOn = true;
            MusicInformation.Music.Setup(MusicInformation.MusicType.CharacterSelect);
        }
        else
        {
            MusicInformation.Music.IsMusicOn = false;
        }
    }

    public void OnSound()
    {

        Toggle t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            SoundInformation.Sound.IsPlay = true;
            SoundInformation.Sound.Setup();
        }
        else
        {
            SoundInformation.Sound.IsPlay = false;
        }
    }

    public void OnVoice()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return;
        }

        Toggle t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            VoiceInformation.Voice.IsPlay = true;
            VoiceInformation.Voice.Setup();
        }
        else
        {
            VoiceInformation.Voice.IsPlay = false;
        }
    }

    public void OnUseMouse()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return;
        }

        Toggle t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            KeyControlInformation.Info.OpMode = OperationMode.UseMouse;

        }
        else
        {
            KeyControlInformation.Info.OpMode = OperationMode.KeyOnly;
        }
    }

    public void OnUseMouseTitle()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return;
        }

        Toggle t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            KeyControlInformation.Info.OpMode = OperationMode.UseMouse;
            GameObject.Find("PushEnter").GetComponent<Text>().text = string.Format("Push {0} or Mouse Double Click", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());

        }
        else
        {
            KeyControlInformation.Info.OpMode = OperationMode.KeyOnly;
            GameObject.Find("PushEnter").GetComponent<Text>().text = string.Format("Push {0}", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());
        }
    }

    public void OnInitialized()
    {
        SaveDataInformation.InitilizeSystemValue();
        KeyControlInformation.Info = new KeyControlModel();
        SoundInformation.Sound.Volume = 0.7f;
        MusicInformation.Music.Volume = 0.7f;
        VoiceInformation.Voice.Volume = 0.7f;

        GameObject.Find("SystemText").GetComponent<Text>().text =
            "設定情報が初期化されました。";
        GameObject.Find("PushEnter").GetComponent<Text>().text = string.Format("Push {0}", KeyControlModel.GetName(KeyControlInformation.Info.MenuOk).Trim());
    }
}
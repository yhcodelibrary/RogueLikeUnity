using Assets.Scripts.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SystemInformation
{
    public string CharacterName;
    public int BGMVolume;
    public int SoundVolume;
    public int VoiceVolume;
    public bool IsBgm;
    public bool IsSound;
    public bool IsVoice;

    public string GetJsonData()
    {
        string json = JsonMapper.ToJson(this);

        return json;
    }

    public SystemInformation Clone()
    {
        SystemInformation c = new SystemInformation();
        c.CharacterName = this.CharacterName;
        c.BGMVolume = this.BGMVolume;
        c.SoundVolume = this.SoundVolume;
        c.VoiceVolume = this.VoiceVolume;
        c.IsBgm = this.IsBgm;
        c.IsSound = this.IsSound;
        c.IsVoice = this.IsVoice;

        return c;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AttackInformation
{
    private bool IsUpdate { get; set; }
    private BehaviorType BehType { get; set; }
    private List<BaseCharacter> Targets { get; set; }
    private Dictionary<Guid, int> Damages { get; set; }
    private Dictionary<Guid, int> Abnormals { get; set; }
    private Dictionary<Guid, bool> IsHit { get; set; }
    //public Dictionary<Guid, bool> IsDeath { get; private set; }
    private List<BaseCharacter> KillList { get; set; }
    private List<SoundInformation.SoundType> Sounds { get; set; }
    private List<VoiceInformation.VoiceType> Voices { get; set; }
    private PlayerType VoiceType { get; set; }
    private List<EffectBase> Effects { get; set; }
    private List<string> Messages { get; set; }
    
    public AttackInformation()
    {
        IsUpdate = false;
        VoiceType = PlayerInformation.Info.PType;
        BehType = BehaviorType.None;
        Targets = new List<BaseCharacter>();
        Damages = new Dictionary<Guid, int>();
        Abnormals = new Dictionary<Guid, int>();
        IsHit = new Dictionary<Guid, bool>();
        //IsDeath = new Dictionary<Guid, bool>();
        KillList = new List<BaseCharacter>();
        Effects = new List<EffectBase>();
        Sounds = new List<SoundInformation.SoundType>();
        Voices = new List<VoiceInformation.VoiceType>();
        Messages = new List<string>();
    }
    public void SetBehType(BehaviorType t)
    {
        BehType = t;
        IsUpdate = true;
    }
    public void AddTarget(BaseCharacter t)
    {
        Targets.Add(t);
        IsUpdate = true;
    }
    public void AddHit(BaseCharacter target, bool hit)
    {
        IsHit.Add(target.Name, true);
    }

    public void AddKillList(BaseCharacter t)
    {
        KillList.Add(t);
        IsUpdate = true;
    }
    public void AddDamage(Guid name,int t)
    {
        Damages.Add(name, t);
        IsUpdate = true;
    }
    public void AddAbnormal(BaseCharacter target, int t)
    {
        if (Abnormals.ContainsKey(target.Name) == false)
        {
            Abnormals.Add(target.Name, t);
        }
        IsUpdate = true;
    }
    public void AddEffect(EffectBase t)
    {
        Effects.Add(t);
        IsUpdate = true;
    }
    public void AddSound(SoundInformation.SoundType t)
    {
        Sounds.Add(t);
        IsUpdate = true;
    }
    public void SetVoiceType(PlayerType p)
    {
        VoiceType = p;
    }
    public void AddVoice(VoiceInformation.VoiceType t)
    {
        Voices.Add(t);
        IsUpdate = true;
    }
    public void AddMessage(string t)
    {
        Messages.Add(t);
        IsUpdate = true;
    }

    public void Initialize()
    {
        if(IsUpdate == false)
        {
            Clear();
        }
    }
    public void Clear()
    {
        IsUpdate = false;
        VoiceType = PlayerInformation.Info.PType;
        BehType = BehaviorType.None;
        Targets.Clear();
        Damages.Clear();
        Abnormals.Clear();
        IsHit.Clear();
        //IsDeath.Clear();
        KillList.Clear();
        Effects.Clear();
        Sounds.Clear();
        Voices.Clear();
        Messages.Clear();
    }

    public void AttackUpdate(BaseCharacter attaker,ManageDungeon dun)
    {
        if (this.IsUpdate == false)
        {
            return;
        }

        switch (this.BehType)
        {
            case BehaviorType.Attack:

                foreach (BaseCharacter c in this.Targets)
                {
                    //攻撃の命中判定
                    if (this.IsHit[c.Name] == true)
                    {
                        //オプションの攻撃効果
                        foreach (BaseOption op in attaker.Options)
                        {
                            op.DamageAttackEffect(attaker, c, this.Damages[c.Name]);
                        }
                        //オプションの防御効果
                        foreach (BaseOption op in c.Options)
                        {
                            op.DamageDefenceEffect(attaker, c, this.Damages[c.Name]);
                        }
                    }
                }
                break;
        }

        //対象が死亡したら
        foreach (BaseCharacter c in this.KillList)
        {
            //対象の死亡アニメーションを実行
            c.DeathAction(dun);
        }

        //エフェクトの実行
        foreach (EffectBase e in this.Effects)
        {
            e.Play();
        }
        //サウンドの実行
        foreach (SoundInformation.SoundType s in this.Sounds)
        {
            SoundInformation.Sound.Play(s);
        }
        //ボイスの実行
        foreach (VoiceInformation.VoiceType v in this.Voices)
        {
            VoiceInformation.Voice.Play(this.VoiceType, v);
        }

        //メッセージの追加
        foreach (string s in this.Messages)
        {
            DisplayInformation.Info.AddMessage(s);
        }

        this.Clear();
    }
}

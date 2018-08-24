using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseOption : BaseObject
{
    public int Plus;
    public float CommonFloat;
    public int AbnormalStateTarget;

    public ItemType TargetItemType;

    public OptionType OType;
    /// <summary>
    /// 種族対象
    /// </summary>
    public int RaceTarget;

    public BaseOption Clone()
    {
        BaseOption bo = new BaseOption();
        bo.Initialize();
        bo.Plus = this.Plus;
        bo.CommonFloat = this.CommonFloat;
        bo.AbnormalStateTarget = this.AbnormalStateTarget;
        bo.TargetItemType = this.TargetItemType;
        bo.OType = this.OType;
        return bo;
    }

    public override string DisplayNameInItem
    {
        get
        {
            if (Plus > 0)
            {
                return string.Format("{0}+{1}", base.DisplayNameInItem, Plus);
            }
            else
            {
                return base.DisplayNameInItem;
            }
        }
    }

    /// <summary>
    /// オプションによる攻撃力返還
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    public float AttackValue(ManageDungeon dun, BaseCharacter attacker, BaseCharacter target, float itematk)
    {
        float atk = 0;
        switch(OType)
        {
            //攻撃＋
            case OptionType.Attack:
                atk = Plus * CommonFloat;
                break;
                //種族特攻
            case OptionType.AttackRace:
                if((target.Race & RaceTarget) == 0)
                {
                    atk = itematk * Plus * CommonFloat;
                }
                break;
                //逆境
            case OptionType.Adversity:
                if((attacker.CurrentHp/attacker.MaxHp) < 0.2f)
                {
                    atk = itematk * Plus * CommonFloat;
                }
                break;
                //打開
            case OptionType.Breakthrough:
                dun.SetUpCharacterMap();
                if(dun.GetNearCharacters(attacker.CurrentPoint, 1).Count > 2)
                {
                    atk = itematk * Plus * CommonFloat;
                }
                break;
                //鉄塊
            case OptionType.Liquid:
                if(CommonFunction.IsRandom(CommonFloat) == true)
                {
                    atk = target.Defense / 4;
                }
                break;
        }
        return atk;
    }

    /// <summary>
    /// オプションによる防御力返還
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    public float DefenceValue(ManageDungeon dun, BaseCharacter attacker, BaseCharacter target, float itemdef)
    {
        float def = 0;
        switch (OType)
        {
            //防御＋
            case OptionType.Defence:
                def = Plus * CommonFloat;
                break;
            //種族特防
            case OptionType.DefenceRace:
                if ((target.Race & RaceTarget) != 0)
                {
                    def = itemdef * Plus * CommonFloat;
                }
                break;
            //鉄壁
            case OptionType.IronWall:
                dun.SetUpCharacterMap();
                //dun.GetNearCharacters(target.CurrentPoint, 1);
                if (dun.GetNearCharacters(attacker.CurrentPoint, 1).Count > 2)
                {
                    def = itemdef * Plus * CommonFloat;
                }
                break;
        }
        return def;
    }


    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public void DamageAttackEffect(BaseCharacter attacker, BaseCharacter target,int damage)
    {
        switch (OType)
        {
            //攻撃吸収
            case OptionType.AttackRecover:
                int rcv = Mathf.CeilToInt((damage * CommonFloat) * Plus);
                attacker.RecoverHp(rcv);
                EffectDamage d = EffectDamage.CreateObject(attacker);
                d.SetText(rcv.ToString(),AttackState.Heal);
                d.Play();

                break;

        }
    }

    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public void DamageDefenceEffect(BaseCharacter attacker, BaseCharacter target, int damage)
    {
        switch (OType)
        {
            //反射
            case OptionType.Reflection:
                int refl = Mathf.CeilToInt((damage * CommonFloat) * Plus);
                attacker.AddDamage(refl);
                EffectDamage d = EffectDamage.CreateObject(attacker);
                d.SetText(refl.ToString(), AttackState.Hit);
                d.Play();

                break;
            //被弾回復
            case OptionType.DefenceRecover:
                int rec = Mathf.CeilToInt((damage * CommonFloat) * Plus);
                target.RecoverHp(rec);
                EffectDamage r = EffectDamage.CreateObject(target);
                r.SetText(rec.ToString(), AttackState.Heal);
                r.Play();
                break;
        }
    }


    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int DamageAttack(ManageDungeon dun, BaseCharacter attacker, BaseCharacter target, int damage)
    {
        int dmg = damage;
        switch (OType)
        {
            //致命
            case OptionType.Fatal:
                if (target.CurrentHp > damage)
                {
                    if (CommonFunction.IsRandom(CommonFloat * Plus) == true)
                    {
                        dmg = (int)target.CurrentHp - 1;
                    }
                }
                break;

        }
        return dmg;
    }

    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int DamageDefence(BaseCharacter attacker, BaseCharacter target, int damage)
    {
        int dmg = damage;
        switch (OType)
        {
            //樫の盾
            case OptionType.Oakenshield:
                if (damage >= target.CurrentHp)
                {
                    if (CommonFunction.IsRandom(CommonFloat * Plus) == true)
                    {
                        dmg = (int)target.CurrentHp - 1;
                    }
                }
                break;
        }
        return dmg;
    }
    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public float TurnRecoverHp(float rhp)
    {
        float rhpr = 0;
        switch (OType)
        {
            //HP自然回復増加
            case OptionType.WalkRecover:
                rhpr = ((rhp * CommonFloat) * Plus);

                break;
        }
        return rhpr;
    }

    /// <summary>
    /// ダメージ効果の変換
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public float TurnReduceSat(float sat)
    {
        float satr = 0;
        switch (OType)
        {
            //小食
            case OptionType.SatSmall:
                satr = -((sat * CommonFloat) * Plus);

                break;
            //大食
            case OptionType.SatBig:
                satr = ((sat * CommonFloat) * Plus);

                break;
        }
        return satr;
    }

    public float GetHp()
    {
        float r = 0;
        switch (OType)
        {
            //HPプラス
            case OptionType.HP:
                r = +(CommonFloat * Plus);

                break;
        }
        return r;
    }

    public int GetExp(int exp)
    {
        int r = 0;
        switch(OType)
        {
            //経験値プラス
            case OptionType.ExpPlus:
                r = Mathf.CeilToInt(exp * CommonFloat * Plus);

                break;
            //経験値マイナス
            case OptionType.ExpMinus:
                r = Mathf.CeilToInt(exp * CommonFloat * Plus);

                break;
        }
        return r;
    }

    public void SetAbnormalPrevent(Dictionary<StateAbnormal, float> dic)
    {
        switch (OType)
        {
            //異常抵抗
            case OptionType.DefenceAbnormal:
                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    if((AbnormalStateTarget & (int)val) != 0)
                    {
                        float prevent = CommonFloat * Plus;
                        if (dic[val] < prevent)
                        {
                            dic[val] = prevent;
                        }
                    }
                }

                break;
        }
    }
    public float GetAbnormalPrevent(StateAbnormal val,float preventP)
    {
        switch (OType)
        {
            //異常抵抗
            case OptionType.DefenceAbnormal:

                if ((AbnormalStateTarget & (int)val) != 0)
                {
                    float prevent = CommonFloat * Plus;
                    if (preventP < prevent)
                    {
                        preventP = prevent;
                    }
                }
                break;
        }
        return preventP;
    }

    public int SetAbnormalAttack(Dictionary<StateAbnormal, float> dic)
    {
        int ab = 0;
        bool r = false;
        switch (OType)
        {
            //異常付与
            case OptionType.AttackAbnormal:
                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    if ((AbnormalStateTarget & (int)val) != 0)
                    {
                        float prevent = CommonFloat * Plus;
                        if (dic[val] < prevent)
                        {
                            dic[val] = prevent;
                            r = true;
                            ab = (int)val;
                        }
                    }
                }

                break;
        }
        return ab;
    }
    public float DexTrap()
    {
        float r = 0;
        switch (OType)
        {
            case OptionType.DexTrap:
                r = CommonFloat * Plus;
                break;
        }
        return r;
    }
    public bool DeathRevive()
    {
        bool r = false;
        switch (OType)
        {
            case OptionType.Sacrifice:
                r = true;
                break;
        }
        return r;
    }

    public void FinishTurn(BaseCharacter target)
    {
        switch(OType)
        {
            case OptionType.Narcolepsy:
                if(CommonFunction.IsRandom((CommonFloat * Plus)) == true)
                {
                    int st = target.AddStateAbnormal((int)StateAbnormal.Sleep);
                    if(st != 0)
                    {
                        DisplayInformation.Info.AddMessage(
                            CommonFunction.GetAbnormalMessage(StateAbnormal.Sleep, target));
                    }
                }
                break;
        }
    }

    public bool IsBreak()
    {
        bool r = false;
        switch(OType)
        {
            case OptionType.Glass:
                if(CommonFunction.IsRandom(CommonFloat) == true)
                {
                    r = true;
                }
                break;
        }
        return r;
    }
}

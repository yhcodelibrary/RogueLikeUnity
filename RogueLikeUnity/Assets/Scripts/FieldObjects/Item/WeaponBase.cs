using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponBase : BaseItem
{
    public override string DisplayNameNormal
    {
        get
        {
            if (StrengthValue > 0)
            {
                return string.Format(CommonConst.Format.StrengthItemName, base.DisplayNameNormal, StrengthValue);
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
        }
    }


    public override string DisplayNameInItem
    {
        get
        {
            if (IsAnalyse == true)
            {
                if (StrengthValue > 0)
                {
                    return string.Format(CommonConst.Format.StrengthItemName, base.DisplayNameNormal, StrengthValue);
                }
                else
                {
                    return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
                }
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultNameWithColor, DisplayNameBefore, DisplayName, DisplayNameAfter
                    ,CommonConst.Color.NotAnalyse);
            }
        }
    }

    public override string DisplayNameInMessage
    {
        get
        {
            if (IsAnalyse == true)
            {
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
            else
            {
                return string.Format(CommonConst.Format.DefaultNameWithColor, DisplayNameBefore, DisplayName, DisplayNameAfter,
                    CommonConst.Color.NotAnalyse);
            }
        }
    }

    /// <summary>
    /// 基本攻撃力
    /// </summary>
    public float WeaponBaseAttack;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public override float ItemAttack
    {
        get
        {
            float value = WeaponBaseAttack;
            value += StrengthValue;

            return value;
        }
    }

    /// <summary>
    /// 基本命中率(0-1)
    /// </summary>
    public float WeaponBaseDexterity;

    /// <summary>
    /// 命中率(0-1)
    /// </summary>
    public float WeaponDexterity
    {
        get { return WeaponBaseDexterity; }
    }

    /// <summary>
    /// クリティカル命中率(0-1)
    /// </summary>
    public float WeaponBaseDexterityCritical;

    /// <summary>
    /// クリティカル命中率(0-1)
    /// </summary>
    public float WeaponDexterityCritical
    {
        get
        {
            return WeaponBaseDexterityCritical;
        }
    }

    /// <summary>
    /// クリティカル
    /// </summary>
    public float WeaponBaseCritical;

    /// <summary>
    /// クリティカル
    /// </summary>
    public float WeaponCritical
    {
        get
        {
            return WeaponBaseCritical;
        }
    }

    /// <summary>
    /// オブジェクト実体
    /// </summary>
    public GameObject WeaponObject;

    /// <summary>
    /// 武器の見た目種類
    /// </summary>
    public WeaponAppearanceType ApType;

    /// <summary>
    /// 攻撃することによって発動するステータス異常
    /// </summary>
    public int AddAbnormal;

    /// <summary>
    /// ステータス異常ごとの発生確率
    /// </summary>
    public Dictionary<StateAbnormal,float> AddAbnormalProb;

    /// <summary>
    /// 武器が与えた総ダメージ
    /// </summary>
    public int TotalDamage;

    /// <summary>
    /// 武器射程
    /// </summary>
    private ushort _Range;
    public ushort Range
    {
        get { return _Range; }
        set { _Range = value; }
    }

    public Vector3 _ObjectVector;
    /// <summary>
    /// プレイヤーに設定する際の位置
    /// </summary>
    public virtual Vector3 ObjectVector
    {
        get
        {
            if(CommonFunction.IsNull(_ObjectVector) == true)
            {
                return Vector3.zero;
            }
            return _ObjectVector;
        }
    }

    public Quaternion _ObjectQuaternion;
    /// <summary>
    /// プレイヤーに設定する際の回転
    /// </summary>
    public virtual Quaternion ObjectQuaternion
    {
        get
        {
            if (CommonFunction.IsNull(_ObjectQuaternion) == true)
            {
                return new Quaternion(0, 0, 0, 0);
            }
            return _ObjectQuaternion;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonWepon";
        IType = ItemType.Weapon;
        IsDriveProb = true;
        _Range = 1;
        ApType = WeaponAppearanceType.None;
        AddAbnormalProb = new Dictionary<StateAbnormal, float>();
        //AddAbnormalProb.Add(StateAbnormal.Normal, 0);
        //AddAbnormalProb.Add(StateAbnormal.Charmed, 0);
        //AddAbnormalProb.Add(StateAbnormal.Confusion, 0);
        //AddAbnormalProb.Add(StateAbnormal.Dark, 0);
        //AddAbnormalProb.Add(StateAbnormal.DeadlyPoison, 0);
        //AddAbnormalProb.Add(StateAbnormal.Palalysis, 0);
        //AddAbnormalProb.Add(StateAbnormal.Poison, 0);
        //AddAbnormalProb.Add(StateAbnormal.Sleep, 0);

        foreach (StateAbnormal val in CommonFunction.StateAbnormals)
        {
            AddAbnormalProb.Add(val, 0);
        }
        Options = new List<BaseOption>();
        TotalDamage = 0;
        AddAbnormal = 0;
    }

    public override bool Equip(BaseCharacter target)
    {

        base.Equip(target);

        return ForceEquip(target);
    }


    public override bool ForceEquip(BaseCharacter target)
    {
        //ClearAnalyse();

        //インスタンスのコピーを作成
        switch (ApType)
        {
            case WeaponAppearanceType.Sword:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipSword",
                    target.EquipRight.transform);
                //UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipSword").gameObject);
                break;
            case WeaponAppearanceType.Staff:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipStaff",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipStaff").gameObject);
                break;
            case WeaponAppearanceType.Knife:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipKnife",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipKnife").gameObject);
                break;
            case WeaponAppearanceType.Axe:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipAxe",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipAxe").gameObject);
                break;
            case WeaponAppearanceType.Mace:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipMace",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipMace").gameObject);
                break;
            case WeaponAppearanceType.Bat:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipBat",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipBat").gameObject);
                _ObjectVector = new Vector3(-0.013f, 0.097f, -0.012f);
                _ObjectQuaternion = new Quaternion(-0.5976952f, 0, 0, 0.8017235f);
                break;
            case WeaponAppearanceType.HockeyStick:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipHockeyStick",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipHockeyStick").gameObject);
                _ObjectVector = new Vector3(-0.006f, 0.089f, -0.001f);
                _ObjectQuaternion = new Quaternion(-0.5015087f, 0.4965344f, -0.5312584f, -0.4687292f);
                break;
            case WeaponAppearanceType.Plunger:
                WeaponObject = ResourceInformation.GetInstance("UnityEquipPlunger",
                    target.EquipRight.transform);
                //WeaponObject = UnityEngine.Object.Instantiate(ResourceInformation.UnityEquip.transform.FindChild("UnityEquipPlunger").gameObject);
                _ObjectVector = new Vector3(-0.011f, 0.092f, -0.011f);
                _ObjectQuaternion = new Quaternion(-0.0183135f, -0.7184151f, 0.6577789f, -0.2255467f);
                break;

        }

        // 右手の子オブジェクトとして登録する
        //WeaponObject.transform.SetParent(target.EquipRight.transform.transform);

        //位置を0クリア
        WeaponObject.transform.localPosition = ObjectVector;
        WeaponObject.transform.localRotation = ObjectQuaternion;

        //装備中にする
        IsEquip = true;

        ResetEquipOptionStatus(target);

        return true;
    }

    //public override bool ForceRemoveEquip(BaseCharacter target)
    //{
    //    base.ForceRemoveEquip(target);
    //    IsEquip = false;
    //    Destroy(WeaponObject);

        //    ResetEquipOptionStatus(target);

        //    return true;
        //}

    public override bool ForceRemoveEquip(BaseCharacter target)
    {
        RemoveEquipUpdateStatus(target);

        RemoveEquipDestroyObject();

        return true;
    }
    public override void RemoveEquipDestroyObject()
    {
        GameObject.Destroy(WeaponObject);
        WeaponObject = null;
    }

    /// <summary>
    /// 装備解除時に画面オブジェクトを削除する
    /// </summary>
    public override bool RemoveEquip(BaseCharacter target,bool isOutMessage = true)
    {
        bool result = base.RemoveEquip(target, isOutMessage);
        if (result == true)
        {
            GameObject.Destroy(WeaponObject);

            ResetEquipOptionStatus(target);
        }

        return result;
    }

    public virtual void AttackEffect(BaseCharacter target, BaseCharacter attacker,string damage, AttackState state, AttackInformation atinfo)
    {
        EffectDamage d = EffectDamage.CreateObject(target);
        d.SetText(damage, state);
        atinfo.AddEffect(d);
    }
    public virtual AttackState Attack(ManageDungeon dun,BaseCharacter target, BaseCharacter attacker,int power,AttackInformation atinf)
    {
        int damage = 0;
        atinf.AddTarget(target);

        //声を鳴らす
        atinf.AddVoice(attacker.VoiceAttack());

        //攻撃の命中判定
        if (CommonFunction.IsRandom(WeaponDexterity) == true)
        {
            //行動タイプを設定
            atinf.SetBehType(BehaviorType.Attack);

            //命中したら
            atinf.AddHit(target, true);

            //声を鳴らす
            atinf.AddVoice(target.VoiceDefence());

            //サウンドを鳴らす
            atinf.AddSound(GetAttackHitSound());

            BaseOption[] atoptions = attacker.Options;
            BaseOption[] tgoptions = target.Options;
            
            //与ダメージを計算
            damage = CalcDamage(dun, attacker, target, power, atoptions, tgoptions, 1);

            //スコア関連値の更新
            if (target.Type == ObjectType.Enemy)
            {
                TotalDamage += damage;
                if(ScoreInformation.Info.MostUseWeaponDamage < TotalDamage)
                {
                    ScoreInformation.Info.MostUseWeaponDamage = TotalDamage;
                    ScoreInformation.Info.MostUseWeaponName = DisplayNameNormal;
                }

                ScoreInformation.Info.AddScore(damage);
            }

            //atinf.AddDamage(target.Name, damage);

            ////ヒットメッセージ
            //atinf.AddMessage(
            //    target.GetMessageAttackHit(this.DisplayNameInMessage, damage));

            ////ダメージ判定
            //AttackState atState = target.AddDamage(damage);
            //ダメージ追加
            AttackState atState = CommonFunction.AddDamage(atinf, attacker, target, damage);

            //ガラスアイテム判定
            WeaponBase atw = attacker.EquipWeapon;
            foreach (BaseOption op in atw.Options)
            {
                if(op.IsBreak() == true)
                {
                    atinf.AddSound(SoundInformation.SoundType.Break);

                    atinf.AddMessage(
                        string.Format(CommonConst.Message.BreakItem, atw.DisplayNameInMessage));

                    atw.ForceRemoveEquip(attacker);
                    PlayerCharacter.RemoveItem(atw);
                }
            }
            ShieldBase tgs = target.EquipShield;
            foreach (BaseOption op in tgs.Options)
            {
                if (op.IsBreak() == true)
                {
                    atinf.AddSound(SoundInformation.SoundType.Break);

                    atinf.AddMessage(
                        string.Format(CommonConst.Message.BreakItem, tgs.DisplayNameInMessage));

                    tgs.ForceRemoveEquip(target);
                    PlayerCharacter.RemoveItem(tgs);
                }
            }


            //対象が死亡したら
            if (atState == AttackState.Death)
            {
                atinf.AddKillList(target);

                atinf.AddMessage(
                    target.GetMessageDeath(target.HaveExperience));
            }
            else
            {
                //atinf.IsDeath.Add(target.Name, false);

                addabnormal.Clear();
                prevents.Clear();
                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    prevents.Add(val, 0);
                    addabnormal.Add(val, AddAbnormalProb[val]);
                }

                int opab = 0;
                //オプションの付加値設定
                foreach (BaseOption o in atoptions)
                {
                    int ab = o.SetAbnormalAttack(addabnormal);
                    opab = opab | ab;
                }

                int tarabn = AddAbnormal | opab;

                if (tarabn != 0)
                {
                    //オプションによる状態異常の取得
                    //foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                    //{
                    //    addabnormal.Add(val, 0);
                    //}

                    //オプションの抵抗値設定
                    foreach (BaseOption o in tgoptions)
                    {
                        o.SetAbnormalPrevent(prevents);
                    }

                    int abn = 0;
                    //状態異常の付与
                    foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                    {
                        int abnormal = (int)val & tarabn;

                        //対象の状態異常を処理
                        if (abnormal != 0)
                        {
                            //float prob = prevents[val] > AddAbnormalProb[val] ? prevents[val] : AddAbnormalProb[val];
                            //状態異常付与が成功したとき
                            if (CommonFunction.IsRandom(addabnormal[val]) == true)
                            {
                                //抵抗に成功したとき
                                if (CommonFunction.IsRandom(prevents[val]) == true)
                                {

                                }
                                else
                                {
                                    int addstate = target.AddStateAbnormal(abnormal);
                                    if (addstate != 0)
                                    {
                                        abn += abnormal;
                                        //atinf.AddAbnormal(target, abnormal);

                                        atinf.AddMessage(
                                            CommonFunction.GetAbnormalMessage(val, target));
                                    }
                                }
                            }
                        }
                    }
                    if(abn != 0)
                    {
                        atinf.AddEffect(EffectBadSmoke.CreateObject(target));
                        atinf.AddAbnormal(target, abn);
                    }
                }
            }

            //エフェクトをかける
            this.AttackEffect(target, attacker, damage.ToString(), AttackState.Hit, atinf);

            return atState;
        }
        else
        {
            //外れた場合
            atinf.AddHit(target, false);

            EffectDamage d = EffectDamage.CreateObject(target);
            d.SetText("Miss", AttackState.Miss);
            atinf.AddEffect(d);

            atinf.AddSound(GetAttackMissSound());

            atinf.AddMessage(
                target.GetMessageAttackMiss());
            return AttackState.Miss;
        }
    }
    static Dictionary<StateAbnormal, float> addabnormal = new Dictionary<StateAbnormal, float>(new StateAbnormalComparer());
    static Dictionary<StateAbnormal, float> prevents = new Dictionary<StateAbnormal, float>(new StateAbnormalComparer());

    public int CalcDamage(ManageDungeon dun,BaseCharacter attacker,BaseCharacter target,int power,BaseOption[] atoptions, BaseOption[] tgoptions,float magnification)
    {
        int damage = 0;

        //オプションの攻撃力を取得
        float optAtk = 0;
        foreach (BaseOption op in atoptions)
        {
            optAtk += op.AttackValue(dun, attacker, target, ItemAttack);
        }
        //オプションの防御力を取得
        float optdef = 0;
        foreach (BaseOption op in tgoptions)
        {
            optdef += op.DefenceValue(dun, attacker, target, target.Defense);
        }

        //クリティカルの命中判定
        if (CommonFunction.IsRandom(WeaponDexterityCritical) == true)
        {
            damage = CommonFunction.CalcDamage(attacker.BaseAttack, (ItemAttack + optAtk) * magnification, power, target.Defense + optdef, WeaponCritical);
        }
        else
        {
            damage = CommonFunction.CalcDamage(attacker.BaseAttack, (ItemAttack + optAtk) * magnification, power, target.Defense + optdef);
        }

        //オプションの攻撃を取得
        foreach (BaseOption op in atoptions)
        {
            damage = op.DamageAttack(dun, attacker, target, damage);
        }
        //オプションの防御を取得
        foreach (BaseOption op in tgoptions)
        {
            damage = op.DamageDefence(attacker, target, damage);
        }
        return damage;
    }

    protected virtual SoundInformation.SoundType GetAttackHitSound()
    {
        return SoundInformation.SoundType.AttackHit;
    }
    protected virtual SoundInformation.SoundType GetAttackMissSound()
    {
        return SoundInformation.SoundType.AttackMiss;
    }

    public override Dictionary<int, MenuItemActionType> GetItemAction()
    {
        int i = 0;
        Dictionary<int, MenuItemActionType> dic = new Dictionary<int, MenuItemActionType>();

        bool has = PlayerCharacter.HasItemPlayer(this);
        if (has == false && GameStateInformation.Info.IsThrowAway == false)
        {
            dic.Add(i++, MenuItemActionType.Get);//拾う
        }
        if (IsEquip ==true)
        {
            dic.Add(i++, MenuItemActionType.RemoveEquip);//外す
        }
        else
        {
            dic.Add(i++, MenuItemActionType.Equip);//装備
        }
        //if ((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Throw);//投げる

        if (Options.Count >= 1)
        {
            dic.Add(i++, MenuItemActionType.LookOption);//オプション参照
        }
        if (has == true)
        {
            dic.Add(i++, MenuItemActionType.Put);//置く
        }

        //ドライブに空きがあれば入れるを追加
        //if (PlayerCharacter.HasDriveGap() == true)
        //{
        //    dic.Add(i++, MenuItemActionType.Putin);
        //}
        return dic;
    }
    
    public override string GetAttack()
    {
        if (_IsAnalyse == true)
        {
            string result = Mathf.Round(WeaponBaseAttack).ToString();
            if (StrengthValue != 0)
            {
                result += string.Format("+{0}", StrengthValue);
            }
            return result;
        }
        else
        {
            return "?";
        }
    }
}

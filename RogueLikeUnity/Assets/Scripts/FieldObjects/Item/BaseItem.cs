using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseItem : BaseObject
{

    /// <summary>
    /// 強化値
    /// </summary>
    public int StrengthValue;

    /// <summary>
    /// アイテムの種別
    /// </summary>
    public ItemType IType;

    public long SortNo;

    /// <summary>
    /// 売値
    /// </summary>
    public int SellingPrice;
    
    /// <summary>
    /// ドライブに格納できるかどうか
    /// </summary>
    public bool IsDriveProb;

    /// <summary>
    /// バグっているかどうか（呪われているかどうか）
    /// </summary>
    //public bool IsBug;
    
    /// <summary>
    /// 装備中かどうか
    /// </summary>
    public bool IsEquip;

    /// <summary>
    /// ドライブに格納されているかどうか
    /// </summary>
    public bool IsDrive
    {
        get
        {
            if(CommonFunction.IsNull(InDrive) == true)
            {
                return false;
            }
            return true;
        }
    }

    public float Weight = 1;

    protected float PosYThrow = 3.2f;
    /// <summary>
    /// 高さのデフォルト値
    /// </summary>
    public override float PosY
    {
        get
        {
            if (IsActive == true)
            {
                if(IsThrowAfterAction == true)
                {
                    return PosYThrow;
                }
                else
                {
                    return _PosY;
                }
            }
            else
            {
                return _PosYNoneActive;
            }
        }
        set { _PosY = value; }
    }

    /// <summary>
    /// 格納されているドライブ
    /// </summary>
    public BagBase InDrive;

    /// <summary>
    /// 投擲時命中率
    /// </summary>
    public float ThrowDexterity;

    public MapPoint ThrowActionPoint
    {
        get;private set;
    }

    public BaseCharacter ThrowAfterActionTarget;
    /// <summary>
    /// 投擲後のアクションが実行済みかどうか
    /// </summary>
    public bool IsThrowAfterAction
    {
        get
        {
            return CommonFunction.IsNull(ThrowActionPoint) == false;
        }
        set
        {
            if(value == true)
            {
                ThrowActionPoint = ThrowPoint;
            }
            else
            {
                ThrowActionPoint = null;
            }
        }
    }

    /// <summary>
    /// 防御力
    /// オプション値も含む
    /// </summary>
    public virtual float ItemDefence
    {
        get { return 0; }
    }

    /// <summary>
    /// 攻撃力
    /// オプション値も含む
    /// </summary>
    public virtual float ItemAttack
    {
        get { return 0; }
    }

    /// <summary>
    /// 持っているオプション
    /// </summary>
    //public Dictionary<Guid, BaseOption> Options;
    public List<BaseOption> Options;


    #region  ステータス更新系
    /// <summary>
    /// 満腹度回復値
    /// </summary>
    public float SatRecoverPoint;

    /// <summary>
    /// Hp回復値
    /// </summary>
    public float HpRecoverPoint;

    /// <summary>
    /// ステータス回復対象
    /// </summary>
    public int StatusRecoverTarget;

    /// <summary>
    /// ステータス悪化対象
    /// </summary>
    public int StatusBadTarget;

    /// <summary>
    /// 鑑定済みかどうか
    /// </summary>
    protected bool _IsAnalyse;
    public virtual bool IsAnalyse
    {
        get
        {
            return _IsAnalyse;
        }
        set
        {
            _IsAnalyse = value;
        }
    }
    public virtual void ClearAnalyse()
    {
        _IsAnalyse = true;
    }

    public override string DisplayNameInMessage
    {
        get
        {
            if(IsAnalyse == true)
            {
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
            else
            {
                return string.Format(CommonConst.Format.UnknownNameWithColor,
                    GameStateInformation.Info.GetUnknownName(ObjNo),
                    CommonConst.Color.NotAnalyse);
            }
        }
    }

    #endregion ステータス更新系

    public DateTime Timestamp;

    public virtual long GetSortNo()
    {
        return ObjNo;
    }

    public override void Initialize()
    {
        base.Initialize();
        Type = ObjectType.Item;
        IsEquip = false;
        PosY = 2.58f;
        SortNo = ObjNo;
        StatusRecoverTarget = 0;
        StatusBadTarget = 0;
        MoveSpeed = CommonConst.SystemValue.MoveSpeedItemThrow;
        IsThrowAfterAction = false;
        StrengthValue = 0;
        ThrowActionPoint = null;
        SellingPrice = 10;
        Weight = 1;
        IsAnalyse = true;
    }



    public virtual void SetInstanceName()
    {

    }
    public virtual void SetThrowInstanceName()
    {

    }
    #region　使用処理

    /// <summary>
    /// 装備
    /// </summary>
    public virtual bool Equip(BaseCharacter target)
    {

        SoundInformation.Sound.Play(SoundInformation.SoundType.Equip);

        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.EquipItem, DisplayNameInMessage));

        if (CommonFunction.HasOptionType(Options, OptionType.Adhesive))
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.ItemAdhesive, DisplayNameInMessage));
        }

        return true;
    }

    public virtual bool ForceEquip(BaseCharacter target)
    {
        return true;
    }

    /// <summary>
    /// 強制的に外す
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool ForceRemoveEquip(BaseCharacter target)
    {
        return true;
    }

    public bool RemoveEquipUpdateStatus(BaseCharacter target)
    {
        IsEquip = false;

        ResetEquipOptionStatus(target);

        return true;
    }
    public virtual void RemoveEquipDestroyObject()
    {

    }

    /// <summary>
    /// 外す
    /// </summary>
    public virtual bool RemoveEquip(BaseCharacter target,bool isOutMessage = true)
    {

        if (CommonFunction.HasOptionType(Options, OptionType.Adhesive))
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.ItemAdhesiveNotRemove, DisplayNameInMessage));

            return false;
        }
        else
        {
            if (isOutMessage == true)
            {

                DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.RemoveEquipItem, DisplayNameInMessage));
            }

            //装備解除する
            IsEquip = false;

            return true;

        }
    }

    /// <summary>
    /// 食べる
    /// </summary>
    public virtual bool Eat(ManageDungeon dun, BaseCharacter player)
    {
        return true;
    }

    /// <summary>
    /// 奏でる
    /// </summary>
    public virtual bool Melody(ManageDungeon dun, PlayerCharacter player)
    {
        return true;
    }

    /// <summary>
    /// 使う
    /// </summary>
    public virtual bool Use(ManageDungeon dun, BaseCharacter player)
    {
        return true;
    }

    /// <summary>
    /// 投げる開始
    /// </summary>
    public virtual bool ThrowStart(PlayerCharacter player)
    {
        
        DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.ThrowAction, this.DisplayNameInMessage));
        IsThrowAfterAction = true;
        base.ResetPositionThrowStart();
        return true;
    }

    /// <summary>
    /// 投げる開始
    /// </summary>
    public virtual bool ThrowStartSpecial(PlayerCharacter player, BaseCharacter target)
    {
        return false;
    }
    /// <summary>
    /// 投げる効果（誰かに当たった時）
    /// </summary>
    public virtual bool ThrowAction(ManageDungeon dun, PlayerCharacter player, BaseCharacter target)
    {
        if (CommonFunction.IsRandom(ThrowDexterity) == true)
        {
            //あたりメッセージ
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.HitThrowAfter, target.DisplayNameInMessage));

            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

            int damage = Mathf.CeilToInt((ItemAttack + ItemDefence + StrengthValue) * 2);
            AttackState atState = target.AddDamage(damage);

            //ダメージエフェクト
            EffectDamage d = EffectDamage.CreateObject(target);
            d.SetText(damage.ToString(), AttackState.Hit);
            d.Play();

            //ヒットメッセージ
            DisplayInformation.Info.AddMessage(
                target.GetMessageAttackHit(damage));

            //対象が死亡したら
            if (atState == AttackState.Death)
            {
                DisplayInformation.Info.AddMessage(
                    target.GetMessageDeath(target.HaveExperience));

                if(target.Type == ObjectType.Player)
                {
                    ScoreInformation.Info.CauseDeath =
                        string.Format(CommonConst.DeathMessage.Throw, DisplayNameNormal);
                    ScoreInformation.Info.CauseDeathType = DeathCouseType.Throw;
                }

                player.Death(target, player.AttackInfo);
                target.DeathAction(dun);
            }

            IsThrowBreak = true;

            return true;
        }
        else
        {
            //サウンドを鳴らす
            SoundInformation.Sound.Play(SoundInformation.SoundType.Put);

            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.AttackMissPlayer, target.DisplayNameInMessage));
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.DropItem, this.DisplayNameInMessage));
            return false;
        }
    }

    /// <summary>
    /// 投げる効果（誰にも当たらなかったとき）
    /// </summary>
    public virtual bool ThrowDrop(ManageDungeon dun, PlayerCharacter player)
    {
        if(IsThrowVanish == true)
        {
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.VanishItem, this.DisplayNameInMessage));
            return true;
        }
        else if(dun.Dungeon.DungeonMap.Get(this.CurrentPoint).State == LoadStatus.Water)
        {
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.WaterInItem, this.DisplayNameInMessage));
            return true;
        }
        else
        {
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.DropItem, this.DisplayNameInMessage));
            return false;

        }
    }
    #endregion 使用処理

    public void ResetEquipOptionStatus(BaseCharacter target)
    {

        //オプションによるHP補正のチェック
        float hp = 0;
        foreach (BaseOption o in target.Options)
        {
            hp += o.GetHp();
        }
        target.MaxHpOption = hp;
    }


    /// <summary>
    /// アイテムの上に移動した場合
    /// </summary>
    /// <returns></returns>
    public virtual bool GetOnItem()
    {
        if(PlayerCharacter.IsGetItem(this) == false || GameStateInformation.Info.IsThrowAway == true)
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.OnItem, DisplayNameInMessage));
            return false;
        }
        else
        {
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.GetItem, DisplayNameInMessage));
            return true;
        }
    }
    /// <summary>
    /// アイテムの上に乗った場合
    /// </summary>
    /// <returns></returns>
    public virtual void OnItem()
    {
        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.OnItem, DisplayNameInMessage));
    }
    /// <summary>
    /// 拾う
    /// </summary>
    /// <returns></returns>
    public virtual void GetItem()
    {
        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.GetItem, DisplayNameInMessage));
    }
    /// <summary>
    /// 拾えなかった場合
    /// </summary>
    /// <returns></returns>
    public virtual void FailGetItem()
    {
        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.FailGetItem));
    }

    /// <summary>
    /// 使う全般
    /// </summary>
    public bool CommonUseItem(BaseCharacter player)
    {

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.Recover);

        //満腹度の回復
        if (SatRecoverPoint > 0)
        {
            if(typeof(PlayerCharacter) == player.GetType())
            {
                ((PlayerCharacter)player).AddSatiety(SatRecoverPoint);
                if (((PlayerCharacter)player).IsSatietyMax == true)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.RecoverSatietyMax));
                }
                else
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.RecoverSatiety));
                }
            }
        }
        //HPの回復
        if (HpRecoverPoint > 0)
        {
            player.RecoverHp(HpRecoverPoint);

            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.RecoverHp, player.DisplayNameInMessage));

            //回復エフェクト
            EffectDamage d = EffectDamage.CreateObject(player);
            d.SetText(HpRecoverPoint.ToString(), AttackState.Heal);
            d.Play();

        }
        //状態の回復
        if (StatusRecoverTarget != 0)
        {

            foreach (StateAbnormal val in CommonFunction.StateAbnormals)
            {
                //対象の状態異常を含んでいなければ無視
                if ((StatusRecoverTarget & (int)val) == 0)
                {
                    continue;
                }

                //状態異常の回復に成功したら
                int ret = player.RecoverState((int)val);
                if (ret != 0)
                {
                    player.RemoveAbnormalObject(val);
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.RecoverState, CommonFunction.StateNames[val]));
                }
            }
        }

        //状態異常の付与
        if (StatusBadTarget != 0)
        {
            int state = player.AddStateAbnormal(StatusBadTarget);

            bool isBad = false;

            foreach (StateAbnormal val in CommonFunction.StateAbnormals)
            {
                if((state & (int)val) != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(val, player));

                    if(CommonFunction.BadAbnormals.Contains(val))
                    {
                        isBad = true;
                    }
                }
            }
            if(state != 0)
            {
                if (isBad == true)
                {
                    EffectBadSmoke.CreateObject(player).Play();
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);
                }
                else
                {
                    EffectSmoke.CreateObject(player).Play();
                }

            }
        }
        return true;
    }

    /// <summary>
    /// 使う全般
    /// </summary>
    public bool CommonUseItemCustom(ManageDungeon dun, BaseCharacter center, BaseCharacter player)
    {

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.Recover);

        //範囲拡大がついているとき
        int plus;
        float cf;
        int dist = 0;
        if (CommonFunction.HasOptionType(this.Options,OptionType.Wildfire,out plus,out cf) == true)
        {
            dist = plus;
        }
        dun.SetUpCharacterMap();
        List<BaseCharacter> targets = dun.GetNearCharacters(center.CurrentPoint, dist, true);

        foreach (BaseCharacter target in targets)
        {
            //満腹度の回復
            if (SatRecoverPoint > 0)
            {
                if (typeof(PlayerCharacter) == target.GetType())
                {

                    float satRecoverPoint = SatRecoverPoint;

                    //効果増幅があったら
                    if (CommonFunction.HasOptionType(this.Options, OptionType.EffectUp, out plus, out cf) == true)
                    {
                        satRecoverPoint = satRecoverPoint + plus * cf;
                    }
                    if (CommonFunction.HasOptionType(this.Options, OptionType.EffectDown, out plus, out cf) == true)
                    {
                        satRecoverPoint = satRecoverPoint - plus * cf;
                    }
                    //ダメージ増減30%
                    float rand = 0.3f;
                    if (CommonFunction.HasOptionType(this.Options, OptionType.EffectStabile, out plus, out cf) == true)
                    {
                        rand -= cf * plus;
                    }
                    else if (CommonFunction.HasOptionType(this.Options, OptionType.EffectNotStabile, out plus, out cf) == true)
                    {
                        rand += cf * plus;
                    }
                    satRecoverPoint += Mathf.CeilToInt(UnityEngine.Random.Range(-rand * satRecoverPoint, rand * satRecoverPoint));


                    //減少判定の時
                    if (CommonFunction.HasOptionType(this.Options, OptionType.ReverceDamage) == true)
                    {
                        ((PlayerCharacter)target).ReduceSatiety(satRecoverPoint);
                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.TrapCroquette2, target.DisplayNameInMessage));
                    }
                    //通常回復判定のとき
                    else
                    {
                        ((PlayerCharacter)target).AddSatiety(satRecoverPoint);
                        if (((PlayerCharacter)target).IsSatietyMax == true)
                        {
                            DisplayInformation.Info.AddMessage(
                                string.Format(CommonConst.Message.RecoverSatietyMax));
                        }
                        else
                        {
                            DisplayInformation.Info.AddMessage(
                                string.Format(CommonConst.Message.RecoverSatiety));
                        }
                    }
                }
            }
            //HPの回復
            if (HpRecoverPoint > 0)
            {
                //ダメージ追加
                int damage = Mathf.FloorToInt(HpRecoverPoint/ (center.CurrentPoint.DistanceAbs(target.CurrentPoint) + 1));

                //効果増幅があったら
                if (CommonFunction.HasOptionType(this.Options, OptionType.EffectUp, out plus, out cf) == true)
                {
                    damage = damage + plus * (int)cf;
                }
                if (CommonFunction.HasOptionType(this.Options, OptionType.EffectDown, out plus, out cf) == true)
                {
                    damage = damage - plus * (int)cf;
                }
                //ダメージ増減30%
                float rand = 0.3f;
                if (CommonFunction.HasOptionType(this.Options, OptionType.EffectStabile, out plus, out cf) == true)
                {
                    rand -= cf * plus;
                }
                else if (CommonFunction.HasOptionType(this.Options, OptionType.EffectNotStabile, out plus, out cf) == true)
                {
                    rand += cf * plus;
                }
                damage += Mathf.CeilToInt(UnityEngine.Random.Range(-rand * damage, rand * damage));


                //減少判定の時
                if (CommonFunction.HasOptionType(this.Options, OptionType.ReverceDamage) == true)
                {
                    AttackState atState = target.AddDamage(damage);

                    //ダメージエフェクト
                    EffectDamage d = EffectDamage.CreateObject(target);
                    d.SetText(damage.ToString(), AttackState.Hit);
                    d.Play();

                    //ヒットメッセージ
                    DisplayInformation.Info.AddMessage(
                            target.GetMessageAttackHit(damage));

                    //対象が死亡したら
                    if (atState == AttackState.Death)
                    {
                        DisplayInformation.Info.AddMessage(
                                        target.GetMessageDeath(target.HaveExperience));
                        if (target.Type == ObjectType.Player)
                        {
                            ScoreInformation.Info.CauseDeath =
                                string.Format(CommonConst.DeathMessage.Item, this.DisplayNameNormal);

                            ScoreInformation.Info.CauseDeathType = DeathCouseType.Item;

                            target.Death();
                        }
                        else
                        {
                            player.Death(target, player.AttackInfo);
                        }
                        target.DeathAction(dun);
                    }
                }
                //通常回復判定のとき
                else
                {
                    target.RecoverHp(HpRecoverPoint);

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.RecoverHp, target.DisplayNameInMessage));

                    //回復エフェクト
                    EffectDamage d = EffectDamage.CreateObject(target);
                    d.SetText(HpRecoverPoint.ToString(), AttackState.Heal);
                    d.Play();
                }
            }

            //Power回復
            if (CommonFunction.HasOptionType(this.Options, OptionType.RecoverPower) == true)
            {
                if (target.RecoverPowerNotMaxup(1) == true)
                {
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.RecoverPower, target.DisplayNameInMessage));
                }
            }
        }
        //状態異常の付与
        EffectAbnormal(targets);

        return true;
    }

    protected void EffectAbnormal(BaseCharacter target)
    {

        int statusRecoverTarget = CommonFunction.GetDefenceAbnormals(this.Options);
        if (statusRecoverTarget != 0)
        {
            //状態の回復
            if (CommonFunction.HasOptionType(this.Options, OptionType.ReverceAbnormal) == false)
            {
                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    //対象の状態異常を含んでいなければ無視
                    if ((statusRecoverTarget & (int)val) == 0)
                    {
                        continue;
                    }

                    //状態異常の回復に成功したら
                    int ret = target.RecoverState((int)val);
                    if (ret != 0)
                    {
                        target.RemoveAbnormalObject(val);
                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.RecoverState, CommonFunction.StateNames[val]));
                    }
                }
            }
            //状態異常の付与
            else
            {
                int state = target.AddStateAbnormal(statusRecoverTarget);

                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    if ((state & (int)val) != 0)
                    {
                        DisplayInformation.Info.AddMessage(
                            CommonFunction.GetAbnormalMessage(val, target));
                    }
                }
            }
        }
    }

    protected void EffectAbnormal(List<BaseCharacter> targets)
    {

        int statusRecoverTarget = CommonFunction.GetDefenceAbnormals(this.Options);
        if (statusRecoverTarget != 0)
        {
            //状態の回復
            if (CommonFunction.HasOptionType(this.Options, OptionType.ReverceAbnormal) == false)
            {
                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    //対象の状態異常を含んでいなければ無視
                    if ((statusRecoverTarget & (int)val) == 0)
                    {
                        continue;
                    }

                    foreach (BaseCharacter c in targets)
                    {
                        //状態異常の回復に成功したら
                        int ret = c.RecoverState((int)val);
                        if (ret != 0)
                        {
                            c.RemoveAbnormalObject(val);
                            DisplayInformation.Info.AddMessage(
                                string.Format(CommonConst.Message.RecoverState, CommonFunction.StateNames[val]));
                        }
                    }
                }
            }
            //状態異常の付与
            else
            {
                foreach (BaseCharacter c in targets)
                {
                    int state = c.AddStateAbnormal(statusRecoverTarget);

                    foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                    {
                        if ((state & (int)val) != 0)
                        {
                            DisplayInformation.Info.AddMessage(
                                CommonFunction.GetAbnormalMessage(val, c));
                        }
                    }
                }
            }
        }
    }

    //protected virtual bool IsEmber()
    //{
    //    if (CommonFunction.IsNull(Options) == false)
    //    {
    //        foreach (BaseOption o in Options.Values)
    //        {
    //            if (o.OType == OptionType.CatchingFire)
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    protected virtual int CountWildfire()
    {
        if (CommonFunction.IsNull(Options) == false)
        {
            foreach (BaseOption o in Options)
            {
                if (o.OType == OptionType.Wildfire)
                {
                    return o.Plus;
                }
            }
        }
        return 0;
    }


    public virtual string GetItemName()
    {
        return DisplayNameInItem;
    }

    public virtual string GetAttack()
    {
        return "-";
    }
    public virtual string GetDefense()
    {
        return "-";
    }
    public virtual string GetDescription()
    {
        if(IsAnalyse == true)
        {
            return Description;
        }
        else
        {
            return "未鑑定";
        }
    }

    public string GetDriveName()
    {
        if(IsDrive == false)
        {
            return "";
        }
        return InDrive.DisplayName;
    }
    public bool IsDriveGap()
    {
        return false;
    }

    public virtual Dictionary<int,MenuItemActionType> GetItemAction()
    {
        Dictionary<int, MenuItemActionType> dic = new Dictionary<int, MenuItemActionType>();
        //dic.Add();
        return dic;
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                InDrive = null;
                ThrowActionPoint = null;
                if(CommonFunction.IsNull(Options) == false)
                {
                    foreach (BaseOption o in Options)
                    {
                        o.Dispose();
                    }
                    Options.Clear();
                    Options = null;
                }
            }
        }
        base.Dispose(disposing);
    }
}

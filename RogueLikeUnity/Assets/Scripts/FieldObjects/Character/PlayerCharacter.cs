using Assets.Scripts.Models;
using Assets.Scripts.Models.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacter : BaseCharacter {

    public GameObject[] StellaGuns;
    
    /// <summary>
    /// 基本防御力
    /// </summary>
    public override float Defense
    {
        get
        {
            float defence = BaseDefense;
            //オプション値と装備値を取得
            foreach (BaseItem b in ItemList.FindAll(i => i.IsEquip == true))
            {
                defence += b.ItemDefence;
            }
            return defence;
        }
    }

    /// <summary>
    /// 満腹度
    /// </summary>
    public float SatietyValue;

    /// <summary>
    /// 満腹度最大値
    /// </summary>
    public float SatietyMax;

    public bool IsSatietyMax
    {
        get
        {
            return SatietyMax == SatietyValue;
        }
    }

    public override string DisplayNameInMessage {
        get
        {
            return string.Format("<color={0}>{1}</color>", CommonConst.Color.Player, base.DisplayNameInMessage);
        }
    }

    public float _SatietyReduce;
    /// <summary>
    /// ターンごとの満腹度減少値
    /// </summary>
    public float SatietyReduce
    {
        get
        {

            float cor = 0;
            foreach (BaseOption op in this.Options)
            {
                cor += op.TurnReduceSat(_SatietyReduce);
            }
            return _SatietyReduce + cor;
        }
        set
        {
            _SatietyReduce = value;
        }
    }

    /// <summary>
    /// 腹ペコ警告
    /// </summary>
    public bool IsSatietyCaution;
    public bool IsSatietyDanger;

    public static ushort ItemMaxCount;

    //public static Dictionary<Guid, BaseItem> ItemList;
    public static List<BaseItem> ItemList;
    
    //public Dictionary<CharacterDirection, MapPoint> DeathBlowTargetPoint;
    //public Dictionary<CharacterDirection, BaseCharacter> DeathBlowTargetCharacter;

    //public AttackInformation DeathBlowInformation;

    /// <summary>
    /// オブジェクト初期化処理 
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        IsMapDisplay = true;
        Type = ObjectType.Player;
        ObjNo = PlayerInformation.Info.ObjNo;
        _isEndAnima = true;
        IsSatietyCaution = false;
        IsSatietyDanger = false;
        InstanceName = PlayerInformation.Info.InstanceName;
        //ThisDisplayObject = GameObject.Find(InstanceName);
        ThisDisplayObject = ResourceInformation.GetPlayerInstance(InstanceName);
        //ThisDisplayObject = this.gameObject;

        // ItemList = new Dictionary<Guid, BaseItem>();

        ////item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23004);
        ////AddItem(item, item.ObjNo);

        //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24011);
        //AddItem(item, item.ObjNo);


    }


    /// <summary>
    /// アニメーションの初期化
    /// </summary>
    public override void UpdateIdleStatus()
    {

        SetAnimator();

        ThisAnimator.SetSpeed(CommonConst.SystemValue.AnimationSpeedDefault);

        MoveSpeed = MoveSpeedDefault;
    }

    public override WeaponBase EquipWeapon
    {
        get
        {
            BaseItem weapon = ItemList.Find(i => i.IsEquip == true && i.IType == ItemType.Weapon);
            if (CommonFunction.IsNull(weapon) == false)
            {
                return (WeaponBase)weapon;
            }
            else
            {
                return WeaponFreeHand.Instance;
            }
        }
    }
    public override ShieldBase EquipShield
    {
        get
        {
            BaseItem shield = ItemList.Find(i => i.IsEquip == true && i.IType == ItemType.Shield);
            if (CommonFunction.IsNull(shield) == false)
            {
                return (ShieldBase)shield;
            }
            else
            {
                return ShieldFreeHand.Instance;
            }
        }
    }
    public override RingBase EquipRing
    {
        get
        {
            BaseItem weapon = ItemList.Find(i => i.IsEquip == true && i.IType == ItemType.Ring);
            if (CommonFunction.IsNull(weapon) == false)
            {
                return (RingBase)weapon;
            }
            else
            {
                return RingFreeHand.Instance;
            }
        }
    }
    

    public void UseItem()
    {

        //ターンカウントを更新
        RestActionTurn--;
    }

    protected override void SetAnimator()
    {
        if(CommonFunction.IsNull(ThisAnimator.Anim) == false)
        {
            return;
        }
        ThisAnimator.Anim = ThisDisplayObject.transform.Find("Body").GetComponent<Animator>();
        
    }

    public void OffAllAnimation()
    {
        SetAnimator();
        ThisAnimator.OffAllAction();
    }

    /// <summary>
    /// 必殺技の発動
    /// </summary>
    public override bool DeathBlow(ManageDungeon dun)
    {
        switch (PlayerInformation.Info.PType)
        {
            case PlayerType.UnityChan:

                //現在位置に強制移動（モーションによるフレームずれを補正）
                ResetObjectPosition();

                //ターンカウントを更新
                RestActionTurn--;

                //必殺技実行
                if (DeathBlowMagicalRecipe(dun) == true)
                {
                    ActType = ActionType.DeathBlow;
                    
                    return true;
                }
                break;
            case PlayerType.OricharChan:
                
                //現在位置に強制移動（モーションによるフレームずれを補正）
                ResetObjectPosition();

                //ターンカウントを更新
                RestActionTurn--;

                //必殺技実行
                if(DeathBlowKugeltanz(dun) == true)
                {
                    ActType = ActionType.DeathBlow;

                    _isEndAnima = false;
                    return true;
                }

                break;
        }

        return false;
    }

    /// <summary>
    /// ターン終了の初期化
    /// </summary>
    public override void FinishTurn(ManageDungeon dun)
    {
        base.FinishTurn(dun);

        //指輪の壊れ判定
        RingBase ring = EquipRing;
        if (ring.CheckBreak() == true)
        {
            ring.ForceRemoveEquip(this);
            RemoveItem(ring);
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.BreakItem, ring.DisplayNameInMessage));

            SoundInformation.Sound.Play(SoundInformation.SoundType.Break);
        }
        ring.TurnFInish(this, dun);

        //スコア値の更新
        if (typeof(RingFreeHand) != ring.GetType())
        {
            ring.TotalRing++;
            if (ring.TotalRing > ScoreInformation.Info.MostUseRingDamage)
            {
                ScoreInformation.Info.MostUseRingDamage = ring.TotalRing;
                ScoreInformation.Info.MostUseRingName = ring.DisplayNameNormal;
            }
        }

        //満腹度処理
        if (SatietyValue <= 0)
        {
            //0を下回ったらHpが減少
            CurrentHp--;

            if(CurrentHp <= 0)
            {
                DisplayInformation.Info.AddMessage(GetMessageDeath());
                ScoreInformation.Info.CauseDeath =
                    string.Format(CommonConst.DeathMessage.Hunger, DisplayNameNormal);

                ScoreInformation.Info.CauseDeathType = DeathCouseType.Hunger;

                Death(this, this.AttackInfo);
                DeathAction(dun);
            }
            //未危険告知の状態で下回ったらメッセージを表示
            if(IsSatietyDanger == false)
            {
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);
                DisplayInformation.Info.AddMessage(CommonConst.Message.HungryDanger);
                IsSatietyDanger = true;
            }
        }
        else
        {
            //Hpの回復
            if(CurrentHp < MaxHp)
            {
                CurrentHp += TrunRecoverHp;
                if(CurrentHp > MaxHp)
                {
                    CurrentHp = MaxHp;
                }
            }

            //満腹度の減少
            SatietyValue -= SatietyReduce;

            //未警告の状態で警告値を下回ったらメッセージを表示
            if (IsSatietyCaution == false 
                && SatietyValue < CommonConst.Status.SatietyCautionTiming)
            {
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);
                DisplayInformation.Info.AddMessage(CommonConst.Message.HungryCaution);
                IsSatietyCaution = true;
            }
        }
    }

    public override void CheckNextLevel(AttackInformation atinf)
    {
        if (CurrentExperience >= NextLevelExperience)
        {
            //サウンドを鳴らす
            atinf.AddSound(SoundInformation.SoundType.Levelup);
            atinf.AddVoice(VoiceInformation.VoiceType.Levelup);

            atinf.AddMessage(
                string.Format(CommonConst.Message.LevelUpPlayer, DisplayNameInMessage));
            Level++;
            atinf.AddEffect(EffectFlareCore.CreateObject(this));
            TablePlayerLebel.SetLevel(this, Level);
        }
    }


    public void AddSatiety(float sat)
    {
        //最大値を超える場合は最大値まで回復
        if (SatietyValue + sat > SatietyMax)
        {
            SatietyValue = SatietyMax;
        }
        else
        {
            //超えない場合はそこまで回復
            SatietyValue += sat;
        }

        if (SatietyValue > CommonConst.Status.SatietyCautionTiming)
        {
            IsSatietyCaution = false;
            IsSatietyDanger = false;
        }
    }



    public void ReduceSatiety(float sat)
    {
        //減少値のほうが多いなら０になる
        if (SatietyValue < sat)
        {
            SatietyValue = 0;
        }
        else
        {
            //超えない場合はそこまで減少
            SatietyValue -= sat;
        }
    }

    /// <summary>
    /// キャラクターのステータス設定
    /// </summary>
    public override void SetFirstStatus(SavePlayingInformation save)
    {
        base.SetFirstStatus(save);
        //基本ステータスの設定
        TablePlayerLebel.SetLevelInitialize(this, save.lv);
        
        //保存データがある場合は保存データの読み込み
        //Hpの設定
        MaxHpCorrection = save.mhc;
        CurrentHp = save.hv;

        //力の設定
        PowerMax = save.pm;
        PowerValue = save.pv;

        //経験値の設定
        CurrentExperience = save.ex;

        //満腹度の設定
        SatietyReduce = CommonConst.Status.SatietyReduce;
        SatietyValue = save.sv;
        SatietyMax = save.sm;
        ItemMaxCount = PlayerInformation.Info.ItemMaxCount;

        //名前
        DisplayName = save.pn;

        if(CommonFunction.IsNull(GameStateInformation.TempItemList) == false &&
            GameStateInformation.TempItemList.Count > 0)
        {
            //アイテムリスト
            ItemList = GameStateInformation.TempItemList;

            GameStateInformation.TempItemList = null;
        }
        else
        {
            ItemList = new List<BaseItem>();

            //大きいコロッケ
            BaseItem item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23002, true);
            AddItem(item, item.ObjNo);
        }


#if UNITY_EDITOR
        if (false)
        {
            //item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20002, true);
            //item.StrengthValue = 1;
            //AddItem(item, item.ObjNo);
            BaseItem item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20002, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20004, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20005, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Weapon, 20006, true);
            AddItem(item, item.ObjNo);
            BaseOption o = TableOptionCommon.GetValue(40001);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40003);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40005);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40006);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40007);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40008);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40070);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40172);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40173);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40174);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40016);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40018);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40019);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40021);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40023);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40024);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40028);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40030);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40031);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40033);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40034);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40050);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40051);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40053);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40054);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40055);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40055);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40056);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40058);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40059);
            item.Options.Add(o);



            item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21002, true);
            AddItem(item, item.ObjNo);
            o = TableOptionCommon.GetValue(40175);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40012);
            item.Options.Add(o);
            //o = TableOptionCommon.GetValue(40032);
            //((ShieldBase)item).Options.Add(o.Name, o);
            o = TableOptionCommon.GetValue(40037);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40022);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40047);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40048);
            item.Options.Add(o);
            o = TableOptionCommon.GetValue(40052);
            item.Options.Add(o);
            ////o = TableOptionCommon.GetValue(40060);
            ////((ShieldBase)item).Options.Add(o.Name, o);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21005, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Shield, 21006, true);
            //AddItem(item, item.ObjNo);

            //item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28001, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28001, true);
            //AddItem(item, item.ObjNo);
            ////item = TableItemIncidence.GetItemObjNo(ItemType.Ring, 22017, true);
            ////AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30009, true);
            //item.StrengthValue = 5;
            //AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30001, true);
            AddItem(item, item.ObjNo);
            o = TableOptionCommon.GetValue(40178);
            item.Options.Add(o);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30002, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30002, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30002, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30002, true);
            AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30003, true);
            //AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30004, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30004, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30004, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30004, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Material, 30005, true);
            AddItem(item, item.ObjNo);



            //item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23001, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23002, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Food, 23004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25011, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25012, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25013, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25014, true);
            //AddItem(item, item.ObjNo);

            //item = TableItemIncidence.GetItemObjNo(ItemType.Ring, 22019, true);
            //AddItem(item, item.ObjNo);


            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24013, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24014, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24015, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24016, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24017, true);
            //AddItem(item, item.ObjNo);


            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26007, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26008, true);
            //AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28002, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28003, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Bag, 28005, true);
            AddItem(item, item.ObjNo);

            item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25006, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25007, true);
            AddItem(item, item.ObjNo);

            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24005, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24006, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24007, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24008, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24009, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24010, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Candy, 24008, true);
            //AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26001, true);
            AddItem(item, item.ObjNo);
            item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26001, true);
            AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26002, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26003, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26004, true);
            //AddItem(item, item.ObjNo);
            //item = TableItemIncidence.GetItemObjNo(ItemType.Melody, 26005, true);
            //AddItem(item, item.ObjNo);

            //item = TableItemIncidence.GetItemObjNo(ItemType.Ball, 25009);
            //AddItem(item, item.ObjNo);
        }
#endif

        if (save.IsLoadSave == true)
        {
            //状態異常
            this.AddForceStateAbnormal(save.asn);


            //装備の反映
            List<BaseItem> eq = ItemList.FindAll(i => i.IsEquip == true);
            foreach(BaseItem i in eq)
            {
                i.ForceEquip(this);
            }

        }
    }

    private bool DeathBlowMagicalRecipe(ManageDungeon dun)
    {
        if (PowerValue <= 1)
        {
            DisplayInformation.Info.AddMessage(
                CommonConst.Message.DeathBlowNotPower);
            return false;
        }

        //メッセージの追加
        AttackInfo.AddMessage(string.Format(CommonConst.Message.DeathBlowMagicalRecipe, this.DisplayNameInMessage));
        AttackInfo.AddMessage(string.Format(CommonConst.Message.DeathBlowMagicalRecipe2, this.DisplayNameInMessage));

        EffectFlareCore ef = EffectFlareCore.CreateObject(this, EffectFlareCore.FLareCoreType.Blue);
        AttackInfo.AddEffect(ef);

        //声を鳴らす
        this.AttackInfo.AddVoice(VoiceInformation.VoiceType.Deathblow);

        ManageAtelier.IsSuccess = true;

        //Power減らす
        ReducePower(1);

        return true;

    }

    private bool DeathBlowKugeltanz(ManageDungeon dun)
    {
        if(PowerValue <= 2)
        {
            DisplayInformation.Info.AddMessage(
                CommonConst.Message.DeathBlowNotPower);
            return false;
        }

        //声を鳴らす
        this.AttackInfo.AddVoice(VoiceInformation.VoiceType.Deathblow);

        StellaDeathBlow b = this.ThisTransform.Find("Body").GetComponent<StellaDeathBlow>();
        b.Character = this;
        DeathBlowKugeltanzBase(dun);

        //Power減らす
        ReducePower(2);

        return true;
    }

    //protected void DeathBlowKugeltanzDir(ManageDungeon dun, CharacterDirection dir, WeaponBase wep, int power, BaseOption[] atoptions, AttackInformation atinf)
    //{
    //    //弾の到達範囲を設定
    //    DeathBlowTargetPoint.Add(dir,
    //        dun.GetHitRangePoint(this.CurrentPoint, dir, 3));

    //    //到達地点に敵がいた場合
    //    if (CommonFunction.IsNull(dun.CharacterMap.Get(DeathBlowTargetPoint[dir])) == false)
    //    {
    //        BaseCharacter target = dun.CharacterMap.Get(DeathBlowTargetPoint[dir]);

    //        if (CommonFunction.IsRandom(wep.WeaponDexterity) == true)
    //        {
    //            BaseOption[] tgoptions = target.Options;

    //            DeathBlowTargetCharacter.Add(dir, target);

    //            //与ダメージを計算
    //            int damage = wep.CalcDamage(dun, this, target, power, atoptions, tgoptions, 5);

    //            //スコア関連値の更新
    //            if (target.Type == ObjectType.Enemy)
    //            {
    //                ScoreInformation.Info.AddScore(damage);
    //            }

    //            //ダメージ追加
    //            AttackState atState = CommonFunction.AddDamage(atinf, this, target, damage);

    //            //エフェクトの追加
    //            wep.AttackEffect(target, this, damage.ToString(), AttackState.Hit, atinf);

    //            //対象が死亡したら
    //            if (atState == AttackState.Death)
    //            {
    //                atinf.AddKillList(target);

    //                atinf.AddMessage(
    //                    target.GetMessageDeath(target.HaveExperience));


    //                Death(target, atinf);
    //            }
    //        }
    //        else
    //        {
    //            //外れた場合
    //            atinf.AddHit(target, false);

    //            EffectDamage d = EffectDamage.CreateObject(target);
    //            d.SetText("Miss", AttackState.Miss);
    //            atinf.AddEffect(d);

    //            DeathBlowTargetCharacter.Add(dir, null);

    //            atinf.AddMessage(
    //                target.GetMessageAttackMiss());
    //        }
    //    }
    //    else
    //    {
    //        DeathBlowTargetCharacter.Add(dir, null);
    //    }
    //}

    /// <summary>
    /// 移動アニメーションオフの切り替え判定
    /// </summary>
    /// <returns></returns>
    //protected override bool CheckIsMoveFalse(DungeonCreateModel dungeon)
    //{
    //    //遊びのカウントを減らす
    //    _endMoveCount--;

    //    //いずれかの移動キーが押されていた場合で壁側を向いていたとしたら即終了
    //    if ((Input.GetKey(KeyControlInformation.Info.MoveUp) && dungeon.DungeonMap.Get(CurrentPoint.X, CurrentPoint.Y + 1).State == LoadStatus.Wall
    //        || Input.GetKey(KeyControlInformation.Info.MoveDown) && dungeon.DungeonMap.Get(CurrentPoint.X, CurrentPoint.Y - 1).State == LoadStatus.Wall
    //        || Input.GetKey(KeyControlInformation.Info.MoveLeft) && dungeon.DungeonMap.Get(CurrentPoint.X - 1, CurrentPoint.Y).State == LoadStatus.Wall
    //        || Input.GetKey(KeyControlInformation.Info.MoveRight) && dungeon.DungeonMap.Get(CurrentPoint.X + 1, CurrentPoint.Y).State == LoadStatus.Wall))
    //    {
    //        _endMoveCount = 0;
    //        return true;
    //    }

    //    //いずれかのボタンが押されているとき
    //    if(Input.GetKey(KeyControlInformation.Info.MoveUp) || Input.GetKey(KeyControlInformation.Info.MoveDown) 
    //        || Input.GetKey(KeyControlInformation.Info.MoveLeft) || Input.GetKey(KeyControlInformation.Info.MoveRight))
    //    { 
    //        //遊びのカウント0なら終了可
    //        if (_endMoveCount <= 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    /// <summary>
    /// 空きのあるドライブを持っているかどうか
    /// </summary>
    /// <returns></returns>
    public static bool HasDriveGap()
    {
        int cnt = ItemList.Count(i => i.IType == ItemType.Bag && i.IsDriveGap() == true);

        if(cnt >0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 現在の所持アイテム数をカウントする
    /// </summary>
    /// <returns></returns>
    public static float ItemCount()
    {
        float cnt = 0;
        foreach (BaseItem item in PlayerCharacter.ItemList)
        {
            if (item.IsDrive == false)
            {
                cnt += item.Weight;
            }
        }
        //切り上げ
        return cnt;
    }

    public static bool IsGetItem(BaseItem item)
    {
        return (PlayerCharacter.ItemCount() + item.Weight ) <= PlayerCharacter.ItemMaxCount;
    }
    public static bool HasItemPlayer(BaseItem val)
    {
        if (PlayerCharacter.ItemList.Contains(val) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool HasItemPlayer(long objNo)
    {
        if (PlayerCharacter.ItemList.Exists(i=>i.ObjNo == objNo) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 対象のアイテムを持ち物に追加する
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(BaseItem item,long sort)
    {
        if (item.IType == ItemType.Other
            && ((OtherBase)item).OType == OtherType.Dollar
            && item.IsDrive == false)
        {
            OtherBase o = (OtherBase)item;
            HaveDollar += o.SellingPrice;
        }
        else
        {

            item.SortNo = sort;


            ScoreInformation.Info.AddScore(1);

            if (ItemList.Contains(item) == true)
            {
                ErrorInformation.Info.Initialize();
                ErrorInformation.Info.SendLogCorutine(new Exception(),
                    CommonFunction.CammaString(item.ObjNo, item.IsDrive, HasItemPlayer(item)));

            }
            else
            {
                ItemList.Add(item);
            }

            //ドライブだったら中身も追加する
            if (item.IType == ItemType.Bag)
            {
                foreach (BaseItem i in ((BagBase)item).BagItems)
                {
                    i.SortNo = sort;
                    ItemList.Add(i);
                }
            }
        }
    }
    

    /// <summary>
    /// 対象のアイテムを持ち物から削除する
    /// </summary>
    /// <param name="item"></param>
    public static void RemoveItem(BaseItem item)
    {
        //ドライブだったら中身も削除する
        if (item.IType == ItemType.Bag)
        {
            foreach (BaseItem i in ((BagBase)item).BagItems)
            {
                ItemList.Remove(i);
            }
        }

        ItemList.Remove(item);
    }

    public static BaseItem GetEquip(ItemType type)
    {
        //装備済みのものがあったら外す
        //Dictionary<Guid, BaseItem> equips =
        //    ItemList.Where(
        //                i => i.Value.ItemType == type
        //                    && i.Value.IsEquip == true).ToDictionary(i => i.Key, i => i.Value);

        //if (equips.Count > 0)
        //{
        //    return equips.Values.First();
        //}
        foreach (BaseItem item in ItemList)
        {
            if(item.IType == type && item.IsEquip == true)
            {
                return item;
            }
        }

        return null;
    }

    public bool IsEnemyVisible()
    {
        //聞き耳指輪を持っているか
        RingBase r = EquipRing;
        if(r.RType == RingType.Listen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsItemVisible()
    {
        //目星指輪を持っているか
        RingBase r = EquipRing;
        if (r.RType == RingType.OneEyes)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTrapVisible()
    {
        //オカルト指輪を持っているか
        RingBase r = EquipRing;
        if (r.RType == RingType.Occult)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override VoiceInformation.VoiceType VoiceAttack()
    {
        if (CommonFunction.IsRandom(0.1f) == true)
        {
            return VoiceInformation.Voice.PlayRandomAttack();
        }
        return VoiceInformation.VoiceType.None;
    }
    public override VoiceInformation.VoiceType VoiceDefence()
    {
        if(CommonFunction.IsRandom(0.1f) == true)
        {
            return VoiceInformation.Voice.PlayRandomDefence();
        }
        return VoiceInformation.VoiceType.None;
    }

    public override void VoiceDeath()
    {
        VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.VoiceType.Death);
    }

}

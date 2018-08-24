using Assets.Scripts.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseEnemyCharacter : BaseCharacter
{

    public ushort UnitNo;

    public EnemyType EType;

    public sbyte FourBallCount;

    /// <summary>
    /// 攻撃射程
    /// </summary>
    public ushort FiringRange;

    public WeaponEffectType WEType;

    /// <summary>
    /// 移動可能か
    /// </summary>
    public bool IsMove;

    /// <summary>
    /// クリティカル命中率(0-1)
    /// </summary>
    public float CriticalDexterity;

    /// <summary>
    /// クリティカル率
    /// </summary>
    public float Critical;

    /// <summary>
    /// 状態異常を付与する確率
    /// 1:0.1,8:0.02
    /// </summary>
    public string AddAbnormalProb;

    /// <summary>
    /// 汎用項目
    /// </summary>
    public string CommonString;

    /// <summary>
    /// 敵が使用するアイテムの一時保管場所
    /// </summary>
    public BaseItem TempEnemyUseItem;

    public BaseItem TempEnemyThrowItem;
    /// <summary>
    /// 有効化フラグ
    /// </summary>
    //public override bool IsActive
    //{
    //    get
    //    {
    //        return _IsActive;
    //    }
    //    set
    //    {
    //        if (_IsActive == value)
    //        {
    //            return;
    //        }

    //        SkinnedMeshRenderer[] ms = ThisDisplayObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    //        MeshRenderer[] mr = ThisDisplayObject.GetComponentsInChildren<MeshRenderer>();
    //        if(value == true)
    //        {
    //            foreach(SkinnedMeshRenderer s in ms)
    //            {
    //                s.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //            }
    //            foreach(MeshRenderer s in mr)
    //            {
    //                s.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //            }
    //        }
    //        else
    //        {
    //            foreach (SkinnedMeshRenderer s in ms)
    //            {
    //                s.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    //            }
    //            foreach (MeshRenderer s in mr)
    //            {
    //                s.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    //            }
    //        }


    //        _IsActive = value;
    //        SetActivePosition();
    //    }
    //}

    /// <summary>
    /// キャラクタをマップに描画するか
    /// </summary>
    public override bool IsMapDisplay
    {
        get
        {
            if(EType == EnemyType.Slime_2 && InstanceName == "EnemyDammyItem")
            {
                return false;
            }
            return base.IsMapDisplay;
        }

        set
        {
            base.IsMapDisplay = value;
        }
    }


    public override string DisplayNameNormal
    {
        get
        {
            if (Level >= 2)
            {
                return string.Format("{0}{1}{2}({3})", DisplayNameBefore, DisplayName, DisplayNameAfter, this.Level);
            }
            else
            {

                return base.DisplayNameInMessage;
            }
        }
    }
    public override string DisplayNameInMessage
    {
        get
        {
            if(Level >= 2)
            {
                return string.Format("<color={0}>{1}{2}{3}({4})</color>", CommonConst.Color.Enemy, DisplayNameBefore, DisplayName, DisplayNameAfter, this.Level);
            }
            else
            {

                return string.Format("<color={0}>{1}</color>", CommonConst.Color.Enemy, base.DisplayNameInMessage);
            }
        }
    }

    private MapPoint _TargetMovePoint;
    public MapPoint TargetMovePoint
    {
        get
        {
            return _TargetMovePoint;
        }
        set
        {
            _TargetMovePoint = MapPoint.Get(value.X, value.Y);
        }
    }

    public override void ErrorInitialize()
    {
        base.ErrorInitialize();
        MoveState = EnemySearchState.FightPlayer;
    }
    public override void Initialize()
    {
        base.Initialize();
        MoveState = EnemySearchState.FightPlayer;
        Type = ObjectType.Enemy;
        MoveType = EnemyMove.Self;
        FiringRange = 1;
        MoveSpeedDefault = CommonConst.SystemValue.MoveSpeedDefaultEnemy;
        MoveSpeed = CommonConst.SystemValue.MoveSpeedDefaultEnemy;
        WEType = WeaponEffectType.None;
        IsMove = true;
        Direction = CharacterDirection.Bottom;
        FourBallCount = 0;
    }

    public override void DeathAction(ManageDungeon dun)
    {
        if(EType == EnemyType.Alarmer_1)
        {
            EffectFireBallLanding.CreateObject(this).Play();
            SoundInformation.Sound.Play(SoundInformation.SoundType.Bomb);
        }
        base.DeathAction(dun);
    }

    public override void SetCharacterDisplayObject(int x, int y)
    {
        base.SetCharacterDisplayObject(x, y);

        switch(EType)
        {
            //擬態
            case EnemyType.Slime_2:
                Transform[] objList = ThisTransform.Find("Body").GetComponentsInChildren<Transform>().Where(i=>i.name != "Body").ToArray();
                if (objList.Length >= 2)
                {
                    Transform tr = objList[UnityEngine.Random.Range(0, objList.Length)];

                    foreach (Transform t in objList)
                    {
                        if (t.name != tr.name)
                        {
                            GameObject.Destroy(t.gameObject);
                        }
                    }
                }
                break;
        }
        TargetMovePoint = CurrentPoint;

    }
    /// <summary>
    /// 対象オブジェクトの現在位置を再設定する
    /// </summary>
    public override void ResetObjectPosition()
    {
        base.ResetObjectPosition();
    }

    protected override void PositionCorrection()
    {
        base.PositionCorrection();

    }

    protected override void SetAnimator()
    {
        if (CommonFunction.IsNull(ThisAnimator.Anim) == false)
        {
            return;
        }
        ThisAnimator.Anim = ThisDisplayObject.transform.Find("Body").GetComponent<Animator>();


        //return ThisDisplayObject.transform.FindChild("Body").GetComponent<Animator>();
    }
    #region ターン消費行動


    #endregion ターン消費行動

    public virtual bool SpecificAction(ManageDungeon dun, PlayerCharacter player, out BaseCharacter target)
    {
        target = null;
        switch (EType)
        {
            case EnemyType.Slime_1:
                dun.SetUpCharacterMap();

                if (CommonFunction.IsRandom(0.03f) == true)
                {
                    //Hpと経験値が一定以下なら分裂しない
                    if (CurrentHp < 4 || HaveExperience < 2)
                    {
                        break;
                    }

                    //キャラ数が20を超えたら分裂しない
                    if (dun.Characters.Count > 20)
                    {
                        break;
                    }
                    //空きポイントを取得
                    MapPoint p = dun.GetCharacterEmptyTarget(CurrentPoint);

                    //空きポイントが取得できたら分裂
                    if (CommonFunction.IsNull(p) == false)
                    {
                        BaseEnemyCharacter enemy = TableEnemy.GetEnemy(this.ObjNo,
                            DisplayInformation.Info.Floor, DungeonInformation.Info.EnemyHpProb,
                            DungeonInformation.Info.EnemyAtkProb,
                            DungeonInformation.Info.EnemyExpProb,
                            DungeonInformation.Info.StartProbHp,
                            DungeonInformation.Info.StartProbAtk,
                            DungeonInformation.Info.StartProbExp);
                        enemy.SetCharacterDisplayObject(p.X, p.Y);
                        dun.AddCharacters.Add(enemy);
                        this.CurrentHp = CurrentHp / float.Parse(CommonString);
                        enemy.CurrentHp = this.CurrentHp;
                        this.HaveExperience = Mathf.CeilToInt((float)HaveExperience / float.Parse(CommonString));
                        enemy.HaveExperience = HaveExperience;

                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.Division, this.DisplayNameInMessage));
                        ActType = ActionType.Throw;

                        if (this.IsActive == true)
                        {
                            AttackInfo.AddSound(SoundInformation.SoundType.Throw);
                        }

                        return true;
                    }
                }
                break;
            //地雷設置
            case EnemyType.PA_Warrior_1:
                if (MoveState == EnemySearchState.FightPlayer)
                {
                    return false;
                }
                dun.SetUpTrapMap();
                if (CommonFunction.IsNull(dun.TrapMap.Get(CurrentPoint)) == false)
                {
                    return false;
                }

                string[] stArrayData = CommonString.Split(',');

                if (CommonFunction.IsRandom(float.Parse(stArrayData[0])) == true)
                {

                    BaseTrap trap = TableTrap.GetTrap(long.Parse(stArrayData[1]));
                    trap.SetThisDisplayTrap(CurrentPoint.X, CurrentPoint.Y);
                    dun.AddNewTrap(trap);
                    AttackInfo.AddMessage(
                        string.Format(CommonConst.Message.TrapSetBomb, this.DisplayNameInMessage, trap.DisplayNameInMessage));
                    ActType = ActionType.Throw;
                    return true;
                }
                break;
            //擬態
            case EnemyType.Slime_2:
                if (InstanceName == "EnemyDammyItem")
                {
                    int decoy = CharacterAbnormalState & (int)StateAbnormal.Decoy;
                    if (decoy != 0)
                    {
                        InstanceName = "Slime_2";
                        return false;
                    }
                    //プレイヤーとの位置を取得
                    int dist = player.CurrentPoint.DistanceAbs(CurrentPoint);
                    if (dist <= 1)
                    {
                        GameObject stp = ThisDisplayObject.transform.Find("AbnormalImage").gameObject;
                        InstanceName = "Slime_2";
                        GameObject newi = ResourceInformation.GetInstance(InstanceName, true);
                        //GameObject newi = UnityEngine.Object.Instantiate(ResourceInformation.CommonImage.transform.FindChild(InstanceName).gameObject,
                        //    ResourceInformation.DungeonDynamicObject.transform);

                        List<Transform> temp = new List<Transform>();
                        //他の状態異常を新インスタンスに移す
                        foreach (Transform g in stp.transform)
                        {
                            temp.Add(g);
                        }

                        foreach (Transform g in temp)
                        {
                            g.SetParent(newi.transform.Find("AbnormalImage"));
                            g.localPosition = Vector3.zero;
                            g.localRotation = ResourceInformation.QuaternionZero;
                        }

                        GameObject.Destroy(ThisDisplayObject);
                        ThisDisplayObject = newi;
                        SetPosition(CurrentPoint.X, CurrentPoint.Y);

                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.EnemyCamouflage, this.DisplayNameInMessage));

                        AttackInfo.AddEffect(EffectSmoke.CreateObject(this));
                        AttackInfo.AddSound(SoundInformation.SoundType.Throw);

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                break;
            //自爆
            case EnemyType.Slime_3:
                if ((CurrentHp < (MaxHp * 0.2f)) && CommonFunction.IsRandom(0.5f) == true)
                {
                    ActType = ActionType.Throw;

                    AttackInfo.AddMessage(
                        string.Format(CommonConst.Message.EnemyExplosion, this.DisplayNameInMessage));

                    dun.SetUpCharacterMap();
                    Explosion(dun);

                    return true;
                }
                break;
            //吹き飛ばし
            case EnemyType.Bat_2:

                if (MoveState == EnemySearchState.FightPlayer && dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.Room)
                {
                    if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                    {
                        ActType = ActionType.Special;

                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.EnemyTornado, this.DisplayNameInMessage));
                        AttackInfo.AddMessage(
                            CommonConst.Message.EnemyTornado2);
                        AttackInfo.AddSound(SoundInformation.SoundType.Cyclone);

                        RoomInformation visible = dun.GetVisibility(CurrentPoint);

                        List<BaseCharacter> list = dun.GetInsightCharacters(this.CurrentPoint, visible);

                        //部屋内のキャラをランダムで任意の部屋に移動
                        foreach (BaseCharacter c in list)
                        {
                            dun.SetUpCharacterMap();
                            int x, y;
                            dun.GetEnemyPosition(visible, c.CurrentPoint, out x, out y);

                            dun.MoveCharacter(MapPoint.Get(x, y), c);

                            c.ResetObjectPosition();

                            AttackInfo.AddEffect(EffectSmoke.CreateObject(c));
                        }

                        return true;
                    }
                }

                break;
            //回復
            case EnemyType.Rabbit_3:
                {
                    //視界を取得
                    RoomInformation visible = GetVisibility(dun);

                    //視界内のHPが減ったターゲットを取得(自分以外)
                    var targets = dun.Characters.FindAll(i => visible.Left <= i.CurrentPoint.X
                        && i.CurrentPoint.X <= visible.Right
                        && visible.Top <= i.CurrentPoint.Y
                        && i.CurrentPoint.Y <= visible.Bottom
                        && i.CurrentHp < i.MaxHp
                        && (i.CharacterAbnormalState & (int)StateAbnormal.Decoy) == 0
                        && i.Type == ObjectType.Enemy
                        && i.Name != this.Name
                        && i.IsDeath == false
                        );

                    //いなかったら通常行動
                    if (targets.Count == 0)
                    {
                        return false;
                    }

                    //いたら最も近いターゲットを取得
                    int dist = 1000;
                    BaseEnemyCharacter rectarget = null;
                    foreach (BaseEnemyCharacter e in targets)
                    {
                        int tardist = this.CurrentPoint.DistanceAbs(e.CurrentPoint);
                        if (tardist < dist)
                        {
                            dist = tardist;
                            rectarget = e;
                        }
                    }

                    //うまく取得できなかったら終了
                    if (dist == 1000)
                    {
                        return false;
                    }

                    //距離が1なら回復行動
                    if (dist == 1)
                    {
                        ActType = ActionType.Throw;

                        //対象の方向を向く
                        ChangeDirection(WhereTarget(rectarget.CurrentPoint));

                        //効果音
                        AttackInfo.AddSound(SoundInformation.SoundType.Equip);

                        int recover = Mathf.RoundToInt(CommonFunction.CalcDamageBase(this.BaseAttack, this.EquipWeapon.ItemAttack, this.PowerValue, 0) / float.Parse(CommonString));
                        if (recover == 0)
                        {
                            recover = 1;
                        }

                        //回復
                        rectarget.RecoverHp(recover);

                        //エフェクト
                        EffectDamage d = EffectDamage.CreateObject(rectarget);
                        d.SetText(recover.ToString(), AttackState.Heal);
                        AttackInfo.AddEffect(d);

                        //メッセージ
                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.EnemyRecover1, this.DisplayNameInMessage, rectarget.DisplayNameInMessage));

                        //メッセージ
                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.EnemyRecover2, rectarget.DisplayNameInMessage));

                        return true;
                    }
                    //距離が1以上なら対象にアプローチ
                    else
                    {
                        MoveState = EnemySearchState.FightPlayer;
                        ApproachTarget(dun, WhereTarget(rectarget.CurrentPoint));
                        RestActionTurn++;
                        return true;
                    }

                }
                break;

            //怒りレベルアップ
            case EnemyType.TeddyBear_2:
                {

                    //視界を取得
                    RoomInformation visible = GetVisibility(dun);

                    //視界内の死亡したターゲットを取得(自分以外)
                    var tar = dun.Characters.Find(i => visible.Left <= i.CurrentPoint.X
                        && i.CurrentPoint.X <= visible.Right
                        && visible.Top <= i.CurrentPoint.Y
                        && i.CurrentPoint.Y <= visible.Bottom
                        && i.IsDeath == true
                        && (i.CharacterAbnormalState & (int)StateAbnormal.Decoy) == 0
                        && i.Type == ObjectType.Enemy
                        && i.Name != this.Name
                        );

                    //いなかったら通常行動
                    if (CommonFunction.IsNull(tar) == true)
                    {
                        return false;
                    }

                    //いたら一定確率でレベルアップ

                    if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                    {
                        ActType = ActionType.Throw;

                        //メッセージ
                        AttackInfo.AddMessage(
                            string.Format(CommonConst.Message.EnemyAngry, this.DisplayNameInMessage));

                        CurrentExperience += 1;
                        CheckNextLevel(this.AttackInfo);

                        return true;
                    }
                }
                break;
            //メテオストーム
            case EnemyType.Plant_King:

                //攻撃対象を取得する
                BaseCharacter attackTarget = GetTarget(dun, player);

                //視界内に攻撃対象がいたら
                if (CommonFunction.IsNull(attackTarget) == false)
                {
                    Direction = WhereTarget(attackTarget.CurrentPoint);

                    //射程内にいなければ特殊攻撃判定
                    if (CanAttack(dun, attackTarget.CurrentPoint) == false)
                    {
                        if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                        {

                            ActType = ActionType.DeathBlow;

                            RoomInformation visible = dun.GetVisibility(CurrentPoint);

                            List<BaseCharacter> list = dun.GetNearCharacters(player.CurrentPoint, 5, true);


                            //メテオエフェクト
                            AttackInfo.AddEffect(EffectMeteorStorm.CreateObject(player));

                            AttackInfo.AddVoice(VoiceInformation.Voice.PlayRandomDefence());

                            //サウンド
                            AttackInfo.AddSound(SoundInformation.SoundType.Meteor);

                            //メッセージ
                            AttackInfo.AddMessage(
                                string.Format(CommonConst.Message.EnemyPlantDeathBlow, this.DisplayNameInMessage));

                            foreach (BaseCharacter c in list)
                            {
                                if (c.Name == this.Name)
                                {
                                    continue;
                                }
                                int damage = Mathf.RoundToInt(CommonFunction.CalcDamageBase(this.BaseAttack, this.EquipWeapon.ItemAttack, this.PowerValue, 0));

                                AttackState atState = c.AddDamage(damage);

                                //ダメージエフェクト
                                EffectDamage d = EffectDamage.CreateObject(c);
                                d.SetText(damage.ToString(), AttackState.Hit);

                                AttackInfo.AddEffect(d);


                                //対象が死亡したら
                                if (atState == AttackState.Death)
                                {
                                    AttackInfo.AddKillList(c);

                                    AttackInfo.AddMessage(
                                        c.GetMessageDeath(c.HaveExperience));

                                    if (c.Type == ObjectType.Player && c.IsDeath == false)
                                    {
                                        ScoreInformation.Info.CauseDeath =
                                            string.Format(CommonConst.DeathMessage.MeteorStorm, DisplayNameNormal);

                                        ScoreInformation.Info.CauseDeathType = DeathCouseType.MeteorStorm;
                                        ScoreInformation.Info.EnemyObjNo = this.ObjNo;
                                    }

                                    Death(c, this.AttackInfo);

                                }
                            }
                        }
                        return true;
                    }
                }

                break;

            //警報
            case EnemyType.Alarmer_1:

                //敵が一定数以下の時のみ
                if (dun.Characters.Count < 10)
                {
                    //攻撃対象を取得する
                    BaseCharacter attackTarget2 = GetTarget(dun, player);

                    //視界内に攻撃対象がいたら
                    if (CommonFunction.IsNull(attackTarget2) == false)
                    {
                        if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                        {

                            ActType = ActionType.DeathBlow;

                            dun.SetUpCharacterMap();
                            //敵の出現地点を取得
                            MapPoint mp = dun.GetCharacterEmptyTarget(this.CurrentPoint);
                            if (CommonFunction.IsNull(mp) == true)
                            {
                                return true;
                            }

                            int enemytype = TableEnemyMap.GetValue(dun.DungeonObjNo, DisplayInformation.Info.Floor);
                            uint rand = CommonFunction.GetRandomUInt32();
                            BaseEnemyCharacter enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, DisplayInformation.Info.Floor);

                            enemy.SetCharacterDisplayObject(mp.X, mp.Y);

                            //エフェクトの発動
                            AttackInfo.AddEffect(EffectSummon.CreateObject(this));

                            AttackInfo.AddSound(SoundInformation.SoundType.Alarm);

                            dun.AddCharacters.Add(enemy);

                            AttackInfo.AddMessage(
                                string.Format(CommonConst.Message.EnemyAlarm1, this.DisplayNameInMessage)
                                );
                            AttackInfo.AddMessage(
                                string.Format(CommonConst.Message.EnemyAlarm2, enemy.DisplayNameInMessage)
                                );

                        }
                    }
                }

                return true;
                break;

            //ジャイロ投げかクーゲルタンズ
            case EnemyType.DarkStella_1:
                {
                    //攻撃対象を取得する
                    BaseCharacter attackTarget2 = GetTarget(dun, player);

                    //視界内に攻撃対象がいたら
                    if (CommonFunction.IsNull(attackTarget2) == false
                        && this.CurrentPoint.IsStraightLine(attackTarget2.CurrentPoint) == true)
                    {

                        if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                        {
                            int dist = this.CurrentPoint.DistanceAbs(attackTarget2.CurrentPoint);

                            bool isDeathBlow = false;

                            //対象の方向を向く
                            ChangeDirection(WhereTarget(attackTarget2.CurrentPoint));

                            //クーゲルタンズ
                            if (1 < dist && dist <= 3)
                            {
                                if ((this.CharacterAbnormalState & (int)StateAbnormal.Decoy) == 0)
                                {
                                    isDeathBlow = true;
                                }
                            }

                            //ジャイロ投げ
                            else if (dist == 1)
                            {
                                //対象が壁と接していればクーゲルタンズ
                                if (dun.IsExistWall(attackTarget2.CurrentPoint, 1) == true)
                                {
                                    if ((this.CharacterAbnormalState & (int)StateAbnormal.Decoy) == 0)
                                    {
                                        isDeathBlow = true;
                                    }
                                }
                                else
                                {

                                    if ((this.CharacterAbnormalState & (int)StateAbnormal.StiffShoulder) == 0)
                                    {
                                        //ボール投げアクション
                                        ActType = ActionType.Special;
                                        ThisAnimator.SpecialMove = AnimationType.IsThrow;

                                        //投げるアイテムの作成
                                        BallBase item = TableBall.GetItem(CommonConst.ObjNo.BallGyro);

                                        //効果音
                                        AttackInfo.AddSound(SoundInformation.SoundType.Throw);

                                        //メッセージの追加
                                        AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemyThrow, this.DisplayNameInMessage, item.DisplayNameInMessage));

                                        //敵のマッピングを最新化
                                        dun.SetUpCharacterMap();

                                        //アイテムの吹き飛ばし位置を取得
                                        bool isVanish;
                                        MapPoint point = dun.GetHitItemThrow(this.CurrentPoint, this.Direction, this.ThrowRange, false, out isVanish);

                                        //フィールド上にアイテムを配置
                                        dun.ThrowItem(item, point, attackTarget2.CurrentPoint, Direction);
                                        item.IsActive = false;
                                        item.IsThrowAfterAction = true;
                                        item.ThrowAfterActionTarget = dun.CharacterMap.Get(point);
                                        ResetPositionThrowStart();

                                        TempEnemyThrowItem = item;

                                        //デリゲート設定
                                        DelegeteAction = new DelegateSpecificAction(ThrowFirstAction);

                                        IsSpecial = true;

                                        return true;
                                    }
                                }
                            }
                            if (isDeathBlow == true)
                            {

                                ActType = ActionType.DeathBlow;

                                StellaDeathBlow b = this.ThisTransform.Find("Body").GetComponent<StellaDeathBlow>();
                                b.Character = this;
                                DeathBlowKugeltanzBase(dun);

                                return true;
                            }
                        }
                    }

                }
                break;

            //ジャイロ投げか手作りボール投げ
            case EnemyType.DarkUnity_1:
                {
                    //攻撃対象を取得する
                    BaseCharacter attackTarget2 = GetTarget(dun, player);

                    //視界内に攻撃対象がいたら
                    if (CommonFunction.IsNull(attackTarget2) == false
                        && this.CurrentPoint.IsStraightLine(attackTarget2.CurrentPoint) == true)
                    {

                        if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                        {
                            int dist = this.CurrentPoint.DistanceAbs(attackTarget2.CurrentPoint);

                            bool isHandmaid = false;
                            
                            //対象の方向を向く
                            ChangeDirection(WhereTarget(attackTarget2.CurrentPoint));

                            //ボ手作りボール投げ
                            if (2 < dist && dist < 15)
                            {
                                isHandmaid = true;
                            }
                            //ジャイロ投げ
                            else if (dist == 1)
                            {
                                //対象が壁と接していれば手作りボール投げ
                                if (dun.IsExistWall(attackTarget2.CurrentPoint, 1) == true)
                                {
                                    if ((this.CharacterAbnormalState & (int)StateAbnormal.Decoy) == 0)
                                    {
                                        isHandmaid = true;
                                    }
                                }
                                else
                                {
                                    if ((this.CharacterAbnormalState & (int)StateAbnormal.StiffShoulder) == 0)
                                    {
                                        //ボール投げアクション
                                        ActType = ActionType.Special;
                                        ThisAnimator.SpecialMove = AnimationType.IsThrow;

                                        //投げるアイテムの作成
                                        BallBase item = TableBall.GetItem(CommonConst.ObjNo.BallGyro);

                                        //効果音
                                        AttackInfo.AddSound(SoundInformation.SoundType.Throw);

                                        //メッセージの追加
                                        AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemyThrow, this.DisplayNameInMessage, item.DisplayNameInMessage));

                                        //敵のマッピングを最新化
                                        dun.SetUpCharacterMap();

                                        //アイテムの吹き飛ばし位置を取得
                                        bool isVanish;
                                        MapPoint point = dun.GetHitItemThrow(this.CurrentPoint, this.Direction, this.ThrowRange, false, out isVanish);

                                        //フィールド上にアイテムを配置
                                        dun.ThrowItem(item, point, attackTarget2.CurrentPoint, Direction);
                                        item.IsActive = false;
                                        item.IsThrowAfterAction = true;
                                        item.IsExp = false;
                                        item.ThrowAfterActionTarget = dun.CharacterMap.Get(point);
                                        ResetPositionThrowStart();

                                        TempEnemyThrowItem = item;

                                        //デリゲート設定
                                        DelegeteAction = new DelegateSpecificAction(ThrowFirstAction);

                                        IsSpecial = true;

                                        return true;
                                    }
                                }
                            }
                            //手作りボール投げ
                            if (isHandmaid == true)
                            {

                                if ((this.CharacterAbnormalState & (int)StateAbnormal.StiffShoulder) == 0)
                                {
                                    //ボール投げアクション
                                    ActType = ActionType.Special;
                                    ThisAnimator.SpecialMove = AnimationType.IsThrow;

                                    //投げるアイテムの作成
                                    BallBase item = TableBall.GetItem(CommonConst.ObjNo.BallHandmaid);
                                    item.Options = new List<BaseOption>();
                                    item.RestCount = 1;
                                    BaseOption o = TableOptionCommon.GetValue(CommonConst.ObjNo.OptionBallCathingFire);
                                    o.Plus = 1;
                                    item.Options.Add(o);
                                    o = TableOptionCommon.GetValue(CommonConst.ObjNo.OptionBallEffectDown);
                                    o.Plus = 3;
                                    item.Options.Add(o);
                                    o = TableOptionCommon.GetValue(CommonConst.ObjNo.OptionBallEffectNotStabile);
                                    o.Plus = 3;
                                    item.Options.Add(o);
                                    o = TableOptionCommon.GetValue(CommonConst.ObjNo.OptionBallWildfire);
                                    o.Plus = 2;
                                    item.Options.Add(o);

                                    //効果音
                                    AttackInfo.AddSound(SoundInformation.SoundType.Throw);

                                    //メッセージの追加
                                    AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemyThrow, this.DisplayNameInMessage, item.DisplayNameInMessage));

                                    //敵のマッピングを最新化
                                    dun.SetUpCharacterMap();

                                    //アイテムの吹き飛ばし位置を取得
                                    bool isVanish;
                                    MapPoint point = dun.GetHitItemThrow(this.CurrentPoint, this.Direction, this.ThrowRange, false, out isVanish);

                                    //フィールド上にアイテムを配置
                                    dun.ThrowItem(item, point, attackTarget2.CurrentPoint, Direction);
                                    item.IsActive = false;
                                    item.IsThrowAfterAction = true;
                                    item.ThrowAfterActionTarget = dun.CharacterMap.Get(point);
                                    ResetPositionThrowStart();

                                    TempEnemyThrowItem = item;

                                    //デリゲート設定
                                    DelegeteAction = new DelegateSpecificAction(ThrowFirstAction);

                                    IsSpecial = true;

                                    return true;
                                }
                            }
                        }
                    }

                }
                break;

                //縮地
            case EnemyType.Drangwin_1:
                {
                    //攻撃対象を取得する
                    BaseCharacter attackTarget2 = GetTarget(dun, player);

                    //視界内に攻撃対象がいたら
                    if (CommonFunction.IsNull(attackTarget2) == false)
                    {
                        //覚醒
                        if (CurrentHp < MaxHp / 2)
                        {

                            if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                            {
                                if ((CharacterAbnormalState & ((int)StateAbnormal.Acceleration)) == 0)
                                {
                                    ActType = ActionType.DeathBlow;

                                    AttackInfo.AddMessage(
                                        string.Format(CommonConst.Message.EnemyAwakening, this.DisplayNameInMessage));

                                    AttackInfo.AddMessage(
                                        CommonFunction.GetAbnormalMessage(StateAbnormal.Acceleration, this));

                                    AttackInfo.AddSound(SoundInformation.SoundType.Howling);

                                    AttackInfo.AddEffect(EffectFlareCore.CreateObject(this, EffectFlareCore.FLareCoreType.Blue));

                                    this.AddStateAbnormal(StateAbnormal.Acceleration);

                                    return true;
                                }
                            }
                        }

                        //縮地
                        if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                        {
                            ActType = ActionType.DeathBlow;

                            int dist = this.CurrentPoint.DistanceAbs(attackTarget2.CurrentPoint);

                            //距離が空いていると縮地
                            if (dist > 1)
                            {

                                //敵の出現地点を取得
                                MapPoint mp = dun.GetCharacterEmptyTarget(attackTarget2.CurrentPoint);
                                if (CommonFunction.IsNull(mp) == true)
                                {
                                }
                                else
                                {
                                    AttackInfo.AddSound(SoundInformation.SoundType.AttackMiss);

                                    AttackInfo.AddMessage(
                                        string.Format(CommonConst.Message.EnemyShorten, this.DisplayNameInMessage));
                                    dun.MoveCharacter(mp, this);

                                    this.ResetObjectPosition();

                                    AttackInfo.AddEffect(EffectSmoke.CreateObject(this));

                                    return true;
                                }
                            }
                        }

                    }
                }
                break;

        }
        return false;
    }

    public void Explosion(ManageDungeon dun)
    {
        Death();
        AttackInfo.AddKillList(this);

        AttackInfo.AddSound(SoundInformation.SoundType.Bomb);

        AttackInfo.AddEffect(EffectBigBang.CreateObject(this));

        List<BaseCharacter> list = dun.GetNearCharacters(this.CurrentPoint, 1);

        foreach(BaseCharacter c in list)
        {
            if(c.IsDeath == true)
            {
                continue;
            }
            if (c.Type == ObjectType.Enemy && ((BaseEnemyCharacter)c).EType == EnemyType.Slime_3)
            {
                ((BaseEnemyCharacter)c).Explosion(dun);
            }
            else
            {
                int damage = Mathf.FloorToInt(c.MaxHp * float.Parse(CommonString));

                //エフェクトをかける
                EffectDamage d = EffectDamage.CreateObject(c);
                d.SetText(damage.ToString(), AttackState.Hit);
                AttackInfo.AddEffect(d);

                //ヒットメッセージ
                AttackInfo.AddMessage(
                    c.GetMessageAttackHit(this.DisplayNameInMessage, damage));

                //ダメージ判定
                AttackState atState = c.AddDamage(damage);

                //対象が死亡したら
                if (atState == AttackState.Death)
                {
                    if (c.Type == ObjectType.Player && c.IsDeath == false)
                    {
                        ScoreInformation.Info.CauseDeath =
                            string.Format(CommonConst.DeathMessage.Explosion, DisplayNameNormal);


                        ScoreInformation.Info.CauseDeathType = DeathCouseType.Explosion;
                        ScoreInformation.Info.EnemyObjNo = this.ObjNo;
                    }
                    AttackInfo.AddKillList(c);

                    AttackInfo.AddMessage(
                        string.Format(CommonConst.Message.DeathCommon, c.DisplayNameInMessage));

                    c.Death();
                }
            }
        }
    }

    /// <summary>
    /// 特殊攻撃
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="player"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool SpecificAttackBefore(ManageDungeon dun, BaseCharacter target)
    {
        switch (EType)
        {
            //のしかかり
            case EnemyType.Rabbit_King:

                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    ActType = ActionType.DeathBlow;

                    int damage = Mathf.RoundToInt(CommonFunction.CalcDamageBase(this.BaseAttack * 2, this.EquipWeapon.ItemAttack, this.PowerValue, 0));

                    AttackState atState = target.AddDamage(damage);

                    AttackInfo.AddSound(SoundInformation.SoundType.Shelling);

                    //ダメージエフェクト
                    EffectDamage d = EffectDamage.CreateObject(target);
                    d.SetText(damage.ToString(), AttackState.Hit);

                    AttackInfo.AddEffect(d);

                    AttackInfo.AddVoice(VoiceInformation.Voice.PlayRandomDefence());

                    EffetHitShockWave sm = EffetHitShockWave.CreateObject(target);
                    AttackInfo.AddEffect(sm);

                    AttackInfo.AddMessage(
                        string.Format(CommonConst.Message.EnemyRabbitDeathBlow, this.DisplayNameInMessage));

                    //ヒットメッセージ
                    AttackInfo.AddMessage(
                            target.GetMessageAttackHit(damage));

                    //対象が死亡したら
                    if (atState == AttackState.Death)
                    {
                        AttackInfo.AddKillList(target);

                        AttackInfo.AddMessage(
                            target.GetMessageDeath(target.HaveExperience));

                        if (target.Type == ObjectType.Player && target.IsDeath == false)
                        {
                            ScoreInformation.Info.CauseDeath =
                                string.Format(CommonConst.DeathMessage.BodySlam, DisplayNameNormal);

                            ScoreInformation.Info.CauseDeathType = DeathCouseType.BodySlam;
                            ScoreInformation.Info.EnemyObjNo = this.ObjNo;
                        }

                        Death(target, this.AttackInfo);

                    }

                    //ターンカウントを更新
                    RestActionTurn--;

                    return true;
                }
                break;
        }
        return false;
    }
    /// <summary>
    /// 特殊攻撃
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="player"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool SpecificAttack(ManageDungeon dun, BaseCharacter target)
    {
        if(target.Type != ObjectType.Player)
        {
            return true;
        }

        switch (EType)
        {
            //吹き飛ばし攻撃
            case EnemyType.Whale_1:
                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    ActType = ActionType.Blow;
                    //吹き飛ばし
                    //ターゲットが吹き飛ぶ先のポイントを取得
                    bool isWall;
                    MapPoint point = dun.GetBlow(target.CurrentPoint, Direction, out isWall);

                    //対象のポイントが取得できなければ
                    if (CommonFunction.IsNull(point) == true)
                    {

                    }
                    //対象ポイントが取得できればそこに移動
                    else
                    {
                        if (typeof(BaseEnemyCharacter) == target.GetType())
                        {
                            ((BaseEnemyCharacter)target).MoveState = EnemySearchState.FightPlayer;
                        }
                        AttackInfo.AddMessage(string.Format(CommonConst.Message.BlowCharacter, target.DisplayNameInMessage));

                        //サウンドを鳴らす
                        AttackInfo.AddSound(SoundInformation.SoundType.BucketFall);

                        target.BlowDirection = Direction;
                        target.BlowPoint = point;
                        target.MoveSpeed = CommonConst.SystemValue.MoveSpeedDash;
                        //target.CurrentPoint = point;
                        dun.MoveCharacter(point, target);

                        if (isWall == true)
                        {
                            //対象に衝突ダメージ
                            target.BlowAfterDamage = 5;
                        }

                        //吹っ飛び先に誰かがいた場合
                        MapPoint vector = CommonFunction.CharacterDirectionVector[Direction];
                        BaseCharacter second = dun.CharacterMap.Get(point.X + vector.X, point.Y + vector.Y);
                        if (CommonFunction.IsNull(second) == false)
                        {

                            second.BlowAfterDamage = 5;
                            second.BlowPoint = second.CurrentPoint;
                            second.BlowDirection = Direction;
                        }
                        IsSpecial = true;
                    }
                }
                break;
            //毒によるパワーダウン
            case EnemyType.SPIDER_1:
                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    AttackInfo.AddMessage(string.Format(CommonConst.Message.DamagePower, target.DisplayNameInMessage));

                    //効果音
                    AttackInfo.AddSound(SoundInformation.SoundType.Shed);

                    AttackInfo.AddEffect(EffectBadSmoke.CreateObject(target));

                    target.ReducePower(1);
                }
                break;
            //武器弾き攻撃
            case EnemyType.TeddyBear_1:

                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {

                    ShieldBase shield = target.EquipShield;
                    WeaponBase weapon = target.EquipWeapon;
                    TempEnemyUseItem = null;
                    //武器も盾も持っている場合は1/2で対象を選択
                    //if (typeof(ShieldFreeHand) != shield.GetType() && typeof(WeaponFreeHand) != weapon.GetType())
                    if (shield.ApType != ShieldAppearanceType.None && weapon.ApType != WeaponAppearanceType.None)
                    {
                        if (CommonFunction.IsRandom(0.5f) == true)
                        {
                            TempEnemyUseItem = shield;
                        }
                        else
                        {
                            TempEnemyUseItem = weapon;
                        }
                    }
                    //盾だけ持っている場合
                    //else if(typeof(ShieldFreeHand) != shield.GetType())
                    else if (shield.ApType != ShieldAppearanceType.None)
                    {
                        TempEnemyUseItem = shield;
                    }
                    //武器だけ持っている場合
                    //else if(typeof(WeaponFreeHand) != weapon.GetType())
                    else if (weapon.ApType != WeaponAppearanceType.None)
                    {
                        TempEnemyUseItem = weapon;
                    }

                    //どちらかを取得できたら
                    if (CommonFunction.IsNull(TempEnemyUseItem) == false)
                    {
                        //弾きアクション
                        ActType = ActionType.Special;

                        //効果音
                        AttackInfo.AddSound(SoundInformation.SoundType.Shed);

                        //メッセージの追加
                        AttackInfo.AddMessage(string.Format(CommonConst.Message.ShedEquipItem, TempEnemyUseItem.DisplayNameInMessage));

                        //装備を強制解除
                        TempEnemyUseItem.RemoveEquipUpdateStatus(target);

                        //アイテムを強制排除
                        PlayerCharacter.RemoveItem(TempEnemyUseItem);

                        //敵のマッピングを最新化
                        dun.SetUpCharacterMap();

                        //アイテムの吹き飛ばし位置を取得
                        bool isVanish;
                        MapPoint point = dun.GetHitItemThrow(target.CurrentPoint, this.Direction, this.ThrowRange, false, out isVanish);

                        //フィールド上にアイテムを配置
                        dun.ThrowItem(TempEnemyUseItem, point, target.CurrentPoint, Direction);
                        TempEnemyUseItem.IsActive = false;
                        TempEnemyUseItem.IsThrowAfterAction = true;
                        if(target.CurrentPoint.Equal(point) == true)
                        {
                            TempEnemyUseItem.ThrowAfterActionTarget = null;
                        }
                        else
                        {
                            TempEnemyUseItem.ThrowAfterActionTarget = dun.CharacterMap.Get(point);
                        }
                        ResetPositionThrowStart();

                        //デリゲート設定
                        DelegeteAction = new DelegateSpecificAction(ShedFirstAction);

                        IsSpecial = true;
                    }
                }
                break;
            //盗み
            case EnemyType.Rabbit_2:
                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    if (CommonFunction.IsNull(TempEnemyUseItem) == false)
                    {
                        break;
                    }
                    //盗む対象を取得
                    BaseItem[] targets = PlayerCharacter.ItemList.FindAll(i => i.IsEquip == false && i.IsDrive == false).ToArray();

                    //対象があればランダムで盗む
                    if (targets.Length > 0)
                    {
                        //盗みアクション
                        ActType = ActionType.Special;

                        //効果音
                        AttackInfo.AddSound(SoundInformation.SoundType.Equip);

                        //対象の中からランダムで一つ抽出
                        TempEnemyUseItem = targets[UnityEngine.Random.Range(0, targets.Length)];

                        AttackInfo.AddMessage(string.Format(CommonConst.Message.StealItem, this.DisplayNameInMessage, TempEnemyUseItem.DisplayNameInMessage));
                        AttackInfo.AddMessage(string.Format(CommonConst.Message.RunAmway, this.DisplayNameInMessage));

                        //プレイヤーから持ち物削除
                        PlayerCharacter.RemoveItem(TempEnemyUseItem);

                        //デリゲート設定
                        DelegeteAction = new DelegateSpecificAction(StealAction);

                    }
                }
                break;
            //マップ忘れ
            case EnemyType.Ghost_2:
                if (dun.IsVisible == true)
                {
                    break;
                }
                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    for (int j = 1; j < dun.X - 1; j++)
                    {
                        for (int i = 1; i < dun.Y - 1; i++)
                        {
                            dun.Dungeon.DungeonMap.Get(i, j).IsClear = false;
                        }
                    }

                    //効果音
                    AttackInfo.AddSound(SoundInformation.SoundType.Summon);

                    AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemyForgetMap, this.DisplayNameInMessage));

                    AttackInfo.AddEffect(EffectBadSmoke.CreateObject(target));
                }
                break;
                //丸吞み
            case EnemyType.Whale_King:

                if (CommonFunction.IsRandom(float.Parse(CommonString)) == true)
                {
                    //盗む対象を取得
                    BaseItem[] targets = PlayerCharacter.ItemList.FindAll(i => i.IsEquip == false && i.IsDrive == false).ToArray();

                    //対象があればランダムで盗む
                    if (targets.Length > 0)
                    {
                        //対象の中からランダムで一つ抽出
                        BaseItem item = targets[UnityEngine.Random.Range(0, targets.Length)];

                        AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemySwallowing1, this.DisplayNameInMessage));
                        AttackInfo.AddMessage(string.Format(CommonConst.Message.EnemySwallowing2, item.DisplayNameInMessage));
                        
                        AttackInfo.AddEffect(EffectBadSmoke.CreateObject(target));

                        PlayerCharacter.RemoveItem(item);
                    }

                }
                break;
        }
        return true;
    }

    /// <summary>
    /// 弾きのアクション開始時処理
    /// </summary>
    protected override void ShedFirstAction(ManageDungeon dun)
    {
        TempEnemyUseItem.IsActive = true;
        TempEnemyUseItem.RemoveEquipDestroyObject();
        TempEnemyUseItem = null;
    }
    protected virtual void ThrowFirstAction(ManageDungeon dun)
    {

        TempEnemyThrowItem.IsActive = true;
        TempEnemyThrowItem.RemoveEquipDestroyObject();
        TempEnemyThrowItem = null;
    }


    /// <summary>
    /// 盗みのアクション開始時処理
    /// </summary>
    protected void StealAction(ManageDungeon dun)
    {
        int x, y;

        dun.SetUpCharacterMap();
        dun.SetUpItemMap();
        //ランダムで配置場所を取得
        dun.GetEnemyPosition(CurrentPoint, out x, out y);

        EffectSmoke.CreateObject(this, false).Play();

        dun.MoveCharacter(MapPoint.Get(x, y), this);
        //CurrentPoint.X = x;
        //CurrentPoint.Y = y;

        ResetObjectPosition();
    }

    /// <summary>
    /// 敵の行動
    /// </summary>
    public BaseCharacter EnemyTurn(ManageDungeon dun, PlayerCharacter player)
    {

        BaseCharacter target = null;

        //ターンの残り消費が1以下なら行動無し
        if(RestActionTurn < 1)
        {
            return target;
        }
        
        //自分が死んでいたら行動無し
        //if (ActType == ActionType.Death)
        if(IsDeath == true)
        {
            return target;
        }

        //睡眠だったら行動無し
        if ((CharacterAbnormalState & (int)StateAbnormal.Sleep) != 0)
        {
            RestActionTurn--;
            return target;
        }
        //特殊行動を行ったら終了
        if (SpecificAction(dun, player,out target) == true)
        {
            RestActionTurn--;
            return target;
        }

        //攻撃対象を取得する
        BaseCharacter attackTarget = GetTarget(dun, player);

        //視界内に攻撃対象がいたら
        if (CommonFunction.IsNull(attackTarget) == false)
        {
            Direction = WhereTarget(attackTarget.CurrentPoint);

            ////混乱していた場合
            //if ((CharacterAbnormalState & (int)StateAbnormal.Confusion) != 0)
            //{
            //    //方向を適当に変更
            //    Direction = CommonFunction.ReverseDirection.Keys.ElementAt(UnityEngine.Random.Range(0, 8));
            //}

            //射程内にいれば攻撃、そうでなければ接近
            if (CanAttack(dun, attackTarget.CurrentPoint) == true)
            {
                ChangeDirection(Direction);
                
                if(SpecificAttackBefore(dun, attackTarget) == false)
                {
                    target = Attack(dun);
                    SpecificAttack(dun, attackTarget);
                }
            }
            else
            {
                //タイプによって接近アプローチ分岐
                switch (MoveType)
                {
                    case EnemyMove.Self:
                        MoveSelf(dun, attackTarget.CurrentPoint);
                        break;
                    case EnemyMove.NAs:
                        MoveNAs(dun, attackTarget.CurrentPoint);
                        break;
                }
            }
        }
        //プレイヤーがいなければダンジョンを探索
        else
        {
            //プレイヤーを見失ったらステートを変更
            if (MoveState == EnemySearchState.FightPlayer)
            {
                if (CommonFunction.IsNull(TargetMovePoint) == true
                    || dun.Dungeon.DungeonMap.Get(TargetMovePoint).State == LoadStatus.Room
                    || dun.Dungeon.DungeonMap.Get(TargetMovePoint).State == LoadStatus.RoomEntrance)
                {
                    //部屋の中なら移動中ステートを設定
                    if (SetNearEntrance(dun) == true)
                    {
                        MoveState = EnemySearchState.ToEntrance;
                    }
                    else
                    {
                        MoveState = EnemySearchState.Load;
                        TargetMovePoint = CurrentPoint;
                    }
                }
                else
                {
                    //部屋の中なら移動中ステートを設定
                    if (dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.Room
                        || dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.RoomEntrance
                        || dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.RoomExit)
                    {
                        MoveState = EnemySearchState.ToEntrance;
                    }
                    else
                    {
                        MoveState = EnemySearchState.Load;
                    }
                }
            }
            //通常探索中
            else
            {
                if (dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.RoomEntrance
                    && MoveState == EnemySearchState.Load)
                {
                    MoveState = EnemySearchState.ToEntrance;
                    SetToExit(dun);
                }
                else
                if (dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.RoomExit
                    && MoveState == EnemySearchState.ToEntrance)
                {
                    MoveState = EnemySearchState.Load;
                }
                else if (dun.Dungeon.DungeonMap.Get(TargetMovePoint).State == LoadStatus.Load
                    && MoveState == EnemySearchState.ToEntrance)
                {
                    MoveState = EnemySearchState.Load;
                    TargetMovePoint = CurrentPoint;
                }
                else if(dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.Load
                    && MoveState == EnemySearchState.ToEntrance)
                {
                    MoveState = EnemySearchState.Load;
                    TargetMovePoint = CurrentPoint;
                }
                //else if (dun.Dungeon.DungeonMap.Get(CurrentPoint.X, CurrentPoint.Y).State == LoadStatus.RoomEntrance
                //    && MoveState == EnemySearchState.Load)
                //{
                //}
            }

            SearchDungeon(dun, player);
        }

        //攻撃対象を取得する
        attackTarget = GetTarget(dun, player);

        //行動終了後にプレイヤーが視界内にいたらそこをターゲットとする
        if (CommonFunction.IsNull(attackTarget) == false)
        {
            IsMapDisplay = true;
            MoveState = EnemySearchState.FightPlayer;
            TargetMovePoint = player.CurrentPoint;
        }
        else
        {
            IsMapDisplay = false ;
        }
        return target;
    }

    public void SearchDungeon(ManageDungeon dun, PlayerCharacter player)
    {
        //目標地点に到達した場合
        if (TargetMovePoint.Equal(CurrentPoint) == true)
        {
            //道を移動中
            if (MoveState == EnemySearchState.Load)
            {
                //部屋に入った
                if (dun.Dungeon.DungeonMap.Get(CurrentPoint).State == LoadStatus.RoomEntrance)
                {
                    //任意の入り口を目標に設定
                    //if (SetToExit(dun) == true)
                    //{
                    //    //いずれかの場所が指定できたらステートを部屋移動に変更
                    //    //MoveState = EnemySearchState.ToEntrance;
                    //}
                    //else
                    //{
                    //    //自分しかなかったら道に帰る
                    //    Direction = GetLoadDirection(dun, Direction);
                    //}
                }
                //道を歩いている
                else
                {
                    //任意の方向を決める
                    CharacterDirection dir = GetLoadDirection(dun, Direction);

                    if (MoveState != EnemySearchState.FightPlayer
                        && dir != CharacterDirection.Match)
                    {
                        Direction = dir;
                        TargetMovePoint = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[Direction]);
                    }
                }
            }
        }
        //部屋の中を移動中
        else
        {
            //出口の方向を取得
            Direction = WhereTarget(TargetMovePoint);
        }

        //移動処理
        switch (MoveState)
        {
            //道の場合
            case EnemySearchState.Load:
                //今向いている方向に最も近いポイントに進む
                ApproachTarget(dun, Direction);
                break;
            //出口に向かって移動中の時
            case EnemySearchState.ToEntrance:
                //出口に向かって進む
                ApproachTarget(dun, Direction);
                break;
            //case EnemySearchState.OutRoom:
            //    //今向いている方向に最も近いポイントに進む
            //    ApproachTarget(dun, Direction);
            //    break;
        }
    }


    public bool SetNearEntrance(ManageDungeon dun)
    {
        //部屋名を取得
        Guid name = dun.Dungeon.DungeonMap.Get(CurrentPoint).RoomName;
        if (name == Guid.Empty)
        {
            return false;
        }

        //部屋の情報を取得
        RoomInformation room = dun.Dungeon.Rooms[name];
        
        //部屋の中から最も近いポイントを設定する
        MapPoint target = MapPoint.Get(1000, 1000);
        for (int i = 0; i < room.Exits.Count; i++)
        {
            if (room.Exits[i].Equal(CurrentPoint) == false)
            {
                if(CurrentPoint.DistanceAbs(target)>CurrentPoint.DistanceAbs(room.Exits[i]))
                {
                    target = room.Exits[i];
                }
            }

        }
        if (target.Equal(MapPoint.Get(1000, 1000)) == false)
        {
            TargetMovePoint = target;
        }
        else
        {
            //一つしかないExitの上で見失ったらそこをターゲットに
            TargetMovePoint = CurrentPoint;
            return false;
        }

        return true;
    }

    private static List<MapPoint> TargetExit = new List<MapPoint>();
    private void SetToExit(ManageDungeon dun)
    {

        //部屋の情報を取得
        RoomInformation room = dun.Dungeon.Rooms[dun.Dungeon.DungeonMap.Get(CurrentPoint).RoomName];

        //自分の後ろを取得
        MapPoint bpoint = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseDirection[Direction]]);

        //自分以外のポイントを対象とする
        TargetExit.Clear();
        for (int i = 0; i < room.Exits.Count; i++)
        {
            if (room.Exits[i].Equal(bpoint) == false)
            {
                TargetExit.Add(room.Exits[i]);
            }
        }

        //自分以外の対象が一件でもいた場合,その中からランダムで対象を選択する
        if (TargetExit.Count > 0)
        {
            int tarcount = UnityEngine.Random.Range(0, TargetExit.Count);
            TargetMovePoint = TargetExit[tarcount];

        }
        //自分しかいなかった場合は向きを変えて移動
        else
        {
            TargetMovePoint = bpoint;
        }
    }

    /// <summary>
    /// 視界内にプレイヤーがいるかを取得
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool GetIsNoticePlayer(ManageDungeon dun, PlayerCharacter player)
    {
        //視界内にプレイヤーがいればTrue
        RoomInformation visible = GetVisibility(dun);
        if(visible.Left <= player.CurrentPoint.X
            && player.CurrentPoint.X <= visible.Right
            && visible.Top <= player.CurrentPoint.Y
            && player.CurrentPoint.Y <= visible.Bottom)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 視界内にいるターゲットを取得
    /// </summary>
    /// <param name="dun"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public BaseCharacter GetTarget(ManageDungeon dun, PlayerCharacter player)
    {
        //視界を取得
        RoomInformation visible = GetVisibility(dun);

        bool isDecoy = false;
        foreach(BaseCharacter c in dun.Characters)
        {
            if((c.CharacterAbnormalState & (int)StateAbnormal.Decoy) != 0)
            {
                isDecoy = true;
            }
        }

        if (isDecoy == true)
        {

            //視界内のデコイターゲットを取得(自分以外)
            var targets = dun.Characters.FindAll(i => visible.Left <= i.CurrentPoint.X
                && i.CurrentPoint.X <= visible.Right
                && visible.Top <= i.CurrentPoint.Y
                && i.CurrentPoint.Y <= visible.Bottom
                && (i.CharacterAbnormalState & (int)StateAbnormal.Decoy) != 0
                && i.Name != this.Name
                && i.IsDeath == false
                );

            //対象となるデコイがいた場合
            if (targets.Count > 0)
            {
                //自分の位置から最も近いキャラを取得
                MapPoint decoy = MapPoint.Get(1000, 1000);
                BaseCharacter temp = null;
                foreach (BaseCharacter c in targets)
                {
                    if (c.CurrentPoint.DistanceAbs(this.CurrentPoint)
                        < decoy.DistanceAbs(this.CurrentPoint))
                    {
                        temp = c;
                        decoy = c.CurrentPoint;
                    }
                }
                //最も近いキャラを返す
                return temp;
            }
        }

        //デコイがおらず、プレイヤーが視界内にいる場合
        if (visible.Left <= player.CurrentPoint.X
            && player.CurrentPoint.X <= visible.Right
            && visible.Top <= player.CurrentPoint.Y
            && player.CurrentPoint.Y <= visible.Bottom)
        {
            //プレイヤーが二つの指輪をつけていたら無視
            if(player.EquipRing.RType == RingType.TwoRing)
            {
                return null;
            }
            else
            {
                //アイテム盗み済みだったら無視
                if(EType == EnemyType.Rabbit_2 && CommonFunction.IsNull(TempEnemyUseItem) == false
                    && player.CurrentPoint.DistanceAbs(this.CurrentPoint) > 1)
                {
                    return null;
                }
                return player;
            }
        }

        //攻撃対象が何もなかった場合
        return null;
    }

    /// <summary>
    /// 視界を取得
    /// </summary>
    /// <returns></returns>
    public RoomInformation GetVisibility(ManageDungeon dun)
    {
        return dun.GetVisibility(CurrentPoint);
    }

    /// <summary>
    /// 各自勝手に動く
    /// </summary>
    public void MoveSelf(ManageDungeon dun, MapPoint target)
    {
        //プレイヤーの方向を取得
        CharacterDirection dir = WhereTarget(target);

        //対象に対してまっすぐ移動
        ApproachTarget(dun, dir);
    }

    /// <summary>
    /// CAs
    /// 各自が他の位置を確認しながら独立して動く
    /// </summary>
    //public void MoveCAs(ManageDungeon dun, MapPoint target)

    /// <summary>
    /// NAs
    /// 各自が他と相談しながら全体最適になるように動く
    /// </summary>
    public void MoveNAs(ManageDungeon dun, MapPoint target)
    {
        //プレイヤーの方向を取得
        CharacterDirection dir = WhereTarget(target);

        //自分のいるエリアを取得
        AreaDirection area = WhereTargetArea(target);

        //自分が向かうエリアを取得
        AreaDirection gotoArea = GetTargetArea(dun, area, this.Name, target);

        GotoArea = gotoArea;

        //エリア移動が必要なければ
        if (gotoArea == AreaDirection.Match)
        {
            //対象に対してまっすぐ移動
            ApproachTarget(dun, dir);
        }
        //エリア移動が必要なら移動方向を補正してアプローチ
        else
        {
            switch(gotoArea)
            {
                case AreaDirection.Top:
                    if(dir == CharacterDirection.Right
                        || dir == CharacterDirection.BottomRight)
                    {
                        dir = CharacterDirection.TopRight;
                    }
                    else if (dir == CharacterDirection.Left
                        || dir == CharacterDirection.BottomLeft)
                    {
                        dir = CharacterDirection.TopLeft;
                    }
                    break;
                case AreaDirection.Bottom:
                    if (dir == CharacterDirection.Right
                        || dir == CharacterDirection.TopRight)
                    {
                        dir = CharacterDirection.BottomRight;
                    }
                    else if (dir == CharacterDirection.Left
                        || dir == CharacterDirection.TopLeft)
                    {
                        dir = CharacterDirection.BottomLeft;
                    }
                    break;
                case AreaDirection.Right:
                    if (dir == CharacterDirection.Top
                        || dir == CharacterDirection.TopLeft)
                    {
                        dir = CharacterDirection.TopRight;
                    }
                    else if (dir == CharacterDirection.Bottom
                        || dir == CharacterDirection.BottomLeft)
                    {
                        dir = CharacterDirection.BottomRight;
                    }
                    break;
                case AreaDirection.Left:
                    if (dir == CharacterDirection.Top
                        || dir == CharacterDirection.TopRight)
                    {
                        dir = CharacterDirection.TopLeft;
                    }
                    else if (dir == CharacterDirection.Bottom
                        || dir == CharacterDirection.BottomRight)
                    {
                        dir = CharacterDirection.BottomLeft;
                    }
                    break;
            }

            //対象に対してまっすぐ移動
            ApproachTarget(dun, dir);
        }

        //        ' 警官の動き：CAs i.e.他の警官の位置を視て自分の４分域を維持しながら、犯人に近づく
        //' 戦略 ①犯人から見た自分の４分域に他の警官がいれば、自分はだれもいない４分域に移動する
        //'      ②その他の場合（４分域には自分しかいない、だれもいない４分域はない）は普通に犯人に近づくように動く
        //Sub Pcas(ByVal id As Integer)
        //  Dim i, j, m, b, x, y As Integer
        //  b = Range("J39").Value                                        ' b:警官の１回の移動コマ数
        //  For i = 1 To b                                                '
        //    m = Where(id)                                               ' m:犯人から見た自分の４分域 0～4
        //    If m = 0 Then                                               ' m=0:警官と犯人の位置が一致
        //      R = id                                                    ' 広域変数 R=警官Id（逮捕成功）
        //      Exit For
        //    End If
        //    If S(m, 1) > 1 Then                                         ' 自分の４分域に警官が複数いる
        //      For j = 1 To 4                                            ' だれもいない４分域を探す
        //        If S(j, 1) = 0 Then                                     ' 見つかれば自分はそこに移動
        //          m = Pempty(id, m, j)                                  ' 最初に見つかった空４分域に移動
        //          Exit For                                              ' 空４分域がなければmはそのままなので普通に移動（Pselfと同じ）
        //        End If                                                  '
        //      Next j
        //    End If
        //    Call Pmove(id, m)                                           ' Pmove: 警官 Id が m (と逆の）方向に１コマ動く
        //    m = Where(id)                                               ' m:犯人から見た自分の４分域 0～4
        //    If m = 0 Then                                               ' m=0:警官と犯人の位置が一致
        //      R = id                                                    ' 広域変数 R=警官Id（逮捕成功）
        //      Exit For
        //    End If
        //    Call State                                                  ' State: 移動後の状態把握（警官全員調べるのは無駄だが...）
        //  Next i
        //End Sub
    }
    
    /// <summary>
    /// CLAs
    /// ボスの指示で統制的に動く
    /// </summary>
    public void MoveCLAs()
    {

    }

    public AreaDirection GetTargetArea(ManageDungeon dun,AreaDirection area,Guid myname,MapPoint target)
    {
        //対象以外でプレイヤーに気付いている敵オブジェクトを取得
        var charas = dun.Characters.Where(i => i.Name != myname 
            && i.Type == ObjectType.Enemy 
            && i.MoveState == EnemySearchState.FightPlayer).ToArray();

        //両隣のエリアを初期化
        List<AreaDirection> neighborArea = new List<AreaDirection>();
        switch(area)
        {
            case AreaDirection.Top:
                neighborArea.Add(AreaDirection.Right);
                neighborArea.Add(AreaDirection.Left);
                break;
            case AreaDirection.Left:
                neighborArea.Add(AreaDirection.Top);
                neighborArea.Add(AreaDirection.Bottom);
                break;
            case AreaDirection.Bottom:
                neighborArea.Add(AreaDirection.Right);
                neighborArea.Add(AreaDirection.Left);
                break;
            case AreaDirection.Right:
                neighborArea.Add(AreaDirection.Top);
                neighborArea.Add(AreaDirection.Bottom);
                break;
            default:
                neighborArea.Add(AreaDirection.Top);
                neighborArea.Add(AreaDirection.Bottom);
                break;
        }

        //移動対象エリアを取得
        Dictionary<AreaDirection, ushort> targetArea = new Dictionary<AreaDirection, ushort>();
        targetArea.Add(AreaDirection.Match, 0);
        targetArea.Add(AreaDirection.Top, 0);
        targetArea.Add(AreaDirection.Left, 0);
        targetArea.Add(AreaDirection.Right, 0);
        targetArea.Add(AreaDirection.Bottom, 0);

        Dictionary<AreaDirection, bool> gotoArea = new Dictionary<AreaDirection, bool>();
        gotoArea.Add(AreaDirection.Match, false);
        gotoArea.Add(AreaDirection.Top, false);
        gotoArea.Add(AreaDirection.Left, false);
        gotoArea.Add(AreaDirection.Right, false);
        gotoArea.Add(AreaDirection.Bottom, false);

        //それぞれのエリアの人数を取得
        foreach (BaseEnemyCharacter c in charas)
        {
            targetArea[c.WhereTargetArea(target)]++;
            gotoArea[c.GotoArea] = true;
        }

        //自分のエリアに誰もいない場合
        if(targetArea[area] == 0)
        {
            return AreaDirection.Match;
        }
        //自分のエリアに誰かがいて隣のエリア1が空白、かつ誰もそのエリアに向かっていない場合
        else if(targetArea[area] != 0
            && targetArea[neighborArea[0]] == 0
            && gotoArea[neighborArea[0]] == false)
        {
            return neighborArea[0];
        }
        //自分のエリアに誰かがいて隣のエリア2が空白、かつ誰もそのエリアに向かっていない場合
        else if (targetArea[area] != 0
            && targetArea[neighborArea[2]] == 0
            && gotoArea[neighborArea[2]] == false)
        {
            return neighborArea[2];
        }

        return AreaDirection.Match;
    }

    public virtual bool CanAttack(ManageDungeon dun,MapPoint target)
    {
        //自オブジェクトとの距離を取得
        int dist = CurrentPoint.DistanceAbs(target);
        bool isLine = CurrentPoint.IsStraightLine(target);
        //射程内かつ直線状にいれば攻撃可
        if(dist <= EquipWeapon.Range && isLine)
        {
            if (Direction == CharacterDirection.BottomLeft
                || Direction == CharacterDirection.BottomRight
                || Direction == CharacterDirection.TopLeft
                || Direction == CharacterDirection.TopRight)
            {
                //斜め方向なら対象に向かって左右の枠に壁がないかチェック
                //□□□□□□□
                //□敵■□□□□
                //□■□■□□□
                //□□■自□□□
                //□□□□□□□
                //□□□□□□□
                MapPoint vector = CommonFunction.CharacterDirectionVector[Direction];
                //1マス先を取得
                MapPoint checkpoint = CurrentPoint.Add(vector);
                for (int i = 1; i <= dist; i++)
                {
                    //左後ろが壁かチェック
                    if (dun.Dungeon.DungeonMap.Get(
                        checkpoint.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseLeftDirectionVector[Direction]]
                        )).State == LoadStatus.Wall)
                    {
                        return false;
                    }
                    //右後ろが壁かチェック
                    if (dun.Dungeon.DungeonMap.Get(
                        checkpoint.Add(CommonFunction.CharacterDirectionVector[CommonFunction.ReverseRightDirectionVector[Direction]]
                        )).State == LoadStatus.Wall)
                    {
                        return false;
                    }
                    checkpoint = checkpoint.Add(vector);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private static List<CharacterDirection> MoveTargetDir = new List<CharacterDirection>();
    /// <summary>
    /// 後方以外のマスをチェックし、道の方向をランダムで返す
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private CharacterDirection GetLoadDirection(ManageDungeon dun, CharacterDirection dir)
    {
        //後方を取得
        CharacterDirection back = CommonFunction.ReverseDirection[dir];
        CharacterDirection rback = CommonFunction.ReverseRightDirectionVector[dir];
        CharacterDirection lback = CommonFunction.ReverseLeftDirectionVector[dir];

        //二分の一の確率で左右は分ける
        bool isright = CommonFunction.IsRandom(0.5f);

        MoveTargetDir.Clear();

        //前後左右の移動可能マスを探す
        dun.SetUpCharacterMap();
        //foreach (CharacterDirection v in CommonFunction.CharacterDirectionVector.Keys)
        for(int i = 0; i < CommonFunction.CharacterDirectionVector.Keys.Count; i++)
        {
            CharacterDirection v = CommonFunction.CharacterDirectionVector.Keys.ElementAt(i);
            if (rback != v && lback != v && back != v)
            {

                MapPoint targetp = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[v]);
                if (dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.Load
                    || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomEntrance
                    || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomExit)
                {

                    if (CommonFunction.IsNull(dun.CharacterMap.Get(targetp)) == true)
                    {
                        MoveTargetDir.Add(v);
                    }
                }
            }
        }
        
        //移動可能マスが1だったらそれを返す
        if (MoveTargetDir.Count == 1)
        {
            return MoveTargetDir[0];
        }
        //移動対象がなかったら後方をさがす
        if (MoveTargetDir.Count == 0)
        {
            MapPoint targetp = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[back]);
            if (dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.Load
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomEntrance
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomExit)
            {
                return back;
            }
            targetp = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[lback]);
            if (dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.Load
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomEntrance
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomExit)
            {
                return lback;
            }
            targetp = CurrentPoint.Add(CommonFunction.CharacterDirectionVector[rback]);
            if (dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.Load
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomEntrance
                || dun.Dungeon.DungeonMap.Get(targetp).State == LoadStatus.RoomExit)
            {
                return rback;
            }

            //対象が全くなかったらMatch
            return CharacterDirection.Match;
        }

        //複数ある場合
        //残った対象からランダムで選択
        return MoveTargetDir[UnityEngine.Random.Range(0, MoveTargetDir.Count)];

    }

    /// <summary>
    /// ターゲットに対して対象の方向に準じた最も近いポイントに移動する
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public void ApproachTarget(ManageDungeon dungeon, CharacterDirection dir)
    {
        //移動不能の場合は移動させない
        if(IsMove == false)
        {
            ChangeDirection(dir);
            RestActionTurn--;
            return;
        }

        //混乱していた場合
        if ((CharacterAbnormalState & (int)StateAbnormal.Confusion) != 0)
        {
            //方向を適当に変更
            dir = CommonFunction.ReverseDirection.Keys.ElementAt(UnityEngine.Random.Range(0, 8));
        }
        //麻痺の場合は移動無し　向きだけ変える
        if ((CharacterAbnormalState & (int)StateAbnormal.Palalysis) != 0)
        {
            ChangeDirection(dir);
            RestActionTurn--;
            return;
        }

        //二分の一の確率で左右は分ける
        bool isright = CommonFunction.IsRandom(0.5f);

        //優先順位リスト
        List<CharacterDirection> list =
            CommonFunction.DirectionPriority[dir][isright];

        //優先順位の高い方向順に移動方向を決定
        //for(int i = 0; i< list.Keys.Count; i++)
        foreach (CharacterDirection c in list)
        {
            //CharacterDirection c = list.Keys.ElementAt(i);
            bool result = CheckMove(dungeon, c);
            if (result == true)
            {
                return;
            }
        }
        //どこにも移動できずに終了した場合は何もせずにターン消費
        RestActionTurn--;
    }
    

    private AreaDirection WhereTargetArea(MapPoint target)
    {
        //自オブジェクトとの距離を取得
        MapPoint dist = target .Distance(CurrentPoint);

        //距離が同じ
        if (dist.X == 0 && dist.Y == 0)
        {
            return AreaDirection.Match;
        }
        else
        //横方向の間隔が大きい
        if (Mathf.Abs(dist.X) > Mathf.Abs(dist.Y))
        {
            if (dist.X > 0)
            {
                return AreaDirection.Right;
            }
            else
            {
                return AreaDirection.Left;
            }
        }
        //縦方向の間隔が大きい
        else
        {
            if (dist.Y > 0)
            {
                return AreaDirection.Bottom;
            }
            else
            {
                return AreaDirection.Top;
            }
        }
    }
    private CharacterDirection WhereTarget(MapPoint target)
    {
        //自オブジェクトとの距離を取得
        MapPoint dist = CurrentPoint.Distance(target);

        //距離が同じ
        if (dist.X == 0 && dist.Y == 0)
        {
            return CharacterDirection.Match;
        }
        else
        //横方向が0以下（左上、左、左下）
        if (dist.X > 0)
        {
            if (dist.Y > 0)
            {
                return CharacterDirection.BottomLeft;
            }
            else if (dist.Y < 0)
            {
                return CharacterDirection.TopLeft;
            }
            else
            {
                return CharacterDirection.Left;
            }
        }
        else
        //横方向が0以上（右上、右、右下）
        if (dist.X < 0)
        {
            if (dist.Y > 0)
            {
                return CharacterDirection.BottomRight;
            }
            else if (dist.Y < 0)
            {
                return CharacterDirection.TopRight;
            }
            else
            {
                return CharacterDirection.Right;
            }
        }
        //横方向が一致
        else
        {
            if (dist.Y > 0)
            {
                return CharacterDirection.Bottom;
            }
            else if (dist.Y < 0)
            {
                return CharacterDirection.Top;
            }
            else
            {
                return CharacterDirection.Match;
            }
        }
        //        ' 警官と犯人の位置関係を調べる共通関数：引数 id:警官Id、値 0:一致、1:右、2:上、3:左、4:下
        //Function Where(ByVal id As Integer) As Integer
        //  Dim x, y As Integer
        //  x = P(id, 1) - F(1)                                           ' x:警官と犯人の縦方向の間隔
        //  y = P(id, 2) - F(2)                                           ' y:  同　　　　横方向の間隔
        //  If x = 0 And y = 0 Then                                       ' 警官と犯人の位置が一致（縦、横共同じ）なら
        //    Where = 0                                                   '   値:0
        //  ElseIf Abs(x) > Abs(y) Then                                   ' 縦方向の間隔が大きい
        //    If x > 0 Then                                               '   下に居るなら
        //      Where = 4                                                 '   値:4
        //    Else                                                        '   上なら
        //      Where = 2                                                 '   値:2
        //    End If
        //  Else                                                          ' 横方向の間隔が大きい
        //    If y > 0 Then                                               '   右に居るなら
        //      Where = 1                                                 '   値:1
        //    Else                                                        '   左なら
        //      Where = 3                                                 '   値:3
        //    End If
        //  End If
        //End Function
    }

    private static long[] BossTargets = new long[]
    {
        10024,
        10025,
        10026,
        10027,
        10028,
        10029,
        10030
    };

    public bool IsFourBallCount()
    {
        if(BossTargets.Contains(ObjNo))
        {
            return false;
        }
        FourBallCount++;
        if(FourBallCount >= 4)
        {
            return true;
        }
        return false;
    }

    public bool IsBoss()
    {
        if (BossTargets.Contains(ObjNo))
        {
            return true;
        }
        return false;
    }

    public override void CheckNextLevel(AttackInformation atinf)
    {
        if (CurrentExperience >= NextLevelExperience)
        {
            Level++;
            if (TableEnemy.SetLevel(this, Level, 
                DisplayInformation.Info.Floor,
                DungeonInformation.Info.EnemyHpProb, 
                DungeonInformation.Info.EnemyAtkProb, 
                DungeonInformation.Info.EnemyExpProb,
                DungeonInformation.Info.StartProbHp,
                DungeonInformation.Info.StartProbAtk,
                DungeonInformation.Info.StartProbExp)  ==false)
            {
                return;
            }
            if (CommonFunction.IsNull(AttackInfo) == true)
            {
                AttackInfo = new AttackInformation();
            }

            AttackInfo.AddMessage(
                string.Format(CommonConst.Message.LevelUpPlayer, DisplayNameInMessage));
            AttackInfo.AddEffect(EffectFlareCore.CreateObject(this));
            //EffectFlareCore.CreateObject(this).Play();
        }
    }
    

    protected WeaponFreeHandEnemy equipWeapon;
    public override WeaponBase EquipWeapon
    {
        get
        {
            if (CommonFunction.IsNull(equipWeapon) == false)
            {
                return equipWeapon;
            }
            equipWeapon = WeaponFreeHandEnemy.CreateWeapon();
            equipWeapon.Initialize();
            equipWeapon.WeaponBaseDexterity = this.Dexterity;
            equipWeapon.Range = FiringRange;
            equipWeapon.WeaponBaseCritical = this.Critical;
            equipWeapon.WeaponBaseDexterityCritical = this.CriticalDexterity;
            equipWeapon.WEType = this.WEType;

            //付与する状態異常の値を分割
            // カンマ区切りで分割して配列に格納する
            if (string.IsNullOrEmpty(AddAbnormalProb) == false)
            {
                string[] stArrayData = AddAbnormalProb.Split(',');
                foreach (string tarval in stArrayData)
                {
                    string[] vals = tarval.Split(':');
                    int val = int.Parse(vals[0]);
                    float prob = float.Parse(vals[1]);
                    equipWeapon.AddAbnormal += val;
                    equipWeapon.AddAbnormalProb[(StateAbnormal)val] = prob;
                }
            }
            
            return equipWeapon;
        }
    }

    private RingBase equipRing;
    public override RingBase EquipRing
    {
        get
        {
            //if(EType == EnemyType.Alarmer_1)
            //{
            //    if(CommonFunction.IsNull(equipRing) == false)
            //    {
            //        return equipRing;
            //    }
            //    else
            //    {
            //        equipRing = TableRing.GetItem(22018);
            //        return equipRing;
            //    }
            //}
            return RingFreeHand.Instance;
        }
    }

    public override string GetMessageDeath(int exp)
    {
        return string.Format(CommonConst.Message.DeathEnemy, DisplayNameInMessage, exp);
    }
    public override string GetMessageAttackHit(string enemyname, int damage)
    {
        return string.Format(CommonConst.Message.AttackEnemy, enemyname, DisplayNameInMessage, damage);
    }
    public override string GetMessageAttackMiss()
    {
        return string.Format(CommonConst.Message.AttackMissEnemy, DisplayNameInMessage);
    }

    public MapPoint MoveApproachPlayer(CharacterDirection dir)
    {
        return MapPoint.Get(1, 1);

        //' 警官が普通に犯人に近づく共通ルーチン、引数 m:逃げる方向（1:右、2:上、3:左、4:下）
//Sub Pmove(ByVal id As Integer, ByVal m As Integer)
//  Select Case m                                                 ' Where:警官と犯人の位置関係 0～4 を返す共通関数
//    Case 1
//      P(id, 2) = Application.Max(P(id, 2) - 1, 1)               ' 左に移動、左端(1)を出ないように最小 1
//    Case 2
//      P(id, 1) = Application.Min(P(id, 1) + 1, 30)              ' 下に移動、下端(30)を出ないように最大 30
//    Case 3
//      P(id, 2) = Application.Min(P(id, 2) + 1, 30)              ' 右に移動、右端(30)を出ないように最大 30
//    Case 4
//      P(id, 1) = Application.Max(P(id, 1) - 1, 1)               ' 上に移動、上端(1)を出ないように最小 1
//    Case Else                                                   ' それ以外(0)なら
//      R = id                                                    ' 広域変数 R=警官Id（逮捕成功）
//  End Select
//End Sub
    }


    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                equipRing = null;
                equipWeapon = null;
                TempEnemyUseItem = null;
                _TargetMovePoint = null;
            }
        }
        base.Dispose(disposing);
    }
}


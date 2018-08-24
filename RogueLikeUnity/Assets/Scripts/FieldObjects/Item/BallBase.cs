using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BallBase : BaseItem
{
    public BallType BType;
    public int FireBallFixDamage = 20;
    public int GyroFixDamage = 5;
    public sbyte RestCount;
    public bool IsExp = true;

    public override bool IsAnalyse
    {
        get
        {
            return GameStateInformation.Info.AnalyseNames.ContainsKey(ObjNo) == false;
        }
    }
    public override void ClearAnalyse()
    {
        if (GameStateInformation.Info.AnalyseNames.ContainsKey(ObjNo) == true)
        {
            GameStateInformation.Info.AnalyseNames.Remove(ObjNo);
        }
    }

    public override string DisplayNameInItem
    {
        get
        {
            if (IsAnalyse == true)
            {
                return string.Format(CommonConst.Format.CountItemName, base.DisplayNameInItem, RestCount);
            }
            else
            {
                return string.Format(CommonConst.Format.CountItemNameWithColor,
                    GameStateInformation.Info.GetUnknownName(ObjNo),
                    RestCount, CommonConst.Color.NotAnalyse);
            }
        }
    }

    public BallBase Copy()
    {
        BallBase c = new BallBase();

        c.Initialize(this.BType);
        c.ObjNo = this.ObjNo;
        c.DisplayName = this.DisplayName;
        c.ThrowDexterity = this.ThrowDexterity;
        if (CommonFunction.IsNull(this.Options) == false)
        {
            c.Options = this.Options;
        }

        return c;
    }

    public override void SetInstanceName()
    {
        InstanceName = "CommonBall";
        PosY = 2.58f;
    }

    public override void SetThrowInstanceName()
    {
        switch (BType)
        {
            case BallType.Fire:
                InstanceName = "FireBall";
                PosY = 3;
                break;
            default:
                InstanceName = "CommonBall";
                break;
        }
    }

    public override long GetSortNo()
    {
        if (IsAnalyse == true)
        {
            return ObjNo;
        }
        else
        {
            return GameStateInformation.Info.GetUnknownSort(ObjNo);
        }
    }

    public void Initialize(BallType type)
    {
        base.Initialize();
        InstanceName = "CommonBall";
        IType = ItemType.Ball;
        BType = type;
        RestCount = 1;
        IsDriveProb = true;

    }


    /// <summary>
    /// 投げる開始
    /// </summary>
    public override bool ThrowStart(PlayerCharacter player)
    {
        base.ThrowStart(player);
        ChangeDirection(player.Direction);
        //RestCount--;
        if (RestCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool ThrowAction(ManageDungeon dun, PlayerCharacter player, BaseCharacter target)
    {
        if (CommonFunction.IsRandom(ThrowDexterity) == false)
        {
            DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.AttackMissPlayer, target.DisplayNameInMessage));
            Options = null;
            return true;
        }

        //あたりメッセージ
        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.HitThrowAfter, target.DisplayNameInMessage));

        return ThrowActionEffect(dun, player, target);
    }
    /// <summary>
    /// 投げる効果（誰かに当たった時）
    /// </summary>
    private bool ThrowActionEffect(ManageDungeon dun, PlayerCharacter player, BaseCharacter target)
    {
        switch(BType)
        {
            case BallType.Fire:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Bomb);

                int damage = FireBallFixDamage + Mathf.FloorToInt( player.BaseAttack * 2);

                AttackState atState = target.AddDamage(damage);
                //ボールエフェクト
                EffectFireBallLanding.CreateObject(target).Play();

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

                    if (target.Type == ObjectType.Player)
                    {
                        ScoreInformation.Info.CauseDeath =
                            string.Format(CommonConst.DeathMessage.Item, this.DisplayNameNormal);

                        ScoreInformation.Info.CauseDeathType = DeathCouseType.Item;
                    }

                    DisplayInformation.Info.AddMessage(
                                target.GetMessageDeath(target.HaveExperience));

                    player.Death(target, player.AttackInfo);
                    target.DeathAction(dun);
                }
                break;
            case BallType.Gyro:

                SoundInformation.Sound.Play(SoundInformation.SoundType.BucketFall);

                //ターゲットが吹き飛ぶ先のポイントを取得
                bool isWall;
                MapPoint point = dun.GetBlow(target.CurrentPoint, Direction,out isWall);

                //対象のポイントが取得できなければ
                if (CommonFunction.IsNull(point) == true)
                {

                }
                //対象ポイントが取得できればそこに移動
                else
                {
                    if(typeof(BaseEnemyCharacter) == target.GetType())
                    {
                        ((BaseEnemyCharacter)target).MoveState = EnemySearchState.FightPlayer;
                    }
                    DisplayInformation.Info.AddMessage(
                                    string.Format(CommonConst.Message.BlowCharacter, target.DisplayNameInMessage));
                    target.BlowDirection = Direction;
                    target.BlowPoint = point;
                    target.MoveSpeed = CommonConst.SystemValue.MoveSpeedDash;
                    dun.MoveCharacter(point, target);
                    
                    //対象に衝突ダメージ
                    if(isWall == true)
                    {
                        target.BlowAfterDamage = GyroFixDamage;
                    }

                    //吹っ飛び先に誰かがいた場合
                    MapPoint vector = CommonFunction.CharacterDirectionVector[Direction];
                    MapPoint next = MapPoint.Get(point.X + vector.X, point.Y + vector.Y);
                    BaseCharacter second = dun.CharacterMap.Get(point.X, point.Y);
                    if (CommonFunction.IsNull(second) == false)
                    {
                        second.BlowAfterDamage = GyroFixDamage;
                        second.BlowPoint = second.CurrentPoint;
                    }
                }

                break;
            case BallType.Change:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                if (typeof(BaseEnemyCharacter) == target.GetType())
                {
                    ((BaseEnemyCharacter)target).MoveState = EnemySearchState.FightPlayer;
                }
                //ターゲットとプレイヤーの場所を交換

                EffectSmoke.CreateObject(target).Play();
                EffectSmoke.CreateObject(player).Play();
                MapPoint tarp = target.CurrentPoint;
                target.SetPosition(player.CurrentPoint.X, player.CurrentPoint.Y);
                player.SetPosition(tarp.X, tarp.Y);
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.ChangePoint, target.DisplayNameInMessage));
                break;
            case BallType.Pickoff:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //対象に混乱を付与
                int result = target.AddStateAbnormal((int)StateAbnormal.Confusion);
                //対象が混乱になったらメッセージを表示
                if(result != 0)
                {
                    EffectSmoke.CreateObject(target).Play();
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Confusion, target));
                }
                break;
            case BallType.Bean:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //対象に麻痺を付与
                int bresult = target.AddStateAbnormal((int)StateAbnormal.Palalysis);
                //対象が麻痺になったらメッセージを表示
                if (bresult != 0)
                {
                    EffectSmoke.CreateObject(target).Play();
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Palalysis, target));
                }
                break;
            case BallType.Decoy:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //対象にデコイを付与
                int dresult = target.AddStateAbnormal((int)StateAbnormal.Decoy);
                //対象がデコイになったらメッセージを表示
                if (dresult != 0)
                {
                    EffectSmoke.CreateObject(target).Play();
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Decoy, target));

                }
                break;
            case BallType.Slow:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //対象にスローを付与
                int sresult = target.AddStateAbnormal((int)StateAbnormal.Slow);
                //対象がスローになったらメッセージを表示
                if (sresult != 0)
                {
                    EffectSmoke.CreateObject(target).Play();
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Slow, target));
                }
                break;
                //ナックル
            case BallType.Knuckle:

                List<BallType> blist = new List<BallType>();
                foreach (BallType val in Enum.GetValues(typeof(BallType)))
                {
                    blist.Add(val);
                }
                blist.Remove(BallType.Knuckle);
                blist.Remove(BallType.Handmaid);

                //ナックル以外の効果をランダムで取得
                BallType temp = blist[UnityEngine.Random.Range(0, blist.Count)];

                BallBase b = new BallBase();
                b.CurrentPoint = CurrentPoint;
                b.Direction = Direction;
                b.BType = temp;
                //効果を発動
                b.ThrowActionEffect(dun, player, target);
                break;
            case BallType.Trap:

                TrapBreaker(dun, player);
                break;

            case BallType.Handmaid:
                
                //ヒットエフェクト
                EffectFireBallLanding.CreateObject(target).Play();

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

                //範囲拡大がついているとき
                int plus;
                float cf;
                int dist = 0;
                if (CommonFunction.HasOptionType(this.Options, OptionType.Wildfire, out plus, out cf) == true)
                {
                    dist = plus;
                }

                dun.SetUpCharacterMap();
                List<BaseCharacter> targets = dun.GetNearCharacters(target.CurrentPoint, dist, true);

                foreach (BaseCharacter t in targets)
                {
                    //int damage2 = FireBallFixDamage + Mathf.FloorToInt(player.BaseAttack * 2);
                    int damage2 = Mathf.FloorToInt((15 + player.BaseAttack) / (target.CurrentPoint.DistanceAbs(t.CurrentPoint) + 1));

                    //効果増幅があったら
                    if (CommonFunction.HasOptionType(this.Options, OptionType.EffectUp, out plus, out cf) == true)
                    {
                        damage2 = damage2 + plus * (int)cf;
                    }
                    //効果縮小があったら
                    if (CommonFunction.HasOptionType(this.Options, OptionType.EffectDown, out plus, out cf) == true)
                    {
                        damage2 = damage2 - plus * (int)cf;
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

                    damage2 += Mathf.CeilToInt(UnityEngine.Random.Range(-rand * damage2, rand * damage2));

                    //通常ダメージの場合
                    if (CommonFunction.HasOptionType(this.Options, OptionType.ReverceDamage) == false)
                    {
                        AttackState atState2 = t.AddDamage(damage2);

                        //ダメージエフェクト
                        EffectDamage d2 = EffectDamage.CreateObject(t);
                        d2.SetText(damage2.ToString(), AttackState.Hit);
                        d2.Play();

                        //ヒットメッセージ
                        DisplayInformation.Info.AddMessage(
                            t.GetMessageAttackHit(damage2));

                        //対象が死亡したら
                        if (atState2 == AttackState.Death)
                        {
                            DisplayInformation.Info.AddMessage(
                                t.GetMessageDeath(t.HaveExperience));

                            if (t.Type == ObjectType.Player)
                            {
                                ScoreInformation.Info.CauseDeath =
                                    string.Format(CommonConst.DeathMessage.Item, this.DisplayNameNormal);
                                
                                ScoreInformation.Info.CauseDeathType = DeathCouseType.Item;

                                t.Death();
                            }
                            else
                            {
                                if (IsExp == true)
                                {
                                    player.Death(t, player.AttackInfo);
                                }
                                else
                                {
                                    t.Death();
                                }
                            }
                            t.DeathAction(dun);
                        }
                    }
                    //基本効果反転
                    else
                    {
                        t.RecoverHp(damage2);

                        //ヒットエフェクト
                        EffetHitShockWave.CreateObject(t).Play();

                        //ダメージエフェクト
                        EffectDamage d2 = EffectDamage.CreateObject(t);
                        d2.SetText(damage2.ToString(), AttackState.Heal);
                        d2.Play();

                        //ヒットメッセージ
                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.RecoverHp, t.DisplayNameInMessage));

                    }
                }

                //状態異常の付与
                EffectAbnormal(targets);

                //火柱があれば火柱を立てる
                if (CommonFunction.HasOptionType(this.Options,OptionType.CatchingFire) == true)
                {
                    dun.SetUpTrapMap();
                    if (CommonFunction.IsNull(dun.TrapMap.Get(CurrentPoint)) == true)
                    {
                        BaseTrap trap = TableTrap.GetTrap(CommonConst.ObjNo.Ember);
                        trap.Options = CommonFunction.CloneOptions(this.Options);
                        trap.IsVisible = true;
                        trap.SetThisDisplayTrap(CurrentPoint.X, CurrentPoint.Y);
                        dun.AddNewTrap(trap);

                        DisplayInformation.Info.AddMessage(
                            CommonConst.Message.TrapEmber);
                    }
                }
                
                break;

            case BallType.Fumble:
                //対象のレベルが2以上ならレベルを下げる
                if (target.Level > 1)
                {
                    if (target.Type == ObjectType.Enemy)
                    {
                        BaseEnemyCharacter enemy = ((BaseEnemyCharacter)target);
                        enemy.Level--;
                        TableEnemy.SetLevel(enemy, enemy.Level,
                            DisplayInformation.Info.Floor,
                            DungeonInformation.Info.EnemyHpProb,
                            DungeonInformation.Info.EnemyAtkProb,
                            DungeonInformation.Info.EnemyExpProb,
                            DungeonInformation.Info.StartProbHp,
                            DungeonInformation.Info.StartProbAtk,
                            DungeonInformation.Info.StartProbExp);

                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.LevelDownPlayer,enemy.DisplayNameInMessage));

                        EffectBadSmoke.CreateObject(enemy).Play();
                    }
                }

                break;

            case BallType.Winning:

                if (target.Type == ObjectType.Enemy)
                {
                    BaseEnemyCharacter enemy = ((BaseEnemyCharacter)target);
                    enemy.Level++;
                    TableEnemy.SetLevel(enemy, enemy.Level,
                        DisplayInformation.Info.Floor,
                        DungeonInformation.Info.EnemyHpProb,
                        DungeonInformation.Info.EnemyAtkProb,
                        DungeonInformation.Info.EnemyExpProb,
                        DungeonInformation.Info.StartProbHp,
                        DungeonInformation.Info.StartProbAtk,
                        DungeonInformation.Info.StartProbExp);

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.LevelUpPlayer, enemy.DisplayNameInMessage));

                    EffectFlareCore.CreateObject(enemy).Play();
                }
                break;

            case BallType.Four:

                if (target.Type == ObjectType.Enemy)
                {
                    BaseEnemyCharacter enemy = ((BaseEnemyCharacter)target);

                    if(enemy.IsFourBallCount() == true)
                    {

                        int dam = Mathf.CeilToInt(enemy.MaxHp);

                        AttackState atstate = target.AddDamage(dam);
                        //ボールエフェクト
                        EffetHitShockWave.CreateObject(enemy).Play();
                        
                        //ダメージエフェクト
                        EffectDamage dt = EffectDamage.CreateObject(target);
                        dt.SetText(dam.ToString(), AttackState.Hit);
                        dt.Play();

                        //ヒットメッセージ
                        DisplayInformation.Info.AddMessage(
                                target.GetMessageAttackHit(dam));

                        SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

                        //対象が死亡したら
                        if (atstate == AttackState.Death)
                        {

                            if (target.Type == ObjectType.Player)
                            {
                                ScoreInformation.Info.CauseDeath =
                                    string.Format(CommonConst.DeathMessage.Item, this.DisplayNameNormal);

                                ScoreInformation.Info.CauseDeathType = DeathCouseType.Item;
                            }
                            DisplayInformation.Info.AddMessage(
                                            target.GetMessageDeath(target.HaveExperience));

                            player.Death(target, player.AttackInfo);
                            target.DeathAction(dun);
                        }
                    }
                    else
                    {
                        SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);
                        //ボールエフェクト
                        EffetHitShockWave.CreateObject(enemy).Play();
                    }
                    
                }

                break;

            case BallType.Emery:

                if (target.Type == ObjectType.Enemy)
                {
                    BaseEnemyCharacter enemy = ((BaseEnemyCharacter)target);

                    if (enemy.IsBoss() == false)
                    {
                        GameStateInformation.Info.EmeryTarget = target.ObjNo;

                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.EmeryCharacter, target.DisplayNameInMessage));

                        List<BaseCharacter> emeryies = dun.Characters.FindAll(i => i.ObjNo == target.ObjNo);

                        SoundInformation.Sound.Play(SoundInformation.SoundType.Summon);

                        foreach (BaseCharacter c in emeryies)
                        {
                            EffectSmoke.CreateObject(c, false).Play();

                            dun.RemoveCharacter(c);
                            ManageDungeon.KillObjectNow(c);
                        }
                    }
                }
                break;
        }
        Options = null;

        return true;
    }

    /// <summary>
    /// 投げる効果（誰にも当たらなかったとき）
    /// </summary>
    public override bool ThrowDrop(ManageDungeon dun, PlayerCharacter player)
    {
        switch (BType)
        {
            case BallType.Trap:
                TrapBreaker(dun, player);
                break;
        }
        Options = null;

        return true;
    }

    public void TrapBreaker(ManageDungeon dun, BaseCharacter target)
    {

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.AttackHit);

        //自オブジェクトとの距離を取得
        int dist = CurrentPoint.DistanceAbs(target.CurrentPoint);
        
        MapPoint vector = CommonFunction.CharacterDirectionVector[target.Direction];
        MapPoint checkpoint = target.CurrentPoint;
        dun.SetUpTrapMap();
        //対象位置までのトラップをすべて破壊
        for (int i = 0; i <= dist; i++)
        {
            BaseTrap tr = dun.TrapMap.Get(checkpoint);
            if (CommonFunction.IsNull(tr) == false)
            {
                EffectSmoke.CreateObject(tr,false).Play();
                dun.RemoveTrap(tr);
                ManageDungeon.KillObjectNow(tr);

                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapBreak, tr.DisplayNameInMessage,
                    CommonFunction.StateNames[StateAbnormal.Slow]));
            }
            //1マス先を取得
            checkpoint = checkpoint.Add(vector);
        }
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
        //if ((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Throw);//投げる

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
}


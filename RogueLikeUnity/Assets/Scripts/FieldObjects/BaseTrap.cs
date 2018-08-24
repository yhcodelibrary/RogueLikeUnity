using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseTrap : BaseObject
{
    /// <summary>
    /// トラップが作動したか
    /// </summary>
    public bool IsInvocation;
    public TrapType TType;
    
    public int CommonNumber = 200;
    public float PerPlayerDamage = 1.05f;
    public int CountStart = 1;
    public float ProbStart = 0.9f;
    public float ProbReduce = 1.5f;
    private BaseCharacter Target;


    /// <summary>
    /// 持っているオプション
    /// </summary>
    public List<BaseOption> Options;

    public override void Initialize()
    {
        base.Initialize();
        Type = ObjectType.Trap;
        PosY = 2.56f;
        MoveSpeed = CommonConst.SystemValue.MoveSpeedItemThrow;
        IsVisible = false;
        IsInvocation = false;
    }

    public void SetThisDisplayTrap(int x, int y)
    {

        this.SetThisDisplayObject(x, y);
        IsActive = false;

        if (CommonFunction.HasOptionType(this.Options,OptionType.ReverceDamage) == true)
        {
            if(TType == TrapType.Ember)
            {
                ParticleSystem.MainModule set = ThisDisplayObject.GetComponent<ParticleSystem>().main;
                set.startColor = new ParticleSystem.MinMaxGradient(Color.green);
            }
        }
    }

    public bool CanStartup(BaseCharacter target)
    {

        if (this.TType == TrapType.Ember)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// トラップ起動
    /// </summary>
    public void Startup(ManageDungeon dun, BaseCharacter player)
    {
        IsVisible = true;
        IsInvocation = true;
        Target = player;
        //メッセージの出力
        switch (TType)
        {
            case TrapType.Bomb:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapBigBang);

                break;
            case TrapType.ColorMonitor:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapColor);
                break;
            case TrapType.Cyclone:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapCyclone);

                break;
            case TrapType.Electric:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapElectric, player.DisplayNameInMessage));
                break;
            case TrapType.Mud:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCommon, DisplayNameInMessage));

                break;
            case TrapType.Palalysis:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapSmoke);

                break;
            case TrapType.Photo:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapCroquette);

                break;
            case TrapType.Poison:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCommon, DisplayNameInMessage));

                break;
            case TrapType.DeadlyPoison:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCommon, DisplayNameInMessage));

                break;
            case TrapType.Rotation:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCommon, DisplayNameInMessage));

                break;
            case TrapType.SandStorm:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapSandStorm);

                break;
            case TrapType.Song:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapSong);
                break;
            case TrapType.Summon:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCommon, DisplayNameInMessage));
                break;
            case TrapType.TheFly:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapFly);

                break;
            case TrapType.WaterBucket:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapWaterBucket);

                break;
            case TrapType.Pollen:

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapPllen, player.DisplayNameInMessage));

                break;
        }
    }

    public bool Invocate(ManageDungeon dun)
    {
        if (IsInvocation == false)
        {
            return false;
        }
        IsActive = true;
        IsInvocation = false;
        int bresult;

        //オプションによるトラップ回避
        float t = 0;
        foreach (BaseOption o in Target.Options)
        {
            t += o.DexTrap();
        }

        if (CommonFunction.IsRandom(t) == true)
        {
            DisplayInformation.Info.AddMessage(CommonConst.Message.DexTrap);
            Target = null;
            return false;
        }

        //スコア値の更新
        DungeonHistoryInformation.Info.iTrapInvokeCount++;

        //サウンドを鳴らす
        if (Target.Type == ObjectType.Player)
        {
            VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.Voice.PlayRandomDefence());
        }

        bool result = false;
        //効果発動
        switch (TType)
        {
            case TrapType.Bomb:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Bomb);

                //エフェクトの発動
                EffectBigBang.CreateObject(this).Play();

                //ダメージ処理
                int damage = Mathf.CeilToInt(Target.CurrentHp / PerPlayerDamage);
                AttackState atState = Target.AddDamage(damage);

                //プレイヤーが死亡したら
                if (atState == AttackState.Death)
                {
                    DisplayInformation.Info.AddMessage(
                        Target.GetMessageDeath(Target.HaveExperience));

                    ScoreInformation.Info.CauseDeath =
                        string.Format(CommonConst.DeathMessage.Trap, DisplayNameNormal);
                    ScoreInformation.Info.CauseDeathType = DeathCouseType.Trap;

                    Target.Death();
                    Target.DeathAction(dun);
                }


                //ダメージエフェクト
                EffectDamage d = EffectDamage.CreateObject(Target);
                d.SetText(damage.ToString(), AttackState.Hit);
                d.Play();

                //ヒットメッセージ
                DisplayInformation.Info.AddMessage(
                    Target.GetMessageAttackHit(damage));

                //周辺キャラのダメージ処理
                dun.SetUpCharacterMap();
                List<BaseCharacter> list = dun.GetNearCharacters(this.CurrentPoint, 1);
                foreach (BaseCharacter c in list)
                {
                    atState = c.AddDamage(CommonNumber);
                    EffectDamage d2 = EffectDamage.CreateObject(c);
                    d2.SetText(CommonNumber.ToString(), AttackState.Hit);
                    d2.Play();

                    //対象が死亡したら
                    if (atState == AttackState.Death)
                    {

                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.DeathCommon, c.DisplayNameInMessage));

                        c.Death();
                        c.DeathAction(dun);
                    }
                }
                result = true;
                break;
            case TrapType.ColorMonitor:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectColorMonitor.CreateObject(this).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.StiffShoulder);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.StiffShoulder, Target));
                }
                result = true;
                break;
            case TrapType.Cyclone:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Cyclone);

                //エフェクトの発動
                EffectCyclone.CreateObject(Target).Play();

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapCyclone2, Target.DisplayNameInMessage));

                //効果

                result = true;
                break;
            case TrapType.Electric:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.ElectricTrap);

                //エフェクトの発動
                EffectThunder.CreateObject(Target).Play();

                BaseItem[] equips = PlayerCharacter.ItemList.FindAll(i => i.IsEquip == true).ToArray();

                if (equips.Length > 0)
                {
                    foreach (BaseItem i in equips)
                    {
                        i.ForceRemoveEquip(Target);
                    }

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.TrapEquipRemove, Target.DisplayNameInMessage));
                }

                result = true;
                break;
            case TrapType.Mud:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectBadSmoke.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Slow);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Slow, Target));
                }
                result = true;
                break;
            case TrapType.Palalysis:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectBadSmoke.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Palalysis);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Palalysis, Target));

                }
                result = true;
                break;
            case TrapType.Photo:
                if (Target.Type == ObjectType.Player)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                    //エフェクトの発動
                    EffectBadSmoke.CreateObject(Target).Play();

                    //メッセージの出力
                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.TrapCroquette2, Target.DisplayNameInMessage));

                    ((PlayerCharacter)Target).ReduceSatiety(CommonNumber);
                    result = true;
                }
                break;
            case TrapType.Poison:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectBadSmoke.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Poison);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Poison, Target));
                }

                result = true;
                break;
            case TrapType.DeadlyPoison:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectBadSmoke.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.DeadlyPoison);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.DeadlyPoison, Target));
                }

                result = true;
                break;
            case TrapType.Rotation:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Rotation);

                //エフェクトの発動
                EffectRotation.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Confusion);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    EffectSmoke.CreateObject(Target);
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Confusion, Target));

                }


                result = true;
                break;
            case TrapType.SandStorm:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Cyclone);

                //エフェクトの発動
                EffectSandStorm.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Dark);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Dark, Target));
                }
                result = true;
                break;
            case TrapType.Song:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectBadSmoke.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Sleep);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Sleep, Target));
                }
                result = true;
                break;
            case TrapType.Summon:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Summon);

                //エフェクトの発動
                EffectSummon.CreateObject(Target).Play();

                int cnt = CommonFunction.ConvergenceRandom(CountStart, ProbStart, ProbReduce);

                for (int i = 0; i < cnt; i++)
                {
                    dun.SetUpCharacterMap();
                    //敵の出現地点を取得
                    MapPoint mp = dun.GetCharacterEmptyTarget(Target.CurrentPoint);
                    if (CommonFunction.IsNull(mp) == true)
                    {
                        break;
                    }

                    int enemytype = TableEnemyMap.GetValue(dun.DungeonObjNo, DisplayInformation.Info.Floor);
                    uint rand = CommonFunction.GetRandomUInt32();
                    BaseEnemyCharacter enemy = TableEnemyIncidence.GetEnemy(enemytype, rand, DisplayInformation.Info.Floor);

                    enemy.SetCharacterDisplayObject(mp.X, mp.Y);
                    dun.AddNewCharacter(enemy);
                }
                DisplayInformation.Info.AddMessage(
                    CommonConst.Message.TrapSummon);

                result = true;
                break;
            case TrapType.TheFly:

                if (Target.Type == ObjectType.Player)
                {
                    //サウンドを鳴らす
                    SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                    //エフェクトの発動
                    EffectBadSmoke.CreateObject(Target).Play();

                    BaseItem[] foods = PlayerCharacter.ItemList.FindAll(i => i.IType == ItemType.Food).ToArray();

                    if (foods.Length > 0)
                    {
                        foreach (BaseItem i in foods)
                        {
                            PlayerCharacter.RemoveItem(i);
                            ((PlayerCharacter)Target).AddItem(TableFood.GetItem(CommonConst.ObjNo.FlyCroquette), i.SortNo);
                        }

                        //メッセージの出力
                        DisplayInformation.Info.AddMessage(
                            string.Format(CommonConst.Message.TrapFly2));
                    }

                    result = true;
                }
                break;
            case TrapType.WaterBucket:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.BucketFall);

                //エフェクトの発動
                EffectWaterBucket.CreateObject(Target).Play();

                //効果
                bresult = Target.AddStateAbnormal((int)StateAbnormal.Reticent);
                //対象が異常になったらメッセージを表示
                if (bresult != 0)
                {
                    DisplayInformation.Info.AddMessage(
                        CommonFunction.GetAbnormalMessage(StateAbnormal.Reticent, Target));
                }

                result = true;
                break;
            //花粉
            case TrapType.Pollen:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                //エフェクトの発動
                EffectSmoke d3 = EffectSmoke.CreateObject(Target);
                d3.SetColor(Color.yellow);
                d3.Play();

                //力減少
                Target.ReducePower((ushort)CommonNumber);

                //メッセージの出力
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.TrapPllen2, Target.DisplayNameInMessage));

                result = true;
                break;
            case TrapType.Ember:

                //ダメージ処理
                int damage2 = CommonNumber;

                //通常ダメージの場合
                if (CommonFunction.HasOptionType(this.Options,OptionType.ReverceDamage) == false)
                {
                    AttackState atState2 = Target.AddDamage(damage2);

                    //ダメージエフェクト
                    EffectDamage d4 = EffectDamage.CreateObject(Target);
                    d4.SetText(damage2.ToString(), AttackState.Hit);
                    d4.Play();

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.TrapDamage, Target.DisplayNameInMessage, this.DisplayNameInMessage, damage2));

                    //対象が死亡したら
                    if (atState2 == AttackState.Death)
                    {
                        DisplayInformation.Info.AddMessage(
                            Target.GetMessageDeath(Target.HaveExperience));

                        if (Target.Type == ObjectType.Player)
                        {
                            ScoreInformation.Info.CauseDeath =
                                string.Format(CommonConst.DeathMessage.Trap, DisplayNameNormal);
                            ScoreInformation.Info.CauseDeathType = DeathCouseType.Trap;
                        }
                        Target.Death();
                        Target.DeathAction(dun);
                    }
                }
                //反転回復の場合
                else
                {
                    Target.RecoverHp(damage2);

                    //ダメージエフェクト
                    EffectDamage d4 = EffectDamage.CreateObject(Target);
                    d4.SetText(damage2.ToString(), AttackState.Heal);
                    d4.Play();


                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.TrapRecover, Target.DisplayNameInMessage, this.DisplayNameInMessage, damage2));
                }
                break;
        }
        Target = null;

        return result;
    }

    public BaseTrap[] TurnAction(ManageDungeon dun)
    {
        //効果発動
        switch (TType)
        {
            case TrapType.Ember:

                int wf = 0;
                if (CommonFunction.IsNull(Options) == false)
                {
                    foreach (BaseOption o in Options)
                    {
                        if (o.OType == OptionType.Wildfire)
                        {
                            wf = o.Plus;
                            o.Plus--;
                        }
                    }
                }

                //燃え広がる
                if (wf > 0)
                {
                    //周囲からトラップのない位置を取得
                    MapPoint point = dun.GetEmptyTrapPoint(CurrentPoint);
                    //場所が取得できたら燃え広がる
                    if (CommonFunction.IsNull(point) == false)
                    {
                        BaseTrap trap = TableTrap.GetTrap(CommonConst.ObjNo.Ember);
                        trap.IsVisible = true;
                        trap.SetThisDisplayTrap(CurrentPoint.X, CurrentPoint.Y);
                        trap.Options = CommonFunction.CloneOptions(this.Options);

                        return  new BaseTrap[] { trap };
                    }
                }
                break;
        }

        return null;
    }


    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Target = null;
                if (CommonFunction.IsNull(Options) == false)
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

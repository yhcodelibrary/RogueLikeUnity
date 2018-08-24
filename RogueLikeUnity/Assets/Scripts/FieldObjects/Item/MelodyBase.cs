using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MelodyBase : BaseItem
{
    public MelodyType MType;

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
                return string.Format(CommonConst.Format.DefaultName, DisplayNameBefore, DisplayName, DisplayNameAfter);
            }
            else
            {
                return string.Format(CommonConst.Format.UnknownNameWithColor,
                    GameStateInformation.Info.GetUnknownName(ObjNo),
                    CommonConst.Color.NotAnalyse
                    );
            }
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

    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonMelody";
        IType = ItemType.Melody;
        IsDriveProb = true;
    }
    
    /// <summary>
    /// 奏でる
    /// </summary>
    public override bool Melody(ManageDungeon dun, PlayerCharacter player)
    {
        BaseCharacter[] targets;

        player.AttackInfo.Initialize();

        //音を鳴らす
        player.AttackInfo.AddVoice(VoiceInformation.VoiceType.Sing);

        switch (MType)
        {
            //放電
            case MelodyType.Electric:

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.ElectricMelody);

                const int ElectricDamage = 20;
                int damage = ElectricDamage + Mathf.FloorToInt(player.BaseAttack);

                //エフェクト
                player.AttackInfo.AddEffect(EffectSpark.CreateObject(player));

                //メッセージ
                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                //部屋の中のキャラクターを取得
                targets = dun.GetRoomCharacters(player);

                //全員にダメージ
                foreach(BaseCharacter c in targets)
                {
                    AttackState atState = c.AddDamage(damage);

                    //ダメージエフェクト
                    EffectDamage d = EffectDamage.CreateObject(c);
                    d.SetText(damage.ToString(), AttackState.Hit);
                    player.AttackInfo.AddEffect(d);

                    //ヒットメッセージ
                    player.AttackInfo.AddMessage(
                        c.GetMessageAttackHit(damage));

                    //対象が死亡したら
                    if (atState == AttackState.Death)
                    {
                        player.AttackInfo.AddKillList(c);

                        player.AttackInfo.AddMessage(
                            c.GetMessageDeath(c.HaveExperience));

                        player.Death(c, player.AttackInfo);
                    }
                }


                break;
                //狂乱
            case MelodyType.Confusion:

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.Smoke);

                //メッセージ
                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                //部屋の中のキャラクターを取得
                targets = dun.GetRoomCharacters(player);

                //全員を混乱
                foreach (BaseCharacter c in targets)
                {

                    //対象に混乱を付与
                    int result = c.AddStateAbnormal((int)StateAbnormal.Confusion);
                    //対象が混乱になったらメッセージを表示
                    if (result != 0)
                    {

                        player.AttackInfo.AddEffect(EffectSmoke.CreateObject(c));

                        player.AttackInfo.AddMessage(
                                CommonFunction.GetAbnormalMessage(StateAbnormal.Confusion, c));
                    }
                }
                break;
                //まどろみ
            case MelodyType.Sleep:

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.Smoke);

                //メッセージ
                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                //部屋の中のキャラクターを取得
                targets = dun.GetRoomCharacters(player);

                //全員を睡眠
                foreach (BaseCharacter c in targets)
                {

                    //対象に睡眠を付与
                    int result = c.AddStateAbnormal((int)StateAbnormal.Sleep);
                    //対象が睡眠になったらメッセージを表示
                    if (result != 0)
                    {
                        player.AttackInfo.AddEffect(EffectSmoke.CreateObject(c));

                        player.AttackInfo.AddMessage(
                            CommonFunction.GetAbnormalMessage(StateAbnormal.Sleep, c));

                    }
                }
                break;
            //無秩序
            case MelodyType.Anarchy:

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.Smoke);

                //メッセージ
                player.AttackInfo.AddMessage(string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                //部屋の中のキャラクターを取得
                targets = dun.GetRoomCharacters(player);

                //全員をデコイ
                foreach (BaseCharacter c in targets)
                {

                    //対象にデコイを付与
                    int result = c.AddStateAbnormal((int)StateAbnormal.Decoy);
                    //対象がデコイになったらメッセージを表示
                    if (result != 0)
                    {
                        player.AttackInfo.AddEffect(EffectSmoke.CreateObject(c));

                        player.AttackInfo.AddMessage(
                            CommonFunction.GetAbnormalMessage(StateAbnormal.Decoy, c));
                        
                    }
                }
                break;
                //薄ら日
            case MelodyType.Light:

                //エフェクト
                player.AttackInfo.AddEffect(EffectFlash.CreateObject());

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.Smoke);

                //メッセージ
                player.AttackInfo.AddMessage(string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                dun.IsVisible = true;
                dun.IsEnemyVisible = true;
                dun.IsItemVisible = true;
                dun.IsTrapVisible = true;
                break;

            //角笛
            case MelodyType.Horn:

                //サウンドを鳴らす
                player.AttackInfo.AddSound(SoundInformation.SoundType.Summon);

                //エフェクトの発動
                player.AttackInfo.AddEffect(EffectSummon.CreateObject(player));

                int cnt = CommonFunction.ConvergenceRandom(3, 0.9f, 1.2f, 10);

                for (int i = 0; i < cnt; i++)
                {
                    dun.SetUpCharacterMap();
                    //敵の出現地点を取得
                    MapPoint mp = dun.GetCharacterEmptyTarget(player.CurrentPoint);
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

                break;

            //　忘却
            case MelodyType.Forget:
                
                //メッセージ
                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                //効果音
                player.AttackInfo.AddSound(SoundInformation.SoundType.Summon);

                if (dun.IsVisible == false)
                {
                    for (int j = 1; j < dun.X - 1; j++)
                    {
                        for (int i = 1; i < dun.Y - 1; i++)
                        {
                            dun.Dungeon.DungeonMap.Get(i, j).IsClear = false;
                        }
                    }

                    player.AttackInfo.AddMessage(CommonConst.Message.ForgetMap);

                    player.AttackInfo.AddEffect(EffectSmoke.CreateObject(player));
                }

                break;

            //捨て置き
            case MelodyType.ThrowAway:

                //メッセージ
                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.MelodySing, DisplayNameInMessage));

                player.AttackInfo.AddMessage(
                    string.Format(CommonConst.Message.ThrowAway, player.DisplayNameInMessage));

                GameStateInformation.Info.IsThrowAway = true;

                break;
        }
        return true;
    }

    public override Dictionary<int, MenuItemActionType> GetItemAction()
    {
        int i = 0;
        Dictionary<int, MenuItemActionType> dic = new Dictionary<int, MenuItemActionType>();
        bool has = PlayerCharacter.HasItemPlayer(this);
        if(has == false && GameStateInformation.Info.IsThrowAway == false)
        {
            dic.Add(i++, MenuItemActionType.Get);//拾う
        }
        //if ((DisplayInformation.Info.State & (int)StateAbnormal.Reticent) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Melody);//奏でる
        //if ((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Throw);//投げる
        if (has == true)
        {
            dic.Add(i++, MenuItemActionType.Put);//置く
        }

        //ドライブに空きがあれば入れるを追加
        //if(PlayerCharacter.HasDriveGap() == true)
        //{
        //    dic.Add(i++, MenuItemActionType.Putin);
        //}
        return dic;
    }
}
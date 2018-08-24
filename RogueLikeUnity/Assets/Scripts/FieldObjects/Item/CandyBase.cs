using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CandyBase : BaseItem
{

    public CandyType CType;

    public override bool IsAnalyse
    {
        get
        {
            return GameStateInformation.Info.AnalyseNames.ContainsKey(ObjNo) == false;
        }
    }
    public override void ClearAnalyse()
    {
        if(GameStateInformation.Info.AnalyseNames.ContainsKey(ObjNo) == true)
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
        InstanceName = "CommonCandy";
        IType = ItemType.Candy;
        IsDriveProb = true;
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
        dic.Add(i++, MenuItemActionType.Use);//使う
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


    /// <summary>
    /// 食べる
    /// </summary>
    public override bool Use(ManageDungeon dun, BaseCharacter player)
    {
        switch (CType)
        {
            case CandyType.Energy:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Recover);

                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.RecoverPower));

                player.RecoverPower(99);

                break;
            case CandyType.Handmaid:

                this.CommonUseItemCustom(dun, player, player);

                break;

            case CandyType.Bomb:

                List<BaseCharacter> list = dun.GetNearCharacters(player.CurrentPoint, 2, true);

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Bomb);

                //エフェクトの発動
                EffectBigBang.CreateObject(player).Play();

                //ダメージ処理
                int damage = Mathf.CeilToInt(player.MaxHp / 2);
                if(damage > 50)
                {
                    damage = 50;
                }
                foreach (BaseCharacter c in list)
                {
                    AttackState atState = c.AddDamage(damage);
                    EffectDamage d2 = EffectDamage.CreateObject(c);
                    d2.SetText(damage.ToString(), AttackState.Hit);
                    d2.Play();

                    //対象が死亡したら
                    if (atState == AttackState.Death)
                    {
                        //プレイヤーが死亡したら
                        if (c.Type == ObjectType.Player)
                        {
                            DisplayInformation.Info.AddMessage(
                                player.GetMessageDeath(player.HaveExperience));

                            ScoreInformation.Info.CauseDeath =
                                string.Format(CommonConst.DeathMessage.Trap, DisplayNameNormal);
                            ScoreInformation.Info.CauseDeathType = DeathCouseType.Trap;
                        }
                        else
                        {
                            DisplayInformation.Info.AddMessage(
                                string.Format(CommonConst.Message.DeathCommon, c.DisplayNameInMessage));
                        }

                        c.Death();
                        c.DeathAction(dun);
                    }

                }

                break;

            case CandyType.Garlic:

                //サウンドを鳴らす
                SoundInformation.Sound.Play(SoundInformation.SoundType.Recover);

                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.IncreaseHp));

                player.MaxHpCorrection += 5;

                break;


            default:
                this.CommonUseItem(player);
                break;
        }
        return true;
    }


    /// <summary>
    /// 投げる効果（誰かに当たった時）
    /// </summary>
    public override bool ThrowAction(ManageDungeon dun, PlayerCharacter player, BaseCharacter target)
    {
        if (CommonFunction.IsRandom(ThrowDexterity) == true)
        {
            //あたりメッセージ
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.HitThrowAfter, target.DisplayNameInMessage));

            Use(dun,target);
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
}


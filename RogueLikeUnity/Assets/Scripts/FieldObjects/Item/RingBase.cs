using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RingBase : BaseItem
{

    /// <summary>
    /// 防ぐステータス異常
    /// </summary>
    public int PreventAbnormal;

    public RingType RType;

    /// <summary>
    /// 使った回数
    /// </summary>
    public int TotalRing;

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
        InstanceName = "CommonRing";
        IType = ItemType.Ring;
        RType = RingType.None;
        IsDriveProb = true;
        TotalRing = 0;
    }

    public override bool Equip(BaseCharacter target)
    {
        base.Equip(target);

        return ForceEquip(target);
    }

    public override bool ForceEquip(BaseCharacter target)
    {
        //装備中にする
        IsEquip = true;

        //盗人なら汚名を付与
        if (RType == RingType.Thief && string.IsNullOrEmpty(target.DisplayNameBefore) == true)
        {
            QualifyInformation bname = TableQualify.GetRandomName(1);
            target.DisplayNameBefore = bname.Name;
            target.DisplayNameBeforeObjNo = bname.ObjNo;
        }

        return true;
    }

    public override bool ForceRemoveEquip(BaseCharacter target)
    {
        base.ForceRemoveEquip(target);
        IsEquip = false;
        return true;
    }

    /// <summary>
    /// 装備解除
    /// </summary>
    public override bool RemoveEquip(BaseCharacter target, bool isOutMessage = true)
    {
        bool result = base.RemoveEquip(target, isOutMessage);

        return result;
    }

    public float GetItemConvergence(float con)
    {
        if(RType == RingType.Thief)
        {
            con -= (con - 1) / 2;
            ScoreInformation.Info.AddScore(-1500);
        }
        return con;
    }

    public float GetEnemyConvergence(float con)
    {
        if (RType == RingType.Ryu)
        {
            con -= (con - 1) / 2;
        }
        return con;
    }

    public float GetTrapConvergence(float con)
    {
        if (RType == RingType.EvilLuck)
        {
            con -= (con - 1) / 2;
        }
        return con;
    }

    //状態異常抵抗値の反映
    public void SetAbnormalPrevent(Dictionary<StateAbnormal,float> prevents)
    {
        switch(RType)
        {
            //耐性指輪による抵抗値の格納
            case RingType.AbnormalPrevent:

                foreach (StateAbnormal val in CommonFunction.StateAbnormals)
                {
                    if((PreventAbnormal & (int)val) != 0)
                    {
                        prevents[val] = 1;
                    }
                }
                break;
            //壮健
            case RingType.Health:

                foreach (StateAbnormal val in CommonFunction.BadAbnormals)
                {
                    prevents[val] = 0.3f;
                }
                break;
        }
    }

    //状態異常抵抗値の反映
    public float GetAbnormalPrevent(StateAbnormal val)
    {
        float prevent = 0;
        switch (RType)
        {
            //耐性指輪による抵抗値の格納
            case RingType.AbnormalPrevent:

                if ((PreventAbnormal & (int)val) != 0)
                {
                    prevent = 1;
                }
                break;
            //壮健
            case RingType.Health:
                prevent = 0.3f;
                break;
        }
        return prevent;
    }

    //状態異常抵抗値の反映
    public bool CheckBreak()
    {
        //聞き耳、目星、オカルトは一定確率で壊れる。
        if(RType == RingType.Listen || RType == RingType.Occult || RType == RingType.OneEyes)
        {
            if(CommonFunction.IsRandom(0.01f) == true)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckTunnel()
    {
        if(RType == RingType.Tunnel)
        {
            return true;
        }
        return false;
    }


    public void TurnFInish(BaseCharacter target, ManageDungeon dun)
    {
        //ふたつの指輪だったら
        if (RType == RingType.TwoRing)
        {
            //毎ターンスコアを減額
            ScoreInformation.Info.AddScore(-100);

            //キャラが正常だったら状態異常をランダム付与
            if (target.CharacterAbnormalState == 0)
            {
                VoiceInformation.Voice.Play(PlayerInformation.Info.PType, VoiceInformation.Voice.PlayRandomDefence());
                SoundInformation.Sound.Play(SoundInformation.SoundType.Smoke);

                EffectSmoke.CreateObject(target).Play();

                StateAbnormal st = CommonFunction.GetRandomAbnormal();
                //switch (st)
                //{
                //    case StateAbnormal.Sleep:
                //        DisplayInformation.Info.AddMessage(
                //            string.Format(CommonConst.Message.AddSleep, target.DisplayNameInMessage));
                //        break;
                //    case StateAbnormal.Slow:
                //        DisplayInformation.Info.AddMessage(
                //            string.Format(CommonConst.Message.AddSlow, target.DisplayNameInMessage));
                //        break;
                //    case StateAbnormal.Dark:
                //        DisplayInformation.Info.AddMessage(
                //            string.Format(CommonConst.Message.AddDark, target.DisplayNameInMessage));
                //        break;
                //    case StateAbnormal.StiffShoulder:
                //        DisplayInformation.Info.AddMessage(
                //            string.Format(CommonConst.Message.AddShoulder, target.DisplayNameInMessage));
                //        break;
                //    default:
                //        DisplayInformation.Info.AddMessage(
                //            string.Format(CommonConst.Message.AddState, target.DisplayNameInMessage,
                //            CommonFunction.StateNames[st]));
                //        break;
                //}

                DisplayInformation.Info.AddMessage(
                    CommonFunction.GetAbnormalMessage(st, target));
                target.AddStateAbnormal(st);
            }
        }
        else if (RType == RingType.Unlucky)
        {
            if (CommonFunction.IsRandom(0.01f) == true)
            {
                //落とす対象を取得
                BaseItem[] targets = PlayerCharacter.ItemList.FindAll(i => i.IsEquip == false && i.IsDrive == false).ToArray();

                //対象があればランダムで落とす
                if (targets.Length > 0)
                {
                    //対象の中からランダムで一つ抽出
                    BaseItem tar = targets[UnityEngine.Random.Range(0, targets.Length)];

                    DisplayInformation.Info.AddMessage(
                        string.Format(CommonConst.Message.ThrowOffItem,
                        target.DisplayNameInMessage,
                        tar.DisplayNameInMessage
                        ));

                    //プレイヤーから持ち物削除
                    PlayerCharacter.RemoveItem(tar);

                    dun.PutItem(tar, this.CurrentPoint, this.CurrentPoint);

                    tar.ResetObjectPosition();
                }
            }
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
        if (IsEquip == true)
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

using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BagBase : BaseItem
{
    /// <summary>
    /// 入っているアイテム
    /// </summary>
    public List<BaseItem> BagItems;

    public BagType BgType;

    /// <summary>
    /// 最大詰め数
    /// </summary>
    public sbyte MaxGap;
    
    public override string DisplayNameInItem
    {
        get
        {
            if(IsAnalyse == true)
            {
                return string.Format(CommonConst.Format.CountPerItemName, DisplayName, BagItems.Count, MaxGap);
            }
            else
            {
                return string.Format(CommonConst.Format.CountPerItemNameWithColor,
                    GameStateInformation.Info.GetUnknownName(ObjNo),
                    BagItems.Count, 
                    MaxGap, CommonConst.Color.NotAnalyse);
            }
        }
    }

    /// <summary>
    /// 中身を表示するか
    /// </summary>
    public bool IsDisplayContents;

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
        InstanceName = "CommonBag";
        IType = ItemType.Bag;
        IsDriveProb = false;
        BagItems = new List<BaseItem>();
        MaxGap = 3;
        IsDisplayContents = false;
    }
    /// <summary>
    /// 投げる効果（誰にも当たらなかったとき）
    /// </summary>
    public override bool ThrowDrop(ManageDungeon dun, PlayerCharacter player)
    {
        DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.BreakItem, this.DisplayNameInMessage));
        IsThrowBreak = true;
        return true;
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
        if (IsPutinItem() == true)
        {
            dic.Add(i++, MenuItemActionType.PutinParent);//入れる
        }
        dic.Add(i++, MenuItemActionType.Look);//覗く
        //if((DisplayInformation.Info.State & (int)StateAbnormal.StiffShoulder) == 0)
        //{
        //}
        dic.Add(i++, MenuItemActionType.Throw);//投げる
        if (has == true)
        {
            dic.Add(i++, MenuItemActionType.Put);//置く
        }
        return dic;
    }

    /// <summary>
    /// アイテムを入れる
    /// </summary>
    public BaseItem PutinItem(int itype, BaseItem target,PlayerCharacter player)
    {

        //サウンドを鳴らす
        SoundInformation.Sound.Play(SoundInformation.SoundType.Putin);

        DisplayInformation.Info.AddMessage(string.Format(CommonConst.Message.PutinAction, target.DisplayNameInMessage, this.DisplayNameInMessage));

        switch(BgType)
        {
            //保存
            case BagType.Save:

                BagItems.Add(target);
                target.InDrive = this;
                return target;
                break;

                //Recycle
            case BagType.Recycle:

                //ターゲットが別のバッグに入っていたら取り出しておく
                if (target.IsDrive == true)
                {
                    BagBase bag = target.InDrive;
                    bag.PutoutItem(target);
                }

                OtherBase d = TableOther.GetItem(CommonConst.ObjNo.Dollar);
                d.Initialize();
                d.SellingPrice = target.SellingPrice;

                player.RecoverHp(999);
                EffectDamage dm = EffectDamage.CreateObject(player);
                dm.SetText("999", AttackState.Heal);
                dm.Play();
                SoundInformation.Sound.Play(SoundInformation.SoundType.Recover);

                BagItems.Add(d);
                d.InDrive = this;

                return d;
                break;
                //変化
            case BagType.Change:
                //ターゲットが別のバッグに入っていたら取り出しておく
                if(target.IsDrive == true)
                {
                    BagBase bag = target.InDrive;
                    bag.PutoutItem(target);
                }
                //uintの乱数を取得
                uint rand = CommonFunction.GetRandomUInt32();
                BaseItem ch = TableItemIncidence.GetItem(itype, rand, true);
                if (CommonFunction.IsNull(ch) == true)
                {
                    return null;
                }
                int stopper = 0;
                while (ch.IType == ItemType.Bag || ch.IType == ItemType.Material)
                {
                    if(stopper > 30)
                    {
                        return null;
                    }
                    stopper++;
                    rand = CommonFunction.GetRandomUInt32();
                    ch = TableItemIncidence.GetItem(itype, rand, true);
                }

                ch.IsAnalyse = DungeonInformation.Info.IsAnalyze == false;
                BagItems.Add(ch);
                ch.InDrive = this;

                //PlayerCharacter.RemoveItem(target);
                //player.AddItem(ch, DateTime.Now.Ticks);

                return ch;
                break;
            //コロッケ
            case BagType.Food:
                //ターゲットが別のバッグに入っていたら取り出しておく
                if (target.IsDrive == true)
                {
                    BagBase bag = target.InDrive;
                    bag.PutoutItem(target);
                }
                BaseItem tar = TableItemIncidence.GetTypeItemRandom(itype, ItemType.Food, true);
                if(CommonFunction.IsNull(tar) == true)
                {
                    return null;
                }
                BagItems.Add(tar);
                tar.InDrive = this;
                
                //PlayerCharacter.RemoveItem(target);
                //player.AddItem(tar, DateTime.Now.Ticks);

                return tar;
                break;

            //普通
            case BagType.Normal:

                BagItems.Add(target);
                target.InDrive = this;
                return target;
                break;

        }

        return target;
    }

    /// <summary>
    /// アイテムを出す
    /// </summary>
    public void PutoutItem(BaseItem target)
    {
        BagItems.Remove(target);
        target.InDrive = null;
    }

    /// <summary>
    /// アイテムが入れられるかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsPutinItem()
    {
        if(BagItems.Count >= MaxGap)
        {
            return false;
        }
        return true;
    }
    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                BagItems.Clear();
                BagItems = null;
            }
        }
        base.Dispose(disposing);
    }
}

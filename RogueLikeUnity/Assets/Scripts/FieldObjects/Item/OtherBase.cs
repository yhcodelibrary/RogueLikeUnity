using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OtherBase : BaseItem
{

    public OtherType OType;

    public override string DisplayNameInItem
    {
        get
        {
            return string.Format(CommonConst.Format.PriceItemName, base.DisplayNameInItem, SellingPrice);
        }
    }

    public override string DisplayNameInMessage
    {
        get
        {
            return string.Format(CommonConst.Format.PriceItemName, base.DisplayNameInItem, SellingPrice);
        }
    }
    public override bool IsAnalyse
    {
        get
        {
            return true;
        }
        set
        {
            _IsAnalyse = value;
        }
    }

    /// <summary>
    /// 攻撃力
    /// オプション値も含む
    /// </summary>
    public override float ItemAttack
    {
        get { return this.SellingPrice; }
    }

    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonOther";
        IType = ItemType.Other;
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


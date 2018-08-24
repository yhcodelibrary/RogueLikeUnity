using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MaterialBase : BaseItem
{
    public MaterialType MType;

    public override string DisplayNameNormal
    {
        get
        {
            if (StrengthValue > 0)
            {
                return string.Format(CommonConst.Format.StrengthItemName, base.DisplayNameNormal, StrengthValue);
            }
            else
            {
                return base.DisplayNameNormal;
            }
        }
    }

    public override string DisplayNameInItem
    {
        get
        {
            return DisplayNameNormal;
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
    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonMaterial";
        IType = ItemType.Material;
        IsDriveProb = false;
        Weight = 0.1f;
        Options = new List<BaseOption>();
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
        //if(PlayerCharacter.HasDriveGap() == true)
        //{
        //    dic.Add(i++, MenuItemActionType.Putin);
        //}
        return dic;
    }
}

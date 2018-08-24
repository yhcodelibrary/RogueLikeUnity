using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class FoodBase : BaseItem
{
    public FoodType FType;

    public override void Initialize()
    {
        base.Initialize();
        InstanceName = "CommonFood";
        IType = ItemType.Food;
        IsDriveProb = true;
        SatRecoverPoint = 50;
        FType = FoodType.Common;
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
    /// 投げる効果（誰かに当たった時）
    /// </summary>
    public override bool ThrowAction(ManageDungeon dun, PlayerCharacter player, BaseCharacter target)
    {
        if (CommonFunction.IsRandom(ThrowDexterity) == true)
        {
            //あたりメッセージ
            DisplayInformation.Info.AddMessage(
                string.Format(CommonConst.Message.HitThrowAfter, target.DisplayNameInMessage));

            Eat(dun, target);
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

    public override Dictionary<int, MenuItemActionType> GetItemAction()
    {
        int i = 0;
        Dictionary<int, MenuItemActionType> dic = new Dictionary<int, MenuItemActionType>();
        bool has = PlayerCharacter.HasItemPlayer(this);
        if (has == false && GameStateInformation.Info.IsThrowAway == false)
        {
            dic.Add(i++, MenuItemActionType.Get);//拾う
        }
        dic.Add(i++, MenuItemActionType.Eat);//食べる
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
    public override bool Eat(ManageDungeon dun, BaseCharacter player)
    {
        DisplayInformation.Info.AddMessage(
            string.Format(CommonConst.Message.EatItem, this.DisplayNameInMessage));

        switch(FType)
        {
            case FoodType.Common:
                this.CommonUseItem(player);
                break;
            case FoodType.Fly:
                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.BadFood, player.DisplayNameInMessage));
                this.CommonUseItem(player);
                break;
            case FoodType.Curry:

                this.CommonUseItem(player);

                DisplayInformation.Info.AddMessage(
                    string.Format(CommonConst.Message.RecoverPower));

                player.RecoverPower(99);
                break;
            case FoodType.Handmaid:
                this.CommonUseItemCustom(dun, player, player);
                break;
        }
        return true;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
public class WeaponFreeHand : WeaponBase
{
    private static WeaponFreeHand instance;

    public static WeaponFreeHand Instance
    {
        get
        {
            if (CommonFunction.IsNull(instance) == false)
            {
                return instance;
            }
            instance = new WeaponFreeHand();
            instance.Initialize();
            return instance;
        }
    }

    private WeaponFreeHand()
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        DisplayName = CommonConst.Message.BareHands;
        WeaponBaseDexterity = 0.92f;
        WeaponBaseAttack = 5;
    }
}

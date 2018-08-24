using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShieldFreeHand : ShieldBase
{
    private static ShieldFreeHand instance;

    public static ShieldFreeHand Instance
    {
        get
        {
            if (CommonFunction.IsNull(instance) == false)
            {
                return instance;
            }
            instance = new ShieldFreeHand();
            instance.Initialize();
            return instance;
        }
    }
    public override void Initialize()
    {
        base.Initialize();
        DisplayName = "-";
    }
}

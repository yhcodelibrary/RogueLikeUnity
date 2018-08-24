using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RingFreeHand : RingBase
{
    private static RingFreeHand instance;

    public static RingFreeHand Instance
    {
        get
        {
            if (CommonFunction.IsNull(instance) == false)
            {
                return instance;
            }
            instance = new RingFreeHand();
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

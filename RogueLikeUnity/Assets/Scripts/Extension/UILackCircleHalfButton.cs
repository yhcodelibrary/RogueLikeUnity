using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UILackCircleHalfButton : UILackCircleButton
{


    protected override void Start()
    {
        base.Start();
        radiusout3 = 100;
        radiusin3 = 69;
    }
}


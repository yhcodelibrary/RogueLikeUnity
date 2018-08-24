using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UILackCircleButton : Button, ICanvasRaycastFilter
{

    public float radiusout3;
    public float radiusin3;
    //public float startrad = -0.4f;
    //public float endrad = 0.4f;
    public const float startAng5 = 0f;
    public const float endAng5 = 45f;
    public float startAng;
    public float endAng;
    private CharacterDirection Direction;

    protected override void Start()
    {
        base.Start();
        radiusout3 = 145;
        radiusin3 = 102;
        startAng = startAng5;
        endAng = endAng5;
        if(startAng > 360)
        {
            startAng -= 360;
        }
        if (endAng > 360)
        {
            endAng -= 360;
        }
        if (this.colors.fadeDuration == 0.001f)
        {
            Direction = CharacterDirection.Right;
        }
        else if (this.colors.fadeDuration == 0.002f)
        {
            Direction = CharacterDirection.TopRight;
            startAng += 45;
            endAng += 45;
        }
        else if (this.colors.fadeDuration == 0.003f)
        {
            Direction = CharacterDirection.Top;
            startAng += 90;
            endAng += 90;
        }
        else if (this.colors.fadeDuration == 0.004f)
        {
            Direction =CharacterDirection.TopLeft;
            startAng += 135;
            endAng += 135;
        }
        else if (this.colors.fadeDuration == 0.005f)
        {
            Direction =CharacterDirection.Left;
            startAng += 180;
            endAng += 180;
        }
        else if (this.colors.fadeDuration == 0.006f)
        {
            Direction =CharacterDirection.BottomLeft;
            startAng += 225;
            endAng += 225;
        }
        else if (this.colors.fadeDuration == 0.007f)
        {
            Direction =CharacterDirection.Bottom;
            startAng += 270;
            endAng += 270;
        }
        else if (this.colors.fadeDuration == 0.008f)
        {
            Direction =CharacterDirection.BottomRight;
            startAng += 315;
            endAng += 315;
        }

        //startrad = (startAng) * Mathf.Deg2Rad;
        //endrad = (endAng) * Mathf.Deg2Rad % 360;
    }


    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {

        float dist = Vector2.Distance(sp, transform.position);

        float x = (sp.x - transform.position.x);
        float y = (sp.y - transform.position.y);

        //移動単位の取得
        //Vector3 velocity = (new Vector2(x,y)).normalized;

        //Debug.Log(Direction);
        //Debug.Log(velocity);
        //Debug.Log(CommonFunction.CheckDirectionVector(Direction, new Vector3(x, 0, y).normalized));

        if (CommonFunction.CheckDirectionVector(Direction, new Vector3(x,0, y).normalized) == false)
        {
            return false;
        }

        float ang = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 22.5f ;
        if (ang < 0)
        {
            ang = 360 + ang;
        }

        bool res = radiusout3 > dist && dist > radiusin3
            && endAng > ang && ang > startAng;

        //Debug.Log(radiusout3);
        //Debug.Log(radiusin3);
        //Debug.Log(dist);

        return res;
    }
}


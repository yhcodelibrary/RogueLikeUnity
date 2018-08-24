using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectWaterBucket : EffectBase
{
    public Vector3 Target;

    private static readonly Vector3 velocity = new Vector3(0, CommonConst.SystemValue.MoveSpeedWaterBucket, 0);

    private void Update()
    {
        //transform.localPosition += new Vector3(0, -5f, 0) * Time.deltaTime;
        transform.localPosition += CommonFunction.GetVelocity(velocity, 1);
        if (transform.localPosition.y < Target.y)
        {
            End();
        }
    }

    public static EffectWaterBucket CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("WaterBucket").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectWaterBucket d = obj.AddComponent<EffectWaterBucket>();
        //d.Parent = obj;
        EffectWaterBucket d = GetGameObject<EffectWaterBucket>(false, "WaterBucket", ResourceInformation.Effect.transform);

        d.transform.localPosition = new Vector3(t.ThisDisplayObject.transform.position.x,
            t.ThisDisplayObject.transform.transform.position.y + 3,
            t.ThisDisplayObject.transform.position.z);

        d.Target = new Vector3(t.ThisDisplayObject.transform.position.x,
            t.ThisDisplayObject.transform.transform.position.y,
            t.ThisDisplayObject.transform.position.z);
        return d;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectBigBang : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(3);
    }

    public static EffectBigBang CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("BigBang").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectBigBang d = obj.AddComponent<EffectBigBang>();
        //d.Parent = obj;
        EffectBigBang d = GetGameObject<EffectBigBang>(false, "BigBang", ResourceInformation.Effect.transform);

        Vector3 v = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
            t.ThisDisplayObject.transform.transform.localPosition.y + 0.5f,
            t.ThisDisplayObject.transform.localPosition.z);

        d.Parent.transform.localPosition = v;

        return d;
    }
}
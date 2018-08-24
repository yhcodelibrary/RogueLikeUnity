using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectSandStorm : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(2);
    }

    public static EffectSandStorm CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("SandStorm").gameObject,
        //    t.ThisDisplayObject.transform);
        //EffectSandStorm d = obj.AddComponent<EffectSandStorm>();
        //d.Parent = obj;
        EffectSandStorm d = GetGameObject<EffectSandStorm>(true, "SandStorm", t.ThisDisplayObject.transform);

        Vector3 v = Vector3.zero;

        d.Parent.transform.localPosition = v;

        return d;
    }
}

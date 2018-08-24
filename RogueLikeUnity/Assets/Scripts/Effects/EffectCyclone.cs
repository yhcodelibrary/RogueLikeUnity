using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectCyclone : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(7);
    }

    public static EffectCyclone CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Cyclone").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectCyclone d = obj.AddComponent<EffectCyclone>();
        //d.Parent = obj;
        EffectCyclone d = GetGameObject<EffectCyclone>(false, "Cyclone", ResourceInformation.Effect.transform);

        Vector3 v = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
            t.ThisDisplayObject.transform.transform.localPosition.y + 0.5f,
            t.ThisDisplayObject.transform.localPosition.z);

        d.Parent.transform.localPosition = v;

        return d;
    }
}

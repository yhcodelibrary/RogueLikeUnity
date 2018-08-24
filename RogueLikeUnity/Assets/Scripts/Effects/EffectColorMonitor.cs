using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectColorMonitor : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(3);
    }

    public static EffectColorMonitor CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("ColorMonitor").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectColorMonitor d = obj.AddComponent<EffectColorMonitor>();
        //d.Parent = obj;
        EffectColorMonitor d = GetGameObject<EffectColorMonitor>(false, "ColorMonitor", ResourceInformation.Effect.transform);

        Vector3 v = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
            t.ThisDisplayObject.transform.transform.localPosition.y + 0.5f,
            t.ThisDisplayObject.transform.localPosition.z);

        d.Parent.transform.localPosition = v;

        return d;
    }
}
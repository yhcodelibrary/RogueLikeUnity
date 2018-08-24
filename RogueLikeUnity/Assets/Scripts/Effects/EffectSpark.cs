using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectSpark : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(3);
    }

    public static EffectSpark CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Spark").gameObject,
        //    t.ThisDisplayObject.transform);
        ////obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //EffectSpark d = obj.AddComponent<EffectSpark>();
        //d.Parent = obj;
        EffectSpark d = GetGameObject<EffectSpark>(true, "Spark", t.ThisDisplayObject.transform);

        Vector3 v = Vector3.zero;

        d.Parent.transform.localPosition = v + Vector3.up;

        return d;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectThunder : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(2);
    }

    public static EffectThunder CreateObject(BaseCharacter t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Thunder").gameObject,
        //    t.ThisDisplayObject.transform);
        //obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //EffectThunder d = obj.AddComponent<EffectThunder>();
        //d.Parent = obj;
        EffectThunder d = GetGameObject<EffectThunder>(true, "Thunder", t.ThisDisplayObject.transform);
        d.Parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Vector3 v = Vector3.zero;

        d.Parent.transform.localPosition = v;

        return d;
    }
}

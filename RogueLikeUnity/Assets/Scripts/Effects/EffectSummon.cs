using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectSummon : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(2);
    }

    public static EffectSummon CreateObject(BaseCharacter t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Summons").gameObject,
        //    t.ThisDisplayObject.transform);
        //obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //EffectSummon d = obj.AddComponent<EffectSummon>();
        //d.Parent = obj;
        EffectSummon d = GetGameObject<EffectSummon>(true, "Summons", t.ThisDisplayObject.transform);
        d.Parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Vector3 v = Vector3.zero;

        d.Parent.transform.localPosition = v;

        return d;
    }
}

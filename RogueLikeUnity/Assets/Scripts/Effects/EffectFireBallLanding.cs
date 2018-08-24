using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EffectFireBallLanding : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(2);
    }
    public static EffectFireBallLanding CreateObject(BaseObject t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("FireBallLanding").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectFireBallLanding d = obj.AddComponent<EffectFireBallLanding>();
        //d.Parent = obj;
        EffectFireBallLanding d = GetGameObject<EffectFireBallLanding>(false, "FireBallLanding", ResourceInformation.Effect.transform);

        Vector3 v = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
            t.ThisDisplayObject.transform.transform.localPosition.y + 0.5f,
            t.ThisDisplayObject.transform.localPosition.z);

        d.Parent.transform.localPosition = v;

        return d;
    }
}

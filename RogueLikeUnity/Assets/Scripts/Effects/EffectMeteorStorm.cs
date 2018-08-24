using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class EffectMeteorStorm : EffectBase
    {
        public override void Play()
        {
            base.Play();
            ParticleSystem p = Parent.transform.Find("ETF_M_Meteor Storm").GetComponent<ParticleSystem>();
            p.Clear();
            p.Play();
            Destroy(gameObject, 7);
        }

        public static EffectMeteorStorm CreateObject(BaseCharacter t)
        {

            EffectMeteorStorm d = GetGameObject<EffectMeteorStorm>(false, "MeteorStorm", ResourceInformation.Effect.transform);

            Vector3 v = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
                t.ThisDisplayObject.transform.transform.localPosition.y + 0.5f,
                t.ThisDisplayObject.transform.localPosition.z);

            d.Parent.transform.localPosition = v;

            return d;
        }
    }
}

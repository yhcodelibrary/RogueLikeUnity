using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class EffectSmoke : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(2);
    }

    public static EffectSmoke CreateObject(BaseObject t,bool isParent = true)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("Smoke").gameObject);
        EffectSmoke d;

        if (isParent == true)
        {
            d = GetGameObject<EffectSmoke>(true, "Smoke", t.ThisDisplayObject.transform);
            Vector3 v = Vector3.zero;

            d.Parent.transform.localPosition = v;
        }
        else
        {
            d = GetGameObject<EffectSmoke>(true, "Smoke", null);
            if (t.IsActive == true)
            {
                d.Parent.transform.localPosition = t.ThisDisplayObject.transform.localPosition;
            }
            else
            {
                d.Parent.transform.localPosition = new Vector3(t.ThisDisplayObject.transform.localPosition.x,
                    t.OriginalPosY,
                    t.ThisDisplayObject.transform.localPosition.z);
            }
        }
        d.Parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //EffectSmoke d = obj.AddComponent<EffectSmoke>();
        //d.Parent = obj;


        return d;
    }

    public void SetColor(Color col)
    {
        ParticleSystem.MainModule set = GetComponent<ParticleSystem>().main;
        set.startColor = new ParticleSystem.MinMaxGradient(col);
        
    }
}

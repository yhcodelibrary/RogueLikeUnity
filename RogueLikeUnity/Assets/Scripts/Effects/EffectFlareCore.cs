using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class EffectFlareCore : EffectBase
{
    public override void Play()
    {
        base.Play();
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        WaitCorutine(3);
    }

    public enum FLareCoreType
    {
        Purple,
        Blue,
        Yellow,
    }

    public static EffectFlareCore CreateObject(BaseCharacter t, FLareCoreType type = FLareCoreType.Purple)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("FlareCore").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectFlareCore d = obj.AddComponent<EffectFlareCore>();
        //d.Parent = obj;
        EffectFlareCore d;
        switch (type)
        {
            case FLareCoreType.Purple:
                d = GetGameObject<EffectFlareCore>(false, "FlareCore", ResourceInformation.Effect.transform);
                break;
            case FLareCoreType.Blue:
                d = GetGameObject<EffectFlareCore>(false, "FlareCore2", ResourceInformation.Effect.transform);
                break;
            case FLareCoreType.Yellow:
                d = GetGameObject<EffectFlareCore>(false, "FlareCore3", ResourceInformation.Effect.transform);
                break;
            default:
                d = GetGameObject<EffectFlareCore>(false, "FlareCore", ResourceInformation.Effect.transform);
                break;
        }

        Vector3 v = new Vector3(t.ThisTransform.localPosition.x,
            t.ThisTransform.transform.localPosition.y+0.5f,
            t.ThisTransform.localPosition.z);

        d.Parent.transform.localPosition = v;

        return d;
    }
    
}

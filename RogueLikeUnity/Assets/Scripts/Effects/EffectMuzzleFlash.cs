using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectMuzzleFlash : EffectBase
{
    public override void Play()
    {
        base.Play();
        this.Parent.transform.Find("MuzzleFlashBody").GetComponent<ParticleSystem>().Clear();
        this.Parent.transform.Find("MuzzleFlashBody").GetComponent<ParticleSystem>().Play();
        WaitCorutine(1);
    }

    public static EffectMuzzleFlash CreateObject(GameObject g)
    {
        EffectMuzzleFlash d = GetGameObject<EffectMuzzleFlash>(false, "MuzzleFlash", ResourceInformation.Effect.transform);

        //Vector3 v = new Vector3(g.transform.position.x,
        //    g.transform.position.y,
        //    g.transform.position.z);

        d.Parent.transform.rotation = g.transform.rotation;

        d.Parent.transform.position = g.transform.position;

        return d;
    }
}

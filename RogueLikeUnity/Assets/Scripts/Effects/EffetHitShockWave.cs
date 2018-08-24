using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffetHitShockWave : EffectBase
{
    private static readonly Vector3 Pos = new Vector3(0, 0.25f, 0);
    private static readonly Vector3 scale = new Vector3(0.5f, 0f, 0.5f);
    public override void Play()
    {
        base.Play();
        ParticleSystem p = Parent.GetComponent<ParticleSystem>();
        p.Clear();
        p.Play();
        WaitCorutine(2);
    }

    public static EffetHitShockWave CreateObject(BaseObject t)
    {
        EffetHitShockWave d = GetGameObject<EffetHitShockWave>(true, "HitShockWave", t.ThisTransform);
        d.Parent.transform.localScale = scale;

        d.Parent.transform.localPosition = Pos;

        return d;
    }
}

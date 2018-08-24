using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class EffectFlash : EffectBase
{
    ScreenOverlay so;

    public override void Play()
    {
        base.Play();
        StartCoroutine(Corutine());
        
    }

    IEnumerator Corutine()
    {
        float interval = 0.5f;
        float time = 0;
        while (time <= interval)
        {
            this.so.intensity = Mathf.Lerp(0f, 2f, time / interval);
            time += Time.smoothDeltaTime;
            yield return 0;
        }

        SpotLightMove.Instance.SetInitial(false);

        time = 0;

        while (time >= -interval)
        {
            this.so.intensity = Mathf.Lerp(0f, 2f, time / interval);
            time -= Time.smoothDeltaTime;
            yield return 0;
        }

        so.intensity = 0f;

        End();

    }

    public static EffectFlash CreateObject()
    {
        //var obj = new GameObject("EffectFlash");
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectFlash d = obj.AddComponent<EffectFlash>();
        //d.Parent = obj;
        EffectFlash d = GetGameObject<EffectFlash>(false, "EffectFlash", ResourceInformation.Effect.transform);

        d.so = Camera.main.GetComponent<ScreenOverlay>();

        return d;
    }
}
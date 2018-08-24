using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectRotation : EffectBase
{
    private BaseCharacter target;
    
    public override void Play()
    {
        base.Play();
        WaitCorutine(1);
    }

    protected override void End()
    {
        target.ChangeDirection(target.Direction);
        target = null;
        base.End();
    }
    // Use this for initialization
    private void Update()
    {
        target.ThisDisplayObject.transform.Rotate(0, CommonFunction.GetDelta(720), 0);
    }
    //private void OnDestroy()
    //{
    //    target.ChangeDirection(target.Direction);
    //}

    public static EffectRotation CreateObject(BaseCharacter t)
    {
        //var obj = GameObject.Instantiate(ResourceInformation.Effect.transform.FindChild("EffectDammy").gameObject);
        //obj.transform.SetParent(ResourceInformation.Effect.transform, false);
        //EffectRotation d = obj.AddComponent<EffectRotation>();
        //d.Parent = obj;
        EffectRotation d = GetGameObject<EffectRotation>(false, "EffectRotate", ResourceInformation.Effect.transform);

        d.target = t;
        return d;
    }
}

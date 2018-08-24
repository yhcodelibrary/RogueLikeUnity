using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

public class EffectBase : MonoBehaviour, IEquatable<EffectBase>
{
    //private static Dictionary<string, List<EffectBase>> EffectPool = new Dictionary<string, List<EffectBase>>();
    private Guid Name = Guid.NewGuid();
    protected static T GetGameObject<T> (bool isOnetime, string name,Transform parent,bool iscanvas = false) where T:EffectBase
    {
        //if (isOnetime == false)
        //{
        //    if (ResourceInformation.EffectPool.ContainsKey(name) == true)
        //    {
        //        EffectBase target = ResourceInformation.EffectPool[name].Find(i => i.isLoad == false);
        //        if (CommonFunction.IsNull(target) == false)
        //        {
        //            target.isLoad = true;
        //            target.Parent.transform.SetParent(parent);
        //            return (T)target;
        //        }
        //    }
        //    else
        //    {
        //        ResourceInformation.EffectPool.Add(name, new List<EffectBase>());
        //    }

        //    T d = GetGameObject<T>(name, parent, iscanvas);
        //    d.isOneTime = false;
        //    ResourceInformation.EffectPool[name].Add(d);

        //    return d;
        //}
        //else
        //{
        //    T d = GetGameObject<T>(name, parent, iscanvas);
        //    d.isOneTime = true;
        //    return d;
        //}

        if (ResourceInformation.EffectPool.ContainsKey(name) == true)
        {
            EffectBase target = ResourceInformation.EffectPool[name].Find(i => i.isLoad == false);
            if (CommonFunction.IsNull(target) == false)
            {
                target.TypeName = name;
                target.isLoad = true;
                target.Parent.transform.SetParent(parent);
                target.isOneTime = isOnetime;
                return (T)target;
            }
        }
        else
        {
            ResourceInformation.EffectPool.Add(name, new List<EffectBase>());
        }

        T d = GetGameObject<T>(name, parent, iscanvas);
        d.TypeName = name;
        d.isOneTime = isOnetime;
        ResourceInformation.EffectPool[name].Add(d);

        return d;
    }

    private static T GetGameObject<T>(string name, Transform parent, bool iscanvas) where T : EffectBase
    {
        GameObject obj;
        if (iscanvas == false)
        {
            obj = GameObject.Instantiate(ResourceInformation.Effect.transform.Find(name).gameObject, parent);
        }
        else
        {
            obj = GameObject.Instantiate(ResourceInformation.EffectCanvas.transform.Find(name).gameObject, parent);
        }
        T d = obj.AddComponent<T>();
        d.Parent = obj;
        d.isLoad = true;
        
        return d;
    }
    
    private string TypeName;

    static readonly Vector3 DammyPosition = new Vector3(0, 250, 0);
    protected virtual void End()
    {
        if (isOneTime == false)
        {
            IsPlay = false;
            isLoad = false;
            this.Parent.transform.localPosition = DammyPosition;
        }
        else
        {
            //ワンタイムの場合は救出を試みる
            if (this.Parent)
            {
                IsPlay = false;
                isLoad = false;
                this.Parent.transform.SetParent(ResourceInformation.Effect.transform);
                this.Parent.transform.localPosition = DammyPosition;

            }
            else
            {
                Destroy(this.Parent);
                this.parent = null;
                ResourceInformation.EffectPool[TypeName].Remove(this);
            }
        }
    }

    private GameObject parent;
    protected GameObject Parent
    {
        get
        {
            return parent;
        }
        private set
        {
            parent = value;
            parent.SetActive(false);
        }
    }
    protected bool isOneTime;
    protected bool isLoad;
    private bool isPlay;
    protected bool IsPlay
    {
        get { return isPlay; }
        set
        {
            Parent.SetActive(value);
            isPlay = value;
        }
    }
    public virtual void Play()
    {
        IsPlay = true;
    }

    protected void WaitCorutine(float time)
    {
        MainThreadDispatcher.StartUpdateMicroCoroutine(WaitCorutineExe(time));
    }

    private IEnumerator WaitCorutineExe(float time)
    {
        //float waitcount = 0;

        ////指定秒待つ
        //while (waitcount <= time)
        //{
        //    waitcount += CommonFunction.GetDelta(1);
        //    yield return null;
        //}
        float waitcount = Time.time + time;

        //指定秒待つ
        while (waitcount >= Time.time)
        {
            yield return null;
        }

        End();
    }

    public override bool Equals(object obj)
    {
        //objがnullか、型が違うときは、等価でない
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        return this.Name == ((EffectBase)obj).Name;
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }

    public bool Equals(EffectBase other)
    {
        return this.Name == other.Name;
    }
}

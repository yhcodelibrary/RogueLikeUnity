using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;


namespace Assets.Scripts
{
    public class ManageRTS : MonoBehaviour
    {
        private bool _IsWait;
        public bool IsWait
        {
            get
            {
                return _IsWait;
            }
            set
            {
                _IsWait = value;
            }
        }
        private IDisposable coroutine;


        void Awake()
        {
            // 自分自身だったり
            DontDestroyOnLoad(this);
        }
       

        public ManageRTS()
        {
            IsWait = false;
            coroutine = null;
        }


        public void Wait()
        {
            IsWait = true;
            if (CommonFunction.IsNull(coroutine) == false)
            {
                coroutine.Dispose();
            }
            coroutine = Observable.FromMicroCoroutine(() => WaitExecute(CommonConst.Wait.RTS)).Subscribe();
        }

        private IEnumerator WaitExecute(float waittime)
        {

            float waitcount = 0;

            while (waitcount < waittime)
            {
                waitcount += CommonFunction.GetDelta(1);
                yield return null;
            }
            
            coroutine = null;
            IsWait = false;
        }
    }
}

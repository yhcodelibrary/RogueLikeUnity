using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Assets.Scripts
{
    public class ManageWait : MonoBehaviour
    {
        private bool _IsWait;
        public bool IsWait
        {
            get
            {

                if (Input.anyKeyDown)
                {
                    WaitCursor = CommonConst.Wait.MenuSelect;

                    if (CommonFunction.IsNull(coroutine) == false)
                    {
                        _IsWait = false;
                        if (CommonFunction.IsNull(coroutine) == false)
                        {
                            coroutine.Dispose();
                        }
                        coroutine = null;
                    }
                }

                return _IsWait;
            }
            set
            {
                _IsWait = value;
            }
        }
        private IDisposable coroutine;

        public float WaitCursor;


        void Awake()
        {
            // 自分自身だったり
            DontDestroyOnLoad(this);
        }

        private static ManageWait _Info;
        public static ManageWait Info
        {
            get
            {

                if (CommonFunction.IsNull(_Info) == false)
                {
                    return _Info;
                }
                GameObject gm = new GameObject("WaitManager");
                _Info = gm.AddComponent<ManageWait>();
                return _Info;
            }
        }

        public ManageWait()
        {
            IsWait = false;
            coroutine = null;
            WaitCursor = CommonConst.Wait.MenuSelect;
        }
        
        public void WaitSelect()
        {
            WaitCursor = Mathf.Clamp(WaitCursor / 2, 0.03f, CommonConst.Wait.MenuSelect);

            Wait(WaitCursor);

        }


        public void Wait(float waittime)
        {
            IsWait = true;
            if(CommonFunction.IsNull(coroutine) == false)
            {
                coroutine.Dispose();
            }
            coroutine = Observable.FromMicroCoroutine(() => WaitExecute(waittime)).Subscribe();
            //coroutine = StartCoroutine(WaitExecute(waittime));
        }

        private IEnumerator WaitExecute(float waittime)
        {
            float waitcount = 0;
            
            while(waitcount < waittime)
            {
                waitcount += CommonFunction.GetDelta(1);
                yield return null;
            }
            //yield return new WaitForSeconds(waittime);

            //yield return new WaitForEndOfFrame();
            coroutine = null;
            IsWait = false;
        }
    }
}

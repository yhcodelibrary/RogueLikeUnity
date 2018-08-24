using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CountTester : MonoBehaviour
    {
        float waitcount = 0;
        private void Update()
        {

            waitcount += CommonFunction.GetDelta(1);
            if (waitcount > 1)
            {
                waitcount = 0;

                //long monoUsed = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
                //long monoSize = UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
                //long totalUsed = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong(); // == Profiler.usedHeapSize
                //long totalSize = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
                this.GetComponent<UnityEngine.UI.Text>().text =
                    (System.GC.GetTotalMemory(false) / 1024).ToString("#,0");
                    // string.Format(
                    //"mono:{0}/{1} kb({2:f1}%)\n total:{3}/{4} kb({5:f1}%)\n",
                    //monoUsed / 1024, monoSize / 1024, 100.0 * monoUsed / monoSize,
                    //totalUsed / 1024, totalSize / 1024, 100.0 * totalUsed / totalSize);
            }
        }
    }
}

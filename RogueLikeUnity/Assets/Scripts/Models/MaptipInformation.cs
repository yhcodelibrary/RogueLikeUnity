using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class MaptipInformation
    {
        public MaptipInformation(GameObject maptip)
        {
            IsActive = true;
            Maptip = maptip;
        }
        public bool IsActive;
        public GameObject Maptip;

        public void SetActive(bool bl)
        {
            if (Maptip.activeSelf != bl)
            {
                Maptip.SetActive(bl);
            }
        }

        public void OnActive()
        {
            //SetActive(true);
            IsActive = true;
        }

        public void OffActive()
        {
            SetActive(false);
            IsActive = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TitleFieldRotation : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, -1f *(60 * Time.smoothDeltaTime), 0);
    }
}

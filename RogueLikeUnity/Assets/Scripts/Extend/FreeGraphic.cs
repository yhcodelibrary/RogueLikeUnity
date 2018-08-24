using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FreeGraphic : Graphic
{
    public Vector3 ltv = Vector3.zero;
    public Vector3 rtv = Vector3.zero;
    public Vector3 lbv = Vector3.zero;
    public Vector3 rbv = Vector3.zero;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        UpdateMesh(vh);
    }
    
    private void UpdateMesh(VertexHelper vh)
    {

        vh.Clear();
        // 左上
        UIVertex lt = UIVertex.simpleVert;
        lt.position = ltv;
        lt.color = Color.green;

        // 右上
        UIVertex rt = UIVertex.simpleVert;
        rt.position = rtv;
        rt.color = Color.red;

        // 右下
        UIVertex rb = UIVertex.simpleVert;
        rb.position = rbv;
        rb.color = Color.yellow;

        // 左下
        UIVertex lb = UIVertex.simpleVert;
        lb.position = lbv;
        lb.color = Color.white;

        vh.AddUIVertexQuad(new UIVertex[] {
            lb, rb, rt, lt
        });
    }
}

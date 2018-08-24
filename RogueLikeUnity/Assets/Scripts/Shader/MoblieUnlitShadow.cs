//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Assets.Scripts.Shader
//{
//    Shader "Unlit/UnlitShadow"
//{
//    Properties
//    {
//        _MainTex("Texture", 2D) = "white" {}
//    }
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//        LOD 100
 
//        Pass {
//            SetTexture[_MainTex] {  } 
//        }
//        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
//    }
//}
//}

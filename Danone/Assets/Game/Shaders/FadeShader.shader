﻿Shader "MyShaders/FadeShader"
{
    Properties
    {
		_Color("Color",Color)=(1,1,1,1)
		_ColorReturn("ColorReturn",Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_RangeTolerancy("Range", Range(0,1)) =0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Color;
			float4 _ColorReturn;
			float _RangeTolerancy;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv)*_Color;
			if (col.r < _RangeTolerancy)
				return _ColorReturn;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

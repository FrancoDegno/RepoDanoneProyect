Shader "Unlit/ReplaceColor"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ColorToChange("ColorToReplace",Color) = (1,1,1,1)
		_DesiredColor("ColorReplace",Color) = (1,1,1,1)
		_TolerancyRed("Red Tolerate",Range(-0.05,1))=0.005
		_TolerancyGreen("Green Tolerate",Range(-1,1))=0.005
		_TolerancyBlue("Blue Tolerate",Range(-1,1))=0.005
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque"
					"Queue" = "Transparent+1"}
			LOD 100
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile DUMMY PIXELSNAP_ON

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _ColorToChange;
				float4 _DesiredColor;
				float _TolerancyRed;
				float _TolerancyGreen;
				float _TolerancyBlue;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
				  /*  fixed4 col = tex2D(_MainTex, i.uv);
				if (all(col == _ColorToReplace))
					return _ColorReplace;*/

				half4 c = tex2D(_MainTex, i.uv);

				if (c.r >= _ColorToChange.r - _TolerancyRed && c.r <= _ColorToChange.r + _TolerancyRed
					&& c.g >= _ColorToChange.g - _TolerancyGreen && c.g <= _ColorToChange.g + _TolerancyGreen
					&& c.b >= _ColorToChange.b - _TolerancyBlue && c.b <= _ColorToChange.b + _TolerancyBlue)
				{
					return _DesiredColor;
				}

				return c;

				// return col;
			 }
			 ENDCG
		 }
		}
}

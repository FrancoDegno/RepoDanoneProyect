Shader "myShaders/myDefaultSprite"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_ColorToChange("ColorToReplace",Color) = (1,1,1,1)
		_DesiredColor("ColorReplace",Color) = (1,1,1,1)
		_TolerancyRed("Red Tolerate",Range(-0.05,1)) = 0.005
		_TolerancyGreen("Green Tolerate",Range(-1,1)) = 0.005
		_TolerancyBlue("Blue Tolerate",Range(-1,1)) = 0.005
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;
				float4 _ColorToChange;
				float4 _DesiredColor;
				float _TolerancyRed;
				float _TolerancyGreen;
				float _TolerancyBlue;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

					if (c.r >= _ColorToChange.r - _TolerancyRed && c.r <= _ColorToChange.r + _TolerancyRed
					&& c.g >= _ColorToChange.g - _TolerancyGreen && c.g <= _ColorToChange.g + _TolerancyGreen
					&& c.b >= _ColorToChange.b - _TolerancyBlue && c.b <= _ColorToChange.b + _TolerancyBlue)
					{
					return _DesiredColor;
					}

					c.rgb *= c.a;
					return c;
				}
			ENDCG
			}
		}
}
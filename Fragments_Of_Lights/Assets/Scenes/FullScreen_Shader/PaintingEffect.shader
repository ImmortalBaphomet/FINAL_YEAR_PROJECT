Shader "Unlit/PaintingEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PaintStrength ("Paint Strength", Range(0, 2)) = 1
        _StrokeSize ("Stroke Size", Range(5, 50)) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
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
            float _PaintStrength;
            float _StrokeSize;

            // Function to generate a pseudo-random noise effect
            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // Function to create a paint stroke effect
            float3 PaintEffect(float2 uv, float3 color)
            {
                float noise = random(uv * _StrokeSize);
                float3 strokeVariation = lerp(color * 0.8, color * 1.2, noise); // Light/Dark strokes
                return lerp(color, strokeVariation, _PaintStrength);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv); // Get screen color
                col.rgb = PaintEffect(i.uv, col.rgb); // Apply the painting effect
                return col;
            }
            ENDHLSL
        }
    }


}

Shader "Hsinpa/UI/UIEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}

        _Color ("Tint", Color) = (1,1,1,1)
        _Blend("Blend Power", Range (0, 1)) = 0 
    }
    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color    : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color    : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            fixed4 _Color;
            float _Blend;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.color = v.color * _Color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 noiseUV = float2(i.uv.x, i.uv.y - _Time.x);
                fixed4 noise = tex2D(_NoiseTex, noiseUV);
                float noiseLerp = sin(noise.x) + 1 * 0.5;
                noise = lerp(noise, float4(0,0,0,0), noiseLerp);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + (noise.xy * _Blend));

                col = col * i.color;

                //col = lerp(col , noise , _Blend);

                col.a = i.color.a;

                return col;
            }
            ENDCG
        }
    }
}

Shader "Custom/SmoothGradientShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Weight("Weight", Range(0, 1)) = 0.0
        _ColorStart("Start Color", Color) = (1, 1, 1, 1)
        _ColorEnd("End Color", Color) = (0, 0, 1, 1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

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
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                float _Weight;
                fixed4 _ColorStart;
                fixed4 _ColorEnd;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Interpolate between start and end colors based on weight
                    fixed4 color = lerp(_ColorStart, _ColorEnd, _Weight);
                    return color;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}

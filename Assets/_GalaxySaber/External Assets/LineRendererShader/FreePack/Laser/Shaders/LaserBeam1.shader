Shader "Unlit/LaserBeam1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("_Tint", Color) = (1, 1, 1, 1)
        _ColorEmission ("Emission Color", Color) = (1, 1, 1, 1)
        _Speed ("Speed", Vector) = (0, 0, 0, 0)
        _Noise ("Noise", Vector) = (0, 0, 1, 1)
        _NoiseAmount ("Noise Amount", Float) = 0
        _DissolveAmount ("Dissolve Amount", Float) = 0
        _Emission ("Emission", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
        }

        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            Name "ForwardUnlit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma target 2.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
                half fogFactor : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Speed;
                half4 _Color;
                half4 _Noise;
                half4 _ColorEmission;
                half _DissolveAmount;
                half _NoiseAmount;
                half _Emission;
            CBUFFER_END

            inline float unity_noise_randomValue(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            inline float unity_noise_interpolate(float a, float b, float t)
            {
                return (1.0 - t) * a + (t * b);
            }

            inline float unity_valueNoise(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);
                f = f * f * (3.0 - 2.0 * f);

                float2 c0 = i + float2(0.0, 0.0);
                float2 c1 = i + float2(1.0, 0.0);
                float2 c2 = i + float2(0.0, 1.0);
                float2 c3 = i + float2(1.0, 1.0);
                float r0 = unity_noise_randomValue(c0);
                float r1 = unity_noise_randomValue(c1);
                float r2 = unity_noise_randomValue(c2);
                float r3 = unity_noise_randomValue(c3);

                float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
                float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
                return unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
            }

            void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
            {
                float t = 0.0;

                float freq = pow(2.0, 0.0);
                float amp = pow(0.5, 3.0);
                t += unity_valueNoise(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                freq = pow(2.0, 1.0);
                amp = pow(0.5, 2.0);
                t += unity_valueNoise(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                freq = pow(2.0, 2.0);
                amp = pow(0.5, 1.0);
                t += unity_valueNoise(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                Out = t;
            }

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                output.color = input.color * _Color;
                output.fogFactor = ComputeFogFactor(output.positionCS.z);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float simpleNoise;
                Unity_SimpleNoise_float(input.uv, _Noise.z, simpleNoise);

                float noiseAfterPower = pow(simpleNoise, max(_Noise.w, 0.0001));
                float2 distortedUV = lerp(input.uv, noiseAfterPower.xx, _NoiseAmount);
                float2 mainUV = distortedUV + _Speed.xy * _Time.y;

                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, mainUV);
                half4 dissolved = lerp(texColor, noiseAfterPower.xxxx * texColor.a, _DissolveAmount);
                half4 color = dissolved * input.color * _Color * _ColorEmission * _Emission;
                color.rgb = MixFog(color.rgb, input.fogFactor);
                return color;
            }
            ENDHLSL
        }
    }

    FallBack Off
}

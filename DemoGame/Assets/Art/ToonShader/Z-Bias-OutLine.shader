Shader "Roystan/Z-Bias-OutLine"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1.0)
		
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1.0)
		_Glossiness("Glossines", Float) = 32
		
		[HDR]
		_RimColor("Rim Color",Color) = (1.0,1.0,1.0,1.0)
		_RimAmount("Rim Amount", Range(0,1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0,1)) = 0.1
		
		[HDR]
		_OutLineColor("OutLine Color", Color) = (0.5,0.5,0.5,1.0)
		_OutLineAmount("OutLine Amount", Range(0,1)) = 0.05
	}
	SubShader
	{
	    Pass {
			Cull Front
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			float _OutLineAmount;
			fixed4 _OutLineColor;
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			}; 
			
			struct v2f {
			    float4 pos : SV_POSITION;
			};
			
			v2f vert (a2v v) {
				v2f o;
				
				float4 pos = mul(UNITY_MATRIX_MV, v.vertex); 
				/*float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);  
				normal.z = -5;*/
				pos.z  += _OutLineAmount;
				//pos = pos + float4(normalize(normal), 0) * _Outline;
				o.pos = mul(UNITY_MATRIX_P, pos);
				
				return o;
			}
			
			float4 frag(v2f i) : SV_Target { 
				return float4(_OutLineColor.rgb, 1);               
			}
			
			ENDCG
		}
		
		Pass
		{
		    Cull Back
			CGPROGRAM
			#pragma multi_compile_fwdbase
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                SHADOW_COORDS(4)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _AmbientColor;
			float4 _SpecularColor;
			float _Glossiness;
			float _RimAmount;
			float4 _RimColor;
			float _RimThreshold;
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float4 frag (v2f i) : SV_Target
			{
			    float3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
			    float3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float3 normal = normalize(i.worldNormal);

				float4 sample = tex2D(_MainTex, i.uv);
				
                float NdotL = dot(lightDir, normal);
                float shadow = SHADOW_ATTENUATION(i);

                float lightIntensity = smoothstep(0,0.01,NdotL * shadow);
                float4 light = lightIntensity * _LightColor0;
                
                float3 halfVector = normalize(viewDir + lightDir);
                float3 NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005,0.01,specularIntensity);
                float4 specular = _SpecularColor * specularIntensitySmooth;
                
                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;
                
                

                                
				return _Color * sample * (light + _AmbientColor + specular + rim);
			}
			ENDCG
		}
		
		// Shadow casting support.
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
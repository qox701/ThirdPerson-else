Shader "Unlit/TestShader1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("_Color",COLOR) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "RenderPipeline" = "UniversalPipeline"  "LightMode" = "UniversalForward"}
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag  
			
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"	
            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;                
                float4 vertex : SV_POSITION;
				float3 positionWS : TEXCOORD1;
            };
	    CBUFFER_START(UnityPerMaterial) //变量引入开始
		//获取属性面板颜色
                float4 _Color; 
		//获取UV重复与偏移
		float4 _MainTex_ST;
            CBUFFER_END //变量引入结束

            //获取面板纹理
            TEXTURE2D(_MainTex);
			//创建贴图收容器
            SAMPLER(sampler_MainTex);

            v2f vert (appdata v)
            {
                v2f o;
				//输入物体空间顶点数据
		VertexPositionInputs positionInputs = GetVertexPositionInputs(v.positionOS.xyz);
                o.vertex = positionInputs.positionCS;
                o.uv = v.uv;        
		o.positionWS = positionInputs.positionWS;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);         
                return col;
            }
            ENDHLSL
        }
    }
}


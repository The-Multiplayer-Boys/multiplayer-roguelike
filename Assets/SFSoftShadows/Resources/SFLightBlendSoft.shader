Shader "Hidden/SFSoftShadows/LightBlendSoft" {
	SubShader {
		Pass {
			Blend DstAlpha OneMinusSrcColor, One Zero
			Cull Off
			Lighting Off
			ZTest Always
			ZWrite Off
			
			CGPROGRAM
				#pragma vertex VShader
				#pragma fragment FShader
				
				sampler2D _MainTex;
				
				struct VertexInput {
					float4 position : POSITION;
					float4 color : COLOR;
				};
				
				struct VertexOutput {
					float4 position : SV_POSITION;
					float4 texCoord : TEXCOORD0;
					float4 color : COLOR;
				};
				
				VertexOutput VShader(VertexInput v){
					float4 texCoord = mul(UNITY_MATRIX_P, v.position);
					VertexOutput o = {v.position, texCoord + texCoord.w, v.color};
					return o;
				}

				fixed4 FShader(VertexOutput v) : SV_Target {
					// Prevent back projection of lights in perspective mode.
					half mask = step(0.0, v.texCoord.w);

					half3 intensity = tex2Dproj(_MainTex, UNITY_PROJ_COORD(v.texCoord)).rgb;
					return half4(mask*intensity*v.color.rgb, 1.0);
				}
			ENDCG
		}
	}
}

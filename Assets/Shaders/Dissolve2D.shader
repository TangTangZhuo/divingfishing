// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:1,cusa:False,bamd:2,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:34219,y:32947,varname:node_1873,prsc:2|emission-5323-RGB,clip-4937-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32668,y:33038,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,tex:908cad6f4db1d4314aeeefccbe73d730,ntxv:2,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:3213,x:32695,y:33262,varname:node_3213,prsc:2|IN-8600-OUT;n:type:ShaderForge.SFN_Tex2d,id:9231,x:32668,y:32825,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_9231,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aaf89f1ea6d8c41e5bb3a755d6c644a9,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Add,id:4937,x:33113,y:33086,varname:node_4937,prsc:2|A-7319-OUT,B-6582-OUT;n:type:ShaderForge.SFN_RemapRange,id:6582,x:32908,y:33275,varname:node_6582,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3213-OUT;n:type:ShaderForge.SFN_Blend,id:7319,x:32883,y:32967,varname:node_7319,prsc:2,blmd:1,clmp:False|SRC-9231-RGB,DST-4805-A;n:type:ShaderForge.SFN_RemapRange,id:1948,x:33243,y:32789,varname:node_1948,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4;n:type:ShaderForge.SFN_Clamp01,id:223,x:33414,y:32789,varname:node_223,prsc:2|IN-1948-OUT;n:type:ShaderForge.SFN_OneMinus,id:209,x:33582,y:32781,varname:node_209,prsc:2|IN-223-OUT;n:type:ShaderForge.SFN_Append,id:6829,x:33814,y:32872,varname:node_6829,prsc:2|A-209-OUT,B-144-OUT;n:type:ShaderForge.SFN_ValueProperty,id:144,x:33606,y:32938,ptovrint:False,ptlb:node_144,ptin:_node_144,varname:node_144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:9639,x:33997,y:32906,varname:node_9639,prsc:2,tex:908cad6f4db1d4314aeeefccbe73d730,ntxv:0,isnm:False|UVIN-6829-OUT,TEX-9211-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9211,x:33814,y:33026,ptovrint:False,ptlb:node_9211,ptin:_node_9211,varname:node_9211,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:908cad6f4db1d4314aeeefccbe73d730,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:8600,x:32360,y:33274,ptovrint:False,ptlb:value,ptin:_value,varname:_value_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.251,cur:0.251,max:0.8;n:type:ShaderForge.SFN_Color,id:5323,x:33630,y:32637,ptovrint:False,ptlb:icolor,ptin:_icolor,varname:node_5323,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:4805-9231-144-9211-8600-5323;pass:END;sub:END;*/

Shader "Shader Forge/Dissolve2D" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "black" {}
        _noise ("noise", 2D) = "black" {}
        _node_144 ("node_144", Float ) = 0
        _node_9211 ("node_9211", 2D) = "white" {}
        _value ("value", Range(0.251, 0.8)) = 0.251
        _icolor ("icolor", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
            "DisableBatching"="LODFading"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _value;
            uniform float4 _icolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(i.uv0, _noise));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(((_noise_var.rgb*_MainTex_var.a)+((1.0 - _value)*2.0+-1.0)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _icolor.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _value;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(i.uv0, _noise));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(((_noise_var.rgb*_MainTex_var.a)+((1.0 - _value)*2.0+-1.0)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

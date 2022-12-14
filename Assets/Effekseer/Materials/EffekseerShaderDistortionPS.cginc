#include <HLSLSupport.cginc>
#include <UnityInstancing.cginc>
Texture2D _colorTex : register(t0);
SamplerState sampler_colorTex : register(s0);

UNITY_DECLARE_SCREENSPACE_TEXTURE(_backTex);

#ifndef DISABLED_SOFT_PARTICLE
Texture2D _depthTex : register(t2);
SamplerState sampler_depthTex : register(s2);
#endif

float4 g_scale;
float4 mUVInversedBack;

float4 flipbookParameter1; // x:enable, y:interpolationType

float4 fUVDistortionParameter; // x:intensity, y:blendIntensity, zw:uvInversed

float4 fBlendTextureParameter; // x:blendType

// which is used for only softparticle
float4 softParticleParam;
float4 reconstructionParam1;
float4 reconstructionParam2;

struct PS_Input
{
	float4 PosVS : SV_POSITION;
	linear centroid float2 UV : TEXCOORD0;
	float4 ProjBinormal : TEXCOORD1;
	float4 ProjTangent : TEXCOORD2;
	float4 PosP : TEXCOORD3;
	linear centroid float4 Color : COLOR0;
	UNITY_VERTEX_OUTPUT_STEREO 
};

#include "EffekseerShaderSoftParticlePS.cginc"

float4 frag(const PS_Input Input)
	: SV_Target
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(Input);
	
	float4 Output = _colorTex.Sample(sampler_colorTex, Input.UV);
	Output.a = Output.a * Input.Color.a;

	float2 pos = Input.PosP.xy / Input.PosP.w;
	float2 posR = Input.ProjTangent.xy / Input.ProjTangent.w;
	float2 posU = Input.ProjBinormal.xy / Input.ProjBinormal.w;

	float xscale = (Output.x * 2.0 - 1.0) * Input.Color.x * g_scale.x;
	float yscale = (Output.y * 2.0 - 1.0) * Input.Color.y * g_scale.x;

	float2 uv = pos + (posR - pos) * xscale + (posU - pos) * yscale;

	uv.x = (uv.x + 1.0) * 0.5;
	uv.y = 1.0 - (uv.y + 1.0) * 0.5;

	uv.y = mUVInversedBack.x + mUVInversedBack.y * uv.y;

#ifdef __OPENGL__
	uv.y = 1.0 - uv.y;
#endif

	float3 color = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_backTex, uv);
	Output.xyz = color;

#ifndef DISABLED_SOFT_PARTICLE
	// softparticle
	float4 screenPos = Input.PosP / Input.PosP.w;
	float2 screenUV = (screenPos.xy + 1.0f) / 2.0f;
	screenUV.y = 1.0f - screenUV.y;

#ifdef __OPENGL__
	screenUV.y = 1.0 - screenUV.y;
#endif

	if (softParticleParam.w != 0.0f)
	{
		float backgroundZ = _depthTex.Sample(sampler_depthTex, screenUV).x;
		Output.a *= SoftParticle(
			backgroundZ,
			screenPos.z,
			softParticleParam,
			reconstructionParam1,
			reconstructionParam2);
	}
#endif


	if (Output.a == 0.0f)
		discard;

	return Output;
}

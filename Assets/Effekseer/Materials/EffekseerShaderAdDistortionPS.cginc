#include <HLSLSupport.cginc>
#include <UnityInstancing.cginc>

Texture2D _colorTex : register(t0);
SamplerState sampler_colorTex : register(s0);

UNITY_DECLARE_SCREENSPACE_TEXTURE(_backTex);

Texture2D _alphaTex : register(t2);
SamplerState sampler_alphaTex : register(s2);

Texture2D _uvDistortionTex : register(t3);
SamplerState sampler_uvDistortionTex : register(s3);

Texture2D _blendTex : register(t4);
SamplerState sampler_blendTex : register(s4);

Texture2D _blendAlphaTex : register(t5);
SamplerState sampler_blendAlphaTex : register(s5);

Texture2D _blendUVDistortionTex : register(t6);
SamplerState sampler_blendUVDistortionTex : register(s6);

#ifndef DISABLED_SOFT_PARTICLE
Texture2D _depthTex : register(t7);
SamplerState sampler_depthTex : register(s7);
#endif

float4 g_scale;
float4 mUVInversedBack;

// Defined in VS
// float4 fFlipbookParameter; // x:enable, y:interpolationType

float4 fUVDistortionParameter; // x:intensity, y:blendIntensity, zw:uvInversed

float4 fBlendTextureParameter; // x:blendType

// which is used for only softparticle
float4 softParticleParam;
float4 reconstructionParam1;
float4 reconstructionParam2;

struct PS_Input
{
	float4 PosVS : SV_POSITION;
	// xy uv z - FlipbookRate, w - AlphaThreshold
	linear centroid float4 UV_Others : TEXCOORD0;
	float4 ProjBinormal : TEXCOORD1;
	float4 ProjTangent : TEXCOORD2;
	float4 PosP : TEXCOORD3;
	linear centroid float4 Color : COLOR0;

	float4 Alpha_Dist_UV : TEXCOORD4;
	float4 Blend_Alpha_Dist_UV : TEXCOORD5;

	// BlendUV, FlipbookNextIndexUV
	float4 Blend_FBNextIndex_UV : TEXCOORD6;

	UNITY_VERTEX_OUTPUT_STEREO
};

#include "EffekseerShaderAdCommonPS.cginc"
#include "EffekseerShaderSoftParticlePS.cginc"

float4 frag(const PS_Input Input)
	: SV_Target
{
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(Input);

	AdvancedParameter advancedParam = DisolveAdvancedParameter(Input);

	float2 UVOffset = UVDistortionOffset(_uvDistortionTex, sampler_uvDistortionTex, advancedParam.UVDistortionUV, fUVDistortionParameter.zw, false);
	UVOffset *= fUVDistortionParameter.x;

	float4 Output = _colorTex.Sample(sampler_colorTex, Input.UV_Others + UVOffset);

	Output.a = Output.a * Input.Color.a;

	ApplyFlipbook(Output, _colorTex, sampler_colorTex, flipbookParameter1, Input.Color, advancedParam.FlipbookNextIndexUV + UVOffset, advancedParam.FlipbookRate, false);

	// apply alpha texture
	float4 AlphaTexColor = _alphaTex.Sample(sampler_alphaTex, advancedParam.AlphaUV + UVOffset);
	Output.a *= AlphaTexColor.r * AlphaTexColor.a;

	// blend texture uv offset
	float2 BlendUVOffset = UVDistortionOffset(_blendUVDistortionTex, sampler_blendUVDistortionTex, advancedParam.BlendUVDistortionUV, fUVDistortionParameter.zw, false);
	BlendUVOffset *= fUVDistortionParameter.y;

	float4 BlendTextureColor = _blendTex.Sample(sampler_blendTex, advancedParam.BlendUV + BlendUVOffset);
	float4 BlendAlphaTextureColor = _blendAlphaTex.Sample(sampler_blendAlphaTex, advancedParam.BlendAlphaUV + BlendUVOffset);
	BlendTextureColor.a *= BlendAlphaTextureColor.r * BlendAlphaTextureColor.a;

	ApplyTextureBlending(Output, BlendTextureColor, fBlendTextureParameter.x);

	// zero + alpha threshold
	if (Output.a <= max(0.0, advancedParam.AlphaThreshold))
	{
		discard;
	}

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


	return Output;
}

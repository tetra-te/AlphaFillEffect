Texture2D InputTexture : register(t0);
SamplerState InputSampler : register(s0);

cbuffer constants : register(b0)
{
	float threshold : packoffset(c0.x);
    float red : packoffset(c0.y);
    float green : packoffset(c0.z);
    float blue : packoffset(c0.w);
    float invert : packoffset(c1.x);
    float keepColor : packoffset(c1.y);
}; 

float4 main(
	float4 pos : SV_POSITION,
	float4 posScene : SCENE_POSITION,
	float4 uv0 : TEXCOORD0
) : SV_Target
{
	float4 color = InputTexture.Sample(InputSampler, uv0.xy);
	
    if (keepColor == 0)
    {
        if (invert == 0)
        {
            if (color.a >= threshold)
            {
                color = float4(red, green, blue, 1);
            }
        }
        else
        {
            if (color.a < threshold)
            {
                color = float4(red, green, blue, 1);
            }
        }
    }
    else
    {
        if (invert == 0)
        {
            if (color.a >= threshold)
            {
                color.rgb /= color.a;
            }
        }
        else
        {
            if (color.a < threshold)
            {
                color.rgb /= color.a;
            }
        }
    }

    return color;
}
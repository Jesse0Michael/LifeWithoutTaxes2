float4x4 World;
float4x4 View;
float4x4 Projection;

// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 UV : TEXCOORD0;
    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 UV : TEXCOORD0;
    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    output.UV = input.UV;

    return output;
}

texture ColorMap;
sampler ColorMapSampler = sampler_state
{
   Texture = <ColorMap>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;
   AddressU = Wrap;
   AddressV = Wrap;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    return tex2D(ColorMapSampler,input.UV.xy);
    //return float4(0, input.UV.x, input.UV.y, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.
        Sampler[0] = (ColorMapSampler);
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}



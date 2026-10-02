using UnityEngine;

namespace Render.Runtime;

public static class ShaderConstant
{
	public static readonly int _BaseMap = Shader.PropertyToID("_BaseMap");

	public static readonly int _BaseColor = Shader.PropertyToID("_BaseColor");

	public static readonly int _EmissionMap = Shader.PropertyToID("_EmissionMap");

	public static readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");

	public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

	public static readonly int _StencilReadMask = Shader.PropertyToID("_StencilReadMask");

	public static readonly int _StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");

	public static readonly int _StencilComp = Shader.PropertyToID("_StencilComp");

	public const string _KEY_WORD_EMISSION = "_EMISSION";

	public static readonly int _Main_Tex = Shader.PropertyToID("_Main_Tex");

	public static readonly int _MainTex = Shader.PropertyToID("_MainTex");

	public static readonly int _BakeUV_ST = Shader.PropertyToID("_BakeUV_ST");

	public static readonly int _BaseMap_ST = Shader.PropertyToID("_BaseMap_ST");

	public static readonly int _Emission = Shader.PropertyToID("_Emission");

	public static readonly int _SrcBlend = Shader.PropertyToID("_SrcBlend");

	public static readonly int _DstBlend = Shader.PropertyToID("_DstBlend");

	public static readonly int _ZWrite = Shader.PropertyToID("_ZWrite");

	public static readonly int _Cull = Shader.PropertyToID("_Cull");

	public static readonly int _Culling = Shader.PropertyToID("_Culling");

	public static readonly int _BleModSour = Shader.PropertyToID("_BleModSour");

	public static readonly int _BleModDest = Shader.PropertyToID("_BleModDest");

	public static readonly int _RefVal = Shader.PropertyToID("_RefVal");

	public static readonly int _Compa = Shader.PropertyToID("_Compa");

	public static readonly int _Oper = Shader.PropertyToID("_Oper");

	public static readonly int _AlphaClip = Shader.PropertyToID("_AlphaClip");

	public static readonly int _Cutoff = Shader.PropertyToID("_Cutoff");

	public static readonly int _UseExraUV = Shader.PropertyToID("_UseExraUV");

	public static readonly int _VertexMin = Shader.PropertyToID("_VertexMin");

	public static readonly int _VertexSize = Shader.PropertyToID("_VertexSize");

	public static readonly int _UVMin = Shader.PropertyToID("_UVMin");

	public static readonly int _UVSize = Shader.PropertyToID("_UVSize");

	public static readonly int _ReColorCount = Shader.PropertyToID("_ReColorCount");

	public static readonly int _LabelColors = Shader.PropertyToID("_LabelColors");

	public static readonly int _ChangeColors = Shader.PropertyToID("_ChangeColors");

	public static readonly int _BlurRaidus = Shader.PropertyToID("_BlurRaidus");

	public static readonly int _WindDir = Shader.PropertyToID("_WindDir");

	public static readonly int _WindNoiseStrength = Shader.PropertyToID("_WindNoiseStrength");

	public static readonly int _WindNoiseTile = Shader.PropertyToID("_WindNoiseTile");

	public static readonly int _WindSpeed = Shader.PropertyToID("_WindSpeed");

	public static readonly int _Wind = Shader.PropertyToID("_Wind");

	public static readonly int _PlantHeight = Shader.PropertyToID("_PlantHeight");

	public static readonly int _Bend = Shader.PropertyToID("_Bend");

	public static readonly int _Floating = Shader.PropertyToID("_Floating");

	public static readonly int _FloatintHeight = Shader.PropertyToID("_FloatintHeight");

	public static readonly int _Laser = Shader.PropertyToID("_Laser");

	public static readonly int _LaserRampMap = Shader.PropertyToID("_LaserRampMap");

	public static readonly int _LaserBlend = Shader.PropertyToID("_LaserBlend");

	public static readonly int _CircleWindBuffer = Shader.PropertyToID("_CircleWindBuffer");

	public static readonly int _CircleWindCount = Shader.PropertyToID("_CircleWindCount");

	public static readonly int _ObjectPosition = Shader.PropertyToID("_ObjectPosition");

	public static readonly int _Color = Shader.PropertyToID("_Color");

	public static readonly int _UVRect = Shader.PropertyToID("_UVRect");

	public static readonly int _PlanePosY = Shader.PropertyToID("_PlanePosY");

	public static readonly int _PlanarReflectionStrength = Shader.PropertyToID("_PlanarReflectionStrength");

	public static int _TextureY = Shader.PropertyToID("_TextureY");

	public static int _TextureU = Shader.PropertyToID("_TextureU");

	public static int _TextureV = Shader.PropertyToID("_TextureV");

	public static int _MovieTexture_ST = Shader.PropertyToID("_MovieTexture_ST");

	public static int _MovieChromaTexture_ST = Shader.PropertyToID("_MovieChromaTexture_ST");

	public static int _TextureA = Shader.PropertyToID("_TextureA");

	public static int _MovieAlphaTexture_ST = Shader.PropertyToID("_MovieAlphaTexture_ST");

	public const string _EMISSION = "_EMISSION";

	public const string _ALPHATEST_ON = "_ALPHATEST_ON";

	public const string _USE_EXTRA_UV = "_USE_EXTRA_UV";

	public const string _WIND = "_WIND";

	public const string _FLOATING = "_FLOATING";

	public const string _LASER = "_LASER";
}

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace Render.Runtime;

public class SceneBakeObject
{
	private Matrix4x4 localToWorldMatrix;

	private Material[] materials;

	private Texture2D[] textures;

	private Vector4[] textureSTArray;

	private Vector4[] textureInvSTArray;

	public bool[] vaildBakes;

	private int bakePassIndex;

	private Mesh mesh;

	private RenderTexture[][] bakeRenderTextures;

	private string[] bakeTexPaths;

	private int posId;

	private float bakeTexPPM;

	private Shader showBakeTexShader;

	private Renderer renderer;

	private MeshFilter meshFilter;

	private Material edgeExpandMaterial;

	private bool isIngnoreScale = true;

	private int minSize = 1;

	private int maxSize = 1024;

	private int realSubMeshCount;

	private bool isBakeToOtherUV;

	private const string toonShader = "Universal Render Pipeline/RealToon/Version 5/Default/Default";

	public Mesh Mesh => mesh;

	private static int ConvertPositionToIndex(Vector3 v3)
	{
		return Mathf.RoundToInt(v3.x * 179f + v3.y * 311f + v3.z * 219f);
	}

	private static Texture2D GetMainTexFromMaterail(Material mat)
	{
		Texture2D texture2D = null;
		if (!texture2D && mat.HasProperty(ShaderConstant._BaseMap))
		{
			texture2D = mat.GetTexture(ShaderConstant._BaseMap) as Texture2D;
		}
		if (!texture2D && mat.HasProperty(ShaderConstant._MainTex))
		{
			texture2D = mat.GetTexture(ShaderConstant._MainTex) as Texture2D;
		}
		return texture2D;
	}

	private static bool IsMatchAnyShader(Material mat, Shader[] shaders)
	{
		bool flag = false;
		for (int i = 0; i < shaders.Length; i++)
		{
			flag |= mat.shader == shaders[i];
		}
		return flag;
	}

	public static bool TryCreateSceneBakeObject(GameObject go, Material[] originMats, int bakePassIndex, Shader[] targetShaders, Shader unlitShader, Material _edgeExapndMat, float ppm, bool ingnoreScale, int minSize, int maxSize, HashSet<Mesh> excludeMeshs, HashSet<Material> excludeMaterils, out SceneBakeObject sceneBakeObj, out SceneBakeObject previewObj)
	{
		Transform transform = go.transform;
		sceneBakeObj = null;
		previewObj = null;
		MeshFilter component = transform.GetComponent<MeshFilter>();
		Renderer component2 = transform.GetComponent<Renderer>();
		bool flag = ((bool)component && (bool)component2) || component2 is SkinnedMeshRenderer;
		bool[] array = null;
		Mesh mesh = null;
		int num = 0;
		bool flag2 = false;
		if (flag)
		{
			mesh = ((!(component2 is SkinnedMeshRenderer skinnedMeshRenderer)) ? component.sharedMesh : skinnedMeshRenderer.sharedMesh);
			num = Mathf.Min(mesh.subMeshCount, originMats.Length);
			array = new bool[num];
			flag = false;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = originMats[i] != null && IsMatchAnyShader(originMats[i], targetShaders) && HasVaildUV(mesh, i, flag2) && !excludeMaterils.Contains(originMats[i]);
				flag = flag || flag3;
				array[i] = flag3;
			}
			flag &= !excludeMeshs.Contains(mesh);
		}
		if (flag)
		{
			sceneBakeObj = new SceneBakeObject();
			sceneBakeObj.localToWorldMatrix = transform.transform.localToWorldMatrix;
			sceneBakeObj.materials = originMats;
			sceneBakeObj.vaildBakes = array;
			sceneBakeObj.realSubMeshCount = num;
			sceneBakeObj.isBakeToOtherUV = flag2;
			sceneBakeObj.meshFilter = component;
			previewObj = new SceneBakeObject();
			previewObj.localToWorldMatrix = transform.transform.localToWorldMatrix;
			previewObj.materials = originMats;
			previewObj.vaildBakes = array;
			previewObj.realSubMeshCount = num;
			previewObj.isBakeToOtherUV = flag2;
			previewObj.meshFilter = component;
			Texture2D[] array2 = new Texture2D[num];
			for (int j = 0; j < num; j++)
			{
				if (j >= array2.Length || j >= originMats.Length)
				{
					Debug.Log(component2.gameObject.name);
				}
				array2[j] = GetMainTexFromMaterail(originMats[j]);
			}
			sceneBakeObj.textures = array2;
			sceneBakeObj.mesh = mesh;
			sceneBakeObj.bakePassIndex = bakePassIndex;
			sceneBakeObj.posId = ConvertPositionToIndex(transform.position);
			sceneBakeObj.bakeTexPPM = ppm;
			sceneBakeObj.showBakeTexShader = unlitShader;
			sceneBakeObj.renderer = component2;
			sceneBakeObj.edgeExpandMaterial = _edgeExapndMat;
			sceneBakeObj.isIngnoreScale = ingnoreScale;
			sceneBakeObj.minSize = minSize;
			sceneBakeObj.maxSize = maxSize;
			sceneBakeObj.bakeRenderTextures = new RenderTexture[num][];
			sceneBakeObj.bakeTexPaths = new string[num];
			previewObj.textures = array2;
			previewObj.mesh = mesh;
			previewObj.bakePassIndex = bakePassIndex;
			previewObj.posId = ConvertPositionToIndex(transform.position);
			previewObj.bakeTexPPM = ppm;
			previewObj.showBakeTexShader = unlitShader;
			previewObj.renderer = component2;
			previewObj.edgeExpandMaterial = _edgeExapndMat;
			previewObj.isIngnoreScale = ingnoreScale;
			previewObj.minSize = minSize;
			previewObj.maxSize = maxSize;
			previewObj.bakeRenderTextures = new RenderTexture[num][];
			previewObj.bakeTexPaths = new string[num];
			Vector4[] array3 = new Vector4[num];
			Vector4[] array4 = new Vector4[num];
			Vector2[] array5 = (flag2 ? mesh.uv2 : mesh.uv);
			for (int k = 0; k < num; k++)
			{
				Vector2 lhs = Vector2.one * float.MinValue;
				Vector2 lhs2 = Vector2.one * float.MaxValue;
				int[] triangles = mesh.GetTriangles(k, applyBaseVertex: true);
				for (int l = 0; l < triangles.Length; l++)
				{
					int num2 = triangles[l];
					if (num2 >= array5.Length)
					{
						Debug.Log($"index:{num2}  uv.lenght:{array5.Length} {mesh.name}");
					}
					Vector2 rhs = array5[triangles[l]];
					lhs = Vector2.Max(lhs, rhs);
					lhs2 = Vector2.Min(lhs2, rhs);
				}
				float num3 = 0.98f / (lhs.x - lhs2.x);
				float num4 = 0.98f / (lhs.y - lhs2.y);
				float z = 0.01f - lhs2.x * num3;
				float w = 0.01f - lhs2.y * num4;
				array3[k] = new Vector4(num3, num4, z, w);
				float x = lhs.x - lhs2.x;
				float y = lhs.y - lhs2.y;
				float x2 = lhs2.x;
				float y2 = lhs2.y;
				array4[k] = new Vector4(x, y, x2, y2);
			}
			sceneBakeObj.textureSTArray = array3;
			sceneBakeObj.textureInvSTArray = array4;
			previewObj.textureSTArray = array3;
			previewObj.textureInvSTArray = array4;
		}
		return flag;
	}

	public bool HasAnyMatEnableLaser()
	{
		bool result = false;
		for (int i = 0; i < materials.Length; i++)
		{
			Material material = materials[i];
			if (material != null && material.IsKeywordEnabled("_LASER"))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void DebugTextureInfo()
	{
		int num = realSubMeshCount;
		for (int i = 0; i < num; i++)
		{
			DebugBakeTexInfo(mesh, i, bakeTexPPM, textureInvSTArray[i]);
		}
	}

	private static float GetTriangleAreaV2(Vector2 p0, Vector2 p1, Vector2 p2)
	{
		float num = Vector2.Distance(p0, p1);
		float num2 = Vector2.Distance(p1, p2);
		float num3 = Vector2.Distance(p2, p0);
		float num4 = (num + num2 + num3) * 0.5f;
		return Mathf.Sqrt(num4 * (num4 - num) * (num4 - num2) * (num4 - num3));
	}

	private static float GetTriangleAreaV3(Vector3 p0, Vector3 p1, Vector3 p2)
	{
		float num = Vector3.Distance(p0, p1);
		float num2 = Vector3.Distance(p1, p2);
		float num3 = Vector3.Distance(p2, p0);
		float num4 = (num + num2 + num3) * 0.5f;
		return Mathf.Sqrt(num4 * (num4 - num) * (num4 - num2) * (num4 - num3));
	}

	private void DebugBakeTexInfo(Mesh mesh, int subMeshIndex, float pixelPerMeter, Vector4 st)
	{
		int[] triangles = mesh.GetTriangles(subMeshIndex);
		Vector2[] array = (isBakeToOtherUV ? mesh.uv2 : mesh.uv);
		Vector3[] vertices = mesh.vertices;
		if (!isIngnoreScale)
		{
			Vector3 lossyScale = localToWorldMatrix.lossyScale;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector = vertices[i];
				for (int j = 0; j < 3; j++)
				{
					vector[j] *= lossyScale[j];
				}
				vertices[i] = vector;
			}
		}
		int num = triangles.Length / 3;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 1000f;
		for (int k = 0; k < num; k++)
		{
			Vector2 p = array[triangles[k * 3]] * num4;
			Vector2 p2 = array[triangles[k * 3 + 1]] * num4;
			Vector2 p3 = array[triangles[k * 3 + 2]] * num4;
			Vector3 p4 = vertices[triangles[k * 3]];
			Vector3 p5 = vertices[triangles[k * 3 + 1]];
			Vector3 p6 = vertices[triangles[k * 3 + 2]];
			num2 += GetTriangleAreaV2(p, p2, p3);
			num3 += GetTriangleAreaV3(p4, p5, p6);
		}
		int num5 = 1;
		int num6 = 1;
		float num7 = 0f;
		if (num2 > 0f && num3 > 0f)
		{
			float num8 = 1f * st.x / (1f * st.y);
			if (num8 > 0.8f && num8 > 1.25f)
			{
				num8 = 1f;
			}
			float num9 = 1f * st.x * (1f * st.y) * num4 * num4;
			num7 = num2 / num9;
			float num10 = num3 * pixelPerMeter * pixelPerMeter / num7;
			num5 = Mathf.RoundToInt(Mathf.Sqrt(num10 * num8));
			num6 = Mathf.RoundToInt(Mathf.Sqrt(num10 / num8));
		}
		Debug.Log($"mesh:{mesh.name} subIndex:{subMeshIndex} p_uv:{num7} sum_vert:{num3} ppm:{pixelPerMeter} size:{num5} x {num6}");
	}

	private static bool HasVaildUV(Mesh mesh, int subMeshIndex, bool isUV4)
	{
		int[] triangles = mesh.GetTriangles(subMeshIndex);
		Vector2[] array = (isUV4 ? mesh.uv2 : mesh.uv);
		int num = triangles.Length / 3;
		float num2 = 0f;
		float num3 = 1000f;
		if (array.Length != 0)
		{
			for (int i = 0; i < num; i++)
			{
				Vector2 p = array[triangles[i * 3]] * num3;
				Vector2 p2 = array[triangles[i * 3 + 1]] * num3;
				Vector2 p3 = array[triangles[i * 3 + 2]] * num3;
				num2 += GetTriangleAreaV2(p, p2, p3);
			}
		}
		return num2 > 0f;
	}

	private bool TryGetTargetRenderTexSize(Mesh mesh, int subMeshIndex, float pixelPerMeter, Vector4 st, out Vector2Int size, int _minSize = 1, int _maxSize = 1024)
	{
		int[] triangles = mesh.GetTriangles(subMeshIndex);
		Vector2[] array = (isBakeToOtherUV ? mesh.uv2 : mesh.uv);
		Vector3[] vertices = mesh.vertices;
		if (!isIngnoreScale)
		{
			Vector3 lossyScale = localToWorldMatrix.lossyScale;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector = vertices[i];
				for (int j = 0; j < 3; j++)
				{
					vector[j] *= lossyScale[j];
				}
				vertices[i] = vector;
			}
		}
		int num = triangles.Length / 3;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 1000f;
		for (int k = 0; k < num; k++)
		{
			Vector2 p = array[triangles[k * 3]] * num4;
			Vector2 p2 = array[triangles[k * 3 + 1]] * num4;
			Vector2 p3 = array[triangles[k * 3 + 2]] * num4;
			Vector3 p4 = vertices[triangles[k * 3]];
			Vector3 p5 = vertices[triangles[k * 3 + 1]];
			Vector3 p6 = vertices[triangles[k * 3 + 2]];
			num2 += GetTriangleAreaV2(p, p2, p3);
			num3 += GetTriangleAreaV3(p4, p5, p6);
		}
		int x = 1;
		int y = 1;
		int num5;
		if (num2 > 0f)
		{
			num5 = ((num3 > 0f) ? 1 : 0);
			if (num5 != 0)
			{
				float num6 = 1f * st.x / (1f * st.y);
				if (num6 > 0.8f && num6 > 1.25f)
				{
					num6 = 1f;
				}
				float num7 = 1f * st.x * (1f * st.y) * num4 * num4;
				float num8 = Mathf.Clamp01(num2 / num7);
				num8 = num8 * 0.5f + 0.5f;
				float num9 = num3 * pixelPerMeter * pixelPerMeter / num8;
				x = Mathf.Clamp(Mathf.RoundToInt(Mathf.Sqrt(num9 * num6)), _minSize, _maxSize);
				y = Mathf.Clamp(Mathf.RoundToInt(Mathf.Sqrt(num9 / num6)), _minSize, _maxSize);
			}
		}
		else
		{
			num5 = 0;
		}
		size = new Vector2Int(x, y);
		return (byte)num5 != 0;
	}

	public bool TryGetTargetRenderTexture(Mesh mesh, int subIndex, float ppm, Vector4 st, out RenderTexture[] rts)
	{
		rts = new RenderTexture[4];
		Vector2Int size;
		bool num = TryGetTargetRenderTexSize(mesh, subIndex, ppm, st, out size, minSize, maxSize);
		if (num)
		{
			int x = size.x;
			int y = size.y;
			RenderTextureDescriptor desc = new RenderTextureDescriptor(x, y, RenderTextureFormat.ARGB32)
			{
				sRGB = true
			};
			RenderTextureDescriptor desc2 = new RenderTextureDescriptor(x, y, RenderTextureFormat.Depth, 32);
			rts[0] = RenderTexture.GetTemporary(desc);
			rts[0].filterMode = FilterMode.Point;
			rts[1] = RenderTexture.GetTemporary(desc);
			rts[1].filterMode = FilterMode.Point;
			rts[2] = RenderTexture.GetTemporary(desc2);
			rts[2].filterMode = FilterMode.Point;
		}
		return num;
	}

	public void CreateMat(string oldRoot, string newRoot, string sceneName, int subIndex)
	{
	}

	public void CreateMesh(string oldRoot, string newRoot, bool isKeepNormal = false)
	{
	}

	public static string ReplaceExpandNameAddSuffix(string path, string exp, string suffix, string oldRoot, string newRoot)
	{
		int length = path.LastIndexOf('.');
		string text = path.Substring(0, length) + suffix + "." + exp;
		if (text.Contains(oldRoot))
		{
			text = text.Replace(oldRoot, newRoot);
		}
		else if (!string.IsNullOrEmpty(newRoot))
		{
			text = newRoot + "Base/" + Path.GetFileName(text);
		}
		string directoryName = Path.GetDirectoryName(text);
		if (!string.IsNullOrEmpty(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		return text;
	}

	public void ExportRT(string oldRoot, string newRoot, string sceneName, int subIndex)
	{
	}

	public void Bake(ScriptableRenderContext context, CommandBuffer cmd, CullingResults cullingResults, ref DrawingSettings drawingSettings)
	{
		int num = realSubMeshCount;
		Material[] sharedMaterials = renderer.sharedMaterials;
		for (int i = 0; i < num; i++)
		{
			Material material = sharedMaterials[i];
			if (vaildBakes[i])
			{
				FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange(material.renderQueue, material.renderQueue));
				if (!TryGetTargetRenderTexture(mesh, i, bakeTexPPM, textureInvSTArray[i], out var rts))
				{
					vaildBakes[i] = false;
					continue;
				}
				bakeRenderTextures[i] = rts;
				cmd.SetRenderTarget(rts[0], rts[2]);
				cmd.ClearRenderTarget(clearDepth: true, clearColor: true, new Color(0f, 0f, 0f, 0f), 1f);
				cmd.SetGlobalFloat(ShaderConstant._UseExraUV, isBakeToOtherUV ? 1 : 0);
				cmd.SetGlobalVector(ShaderConstant._BakeUV_ST, textureSTArray[i]);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
			}
		}
	}
}

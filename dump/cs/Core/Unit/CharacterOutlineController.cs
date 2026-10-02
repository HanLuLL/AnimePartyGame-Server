using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Render.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Unit;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterOutlineController : MonoBehaviour
{
	[SerializeField]
	private Material blurMaterial;

	[SerializeField]
	private Material stepMaterail;

	[SerializeField]
	private Material drawMaterial;

	[SerializeField]
	private Color outlineColor = Color.black;

	[SerializeField]
	[Range(1f, 128f)]
	private int ppm = 48;

	[SerializeField]
	[Range(1f, 8f)]
	private int iterateCount = 3;

	[SerializeField]
	private float blurRadius = 2f;

	[SerializeField]
	private bool isOutline = true;

	private List<RenderTexture> rtList = new List<RenderTexture>();

	private SpriteRenderer _sprieteRenderer;

	private MeshRenderer childRenderer;

	private Mesh outlineMesh;

	private MaterialPropertyBlock mpb;

	private SpriteRenderer spriteRenderer
	{
		get
		{
			if (_sprieteRenderer == null)
			{
				_sprieteRenderer = GetComponent<SpriteRenderer>();
			}
			return _sprieteRenderer;
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		ClearAllAsset();
	}

	private void ClearAllAsset()
	{
		ReleaseRT();
		ReleaseMesh();
		ReleaseOutlineGameObject();
	}

	private void Update()
	{
		if (isOutline && stepMaterail != null && blurMaterial != null && drawMaterial != null)
		{
			RenderOutline();
		}
		else
		{
			ClearAllAsset();
		}
	}

	private void ReleaseRT()
	{
		if (rtList.Count <= 0)
		{
			return;
		}
		foreach (RenderTexture rt in rtList)
		{
			RenderTexture.ReleaseTemporary(rt);
		}
		rtList.Clear();
	}

	private void ReleaseMesh()
	{
		if (outlineMesh != null)
		{
			Object.DestroyImmediate(outlineMesh);
		}
	}

	private void ReleaseOutlineGameObject()
	{
		if (childRenderer != null)
		{
			Object.DestroyImmediate(childRenderer.gameObject);
			childRenderer = null;
		}
	}

	private void SetOutlineGameObject(float x, float y, Vector3 pos, Quaternion rotation, Color color, Texture mainTex)
	{
		mpb = mpb ?? new MaterialPropertyBlock();
		if (outlineMesh == null)
		{
			outlineMesh = new Mesh();
			outlineMesh.vertices = new Vector3[4];
			outlineMesh.uv = new Vector2[4]
			{
				new Vector2(0f, 0f),
				new Vector2(1f, 0f),
				new Vector2(1f, 1f),
				new Vector2(0f, 1f)
			};
			outlineMesh.triangles = new int[6] { 0, 2, 1, 0, 3, 2 };
		}
		outlineMesh.vertices = new Vector3[4]
		{
			new Vector3((0f - x) / 2f, (0f - y) / 2f, 0f),
			new Vector3(x / 2f, (0f - y) / 2f, 0f),
			new Vector3(x / 2f, y / 2f, 0f),
			new Vector3((0f - x) / 2f, y / 2f, 0f)
		};
		if (childRenderer == null)
		{
			GameObject gameObject = new GameObject("Outline");
			gameObject.layer = base.gameObject.layer;
			gameObject.AddComponent<MeshFilter>().sharedMesh = outlineMesh;
			childRenderer = gameObject.AddComponent<MeshRenderer>();
			childRenderer.sharedMaterial = drawMaterial;
		}
		mpb.SetTexture(ShaderConstant._MainTex, mainTex);
		mpb.SetColor(ShaderConstant._Color, color);
		childRenderer.SetPropertyBlock(mpb);
		childRenderer.transform.position = pos;
		childRenderer.transform.rotation = rotation;
		childRenderer.sortingOrder = spriteRenderer.sortingOrder;
	}

	public async UniTask SetOutline(bool isActive, Color color)
	{
		if (blurMaterial == null)
		{
			blurMaterial = await SimpleSingletonProvider<MaterialManager>.inst.GetMaterial("Outline_Blur");
		}
		if (stepMaterail == null)
		{
			stepMaterail = await SimpleSingletonProvider<MaterialManager>.inst.GetMaterial("Outline_Step");
		}
		if (drawMaterial == null)
		{
			drawMaterial = await SimpleSingletonProvider<MaterialManager>.inst.GetMaterial("Outline_Draw");
		}
		isOutline = isActive;
		outlineColor = color;
	}

	private void RenderOutline()
	{
		CommandBuffer commandBuffer = CommandBufferPool.Get("Outline");
		Mesh fullscreenMesh = RenderingUtils.fullscreenMesh;
		Bounds bounds = spriteRenderer.bounds;
		Vector3 center = bounds.center;
		float num = bounds.size.x * 1.2f;
		float num2 = bounds.size.y * 1.2f;
		Vector3 forward = base.transform.forward;
		Vector3 up = base.transform.up;
		Matrix4x4 inverse = Matrix4x4.TRS(center + forward * 5f, Quaternion.LookRotation(forward, up), Vector3.one).inverse;
		Matrix4x4 proj = Matrix4x4.Ortho((0f - num) / 2f, num / 2f, (0f - num2) / 2f, num2 / 2f, 0f, 10f);
		proj = GL.GetGPUProjectionMatrix(proj, renderIntoTexture: false);
		int num3 = Mathf.Clamp(Mathf.RoundToInt(num * (float)ppm), 1, 4096);
		int num4 = Mathf.Clamp(Mathf.RoundToInt(num2 * (float)ppm), 1, 4096);
		if (rtList.Count == 0 || rtList[0].width != num3 || rtList[0].height != num4 || rtList.Count != iterateCount + 1)
		{
			ReleaseRT();
			int num5 = num3;
			int num6 = num4;
			RenderTextureDescriptor desc = new RenderTextureDescriptor(num5, num6, RenderTextureFormat.RHalf);
			for (int i = 0; i < iterateCount + 1; i++)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(desc);
				rtList.Add(temporary);
				num5 = Mathf.Max(1, num5 / 2);
				num6 = Mathf.Max(1, num6 / 2);
				desc.width = num5;
				desc.height = num6;
			}
		}
		SetOutlineGameObject(num, num2, center, base.transform.rotation, outlineColor, rtList[0]);
		commandBuffer.SetViewProjectionMatrices(inverse, proj);
		commandBuffer.SetRenderTarget(rtList[0]);
		commandBuffer.ClearRenderTarget(clearDepth: false, clearColor: true, Color.black);
		commandBuffer.DrawRenderer(spriteRenderer, stepMaterail, 0, 0);
		commandBuffer.SetGlobalFloat(ShaderConstant._BlurRaidus, blurRadius);
		for (int j = 0; j < iterateCount; j++)
		{
			commandBuffer.SetRenderTarget(rtList[j + 1]);
			commandBuffer.SetGlobalTexture(ShaderConstant._MainTex, rtList[j]);
			commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, blurMaterial, 0, 1);
		}
		for (int num7 = iterateCount - 1; num7 >= 0; num7--)
		{
			commandBuffer.SetRenderTarget(rtList[num7]);
			commandBuffer.SetGlobalTexture(ShaderConstant._MainTex, rtList[num7 + 1]);
			commandBuffer.DrawMesh(fullscreenMesh, Matrix4x4.identity, blurMaterial, 0, 2);
		}
		Graphics.ExecuteCommandBuffer(commandBuffer);
		CommandBufferPool.Release(commandBuffer);
	}
}

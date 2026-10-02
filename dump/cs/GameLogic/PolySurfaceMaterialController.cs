using System;
using System.Collections.Generic;
using Render.Runtime;
using UnityEngine;

namespace GameLogic;

[Serializable]
public class PolySurfaceMaterialController
{
	public GameObject OriginSurface;

	private GameObject copySurface;

	private MeshRenderer originMeshRenderer;

	private MeshRenderer copyMeshRenderer;

	private List<Material> instanceMats = new List<Material>();

	private Material originMat;

	public PolySurfaceMaterialController(GameObject go)
	{
		OriginSurface = go;
	}

	public void OnStartSubMapSwitch(Material srcFromMat, Material srcToMat)
	{
		if (copySurface == null)
		{
			copySurface = UnityEngine.Object.Instantiate(OriginSurface);
			originMeshRenderer = OriginSurface.GetComponent<MeshRenderer>();
			copyMeshRenderer = copySurface.GetComponent<MeshRenderer>();
			originMat = originMeshRenderer.sharedMaterial;
		}
		copySurface.transform.position = OriginSurface.transform.position;
		copySurface.transform.rotation = OriginSurface.transform.rotation;
		copySurface.transform.localScale = OriginSurface.transform.localScale;
		copySurface.gameObject.SetActive(value: true);
		Material material = UnityEngine.Object.Instantiate(srcFromMat);
		Material material2 = UnityEngine.Object.Instantiate(srcToMat);
		instanceMats.Add(material);
		instanceMats.Add(material2);
		material.SetInt(ShaderConstant._StencilRef, 8);
		material.SetInt(ShaderConstant._StencilReadMask, 8);
		material.SetInt(ShaderConstant._StencilComp, 6);
		material2.SetInt(ShaderConstant._StencilRef, 8);
		material2.SetInt(ShaderConstant._StencilReadMask, 8);
		material2.SetInt(ShaderConstant._StencilComp, 3);
		copyMeshRenderer.sharedMaterial = material;
		originMeshRenderer.sharedMaterial = material2;
	}

	public void OnSwitchFinish()
	{
		Material sharedMaterial = copyMeshRenderer.sharedMaterial;
		sharedMaterial.SetInt(ShaderConstant._StencilRef, 0);
		sharedMaterial.SetInt(ShaderConstant._StencilReadMask, 0);
		sharedMaterial.SetInt(ShaderConstant._StencilComp, 0);
		Material sharedMaterial2 = originMeshRenderer.sharedMaterial;
		sharedMaterial2.SetInt(ShaderConstant._StencilRef, 0);
		sharedMaterial2.SetInt(ShaderConstant._StencilReadMask, 0);
		sharedMaterial2.SetInt(ShaderConstant._StencilComp, 0);
		copySurface.gameObject.SetActive(value: false);
	}

	public void OnDestory()
	{
		instanceMats.ForEach(delegate(Material x)
		{
			UnityEngine.Object.DestroyImmediate(x);
		});
		instanceMats.Clear();
		if ((bool)copySurface)
		{
			UnityEngine.Object.Destroy(copySurface);
			copySurface = null;
			copyMeshRenderer = null;
		}
		if ((bool)originMeshRenderer)
		{
			originMeshRenderer.sharedMaterial = originMat;
		}
	}
}

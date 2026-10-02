using System;
using System.Collections.Generic;
using Render.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameLogic;

[Serializable]
public class SubMap
{
	[SerializeField]
	private Transform mapRoot;

	private List<Material> instanceMaterils;

	public Transform MapRoot => mapRoot;

	public void SetStencil(int stencilRef, CompareFunction stencilComp)
	{
		if (instanceMaterils == null)
		{
			DecalProjector[] array;
			Renderer[] array2;
			if ((bool)mapRoot)
			{
				array = mapRoot.GetComponentsInChildren<DecalProjector>(includeInactive: true);
				array2 = mapRoot.GetComponentsInChildren<Renderer>(includeInactive: true);
			}
			else
			{
				array = (DecalProjector[])(object)new DecalProjector[0];
				array2 = new Renderer[0];
			}
			instanceMaterils = new List<Material>();
			foreach (Renderer renderer in array2)
			{
				Material[] sharedMaterials = renderer.sharedMaterials;
				Material[] array3 = new Material[sharedMaterials.Length];
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					array3[j] = UnityEngine.Object.Instantiate(sharedMaterials[j]);
				}
				renderer.sharedMaterials = array3;
				instanceMaterils.AddRange(array3);
			}
			foreach (DecalProjector obj in array)
			{
				Material item = (obj.material = UnityEngine.Object.Instantiate(obj.material));
				instanceMaterils.Add(item);
			}
		}
		for (int l = 0; l < instanceMaterils.Count; l++)
		{
			Material material2 = instanceMaterils[l];
			material2.SetInt(ShaderConstant._StencilRef, stencilRef);
			material2.SetInt(ShaderConstant._StencilReadMask, stencilRef);
			material2.SetInt(ShaderConstant._StencilComp, (int)stencilComp);
		}
	}

	public void OnDestory()
	{
		if (instanceMaterils == null)
		{
			return;
		}
		foreach (Material instanceMateril in instanceMaterils)
		{
			UnityEngine.Object.DestroyImmediate(instanceMateril);
		}
		instanceMaterils = null;
	}
}

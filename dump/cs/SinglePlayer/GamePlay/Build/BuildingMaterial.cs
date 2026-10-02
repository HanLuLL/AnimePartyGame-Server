using System.Collections.Generic;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class BuildingMaterial : MonoBehaviour
{
	private MeshRenderer[] _meshRenderers;

	private Dictionary<int, Material> _materials = new Dictionary<int, Material>();

	public void Init()
	{
		_meshRenderers = GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] meshRenderers = _meshRenderers;
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			_materials.Add(meshRenderer.gameObject.GetInstanceID(), meshRenderer.material);
		}
	}

	public void ShowPreview(Material material)
	{
		MeshRenderer[] meshRenderers = _meshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].material = material;
		}
	}

	public void Clear()
	{
		MeshRenderer[] meshRenderers = _meshRenderers;
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			meshRenderer.material = _materials[meshRenderer.gameObject.GetInstanceID()];
		}
		_meshRenderers = null;
		_materials.Clear();
	}
}

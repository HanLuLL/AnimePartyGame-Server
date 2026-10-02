using Core.Unit;
using UnityEngine;

namespace Core;

public class OcclusionTest : Core.Unit.Unit
{
	private void OnTriggerEnter(Collider other)
	{
		ChangeMaterial(other, IsTransparent: true);
	}

	private void OnTriggerExit(Collider other)
	{
		ChangeMaterial(other, IsTransparent: false);
	}

	private void ChangeMaterial(Collider other, bool IsTransparent)
	{
		TransparentMaterialManager[] components = ((Component)(object)other).GetComponents<TransparentMaterialManager>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].SwitchMaterial(IsTransparent);
		}
	}
}

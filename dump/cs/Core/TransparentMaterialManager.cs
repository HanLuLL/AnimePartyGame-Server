using Core.Unit;
using UnityEngine;

namespace Core;

public class TransparentMaterialManager : Core.Unit.Unit
{
	[SerializeField]
	public Material OriginalMaterial;

	[SerializeField]
	public Material TransparentMaterial;

	public void SwitchMaterial(bool Transparent)
	{
		Renderer component = GetComponent<Renderer>();
		if (Transparent)
		{
			component.material = TransparentMaterial;
		}
		else
		{
			component.material = OriginalMaterial;
		}
	}
}

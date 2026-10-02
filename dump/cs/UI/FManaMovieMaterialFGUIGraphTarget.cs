using System;
using CriWare.Assets;
using UnityEngine;

namespace UI;

[Serializable]
public struct FManaMovieMaterialFGUIGraphTarget : ICriManaMovieMaterialTarget
{
	[SerializeField]
	private Renderer renderer;

	public Material Material
	{
		get
		{
			return renderer.material;
		}
		set
		{
			renderer.material = value;
		}
	}

	public bool IsActive
	{
		get
		{
			return renderer.enabled;
		}
		set
		{
			renderer.enabled = value;
		}
	}

	public bool uiRenderMode => true;

	public FManaMovieMaterialFGUIGraphTarget(Renderer renderer)
	{
		this.renderer = renderer;
	}
}

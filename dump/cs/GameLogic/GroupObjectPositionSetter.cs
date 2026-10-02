using System.Collections.Generic;
using Render.Runtime;
using UnityEngine;

namespace GameLogic;

[ExecuteInEditMode]
public class GroupObjectPositionSetter : MonoBehaviour
{
	[SerializeField]
	private Transform targetTransform;

	[SerializeField]
	private List<Renderer> subRenderers = new List<Renderer>();

	[SerializeField]
	private float floatingHeight;

	private MaterialPropertyBlock _mpb;

	private MaterialPropertyBlock mpb => _mpb ?? (_mpb = new MaterialPropertyBlock());

	private Transform TargetTransform
	{
		get
		{
			if (!(targetTransform == null))
			{
				return targetTransform;
			}
			return base.transform;
		}
	}

	private void FindChildRenderers()
	{
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!subRenderers.Contains(renderer) && renderer != null)
			{
				subRenderers.Add(renderer);
			}
		}
	}

	private void UpdateParameter()
	{
		Vector4 value = new Vector4(TargetTransform.position.x, TargetTransform.position.y, TargetTransform.position.z, 1f);
		mpb.SetVector(ShaderConstant._ObjectPosition, value);
		mpb.SetFloat(ShaderConstant._FloatintHeight, floatingHeight);
		foreach (Renderer subRenderer in subRenderers)
		{
			if (!(subRenderer == null))
			{
				subRenderer.SetPropertyBlock(mpb);
			}
		}
	}

	private void ClearParameter()
	{
		mpb.Clear();
		foreach (Renderer subRenderer in subRenderers)
		{
			if (!(subRenderer == null))
			{
				subRenderer.SetPropertyBlock(mpb);
			}
		}
	}

	private void OnEnable()
	{
		UpdateParameter();
	}

	private void OnDisable()
	{
		ClearParameter();
	}
}

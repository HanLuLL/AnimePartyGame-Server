using System;
using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

namespace Core;

[Serializable]
public class ParticleColorController : Core.Unit.Unit
{
	[Header("渐变色")]
	public List<Gradient> Colors = new List<Gradient>();

	[Header("HDR")]
	[ColorUsage(true, true)]
	public List<Color> HDRColors = new List<Color>();

	public void SetGradient(int slot)
	{
		if (Colors.Count > slot)
		{
			SetGradient(Colors[slot]);
		}
	}

	public void SetGradient(Gradient color)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		ParticleSystem component = GetComponent<ParticleSystem>();
		if ((UnityEngine.Object)(object)component != null)
		{
			ColorOverLifetimeModule colorOverLifetime = component.colorOverLifetime;
			((ColorOverLifetimeModule)(ref colorOverLifetime)).enabled = true;
			((ColorOverLifetimeModule)(ref colorOverLifetime)).color = MinMaxGradient.op_Implicit(color);
		}
	}

	public void SetMaterialColor(int index)
	{
		if (HDRColors.Count > index)
		{
			SetMaterialColor(HDRColors[index]);
		}
	}

	public void SetMaterialColor(Color color)
	{
		((Renderer)(object)GetComponent<ParticleSystemRenderer>())?.material.SetColor("_Add_Color", color);
	}
}

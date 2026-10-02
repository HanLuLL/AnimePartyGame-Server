using UnityEngine;

namespace FairyGUI;

internal class GearColorValue
{
	public Color color;

	public Color strokeColor;

	public GearColorValue()
	{
	}

	public GearColorValue(Color color, Color strokeColor)
	{
		this.color = color;
		this.strokeColor = strokeColor;
	}
}

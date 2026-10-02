using FairyGUI;
using UnityEngine;

namespace UI;

public class UIColorRecorder
{
	public IColorGear ColorGear;

	public Color OriginColor;

	public UIColorRecorder(IColorGear gear, Color color)
	{
		ColorGear = gear;
		OriginColor = color;
	}
}

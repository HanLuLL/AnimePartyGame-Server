using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlParseOptions
{
	public bool linkUnderline;

	public Color linkColor;

	public Color linkBgColor;

	public Color linkHoverBgColor;

	public bool ignoreWhiteSpace;

	public static bool DefaultLinkUnderline = true;

	public static Color DefaultLinkColor = new Color32(58, 103, 204, byte.MaxValue);

	public static Color DefaultLinkBgColor = Color.clear;

	public static Color DefaultLinkHoverBgColor = Color.clear;

	public HtmlParseOptions()
	{
		linkUnderline = DefaultLinkUnderline;
		linkColor = DefaultLinkColor;
		linkBgColor = DefaultLinkBgColor;
		linkHoverBgColor = DefaultLinkHoverBgColor;
	}
}

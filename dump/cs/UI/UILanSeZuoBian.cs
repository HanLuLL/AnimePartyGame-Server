using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILanSeZuoBian : GComponent
{
	public GLoader loader_Left;

	public const string URL = "ui://50xzye56r1gk1q";

	public static UILanSeZuoBian CreateInstance()
	{
		return (UILanSeZuoBian)UIPackage.CreateObject("AssistVoteS7", "蓝色左边");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Left = (GLoader)GetChildAt(0);
	}
}

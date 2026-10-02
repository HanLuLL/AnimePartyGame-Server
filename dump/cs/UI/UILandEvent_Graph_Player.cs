using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandEvent_Graph_Player : GComponent
{
	public Controller playerState;

	public GGraph loader_Animation;

	public GGraph loader_GoldEffect;

	public GTextField txt_AddGold;

	public GTextField txt_SubGold;

	public Transition showGold;

	public const string URL = "ui://duzt7gzkp4oi6";

	public static UILandEvent_Graph_Player CreateInstance()
	{
		return (UILandEvent_Graph_Player)UIPackage.CreateObject("LandEvent", "LandEvent_Graph_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		playerState = GetControllerAt(0);
		loader_Animation = (GGraph)GetChildAt(0);
		loader_GoldEffect = (GGraph)GetChildAt(1);
		txt_AddGold = (GTextField)GetChildAt(2);
		txt_SubGold = (GTextField)GetChildAt(6);
		showGold = GetTransitionAt(0);
	}
}

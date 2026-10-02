using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Lin : GComponent
{
	public GLoader loader_BG;

	public UIActivity_Com_Type5_Scratchoff_Lin com_Scratchoff;

	public GLoader loader_Character;

	public UIActivity_Com_Type5_ShowSkin_Lin com_ShowSkin;

	public Transition Cut_in;

	public const string URL = "ui://vckl96ksxcsllsq7v";

	public static UIActivity_Com_Type5_Lin CreateInstance()
	{
		return (UIActivity_Com_Type5_Lin)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_BG = (GLoader)GetChildAt(0);
		com_Scratchoff = (UIActivity_Com_Type5_Scratchoff_Lin)GetChildAt(1);
		loader_Character = (GLoader)GetChildAt(2);
		com_ShowSkin = (UIActivity_Com_Type5_ShowSkin_Lin)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}

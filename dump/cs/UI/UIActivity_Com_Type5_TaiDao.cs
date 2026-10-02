using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_TaiDao : GComponent
{
	public GLoader loader_BG;

	public GLoader loader_Character;

	public UIActivity_Com_Type5_Scratchoff_TaiDao com_Scratchoff;

	public UIActivity_Com_Type5_ShowSkin_TaiDao com_ShowSkin;

	public Transition Cut_in;

	public const string URL = "ui://vckl96ksyjnjlsq83";

	public static UIActivity_Com_Type5_TaiDao CreateInstance()
	{
		return (UIActivity_Com_Type5_TaiDao)UIPackage.CreateObject("Activity", "Activity_Com_Type5_TaiDao");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_BG = (GLoader)GetChildAt(0);
		loader_Character = (GLoader)GetChildAt(4);
		com_Scratchoff = (UIActivity_Com_Type5_Scratchoff_TaiDao)GetChildAt(5);
		com_ShowSkin = (UIActivity_Com_Type5_ShowSkin_TaiDao)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}

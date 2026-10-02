using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_ShowSkin_Lin : GComponent
{
	public Controller language;

	public GButton btn_ShowSkin;

	public GImage txt_Festival;

	public GTextField txt_SkinName;

	public GComponent com_Qulity;

	public GGraph graph_Video;

	public UIActivity_Com_Type5_Button_ShowPlatform btn_ShowVideo;

	public UIActivity_Com_Type5_Button_ShowPlatform btn_CloseVideo;

	public Transition ShowVideo;

	public Transition CloseVideo;

	public Transition Reset;

	public const string URL = "ui://vckl96ksxcsllsq80";

	public static UIActivity_Com_Type5_ShowSkin_Lin CreateInstance()
	{
		return (UIActivity_Com_Type5_ShowSkin_Lin)UIPackage.CreateObject("Activity", "Activity_Com_Type5_ShowSkin_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		btn_ShowSkin = (GButton)GetChildAt(0);
		txt_Festival = (GImage)GetChildAt(4);
		txt_SkinName = (GTextField)GetChildAt(6);
		com_Qulity = (GComponent)GetChildAt(7);
		graph_Video = (GGraph)GetChildAt(10);
		btn_ShowVideo = (UIActivity_Com_Type5_Button_ShowPlatform)GetChildAt(11);
		btn_CloseVideo = (UIActivity_Com_Type5_Button_ShowPlatform)GetChildAt(12);
		ShowVideo = GetTransitionAt(0);
		CloseVideo = GetTransitionAt(1);
		Reset = GetTransitionAt(2);
	}
}

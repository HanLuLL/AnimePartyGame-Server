using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Screen : GComponent
{
	public Controller country;

	public Controller isMobile;

	public UISetting_Com_ComboBox com_Resolution;

	public UISetting_Com_Camera com_camera;

	public UISetting_Item_Buttom Screen_Refresh_Item;

	public UISetting_Com_Toggle com_vSyncCount;

	public UISetting_Com_ComboBox com_FrameRate;

	public UISetting_Com_ComboBox com_Window;

	public UISetting_Com_ComboBox com_Language;

	public UISetting_Com_ComboBox com_Quality;

	public UISetting_Com_EnergySaving com_energySaving;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n87";

	public static UISetting_Com_Screen CreateInstance()
	{
		return (UISetting_Com_Screen)UIPackage.CreateObject("Setting", "Setting_Com_Screen");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		country = GetControllerAt(0);
		isMobile = GetControllerAt(1);
		com_Resolution = (UISetting_Com_ComboBox)GetChildAt(1);
		com_camera = (UISetting_Com_Camera)GetChildAt(3);
		Screen_Refresh_Item = (UISetting_Item_Buttom)GetChildAt(4);
		com_vSyncCount = (UISetting_Com_Toggle)GetChildAt(5);
		com_FrameRate = (UISetting_Com_ComboBox)GetChildAt(6);
		com_Window = (UISetting_Com_ComboBox)GetChildAt(9);
		com_Language = (UISetting_Com_ComboBox)GetChildAt(12);
		com_Quality = (UISetting_Com_ComboBox)GetChildAt(15);
		com_energySaving = (UISetting_Com_EnergySaving)GetChildAt(17);
		cut_in = GetTransitionAt(0);
	}
}

using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Volume : GComponent
{
	public Controller gameState;

	public Controller isMobile;

	public GSlider slider_MasterVolume;

	public GSlider slider_BGMVolume;

	public GSlider slider_SFXVolume;

	public UISetting_Item_Buttom Character_Voice_Settings;

	public GSlider slider_VoiceVolume;

	public UISetting_Com_ComboBox com_VoiceLanguages;

	public UISetting_Com_Vibrate com_vibrate;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n8q";

	public static UISetting_Com_Volume CreateInstance()
	{
		return (UISetting_Com_Volume)UIPackage.CreateObject("Setting", "Setting_Com_Volume");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gameState = GetControllerAt(0);
		isMobile = GetControllerAt(1);
		slider_MasterVolume = (GSlider)GetChildAt(1);
		slider_BGMVolume = (GSlider)GetChildAt(4);
		slider_SFXVolume = (GSlider)GetChildAt(7);
		Character_Voice_Settings = (UISetting_Item_Buttom)GetChildAt(9);
		slider_VoiceVolume = (GSlider)GetChildAt(10);
		com_VoiceLanguages = (UISetting_Com_ComboBox)GetChildAt(13);
		com_vibrate = (UISetting_Com_Vibrate)GetChildAt(15);
		cut_in = GetTransitionAt(0);
	}
}

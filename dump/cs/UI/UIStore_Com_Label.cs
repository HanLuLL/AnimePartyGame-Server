using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Label : GComponent
{
	public Controller labelController;

	public const string URL = "ui://zyd0rl00qdq5j";

	public static UIStore_Com_Label CreateInstance()
	{
		return (UIStore_Com_Label)UIPackage.CreateObject("Store", "Store_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		labelController = GetControllerAt(0);
	}
}

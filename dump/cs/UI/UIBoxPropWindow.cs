using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBoxPropWindow : GComponent
{
	public Controller type;

	public GComponent mohu;

	public GLabel bottom;

	public UIBoxProp_Com_OpenChest com_Chest;

	public UIBoxProp_Com_GiftContent com_Gift;

	public Transition Cut_in;

	public const string URL = "ui://crbpicgjhhk60";

	public static UIBoxPropWindow CreateInstance()
	{
		BindAll();
		return (UIBoxPropWindow)UIPackage.CreateObject("BoxProp", "BoxPropWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://crbpicgjaxwd10", typeof(UIBoxProp_Com_OpenChest));
		UIObjectFactory.SetPackageItemExtension("ui://crbpicgjaxwdz", typeof(UIBoxProp_Com_GiftContent));
		UIObjectFactory.SetPackageItemExtension("ui://crbpicgjhhk60", typeof(UIBoxPropWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		com_Chest = (UIBoxProp_Com_OpenChest)GetChildAt(2);
		com_Gift = (UIBoxProp_Com_GiftContent)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}

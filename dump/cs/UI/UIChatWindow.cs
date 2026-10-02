using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChatWindow : GComponent
{
	public GGraph mohu;

	public UIChat_Com com_chat;

	public const string URL = "ui://y0luzhk8b93d0";

	public static UIChatWindow CreateInstance()
	{
		BindAll();
		return (UIChatWindow)UIPackage.CreateObject("Chat", "ChatWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8b93d0", typeof(UIChatWindow));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8b93d3", typeof(UIChat_Com_Left));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8b93d6", typeof(UIChat_Com_Right));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednm10", typeof(UIChat_Com_ExpressionItem));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednm11", typeof(UIChat_Button_Pin));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednm13", typeof(UIChat_Button_ExpressionItem));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednm15", typeof(UIChat_Button_ExpressionTab));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednm16", typeof(UIChat_Com_ExpressionPopup));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednmx", typeof(UIChat_Button_Session));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8ednmz", typeof(UIChat_Button_OpenExpression));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8mfy81f", typeof(UIChat_Effect));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8njjna", typeof(UIChat_Com_SessionLabel));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8sgc41d", typeof(UIChat_Com_SessionOperatePopup));
		UIObjectFactory.SetPackageItemExtension("ui://y0luzhk8x1udb", typeof(UIChat_Com));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		com_chat = (UIChat_Com)GetChildAt(1);
	}
}

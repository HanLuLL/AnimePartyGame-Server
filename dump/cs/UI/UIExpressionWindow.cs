using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpressionWindow : GComponent
{
	public GGraph graph_Line;

	public UIExpression_Com_Interact_PC com_Interact_PC;

	public UIExpression_Com_Interact_Mobile com_Interact_Mobile;

	public UIExpression_Com_Mark com_Mark;

	public UIExpression_Com_ChatMenu com_ChatMenu;

	public const string URL = "ui://mp1ylwytwtax7";

	public static UIExpressionWindow CreateInstance()
	{
		BindAll();
		return (UIExpressionWindow)UIPackage.CreateObject("Expression", "ExpressionWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytgbr1r", typeof(UIExpression_Button_ShortInfo_PC));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytikma10", typeof(UIExpression_Button_QuickReply));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytnrib8", typeof(UIExpression_Button_ListExpression));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytq93cu", typeof(UIExpression_Button_MapChat));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytr4n912", typeof(UIExpression_Com_Mark));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytr5j51z", typeof(UIExpression_Com_Interact_PC));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytrct9z", typeof(UIExpression_Button_ShortInfo_Mobile));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytuapr20", typeof(UIExpression_Com_Interact_Mobile));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytvkgo13", typeof(UIExpression_Com_ChatMenu));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytvkgo17", typeof(UIExpression_Button_MenuItem));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytvkgo1i", typeof(UIExpression_Com_MenuTabFour));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytvkgo1j", typeof(UIExpression_Com_MenuTabThree));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytwfpy1x", typeof(UIExpression_Com_Preview));
		UIObjectFactory.SetPackageItemExtension("ui://mp1ylwytwtax7", typeof(UIExpressionWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Line = (GGraph)GetChildAt(0);
		com_Interact_PC = (UIExpression_Com_Interact_PC)GetChildAt(1);
		com_Interact_Mobile = (UIExpression_Com_Interact_Mobile)GetChildAt(2);
		com_Mark = (UIExpression_Com_Mark)GetChildAt(3);
		com_ChatMenu = (UIExpression_Com_ChatMenu)GetChildAt(4);
	}
}

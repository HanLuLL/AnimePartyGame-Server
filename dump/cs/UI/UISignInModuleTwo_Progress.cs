using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInModuleTwo_Progress : GProgressBar
{
	public GGroup arrow;

	public Transition LOOP;

	public const string URL = "ui://ik9iuwgyb1ak1q";

	public static UISignInModuleTwo_Progress CreateInstance()
	{
		return (UISignInModuleTwo_Progress)UIPackage.CreateObject("SignInModuleTwo", "SignInModuleTwo_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		arrow = (GGroup)GetChildAt(4);
		LOOP = GetTransitionAt(0);
	}
}

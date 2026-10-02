using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISignInModuleOne_Progress : GProgressBar
{
	public GGroup arrow;

	public Transition LOOP;

	public const string URL = "ui://3bkg2bqqkei21b";

	public static UISignInModuleOne_Progress CreateInstance()
	{
		return (UISignInModuleOne_Progress)UIPackage.CreateObject("SignInModuleOne", "SignInModuleOne_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		arrow = (GGroup)GetChildAt(4);
		LOOP = GetTransitionAt(0);
	}
}

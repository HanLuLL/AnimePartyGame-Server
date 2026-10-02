using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_CrimeNum : GComponent
{
	public GTextField txt_Counter;

	public GGroup group_Counter;

	public Transition Shake;

	public const string URL = "ui://1ov1i0v9l6yms8u";

	public void Refresh(int count)
	{
		txt_Counter.visible = count > 0;
		group_Counter.visible = count > 0;
		if (count > 0)
		{
			txt_Counter.text = count.ToString();
		}
	}

	public static UICom_CrimeNum CreateInstance()
	{
		return (UICom_CrimeNum)UIPackage.CreateObject("Common_Internal", "Com_CrimeNum");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Counter = (GTextField)GetChildAt(1);
		group_Counter = (GGroup)GetChildAt(2);
		Shake = GetTransitionAt(0);
	}
}

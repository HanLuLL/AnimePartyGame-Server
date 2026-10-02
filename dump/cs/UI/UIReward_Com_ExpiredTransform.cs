using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReward_Com_ExpiredTransform : GComponent
{
	public GGraph mohu;

	public GList list_Prop_Expire;

	public GList list_Prop_Transform;

	public const string URL = "ui://r5u0087tvau3q";

	public static UIReward_Com_ExpiredTransform CreateInstance()
	{
		return (UIReward_Com_ExpiredTransform)UIPackage.CreateObject("Reward", "Reward_Com_ExpiredTransform");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		list_Prop_Expire = (GList)GetChildAt(2);
		list_Prop_Transform = (GList)GetChildAt(4);
	}
}

using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Com_UpgradeTips : GComponent
{
	public Transition CutIn;

	public const string URL = "ui://fxejlqlfjcp5bb";

	public Transform Target;

	public static UIBattleInfo_Com_UpgradeTips CreateInstance()
	{
		return (UIBattleInfo_Com_UpgradeTips)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_UpgradeTips");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		CutIn = GetTransitionAt(0);
	}

	public void ShowUpgradeTips(Transform target)
	{
		Target = target;
	}

	public override void Dispose()
	{
		base.Dispose();
	}
}

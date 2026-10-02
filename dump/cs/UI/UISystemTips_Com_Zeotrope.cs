using System;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISystemTips_Com_Zeotrope : GComponent
{
	private GTweener tweener;

	public GGraph graph_Di;

	public GTextField txt_ZoetropeContent;

	public GGraph graph_Pos;

	public const string URL = "ui://jrqrz0oqvlgj2";

	public void StartShow(string content, Action complete)
	{
		txt_ZoetropeContent.text = content;
		LocalToRoot(graph_Pos.position, GRoot.inst);
		txt_ZoetropeContent.x = graph_Di.x + graph_Di.size.x;
		float endValue = 0f - txt_ZoetropeContent.size.x;
		float duration = (float)content.Length * 0.25f;
		tweener?.Kill();
		tweener = txt_ZoetropeContent.TweenMoveX(endValue, duration).OnComplete(complete.Invoke).SetEase(EaseType.Linear);
	}

	public void FinishShow()
	{
		tweener?.Kill();
	}

	public static UISystemTips_Com_Zeotrope CreateInstance()
	{
		return (UISystemTips_Com_Zeotrope)UIPackage.CreateObject("SystemTips", "SystemTips_Com_Zeotrope");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Di = (GGraph)GetChildAt(0);
		txt_ZoetropeContent = (GTextField)GetChildAt(1);
		graph_Pos = (GGraph)GetChildAt(2);
	}
}

using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using Tools;
using UnityEngine;

namespace UI;

public class UITutorial_Com_SystemInfo : GComponent
{
	public Controller systemIndex;

	public GLoader loader_Icon;

	public GTextField txt_Title;

	public GRichTextField txt_Tutorial;

	public GButton btn_Comfirm;

	public GButton btn_Cancel;

	public Transition Cut_in;

	public const string URL = "ui://b96qpoz6mk9qq48";

	public async UniTask ShowInfo(int infoId, float delay)
	{
		if (!StaticConfigure.Tutorial.PopupDict.TryGetValue(infoId, out var value))
		{
			Debug.LogError($"无法通关{infoId} 在Tutorial.PopupDict中找到对应配置");
			return;
		}
		btn_Cancel.visible = false;
		btn_Comfirm.visible = true;
		systemIndex.selectedIndex = value.UiIndex;
		txt_Tutorial.text = value.ContentId.GetLocal(UIStringType.Tutorial);
		txt_Title.text = 100.GetLocal(UIStringType.Tutorial);
		if (delay > 0f)
		{
			base.visible = false;
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay((int)(delay * 1000f));
			base.visible = true;
		}
		else
		{
			base.visible = true;
		}
		Cut_in.Play();
	}

	public static UITutorial_Com_SystemInfo CreateInstance()
	{
		return (UITutorial_Com_SystemInfo)UIPackage.CreateObject("Tutorial", "Tutorial_Com_SystemInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		systemIndex = GetControllerAt(0);
		loader_Icon = (GLoader)GetChildAt(2);
		txt_Title = (GTextField)GetChildAt(3);
		txt_Tutorial = (GRichTextField)GetChildAt(4);
		btn_Comfirm = (GButton)GetChildAt(5);
		btn_Cancel = (GButton)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}

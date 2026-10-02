using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using Tools;
using UnityEngine;

namespace UI;

public class UITutorial_Com_Tutorial : GComponent
{
	public GLoader loader_Skin;

	public GLoader loader_Expression;

	public UITutorial_Com_Dialog com_Dialog;

	public GGraph graph_Speaker;

	public GTextField txt_Speaker;

	public GLoader loader_Speaker;

	public GGroup group_Speaker;

	public Transition Cut_in;

	public const string URL = "ui://b96qpoz68vxw8";

	public async UniTask RefreshStandingPainting(TutorialdialogConfigure dialogConfig, int index)
	{
		string emoji = dialogConfig.TutorialdialogConfigureItems[index].Expression;
		string sp = GetStandingPainting(dialogConfig.SfwStandingPainting, dialogConfig.StandingPainting);
		if (!string.IsNullOrWhiteSpace(sp))
		{
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(sp, null, null);
		}
		if (!string.IsNullOrEmpty(emoji))
		{
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(emoji, null, null);
		}
		if (!string.IsNullOrWhiteSpace(sp))
		{
			loader_Skin.customOffset = new Vector2(53f, 62f);
			loader_Skin.url = sp;
			loader_Skin.visible = true;
		}
		else
		{
			loader_Skin.visible = false;
		}
		if (!string.IsNullOrEmpty(emoji))
		{
			loader_Expression.customOffset = new Vector2(446f, -36f);
			loader_Expression.url = emoji;
			loader_Expression.visible = true;
		}
		else
		{
			loader_Expression.visible = false;
		}
	}

	private string GetStandingPainting(string sfwStandingPainting, string standingPainting)
	{
		if (GameSettings.angelMode)
		{
			if (string.IsNullOrEmpty(sfwStandingPainting))
			{
				return standingPainting;
			}
			return sfwStandingPainting;
		}
		return standingPainting;
	}

	public static UITutorial_Com_Tutorial CreateInstance()
	{
		return (UITutorial_Com_Tutorial)UIPackage.CreateObject("Tutorial", "Tutorial_Com_Tutorial");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Skin = (GLoader)GetChildAt(0);
		loader_Expression = (GLoader)GetChildAt(1);
		com_Dialog = (UITutorial_Com_Dialog)GetChildAt(3);
		graph_Speaker = (GGraph)GetChildAt(4);
		txt_Speaker = (GTextField)GetChildAt(5);
		loader_Speaker = (GLoader)GetChildAt(6);
		group_Speaker = (GGroup)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}

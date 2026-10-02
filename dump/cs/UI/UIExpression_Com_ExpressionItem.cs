using System;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using Tools;
using UnityEngine;
using UnityTimer;

namespace UI;

public class UIExpression_Com_ExpressionItem : GComponent
{
	private Timer timer;

	public GImage dialog;

	public GComponent com_Expression;

	public const string URL = "ui://bdqipkfgnriba";

	public async void RefreshInfo(BattleMessage msg)
	{
		base.touchable = false;
		if (msg.expressionData.isVideo)
		{
			((UICom_Expression)com_Expression).type.selectedIndex = 1;
			GGraph loader_Expression_Video = ((UICom_Expression)com_Expression).loader_Expression_Video;
			await SimpleSingletonProvider<CriMovieManager>.inst.PlaAutoReleaseVideo(msg.expressionData.expressionConfig.VideoKey, loader_Expression_Video);
		}
		else
		{
			((UICom_Expression)com_Expression).type.selectedIndex = 0;
			((UICom_Expression)com_Expression).loader_Expression_Image.url = msg.expressionData.textureUrl;
		}
		dialog.color = GameConfig.slotColor[msg.Sender.player.Slot];
		base.visible = true;
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
		timer = Timer.Register(0f, (float)StaticGlobalData.GAME_CHARACTER_EXPRESSION_DURATION * 0.001f, (Action)delegate
		{
			base.visible = false;
			Release();
		}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
	}

	private void Release()
	{
		if (((UICom_Expression)com_Expression).type.selectedIndex == 1)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAuto(((UICom_Expression)com_Expression).loader_Expression_Video);
		}
		else
		{
			((UICom_Expression)com_Expression).loader_Expression_Image.texture = null;
		}
	}

	public void Close()
	{
		Release();
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
		timer = null;
	}

	public override void Dispose()
	{
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
		timer = null;
		base.Dispose();
	}

	public static UIExpression_Com_ExpressionItem CreateInstance()
	{
		return (UIExpression_Com_ExpressionItem)UIPackage.CreateObject("ExpressionList", "Expression_Com_ExpressionItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		dialog = (GImage)GetChildAt(0);
		com_Expression = (GComponent)GetChildAt(1);
	}
}

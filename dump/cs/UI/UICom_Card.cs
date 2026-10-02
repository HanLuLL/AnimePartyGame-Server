using System;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Card : GButton
{
	public Controller showFront;

	public Controller isVideo;

	public Controller redPoint;

	public Controller frontState;

	public Controller hideTitle;

	public GLoader loader_CardFrame;

	public GLoader loader_FrontCard;

	public GGraph video_FrontCard;

	public GLoader loader_FullCard;

	public GGraph video_FullCard;

	public GLoader loader_CardFront;

	public GImage bg_txt;

	public UICom_Card_Name com_CardName;

	public UICom_Card_Icon com_CardIcon;

	public GTextField txt_CardIndex;

	public GTextField txt_CardIndex_2;

	public GTextField txt_CardTips;

	public GRichTextField txt_Content;

	public GTextField txt_Name;

	public UICom_CardBack com_CardBack;

	public GGraph graph_Hover;

	public Transition Cut__In;

	public Transition Cut_out;

	public Transition HideDescState;

	public Transition ShowDescState;

	public const string URL = "ui://xuaw6o8jmicc2g";

	private bool isHideDesc;

	public bool opened
	{
		get
		{
			return showFront.selectedIndex == 0;
		}
		set
		{
			GTween.Kill(this);
			showFront.selectedIndex = ((!value) ? 1 : 0);
		}
	}

	public static UICom_Card CreateInstance()
	{
		return (UICom_Card)UIPackage.CreateObject("Common", "Com_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showFront = GetControllerAt(0);
		isVideo = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		frontState = GetControllerAt(3);
		hideTitle = GetControllerAt(4);
		loader_CardFrame = (GLoader)GetChildAt(0);
		loader_FrontCard = (GLoader)GetChildAt(1);
		video_FrontCard = (GGraph)GetChildAt(2);
		loader_FullCard = (GLoader)GetChildAt(3);
		video_FullCard = (GGraph)GetChildAt(4);
		loader_CardFront = (GLoader)GetChildAt(5);
		bg_txt = (GImage)GetChildAt(6);
		com_CardName = (UICom_Card_Name)GetChildAt(34);
		com_CardIcon = (UICom_Card_Icon)GetChildAt(35);
		txt_CardIndex = (GTextField)GetChildAt(36);
		txt_CardIndex_2 = (GTextField)GetChildAt(37);
		txt_CardTips = (GTextField)GetChildAt(38);
		txt_Content = (GRichTextField)GetChildAt(39);
		txt_Name = (GTextField)GetChildAt(40);
		com_CardBack = (UICom_CardBack)GetChildAt(41);
		graph_Hover = (GGraph)GetChildAt(43);
		Cut__In = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
		HideDescState = GetTransitionAt(2);
		ShowDescState = GetTransitionAt(3);
	}

	public void Turn(Action _onComplete = null)
	{
		if (!GTween.IsTweening(this))
		{
			base.rotationY = 0f;
			GTween.To(0f, 180f, 0.8f).SetTarget(this).SetEase(EaseType.QuadOut)
				.OnUpdate(TurnInTween)
				.OnComplete((GTweenCallback)delegate
				{
					_onComplete?.Invoke();
				});
		}
	}

	private void TurnInTween(GTweener tweener)
	{
		float num = tweener.value.x;
		if (num > 90f)
		{
			base.rotationY = -180f + num;
			showFront.selectedIndex = 0;
		}
		else
		{
			base.rotationY = num;
			showFront.selectedIndex = 1;
		}
	}

	public void ChangeCardDescState(bool isHide, bool isPlayAni)
	{
		if (!isPlayAni)
		{
			if (isHide)
			{
				HideDescState.Play();
			}
			else
			{
				ShowDescState.Play();
			}
			isHideDesc = isHide;
		}
		else if (isHide != isHideDesc)
		{
			if (isHide)
			{
				Cut_out.Play();
				Cut__In.Stop();
			}
			else
			{
				Cut__In.Play();
				Cut_out.Stop();
			}
			isHideDesc = isHide;
		}
	}
}

using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIHome_Com_ExpandActivity : GComponent
{
	private enum ExpandActivityEntryType
	{
		ComeBack,
		Questionnaire
	}

	private const float AutoScrollInterval = 2f;

	private readonly List<ExpandActivityEntryType> visibleEntries = new List<ExpandActivityEntryType>();

	private float autoScrollTime;

	private bool startScrollStatus;

	private bool isPointerOver;

	private bool hasComebackRedPoint;

	private bool hasQuestionnaireRedPoint;

	public GList list_Activity;

	public GList list_Page;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgumyupq5m";

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Activity.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Activity.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	public void InitComponent()
	{
		list_Activity.SetVirtualAndLoop();
		list_Activity.scrollPane.decelerationRate = 0.05f;
		list_Activity.itemProvider = ProvideActivityItem;
		list_Activity.itemRenderer = RefreshActivityButton;
	}

	public void Show()
	{
		isPointerOver = false;
		RefreshItems();
	}

	public void RefreshItems()
	{
		visibleEntries.Clear();
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null && comeback.Data?.IsInPeriod() == true)
		{
			visibleEntries.Add(ExpandActivityEntryType.ComeBack);
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.survey.surveyCenterData.CheckView())
		{
			visibleEntries.Add(ExpandActivityEntryType.Questionnaire);
		}
		hasComebackRedPoint = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.comebackRedSignal.Value ?? false;
		hasQuestionnaireRedPoint = SimpleSingletonProvider<GameLogicManager>.inst.survey?.questionnaireRedSignal.Value ?? false;
		int count = visibleEntries.Count;
		list_Activity.numItems = count;
		if (count > 1)
		{
			list_Activity.RefreshVirtualList();
		}
		base.visible = count > 0;
		list_Page.visible = count > 1;
		list_Page.numItems = ((count > 1) ? count : 0);
		list_Page.selectedIndex = 0;
		list_Activity.scrollPane.touchEffect = count > 1;
		if (count > 0)
		{
			list_Activity.scrollPane.SetCurrentPageX(0, ani: false);
			list_Activity.scrollPane.onScroll.Call();
		}
		if (count > 1 && !isPointerOver)
		{
			StartScroll();
		}
		else
		{
			StopScroll();
		}
	}

	public void AddEvent()
	{
		list_Activity.scrollPane.onScroll.Add(OnActivityScroll);
		base.onRollOver.Add(OnRollOver);
		base.onRollOut.Add(OnRollOut);
	}

	public void RemoveEvent()
	{
		list_Activity.scrollPane.onScroll.Remove(OnActivityScroll);
		base.onRollOver.Remove(OnRollOver);
		base.onRollOut.Remove(OnRollOut);
	}

	public void AddListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.AddListener(OnComebackInfoUpdated);
			comeback.comebackRedSignal.AddListener(OnComebackRedPointChanged);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.survey?.questionnaireRedSignal.AddListener(OnQuestionnaireRedPointChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.survey?.signal.surveyStateUpdated.AddListener(OnSurveyStateUpdated);
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal.activityStatus.AddListener(OnActivityStatusChanged);
	}

	public void RemoveListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.RemoveListener(OnComebackInfoUpdated);
			comeback.comebackRedSignal.RemoveListener(OnComebackRedPointChanged);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.survey?.questionnaireRedSignal.RemoveListener(OnQuestionnaireRedPointChanged);
		SimpleSingletonProvider<GameLogicManager>.inst.survey?.signal.surveyStateUpdated.RemoveListener(OnSurveyStateUpdated);
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal.activityStatus.RemoveListener(OnActivityStatusChanged);
	}

	public void StopScroll()
	{
		autoScrollTime = 0f;
		startScrollStatus = false;
	}

	private void StartScroll()
	{
		autoScrollTime = 0f;
		startScrollStatus = list_Activity.numItems > 1;
	}

	private void OnRollOver(EventContext context)
	{
		isPointerOver = true;
		StopScroll();
	}

	private void OnRollOut(EventContext context)
	{
		isPointerOver = false;
		StartScroll();
	}

	private void OnActivityScroll(EventContext context)
	{
		if (list_Activity.numItems > 0)
		{
			int selectedIndex = list_Activity.scrollPane.currentPageX % list_Activity.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private string ProvideActivityItem(int index)
	{
		if (index < 0 || index >= visibleEntries.Count)
		{
			return "ui://u7xbdcguy9qnq5j";
		}
		return visibleEntries[index] switch
		{
			ExpandActivityEntryType.ComeBack => "ui://u7xbdcguy9qnq5j", 
			ExpandActivityEntryType.Questionnaire => "ui://u7xbdcgumyupq5o", 
			_ => "ui://u7xbdcguy9qnq5j", 
		};
	}

	private void RefreshActivityButton(int index, GObject item)
	{
		if (index < 0 || index >= visibleEntries.Count)
		{
			item.visible = false;
			return;
		}
		switch (visibleEntries[index])
		{
		case ExpandActivityEntryType.ComeBack:
		{
			UIHome_Button_ComeBack uIHome_Button_ComeBack = item as UIHome_Button_ComeBack;
			if (uIHome_Button_ComeBack != null)
			{
				item.visible = true;
				ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
				if (returnInfo == null || returnInfo.TriggerTime == 0L || returnInfo.EndTime == 0L)
				{
					uIHome_Button_ComeBack.txt_Time.visible = false;
				}
				else
				{
					uIHome_Button_ComeBack.txt_Time.text = TimeHelper.GetDurationText(returnInfo.TriggerTime, returnInfo.EndTime, OnlyDuration: true);
					uIHome_Button_ComeBack.txt_Time.visible = true;
				}
				uIHome_Button_ComeBack.title = 6999801.GetLocal(UIStringType.Activity);
				uIHome_Button_ComeBack.redPoint.selectedIndex = (hasComebackRedPoint ? 1 : 0);
				uIHome_Button_ComeBack.onClick.Set((EventCallback0)async delegate
				{
					uIHome_Button_ComeBack.onClick.Retain();
					await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.ActivityComeback);
					uIHome_Button_ComeBack.onClick.Release();
				});
				return;
			}
			break;
		}
		case ExpandActivityEntryType.Questionnaire:
		{
			UIHome_Button_Questionnaire uIHome_Button_Questionnaire = item as UIHome_Button_Questionnaire;
			if (uIHome_Button_Questionnaire != null)
			{
				item.visible = true;
				uIHome_Button_Questionnaire.redPoint.selectedIndex = (hasQuestionnaireRedPoint ? 1 : 0);
				uIHome_Button_Questionnaire.onClick.Set((EventCallback0)async delegate
				{
					uIHome_Button_Questionnaire.onClick.Retain();
					await SimpleSingletonProvider<UIManager>.inst.SurveyCenter.ShowSurveyCenter();
					uIHome_Button_Questionnaire.onClick.Release();
				});
				uIHome_Button_Questionnaire.title = 2609011.GetLocal(UIStringType.Activity);
				return;
			}
			break;
		}
		}
		item.visible = false;
	}

	private void OnComebackInfoUpdated(ReturnInfo _)
	{
		RefreshItems();
	}

	private void OnSurveyStateUpdated(int surveyId)
	{
		RefreshItems();
	}

	private void OnComebackRedPointChanged(bool hasRedPoint)
	{
		hasComebackRedPoint = hasRedPoint;
		list_Activity.RefreshVirtualList();
	}

	private void OnQuestionnaireRedPointChanged(bool hasRedPoint)
	{
		hasQuestionnaireRedPoint = hasRedPoint;
		list_Activity.RefreshVirtualList();
	}

	private void OnActivityStatusChanged(int _)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.comeback?.RegisterRed();
	}

	public static UIHome_Com_ExpandActivity CreateInstance()
	{
		return (UIHome_Com_ExpandActivity)UIPackage.CreateObject("Home", "Home_Com_ExpandActivity");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Activity = (GList)GetChildAt(0);
		list_Page = (GList)GetChildAt(1);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}

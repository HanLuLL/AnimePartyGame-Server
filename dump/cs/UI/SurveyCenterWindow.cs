using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class SurveyCenterWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private readonly List<int> _showSurveyIds = new List<int>();

	private int _curSurveyId;

	public SurveyCenterWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISurveyCenterWindow.CreateInstance();
		base.OnInit();
		if (base.contentPane is UISurveyCenterWindow uISurveyCenterWindow)
		{
			uISurveyCenterWindow.list_tab.itemRenderer = RendererTabItem;
		}
	}

	public async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private void RefreshData()
	{
		_showSurveyIds.Clear();
		foreach (SurveyData value in SimpleSingletonProvider<GameLogicManager>.inst.survey.surveyCenterData.surveyDatas.Values)
		{
			if (value.serverData.State == QuestionModel.Types.QuestionState.None)
			{
				_showSurveyIds.Add(value.id);
				SimpleSingletonProvider<GameLogicManager>.inst.survey.RequestGetQuestionUrl(value.id);
			}
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISurveyCenterWindow { bottom: UICom_PopUpWindow_Bottom bottom } uISurveyCenterWindow)
		{
			RefreshWindow();
			bottom.closeButton.onClick.Add(base.Hide);
			uISurveyCenterWindow.btn_confirm.onClick.Add(OnClickSurvey);
			SimpleSingletonProvider<GameLogicManager>.inst.survey.signal.surveyStateUpdated.AddListener(OnSurveyStateUpdated);
			SimpleSingletonProvider<GameLogicManager>.inst.survey.CacheRed();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UISurveyCenterWindow { bottom: UICom_PopUpWindow_Bottom bottom } uISurveyCenterWindow)
		{
			uISurveyCenterWindow.list_tab.onClickItem.Remove(OnTabItemClick);
			uISurveyCenterWindow.list_tab.numItems = 0;
			uISurveyCenterWindow.com_Content.visible = false;
			_showSurveyIds.Clear();
			bottom.closeButton.onClick.Remove(base.Hide);
			uISurveyCenterWindow.btn_confirm.onClick.Remove(OnClickSurvey);
			SimpleSingletonProvider<GameLogicManager>.inst.survey.signal.surveyStateUpdated.RemoveListener(OnSurveyStateUpdated);
			blurBgCtrl.OnHide();
		}
	}

	public void RefreshWindow()
	{
		if (base.contentPane is UISurveyCenterWindow uISurveyCenterWindow)
		{
			RefreshData();
			uISurveyCenterWindow.list_tab.onClickItem.Set(OnTabItemClick);
			uISurveyCenterWindow.list_tab.numItems = _showSurveyIds.Count;
			uISurveyCenterWindow.com_Content.visible = _showSurveyIds.Count > 0;
			if (_showSurveyIds.Count > 0)
			{
				RefreshContent();
			}
			if (_showSurveyIds.Count == 0)
			{
				Hide();
			}
		}
	}

	public async UniTask ShowSurveyCenter(int surveyId = 0)
	{
		_curSurveyId = surveyId;
		await TryShowAsync();
	}

	private void RendererTabItem(int index, GObject item)
	{
		if (item is UISurveyCenter_Button_TabItem uISurveyCenter_Button_TabItem && index >= 0 && index < _showSurveyIds.Count)
		{
			SurveyData surveyData = SimpleSingletonProvider<GameLogicManager>.inst.survey.GetSurveyData(_showSurveyIds[index]);
			uISurveyCenter_Button_TabItem.title = surveyData.surveyInfoConfigure.NameID.GetLocal(UIStringType.Survey);
		}
	}

	private void OnTabItemClick(EventContext context)
	{
		if (base.contentPane is UISurveyCenterWindow uISurveyCenterWindow && context.data is GObject child)
		{
			int childIndex = uISurveyCenterWindow.list_tab.GetChildIndex(child);
			if (childIndex >= 0 && childIndex < _showSurveyIds.Count)
			{
				RefreshContent(childIndex);
			}
		}
	}

	private void RefreshContent(int index)
	{
		if (!(base.contentPane is UISurveyCenterWindow uISurveyCenterWindow) || index < 0 || index >= _showSurveyIds.Count)
		{
			return;
		}
		for (int i = 0; i < uISurveyCenterWindow.list_tab.numItems; i++)
		{
			if (uISurveyCenterWindow.list_tab.GetChildAt(i) is UISurveyCenter_Button_TabItem uISurveyCenter_Button_TabItem)
			{
				uISurveyCenter_Button_TabItem.selected = index == i;
			}
		}
		_curSurveyId = _showSurveyIds[index];
		SurveyData surveyData = SimpleSingletonProvider<GameLogicManager>.inst.survey.GetSurveyData(_curSurveyId);
		uISurveyCenterWindow.com_Content.RefreshSurvey(surveyData);
		uISurveyCenterWindow.com_Content.scrollPane.posY = 0f;
	}

	private void RefreshContent()
	{
		if (_curSurveyId == 0)
		{
			RefreshContent(0);
			return;
		}
		for (int i = 0; i < _showSurveyIds.Count; i++)
		{
			if (_showSurveyIds[i] == _curSurveyId)
			{
				RefreshContent(i);
				return;
			}
		}
		RefreshContent(0);
	}

	private async void OnClickSurvey(EventContext context)
	{
		if (_curSurveyId == 0)
		{
			Debug.LogError("异常数据 选择的问卷id为 0 或未选中问卷");
			return;
		}
		SurveyData surveyData = SimpleSingletonProvider<GameLogicManager>.inst.survey.GetSurveyData(_curSurveyId);
		if (string.IsNullOrEmpty(surveyData.url))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.survey.RequestGetQuestionUrl(surveyData.id);
		}
		if (!string.IsNullOrEmpty(surveyData.url))
		{
			Application.OpenURL(surveyData.url);
		}
	}

	private void OnSurveyStateUpdated(int surveyId)
	{
		if (surveyId == _curSurveyId && SimpleSingletonProvider<GameLogicManager>.inst.survey.GetSurveyData(_curSurveyId).serverData.State != QuestionModel.Types.QuestionState.None)
		{
			RefreshWindow();
		}
	}
}

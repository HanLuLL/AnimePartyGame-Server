using System.Collections.Generic;
using Core;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIActivityComeback_Gift_Com : GComponent
{
	private int _chestId;

	private int _heroId0;

	private int _heroId1;

	private int _heroId2;

	private int _heroId3;

	private string _questionnaireId;

	public Controller language;

	public UIActivityComeback_Hero_Button hero4;

	public UIActivityComeback_Hero_Button hero3;

	public UIActivityComeback_Hero_Button hero2;

	public UIActivityComeback_Hero_Button hero1;

	public UIActivityComeback_GetGift_Button btn_getgift;

	public UIActivityComeback_Gift_Item gift_item;

	public GTextField gift_name;

	public GLoader loader_question;

	public UIActivityComeback_Questionnaire_Button btn_Questionnaire;

	public GTextField comeback_desc;

	public Transition Cut_in;

	public const string URL = "ui://hconmwfcy9qn7";

	public bool HasRedPoint
	{
		get
		{
			UIActivityComeback_GetGift_Button uIActivityComeback_GetGift_Button = btn_getgift;
			if (uIActivityComeback_GetGift_Button == null || uIActivityComeback_GetGift_Button.redPoint?.selectedIndex != 1)
			{
				UIActivityComeback_Questionnaire_Button uIActivityComeback_Questionnaire_Button = btn_Questionnaire;
				if (uIActivityComeback_Questionnaire_Button == null)
				{
					return false;
				}
				return uIActivityComeback_Questionnaire_Button.redPoint?.selectedIndex == 1;
			}
			return true;
		}
	}

	public void Init(int activityId)
	{
		ComebackParamsConfigure comebackParamsConfigure = StaticConfigure.Comeback?.ParamsDict?.GetValueOrDefault(1);
		if (comebackParamsConfigure == null)
		{
			Debug.LogError("[UIActivityComeback_Gift_Com] Comeback.ParamsDict[1] 为空，无法初始化");
			return;
		}
		_chestId = comebackParamsConfigure.ChestId;
		_questionnaireId = GetQuestionID(comebackParamsConfigure);
		language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 3);
		RepeatedField<int> heroIDs = comebackParamsConfigure.HeroIDs;
		_heroId0 = heroIDs.GetSafeByIndex(0);
		_heroId1 = heroIDs.GetSafeByIndex(1);
		_heroId2 = heroIDs.GetSafeByIndex(2);
		_heroId3 = heroIDs.GetSafeByIndex(3);
		if (heroIDs.Count > 4)
		{
			Debug.LogWarning($"[UIActivityComeback_Gift_Com] Comeback.HeroIDs 配置数量 {heroIDs.Count} 超出 4 个槽位，仅取前 4 个");
		}
	}

	private string GetQuestionID(ComebackParamsConfigure data)
	{
		if (data.QuestionnaireId != null)
		{
			int dataForLanguage = GameSettings.GetDataForLanguage(1, 2, 0, 3);
			if (data.QuestionnaireId.Count > dataForLanguage)
			{
				return data.QuestionnaireId[dataForLanguage];
			}
		}
		return "";
	}

	public void AddEvent()
	{
		if (gift_item != null)
		{
			gift_item.onClick.Add(OnGiftItemClick);
		}
		if (btn_getgift != null)
		{
			btn_getgift.onClick.Add(OnGetGiftClick);
		}
		if (btn_Questionnaire != null)
		{
			btn_Questionnaire.onClick.Add(OnQuestionnaireClick);
		}
		if (hero1 != null)
		{
			hero1.onClick.Add(OnHero1Click);
		}
		if (hero2 != null)
		{
			hero2.onClick.Add(OnHero2Click);
		}
		if (hero3 != null)
		{
			hero3.onClick.Add(OnHero3Click);
		}
		if (hero4 != null)
		{
			hero4.onClick.Add(OnHero4Click);
		}
	}

	public void RemoveEvent()
	{
		gift_item?.onClick.Remove(OnGiftItemClick);
		btn_getgift?.onClick.Remove(OnGetGiftClick);
		btn_Questionnaire?.onClick.Remove(OnQuestionnaireClick);
		hero1?.onClick.Remove(OnHero1Click);
		hero2?.onClick.Remove(OnHero2Click);
		hero3?.onClick.Remove(OnHero3Click);
		hero4?.onClick.Remove(OnHero4Click);
	}

	public void OnShow()
	{
		RefreshGift();
		RefreshPrivilege();
		RefreshQuestionnaire();
	}

	public void RefreshRedPoints()
	{
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (btn_getgift != null && btn_getgift.redPoint != null)
		{
			btn_getgift.redPoint.selectedIndex = ((returnInfo != null && !returnInfo.FreeGiftClaimed) ? 1 : 0);
		}
		if (btn_Questionnaire != null && btn_Questionnaire.redPoint != null)
		{
			btn_Questionnaire.redPoint.selectedIndex = ((returnInfo != null && ComputeQuestionnaireState(returnInfo) == 1) ? 1 : 0);
		}
	}

	public void AddListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.AddListener(OnInfoUpdated);
			comeback.signal.freeGiftClaimed.AddListener(OnFreeGiftClaimed);
			comeback.signal.surveyStateUpdated.AddListener(OnSurveyStateUpdated);
		}
	}

	public void RemoveListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.RemoveListener(OnInfoUpdated);
			comeback.signal.freeGiftClaimed.RemoveListener(OnFreeGiftClaimed);
			comeback.signal.surveyStateUpdated.RemoveListener(OnSurveyStateUpdated);
		}
	}

	public void ClearData()
	{
		_chestId = 0;
		_heroId0 = (_heroId1 = (_heroId2 = (_heroId3 = 0)));
		_questionnaireId = null;
	}

	public override void Dispose()
	{
		_chestId = 0;
		_heroId0 = (_heroId1 = (_heroId2 = (_heroId3 = 0)));
		_questionnaireId = null;
		base.Dispose();
	}

	private void RefreshGift()
	{
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		ItemInfoConfigure itemInfoConfigure = _chestId.GetItemInfoConfigure();
		if (itemInfoConfigure != null)
		{
			if (gift_item != null && gift_item.gitf_loader != null)
			{
				gift_item.gitf_loader.url = itemInfoConfigure.ShowIcon;
			}
			if (gift_name != null)
			{
				gift_name.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
			}
		}
		if (btn_getgift != null)
		{
			bool flag = returnInfo?.FreeGiftClaimed ?? false;
			Controller getType = btn_getgift.getType;
			if (getType != null)
			{
				getType.selectedIndex = (flag ? 1 : 0);
			}
			btn_getgift.touchable = !flag && returnInfo != null;
		}
	}

	private void RefreshPrivilege()
	{
		if (comeback_desc != null)
		{
			comeback_desc.text = 1131.GetLocal(UIStringType.Message);
		}
		ApplyHeroSlot(hero1, _heroId0);
		ApplyHeroSlot(hero2, _heroId1);
		ApplyHeroSlot(hero3, _heroId2);
		ApplyHeroSlot(hero4, _heroId3);
	}

	private void ApplyHeroSlot(UIActivityComeback_Hero_Button hero, int heroId)
	{
		if (hero == null)
		{
			return;
		}
		if (heroId == 0)
		{
			ClearHeroTexts(hero);
			hero.visible = false;
			return;
		}
		SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem = SimpleSingletonProvider<GameLogicManager>.inst.heroCard?.GetConfigStandingPainting(heroId, 0, 0);
		if (skinStandingPaintingConfigureItem != null)
		{
			if (hero.hero_loader != null)
			{
				hero.hero_loader.url = skinStandingPaintingConfigureItem.GetCharacterReady();
			}
			if (hero.hero_name != null)
			{
				hero.hero_name.text = CharacterHandle.GetCharacterName(heroId);
			}
			if (hero.hero_title != null)
			{
				hero.hero_title.text = CharacterHandle.GetCharacterNickName(heroId);
			}
			hero.visible = true;
		}
		else
		{
			ClearHeroTexts(hero);
			hero.visible = false;
		}
	}

	private void ClearHeroTexts(UIActivityComeback_Hero_Button hero)
	{
		if (hero != null)
		{
			if (hero.hero_name != null)
			{
				hero.hero_name.text = string.Empty;
			}
			if (hero.hero_title != null)
			{
				hero.hero_title.text = string.Empty;
			}
		}
	}

	private void RefreshQuestionnaire()
	{
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (returnInfo != null && btn_Questionnaire != null)
		{
			int num = ComputeQuestionnaireState(returnInfo);
			Controller controller = btn_Questionnaire.GetController("state");
			if (controller != null)
			{
				controller.selectedIndex = num;
			}
			if (num == 0 && btn_Questionnaire.time != null)
			{
				int num2 = ComputeHoursUntilUnlock(returnInfo);
				btn_Questionnaire.time.SetVar("time", num2.ToString()).FlushVars();
			}
			btn_Questionnaire.touchable = num == 1;
		}
	}

	private int ComputeQuestionnaireState(ReturnInfo info)
	{
		if (info == null)
		{
			return -1;
		}
		long num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		long num2 = info.TriggerTime + 86400;
		if (num < num2)
		{
			return 0;
		}
		return info.SurveyState;
	}

	private int ComputeHoursUntilUnlock(ReturnInfo info)
	{
		if (info == null)
		{
			return 0;
		}
		long num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		long num2 = info.TriggerTime + 86400 - num;
		if (num2 <= 0)
		{
			return 1;
		}
		return Mathf.Max(1, Mathf.CeilToInt((float)num2 / 3600f));
	}

	private async void OnGiftItemClick()
	{
		if (gift_item == null)
		{
			return;
		}
		gift_item.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(_chestId, 1, _Usable: true);
		}
		finally
		{
			gift_item.onClick.Release();
		}
	}

	private async void OnGetGiftClick()
	{
		if (btn_getgift == null)
		{
			return;
		}
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (returnInfo == null || returnInfo.FreeGiftClaimed)
		{
			return;
		}
		btn_getgift.onClick.Retain();
		try
		{
			RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.comeback.RequestFreeGift();
			if (rPCAsyncResult != null)
			{
				await rPCAsyncResult;
			}
		}
		finally
		{
			btn_getgift.onClick.Release();
		}
	}

	private async void OnHero1Click()
	{
		if (hero1 == null || _heroId0 == 0)
		{
			return;
		}
		hero1.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Hero, _heroId0);
		}
		finally
		{
			hero1.onClick.Release();
		}
	}

	private async void OnHero2Click()
	{
		if (hero2 == null || _heroId1 == 0)
		{
			return;
		}
		hero2.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Hero, _heroId1);
		}
		finally
		{
			hero2.onClick.Release();
		}
	}

	private async void OnHero3Click()
	{
		if (hero3 == null || _heroId2 == 0)
		{
			return;
		}
		hero3.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Hero, _heroId2);
		}
		finally
		{
			hero3.onClick.Release();
		}
	}

	private async void OnHero4Click()
	{
		if (hero4 == null || _heroId3 == 0)
		{
			return;
		}
		hero4.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Hero, _heroId3);
		}
		finally
		{
			hero4.onClick.Release();
		}
	}

	private async void OnQuestionnaireClick()
	{
		if (btn_Questionnaire == null)
		{
			return;
		}
		ReturnInfo info = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (ComputeQuestionnaireState(info) != 1)
		{
			return;
		}
		btn_Questionnaire.onClick.Retain();
		try
		{
			if (!string.IsNullOrEmpty(_questionnaireId))
			{
				Application.OpenURL(_questionnaireId);
			}
			RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.comeback.RequestSurveyFinish();
			if (rPCAsyncResult != null)
			{
				await rPCAsyncResult;
			}
		}
		finally
		{
			btn_Questionnaire.onClick.Release();
		}
	}

	private void OnInfoUpdated(ReturnInfo _)
	{
		OnShow();
	}

	private void OnFreeGiftClaimed(bool _)
	{
		RefreshGift();
	}

	private void OnSurveyStateUpdated(int _)
	{
		RefreshQuestionnaire();
	}

	public static UIActivityComeback_Gift_Com CreateInstance()
	{
		return (UIActivityComeback_Gift_Com)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Gift_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		hero4 = (UIActivityComeback_Hero_Button)GetChildAt(0);
		hero3 = (UIActivityComeback_Hero_Button)GetChildAt(1);
		hero2 = (UIActivityComeback_Hero_Button)GetChildAt(2);
		hero1 = (UIActivityComeback_Hero_Button)GetChildAt(3);
		btn_getgift = (UIActivityComeback_GetGift_Button)GetChildAt(9);
		gift_item = (UIActivityComeback_Gift_Item)GetChildAt(10);
		gift_name = (GTextField)GetChildAt(12);
		loader_question = (GLoader)GetChildAt(15);
		btn_Questionnaire = (UIActivityComeback_Questionnaire_Button)GetChildAt(16);
		comeback_desc = (GTextField)GetChildAt(18);
		Cut_in = GetTransitionAt(0);
	}
}

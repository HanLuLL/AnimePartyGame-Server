using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Tutorial;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class RelicWindow : BaseWindow
{
	public enum RecommendedType
	{
		NotRecommended,
		Recommended,
		ParticularlyRecommend
	}

	private UIRelic_Button_Item CurSelectRelicItem;

	private List<(int score, RecommendedType recommendedType)> relicScores = new List<(int, RecommendedType)>();

	private RepeatedField<int> vailRelics;

	private party.model.Action action;

	private readonly string[] ProbabilityColor = new string[3] { "#2685F9", "#FF37FF", "#F5A43E" };

	private List<GGraph> EffectGraphs = new List<GGraph>();

	private int reRelicCount => SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().Property.reRelicCount.Value;

	public RelicWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRelicWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIRelicWindow uIRelicWindow)
		{
			uIRelicWindow.list_Relic.itemRenderer = RendererRelicItem;
			uIRelicWindow.btn_Select.onClick.Add(SelectRelic);
			uIRelicWindow.btn_Reset.onClick.Add(ResetRelic);
			uIRelicWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uIRelicWindow.Hide.selectedIndex = 0;
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				uIRelicWindow.btn_Reset.visible = false;
			}
			uIRelicWindow.Hide.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (!(base.contentPane is UIRelicWindow uIRelicWindow))
		{
			return;
		}
		uIRelicWindow.btn_Select.onClick.Remove(SelectRelic);
		uIRelicWindow.btn_Reset.onClick.Remove(ResetRelic);
		vailRelics = null;
		action = null;
		foreach (GGraph effectGraph in EffectGraphs)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectGraph);
		}
		EffectGraphs.Clear();
		uIRelicWindow.Hide.onChanged.Remove(OnHideControllerChange);
	}

	private async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private void OnHideControllerChange()
	{
	}

	public async void ShowRelicData(party.model.Action _action, SelectRelicC2S _relicData)
	{
		if (!base.isShowing || action == null || action.Sn != _action.Sn)
		{
			await TryShow();
			action = _action;
			vailRelics = _relicData.Relics;
			if (base.contentPane is UIRelicWindow uIRelicWindow)
			{
				RefreshSelectButton(vail: false);
				ShowProbability(_relicData.Lv, uIRelicWindow.txt_Probability);
				RecommendRelic(vailRelics);
				uIRelicWindow.list_Relic.numItems = vailRelics.Count;
				uIRelicWindow.btn_Reset.txt_Count.SetVar("count", reRelicCount.ToString()).FlushVars();
				uIRelicWindow.btn_Reset.visible = reRelicCount > 0;
				uIRelicWindow.btn_Select.onClick.Release();
				uIRelicWindow.btn_Reset.onClick.Release();
				OperationTimer.ActionDownTime(action.Sn, 5211, OnCompleteRelic, null, null, operateCard: false, showTimerToPlayer: false);
				relicScores.Clear();
			}
		}
	}

	private void RefreshSelectButton(bool vail)
	{
		if (base.contentPane is UIRelicWindow uIRelicWindow)
		{
			uIRelicWindow.btn_Select.grayed = !vail;
			uIRelicWindow.btn_Select.touchable = vail;
			uIRelicWindow.btn_Select.status.selectedIndex = (vail ? 1 : 0);
		}
	}

	private void ShowProbability(int CostLevel, GTextField txt_Probability)
	{
		string text = "";
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			RepeatedField<UpgradeDataConfigureItem> upgradeDataConfigureItems = StaticConfigure.Upgrade.DataDict[curRoomInfo.UpgradePlan].UpgradeDataConfigureItems;
			for (int i = 0; i < upgradeDataConfigureItems.Count; i++)
			{
				if (upgradeDataConfigureItems[i].CostEnhance != CostLevel)
				{
					continue;
				}
				RepeatedField<int> relicWeights = upgradeDataConfigureItems[i].RelicWeights;
				int num = 0;
				foreach (int item in relicWeights)
				{
					num += item;
				}
				for (int j = 0; j < relicWeights.Count; j++)
				{
					string arg = ProbabilityColor[j % ProbabilityColor.Length];
					float num2 = (float)relicWeights[j] * 1f / (float)num;
					text = text + $"[color={arg}]{num2:P0}[/color]" + ((j == relicWeights.Count - 1) ? "" : "/");
				}
				break;
			}
		}
		txt_Probability.text = string.Format(100.GetLocal(UIStringType.Relic), text);
	}

	private void OnCompleteRelic()
	{
		if (base.contentPane is UIRelicWindow uIRelicWindow)
		{
			GObject childAt = uIRelicWindow.list_Relic.GetChildAt(0);
			if (vailRelics != null && vailRelics.Count > 0 && childAt is UIRelic_Button_Item curSelectRelicItem)
			{
				CurSelectRelicItem = curSelectRelicItem;
				uIRelicWindow.btn_Select.onClick.Call();
			}
		}
	}

	private void RendererRelicItem(int index, GObject item)
	{
		UIRelic_Button_Item btn = item as UIRelic_Button_Item;
		if (btn == null)
		{
			return;
		}
		RelicInfoConfigure relicConfig = vailRelics[index].GetRelicInfoConfigure();
		btn.txt_tltle.text = relicConfig.NameID.GetLocal(UIStringType.Relic);
		btn.txt_Desc.text = relicConfig.DescID.GetLocal(UIStringType.Relic);
		((UICom_RelicQuality_Large)btn.loader_Frame).qualityType.selectedIndex = (int)relicConfig.RelicQualityType;
		btn.loader_Icon.url = relicConfig.Icon;
		btn.status.selectedIndex = 0;
		btn.data = index;
		TryShowRelicGoldEffect(relicConfig.RelicQualityType, btn.graph_Gold);
		btn.cutIn.Play();
		btn.StarCutin.Play();
		btn.onClick.Set((EventCallback0)delegate
		{
			btn.onClick.Retain();
			if (CurSelectRelicItem != null && CurSelectRelicItem != btn)
			{
				CurSelectRelicItem.status.selectedIndex = 0;
			}
			CurSelectRelicItem = btn;
			CurSelectRelicItem.status.selectedIndex = 1;
			RefreshKeywords(relicConfig.KeyWordType, btn);
			RefreshSelectButton(vail: true);
			btn.onClick.Release();
		});
		RefreshRelicRecommend(index, btn);
	}

	private void TryShowRelicGoldEffect(RelicQualityType qualityType, GGraph graph)
	{
		if (qualityType == RelicQualityType.Orange)
		{
			EffectGraphs.Add(graph);
			if (StaticConfigure.Effect.InfoDict.TryGetValue(106, out var value))
			{
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, graph, 11f).Forget();
			}
		}
	}

	private void RefreshRelicRecommend(int index, UIRelic_Button_Item item)
	{
		item.RecommendType.selectedIndex = 0;
		if (!GameSettings.RelicSuggest)
		{
			return;
		}
		if (relicScores.Count(((int score, RecommendedType recommendedType) tuple) => tuple.recommendedType == RecommendedType.ParticularlyRecommend) >= 1 && relicScores.GetSafeByIndex(index).recommendedType == RecommendedType.ParticularlyRecommend)
		{
			item.RecommendType.selectedIndex = 2;
		}
		else if (relicScores.Count(((int score, RecommendedType recommendedType) tuple) => tuple.recommendedType == RecommendedType.Recommended) >= 1)
		{
			int num = relicScores.Max(((int score, RecommendedType recommendedType) r) => r.score);
			if (relicScores.GetSafeByIndex(index).score == num)
			{
				item.RecommendType.selectedIndex = 1;
			}
		}
		else
		{
			item.RecommendType.selectedIndex = 0;
		}
	}

	private void RefreshKeywords(RelicKeyWordType KeyWordType, UIRelic_Button_Item relicItem)
	{
		if (StaticConfigure.Relic.KeywordsDict.TryGetValue((int)KeyWordType, out var value))
		{
			((UICom_RelicKeyword)relicItem.com_Keyword).Refresh(value);
		}
		else
		{
			relicItem.com_Keyword.visible = false;
		}
	}

	private void SelectRelic()
	{
		if (CurSelectRelicItem != null && CurSelectRelicItem.data is int num && base.contentPane is UIRelicWindow uIRelicWindow)
		{
			uIRelicWindow.btn_Select.onClick.Retain();
			uIRelicWindow.btn_Reset.onClick.Retain();
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				List<int> relicIds = new List<int>(vailRelics);
				TutorialGame.GetSystem<TutorialBoardManager>().relicManager.SelectRelic(action.PlayerId, relicIds, num).Forget();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.relic.RequestSelectRelicC2S(action.Sn, num);
			}
		}
	}

	private void ResetRelic()
	{
		if (reRelicCount <= 0 || !(base.contentPane is UIRelicWindow uIRelicWindow))
		{
			return;
		}
		uIRelicWindow.btn_Select.onClick.Retain();
		uIRelicWindow.btn_Reset.onClick.Retain();
		foreach (GGraph effectGraph in EffectGraphs)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(effectGraph);
		}
		EffectGraphs.Clear();
		SimpleSingletonProvider<GameLogicManager>.inst.relic.RequestResetRelic(action.Sn);
	}

	private void RecommendRelic(RepeatedField<int> relic)
	{
		relicScores.Clear();
		RelicParamsConfigure safeByIndex = StaticConfigure.Relic.Paramss.GetSafeByIndex(0);
		for (int i = 0; i < relic.Count; i++)
		{
			int index = i;
			int num = RecommendedScore(relic[i], index);
			RecommendedType item = ((num < safeByIndex.RelicRecSLimitScore) ? ((num >= safeByIndex.RelicRecLimitScore) ? RecommendedType.Recommended : RecommendedType.NotRecommended) : RecommendedType.ParticularlyRecommend);
			relicScores.Add((num, item));
		}
	}

	private int RecommendedScore(int relicId, int index)
	{
		RelicInfoConfigure relicInfoConfigure = relicId.GetRelicInfoConfigure();
		if (relicInfoConfigure == null)
		{
			return 0;
		}
		int num = 0;
		int num2 = RelicBasicScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 基础推荐分：{num2}");
		int num3 = RelicCharacterMatchScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 角色适配推荐分：{num3}");
		int num4 = RelicCharacterNormalMatchScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 角色普通适配荐分：{num4}");
		int num5 = RelicGrowthMatchScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 成长型推荐分：{num5}");
		int num6 = RelicKeyWordsMatchScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 关键词推荐分：{num6}");
		int num7 = RelicTagMatchScore(relicInfoConfigure);
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} Tag推荐分：{num7}");
		num = num2 + num3 + num5 + num6 + num7 + num4;
		Debug.Log($"#筹码推荐分计算# Index:{index} Id:{relicId} 总推荐分：{num}");
		return num;
	}

	private int RelicKeyWordsMatchScore(RelicInfoConfigure relicInfoConfigure)
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (int relicId in selfPlayerData.GetRelicIds())
		{
			RelicInfoConfigure relicInfoConfigure2 = relicId.GetRelicInfoConfigure();
			if (relicInfoConfigure2 != null)
			{
				switch (relicInfoConfigure2.KeyWordType)
				{
				case RelicKeyWordType.Cure:
					num++;
					break;
				case RelicKeyWordType.Salary:
					num2++;
					break;
				case RelicKeyWordType.Mark:
					num3++;
					break;
				}
			}
		}
		RelicParamsConfigure safeByIndex = StaticConfigure.Relic.Paramss.GetSafeByIndex(0);
		switch (relicInfoConfigure.KeyWordType)
		{
		case RelicKeyWordType.Salary:
			if (num2 > 0)
			{
				return safeByIndex.RelicRecWordTypeScore;
			}
			break;
		case RelicKeyWordType.Mark:
			if (num3 > 0)
			{
				return safeByIndex.RelicRecWordTypeScore;
			}
			break;
		case RelicKeyWordType.Cure:
			if (num > 0)
			{
				return safeByIndex.RelicRecWordTypeScore;
			}
			break;
		}
		return 0;
	}

	private int RelicBasicScore(RelicInfoConfigure optionalRelicInfoConfigure)
	{
		if (optionalRelicInfoConfigure.RelicRecBaseScore != 0)
		{
			return optionalRelicInfoConfigure.RelicRecBaseScore;
		}
		RelicParamsConfigure safeByIndex = StaticConfigure.Relic.Paramss.GetSafeByIndex(0);
		return optionalRelicInfoConfigure.RelicQualityType switch
		{
			RelicQualityType.Blue => safeByIndex.RelicRecBlueScore, 
			RelicQualityType.Purple => safeByIndex.RelicRecPurpleScore, 
			RelicQualityType.Orange => safeByIndex.RelicRecOrangeScore, 
			RelicQualityType.None => 0, 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	private int RelicCharacterMatchScore(RelicInfoConfigure relicInfoConfigure)
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (relicInfoConfigure.RelicRecRole.Contains(selfPlayerData.player.Hero.HeroId))
		{
			return StaticConfigure.Relic.Paramss.GetSafeByIndex(0).RelicRecRoleScore;
		}
		return 0;
	}

	private int RelicCharacterNormalMatchScore(RelicInfoConfigure relicInfoConfigure)
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (relicInfoConfigure.RelicRecRoleNormal.Contains(selfPlayerData.player.Hero.HeroId))
		{
			return StaticConfigure.Relic.Paramss.GetSafeByIndex(0).RelicRecRoleNormalScore;
		}
		return 0;
	}

	private int RelicGrowthMatchScore(RelicInfoConfigure relicInfoConfigure)
	{
		if (relicInfoConfigure.RelicRecGrowth == 0)
		{
			return 0;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round <= relicInfoConfigure.RelicRecGrowth)
		{
			return StaticConfigure.Relic.Paramss.GetSafeByIndex(0).RelicRecGrowthScore;
		}
		return 0;
	}

	private int RelicTagMatchScore(RelicInfoConfigure optionalRelicInfoConfigure)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		foreach (int relicId in selfPlayerData.GetRelicIds())
		{
			RelicInfoConfigure relicInfoConfigure = relicId.GetRelicInfoConfigure();
			if (relicInfoConfigure != null)
			{
				num += relicInfoConfigure.AttackPt;
				num2 += relicInfoConfigure.CardPt;
				num3 += relicInfoConfigure.SupportPt;
				num4 += relicInfoConfigure.TankPt;
			}
		}
		if (!StaticConfigure.Relic.CharacterRelicDict.TryGetValue(selfPlayerData.player.Hero.HeroId, out var value))
		{
			Debug.LogError($"筹码推荐积分计算失败，无法找到角色：{selfPlayerData.player.Hero.HeroId}");
			return 0;
		}
		num += value.AttackPt;
		num2 += value.CardPt;
		num3 += value.SupportPt;
		num4 += value.TankPt;
		RelicParamsConfigure safeByIndex = StaticConfigure.Relic.Paramss.GetSafeByIndex(0);
		if (optionalRelicInfoConfigure.TagType.Contains(CharacterTagType.Attack) && num >= safeByIndex.RelicRecTagTypeNdPt)
		{
			return safeByIndex.RelicRecTagTypeScore;
		}
		if (optionalRelicInfoConfigure.TagType.Contains(CharacterTagType.Card) && num2 >= safeByIndex.RelicRecTagTypeNdPt)
		{
			return safeByIndex.RelicRecTagTypeScore;
		}
		if (optionalRelicInfoConfigure.TagType.Contains(CharacterTagType.Support) && num3 >= safeByIndex.RelicRecTagTypeNdPt)
		{
			return safeByIndex.RelicRecTagTypeScore;
		}
		if (optionalRelicInfoConfigure.TagType.Contains(CharacterTagType.Tank) && num4 >= safeByIndex.RelicRecTagTypeNdPt)
		{
			return safeByIndex.RelicRecTagTypeScore;
		}
		return 0;
	}
}

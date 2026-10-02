using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleSettlement_Com_Item : GComponent
{
	private PlayerSettlement _PlayerSettlement;

	private bool _RelicExpand;

	public Controller slot;

	public Controller IsWinner;

	public Controller ShowAchievement;

	public Controller step;

	public UIBattleSettlement_Com_ItemRole com_Role;

	public UIBattleSettlement_Com_AchieveBottom com_AchieveBottom;

	public GTextField txt_Slot;

	public UIBattleSettlement_Button_OperatePlayer btn_Praise;

	public UIBattleSettlement_Button_OperatePlayer btn_AddFriend;

	public UIBattleSettlement_Com_BattleData com_BattleData;

	public UIBattleSettlement_Com_AchievementDetail com_Achievement;

	public GComponent com_PlayerLabel;

	public UIBattleSettlement_Button_AchieveItem loader_Achieve_3;

	public UIBattleSettlement_Button_AchieveItem loader_Achieve_2;

	public UIBattleSettlement_Button_AchieveItem loader_Achieve_1;

	public Transition SJ_Cut_in;

	public Transition LH_Cut_in;

	public Transition CJ_Cut_in;

	public Transition Winer;

	public const string URL = "ui://avgradidqees2o";

	public void Refresh(PlayerSettlement playerSettlement, bool ShowPVP)
	{
		_PlayerSettlement = playerSettlement;
		base.visible = true;
		step.selectedIndex = 0;
		IsWinner.selectedIndex = ((playerSettlement.IsWinner && ShowPVP) ? 1 : 0);
		BattlePlayerData playerData = playerSettlement.PlayerData;
		slot.selectedIndex = Mathf.Min(playerData.player.Slot, 3);
		com_AchieveBottom.slot.selectedIndex = Mathf.Min(playerData.player.Slot, 3);
		com_Role.loader_Role.url = playerData.player.standingPainting.GetCharacterReady();
		RefreshAchieve(loader_Achieve_1, playerSettlement.GetAchieve(0));
		RefreshAchieve(loader_Achieve_2, playerSettlement.GetAchieve(1));
		RefreshAchieve(loader_Achieve_3, playerSettlement.GetAchieve(2));
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id);
		btn_Praise.isFinish.selectedIndex = ((flag || playerData.player.IsBot) ? 1 : 0);
		if (!flag && !playerData.player.IsBot)
		{
			bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(playerData.player.Id);
			btn_AddFriend.operateType.selectedIndex = ((!flag2) ? 1 : 2);
			btn_AddFriend.isFinish.selectedIndex = (flag2 ? 1 : 0);
		}
		else
		{
			btn_AddFriend.isFinish.selectedIndex = 1;
		}
		btn_AddFriend.onClick.Set((EventCallback0)delegate
		{
			if (btn_AddFriend.isFinish.selectedIndex != 1)
			{
				btn_AddFriend.onClick.Retain();
				btn_AddFriend.isFinish.selectedIndex = 1;
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendApplyC2S(playerData.player.Id);
			}
		});
		if (ShowPVP)
		{
			com_BattleData.type.selectedIndex = 0;
			com_BattleData.com_PVP.rand.selectedIndex = Mathf.Min(playerData.rank, 3);
			com_BattleData.com_PVP.slot.selectedIndex = Mathf.Min(playerData.player.Slot, 3);
			com_BattleData.com_PVP.list_Level.itemRenderer = delegate(int index, GObject star)
			{
				if (star is UICom_PlayerLevel uICom_PlayerLevel)
				{
					uICom_PlayerLevel.slot.selectedIndex = playerData.player.Slot;
					uICom_PlayerLevel.avtiveLevel.selectedIndex = ((playerData.Property.level.Value > index) ? 1 : 0);
				}
			};
			com_BattleData.com_PVP.list_Level.numItems = 3;
			com_BattleData.com_PVP.txt_GoldCount.text = playerSettlement.GetBattleData(FinishAchieveType.ResultGoldPvp).ToString();
		}
		else
		{
			com_BattleData.type.selectedIndex = 1;
			com_BattleData.com_PVE.txt_GoldCount.text = playerSettlement.GetBattleData(FinishAchieveType.GetAccrueGoldPve).ToString();
			com_BattleData.com_PVE.txt_TranGoldCount.text = playerSettlement.GetBattleData(FinishAchieveType.GiveAccrueGoldPve).ToString();
			List<int> relicIds = playerSettlement.PlayerData.GetRelicIds();
			com_BattleData.com_PVE.com_Relic.list_RelicGroup.itemRenderer = delegate(int index, GObject item)
			{
				int num = index * 7;
				if (item is UIBattleSettlement_Com_RelicGroup uIBattleSettlement_Com_RelicGroup)
				{
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_1, (relicIds.Count > num) ? relicIds[num] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_2, (relicIds.Count > num + 1) ? relicIds[num + 1] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_3, (relicIds.Count > num + 2) ? relicIds[num + 2] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_4, (relicIds.Count > num + 3) ? relicIds[num + 3] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_5, (relicIds.Count > num + 4) ? relicIds[num + 4] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_6, (relicIds.Count > num + 5) ? relicIds[num + 5] : 0);
					RendererRelicItem(uIBattleSettlement_Com_RelicGroup.btn_RelicItem_7, (relicIds.Count > num + 6) ? relicIds[num + 6] : 0);
				}
			};
			com_BattleData.com_PVE.com_Relic.list_RelicGroup.numItems = (int)Mathf.Max((float)relicIds.Count / 7f + 1f, 3f);
			com_BattleData.com_PVE.com_Relic.btn_ChangeWin.onClick.Set((EventCallback0)delegate
			{
				com_BattleData.com_PVE.com_Relic.btn_ChangeWin.onClick.Retain();
				_RelicExpand = !_RelicExpand;
				if (_RelicExpand)
				{
					com_BattleData.com_PVE.Expand.Play();
				}
				else
				{
					com_BattleData.com_PVE.Folder.Play();
				}
				com_BattleData.com_PVE.com_Relic.btn_ChangeWin.onClick.Release();
			});
		}
		LH_Cut_in.SetHook("ShowRole", delegate
		{
			step.selectedIndex = 0;
			Winer.Play();
		});
		CJ_Cut_in.SetHook("ShowAchievement", delegate
		{
			step.selectedIndex = 1;
		});
		SJ_Cut_in.SetHook("ShowData", delegate
		{
			ShowAchievement.selectedIndex = 0;
			IsWinner.selectedIndex = 0;
			step.selectedIndex = 2;
		});
	}

	public void RefreshBattleData(Dictionary<int, int> BattleTotalData, Dictionary<int, int> maxData)
	{
		RefreshBattleDataItem(3001, 0, com_BattleData.com_killCount, FinishAchieveType.KillCount, BattleTotalData, maxData);
		RefreshBattleDataItem(3002, 1, com_BattleData.com_TotalDie, FinishAchieveType.TotalDie, BattleTotalData, maxData);
		RefreshBattleDataItem(3003, 2, com_BattleData.com_TotalDamage, FinishAchieveType.TotalDamage, BattleTotalData, maxData);
		RefreshBattleDataItem(3004, 3, com_BattleData.com_TotalInjured, FinishAchieveType.TotalInjured, BattleTotalData, maxData);
		RefreshBattleDataItem(3005, 4, com_BattleData.com_TreatmentScore, FinishAchieveType.RestoreScoreMax, BattleTotalData, maxData);
	}

	private void RefreshBattleDataItem(int achieveId, int index, UIBattleSettlement_Com_BattleDataItem com_Item, FinishAchieveType type, Dictionary<int, int> BattleTotalData, Dictionary<int, int> maxData)
	{
		com_Item.Cut_in.Play(1, 0.01f * (float)index, null, null);
		int battleData = _PlayerSettlement.GetBattleData(type);
		int num = Mathf.Abs(maxData.GetValueOrDefault((int)type));
		com_Item.title = achieveId.GetLocal(UIStringType.Achieve);
		com_Item.txt_Count.text = battleData.ToString();
		com_Item.slider_Data.min = 0.0;
		com_Item.slider_Data.max = Mathf.Max(1, BattleTotalData.GetValueOrDefault((int)type));
		com_Item.slider_Data.value = battleData;
		com_Item.type.selectedIndex = ((num != 0 && num == battleData) ? 1 : 0);
	}

	private void RendererRelicItem(UIBattleSettlement_Button_Relic relicItem, int relicId)
	{
		if (relicId != 0)
		{
			RelicInfoConfigure relicInfoConfigure = relicId.GetRelicInfoConfigure();
			((UICom_Relic_Quality)relicItem.com_Quality).quality.selectedIndex = (int)relicInfoConfigure.RelicQualityType;
			relicItem.data = relicInfoConfigure.Id;
			relicItem.touchable = true;
			relicItem.loader_Relic.url = relicInfoConfigure.Icon;
			relicItem.loader_Relic.visible = true;
		}
	}

	private void RefreshAchieve(UIBattleSettlement_Button_AchieveItem loader, (int, int) Achieve)
	{
		bool flag;
		if (Achieve.Item1 == 0)
		{
			UIBattleSettlement_Button_AchieveItem uIBattleSettlement_Button_AchieveItem = loader;
			flag = (loader.visible = false);
			uIBattleSettlement_Button_AchieveItem.touchable = flag;
			return;
		}
		UIBattleSettlement_Button_AchieveItem uIBattleSettlement_Button_AchieveItem2 = loader;
		flag = (loader.visible = true);
		uIBattleSettlement_Button_AchieveItem2.touchable = flag;
		AchieveInfoConfigure achieveConfig = Achieve.Item1.GetAchieveConfig();
		loader.icon = achieveConfig.AchievePicSettlement;
		GTextField title_Down = loader.title_Down;
		string text = (loader.title_Up.text = Mathf.Abs(Achieve.Item2).ToString());
		title_Down.text = text;
		GTextField title_Down2 = loader.title_Down;
		flag = (loader.title_Up.visible = achieveConfig.FinishAchieveType != FinishAchieveType.Winner);
		title_Down2.visible = flag;
		loader.onClick.Set((EventCallback0)delegate
		{
			loader.onClick.Retain();
			ShowAchievementInfo(achieveConfig, Achieve.Item2);
			loader.onClick.Release();
		});
	}

	public void CloseAchievementInfo()
	{
		if (ShowAchievement.selectedIndex == 0)
		{
			return;
		}
		if (step.selectedIndex == 2)
		{
			com_Achievement.Cut_in.Stop();
			com_Achievement.Cut_out.Play(delegate
			{
				ShowAchievement.selectedIndex = 0;
			});
		}
		else
		{
			com_Achievement.CJ_Cut_in.Stop();
			com_Achievement.CJ_Cut_out.Play(delegate
			{
				ShowAchievement.selectedIndex = 0;
			});
		}
	}

	private void ShowAchievementInfo(AchieveInfoConfigure achieveConfig, int achieveValue)
	{
		if (step.selectedIndex == 2)
		{
			com_Achievement.Cut_out.Stop();
			com_Achievement.Cut_in.Play();
		}
		else
		{
			com_Achievement.CJ_Cut_out.Stop();
			com_Achievement.CJ_Cut_in.Play();
		}
		ShowAchievement.selectedIndex = 1;
		com_Achievement.loader_Achieve.url = achieveConfig.AchievePicSettlement;
		com_Achievement.txt_Name.text = achieveConfig.AchievedescId.GetLocal(UIStringType.Achieve);
		com_Achievement.txt_Desc.text = achieveConfig.DescId.GetLocal(UIStringType.Achieve);
		com_Achievement.txt_Value.visible = achieveConfig.FinishAchieveType != FinishAchieveType.Winner;
		com_Achievement.txt_Value.text = Mathf.Abs(achieveValue).ToString();
	}

	private void RendererSkin(string url)
	{
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(url, out var value))
		{
			customOffset = value.skinOffset;
		}
		if (!(com_Role.loader_Role.url == url))
		{
			com_Role.loader_Role.customOffset = customOffset;
			com_Role.loader_Role.customScale = Vector2.one;
			com_Role.loader_Role.url = url;
		}
	}

	public void RefreshPlaceholderBot(GameModeNPCPlayerConfigure npcPlayerConfigure)
	{
		MonsterInfoConfigure monsterInfoConfigure = npcPlayerConfigure.MonsterId.GetMonsterInfoConfigure();
		if (monsterInfoConfigure != null)
		{
			SkinStandingPaintingConfigureItem defeatStandingPaintingById = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetDefeatStandingPaintingById(monsterInfoConfigure.StandingPainting);
			if (defeatStandingPaintingById != null)
			{
				step.selectedIndex = 0;
				IsWinner.selectedIndex = 0;
				ShowAchievement.selectedIndex = 0;
				com_Role.loader_Role.url = defeatStandingPaintingById.GetCharacterReady();
			}
		}
	}

	public static UIBattleSettlement_Com_Item CreateInstance()
	{
		return (UIBattleSettlement_Com_Item)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		slot = GetControllerAt(0);
		IsWinner = GetControllerAt(1);
		ShowAchievement = GetControllerAt(2);
		step = GetControllerAt(3);
		com_Role = (UIBattleSettlement_Com_ItemRole)GetChildAt(5);
		com_AchieveBottom = (UIBattleSettlement_Com_AchieveBottom)GetChildAt(6);
		txt_Slot = (GTextField)GetChildAt(9);
		btn_Praise = (UIBattleSettlement_Button_OperatePlayer)GetChildAt(11);
		btn_AddFriend = (UIBattleSettlement_Button_OperatePlayer)GetChildAt(12);
		com_BattleData = (UIBattleSettlement_Com_BattleData)GetChildAt(13);
		com_Achievement = (UIBattleSettlement_Com_AchievementDetail)GetChildAt(14);
		com_PlayerLabel = (GComponent)GetChildAt(15);
		loader_Achieve_3 = (UIBattleSettlement_Button_AchieveItem)GetChildAt(16);
		loader_Achieve_2 = (UIBattleSettlement_Button_AchieveItem)GetChildAt(17);
		loader_Achieve_1 = (UIBattleSettlement_Button_AchieveItem)GetChildAt(18);
		SJ_Cut_in = GetTransitionAt(0);
		LH_Cut_in = GetTransitionAt(1);
		CJ_Cut_in = GetTransitionAt(2);
		Winer = GetTransitionAt(3);
	}
}

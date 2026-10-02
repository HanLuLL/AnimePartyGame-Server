using System.Collections.Generic;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class UIWatch_Com_Player : GComponent
{
	public Controller p;

	public Controller isSelf;

	public Controller isChoose;

	public Controller GameMode;

	public GLoader loader_Character;

	public GGraph loader_Animation;

	public GTextField txt_progress;

	public UIWatch_Button_DetailInfo btn_DetailInfo;

	public GComponent com_PlayerLabel;

	public GImage YOU;

	public GGroup group_Ok;

	public GTextField txt_PVELV;

	public const string URL = "ui://sv6gwhbej6om1";

	private RoomPlayer playerData;

	public CharacterInfoConfigure characterConfig;

	public static UIWatch_Com_Player CreateInstance()
	{
		return (UIWatch_Com_Player)UIPackage.CreateObject("Watch", "Watch_Com_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		isSelf = GetControllerAt(1);
		isChoose = GetControllerAt(2);
		GameMode = GetControllerAt(3);
		loader_Character = (GLoader)GetChildAt(3);
		loader_Animation = (GGraph)GetChildAt(4);
		txt_progress = (GTextField)GetChildAt(5);
		btn_DetailInfo = (UIWatch_Button_DetailInfo)GetChildAt(6);
		com_PlayerLabel = (GComponent)GetChildAt(7);
		YOU = (GImage)GetChildAt(8);
		group_Ok = (GGroup)GetChildAt(13);
		txt_PVELV = (GTextField)GetChildAt(16);
	}

	public void InitData(int index, RoomPlayer player, int MapType)
	{
		playerData = player;
		p.selectedIndex = index;
		isChoose.selectedIndex = 0;
		txt_progress.visible = false;
		txt_progress.SetVar("progress", "0").FlushVars();
		btn_DetailInfo.touchable = false;
		com_PlayerLabel.visible = true;
		GameMode.selectedIndex = (BattleConfig.IsPVE(MapType) ? 1 : 0);
		if (playerData != null)
		{
			RefreshPlayerLabel();
			RefreshDetailInfo(playerData.Hero.HeroId, playerData.PVEHeroLV, player.GetTalentId());
		}
		if (CheckPlaceholder(index, MapType))
		{
			RefreshPlaceholder(MapType.GetGameModeNPCPlayerConfigure());
		}
	}

	public void RefreshDetailInfo(int HeroId, int PveHeroLv, int talentId)
	{
		if (HeroId > 0)
		{
			txt_PVELV.text = PveHeroLv.ToString();
			characterConfig = CharacterHandle.GetHeroCharacterConfigure(HeroId);
			loader_Character.url = playerData.standingPainting.GetCharacterReady();
			isChoose.selectedIndex = 1;
			btn_DetailInfo.txt_CharaterName.text = CharacterHandle.GetCharacterName(HeroId);
			btn_DetailInfo.txt_CharaterNick.text = CharacterHandle.GetCharacterNickName(HeroId);
			btn_DetailInfo.txt_HP.text = characterConfig.Blood.ToString();
			btn_DetailInfo.txt_ATK.text = characterConfig.Attack.ToString();
			btn_DetailInfo.txt_DEF.text = characterConfig.Defense.ToString();
			int battleActiveSkillId = CharacterHandle.GetBattleActiveSkillId(HeroId, CharacterType.Hero, talentId);
			RepeatedField<int> battlePassiveSkills = CharacterHandle.GetBattlePassiveSkills(HeroId, CharacterType.Hero, talentId);
			List<SkillInfoConfigure> skillInfos = UIHelper.GetSkillConfigs(battleActiveSkillId, battlePassiveSkills);
			btn_DetailInfo.list_Skill.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIWatch_Com_Skill uIWatch_Com_Skill && index >= 0 && index <= skillInfos.Count - 1)
				{
					uIWatch_Com_Skill.skillType.selectedIndex = ((skillInfos[index].SkillType != SkillType.Active) ? 1 : 0);
					if (skillInfos[index].SkillType == SkillType.Active)
					{
						btn_DetailInfo.txt_CD.text = skillInfos[index].Round.ToString();
					}
					uIWatch_Com_Skill.txt_SkillName.SetVar("skillName", skillInfos[index].NameID.GetLocal(UIStringType.Skill)).FlushVars();
					skillInfos[index].DescID.RefreshCharacterSkillHyperlinkDesc(uIWatch_Com_Skill.txt_SkillDesc);
				}
			};
			btn_DetailInfo.list_Skill.numItems = 0;
			btn_DetailInfo.list_Skill.numItems = skillInfos.Count;
			btn_DetailInfo.touchable = true;
			RefreshHeroAnimation();
		}
		else
		{
			btn_DetailInfo.selected = false;
			btn_DetailInfo.touchable = false;
		}
	}

	private async void RefreshHeroAnimation()
	{
		group_Ok.visible = true;
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(playerData.standingPainting, "Walk", loader_Animation, 10f);
		loader_Animation.visible = true;
	}

	private void RefreshPlayerLabel()
	{
		isSelf.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.Id) ? 1 : 0);
		UICom_PlayerLabel com_Label = (UICom_PlayerLabel)com_PlayerLabel;
		CommonUIManager.RendererLabelInfo(com_Label, playerData.GetNick(showRemark: true), playerData.Level);
		(string, bool) labelData = playerData.AccountBackgroundURL();
		CommonUIManager.RendererLabel(UIType.Panel, 28, com_Label, labelData.Item1, labelData.Item2);
		string headShotUrl = playerData.HeadURL();
		CommonUIManager.RendererHeadShot(com_Label, headShotUrl, isVideo: false);
		com_PlayerLabel.onClick.Set((EventCallback0)delegate
		{
			if (playerData.IsBot)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1002.GetLocal(UIStringType.Spectate));
			}
			else
			{
				com_PlayerLabel.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(playerData.Id, playerData.GetNick(), playerData.Level, headShotUrl, labelData, delegate
				{
					com_PlayerLabel.onClick.Release();
				});
			}
		});
	}

	private bool CheckPlaceholder(int index, int mapType)
	{
		if (!BattleConfig.IsMutatorPve(mapType))
		{
			return false;
		}
		return mapType.GetGameModeNPCPlayerConfigure().RoomPosition == index;
	}

	private void RefreshPlaceholder(GameModeNPCPlayerConfigure npcPlayerConfigure)
	{
		MonsterInfoConfigure monsterInfoConfigure = npcPlayerConfigure.MonsterId.GetMonsterInfoConfigure();
		if (monsterInfoConfigure != null)
		{
			SkinStandingPaintingConfigureItem defeatStandingPaintingById = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetDefeatStandingPaintingById(monsterInfoConfigure.StandingPainting);
			if (defeatStandingPaintingById != null)
			{
				GameMode.selectedIndex = 2;
				com_PlayerLabel.visible = true;
				loader_Character.url = defeatStandingPaintingById.GetCharacterReady();
				CommonUIManager.RendererLabel((UICom_PlayerLabel)com_PlayerLabel, npcPlayerConfigure);
			}
		}
	}
}

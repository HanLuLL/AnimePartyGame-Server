using System.Collections.Generic;
using Core;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.model;

namespace UI;

public class UIRoomHero_Com_Player : GComponent
{
	private RoomPlayer playerData;

	private int mapType;

	public HeroCardData selectHero;

	private UIRoomHero_Com_MedalTip _medalTip;

	public Controller p;

	public Controller isSelf;

	public Controller isChoose;

	public Controller battlePass;

	public Controller GameMode;

	public Controller affirm;

	public Controller showSportsMedal;

	public GGroup group_Slot;

	public GLoader loader_Character;

	public GComponent com_Qulity;

	public GGraph loader_Animation;

	public GTextField txt_progress;

	public GGraph loader_BattlePass;

	public GGroup group_Ok;

	public GImage image_BreakThough;

	public GTextField txt_PVELV;

	public UIRoomHero_Com_ChangeSlot com_ChangeSlot;

	public GComponent com_PlayerLabel;

	public GImage YOU;

	public UIRoomHero_Button_DetailInfo btn_DetailInfo;

	public UIRoomHero_Com_SportsMedal com_sportsMedal;

	public const string URL = "ui://l82hrmsqqzg8g";

	public bool IsChangePlayer
	{
		get
		{
			if (playerData == null)
			{
				return false;
			}
			return playerData.Slot != p.selectedIndex;
		}
	}

	public void InitData(int index, RoomPlayer player, ref UIRoomHero_Com_Player ownerCom, int MapType, UIRoomHero_Com_MedalTip medalTip)
	{
		playerData = player;
		selectHero = null;
		mapType = MapType;
		p.selectedIndex = index;
		loader_Character.url = null;
		isChoose.selectedIndex = 0;
		affirm.selectedIndex = 0;
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(loader_Animation);
		loader_Animation.visible = false;
		txt_progress.visible = false;
		txt_progress.SetVar("progress", "0").FlushVars();
		btn_DetailInfo.selected = false;
		btn_DetailInfo.touchable = false;
		battlePass.selectedIndex = 0;
		group_Slot.visible = true;
		_medalTip = medalTip;
		if (player != null)
		{
			RefreshPlayerLabel();
			HeroBar heroBar = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.GetHeroBarById(player.Id);
			if (heroBar != null)
			{
				if (heroBar.HeroId != 0)
				{
					SureHero(heroBar.HeroId, !heroBar.Affirm);
					RefreshDetailInfo(heroBar.HeroId, heroBar.PveLevel, heroBar.PveTalentId);
				}
				if (heroBar.AffirmedSkin)
				{
					RefreshHeroAnimation(player.standingPainting, affirmed: true).Forget();
				}
			}
			ownerCom = (IsSelfSlot(player.Id) ? this : ownerCom);
			com_PlayerLabel.visible = true;
		}
		else
		{
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				base.visible = false;
			}
			com_PlayerLabel.visible = false;
			isSelf.selectedIndex = 0;
		}
		com_sportsMedal.onClick.Set(OnClickShowMedalTipBtn);
		RefreshGameMode();
	}

	public void RefreshHero(HeroBarBox _heroBarBox)
	{
		if (_heroBarBox != null && _heroBarBox.Box.TryGetValue(playerData.Id, out var value))
		{
			RefreshDetailInfo(value.HeroId, value.PveLevel, value.PveTalentId);
		}
	}

	public void RefreshDetailInfo(int HeroId, int PVELV, int talentId, bool isLocal = false)
	{
		if (HeroId > 0)
		{
			selectHero = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(HeroId);
			txt_PVELV.text = PVELV.ToString();
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 6 && roomInfo.MapId == 101)
			{
				txt_PVELV.text = "6";
			}
			image_BreakThough.visible = selectHero.InfoConfig.PveBreak.Contains(talentId);
			if (BattleConfig.IsPVE(mapType))
			{
				RefreshGameMode();
			}
			isChoose.selectedIndex = 1;
			SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem = (isLocal ? SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCurStandingPainting(HeroId) : playerData.standingPainting);
			loader_Character.url = skinStandingPaintingConfigureItem.GetCharacterReady();
			((UICom_SkinQuality)com_Qulity).appearanceType.selectedIndex = (int)skinStandingPaintingConfigureItem.SkinAppearanceType;
			btn_DetailInfo.txt_CharaterName.text = CharacterHandle.GetCharacterName(HeroId);
			btn_DetailInfo.txt_CharaterNick.text = CharacterHandle.GetCharacterNickName(HeroId);
			btn_DetailInfo.txt_HP.text = selectHero.InfoConfig.Blood.ToString();
			btn_DetailInfo.txt_ATK.text = selectHero.InfoConfig.Attack.ToString();
			btn_DetailInfo.txt_DEF.text = selectHero.InfoConfig.Defense.ToString();
			int battleActiveSkillId = CharacterHandle.GetBattleActiveSkillId(HeroId, CharacterType.None, talentId);
			RepeatedField<int> battlePassiveSkills = CharacterHandle.GetBattlePassiveSkills(HeroId, CharacterType.None, talentId);
			List<SkillInfoConfigure> skillInfos = UIHelper.GetSkillConfigs(battleActiveSkillId, battlePassiveSkills);
			btn_DetailInfo.list_Skill.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIRoomHero_Com_Skill uIRoomHero_Com_Skill && index >= 0 && index <= skillInfos.Count - 1)
				{
					uIRoomHero_Com_Skill.skillType.selectedIndex = ((skillInfos[index].SkillType != SkillType.Active) ? 1 : 0);
					if (skillInfos[index].SkillType == SkillType.Active)
					{
						btn_DetailInfo.txt_CD.text = skillInfos[index].Round.ToString();
					}
					uIRoomHero_Com_Skill.txt_SkillName.SetVar("skillName", skillInfos[index].NameID.GetLocal(UIStringType.Skill)).FlushVars();
					skillInfos[index].DescID.RefreshCharacterSkillHyperlinkDesc(uIRoomHero_Com_Skill.txt_SkillDesc);
				}
			};
			btn_DetailInfo.list_Skill.numItems = 0;
			btn_DetailInfo.list_Skill.numItems = skillInfos.Count;
			btn_DetailInfo.touchable = true;
			group_Slot.visible = false;
		}
		else
		{
			btn_DetailInfo.selected = false;
			btn_DetailInfo.touchable = false;
		}
	}

	public void SureHero(int heroId, bool hasChoice)
	{
		if (!hasChoice)
		{
			affirm.selectedIndex = 1;
		}
		selectHero = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(heroId);
	}

	public async UniTask RefreshHeroAnimation(SkinStandingPaintingConfigureItem standingPainting, bool affirmed)
	{
		if (standingPainting == null)
		{
			return;
		}
		loader_Character.url = standingPainting.GetCharacterReady();
		((UICom_SkinQuality)com_Qulity).appearanceType.selectedIndex = (int)standingPainting.SkinAppearanceType;
		if (affirmed)
		{
			if (playerData.gearType >= BattlePassGearType.PREMIUM)
			{
				battlePass.selectedIndex = 1;
				SimpleSingletonProvider<CriMovieManager>.inst.Play(201.GetVideoKey(), loader_BattlePass, null, FinishShow1).Forget();
			}
			await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(standingPainting, "Walk", loader_Animation, 10f);
			loader_Animation.visible = true;
		}
		affirm.selectedIndex = 1;
	}

	private void FinishShow1(Player source, int status)
	{
		battlePass.selectedIndex = 0;
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_BattlePass);
	}

	private void RefreshPlayerLabel()
	{
		isSelf.selectedIndex = (IsSelfSlot(playerData.Id) ? 1 : 0);
		UICom_PlayerLabel com_Label = (UICom_PlayerLabel)com_PlayerLabel;
		CommonUIManager.RendererLabelInfo(com_Label, playerData.GetNick(showRemark: true), playerData.Level);
		(string, bool) tuple = playerData.AccountBackgroundURL();
		CommonUIManager.RendererLabel(UIType.Panel, 17, com_Label, tuple.Item1, tuple.Item2);
		string headShot = playerData.HeadURL();
		CommonUIManager.RendererHeadShot(com_Label, headShot, isVideo: false);
	}

	private bool IsSelfSlot(long playerId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId);
	}

	public void InitChangeSlotComponent()
	{
		if (playerData == null)
		{
			com_ChangeSlot.visible = false;
			return;
		}
		com_ChangeSlot.visible = true;
		com_ChangeSlot.btn_ChangeSlot.visible = !IsSelfSlot(playerData.Id);
		com_ChangeSlot.com_ApplyChangeSlot.txt_Apply.text = 1115.GetLocal(UIStringType.Message);
		com_ChangeSlot.com_ApplyChangeSlot.visible = false;
		UIRoomHero_Com_ReceiveChangeSlot com_Receive = com_ChangeSlot.com_Receive1;
		UIRoomHero_Com_ReceiveChangeSlot com_Receive2 = com_ChangeSlot.com_Receive2;
		bool flag = (com_ChangeSlot.com_Receive3.visible = false);
		bool flag3 = (com_Receive2.visible = flag);
		com_Receive.visible = flag3;
		GTextField txt_Receive = com_ChangeSlot.com_Receive1.txt_Receive;
		GTextField txt_Receive2 = com_ChangeSlot.com_Receive2.txt_Receive;
		string text = (com_ChangeSlot.com_Receive3.txt_Receive.text = 1114.GetLocal(UIStringType.Message));
		string text2 = (txt_Receive2.text = text);
		txt_Receive.text = text2;
		com_ChangeSlot.btn_ChangeSlot.onClick.Set((EventCallback0)delegate
		{
			if (playerData != null && !IsSelfSlot(playerData.Id))
			{
				com_ChangeSlot.btn_ChangeSlot.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestApplyChangeSlot(playerData.Id, isCancel: false).OnFinishedOnly.AddOnce(delegate
				{
					com_ChangeSlot.btn_ChangeSlot.onClick.Release();
				});
			}
		});
	}

	public void RefreshChangeSlotComponent()
	{
		if (playerData != null)
		{
			List<ApplySlotData> changeSlotData = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.ChangeSlotData;
			com_ChangeSlot.com_Receive1.visible = false;
			com_ChangeSlot.com_Receive2.visible = false;
			com_ChangeSlot.com_Receive3.visible = false;
			com_ChangeSlot.com_ApplyChangeSlot.visible = false;
			com_ChangeSlot.btn_ChangeSlot.visible = false;
			if (IsSelfSlot(playerData.Id))
			{
				RefreshPlayerSlot(changeSlotData);
			}
			else
			{
				RefreshPlayerTargetSlot(changeSlotData);
			}
		}
	}

	private void RefreshPlayerSlot(List<ApplySlotData> changeSlotData)
	{
		List<ApplySlotData> list = changeSlotData.FindAll((ApplySlotData slotData) => !IsSelfSlot(slotData.applyId) && slotData.targetId == playerData.Id && !slotData.Reject);
		if (list.Count > 0)
		{
			RefreshReceiveComponent(list[0], com_ChangeSlot.com_Receive1);
		}
		if (list.Count > 1)
		{
			RefreshReceiveComponent(list[1], com_ChangeSlot.com_Receive2);
		}
		if (list.Count > 2)
		{
			RefreshReceiveComponent(list[2], com_ChangeSlot.com_Receive3);
		}
	}

	private void RefreshPlayerTargetSlot(List<ApplySlotData> changeSlotData)
	{
		int num = changeSlotData.FindIndex((ApplySlotData slotData) => IsSelfSlot(slotData.applyId) && slotData.targetId == playerData.Id);
		if (num >= 0)
		{
			ApplySlotData targetSlot = changeSlotData[num];
			if (targetSlot.Reject)
			{
				return;
			}
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo == null)
			{
				return;
			}
			RoomPlayer playerById = curRoomInfo.GetPlayerById(targetSlot.targetId);
			if (playerById == null)
			{
				return;
			}
			int slot = playerById.Slot;
			com_ChangeSlot.com_ApplyChangeSlot.txt_TargetSlot.color = GameConfig.slotColor[slot];
			com_ChangeSlot.com_ApplyChangeSlot.txt_TargetSlot.text = $"P{slot + 1}.";
			com_ChangeSlot.com_ApplyChangeSlot.btn_Cancel.onClick.Set((EventCallback0)delegate
			{
				com_ChangeSlot.com_ApplyChangeSlot.btn_Cancel.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestApplyChangeSlot(targetSlot.targetId, isCancel: true).OnFinishedOnly.AddOnce(delegate
				{
					com_ChangeSlot.com_ApplyChangeSlot.btn_Cancel.onClick.Release();
				});
			});
			com_ChangeSlot.com_ApplyChangeSlot.visible = true;
		}
		else if (SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.CHOICE)
		{
			int num2 = changeSlotData.FindIndex((ApplySlotData slotData) => (IsSelfSlot(slotData.applyId) && !slotData.Reject) || (playerData.Id == slotData.applyId && IsSelfSlot(slotData.targetId) && !slotData.Reject));
			com_ChangeSlot.btn_ChangeSlot.visible = num2 == -1;
		}
		else
		{
			com_ChangeSlot.btn_ChangeSlot.visible = false;
		}
	}

	private void RefreshReceiveComponent(ApplySlotData slotData, UIRoomHero_Com_ReceiveChangeSlot comReceive)
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return;
		}
		RoomPlayer playerById = curRoomInfo.GetPlayerById(slotData.applyId);
		if (playerById == null)
		{
			return;
		}
		int slot = playerById.Slot;
		comReceive.txt_ApplySlot.color = GameConfig.slotColor[slot];
		comReceive.txt_ApplySlot.text = $"P{slot + 1}.";
		comReceive.btn_Accept.onClick.Set((EventCallback0)delegate
		{
			comReceive.btn_Accept.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestOpsChangeSlot(isAgree: true, slotData.applyId).OnFinishedOnly.AddOnce(delegate
			{
				comReceive.btn_Accept.onClick.Release();
			});
		});
		comReceive.btn_Reject.onClick.Set((EventCallback0)delegate
		{
			comReceive.btn_Reject.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestOpsChangeSlot(isAgree: false, slotData.applyId).OnFinishedOnly.AddOnce(delegate
			{
				comReceive.btn_Reject.onClick.Release();
			});
		});
		comReceive.visible = true;
	}

	private void RefreshGameMode()
	{
		if (BattleConfig.IsPVP(mapType))
		{
			GameMode.selectedIndex = 0;
		}
		else if (BattleConfig.IsPVE(mapType))
		{
			GameMode.selectedIndex = ((selectHero != null && !BattleConfig.IsNovice(mapType)) ? ((!BattleConfig.IsMutatorPve(mapType)) ? 1 : 3) : 0);
		}
		else if (BattleConfig.IsLuckyStarBattle(mapType))
		{
			GameMode.selectedIndex = 2;
		}
		else
		{
			GameMode.selectedIndex = 0;
		}
		if (BattleConfig.IsMutatorPve(mapType))
		{
			btn_DetailInfo.visible = false;
			RefreshSportsMedal();
			TryRefreshMedalTip();
			if (CheckPlaceholder(p.selectedIndex))
			{
				RefreshPlaceholder(mapType.GetGameModeNPCPlayerConfigure());
			}
		}
	}

	private void OnClickShowMedalTipBtn()
	{
		if (_medalTip != null)
		{
			ShowMedalTip(_medalTip, isShow: true);
		}
	}

	private void TryRefreshMedalTip()
	{
		if (_medalTip != null && _medalTip.parent == this)
		{
			ShowMedalTip(_medalTip, isShow: true);
		}
	}

	private void RefreshSportsMedal()
	{
		if (playerData == null)
		{
			showSportsMedal.selectedIndex = 0;
			return;
		}
		MapField<int, int> mapField = (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.GetHeroBarById(playerData.Id))?.GameData;
		if (IsSelfSlot(playerData.Id) && selectHero != null && selectHero.HeroId != 0)
		{
			mapField = (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.heroChallengeData.TryGetValue(selectHero.HeroId, out var value) ? value.GameData : null);
		}
		else if (mapField == null)
		{
			showSportsMedal.selectedIndex = 0;
			return;
		}
		showSportsMedal.selectedIndex = 1;
		int mapRecordById = SportsMeetData.GetMapRecordById(mapField, SportsMeetData.GetMapIdBySportsMeetRank(1));
		int mapRecordById2 = SportsMeetData.GetMapRecordById(mapField, SportsMeetData.GetMapIdBySportsMeetRank(2));
		int mapRecordById3 = SportsMeetData.GetMapRecordById(mapField, SportsMeetData.GetMapIdBySportsMeetRank(3));
		int num = mapRecordById + mapRecordById2 + mapRecordById3;
		if (mapRecordById > 0 && mapRecordById2 > 0 && mapRecordById3 > 0)
		{
			com_sportsMedal.txt_VictoryCount.text = num.ToString();
		}
		else
		{
			com_sportsMedal.txt_VictoryCount.text = "";
		}
		com_sportsMedal.rank_1.visible = mapRecordById > 0;
		com_sportsMedal.rank_2.visible = mapRecordById2 > 0;
		com_sportsMedal.rank_3.visible = mapRecordById3 > 0;
	}

	public void ShowMedalTip(UIRoomHero_Com_MedalTip tip, bool isShow)
	{
		if (isShow)
		{
			HeroBar heroBar = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.GetHeroBarById(playerData.Id);
			if (heroBar == null || heroBar.HeroId == 0)
			{
				return;
			}
			tip.list_medalTip.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIRoomHero_ListItem_Medal uIRoomHero_ListItem_Medal)
				{
					int rank = 3 - index;
					uIRoomHero_ListItem_Medal.type.selectedIndex = index;
					uIRoomHero_ListItem_Medal.haveMedal.selectedIndex = ((SportsMeetData.GetMapRecordById(heroBar.GameData, SportsMeetData.GetMapIdBySportsMeetRank(rank)) > 0) ? 1 : 0);
				}
			};
			tip.list_medalTip.numItems = 3;
			GRoot.inst.ShowPopup(tip, com_sportsMedal.medalRoot, PopupDirection.Down);
		}
		else
		{
			GRoot.inst.HidePopup(tip);
		}
	}

	private bool CheckPlaceholder(int index)
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
				affirm.selectedIndex = 1;
				com_PlayerLabel.visible = true;
				loader_Character.url = defeatStandingPaintingById.GetCharacterReady();
				CommonUIManager.RendererLabel((UICom_PlayerLabel)com_PlayerLabel, npcPlayerConfigure);
			}
		}
	}

	public static UIRoomHero_Com_Player CreateInstance()
	{
		return (UIRoomHero_Com_Player)UIPackage.CreateObject("RoomHero", "RoomHero_Com_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		isSelf = GetControllerAt(1);
		isChoose = GetControllerAt(2);
		battlePass = GetControllerAt(3);
		GameMode = GetControllerAt(4);
		affirm = GetControllerAt(5);
		showSportsMedal = GetControllerAt(6);
		group_Slot = (GGroup)GetChildAt(15);
		loader_Character = (GLoader)GetChildAt(16);
		com_Qulity = (GComponent)GetChildAt(17);
		loader_Animation = (GGraph)GetChildAt(24);
		txt_progress = (GTextField)GetChildAt(25);
		loader_BattlePass = (GGraph)GetChildAt(26);
		group_Ok = (GGroup)GetChildAt(31);
		image_BreakThough = (GImage)GetChildAt(33);
		txt_PVELV = (GTextField)GetChildAt(35);
		com_ChangeSlot = (UIRoomHero_Com_ChangeSlot)GetChildAt(37);
		com_PlayerLabel = (GComponent)GetChildAt(38);
		YOU = (GImage)GetChildAt(39);
		btn_DetailInfo = (UIRoomHero_Button_DetailInfo)GetChildAt(40);
		com_sportsMedal = (UIRoomHero_Com_SportsMedal)GetChildAt(41);
	}
}

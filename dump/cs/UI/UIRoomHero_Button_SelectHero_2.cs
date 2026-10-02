using System.Collections.Generic;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIRoomHero_Button_SelectHero_2 : GButton
{
	public int _index;

	public HeroCardData HeroCard;

	private UIOverallColorController colorController;

	public Controller battlePass;

	public Controller GameMode;

	public Controller talentStatus;

	public Controller isLock;

	public UIRoomHero_Com_SelectHero com_Hero;

	public GGraph loader_BattlePass;

	public GTextField txt_PVELV;

	public GTextField txt_Trial;

	public GButton btn_Collect;

	public GImage img_LockIcon;

	public const string URL = "ui://l82hrmsqm49i1c";

	public void InitData(int index, HeroCardData heroCard)
	{
		_index = index;
		if (heroCard != null)
		{
			HeroCard = heroCard;
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			RepeatedField<int> forbiddenHeroIds = curRoomInfo.GetForbiddenHeroIds();
			com_Hero.Sure.selectedIndex = 0;
			battlePass.selectedIndex = 0;
			com_Hero.load_Character.url = heroCard.standingPainting.GetCharacterThin();
			com_Hero.load_SelectedCharacter.url = heroCard.standingPainting.GetCharacterThin();
			com_Hero.com_CharacterName.txt_Title.TryScrollTextField(CharacterHandle.GetCharacterNickName(HeroCard.HeroId));
			com_Hero.p.selectedIndex = ((!SimpleSingletonProvider<GameLogicManager>.inst.watch.PlayerIsWatcher()) ? (curRoomInfo.GetPlayerById(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID())?.Slot ?? 0) : 0);
			talentStatus.selectedIndex = ((heroCard.PveData.Talent.Count > 0) ? 1 : 0);
			GameMode.selectedIndex = (curRoomInfo.IsPVE() ? 1 : 0);
			bool flag = forbiddenHeroIds.Contains(HeroCard.HeroId);
			base.grayed = flag || heroCard.heroStatus == HeroStatus.None;
			if (curRoomInfo.IsPVE())
			{
				int battlePveLevel = heroCard.PveData.GetBattlePveLevel();
				txt_PVELV.text = battlePveLevel.ToString();
				GTextField gTextField = txt_Trial;
				int num;
				if (battlePveLevel == heroCard.PveData.Level)
				{
					HeroStatus heroStatus = heroCard.heroStatus;
					num = ((heroStatus != HeroStatus.Activate && heroStatus != HeroStatus.None) ? 1 : 0);
				}
				else
				{
					num = 1;
				}
				gTextField.visible = (byte)num != 0;
				if (!base.grayed)
				{
					RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
					if (roomInfo != null && roomInfo.MapType == 6 && roomInfo.MapId == 101)
					{
						txt_PVELV.text = "6";
						talentStatus.selectedIndex = 1;
						txt_Trial.visible = battlePveLevel != 6;
					}
				}
				if (!base.grayed && curRoomInfo.IsMutatorPve())
				{
					isLock.selectedIndex = ((!SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.HeroMapIsUnlockByHeroId(heroCard.HeroId, curRoomInfo.MapId)) ? 1 : 0);
					if (isLock.selectedIndex == 1)
					{
						if (colorController == null)
						{
							colorController = new UIOverallColorController(this, new HashSet<GObject> { img_LockIcon });
						}
						colorController.SetOverallColor(new Color(0.5f, 0.5f, 0.5f, 1f));
					}
					else
					{
						colorController?.Recover();
					}
				}
			}
			else
			{
				txt_Trial.visible = heroCard.heroStatus != HeroStatus.Activate && !base.grayed;
			}
		}
		else
		{
			GameMode.selectedIndex = 0;
			com_Hero.load_Character.url = "";
			com_Hero.load_SelectedCharacter.url = "";
			com_Hero.com_CharacterName.txt_Title.TryScrollTextField("");
		}
		base.onRollOut.Set((EventCallback0)delegate
		{
			if (!base.selected)
			{
				com_Hero.stateChange.selectedIndex = 0;
				com_Hero.xuanting.Stop();
			}
		});
		base.onRollOver.Set((EventCallback0)delegate
		{
			com_Hero.stateChange.selectedIndex = 1;
			com_Hero.xuanting.Play();
			com_Hero.OverSfx.Play();
		});
		btn_Collect.visible = heroCard?.CollectStatus ?? false;
		if (btn_Collect is UIButton_Collect uIButton_Collect)
		{
			uIButton_Collect.collected.selectedIndex = 1;
		}
	}

	public void RefreshData(HeroBar heroBar)
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (heroBar != null && heroBar.Affirm)
		{
			RoomPlayer playerById = curRoomInfo.GetPlayerById(heroBar.PlayerId);
			com_Hero.p.selectedIndex = playerById.Slot;
			if (com_Hero.Sure.selectedIndex == 0)
			{
				com_Hero.com_selectedaMovie.p.selectedIndex = com_Hero.p.selectedIndex;
				com_Hero.com_selectedaMovie.aMoive.SetHook("Switch", delegate
				{
					com_Hero.Sure.selectedIndex = 1;
					com_Hero.xuanze.Play();
				});
				com_Hero.com_selectedaMovie.aMoive.Play();
				PlayBattlePassSelected(playerById);
			}
		}
		else
		{
			com_Hero.Sure.selectedIndex = 0;
		}
	}

	public void RefreshSlotIndex(int curSlot)
	{
		com_Hero.p.selectedIndex = curSlot;
		com_Hero.com_selectedaMovie.p.selectedIndex = curSlot;
	}

	private void PlayBattlePassSelected(RoomPlayer playerData)
	{
		if (playerData.gearType >= BattlePassGearType.PREMIUM)
		{
			battlePass.selectedIndex = 1;
			SimpleSingletonProvider<CriMovieManager>.inst.Play(201.GetVideoKey(), loader_BattlePass, null, FinishShow).Forget();
		}
	}

	private void FinishShow(Player source, int status)
	{
		battlePass.selectedIndex = 0;
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_BattlePass);
	}

	public static UIRoomHero_Button_SelectHero_2 CreateInstance()
	{
		return (UIRoomHero_Button_SelectHero_2)UIPackage.CreateObject("RoomHero", "RoomHero_Button_SelectHero_2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		battlePass = GetControllerAt(0);
		GameMode = GetControllerAt(2);
		talentStatus = GetControllerAt(3);
		isLock = GetControllerAt(4);
		com_Hero = (UIRoomHero_Com_SelectHero)GetChildAt(0);
		loader_BattlePass = (GGraph)GetChildAt(1);
		txt_PVELV = (GTextField)GetChildAt(4);
		txt_Trial = (GTextField)GetChildAt(6);
		btn_Collect = (GButton)GetChildAt(7);
		img_LockIcon = (GImage)GetChildAt(8);
	}
}

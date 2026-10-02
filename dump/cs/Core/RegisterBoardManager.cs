using System.Collections.Generic;
using Core.Unit;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core;

public class RegisterBoardManager : SimpleSingletonProvider<RegisterBoardManager>
{
	public readonly RegisterTabInfo tabInfo = new RegisterTabInfo();

	private readonly List<AttrBoardData> tempAttrDatas = new List<AttrBoardData>();

	public void RegisterAttrData(UpdateHeroAttrS2C model)
	{
		tempAttrDatas.Clear();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer();
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			DealEffectData(effectData);
		}
		switch (model.Cause.S)
		{
		case CauseOrigin.Types.source.Unknown:
		{
			foreach (AttrBoardData tempAttrData in tempAttrDatas)
			{
				tabInfo.UpdateData(round, tempAttrData);
			}
			break;
		}
		case CauseOrigin.Types.source.Skill:
		{
			SkillInfoConfigure skillConfigure = ((int)model.Cause.Id).GetSkillConfigure();
			{
				foreach (AttrBoardData tempAttrData2 in tempAttrDatas)
				{
					tempAttrData2.Source = "Skill: " + skillConfigure.NameID.GetLocal(UIStringType.Skill);
					tabInfo.UpdateData(round, tempAttrData2);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.Card:
		{
			CardInfoConfigure cardConfigure = ((int)model.Cause.Id).GetCardConfigure();
			{
				foreach (AttrBoardData tempAttrData3 in tempAttrDatas)
				{
					tempAttrData3.Source = "Card: " + cardConfigure.NameID.GetLocal(UIStringType.Card);
					tabInfo.UpdateData(round, tempAttrData3);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.Event:
		{
			EventInfoConfigure eventConfigure = ((int)model.Cause.Id).GetEventConfigure();
			{
				foreach (AttrBoardData tempAttrData4 in tempAttrDatas)
				{
					tempAttrData4.Source = "Event: " + eventConfigure.NameID.GetLocal(UIStringType.Event);
					tabInfo.UpdateData(round, tempAttrData4);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.Destiny:
		{
			if (!StaticConfigure.Destiny.InfoDict.TryGetValue((int)model.Cause.Id, out var value2))
			{
				break;
			}
			{
				foreach (AttrBoardData tempAttrData5 in tempAttrDatas)
				{
					tempAttrData5.Source = "Event: " + value2.NameID.GetLocal(UIStringType.Destiny);
					tabInfo.UpdateData(round, tempAttrData5);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.Divination:
		{
			if (!StaticConfigure.Divination.InfoDict.TryGetValue((int)model.Cause.Id, out var value))
			{
				break;
			}
			{
				foreach (AttrBoardData tempAttrData6 in tempAttrDatas)
				{
					tempAttrData6.Source = "Event: " + value.NameID.GetLocal(UIStringType.Divination);
					tabInfo.UpdateData(round, tempAttrData6);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.HeroBuff:
		{
			Buff buffById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).buffContainer.GetBuffById(model.Cause.Id);
			if (buffById == null)
			{
				break;
			}
			BuffInfoConfigure buffConfigure = buffById.BuffId.GetBuffConfigure();
			{
				foreach (AttrBoardData tempAttrData7 in tempAttrDatas)
				{
					if (buffConfigure.IsShow && buffConfigure.DescId > 0)
					{
						tempAttrData7.Source = "Buff: " + buffConfigure.DescId.GetLocal(UIStringType.Buff);
					}
					tabInfo.UpdateData(round, tempAttrData7);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.LandBuff:
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.TryGetLandBuff(model.Cause.Id, out var buffData) || buffData.buffData.Source.S != buff_source.Types.source.Summon)
			{
				break;
			}
			SummonInfoConfigure summonDataConfigure = buffData.buffData.Source.Id.GetSummonDataConfigure();
			{
				foreach (AttrBoardData tempAttrData8 in tempAttrDatas)
				{
					tempAttrData8.Source = $"Summon: {summonDataConfigure.Id}";
					tabInfo.UpdateData(round, tempAttrData8);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.Land:
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById((int)model.Cause.Id);
			LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[(int)landById.LandType];
			{
				foreach (AttrBoardData tempAttrData9 in tempAttrDatas)
				{
					tempAttrData9.Source = "Land: " + landInfoConfigure.NameID.GetLocal(UIStringType.Land);
					tabInfo.UpdateData(round, tempAttrData9);
				}
				break;
			}
		}
		case CauseOrigin.Types.source.BombDie:
		{
			foreach (AttrBoardData tempAttrData10 in tempAttrDatas)
			{
				tempAttrData10.Source = "BombDie";
				tabInfo.UpdateData(round, tempAttrData10);
			}
			break;
		}
		case CauseOrigin.Types.source.RoundAward:
		{
			foreach (AttrBoardData tempAttrData11 in tempAttrDatas)
			{
				tabInfo.UpdateData(round + 1, tempAttrData11);
			}
			break;
		}
		case CauseOrigin.Types.source.ShopOpen:
		{
			foreach (AttrBoardData tempAttrData12 in tempAttrDatas)
			{
				tempAttrData12.Source = "ShopOpen";
				tabInfo.UpdateData(round, tempAttrData12);
			}
			break;
		}
		case CauseOrigin.Types.source.ShopBuy:
		{
			foreach (AttrBoardData tempAttrData13 in tempAttrDatas)
			{
				tempAttrData13.Source = "商店购买";
				tabInfo.UpdateData(round, tempAttrData13);
			}
			break;
		}
		case (CauseOrigin.Types.source)4:
		case CauseOrigin.Types.source.Battle:
			break;
		}
	}

	private void DealEffectData(HeroAttrEffect _EffectData)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_EffectData.PlayerId);
		int index = ((playerDataById.characterType == CharacterType.Hero) ? playerDataById.player.Slot : 4);
		string text = ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[index]);
		if (_EffectData.Lv != null)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "LV";
			attrBoardData.AttrValue = _EffectData.Lv.CurrLv;
			AttrBoardData item = attrBoardData;
			tempAttrDatas.Add(item);
		}
		if (_EffectData.Gold != null && _EffectData.Gold.ChangeGold != 0)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "GOLD";
			attrBoardData.AttrValue = _EffectData.Gold.ChangeGold;
			AttrBoardData item2 = attrBoardData;
			tempAttrDatas.Add(item2);
		}
		if (_EffectData.Hp != null)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "HP";
			attrBoardData.AttrValue = _EffectData.Hp.RealChangeHp;
			AttrBoardData item3 = attrBoardData;
			tempAttrDatas.Add(item3);
		}
		if (_EffectData.Atk != null)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "ATK";
			attrBoardData.AttrValue = _EffectData.Atk.CurrAtk;
			AttrBoardData item4 = attrBoardData;
			tempAttrDatas.Add(item4);
		}
		if (_EffectData.Def != null)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "DEF";
			attrBoardData.AttrValue = _EffectData.Def.CurrDef;
			AttrBoardData item5 = attrBoardData;
			tempAttrDatas.Add(item5);
		}
		if (_EffectData.Card != null)
		{
			AttrBoardData attrBoardData = new AttrBoardData();
			attrBoardData.PlayerName = "[color=#" + text + "]" + playerDataById.player.GetNick() + "[/color]";
			attrBoardData.AttrName = "Card";
			AttrBoardData attrBoardData2 = attrBoardData;
			RepeatedField<CardInfo> cards = _EffectData.Card.Cards;
			List<CardInfo> cardInfos = playerDataById.cardContainer._CardInfos;
			string text2 = "";
			foreach (CardInfo item6 in cards)
			{
				if (!cardInfos.Contains(item6))
				{
					text2 += item6.CardId.GetCardConfigure().NameID.GetLocal(UIStringType.Card);
				}
			}
			string text3 = "";
			foreach (CardInfo item7 in cardInfos)
			{
				if (!cards.Contains(item7))
				{
					text3 += item7.CardId.GetCardConfigure().NameID.GetLocal(UIStringType.Card);
				}
			}
			attrBoardData2.AttrValue = cards.Count - cardInfos.Count;
			attrBoardData2.TargetName = text2 + text3;
			tempAttrDatas.Add(attrBoardData2);
		}
		_ = _EffectData.Buff;
		_ = _EffectData.Lottery;
	}

	public void Dispose()
	{
		tabInfo?.Dispose();
	}
}

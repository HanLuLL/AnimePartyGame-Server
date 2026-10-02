using System;
using System.Collections.Generic;
using Core;
using FairyGUI;
using Google.Protobuf.Collections;
using UnityEngine;

namespace UI;

public static class UIHelper
{
	public static Vector2 World2Local(Camera camera, Vector3 pos)
	{
		Vector3 vector = camera.WorldToScreenPoint(pos);
		vector.y = (float)Screen.height - vector.y;
		return GRoot.inst.GlobalToLocal(vector);
	}

	public static (float width, float height) ExpandToAspectRatio(float originalWidth, float originalHeight, float targetWidthNormal = 16f, float targetHeightNormal = 9f)
	{
		double num = targetWidthNormal / targetHeightNormal;
		int num2 = (int)Math.Ceiling((double)originalWidth / num);
		if ((float)num2 >= originalHeight)
		{
			return (width: originalWidth, height: num2);
		}
		return (width: (int)Math.Ceiling((double)originalHeight * num), height: originalHeight);
	}

	public static UIPanelConfigure GetPanelConfig(UIPanelType panelType)
	{
		if (StaticConfigure.UI.PanelDict.TryGetValue((int)panelType, out var value))
		{
			return value;
		}
		return null;
	}

	public static UIWindowConfigure GetWindowConfig(UIWindowType windowType)
	{
		if (StaticConfigure.UI.WindowDict.TryGetValue((int)windowType, out var value))
		{
			return value;
		}
		return null;
	}

	public static CharacterInfoConfigure GetCharacterConfigure(this int id)
	{
		if (!StaticConfigure.Character.InfoDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static BattleResourceInfoConfigure GetTimelineAssetName(this int heroId)
	{
		if (!StaticConfigure.BattleResource.InfoDict.TryGetValue(heroId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CharacterHeroFavorGiftConfigure GetheroFavorGift(this int heroFavorGiftId)
	{
		if (!StaticConfigure.Character.HeroFavorGiftDict.TryGetValue(heroFavorGiftId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CharacterVoiceConfigure GetVoiceConfigure(this int VoiceId)
	{
		if (!StaticConfigure.Character.VoiceDict.TryGetValue(VoiceId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CharacterExpressionPackConfigure GetCharacterExpressionPackConfigure(this int heroID)
	{
		if (!StaticConfigure.Character.ExpressionPackDict.TryGetValue(heroID, out var value))
		{
			return null;
		}
		return value;
	}

	public static MonsterInfoConfigure GetMonsterInfoConfigure(this int monsterId)
	{
		if (!StaticConfigure.Monster.InfoDict.TryGetValue(monsterId, out var value))
		{
			return null;
		}
		return value;
	}

	public static MonsterAttributeConfigure GetMonsterAttributeConfigure(this int monsterId)
	{
		if (!StaticConfigure.Monster.AttributeDict.TryGetValue(monsterId, out var value))
		{
			return null;
		}
		return value;
	}

	public static BuffInfoConfigure GetBuffConfigure(this int id)
	{
		if (!StaticConfigure.Buff.InfoDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static CardInfoConfigure GetCardConfigure(this int id)
	{
		if (!StaticConfigure.Card.InfoDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static EventInfoConfigure GetEventConfigure(this int id)
	{
		if (!StaticConfigure.Event.InfoDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static SkillInfoConfigure GetSkillConfigure(this int id)
	{
		if (!StaticConfigure.Skill.InfoDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static List<SkillInfoConfigure> GetSkillConfigs(int activeSkill, RepeatedField<int> passiveSkills, RepeatedField<int> pveActiveSkillExtra = null)
	{
		List<SkillInfoConfigure> list = new List<SkillInfoConfigure>();
		if (activeSkill > 0 && StaticConfigure.Skill.InfoDict.TryGetValue(activeSkill, out var value) && value.IsShow)
		{
			list.Add(value);
		}
		if (pveActiveSkillExtra != null && pveActiveSkillExtra.Count > 0)
		{
			foreach (int item in pveActiveSkillExtra)
			{
				if (item > 0 && StaticConfigure.Skill.InfoDict.TryGetValue(item, out var value2) && value2.IsShow)
				{
					list.Add(value2);
				}
			}
		}
		foreach (int passiveSkill in passiveSkills)
		{
			if (passiveSkill > 0 && StaticConfigure.Skill.InfoDict.TryGetValue(passiveSkill, out var value3) && value3.IsShow)
			{
				list.Add(value3);
			}
		}
		return list;
	}

	public static EffectInfoConfigure GetEffectDataConfigure(this int EffectID)
	{
		if (!StaticConfigure.Effect.InfoDict.TryGetValue(EffectID, out var value))
		{
			return null;
		}
		return value;
	}

	public static SinglePlayerLevelConfigure GetSignalPlayerDataConfigure(this int LeverId)
	{
		if (!StaticConfigure.SinglePlayer.LevelDict.TryGetValue(LeverId, out var value))
		{
			return null;
		}
		return value;
	}

	public static MapInfoConfigure GetMapDataConfigure(this int MapID)
	{
		if (!StaticConfigure.Map.InfoDict.TryGetValue(MapID, out var value))
		{
			return null;
		}
		return value;
	}

	public static SummonInfoConfigure GetSummonDataConfigure(this int SummonID)
	{
		if (!StaticConfigure.Summon.InfoDict.TryGetValue(SummonID, out var value))
		{
			return null;
		}
		return value;
	}

	public static ItemInfoConfigure GetItemInfoConfigure(this int itemId)
	{
		if (!StaticConfigure.Item.InfoDict.TryGetValue(itemId, out var value))
		{
			return null;
		}
		return value;
	}

	public static ItemTagConfigure GetItemTagConfigure(this int itemType)
	{
		if (!StaticConfigure.Item.TagDict.TryGetValue(itemType, out var value))
		{
			return null;
		}
		return value;
	}

	public static ChestInfoConfigure GetChestInfoConfigure(this int chestId)
	{
		if (!StaticConfigure.Chest.InfoDict.TryGetValue(chestId, out var value))
		{
			return null;
		}
		return value;
	}

	public static ChestRandomRewardConfigure GetChestRandomRewardConfigure(this int rewardId)
	{
		if (!StaticConfigure.Chest.RandomRewardDict.TryGetValue(rewardId, out var value))
		{
			return null;
		}
		return value;
	}

	public static ExchangeStoreShelfConfigure GetExchangeStoreShelfConfigure(this int shelfId)
	{
		if (!StaticConfigure.ExchangeStore.ShelfDict.TryGetValue(shelfId, out var value))
		{
			return null;
		}
		return value;
	}

	public static ExchangeStoreInfoConfigure GetExchangeStoreInfoConfigure(this int shopType)
	{
		if (!StaticConfigure.ExchangeStore.InfoDict.TryGetValue(shopType, out var value))
		{
			return null;
		}
		return value;
	}

	public static ExchangeStoreGoodsConfigure GetExchangeStoreGoodsConfigure(this int goodsId)
	{
		if (!StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(goodsId, out var value))
		{
			return null;
		}
		return value;
	}

	public static RechargeStoreShelfConfigure GetRechargeStoreShelfConfigure(this int shelfId)
	{
		if (!StaticConfigure.RechargeStore.ShelfDict.TryGetValue(shelfId, out var value))
		{
			return null;
		}
		return value;
	}

	public static RechargeStoreInfoConfigure GetRechargeStoreInfoConfigure(this int shopType)
	{
		if (!StaticConfigure.RechargeStore.InfoDict.TryGetValue(shopType, out var value))
		{
			return null;
		}
		return value;
	}

	public static RechargeStoreGoodsConfigure GetRechargeStoreGoodsConfigure(this int goodsId)
	{
		if (!StaticConfigure.RechargeStore.GoodsDict.TryGetValue(goodsId, out var value))
		{
			return null;
		}
		return value;
	}

	public static string GetFashionAccountHeadShot(this int headId)
	{
		if (!StaticConfigure.Fashion.AccountHeadShotDict.TryGetValue(headId, out var value))
		{
			return null;
		}
		return value.GetHeadShot();
	}

	public static FashionAccountBackgroundConfigure GetFashionAccountBackgroundConfigure(this int BackgroundId)
	{
		if (!StaticConfigure.Fashion.AccountBackgroundDict.TryGetValue(BackgroundId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FashionEffectConfigure GetFashionFashionEffectConfigure(this int EffectId)
	{
		if (!StaticConfigure.Fashion.EffectDict.TryGetValue(EffectId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FashionDiceConfigure GetFashionDiceConfigure(this int DiceId)
	{
		if (!StaticConfigure.Fashion.DiceDict.TryGetValue(DiceId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FashionCardBackConfigure GetFashionCardBackConfigure(this int CardBack)
	{
		if (!StaticConfigure.Fashion.CardBackDict.TryGetValue(CardBack, out var value))
		{
			return null;
		}
		return value;
	}

	public static FashionKVConfigure GetFashionMainBgConfigure(this int MainId)
	{
		if (!StaticConfigure.Fashion.KVDict.TryGetValue(MainId, out var value))
		{
			return null;
		}
		return value;
	}

	public static GachaBackstageConfigure GetGachaBackstageConfigure(this int BackstageId)
	{
		if (!StaticConfigure.Gacha.BackstageDict.TryGetValue(BackstageId, out var value))
		{
			return null;
		}
		return value;
	}

	public static GachaPoolConfigure GetGachaPoolConfigure(this int PoolId)
	{
		if (!StaticConfigure.Gacha.PoolDict.TryGetValue(PoolId, out var value))
		{
			return null;
		}
		return value;
	}

	public static GachaCombConfigure GetGachaCombConfigure(this int CombId)
	{
		if (!StaticConfigure.Gacha.CombDict.TryGetValue(CombId, out var value))
		{
			return null;
		}
		return value;
	}

	public static GachaGroupConfigure GetGachaGroupConfigure(this int GroupId)
	{
		if (!StaticConfigure.Gacha.GroupDict.TryGetValue(GroupId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FavorLevelConfigure GetFavorLevelConfigure(this int favorLevelId)
	{
		if (!StaticConfigure.Favor.LevelDict.TryGetValue(favorLevelId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FavorLevelRewardConfigure GetFavorLevelRewardConfigure(this int HeroId)
	{
		if (!StaticConfigure.Favor.LevelRewardDict.TryGetValue(HeroId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FavorBreakthroughConfigure GetFavorBreakthroughConfigure(this int HeroId)
	{
		if (!StaticConfigure.Favor.BreakthroughDict.TryGetValue(HeroId, out var value))
		{
			return null;
		}
		return value;
	}

	public static FavorGiftConfigure GetFavorGiftConfigure(this int GiftId)
	{
		if (!StaticConfigure.Favor.GiftDict.TryGetValue(GiftId, out var value))
		{
			return null;
		}
		return value;
	}

	public static PlayerLevelConfigure GetPlayerLevelConfigure(this int level)
	{
		if (!StaticConfigure.Player.LevelDict.TryGetValue(level, out var value))
		{
			return null;
		}
		return value;
	}

	public static GameModeInfoConfigure GetGameModeInfoConfigure(this int mapModeType)
	{
		if (!StaticConfigure.GameMode.InfoDict.TryGetValue(mapModeType, out var value))
		{
			return null;
		}
		return value;
	}

	public static ChoosingTimeLimitdifficultyConfigure GetChoosingTimeLimitDifficultyConfigure(this int difficulty)
	{
		if (!StaticConfigure.ChoosingTimeLimit.DifficultyDict.TryGetValue(difficulty, out var value))
		{
			return null;
		}
		return value;
	}

	public static GameModeNPCPlayerConfigure GetGameModeNPCPlayerConfigure(this int mapModeType)
	{
		if (!StaticConfigure.GameMode.NPCPlayerDict.TryGetValue(mapModeType, out var value))
		{
			return null;
		}
		return value;
	}

	public static string GetVideoKey(this int id)
	{
		if (!StaticConfigure.Video.GlobalDict.TryGetValue(id, out var value))
		{
			return null;
		}
		string result = value.LoadedKey;
		if (!string.IsNullOrWhiteSpace(value.LoadedKeySFW))
		{
			result = (GameSettings.angelMode ? value.LoadedKeySFW : value.LoadedKey);
		}
		return result;
	}

	public static FashionKVConfigure GetKVVideoConfig(this int id)
	{
		if (!StaticConfigure.Fashion.KVDict.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public static string GetKVVideoKey(this int id)
	{
		FashionKVConfigure kVVideoConfig = id.GetKVVideoConfig();
		if (kVVideoConfig == null)
		{
			return null;
		}
		string result = kVVideoConfig.LoadedKey;
		if (!string.IsNullOrWhiteSpace(kVVideoConfig.LoadedKeySFW) && GameSettings.angelMode)
		{
			result = kVVideoConfig.LoadedKeySFW;
		}
		return result;
	}

	public static RelicInfoConfigure GetRelicInfoConfigure(this int relicId)
	{
		if (!StaticConfigure.Relic.InfoDict.TryGetValue(relicId, out var value))
		{
			return null;
		}
		return value;
	}

	public static PVEMissionInfoConfigure GetPVEMissionInfoConfigure(this int missionId)
	{
		if (!StaticConfigure.PVEMission.InfoDict.TryGetValue(missionId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CampaignLevelConfigure GetCampaignLevelConfigure(this int levelId)
	{
		if (!StaticConfigure.Campaign.LevelDict.TryGetValue(levelId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CollaborationInfoConfigure GetCollaborationInfoConfigure(this int CollaborateId)
	{
		if (!StaticConfigure.Collaboration.InfoDict.TryGetValue(CollaborateId, out var value))
		{
			return null;
		}
		return value;
	}

	public static CollaborationGoodsConfigure GetCollaborationGoodsConfigure(this int index)
	{
		if (!StaticConfigure.Collaboration.GoodsDict.TryGetValue(index, out var value))
		{
			return null;
		}
		return value;
	}

	public static CouponsInfoConfigure GetCouponsInfoConfigure(this int itemId)
	{
		if (!StaticConfigure.Coupons.InfoDict.TryGetValue(itemId, out var value))
		{
			return null;
		}
		return value;
	}

	public static string GetImageLocalization(this int iconId)
	{
		if (!StaticConfigure.ImageLocalization.LocalDict.TryGetValue(iconId, out var value))
		{
			return null;
		}
		return GameSettings.GetDataForLanguage(value.English, value.Japanese, value.Simplified, value.Traditional);
	}

	public static MutatorInfoConfigure GetMutatorInfoConfigure(this int mutatorId)
	{
		if (!StaticConfigure.Mutator.InfoDict.TryGetValue(mutatorId, out var value))
		{
			return null;
		}
		return value;
	}

	public static RepeatedField<MutatorPoolConfigureItem> GetMutatorPoolComposeConfigure(this int mutatorPoolId)
	{
		if (!StaticConfigure.Mutator.PoolDict.TryGetValue(mutatorPoolId, out var value))
		{
			return null;
		}
		return value.MutatorPoolConfigureItems;
	}

	public static RepeatedField<MapGameDifficultyConfigureItem> GetMapGameDifficultyItems(this int difficultyId)
	{
		if (!StaticConfigure.Map.GameDifficultyDict.TryGetValue(difficultyId, out var value))
		{
			Debug.LogError("在Map.GameDifficultyDict表里并没有找到难度DifficultyId：" + difficultyId);
			return null;
		}
		return value.MapGameDifficultyConfigureItems;
	}

	public static AchieveInfoConfigure GetAchieveConfig(this int Type)
	{
		if (!StaticConfigure.Achieve.InfoDict.TryGetValue(Type, out var value))
		{
			return null;
		}
		return value;
	}

	public static SkinSkinPendantConfigure GetSkinPendantConfig(this int skinItemId)
	{
		if (!StaticConfigure.Skin.SkinPendantDict.TryGetValue(skinItemId, out var value))
		{
			return null;
		}
		return value;
	}

	public static MissionDataConfigure GetMissionDataConfigure(this int missionId)
	{
		if (!StaticConfigure.Mission.DataDict.TryGetValue(missionId, out var value))
		{
			return null;
		}
		return value;
	}

	public static RepeatedField<MapMapLevelConfigureItem> GetMapLevelConfigureItems(this int mapId)
	{
		if (!StaticConfigure.Map.MapLevelDict.TryGetValue(mapId, out var value))
		{
			Debug.LogError("在Map.MapLevelDict表里并没有找到地图Id：" + mapId);
			return null;
		}
		return value.MapMapLevelConfigureItems;
	}

	public static PerformTriggerSetConfigure GetPerformTriggerSetConfigure(this int performTriggerSetId)
	{
		if (!StaticConfigure.Perform.TriggerSetDict.TryGetValue(performTriggerSetId, out var value))
		{
			return null;
		}
		return value;
	}

	public static PerformTriggerConfigure GetPerformTriggerConfigure(this int performTriggerId)
	{
		if (!StaticConfigure.Perform.TriggerDict.TryGetValue(performTriggerId, out var value))
		{
			return null;
		}
		return value;
	}

	public static SurveyInfoConfigure GetSurveyInfoConfigure(this int surveyId)
	{
		if (!StaticConfigure.Survey.InfoDict.TryGetValue(surveyId, out var value))
		{
			return null;
		}
		return value;
	}

	public static string GetLocal(this int id, UIStringType type)
	{
		switch (type)
		{
		case UIStringType.Achieve:
		{
			if (StaticConfigure.STRAchieve.LocalDict.TryGetValue(id, out var value48))
			{
				return value48.GetLocal();
			}
			Debug.LogError($"STRAchieve表没有id={id}的数据!");
			break;
		}
		case UIStringType.Acquisition:
		{
			if (StaticConfigure.STRAcquisition.LocalDict.TryGetValue(id, out var value26))
			{
				return value26.GetLocal();
			}
			Debug.LogError($"STRAcquisition表没有id={id}的数据!");
			break;
		}
		case UIStringType.Activity:
		{
			if (StaticConfigure.STRActivity.LocalDict.TryGetValue(id, out var value54))
			{
				return value54.GetLocal();
			}
			Debug.LogError($"STRActivity表没有id={id}的数据!");
			break;
		}
		case UIStringType.Banner:
		{
			if (StaticConfigure.STRBanner.LocalDict.TryGetValue(id, out var value8))
			{
				return value8.GetLocal();
			}
			Debug.LogError($"STRBanner表没有id={id}的数据!");
			break;
		}
		case UIStringType.BattlePass:
		{
			if (StaticConfigure.STRBattlePass.LocalDict.TryGetValue(id, out var value35))
			{
				return value35.GetLocal();
			}
			Debug.LogError($"STRBattlePass表没有id={id}的数据!");
			break;
		}
		case UIStringType.BeginTips:
		{
			if (StaticConfigure.STRBeginTips.LocalDict.TryGetValue(id, out var value5))
			{
				return value5.GetLocal();
			}
			Debug.LogError($"STRBeginTips表没有id={id}的数据!");
			break;
		}
		case UIStringType.Bot:
		{
			if (StaticConfigure.STRBot.LocalDict.TryGetValue(id, out var value39))
			{
				return value39.GetLocal();
			}
			Debug.LogError($"STRBot表没有id={id}的数据!");
			break;
		}
		case UIStringType.Buff:
		{
			if (StaticConfigure.STRBuff.LocalDict.TryGetValue(id, out var value18))
			{
				return value18.GetLocal();
			}
			Debug.LogError($"STRBuff表没有id={id}的数据!");
			break;
		}
		case UIStringType.Campaign:
		{
			if (StaticConfigure.STRCampaign.LocalDict.TryGetValue(id, out var value59))
			{
				return value59.GetLocal();
			}
			Debug.LogError($"STRCampaign表没有id={id}的数据!");
			break;
		}
		case UIStringType.Card:
		{
			if (StaticConfigure.STRCard.LocalDict.TryGetValue(id, out var value45))
			{
				return value45.GetLocal();
			}
			Debug.LogError($"STRCard表没有id={id}的数据!");
			break;
		}
		case UIStringType.Character:
		{
			if (StaticConfigure.STRCharacter.LocalDict.TryGetValue(id, out var value27))
			{
				return value27.GetLocal();
			}
			Debug.LogError($"STRCharacter表没有id={id}的数据!");
			break;
		}
		case UIStringType.Chat:
		{
			if (StaticConfigure.STRChat.LocalDict.TryGetValue(id, out var value14))
			{
				return value14.GetLocal();
			}
			Debug.LogError($"STRChat表没有id={id}的数据!");
			break;
		}
		case UIStringType.Chest:
		{
			if (StaticConfigure.STRChest.LocalDict.TryGetValue(id, out var value62))
			{
				return value62.GetLocal();
			}
			Debug.LogError($"STRChest表没有id={id}的数据!");
			break;
		}
		case UIStringType.ChoosingTimeLimit:
		{
			if (StaticConfigure.STRChoosingTimeLimit.LocalDict.TryGetValue(id, out var value53))
			{
				return value53.GetLocal();
			}
			Debug.LogError($"STRChoosingTimeLimit表没有id={id}的数据!");
			break;
		}
		case UIStringType.Collaboration:
		{
			if (StaticConfigure.STRCollaboration.LocalDict.TryGetValue(id, out var value41))
			{
				return value41.GetLocal();
			}
			Debug.LogError($"STRCollaboration表没有id={id}的数据!");
			break;
		}
		case UIStringType.Day7GiftPackage:
		{
			if (StaticConfigure.STRDay7GiftPackage.LocalDict.TryGetValue(id, out var value32))
			{
				return value32.GetLocal();
			}
			Debug.LogError($"STRDay7GiftPackage表没有id={id}的数据!");
			break;
		}
		case UIStringType.Destiny:
		{
			if (StaticConfigure.STRDestiny.LocalDict.TryGetValue(id, out var value21))
			{
				return value21.GetLocal();
			}
			Debug.LogError($"STRDestiny表没有id={id}的数据!");
			break;
		}
		case UIStringType.Dialog:
		{
			if (StaticConfigure.STRDialog.LocalDict.TryGetValue(id, out var value12))
			{
				return value12.GetLocal();
			}
			Debug.LogError($"STRDialog表没有id={id}的数据!");
			break;
		}
		case UIStringType.DiceActivity:
		{
			if (StaticConfigure.STRDiceActivity.LocalDict.TryGetValue(id, out var value63))
			{
				return value63.GetLocal();
			}
			Debug.LogError($"STRDiceActivity表没有id={id}的数据!");
			break;
		}
		case UIStringType.Divination:
		{
			if (StaticConfigure.STRDivination.LocalDict.TryGetValue(id, out var value57))
			{
				return value57.GetLocal();
			}
			Debug.LogError($"STRDivination表没有id={id}的数据!");
			break;
		}
		case UIStringType.Dynamic:
		{
			if (StaticConfigure.STRDynamic.LocalDict.TryGetValue(id, out var value50))
			{
				return value50.GetLocal();
			}
			Debug.LogError($"STRDynamic表没有id={id}的数据!");
			break;
		}
		case UIStringType.Event:
		{
			if (StaticConfigure.STREvent.LocalDict.TryGetValue(id, out var value44))
			{
				return value44.GetLocal();
			}
			Debug.LogError($"STREvent表没有id={id}的数据!");
			break;
		}
		case UIStringType.ExchangeStore:
		{
			if (StaticConfigure.STRExchangeStore.LocalDict.TryGetValue(id, out var value36))
			{
				return value36.GetLocal();
			}
			Debug.LogError($"STRExchangeStore表没有id={id}的数据!");
			break;
		}
		case UIStringType.Friend:
		{
			if (StaticConfigure.STRFriend.LocalDict.TryGetValue(id, out var value30))
			{
				return value30.GetLocal();
			}
			Debug.LogError($"STRFriend表没有id={id}的数据!");
			break;
		}
		case UIStringType.Gacha:
		{
			if (StaticConfigure.STRGacha.LocalDict.TryGetValue(id, out var value23))
			{
				return value23.GetLocal();
			}
			Debug.LogError($"STRGacha表没有id={id}的数据!");
			break;
		}
		case UIStringType.GameMode:
		{
			if (StaticConfigure.STRGameMode.LocalDict.TryGetValue(id, out var value17))
			{
				return value17.GetLocal();
			}
			Debug.LogError($"STRGameMode表没有id={id}的数据!");
			break;
		}
		case UIStringType.GUI:
		{
			if (StaticConfigure.STRGUI.LocalDict.TryGetValue(id, out var value9))
			{
				return value9.GetLocal();
			}
			Debug.LogError($"STRGUI表没有id={id}的数据!");
			break;
		}
		case UIStringType.Guild:
		{
			if (StaticConfigure.STRGuild.LocalDict.TryGetValue(id, out var value3))
			{
				return value3.GetLocal();
			}
			Debug.LogError($"STRGuild表没有id={id}的数据!");
			break;
		}
		case UIStringType.Item:
		{
			if (StaticConfigure.STRItem.LocalDict.TryGetValue(id, out var value60))
			{
				return value60.GetLocal();
			}
			Debug.LogError($"STRItem表没有id={id}的数据!");
			break;
		}
		case UIStringType.Land:
		{
			if (StaticConfigure.STRLand.LocalDict.TryGetValue(id, out var value56))
			{
				return value56.GetLocal();
			}
			Debug.LogError($"STRLand表没有id={id}的数据!");
			break;
		}
		case UIStringType.LuckyStarBattle:
		{
			if (StaticConfigure.STRLuckyStarBattle.LocalDict.TryGetValue(id, out var value51))
			{
				return value51.GetLocal();
			}
			Debug.LogError($"STRLuckyStarBattle表没有id={id}的数据!");
			break;
		}
		case UIStringType.Map:
		{
			if (StaticConfigure.STRMap.LocalDict.TryGetValue(id, out var value47))
			{
				return value47.GetLocal();
			}
			Debug.LogError($"STRMap表没有id={id}的数据!");
			break;
		}
		case UIStringType.MapEvent:
		{
			if (StaticConfigure.STRMapEvent.LocalDict.TryGetValue(id, out var value42))
			{
				return value42.GetLocal();
			}
			Debug.LogError($"STRMapEvent表没有id={id}的数据!");
			break;
		}
		case UIStringType.Match:
		{
			if (StaticConfigure.STRMatch.LocalDict.TryGetValue(id, out var value38))
			{
				return value38.GetLocal();
			}
			Debug.LogError($"STRMatch表没有id={id}的数据!");
			break;
		}
		case UIStringType.Message:
		{
			if (StaticConfigure.STRMessage.LocalDict.TryGetValue(id, out var value33))
			{
				return value33.GetLocal();
			}
			Debug.LogError($"STRMessage表没有id={id}的数据!");
			break;
		}
		case UIStringType.Mission:
		{
			if (StaticConfigure.STRMission.LocalDict.TryGetValue(id, out var value29))
			{
				return value29.GetLocal();
			}
			Debug.LogError($"STRMission表没有id={id}的数据!");
			break;
		}
		case UIStringType.Monster:
		{
			if (StaticConfigure.STRMonster.LocalDict.TryGetValue(id, out var value24))
			{
				return value24.GetLocal();
			}
			Debug.LogError($"STRMonster表没有id={id}的数据!");
			break;
		}
		case UIStringType.MonthlyCard:
		{
			if (StaticConfigure.STRMonthlyCard.LocalDict.TryGetValue(id, out var value20))
			{
				return value20.GetLocal();
			}
			Debug.LogError($"STRMonthlyCard表没有id={id}的数据!");
			break;
		}
		case UIStringType.Mutator:
		{
			if (StaticConfigure.STRMutator.LocalDict.TryGetValue(id, out var value15))
			{
				return value15.GetLocal();
			}
			Debug.LogError($"STRMutator表没有id={id}的数据!");
			break;
		}
		case UIStringType.Perform:
		{
			if (StaticConfigure.STRPerform.LocalDict.TryGetValue(id, out var value11))
			{
				return value11.GetLocal();
			}
			Debug.LogError($"STRPerform表没有id={id}的数据!");
			break;
		}
		case UIStringType.Player:
		{
			if (StaticConfigure.STRPlayer.LocalDict.TryGetValue(id, out var value6))
			{
				return value6.GetLocal();
			}
			Debug.LogError($"STRPlayer表没有id={id}的数据!");
			break;
		}
		case UIStringType.ProductRecommendation:
		{
			if (StaticConfigure.STRProductRecommendation.LocalDict.TryGetValue(id, out var value2))
			{
				return value2.GetLocal();
			}
			Debug.LogError($"STRProductRecommendation表没有id={id}的数据!");
			break;
		}
		case UIStringType.PVEMission:
		{
			if (StaticConfigure.STRPVEMission.LocalDict.TryGetValue(id, out var value61))
			{
				return value61.GetLocal();
			}
			Debug.LogError($"STRPVEMission表没有id={id}的数据!");
			break;
		}
		case UIStringType.PVENurturance:
		{
			if (StaticConfigure.STRPVENurturance.LocalDict.TryGetValue(id, out var value58))
			{
				return value58.GetLocal();
			}
			Debug.LogError($"STRPVENurturance表没有id={id}的数据!");
			break;
		}
		case UIStringType.RechargeStore:
		{
			if (StaticConfigure.STRRechargeStore.LocalDict.TryGetValue(id, out var value55))
			{
				return value55.GetLocal();
			}
			Debug.LogError($"STRRechargeStore表没有id={id}的数据!");
			break;
		}
		case UIStringType.RechargeStoreAds:
		{
			if (StaticConfigure.STRRechargeStoreAds.LocalDict.TryGetValue(id, out var value52))
			{
				return value52.GetLocal();
			}
			Debug.LogError($"STRRechargeStoreAds表没有id={id}的数据!");
			break;
		}
		case UIStringType.Relic:
		{
			if (StaticConfigure.STRRelic.LocalDict.TryGetValue(id, out var value49))
			{
				return value49.GetLocal();
			}
			Debug.LogError($"STRRelic表没有id={id}的数据!");
			break;
		}
		case UIStringType.Server:
		{
			if (StaticConfigure.STRServer.LocalDict.TryGetValue(id, out var value46))
			{
				return value46.GetLocal();
			}
			Debug.LogError($"STRServer表没有id={id}的数据!");
			break;
		}
		case UIStringType.Settings:
		{
			if (StaticConfigure.STRSettings.LocalDict.TryGetValue(id, out var value43))
			{
				return value43.GetLocal();
			}
			Debug.LogError($"STRSettings表没有id={id}的数据!");
			break;
		}
		case UIStringType.SignIn:
		{
			if (StaticConfigure.STRSignIn.LocalDict.TryGetValue(id, out var value40))
			{
				return value40.GetLocal();
			}
			Debug.LogError($"STRSignIn表没有id={id}的数据!");
			break;
		}
		case UIStringType.SinglePlayer:
		{
			if (StaticConfigure.STRSinglePlayer.LocalDict.TryGetValue(id, out var value37))
			{
				return value37.GetLocal();
			}
			Debug.LogError($"STRSinglePlayer表没有id={id}的数据!");
			break;
		}
		case UIStringType.Skill:
		{
			if (StaticConfigure.STRSkill.LocalDict.TryGetValue(id, out var value34))
			{
				return value34.GetLocal();
			}
			Debug.LogError($"STRSkill表没有id={id}的数据!");
			break;
		}
		case UIStringType.Skin:
		{
			if (StaticConfigure.STRSkin.LocalDict.TryGetValue(id, out var value31))
			{
				return value31.GetLocal();
			}
			Debug.LogError($"STRSkin表没有id={id}的数据!");
			break;
		}
		case UIStringType.SkinSell:
		{
			if (StaticConfigure.STRSkinSell.LocalDict.TryGetValue(id, out var value28))
			{
				return value28.GetLocal();
			}
			Debug.LogError($"STRSkinSell表没有id={id}的数据!");
			break;
		}
		case UIStringType.Spectate:
		{
			if (StaticConfigure.STRSpectate.LocalDict.TryGetValue(id, out var value25))
			{
				return value25.GetLocal();
			}
			Debug.LogError($"STRSpectate表没有id={id}的数据!");
			break;
		}
		case UIStringType.Story:
		{
			if (StaticConfigure.STRStory.LocalDict.TryGetValue(id, out var value22))
			{
				return value22.GetLocal();
			}
			Debug.LogError($"STRStory表没有id={id}的数据!");
			break;
		}
		case UIStringType.Summon:
		{
			if (StaticConfigure.STRSummon.LocalDict.TryGetValue(id, out var value19))
			{
				return value19.GetLocal();
			}
			Debug.LogError($"STRSummon表没有id={id}的数据!");
			break;
		}
		case UIStringType.Survey:
		{
			if (StaticConfigure.STRSurvey.LocalDict.TryGetValue(id, out var value16))
			{
				return value16.GetLocal();
			}
			Debug.LogError($"STRSurvey表没有id={id}的数据!");
			break;
		}
		case UIStringType.Task:
		{
			if (StaticConfigure.STRTask.LocalDict.TryGetValue(id, out var value13))
			{
				return value13.GetLocal();
			}
			Debug.LogError($"STRTask表没有id={id}的数据!");
			break;
		}
		case UIStringType.Tutorial:
		{
			if (StaticConfigure.STRTutorial.LocalDict.TryGetValue(id, out var value10))
			{
				return value10.GetLocal();
			}
			Debug.LogError($"STRTutorial表没有id={id}的数据!");
			break;
		}
		case UIStringType.Voice:
		{
			if (StaticConfigure.STRVoice.LocalDict.TryGetValue(id, out var value7))
			{
				return value7.GetLocal();
			}
			Debug.LogError($"STRVoice表没有id={id}的数据!");
			break;
		}
		case UIStringType.Way:
		{
			if (StaticConfigure.STRWay.LocalDict.TryGetValue(id, out var value4))
			{
				return value4.GetLocal();
			}
			Debug.LogError($"STRWay表没有id={id}的数据!");
			break;
		}
		case UIStringType.Welfare:
		{
			if (StaticConfigure.STRWelfare.LocalDict.TryGetValue(id, out var value))
			{
				return value.GetLocal();
			}
			Debug.LogError($"STRWelfare表没有id={id}的数据!");
			break;
		}
		default:
			Debug.LogError($"未找到定义的枚举:{type},请执行客户端导表代码的bat脚本！");
			break;
		}
		return "";
	}
}

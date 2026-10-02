using System;
using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class HeroCardLogic : IRPCSync
{
	private Dictionary<int, HeroCardData> heroCardDict;

	private readonly Dictionary<int, Dictionary<int, SkinStandingPaintingConfigureItem>> _StandingPaintingDict;

	public SportsMeetData sportsMeetData = new SportsMeetData();

	public Dictionary<string, HeroSkinOffset> heroSkinOffsetDict;

	public HeroCardLogic()
	{
		RepeatedField<SkinStandingPaintingConfigure> standingPaintings = StaticConfigure.Skin.StandingPaintings;
		_StandingPaintingDict = new Dictionary<int, Dictionary<int, SkinStandingPaintingConfigureItem>>(standingPaintings.Count);
		foreach (SkinStandingPaintingConfigure item in standingPaintings)
		{
			RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems = item.SkinStandingPaintingConfigureItems;
			Dictionary<int, SkinStandingPaintingConfigureItem> dictionary = new Dictionary<int, SkinStandingPaintingConfigureItem>(skinStandingPaintingConfigureItems.Count);
			foreach (SkinStandingPaintingConfigureItem item2 in skinStandingPaintingConfigureItems)
			{
				dictionary.TryAdd(item2.ItemID, item2);
			}
			_StandingPaintingDict.TryAdd(item.Id, dictionary);
		}
		UpdateOffsetData();
	}

	public bool IsActivityTrialHeroIds(int heroId)
	{
		foreach (TrialActivityConfigure activity in StaticConfigure.Trial.Activitys)
		{
			if (TimeHelper.ValidityTime(activity.BeginTime, activity.EndTime))
			{
				return activity.HeroIDs.Contains(heroId);
			}
		}
		return false;
	}

	public bool IsNoviceTrialHeroIds(int heroId)
	{
		TrialParamsConfigure trialParamsConfigure = StaticConfigure.Trial.Paramss[0];
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level < trialParamsConfigure.PlayerLevel && trialParamsConfigure.HeroIDs.Contains(heroId))
		{
			return true;
		}
		return false;
	}

	public bool IsComebackTrialHeroIds(int heroId)
	{
		ComebackParamsConfigure comebackParamsConfigure = StaticConfigure.Comeback?.ParamsDict?.GetValueOrDefault(1);
		if (comebackParamsConfigure == null || comebackParamsConfigure.HeroIDs == null || comebackParamsConfigure.HeroIDs.Count == 0)
		{
			return false;
		}
		if (!comebackParamsConfigure.HeroIDs.Contains(heroId))
		{
			return false;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.comeback != null)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.comeback.Data.IsInPeriod();
		}
		return false;
	}

	public bool IsCampaignTrialHeroStatus(int heroId)
	{
		RepeatedField<CampaignTryOutConfigure> tryOuts = StaticConfigure.Campaign.TryOuts;
		if (tryOuts.Count != 0 && tryOuts[0].FobiddenTryOut.Contains(heroId))
		{
			return false;
		}
		RepeatedField<int> repeatedField = SimpleSingletonProvider<GameLogicManager>.inst.campaign?.CampaignLevel?.HeroesLimited;
		if (repeatedField != null && repeatedField.Count > 0)
		{
			return repeatedField.Contains(heroId);
		}
		return true;
	}

	private void UpdateOffsetData()
	{
		RepeatedField<SkinOffsetConfigure> offsets = StaticConfigure.Skin.Offsets;
		heroSkinOffsetDict = new Dictionary<string, HeroSkinOffset>(offsets.Count);
		foreach (SkinOffsetConfigure item in offsets)
		{
			heroSkinOffsetDict.TryAdd(item.ImageName, new HeroSkinOffset(item));
		}
	}

	public void InitFromServer(MapField<int, RoleCard> playerRoleCard, SportsMeetInfo sportsMeetInfo)
	{
		heroCardDict = new Dictionary<int, HeroCardData>();
		foreach (CharacterInfoConfigure info in StaticConfigure.Character.Infos)
		{
			if (info.HeroType == CharacterType.Hero)
			{
				playerRoleCard.TryGetValue(info.Id, out var value);
				heroCardDict.Add(info.Id, new HeroCardData(info.Id, value));
			}
		}
		sportsMeetData.InitFromServer(sportsMeetInfo);
	}

	public HeroCardData GetCardData(int HeroID)
	{
		return heroCardDict.GetValueOrDefault(HeroID);
	}

	public List<HeroCardData> GetHeroCards(bool checkStatus)
	{
		if (checkStatus)
		{
			return heroCardDict.Values.Where(IsValidHeroCardItem).ToList();
		}
		return heroCardDict.Values.ToList();
	}

	public List<HeroCardData> GetSportsMeetHeroCards()
	{
		List<HeroCardData> list = heroCardDict.Values.Where(IsSportsMeetHeroCard).ToList();
		list.Sort((HeroCardData x, HeroCardData y) => CompareTo(x, y, ascending: true));
		return list;
	}

	private bool IsSportsMeetHeroCard(HeroCardData heroCard)
	{
		if (!IsValidHeroCardItem(heroCard) || heroCard.heroStatus == HeroStatus.None)
		{
			return sportsMeetData.HeroHasStar(heroCard.HeroId);
		}
		return true;
	}

	private bool IsValidHeroCardItem(HeroCardData heroCard)
	{
		if (StaticConfigure.Item.InfoDict.TryGetValue(heroCard.HeroId, out var value))
		{
			return value.IsVailItem();
		}
		return false;
	}

	public SkinStandingPaintingConfigureItem GetCurStandingPainting(int HeroID)
	{
		return GetCardData(HeroID)?.standingPainting;
	}

	public SkinStandingPaintingConfigureItem GetConfigStandingPainting(int heroId, int itemId, int standingPaintingId)
	{
		int key = ((standingPaintingId != 0) ? standingPaintingId : CharacterHandle.GetCharacterStandingPainting(heroId));
		if (_StandingPaintingDict.TryGetValue(key, out var value))
		{
			if (itemId != 0 && value.TryGetValue(itemId, out var value2))
			{
				return value2;
			}
			foreach (KeyValuePair<int, SkinStandingPaintingConfigureItem> item in value)
			{
				if (item.Value.IsDefault)
				{
					return item.Value;
				}
			}
			return null;
		}
		return null;
	}

	public SkinStandingPaintingConfigureItem GetConfigStandingPainting(int skinItemId)
	{
		foreach (SkinStandingPaintingConfigure standingPainting in StaticConfigure.Skin.StandingPaintings)
		{
			foreach (SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem in standingPainting.SkinStandingPaintingConfigureItems)
			{
				if (skinStandingPaintingConfigureItem.ItemID == skinItemId)
				{
					return skinStandingPaintingConfigureItem;
				}
			}
		}
		return null;
	}

	public int CompareTo(HeroCardData x, HeroCardData y, bool ascending)
	{
		int num = CompareBaseStatus(x, y);
		if (num != 0)
		{
			return num;
		}
		return Compare(x.InfoConfig.OrderWeight, y.InfoConfig.OrderWeight, ascending);
	}

	public int CompareToLVAndExp(HeroCardData x, HeroCardData y, bool ascending)
	{
		int num = CompareBaseStatus(x, y);
		if (num != 0)
		{
			return num;
		}
		int a = x.LV;
		int b = y.LV;
		int a2 = x.Exp;
		int b2 = y.Exp;
		if (!x.InfoConfig.HasKizuna)
		{
			RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
			a = levels[levels.Count - 1].Id;
			RepeatedField<FavorLevelConfigure> levels2 = StaticConfigure.Favor.Levels;
			a2 = levels2[levels2.Count - 1].TotalExp;
		}
		if (!y.InfoConfig.HasKizuna)
		{
			RepeatedField<FavorLevelConfigure> levels3 = StaticConfigure.Favor.Levels;
			b = levels3[levels3.Count - 1].Id;
			RepeatedField<FavorLevelConfigure> levels4 = StaticConfigure.Favor.Levels;
			b2 = levels4[levels4.Count - 1].TotalExp;
		}
		num = Compare(a, b, ascending);
		if (num != 0)
		{
			return num;
		}
		num = Compare(a2, b2, ascending);
		if (num != 0)
		{
			return num;
		}
		return Compare(x.InfoConfig.OrderWeight, y.InfoConfig.OrderWeight, ascending);
	}

	public int CompareToPve(HeroCardData x, HeroCardData y, bool ascending)
	{
		int num = CompareBaseStatus(x, y);
		if (num != 0)
		{
			return num;
		}
		PVENurturanceBreakConfigure currentTalentConfigure = x.PveData.GetCurrentTalentConfigure();
		PVENurturanceBreakConfigure currentTalentConfigure2 = y.PveData.GetCurrentTalentConfigure();
		int a = x.PveData.Level;
		int b = y.PveData.Level;
		if (currentTalentConfigure != null && x.PveData.IsTalentUnlock(currentTalentConfigure.Id))
		{
			a = 7;
		}
		if (currentTalentConfigure2 != null && y.PveData.IsTalentUnlock(currentTalentConfigure2.Id))
		{
			b = 7;
		}
		num = Compare(a, b, ascending);
		if (num != 0)
		{
			return num;
		}
		num = Compare(x.PveData.Exp, y.PveData.Exp, ascending);
		if (num != 0)
		{
			return num;
		}
		return Compare(x.InfoConfig.OrderWeight, y.InfoConfig.OrderWeight, ascending);
	}

	private int CompareBaseStatus(HeroCardData x, HeroCardData y)
	{
		int num = y.CollectStatus.CompareTo(x.CollectStatus);
		if (num != 0)
		{
			return num;
		}
		bool flag = x.heroStatus != HeroStatus.None;
		bool flag2 = y.heroStatus != HeroStatus.None;
		if (flag != flag2)
		{
			return flag2.CompareTo(flag);
		}
		if (x.IsHas != y.IsHas)
		{
			return y.IsHas.CompareTo(x.IsHas);
		}
		return 0;
	}

	private int Compare<T>(T a, T b, bool ascending) where T : IComparable<T>
	{
		if (!ascending)
		{
			return b.CompareTo(a);
		}
		return a.CompareTo(b);
	}

	public List<SkinStandingPaintingConfigureItem> GetStandingPaintingsById(int heroStandingPaintingId)
	{
		if (!StaticConfigure.Skin.StandingPaintingDict.TryGetValue(heroStandingPaintingId, out var value))
		{
			Debug.LogError($"StaticConfigure.Skin.StandingPaintingDict 无法通过Id:{heroStandingPaintingId}找到配置");
			return null;
		}
		List<SkinStandingPaintingConfigureItem> list = new List<SkinStandingPaintingConfigureItem>();
		RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems = value.SkinStandingPaintingConfigureItems;
		for (int i = 0; i < skinStandingPaintingConfigureItems.Count; i++)
		{
			ItemInfoConfigure itemInfoConfigure = skinStandingPaintingConfigureItems[i].ItemID.GetItemInfoConfigure();
			if (itemInfoConfigure != null && itemInfoConfigure.IsClientShow && itemInfoConfigure.IsVailItem())
			{
				list.Add(skinStandingPaintingConfigureItems[i]);
			}
		}
		list.Sort(delegate(SkinStandingPaintingConfigureItem x, SkinStandingPaintingConfigureItem y)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(x.ItemID);
			bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(y.ItemID);
			return (flag != flag2) ? ((!flag) ? 1 : (-1)) : (x.ItemID - y.ItemID);
		});
		return list;
	}

	public SkinStandingPaintingConfigureItem GetDefeatStandingPaintingById(int standingPaintingId)
	{
		if (!StaticConfigure.Skin.StandingPaintingDict.TryGetValue(standingPaintingId, out var value))
		{
			Debug.LogError($"StaticConfigure.Skin.StandingPaintingDict 无法通过Id:{standingPaintingId}找到配置");
			return null;
		}
		RepeatedField<SkinStandingPaintingConfigureItem> skinStandingPaintingConfigureItems = value.SkinStandingPaintingConfigureItems;
		if (skinStandingPaintingConfigureItems != null && skinStandingPaintingConfigureItems.Count > 0)
		{
			return skinStandingPaintingConfigureItems[0];
		}
		return null;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardUpLvS2C.OnRoleCardUpLvS2CServerCallBackAsync = OnRoleCardUpLvS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardBreakThroughS2C.OnRoleCardBreakThroughS2CServerCallBackAsync = OnRoleCardBreakThroughS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardChoiceResS2C.OnRoleCardChoiceResS2CServerCallBackAsync = OnRoleCardChoiceResS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardChangeS2C.OnRoleCardChangeS2CServerCallBackAsync = OnRoleCardChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PveHeroUpLvS2C.OnPveHeroUpLvS2CServerCallBackAsync = OnPveHeroUpLvS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardCollectS2C.OnRoleCardCollectS2CServerCallBackAsync = OnRoleCardCollectS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PveHeroTalentUpS2C.OnPveHeroTalentUpS2CServerCallBackAsync = OnRolePveHeroTalentUpS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChallengeDataChangeS2C.OnChallengeDataChangeS2CServerCallBackAsync = OnChallengeDataChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GmUnlockRoleInfoS2C.OnGmUnlockRoleInfoS2CServerCallBackAsync = OnGmUnlockRoleInfoS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardUpLvS2C.OnRoleCardUpLvS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardBreakThroughS2C.OnRoleCardBreakThroughS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardChoiceResS2C.OnRoleCardChoiceResS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardChangeS2C.OnRoleCardChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PveHeroUpLvS2C.OnPveHeroUpLvS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RoleCardCollectS2C.OnRoleCardCollectS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PveHeroTalentUpS2C.OnPveHeroTalentUpS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChallengeDataChangeS2C.OnChallengeDataChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GmUnlockRoleInfoS2C.OnGmUnlockRoleInfoS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestRoleCardUpLvC2S(int heroId, int itemId, int count)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RoleCardUpLvC2S.RoleCardUpLvC2SCall(new RoleCardUpLvC2S
		{
			DefId = heroId,
			ItemDefId = itemId,
			ItemCount = count
		});
	}

	private async UniTask OnRoleCardUpLvS2CServerCallBack(RoleCardUpLvS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			HeroCardData cardData = GetCardData(model.DefId);
			int lV = cardData.LV;
			cardData.UpdateExp(model.Exp);
			cardData.UpdateLV(model.Lv);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
			{
				await heroPanel.HeroCardFavorUp(cardData.LV > lV);
			}
		}
	}

	public RPCAsyncResult RequestRoleCardBreakThroughC2S(int heroId, bool isSuper)
	{
		GetCardData(heroId).UpdateBreakThough(breakThrough: true);
		return MonoSingletonProvider<NetManager>.inst.RPC.RoleCardBreakThroughC2S.RoleCardBreakThroughC2SCall(new RoleCardBreakThroughC2S
		{
			DefId = heroId,
			IsSuper = isSuper
		});
	}

	private async UniTask OnRoleCardBreakThroughS2CServerCallBack(RoleCardBreakThroughS2C model, int errId, bool isdispatch)
	{
		if (errId == 0 && SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			await heroPanel.ShowBreakThrough();
		}
	}

	public RPCAsyncResult RequestRoleTalentUpC2S(int heroId, int talentId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.PveHeroTalentUpC2S.PveHeroTalentUpC2SCall(new PveHeroTalentUpC2S
		{
			RoleId = heroId,
			TalentId = talentId
		});
	}

	private async UniTask OnRolePveHeroTalentUpS2CServerCallBack(PveHeroTalentUpS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			GetCardData(model.RoleId)?.PveData.UpdateTalent(model.Talents);
			await UniTask.CompletedTask;
		}
	}

	public int GetTalentSkillId(int heroId, int talentId)
	{
		if (!heroCardDict.TryGetValue(heroId, out var value))
		{
			Debug.LogError($"未获取到ID：{heroId}的HeroCardData！");
			return 0;
		}
		foreach (PVENurturanceBreakConfigure pveNurturanceBreakConfigure in value.PveData.PveNurturanceBreakConfigures)
		{
			if (pveNurturanceBreakConfigure.Id == talentId)
			{
				return pveNurturanceBreakConfigure.ReplaceActiveSkill;
			}
		}
		return 0;
	}

	public RPCAsyncResult RequestRoleCardChoiceResC2S(int heroId, int itemID)
	{
		GetCardData(heroId).UpdateUseDressId(itemID);
		return MonoSingletonProvider<NetManager>.inst.RPC.RoleCardChoiceResC2S.RoleCardChoiceResC2SCall(new RoleCardChoiceResC2S
		{
			DefId = heroId,
			ItemId = itemID
		});
	}

	private async UniTask OnRoleCardChoiceResS2CServerCallBack(RoleCardChoiceResS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnRoleCardChangeS2CServerCallBack(RoleCardChangeS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
			if (heroCardDict.ContainsKey(model.Item.DefId))
			{
				heroCardDict[model.Item.DefId] = new HeroCardData(model.Item.DefId, model.Item);
			}
			else
			{
				heroCardDict.TryAdd(model.Item.DefId, new HeroCardData(model.Item.DefId, model.Item));
			}
		}
	}

	public RPCAsyncResult RequestPveHeroUpLvC2S(int heroId, Dictionary<int, int> UseItems)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.PveHeroUpLvC2S.PveHeroUpLvC2SCall(new PveHeroUpLvC2S
		{
			DefId = heroId,
			UseItems = { (IDictionary<int, int>)UseItems }
		});
	}

	private async UniTask OnPveHeroUpLvS2CServerCallBack(PveHeroUpLvS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		GetCardData(model.DefId).PveData.UpdateData(model.Lv, model.Exp);
		if (model.ReturnItems != null && model.ReturnItems.Count > 0)
		{
			List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
			foreach (KeyValuePair<int, int> returnItem in model.ReturnItems)
			{
				list.Add(new KeyValuePair<int, int>(returnItem.Key, returnItem.Value));
			}
			SimpleSingletonProvider<UIManager>.inst.reward.ShowReward(list, 1009);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestRoleCardCollectC2S(int heroId, bool status)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RoleCardCollectC2S.RoleCardCollectC2SCall(new RoleCardCollectC2S
		{
			Collected = !status,
			DefId = heroId
		});
	}

	private async UniTask OnRoleCardCollectS2CServerCallBack(RoleCardCollectS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
			if (heroCardDict.TryGetValue(model.DefId, out var value))
			{
				value.UpdateCollectStatus();
			}
		}
	}

	private async UniTask OnChallengeDataChangeS2CServerCallBack(ChallengeDataChangeS2C model, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		sportsMeetData.UpdateChallengeData(model.ChallengeData);
		RepeatedField<int> passMapIds = model.PassMapIds;
		if (passMapIds != null && passMapIds.Count > 0)
		{
			foreach (int passMapId in model.PassMapIds)
			{
				if (!sportsMeetData.passMapIds.Contains(passMapId))
				{
					sportsMeetData.passMapIds.Add(passMapId);
				}
			}
		}
		sportsMeetData.isKnockoutMatch = model.IsKnockoutMatch;
		sportsMeetData.isFinalMatch = model.IsFinalMatch;
		await UniTask.CompletedTask;
	}

	private async UniTask OnGmUnlockRoleInfoS2CServerCallBack(GmUnlockRoleInfoS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		foreach (CharacterInfoConfigure info in StaticConfigure.Character.Infos)
		{
			if (info.HeroType == CharacterType.Hero)
			{
				model.RoleInfo.TryGetValue(info.Id, out var value);
				if (!heroCardDict.ContainsKey(info.Id))
				{
					heroCardDict.Add(info.Id, new HeroCardData(info.Id, value));
				}
				else
				{
					heroCardDict[info.Id] = new HeroCardData(info.Id, value);
				}
			}
		}
		await UniTask.CompletedTask;
	}
}

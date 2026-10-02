using System.Collections.Generic;
using System.Linq;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Relic;
using Tools;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay.Character;

public class HeroProperty : UnitProperty
{
	private Int32Encryptor movePoint2Encryptor = new Int32Encryptor();

	public readonly List<RelicInfo> RelicList = new List<RelicInfo>();

	private Int32Encryptor _cardCostReductionBonusEncryptor = new Int32Encryptor();

	private Int32Encryptor _cardRevenueBonusEncryptor = new Int32Encryptor();

	public Dictionary<int, int> RelicGlodDic = new Dictionary<int, int>();

	public Dictionary<int, int> BuildingGlodDic = new Dictionary<int, int>();

	public int MovePoint
	{
		get
		{
			return movePoint2Encryptor.DecryptGet();
		}
		set
		{
			movePoint2Encryptor.EncryptSet(value);
		}
	}

	public int StandLandId { get; private set; }

	public List<int> NextLandIds { get; private set; }

	public int DevelopLandCount { get; private set; }

	public ReactiveProperty<int> DoubleDiceTime { get; private set; } = new ReactiveProperty<int>();

	private int _cardCostReductionBonus
	{
		get
		{
			return _cardCostReductionBonusEncryptor.DecryptGet();
		}
		set
		{
			_cardCostReductionBonusEncryptor.EncryptSet(value);
		}
	}

	public float CardCostReductionBonus => (float)_cardCostReductionBonus * 0.01f;

	private int _cardRevenueBonus
	{
		get
		{
			return _cardRevenueBonusEncryptor.DecryptGet();
		}
		set
		{
			_cardRevenueBonusEncryptor.EncryptSet(value);
		}
	}

	public float CardRevenueBonus => (float)_cardRevenueBonus * 0.01f;

	public int ForceFirstDicePoint { get; private set; }

	public HeroProperty(int roleId, int bornId, List<int> nextIds)
	{
		base.CharacterType = CharacterType.Hero;
		Id = roleId;
		StandLandId = bornId;
		NextLandIds = nextIds;
		base.Star = 0;
		SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
		if (safeByIndex == null)
		{
			Debug.LogError("无法取得 单人玩法参数配置");
			return;
		}
		base.Gold.Value = safeByIndex.InitGold;
		SinglePlayerUpgradeConfigureItem heroUpgradeInfo = Game.GetModel<GameData>().GetHeroUpgradeInfo(base.Star);
		if (heroUpgradeInfo != null)
		{
			base.MaxHP = heroUpgradeInfo.CharacterMaxHp;
			base.HP = base.MaxHP;
			base.ATK = heroUpgradeInfo.CharacterAtk;
			base.DEF = heroUpgradeInfo.CharacterDef;
		}
	}

	public virtual void InitDataFromServer(SingleHero heroData)
	{
		base.Gold.Value = heroData.Gold;
		StandLandId = heroData.StandLandId;
		NextLandIds.Clear();
		NextLandIds.AddRange(heroData.NextLandIds);
		DevelopLandCount = heroData.DevelopLandCount;
		ForceFirstDicePoint = heroData.ForceFirstDicePoint;
		_cardCostReductionBonus = heroData.CardCostReductionBonus;
		_cardRevenueBonus = heroData.CardRevenueBonus;
		DoubleDiceTime.Value = heroData.DoubleDiceTime;
		for (int i = 0; i < heroData.Relic.Count; i++)
		{
			int safeByIndex = heroData.RelicCurrPoint.GetSafeByIndex(i);
			RelicInfo relicInfo = new RelicInfo(heroData.Relic[i]);
			relicInfo.BuffData.ChargeCount = safeByIndex;
			RelicList.Add(relicInfo);
			Game.GetModel<GlobalSignal>().AddRelic.Dispatch(relicInfo.BuffData.Id);
		}
		foreach (KeyValuePair<int, int> item in heroData.BuildingStarCoin)
		{
			BuildingGlodDic.Add(item.Key, item.Value);
		}
		foreach (KeyValuePair<int, int> item2 in heroData.RelicStarCoin)
		{
			RelicGlodDic.Add(item2.Key, item2.Value);
		}
	}

	public void ChangeStandLandId(int id)
	{
		StandLandId = id;
	}

	public void ChangeNextLandIds(List<int> landIds)
	{
		NextLandIds = landIds;
	}

	public void UpdateDoubleDiceTime(int changeTime)
	{
		DoubleDiceTime.Value = Mathf.Clamp(DoubleDiceTime.Value + changeTime, 0, 1);
	}

	public void SetForceFirstDicePoint(int point)
	{
		ForceFirstDicePoint = point;
	}

	public void ClearForceFirstDicePoint()
	{
		ForceFirstDicePoint = 0;
	}

	public void AddDevelopCount()
	{
		DevelopLandCount++;
	}

	public void UpdateCardCostReductionBonus(int delta)
	{
		_cardCostReductionBonus += delta;
	}

	public void UpdateCardRevenueBonus(int delta)
	{
		_cardRevenueBonus += delta;
	}

	public void ChangeAccumulateGold(int delta)
	{
	}

	public void AddSourceGold(AttributeChangeSource sourceType, int configId, int changeGold)
	{
		switch (sourceType)
		{
		case AttributeChangeSource.Building:
		{
			if (!BuildingGlodDic.TryGetValue(configId, out var value2))
			{
				value2 = 0;
			}
			value2 += changeGold;
			BuildingGlodDic[configId] = value2;
			break;
		}
		case AttributeChangeSource.Relic:
		{
			if (!RelicGlodDic.TryGetValue(configId, out var value))
			{
				value = 0;
			}
			value += changeGold;
			RelicGlodDic[configId] = value;
			break;
		}
		}
	}

	public int CalculateScore()
	{
		int num = 0;
		BoardCardManager cardManager = Game.GetSystem<BoardManager>().cardManager;
		foreach (BuildingBase buildingDatum in Game.GetModel<GameData>().BuildingData)
		{
			num += cardManager.GetRecycleCardPrice(buildingDatum.Card.UID);
		}
		int num2 = 0;
		foreach (int bagCard in Game.GetModel<GameData>().CardData.BagCards)
		{
			num2 += cardManager.GetRecycleCardPrice(bagCard);
		}
		int missionScore = Game.GetSystem<BoardManager>().missionManager.GetMissionScore();
		int num3 = Mathf.CeilToInt((float)base.Gold.Value * 0.5f);
		int num4 = num + num2 + missionScore + num3;
		Debug.Log($"总积分:{num4} = 建筑物积分：{num} + 背包卡牌积分：{num2} + 任务积分：{missionScore} + 剩余金币：{num3}");
		int score = Game.GetModel<GMData>().Score;
		if (score > 0)
		{
			return score;
		}
		return num4;
	}

	public int[] CalculateSubScores()
	{
		int num = 0;
		BoardCardManager cardManager = Game.GetSystem<BoardManager>().cardManager;
		foreach (BuildingBase buildingDatum in Game.GetModel<GameData>().BuildingData)
		{
			num += cardManager.GetRecycleCardPrice(buildingDatum.Card.UID);
		}
		int num2 = 0;
		foreach (int bagCard in Game.GetModel<GameData>().CardData.BagCards)
		{
			num2 += cardManager.GetRecycleCardPrice(bagCard);
		}
		int missionScore = Game.GetSystem<BoardManager>().missionManager.GetMissionScore();
		int num3 = Mathf.CeilToInt((float)base.Gold.Value * 0.5f);
		return new int[3]
		{
			missionScore,
			num3,
			num + num2
		};
	}

	public SingleHero GetUploadData()
	{
		SingleHero singleHero = new SingleHero();
		singleHero.HeroId = Id;
		singleHero.Gold = base.Gold.Value;
		singleHero.StandLandId = StandLandId;
		singleHero.NextLandIds.AddRange(NextLandIds);
		singleHero.DevelopLandCount = DevelopLandCount;
		singleHero.ForceFirstDicePoint = ForceFirstDicePoint;
		singleHero.CardCostReductionBonus = _cardCostReductionBonus;
		singleHero.CardRevenueBonus = _cardRevenueBonus;
		singleHero.DoubleDiceTime = DoubleDiceTime.Value;
		List<int> values = RelicList.Select((RelicInfo x) => x.BuffData.Id).ToList();
		singleHero.Relic.AddRange(values);
		List<int> values2 = RelicList.Select((RelicInfo x) => x.BuffData.ChargeCount).ToList();
		singleHero.RelicCurrPoint.AddRange(values2);
		foreach (KeyValuePair<int, int> item in BuildingGlodDic)
		{
			singleHero.BuildingStarCoin.Add(item.Key, item.Value);
		}
		foreach (KeyValuePair<int, int> item2 in RelicGlodDic)
		{
			singleHero.RelicStarCoin.Add(item2.Key, item2.Value);
		}
		return singleHero;
	}
}

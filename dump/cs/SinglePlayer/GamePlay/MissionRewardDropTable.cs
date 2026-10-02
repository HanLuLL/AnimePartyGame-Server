using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class MissionRewardDropTable
{
	private class RarityDropRate
	{
		public int Rarity;

		public int Weight;

		public readonly List<int> Rewards = new List<int>();
	}

	private readonly List<RarityDropRate> _rarityDropRates = new List<RarityDropRate>();

	private readonly List<(double Threshold, RarityDropRate Data)> _weightedList = new List<(double, RarityDropRate)>();

	private int _totalWeight;

	private readonly System.Random random;

	public MissionRewardDropTable()
	{
		random = new System.Random();
	}

	private int DrawOne()
	{
		double item = random.NextDouble() * (double)_totalWeight;
		int num = _weightedList.BinarySearch((item, null), Comparer<(double, RarityDropRate)>.Create(((double, RarityDropRate) a, (double, RarityDropRate) b) => a.Item1.CompareTo(b.Item1)));
		if (num < 0)
		{
			num = ~num;
		}
		if (num >= _weightedList.Count)
		{
			num = _weightedList.Count - 1;
		}
		if (_weightedList.Count == 0)
		{
			return 0;
		}
		RarityDropRate item2 = _weightedList[num].Data;
		return item2.Rewards[random.Next(item2.Rewards.Count)];
	}

	private void RebuildWeightedList()
	{
		_weightedList.Clear();
		double num = 0.0;
		foreach (RarityDropRate rarityDropRate in _rarityDropRates)
		{
			num += (double)rarityDropRate.Weight;
			_weightedList.Add((num, rarityDropRate));
		}
		_totalWeight = _rarityDropRates.Sum((RarityDropRate x) => x.Weight);
	}

	private void BuildCardPool(RepeatedField<int> weights)
	{
		RepeatedField<SinglePlayerCardConfigure> cards = StaticConfigure.SinglePlayer.Cards;
		Dictionary<int, RarityDropRate> dictionary = new Dictionary<int, RarityDropRate>();
		HashSet<int> selectedCardPacks = Game.GetModel<GameData>().SelectedCardPacks;
		foreach (SinglePlayerCardConfigure item in cards)
		{
			if (item.Rarity == 4)
			{
				continue;
			}
			bool flag = true;
			foreach (int item2 in item.CardPackID)
			{
				flag &= selectedCardPacks.Contains(item2);
			}
			if (!flag)
			{
				continue;
			}
			int safeByIndex = weights.GetSafeByIndex(item.Rarity);
			if (safeByIndex > 0)
			{
				if (!dictionary.TryGetValue(item.Rarity, out var value))
				{
					value = new RarityDropRate
					{
						Rarity = item.Rarity,
						Weight = safeByIndex
					};
					dictionary.Add(item.Rarity, value);
				}
				value.Rewards.Add(item.Id);
			}
		}
		_rarityDropRates.Clear();
		_rarityDropRates.AddRange(dictionary.Values.OrderBy((RarityDropRate x) => x.Rarity));
		RebuildWeightedList();
	}

	private void BuildRelicPool(RepeatedField<int> weights, List<int> exclude)
	{
		BoardRelicManager relicManager = Game.GetSystem<BoardManager>().relicManager;
		RepeatedField<SinglePlayerRelicConfigure> relics = StaticConfigure.SinglePlayer.Relics;
		Dictionary<int, RarityDropRate> dictionary = new Dictionary<int, RarityDropRate>();
		HashSet<int> selectedCardPacks = Game.GetModel<GameData>().SelectedCardPacks;
		foreach (SinglePlayerRelicConfigure item in relics)
		{
			if (relicManager.ExistRelic(item.Id) || exclude.Contains(item.Id) || (!relicManager.CanObtainColorfulRelic() && item.Rarity == 4) || weights.Count <= item.Rarity)
			{
				continue;
			}
			bool flag = true;
			foreach (int item2 in item.CardPackID)
			{
				flag &= selectedCardPacks.Contains(item2);
			}
			if (!flag)
			{
				continue;
			}
			int safeByIndex = weights.GetSafeByIndex(item.Rarity);
			if (safeByIndex > 0)
			{
				if (!dictionary.TryGetValue(item.Rarity, out var value))
				{
					value = new RarityDropRate
					{
						Rarity = item.Rarity,
						Weight = safeByIndex
					};
					dictionary.Add(item.Rarity, value);
				}
				value.Rewards.Add(item.Id);
			}
		}
		_rarityDropRates.Clear();
		_rarityDropRates.AddRange(dictionary.Values.OrderBy((RarityDropRate x) => x.Rarity));
		RebuildWeightedList();
	}

	public List<int> GetReward(int count, MissionSettleType type, RepeatedField<int> weights)
	{
		List<int> list = new List<int>();
		switch (type)
		{
		case MissionSettleType.Card:
		{
			BuildCardPool(weights);
			for (int j = 0; j < count; j++)
			{
				list.Add(DrawOne());
			}
			break;
		}
		case MissionSettleType.Relic:
		{
			for (int i = 0; i < count; i++)
			{
				BuildRelicPool(weights, list);
				int num = DrawOne();
				if (num > 0)
				{
					list.Add(num);
				}
			}
			break;
		}
		default:
			Debug.LogError($"无法 抽取任务计算奖励:{type}");
			return list;
		}
		return list;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Map;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class CardDropTable
{
	private class RarityDropRate
	{
		public int Rarity;

		public int Weight;

		public readonly List<int> Cards = new List<int>();
	}

	private class TempRarityPool
	{
		public int Weight;

		public List<int> Cards;
	}

	private readonly List<RarityDropRate> _rarityDropRates = new List<RarityDropRate>();

	private readonly List<(double Threshold, RarityDropRate Data)> _weightedList = new List<(double, RarityDropRate)>();

	private int _totalWeight;

	private readonly System.Random random;

	public CardDropTable()
	{
		random = new System.Random();
		BuildInitialPool();
	}

	public void RebuildInitialPool()
	{
		BuildInitialPool();
	}

	private void BuildInitialPool()
	{
		HashSet<int> selectedCardPacks = Game.GetModel<GameData>().SelectedCardPacks;
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData == null)
		{
			return;
		}
		RepeatedField<SinglePlayerCardConfigure> cards = StaticConfigure.SinglePlayer.Cards;
		Dictionary<int, RarityDropRate> dictionary = new Dictionary<int, RarityDropRate>();
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
			if (flag)
			{
				int safeByIndex = currentMissionData.MissionConfig.CardWeights.GetSafeByIndex(item.Rarity);
				if (!dictionary.TryGetValue(item.Rarity, out var value))
				{
					value = new RarityDropRate
					{
						Rarity = item.Rarity,
						Weight = safeByIndex
					};
					dictionary.Add(item.Rarity, value);
				}
				value.Cards.Add(item.Id);
			}
		}
		_rarityDropRates.Clear();
		_rarityDropRates.AddRange(dictionary.Values.OrderBy((RarityDropRate x) => x.Rarity));
		RebuildWeightedList();
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

	public void RefreshWeights()
	{
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData == null)
		{
			return;
		}
		foreach (RarityDropRate rarityDropRate in _rarityDropRates)
		{
			rarityDropRate.Weight = currentMissionData.MissionConfig.CardWeights.GetSafeByIndex(rarityDropRate.Rarity);
		}
		RebuildWeightedList();
	}

	public List<int> GetCards(int count)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < count; i++)
		{
			list.Add(DrawOne());
		}
		return list;
	}

	public int GetTheCardSpawnProbability(CardRarity cardRarity)
	{
		RarityDropRate rarityDropRate = _rarityDropRates.Find((RarityDropRate rate) => rate.Rarity == (int)cardRarity);
		if (rarityDropRate == null)
		{
			return 0;
		}
		return rarityDropRate.Weight * 100 / _totalWeight;
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
		RarityDropRate item2 = _weightedList[num].Data;
		return item2.Cards[random.Next(item2.Cards.Count)];
	}

	public int DrawOneByTag(SinglePlayerTagType tag)
	{
		List<TempRarityPool> list = new List<TempRarityPool>();
		foreach (RarityDropRate rarityDropRate in _rarityDropRates)
		{
			List<int> list2 = new List<int>();
			foreach (int card in rarityDropRate.Cards)
			{
				if (StaticConfigure.SinglePlayer.CardDict.TryGetValue(card, out var value) && value.CardTag.Contains(tag))
				{
					list2.Add(card);
				}
			}
			if (list2.Count > 0)
			{
				list.Add(new TempRarityPool
				{
					Weight = rarityDropRate.Weight,
					Cards = list2
				});
			}
		}
		if (list.Count == 0)
		{
			Debug.LogError($"没有可掉落的 Tag 卡牌: {tag}");
		}
		List<(double, TempRarityPool)> list3 = new List<(double, TempRarityPool)>();
		double num = 0.0;
		foreach (TempRarityPool item3 in list)
		{
			num += (double)item3.Weight;
			list3.Add((num, item3));
		}
		double item = random.NextDouble() * num;
		int num2 = list3.BinarySearch((item, null), Comparer<(double, TempRarityPool)>.Create(((double, TempRarityPool) a, (double, TempRarityPool) b) => a.Item1.CompareTo(b.Item1)));
		if (num2 < 0)
		{
			num2 = ~num2;
		}
		if (num2 >= list3.Count)
		{
			num2 = list3.Count - 1;
		}
		TempRarityPool item2 = list3[num2].Item2;
		return item2.Cards[random.Next(item2.Cards.Count)];
	}
}

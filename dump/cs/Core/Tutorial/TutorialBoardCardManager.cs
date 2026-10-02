using System;
using System.Collections.Generic;
using GameLogic;

namespace Core.Tutorial;

public class TutorialBoardCardManager
{
	private class WeightedCardPool
	{
		public int TotalWeight;

		public readonly List<(int Threshold, int CardId)> DropList = new List<(int, int)>();
	}

	private readonly List<(int CardId, int Weight)> Cards = new List<(int, int)>
	{
		(10001, 2000),
		(10003, 3000),
		(10005, 5000),
		(10004, 2000),
		(10006, 3000),
		(21002, 2000),
		(20002, 1000),
		(20008, 1000),
		(21001, 5000),
		(21006, 1000)
	};

	private WeightedCardPool CardPool;

	private Random random;

	public void Initialize()
	{
		random = new Random();
		CardPool = new WeightedCardPool();
		int num = 0;
		foreach (var card in Cards)
		{
			int item = card.CardId;
			int item2 = card.Weight;
			num += item2;
			CardPool.DropList.Add((num, item));
		}
		CardPool.TotalWeight = num;
	}

	public void Dispose()
	{
	}

	public List<HandCardData> TryGetCards(int cardCount)
	{
		List<HandCardData> list = new List<HandCardData>();
		if (cardCount <= 0)
		{
			return list;
		}
		for (int i = 0; i < cardCount; i++)
		{
			int id = DrawOneCard(random);
			list.Add(HandCardData.GetTutorialHandCardData(id));
		}
		return list;
	}

	private int DrawOneCard(Random r)
	{
		int item = r.Next(CardPool.TotalWeight);
		int num = CardPool.DropList.BinarySearch((item, 0), Comparer<(int, int)>.Create(((int Threshold, int CardId) a, (int Threshold, int CardId) b) => a.Threshold.CompareTo(b.Threshold)));
		if (num < 0)
		{
			num = ~num;
		}
		if (num >= CardPool.DropList.Count)
		{
			num = CardPool.DropList.Count - 1;
		}
		return CardPool.DropList[num].CardId;
	}
}

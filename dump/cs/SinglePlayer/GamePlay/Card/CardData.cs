using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay.Card;

public class CardData
{
	private int _refreshCount;

	private RepeatedField<int> CardPrice;

	private readonly Dictionary<int, Card> _cardDepot = new Dictionary<int, Card>();

	public List<int> BagCards { get; private set; }

	public List<int> ShopCards { get; private set; }

	public ReactiveProperty<int> FreeRefreshCount { get; private set; } = new ReactiveProperty<int>();

	public int BagSlotCount { get; private set; }

	public int ShopSellCount { get; private set; }

	public int RefreshPrice
	{
		get
		{
			SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
			if (safeByIndex == null || safeByIndex.RefreshPrice.Count == 0)
			{
				Debug.LogError("无法取得 单人玩法参数配置");
				return 0;
			}
			int safeByIndex2 = safeByIndex.RefreshPrice.GetSafeByIndex(0);
			int safeByIndex3 = safeByIndex.RefreshPrice.GetSafeByIndex(1);
			int safeByIndex4 = safeByIndex.RefreshPrice.GetSafeByIndex(2);
			return Mathf.Clamp(safeByIndex2 + _refreshCount * safeByIndex3, safeByIndex2, safeByIndex4);
		}
	}

	public Dictionary<int, Card> CardDepot => _cardDepot;

	public void Initialize(SingleGameData gameData)
	{
		SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
		if (safeByIndex == null)
		{
			Debug.LogError("无法取得 单人玩法参数配置");
			return;
		}
		BagSlotCount = safeByIndex.BagSlotCount;
		ShopSellCount = safeByIndex.ShopSellCount;
		CardPrice = safeByIndex.CardPrice;
		BagCards = Enumerable.Repeat(0, BagSlotCount).ToList();
		ShopCards = Enumerable.Repeat(0, ShopSellCount).ToList();
		if (gameData == null || gameData.CardData == null)
		{
			return;
		}
		_refreshCount = gameData.CardData.ShopRefreshCount;
		BagCards.Clear();
		BagCards.AddRange(gameData.CardData.BagCardUIDs);
		ShopCards.Clear();
		ShopCards.AddRange(gameData.CardData.ShopCardUIDs);
		foreach (KeyValuePair<int, SingleCard> item in gameData.CardData.CardDepot)
		{
			item.Deconstruct(out var key, out var value);
			int key2 = key;
			SingleCard singleCard = value;
			Card card = new Card();
			card.UID = singleCard.CardUID;
			card.CardConfigure = StaticConfigure.SinglePlayer.CardDict[singleCard.ConfigId];
			card.Level.Value = singleCard.Level;
			card.Exp.Value = singleCard.Exp;
			Card value2 = card;
			_cardDepot[key2] = value2;
		}
		foreach (int bagCard in BagCards)
		{
			if (_cardDepot.TryGetValue(bagCard, out var value3))
			{
				value3.HasPurchase = true;
			}
		}
		foreach (SingleBuilding building in gameData.BuildingData.Buildings)
		{
			if (_cardDepot.TryGetValue(building.CardUID, out var value4))
			{
				value4.HasPurchase = true;
			}
		}
	}

	public void Dispose()
	{
	}

	public void ChangeFreeRefreshCount(int delta)
	{
		FreeRefreshCount.Value += delta;
	}

	public void AddRefreshCount()
	{
		_refreshCount++;
	}

	public void ResetRefreshCount()
	{
		_refreshCount = 0;
	}

	public SingleCardData GetUploadData()
	{
		MapField<int, SingleCard> mapField = new MapField<int, SingleCard>();
		foreach (KeyValuePair<int, Card> item in _cardDepot)
		{
			item.Deconstruct(out var key, out var value);
			int num = key;
			Card card = value;
			mapField[num] = new SingleCard
			{
				CardUID = num,
				ConfigId = card.CardConfigure.Id,
				Level = card.Level.Value,
				Exp = card.Exp.Value
			};
		}
		return new SingleCardData
		{
			BagCardUIDs = { (IEnumerable<int>)BagCards },
			ShopCardUIDs = { (IEnumerable<int>)ShopCards },
			CardDepot = { (IDictionary<int, SingleCard>)mapField },
			ShopRefreshCount = _refreshCount
		};
	}
}

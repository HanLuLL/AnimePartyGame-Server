using System;
using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using UI;
using party.model;

namespace Core;

public class CardContainer
{
	private readonly long PlayerId;

	private readonly List<HandCardData> HandCards = new List<HandCardData>();

	public int PreUseCardGuid;

	private readonly Random _rand = new Random();

	public Dictionary<int, HandCardData> _HandCardData = new Dictionary<int, HandCardData>();

	public List<CardInfo> _CardInfos => HandCards.ConvertAll((HandCardData c) => new CardInfo
	{
		UniqueId = c.Guid,
		CardId = c.CardId,
		IsTemp = c.IsTemp
	});

	public List<HandCardData> _HandCards => HandCards;

	public int CardCount { get; private set; }

	public CardContainer(long playerId, RepeatedField<CardInfo> _HandCardIds)
	{
		PlayerId = playerId;
		UpdateCardCount(_HandCardIds?.Count ?? 0);
		UpdateHandCards(_HandCardIds);
	}

	public void UpdateHandCards(RepeatedField<CardInfo> _HandCardInfos)
	{
		HandCards.Clear();
		for (int i = 0; i < _HandCardInfos.Count; i++)
		{
			CardInfo cardInfo = _HandCardInfos[i];
			if (!_HandCardData.ContainsKey(cardInfo.UniqueId))
			{
				_HandCardData.Add(cardInfo.UniqueId, new HandCardData(cardInfo));
			}
			else
			{
				_HandCardData[cardInfo.UniqueId].UpdateCardData(cardInfo);
			}
			HandCards.Add(_HandCardData[cardInfo.UniqueId]);
		}
	}

	private void MaskCardIds(RepeatedField<CardInfo> cardIds)
	{
		if (cardIds != null && cardIds.Count != 0)
		{
			for (int i = 0; i < cardIds.Count; i++)
			{
				cardIds[i].CardId = -_rand.Next(1, 1000);
			}
		}
	}

	public void RemoveById(int _cardId)
	{
		int i;
		for (i = 0; i < HandCards.Count && HandCards[i].CardId != _cardId; i++)
		{
		}
		HandCards.RemoveAt(i);
	}

	public bool IsContainCard(int _cardId)
	{
		for (int i = 0; i < HandCards.Count; i++)
		{
			if (HandCards[i].CardId == _cardId)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsContainBattleCard(EffectType effectType)
	{
		foreach (HandCardData handCard in HandCards)
		{
			if (handCard.CardId.GetCardConfigure().EffectType == effectType)
			{
				return true;
			}
		}
		return false;
	}

	public HandCardData CardConvertChanged(CardInfo cardInfo)
	{
		if (cardInfo == null)
		{
			return null;
		}
		if (!_HandCardData.ContainsKey(cardInfo.UniqueId))
		{
			return null;
		}
		_HandCardData[cardInfo.UniqueId].UpdateCardData(cardInfo);
		return _HandCardData[cardInfo.UniqueId];
	}

	public void UpdateCardCount(int count)
	{
		CardCount = count;
	}
}

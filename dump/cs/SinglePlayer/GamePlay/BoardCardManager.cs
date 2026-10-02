using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.Scene;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class BoardCardManager
{
	private CardDropTable _cardDropTable;

	private CardData _cardData;

	private List<SinglePlayer.GamePlay.Card.Card> waitDisplayRemoveCards = new List<SinglePlayer.GamePlay.Card.Card>();

	public bool HasWaitDisplayRemoveCards => waitDisplayRemoveCards.Count > 0;

	public void Initialize()
	{
		_cardData = Game.GetModel<GameData>().CardData;
		_cardDropTable = new CardDropTable();
		Game.GetModel<GlobalSignal>().CardUsed.AddListener(OnCardUsed);
		Game.GetModel<GlobalSignal>().MissionStart.AddListener(OnMissionStart);
		if (Game.GetModel<GameData>().Round.Value == 0)
		{
			UpdateCardShop();
			AddInitialCardToBag();
		}
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().CardUsed.RemoveListener(OnCardUsed);
		Game.GetModel<GlobalSignal>().MissionStart.RemoveListener(OnMissionStart);
	}

	public void RebuildCardDropTableInitialPool()
	{
		_cardDropTable.RebuildInitialPool();
		UpdateCardShop();
	}

	public void ManualUpdateCardShop()
	{
		if (_cardData.FreeRefreshCount.Value > 0)
		{
			_cardData.ChangeFreeRefreshCount(-1);
		}
		else
		{
			if (!Game.GetSystem<BoardManager>().CheckGold(_cardData.RefreshPrice))
			{
				return;
			}
			Game.GetSystem<BoardManager>().characterManager.ChangeHeroGold(-_cardData.RefreshPrice).Forget();
			_cardData.AddRefreshCount();
		}
		UpdateCardShop();
	}

	private void AddInitialCardToBag()
	{
		SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
		if (safeByIndex == null)
		{
			return;
		}
		foreach (int item in safeByIndex.InitCard)
		{
			AddCardToBag(item);
		}
	}

	public bool CanRefresh()
	{
		if (_cardData.FreeRefreshCount.Value > 0)
		{
			return true;
		}
		if (Game.GetSystem<BoardManager>().CheckGold(_cardData.RefreshPrice))
		{
			return true;
		}
		return false;
	}

	public bool OnRequestPurchaseCardPutInBag(int uid, int index = -1)
	{
		if (IsBagFull())
		{
			Debug.LogError("SinglePlayer 背包已满，无法购买！");
			return false;
		}
		if (!PurchaseCard(uid))
		{
			return false;
		}
		if (!TryAddToBag(uid, index))
		{
			Debug.LogError("SinglePlayer 背包异常，无法添加卡牌！");
			return false;
		}
		return true;
	}

	private bool PurchaseCard(int uid)
	{
		if (!TryRemoveFromShop(uid))
		{
			Debug.LogError("SinglePlayer 商店中找不到该卡牌！");
			return false;
		}
		SinglePlayer.GamePlay.Card.Card cardByUID = GetCardByUID(uid);
		if (cardByUID.HasPurchase && _cardData.BagCards.Contains(uid))
		{
			Debug.LogError("SinglePlayer 该卡牌已购买！");
			return false;
		}
		if (cardByUID.HasPurchase && _cardData.BagCards.Contains(uid))
		{
			return false;
		}
		if (!Game.GetSystem<BoardManager>().CheckGold(cardByUID.BuyPrice))
		{
			return false;
		}
		Game.GetSystem<BoardManager>().characterManager.Hero.Property.ChangeGold(-cardByUID.BuyPrice);
		cardByUID.HasPurchase = true;
		return true;
	}

	public bool IsBagFull()
	{
		return _cardData.BagCards.All((int x) => x > 0);
	}

	public bool IsExistInSlot(int index)
	{
		return _cardData.BagCards.GetSafeByIndex(index) != 0;
	}

	private bool TryRemoveFromShop(int uid)
	{
		int num = _cardData.ShopCards.IndexOf(uid);
		if (num < 0)
		{
			return false;
		}
		_cardData.ShopCards[num] = 0;
		Game.GetModel<GlobalSignal>().ShopCardChange.Dispatch();
		return true;
	}

	private bool TryRemoveFromBag(int uid)
	{
		int num = _cardData.BagCards.IndexOf(uid);
		if (num < 0)
		{
			return false;
		}
		_cardData.BagCards[num] = 0;
		Game.GetModel<GlobalSignal>().BagCardChange.Dispatch();
		return true;
	}

	public bool TryAddToBag(int uid, int index = -1)
	{
		if (index == -1)
		{
			int num = _cardData.BagCards.FindIndex((int x) => x <= 0);
			if (num == -1)
			{
				return false;
			}
			_cardData.BagCards[num] = uid;
			Game.GetModel<GlobalSignal>().BagCardChange.Dispatch();
			return true;
		}
		_cardData.BagCards[index] = uid;
		Game.GetModel<GlobalSignal>().BagCardChange.Dispatch();
		return true;
	}

	public bool RecycleCard(int uid)
	{
		int recycleCardPrice = GetRecycleCardPrice(uid);
		TryRemoveFromBag(uid);
		SinglePlayer.GamePlay.Card.Card card = TryRemoveCardByUid(uid);
		if (card != null)
		{
			Game.GetSystem<BoardManager>().buildingManager.TrySellingBuilding(uid);
			Game.GetModel<GlobalSignal>().SellingCard.Dispatch(card.CardConfigure.Id, uid);
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(11, Game.GetController<SinglePlayerSceneController>().gameObject);
			Game.GetSystem<BoardManager>().characterManager.Hero.GoldChange.Dispatch(Mathf.CeilToInt(recycleCardPrice));
			Game.GetSystem<BoardManager>().characterManager.Hero.Property.ChangeGold(Mathf.CeilToInt(recycleCardPrice));
			card.Dispose();
			AttributeChangeSource sourceType = AttributeChangeSource.Building;
			int id = card.CardConfigure.Id;
			Game.GetModel<GameData>().heroProperty.AddSourceGold(sourceType, id, recycleCardPrice);
		}
		return true;
	}

	public bool TryRemoveCardOnTheTable(int uid, bool isDelayRemove = false)
	{
		SinglePlayer.GamePlay.Card.Card card = TryRemoveCardByUid(uid);
		bool result = card != null;
		if (card != null)
		{
			if (isDelayRemove)
			{
				waitDisplayRemoveCards.Add(card);
				return result;
			}
			Game.GetSystem<BoardManager>().buildingManager.RemoveBuilding(uid);
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(11, Game.GetController<SinglePlayerSceneController>().gameObject);
			card.Dispose();
		}
		return result;
	}

	public void RemoveWaitDisplayRemoveCards()
	{
		foreach (SinglePlayer.GamePlay.Card.Card waitDisplayRemoveCard in waitDisplayRemoveCards)
		{
			if (waitDisplayRemoveCard != null)
			{
				Game.GetSystem<BoardManager>().buildingManager.RemoveBuilding(waitDisplayRemoveCard.UID);
				SimpleSingletonProvider<AudioManager>.inst.SendEvent(11, Game.GetController<SinglePlayerSceneController>().gameObject);
				waitDisplayRemoveCard.Dispose();
			}
		}
		waitDisplayRemoveCards.Clear();
	}

	public int GetRecycleCardPrice(int uid)
	{
		SinglePlayer.GamePlay.Card.Card cardByUID = GetCardByUID(uid);
		if (cardByUID != null)
		{
			float cardRevenueBonus = Game.GetModel<GameData>().heroProperty.CardRevenueBonus;
			int sellAddPrice = cardByUID.GetSellAddPrice();
			return Mathf.CeilToInt(((float)(cardByUID.Price * cardByUID.Level.Value) * 0.5f + (float)sellAddPrice) * (1f + cardRevenueBonus));
		}
		return 0;
	}

	public void UpdateCardShop()
	{
		List<int> cards = _cardDropTable.GetCards(_cardData.ShopSellCount);
		_cardData.ShopCards.ForEach(delegate(int uid)
		{
			if (uid > 0)
			{
				TryRemoveCardByUid(uid);
			}
		});
		List<int> list = cards.Select((int id) => GenerateCard(id).UID).ToList();
		for (int num = 0; num < _cardData.ShopCards.Count; num++)
		{
			_cardData.ShopCards[num] = ((num < list.Count) ? list[num] : 0);
		}
		Game.GetModel<GlobalSignal>().ShopCardChange.Dispatch();
		Game.GetModel<GlobalSignal>().RefreshShop.Dispatch();
	}

	public List<int> RandomGetCards(int count)
	{
		return _cardDropTable.GetCards(count);
	}

	public SinglePlayer.GamePlay.Card.Card GetCardByUID(int uid)
	{
		if (uid == 0)
		{
			return null;
		}
		if (!_cardData.CardDepot.TryGetValue(uid, out var value))
		{
			Debug.LogError($"无法通过UID:{uid} 获取对应的卡牌");
			return null;
		}
		return value;
	}

	private SinglePlayer.GamePlay.Card.Card TryRemoveCardByUid(int uid)
	{
		if (!_cardData.CardDepot.Remove(uid, out var value))
		{
			Debug.LogError($"无法通过UID:{uid} 获取对应的卡牌");
			return null;
		}
		return value;
	}

	public SinglePlayer.GamePlay.Card.Card GenerateCard(int cardConfigId)
	{
		if (!StaticConfigure.SinglePlayer.CardDict.TryGetValue(cardConfigId, out var value))
		{
			Debug.LogError($"无法通过Id: {cardConfigId} 获取对应的卡牌");
			return null;
		}
		SinglePlayer.GamePlay.Card.Card card = new SinglePlayer.GamePlay.Card.Card
		{
			UID = UIDGenerator.NextUID(),
			CardConfigure = value
		};
		_cardData.CardDepot.TryAdd(card.UID, card);
		return card;
	}

	public void UpdateCardAfterUseCard(int uid, bool result)
	{
		if (uid <= 0 || !result)
		{
			return;
		}
		SinglePlayer.GamePlay.Card.Card cardByUID = GetCardByUID(uid);
		if (cardByUID.CardConfigure.Rarity != 4)
		{
			if (!cardByUID.HasPurchase)
			{
				PurchaseCard(uid);
			}
			else
			{
				TryRemoveFromBag(uid);
			}
		}
	}

	public int AddCardToBag(int cardConfigId)
	{
		if (IsBagFull())
		{
			int cardPrice = GetCardPrice(cardConfigId);
			Game.GetModel<GlobalSignal>().SellingCard.Dispatch(cardConfigId, 0);
			Game.GetSystem<BoardManager>().characterManager.Hero.GoldChange.Dispatch(cardPrice);
			Game.GetSystem<BoardManager>().characterManager.Hero.Property.ChangeGold(cardPrice);
		}
		SinglePlayer.GamePlay.Card.Card card = GenerateCard(cardConfigId);
		if (card == null)
		{
			return -1;
		}
		card.HasPurchase = true;
		TryAddToBag(card.UID);
		return card.UID;
	}

	private int GetCardPrice(int cardConfigId)
	{
		if (!StaticConfigure.SinglePlayer.CardDict.TryGetValue(cardConfigId, out var value))
		{
			Debug.LogError($"无法通过配置Id: {cardConfigId} 获取对应的卡牌配置");
			return 0;
		}
		SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
		if (safeByIndex == null)
		{
			return 0;
		}
		float cardRevenueBonus = Game.GetModel<GameData>().heroProperty.CardRevenueBonus;
		int safeByIndex2 = safeByIndex.CardPrice.GetSafeByIndex(value.Rarity);
		int addPrice = value.SinglePlayerCardConfigureItems.GetSafeByIndex(0).AddPrice;
		return Mathf.CeilToInt(((float)safeByIndex2 * 0.5f + (float)addPrice) * (1f + cardRevenueBonus));
	}

	private void OnCardUsed(int cardUid, bool result)
	{
		UpdateCardAfterUseCard(cardUid, result);
	}

	private void OnHeroStarChanged(int star)
	{
		_cardDropTable.RefreshWeights();
	}

	private void OnMissionStart()
	{
		_cardDropTable.RefreshWeights();
	}

	public int GetTheCardSpawnProbability(CardRarity cardRarity)
	{
		return _cardDropTable.GetTheCardSpawnProbability(cardRarity);
	}

	public void CloseShop()
	{
		for (int i = 0; i < _cardData.ShopCards.Count; i++)
		{
			if (_cardData.ShopCards[i] != 0)
			{
				TryRemoveCardByUid(_cardData.ShopCards[i]);
				_cardData.ShopCards[i] = 0;
			}
		}
		_cardData.ResetRefreshCount();
		Game.GetModel<GlobalSignal>().ShopCardChange.Dispatch();
	}

	public void AddCardToBagByTag(SinglePlayerTagType tag)
	{
		int cardConfigId = _cardDropTable.DrawOneByTag(tag);
		AddCardToBag(cardConfigId);
	}

	public void TestCardProbability()
	{
		var enumerable = from x in _cardDropTable.GetCards(1000)
			group x by x into g
			orderby g.Key
			select new
			{
				Value = g.Key,
				Count = g.Count()
			};
		string text = $"{Application.persistentDataPath}/单人玩法卡牌概率{DateTime.Now.Ticks}.csv";
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("ID,次数");
		foreach (var item in enumerable)
		{
			stringBuilder.AppendLine($"{item.Value},{item.Count}");
		}
		File.WriteAllText(text, stringBuilder.ToString(), Encoding.UTF8);
		Application.OpenURL(text);
	}
}

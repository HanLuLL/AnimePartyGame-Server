using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class AltArtCardLogic : IRPCSync, IReadPoint
{
	private AltArtCardData playerAltArtCardData = new AltArtCardData();

	private Dictionary<int, List<int>> newAltArtCards = new Dictionary<int, List<int>>();

	public readonly Signal<int> OnCardChanged = new Signal<int>();

	public readonly Signal<bool> OnFinishShowCard = new Signal<bool>();

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SetCardAltArtS2C.OnSetCardAltArtS2CServerCallBackAsync = OnSetCardAltArtS2CServer;
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemAdded.AddListener(OnItemAdded);
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SetCardAltArtS2C.OnSetCardAltArtS2CServerCallBackAsync = null;
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemAdded.RemoveListener(OnItemAdded);
	}

	public void InitFromServer(MapField<int, AltArtCardInfo> artCards)
	{
		playerAltArtCardData.InitFromServer(artCards);
		LocalLoadNewAltArtCards();
	}

	public void RegisterRed()
	{
	}

	public bool TryGetAltArtCardId(int cardId, out int altArtCardId)
	{
		return playerAltArtCardData.TryGetArtCardId(cardId, out altArtCardId);
	}

	public bool HasUnlockedAltCard(int cardId)
	{
		CardAltArtConfigure value;
		bool flag = StaticConfigure.Card.AltArtDict.TryGetValue(cardId, out value);
		if (flag)
		{
			flag = false;
			foreach (CardAltArtConfigureItem cardAltArtConfigureItem in value.CardAltArtConfigureItems)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(cardAltArtConfigureItem.AltArtId))
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	public bool HasAltCard(int cardId)
	{
		return StaticConfigure.Card.AltArtDict.ContainsKey(cardId);
	}

	public RPCAsyncResult SetCardAltArtC2S(int cardId, int artCardId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SetCardAltArtC2S.SetCardAltArtC2SCall(new SetCardAltArtC2S
		{
			CardId = cardId,
			CardFaceId = artCardId
		});
	}

	private async UniTask OnSetCardAltArtS2CServer(SetCardAltArtS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			playerAltArtCardData.SetArtCardId(model.CardId, model.CardFaceId);
			OnCardChanged.Dispatch(model.CardId);
			await UniTask.CompletedTask;
		}
	}

	public bool IsNewAltArtCard(int cardId, int altArtId)
	{
		bool result = false;
		if (newAltArtCards.TryGetValue(cardId, out var value))
		{
			result = value.Contains(altArtId);
		}
		return result;
	}

	public List<(int, bool)> GetAltArtCardInfos(int cardId)
	{
		List<(int, bool)> list = new List<(int, bool)>();
		list.Add((cardId, true));
		if (StaticConfigure.Card.AltArtDict.TryGetValue(cardId, out var value))
		{
			foreach (CardAltArtConfigureItem cardAltArtConfigureItem in value.CardAltArtConfigureItems)
			{
				bool item = SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(cardAltArtConfigureItem.AltArtId);
				list.Add((cardAltArtConfigureItem.AltArtId, item));
			}
		}
		return list;
	}

	public bool IsReceiveNewAltArtCard(int cardId)
	{
		if (newAltArtCards.TryGetValue(cardId, out var value))
		{
			return value.Count > 0;
		}
		return false;
	}

	public bool HasAnyNewAltArtCard()
	{
		return newAltArtCards.Count > 0;
	}

	public void RemoveNewAltCardTip(int cardId, int altArtId)
	{
		if (newAltArtCards.TryGetValue(cardId, out var value))
		{
			value.Remove(altArtId);
			if (value.Count == 0)
			{
				newAltArtCards.Remove(cardId);
				OnCardChanged.Dispatch(cardId);
			}
			LocalSaveNewAltArtCards();
		}
		Dictionary<int, List<int>> dictionary = newAltArtCards;
		if (dictionary != null && dictionary.Count == 0)
		{
			OnFinishShowCard.Dispatch(t: true);
		}
	}

	private void LocalSaveNewAltArtCards()
	{
		ES3.Save(LocalStore.NewAltArtCardCache, newAltArtCards);
	}

	private void LocalLoadNewAltArtCards()
	{
		if (ES3.KeyExists(LocalStore.NewAltArtCardCache))
		{
			newAltArtCards = ES3.Load<Dictionary<int, List<int>>>(LocalStore.NewAltArtCardCache);
		}
	}

	private void OnItemAdded(int itemId)
	{
		RepeatedField<CardAltArtConfigure> altArts = StaticConfigure.Card.AltArts;
		int num = -1;
		foreach (CardAltArtConfigure item in altArts)
		{
			foreach (CardAltArtConfigureItem cardAltArtConfigureItem in item.CardAltArtConfigureItems)
			{
				if (cardAltArtConfigureItem.AltArtId == itemId)
				{
					num = item.CardId;
					break;
				}
			}
			if (num >= 0)
			{
				break;
			}
		}
		if (num >= 0)
		{
			if (!newAltArtCards.TryGetValue(num, out var value))
			{
				value = new List<int>();
				newAltArtCards[num] = value;
			}
			value.Add(itemId);
			LocalSaveNewAltArtCards();
		}
	}
}

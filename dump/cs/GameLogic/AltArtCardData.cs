using System.Collections.Generic;
using Google.Protobuf.Collections;
using party.model;

namespace GameLogic;

public class AltArtCardData
{
	private Dictionary<int, int> altArtCardDic = new Dictionary<int, int>();

	public void InitFromServer(MapField<int, AltArtCardInfo> artCards)
	{
		altArtCardDic.Clear();
		foreach (KeyValuePair<int, AltArtCardInfo> artCard in artCards)
		{
			altArtCardDic.Add(artCard.Value.CardId, artCard.Value.CardFaceId);
		}
	}

	public bool TryGetArtCardId(int cardId, out int artCardId)
	{
		bool result = altArtCardDic.TryGetValue(cardId, out artCardId);
		if (artCardId == cardId || artCardId == 0)
		{
			artCardId = cardId;
			result = false;
		}
		return result;
	}

	public void SetArtCardId(int cardId, int artCardId)
	{
		altArtCardDic[cardId] = artCardId;
	}
}

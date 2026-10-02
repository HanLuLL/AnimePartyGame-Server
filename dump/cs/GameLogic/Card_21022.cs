using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using UI;

namespace GameLogic;

public class Card_21022 : Card
{
	public Card_21022()
	{
		cardId = 21022;
		config = cardId.GetCardConfigure();
	}

	public override UniTask CardAction(long _Sn)
	{
		return base.CardAction(_Sn);
	}

	public override UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		return base.CardCallBack(PlayerId, TargetIds, reverse, OriginalCardId);
	}

	public override string CardDescription(long playerId, HandCardData handCardData)
	{
		string text = base.CardDescription(playerId);
		if (handCardData == null)
		{
			return text;
		}
		int num = config.Params[3] - handCardData.PurifyNum;
		if (handCardData.PurifyNum > 0)
		{
			return text.Replace($"stack={config.Params[3]}", $"stack=[color=#94FF46]{num}[/color]");
		}
		return text.Replace($"stack={config.Params[3]}", $"stack={num}");
	}
}

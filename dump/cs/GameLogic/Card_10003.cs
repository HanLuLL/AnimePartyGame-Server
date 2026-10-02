using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using UI;

namespace GameLogic;

public class Card_10003 : Card
{
	public Card_10003()
	{
		cardId = 10003;
		config = cardId.GetCardConfigure();
	}

	public override UniTask CardAction(long _Sn)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		return UniTask.CompletedTask;
	}
}

using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_20015 : Card
{
	public Card_20015()
	{
		cardId = 20015;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<GameLogicManager>.inst.action.UsableCards = null;
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RequestUseQuickCard(_Sn, SimpleSingletonProvider<GameLogicManager>.inst.card.prePlayerId);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult_QuickCard(config, PlayerId, TargetIds, OriginalCardId);
	}
}

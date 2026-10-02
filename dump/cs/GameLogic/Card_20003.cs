using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_20003 : Card
{
	public Card_20003()
	{
		cardId = 20003;
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
		CardWindow cardWin = await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult();
		await cardWin.RefreshResult_QuickCard(config, PlayerId, TargetIds, OriginalCardId);
		cardWin.HideImmediately();
	}
}

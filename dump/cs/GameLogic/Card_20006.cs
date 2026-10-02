using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_20006 : Card
{
	public Card_20006()
	{
		cardId = 20006;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RequestUseEffectCard(_Sn);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}
}

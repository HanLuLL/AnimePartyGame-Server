using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21023 : Card
{
	public Card_21023()
	{
		cardId = 21023;
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

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).CharacterInst.SwitchCamera();
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager010 mapGimmickManager)
		{
			mapGimmickManager.PlayEffectFireworksPro();
		}
	}
}

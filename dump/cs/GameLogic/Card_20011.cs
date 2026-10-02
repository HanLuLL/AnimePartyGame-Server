using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_20011 : Card
{
	public Card_20011()
	{
		cardId = 20011;
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
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(PlayerId);
		playerDataById.CharacterInst.ResetFromLandId(-1);
		playerDataById.CharacterInst.ShowWalkDirections(null);
	}
}

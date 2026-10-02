using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_20028 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_20028()
	{
		cardId = 20028;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectPlayer(GetTargetPlayers(), config.Params[1], config.Params[0], _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		if (_self.CharacterInst == null || _target.CharacterInst == null)
		{
			return false;
		}
		return SimpleSingletonProvider<LandManager>.inst.CheckDistance(_self.CharacterInst.standLand.Id, _target.CharacterInst.standLand.Id, config.Params[0] + _self.Property.CardAddDistance.Value, 0);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override void CardScope(bool state)
	{
		showPlayers = GetTargetPlayers();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.showSelectPlayer.Dispatch(showPlayers, state);
	}

	public override string CardDescription(long playerId)
	{
		string text = base.CardDescription(playerId);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null)
		{
			return text;
		}
		int value = playerDataById.Property.CardAddDistance.Value;
		if (value <= 0)
		{
			return text;
		}
		return text.Replace($"range={config.Params[0]}", $"range=[color=#94FF46]{config.Params[0] + value}[/color]");
	}
}

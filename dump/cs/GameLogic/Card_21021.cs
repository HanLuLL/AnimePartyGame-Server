using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Card_21021 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21021()
	{
		cardId = 21021;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTarget(GetTargetPlayers(), cardId, 1, _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		if (_self.CharacterInst == null || _target.CharacterInst == null)
		{
			return false;
		}
		return _target.player.Hero.HeroId == 1050;
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override async void CardScope(bool state)
	{
		showPlayers = GetTargetPlayers();
		if (showPlayers.Count == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.battlePreMonster.HideImmediately();
		}
		else if (state)
		{
			await SimpleSingletonProvider<UIManager>.inst.battlePreMonster.PreviewMonsterTarget(showPlayers);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.battlePreMonster.HideImmediately();
		}
	}
}

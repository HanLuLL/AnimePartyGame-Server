using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21008 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21008()
	{
		cardId = 21008;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectPlayer(GetTargetPlayers(), 1, -1, _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		return true;
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

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: true);
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, config.ReleaseDefaultPerform, "支援卡 开始");
		if (perform.isCancel)
		{
			return;
		}
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.EffectDatas[i].Place != null)
			{
				playerData.CharacterInst.SendCharacter(model.EffectDatas[i].Place.Place.NodeId, model.EffectDatas[i].Place.Place.FrontNodeIds);
				break;
			}
		}
		await perform.PlayPlayerShow(model.PlayerId, config.TargetDefaultPerform, "支援卡 结束");
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
	}
}

using Core.Tutorial;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21006 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21006()
	{
		cardId = 21006;
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

	public override async UniTask<bool> TutorialCardEffect(long actionPlayer, UseEffectCardC2S msg)
	{
		if (!(await base.TutorialCardEffect(actionPlayer, msg)))
		{
			return false;
		}
		long targetPlayerId = msg.TargetIds.GetSafeByIndex(0);
		int safeByIndex = config.Params.GetSafeByIndex(1);
		RepeatedField<long> targetLimitPlayers = GetTargetLimitPlayers(targetPlayerId);
		UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = cardId
			},
			PlayerId = targetPlayerId
		};
		foreach (long item in targetLimitPlayers)
		{
			HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(item, safeByIndex);
			updateHeroAttrS2C.EffectDatas.Add(hpUpdate);
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(updateHeroAttrS2C);
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(targetPlayerId))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerId);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.SHORTINFO, playerDataById, 50000));
		}
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return true;
	}

	private RepeatedField<long> GetTargetLimitPlayers(long targetPlayerId)
	{
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerId);
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType == CharacterType.Hero && playerData.Property.HP.Value > 0)
			{
				if (playerData.player.Id == targetPlayerId)
				{
					repeatedField.Add(targetPlayerId);
				}
				else if (SimpleSingletonProvider<LandManager>.inst.CheckDistance(playerDataById.CharacterInst.standLand.Id, playerData.CharacterInst.standLand.Id, config.Params[0], 0))
				{
					repeatedField.Add(playerData.player.Id);
				}
			}
		}
		return repeatedField;
	}
}

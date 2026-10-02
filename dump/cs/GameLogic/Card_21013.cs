using Core.Tutorial;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21013 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21013()
	{
		cardId = 21013;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTarget(GetTargetPlayers(), cardId, config.Params[1], _Sn);
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

	public override bool VailStatus()
	{
		return GetTargetPlayers().Count > 0;
	}

	public override async UniTask<bool> TutorialCardEffect(long actionPlayer, UseEffectCardC2S msg)
	{
		if (!(await base.TutorialCardEffect(actionPlayer, msg)))
		{
			return false;
		}
		long targetPlayerId = msg.TargetIds.GetSafeByIndex(0);
		int safeByIndex = config.Params.GetSafeByIndex(2);
		UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = cardId
			},
			PlayerId = targetPlayerId
		};
		TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
		HeroAttrEffect hpAttrData = characterManager.GetHpUpdate(targetPlayerId, safeByIndex);
		updateHeroAttrS2C.EffectDatas.Add(hpAttrData);
		await characterManager.CreateUpdateAttrData(updateHeroAttrS2C);
		if (hpAttrData.Hp.CurrHp <= 0)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerId);
			if (playerDataById != null && playerDataById.Property.gold.Value > 0)
			{
				HeroAttrEffect goldUpdate = characterManager.GetGoldUpdate(actionPlayer, playerDataById.Property.gold.Value);
				UpdateHeroAttrS2C updateHeroAttrS2C2 = new UpdateHeroAttrS2C
				{
					Cause = new CauseOrigin(),
					PlayerId = actionPlayer
				};
				updateHeroAttrS2C2.EffectDatas.Add(goldUpdate);
				await characterManager.CreateUpdateAttrData(updateHeroAttrS2C2);
			}
		}
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return true;
	}
}

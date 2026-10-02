using Core;
using Core.Camera;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class BattlePlayerMarkMessage : BattleMessage
{
	private Effect _signalEffect;

	public BattlePlayerMarkMessage(BattlePlayerData _playerData, long playerId, int markId)
		: base(_playerData)
	{
		ReadyMsg(playerId, markId);
	}

	public BattlePlayerMarkMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 3 && long.TryParse(_msg[1], out var result) && int.TryParse(_msg[2], out var result2))
		{
			ReadyMsg(result, result2);
		}
	}

	private void ReadyMsg(long playerId, int markId)
	{
		MsgType = MessageType.PLAYERMARK;
		SendMsg = $"{(int)MsgType}, {playerId}, {markId}";
		UIWeight = 1;
		if (StaticConfigure.Chat.MarkDict.TryGetValue(markId, out var value))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null)
			{
				ShowSignalEffect(value, playerDataById).Forget();
			}
			shortInfo = GetMsg(markId, playerId);
		}
	}

	private async UniTask ShowSignalEffect(ChatMarkConfigure mark, BattlePlayerData targetPlayer)
	{
		if (mark.EffctID != 0 && targetPlayer.CharacterInst != null)
		{
			base.ShowAddress = true;
			_signalEffect = await targetPlayer.CharacterInst.PlayCharacterEffect(mark.EffctID);
			_signalEffect.transform.position += Vector3.up * 5f;
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.ManagerMarkEffect(Sender.player.Id, _signalEffect);
		}
	}

	public override void ShowAddressInfo()
	{
		base.ShowAddressInfo();
		if (_signalEffect is GameCycleEffect gameCycleEffect && gameCycleEffect.ResetEffectTimer())
		{
			SimpleSingletonProvider<CameraManager>.inst.ControlFreeCamera(gameCycleEffect.transform.position);
		}
	}

	public static string GetMsg(int markId, long playerId)
	{
		if (StaticConfigure.Chat.MarkDict.TryGetValue(markId, out var value))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			object[] array = new object[2];
			if (playerDataById != null)
			{
				string text = CharacterHandle.GetCharacterName(playerDataById.player.characterConfig.Id, playerDataById.characterType);
				if (playerDataById.characterType == CharacterType.Monster)
				{
					text += $" ({playerDataById.player.Hero.MonsterIndex})";
				}
				string text2 = GameConfig.HTMLStringRGB(playerDataById.player.Slot);
				array[0] = "[color=#" + text2 + "]" + text + "[/color]";
				if (markId == 40007)
				{
					int cardCount = playerDataById.cardContainer.CardCount;
					array[1] = $"[color=#{text2}]{cardCount}[/color]";
				}
			}
			return string.Format(value.ChatInfo, array);
		}
		return null;
	}
}

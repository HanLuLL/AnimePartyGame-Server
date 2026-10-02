using Core;
using Core.Camera;
using Core.Unit;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class BattleLandMessage : BattleMessage
{
	private Effect _signalEffect;

	public BattleLandMessage(BattlePlayerData _playerData, int markId, int LandId)
		: base(_playerData)
	{
		ReadyMsg(markId, LandId);
	}

	public BattleLandMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 3 && int.TryParse(_msg[1], out var result) && int.TryParse(_msg[2], out var result2))
		{
			ReadyMsg(result, result2);
		}
	}

	private async void ReadyMsg(int markId, int LandId)
	{
		base.ShowAddress = true;
		MsgType = MessageType.LANDMARK;
		SendMsg = $"{(int)MsgType}, {markId}, {LandId}";
		UIWeight = 1;
		if (StaticConfigure.Chat.MarkDict.TryGetValue(markId, out var value))
		{
			shortInfo = value.ChatInfo;
			Vector3 position = SimpleSingletonProvider<LandManager>.inst.GetLandById(LandId).transform.position + Vector3.up * 5f;
			_signalEffect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(value.EffctID, position, Quaternion.identity);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.ManagerMarkEffect(Sender.player.Id, _signalEffect);
		}
		else
		{
			Debug.LogError($"Chat.Mark中 无法获取 标记Id:{markId}的配置");
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
}

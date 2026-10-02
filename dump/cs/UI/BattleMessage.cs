using Core;
using GameLogic;
using Tools;

namespace UI;

public class BattleMessage
{
	public MessageType MsgType;

	public readonly ExpressionData expressionData;

	public string shortInfo;

	public readonly BattlePlayerData Sender;

	public int UIWeight;

	public string SendMsg;

	public bool ShowAddress { get; protected set; }

	public BattleMessage(BattlePlayerData _playerData)
	{
		Sender = _playerData;
	}

	public BattleMessage(MessageType messageType, BattlePlayerData _playerData, int id)
	{
		MsgType = messageType;
		Sender = _playerData;
		switch (messageType)
		{
		case MessageType.EXPRESSION:
			UIWeight = 2;
			expressionData = SimpleSingletonProvider<CharacterAssetManager>.inst.GetExpressionTexture(Sender.player.Hero.HeroId, id);
			break;
		case MessageType.SHORTINFO:
			UIWeight = 1;
			shortInfo = id.GetLocal(UIStringType.Chat);
			break;
		}
	}

	public BattleMessage(BattlePlayerData _playerData, string chat)
	{
		MsgType = MessageType.SHORTINFO;
		Sender = _playerData;
		UIWeight = 1;
		shortInfo = chat;
	}

	public BattleMessage(BattlePlayerData _playerData, string chat, MessageType messageType)
	{
		MsgType = messageType;
		Sender = _playerData;
		UIWeight = 1;
		shortInfo = chat;
	}

	public virtual void ShowAddressInfo()
	{
	}
}

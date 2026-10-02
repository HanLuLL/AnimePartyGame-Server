using GameLogic;
using Tools;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_104 : CharacterShowComponent
{
	private BuffEffectQueue _messageQueue;

	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		_messageQueue = new BuffEffectQueue(character);
		OnBuffChange();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(OnBuffChange);
	}

	public override void Dispose()
	{
		base.Dispose();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(OnBuffChange);
		_messageQueue.Stop();
	}

	private void OnBuffChange()
	{
		Buff buff = Owner.player.buffContainer?.GetBuff(1041101);
		if (buff != null)
		{
			_messageQueue.Enqueue(buff.Progress);
		}
	}
}

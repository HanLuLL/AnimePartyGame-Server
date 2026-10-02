using GameLogic;
using Tools;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_125 : CharacterShowComponent
{
	private BuffEffectQueue_125 _messageQueue;

	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		_messageQueue = new BuffEffectQueue_125(character);
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
		Buff buff = Owner.player.buffContainer?.GetBuff(1251201);
		if (buff != null)
		{
			_messageQueue.Enqueue(buff.Progress);
		}
	}
}

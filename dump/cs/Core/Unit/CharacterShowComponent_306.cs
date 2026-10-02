using GameLogic;
using Tools;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_306 : CharacterShowComponent
{
	private BuffEffectQueue_306 _messageQueue;

	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		_messageQueue = new BuffEffectQueue_306(character);
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
		object obj = Owner.player.buffContainer?.GetBuff(3061101);
		Buff buff = Owner.player.buffContainer?.GetBuff(3061201);
		if (obj == null)
		{
			obj = buff;
		}
		Buff buff2 = (Buff)obj;
		if (buff2 != null)
		{
			_messageQueue.Enqueue(buff2.Progress);
		}
	}
}

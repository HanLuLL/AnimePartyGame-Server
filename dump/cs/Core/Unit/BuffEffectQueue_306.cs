using Cysharp.Threading.Tasks;
using Tools;

namespace Core.Unit;

public class BuffEffectQueue_306 : MessageQueueAsync<int>
{
	private Effect _buffEffect;

	private readonly Character _owner;

	public BuffEffectQueue_306(Character owner)
	{
		_owner = owner;
	}

	protected override async UniTask HandleMessage(int buffProgress, bool cancel)
	{
		if (cancel)
		{
			return;
		}
		if (buffProgress != 0)
		{
			if (_buffEffect == null)
			{
				_buffEffect = await _owner.PlayCharacterEffect(30603);
			}
			return;
		}
		if (_buffEffect != null)
		{
			_buffEffect.ReleaseEffect();
		}
		_buffEffect = null;
	}

	public override void Stop()
	{
		_buffEffect = null;
		base.Stop();
	}
}

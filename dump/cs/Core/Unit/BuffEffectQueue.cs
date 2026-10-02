using Cysharp.Threading.Tasks;
using Tools;

namespace Core.Unit;

public class BuffEffectQueue : MessageQueueAsync<int>
{
	private Effect _buffEffect;

	private readonly Character _owner;

	public BuffEffectQueue(Character owner)
	{
		_owner = owner;
	}

	protected override async UniTask HandleMessage(int buffProgress, bool cancel)
	{
		if (cancel)
		{
			return;
		}
		if (buffProgress == 2)
		{
			if (_buffEffect == null)
			{
				_buffEffect = await _owner.PlayCharacterEffect(10402);
			}
			return;
		}
		if (_buffEffect != null)
		{
			_buffEffect.ReleaseEffect();
			_owner.PlayCharacterEffect(10401).Forget();
		}
		_buffEffect = null;
	}

	public override void Stop()
	{
		_buffEffect = null;
		base.Stop();
	}
}

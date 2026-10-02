namespace SinglePlayer.GamePlay.BuffSystem;

public abstract class BuffEffectBase : IBuffEffect
{
	private BuffBase _buff;

	public BuffEffectBase(BuffBase buff)
	{
		_buff = buff;
	}

	public abstract void Execute();
}

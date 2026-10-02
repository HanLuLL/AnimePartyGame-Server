using Core.Unit;
using Cysharp.Threading.Tasks;
using party.model;

namespace Core;

public class BangNiStealthEffectHandler : IBuffEffectHandler
{
	public async UniTask Play(Buff buff, Character target)
	{
		target.SetSpriteAlpha(Character.AlphaSourceType.BangNi, 0.5f);
		await UniTask.CompletedTask;
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		target.RemoveSpriteAlpha(Character.AlphaSourceType.BangNi);
	}

	public void Dispose()
	{
	}
}

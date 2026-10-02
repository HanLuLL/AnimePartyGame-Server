using Core.Unit;
using Cysharp.Threading.Tasks;
using party.model;

namespace Core;

public interface IBuffEffectHandler
{
	UniTask Play(Buff buff, Character target);

	UniTask UpdateEffect(Buff buff, Character target);

	void DestroyEffect(Buff buff, Character target);

	void Dispose();
}

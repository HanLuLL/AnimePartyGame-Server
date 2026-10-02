using Cysharp.Threading.Tasks;
using party.model;

namespace Core.Tutorial.Buff;

public abstract class BaseBuffModule
{
	public abstract UniTask Apply(party.model.Buff buff);
}

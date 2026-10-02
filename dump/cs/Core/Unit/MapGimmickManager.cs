using Cysharp.Threading.Tasks;

namespace Core.Unit;

public abstract class MapGimmickManager : Unit
{
	public abstract void Initialize();

	public abstract void RefreshGimmickData(int groupId, int statusId);

	public abstract UniTask SwitchGimmick(int groupId, int statusId, bool wait = true);
}

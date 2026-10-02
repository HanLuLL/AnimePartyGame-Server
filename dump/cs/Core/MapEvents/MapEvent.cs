using Cysharp.Threading.Tasks;
using party.protocol;

namespace Core.MapEvents;

public abstract class MapEvent
{
	public MapEventInfoConfigure config;

	public virtual void MonsterShow()
	{
	}

	public virtual async UniTask MapEventShowByAttr(UpdateHeroAttrS2C model)
	{
		await UniTask.CompletedTask;
	}

	public virtual async UniTask MapEventShowByEvent()
	{
		await UniTask.CompletedTask;
	}

	public virtual async UniTask MapEventShowByEvent_Front()
	{
		await UniTask.CompletedTask;
	}
}

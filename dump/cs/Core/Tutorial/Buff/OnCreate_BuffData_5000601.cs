using Cysharp.Threading.Tasks;
using Tools;
using party.model;

namespace Core.Tutorial.Buff;

public class OnCreate_BuffData_5000601 : BaseBuffModule
{
	public override async UniTask Apply(party.model.Buff buff)
	{
		int id = buff.Source.Id;
		if (StaticConfigure.Relic.InfoDict.TryGetValue(id, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			TutorialGame.GetSystem<TutorialBoardManager>().gameManager.AddBunsPoint(safeByIndex, buff.UniqueId);
		}
		await UniTask.CompletedTask;
	}
}

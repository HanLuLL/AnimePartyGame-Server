using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Core.Tutorial;

[Preserve]
public class TutorialMonster_1005 : TutorialBaseMonster
{
	public override async UniTask StartEncounterEvent(long playerId, long target)
	{
		await TutorialGame.GetSystem<TutorialBoardManager>().gameManager.StartPK(playerId, target);
	}
}

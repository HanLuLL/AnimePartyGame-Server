using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public interface IKillMonsterMission
{
	UniTask TryUpdateKillMonsterProgress(long playerId);
}

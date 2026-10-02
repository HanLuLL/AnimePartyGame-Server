using Cysharp.Threading.Tasks;

namespace GameLogic;

public interface IChoiceEffectCard
{
	UniTask ActiveEffectCardAfterChoose(int ChoiceCardId, long actionSn);
}

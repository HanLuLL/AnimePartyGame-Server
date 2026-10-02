using Cysharp.Threading.Tasks;
using Tools;

namespace SinglePlayer.GamePlay.Character;

public interface ICharacter : IChild<CharacterLogic>
{
	UniTask DoHit(int value);

	UniTask DoDamage(int value);

	UniTask DoDefend(int value);

	UniTask DoDodge(int value);

	UniTask DoDead();
}

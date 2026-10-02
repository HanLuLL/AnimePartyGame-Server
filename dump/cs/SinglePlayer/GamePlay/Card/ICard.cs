using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Card;

public interface ICard
{
	UniTask UseCard();
}

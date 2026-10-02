using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay;

public interface IFireBullet
{
	UniTask FireBullet(int bulletId);
}

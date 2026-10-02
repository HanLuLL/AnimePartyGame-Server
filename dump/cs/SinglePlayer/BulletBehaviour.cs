using System;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay;
using UnityEngine;
using UnityEngine.Playables;

namespace SinglePlayer;

[Serializable]
public class BulletBehaviour : PlayableBehaviour
{
	[SerializeField]
	private int _bulletId;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		PlayableDirector playableDirector = playable.GetPlayableDirector();
		if (_bulletId <= 0)
		{
			Debug.LogError($"子弹不存在：{_bulletId}");
		}
		else
		{
			((Component)(object)playableDirector).GetComponent<IFireBullet>()?.FireBullet(_bulletId).Forget();
		}
	}
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

public class BattleEffectManager : MonoBehaviour
{
	public List<GameObject> battleElements = new List<GameObject>();

	public Transform KillVFXRoot;

	public Transform SceneCamLock;

	public void TryAddBattleElement(GameObject battleElement)
	{
		battleElements.Add(battleElement);
	}

	public void DestroyBattleElement()
	{
		if (battleElements == null || battleElements.Count <= 0)
		{
			return;
		}
		foreach (GameObject battleElement in battleElements)
		{
			Object.Destroy(battleElement);
		}
		battleElements.Clear();
	}

	public async UniTask TryShowKillVfx(FashionEffectConfigure killVfxConfig, PlayableDirector victimDirector)
	{
		Transform parent = (killVfxConfig.IsRoot ? KillVFXRoot : ((Component)(object)victimDirector).transform);
		(await SimpleSingletonProvider<EffectManager>.inst.PlayById(killVfxConfig.KillVfx, Vector3.zero, Quaternion.identity, parent)).transform.localPosition = new Vector3(0f, 0f, 0f);
	}
}

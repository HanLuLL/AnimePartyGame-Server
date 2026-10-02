using System;
using Core.Scene;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class HideDefenderBehaviour : PlayableBehaviour
{
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
		if (directorManager != null)
		{
			directorManager.victim.effectParent.gameObject.SetActive(value: false);
			directorManager.victim.battleInfoUI.gameObject.SetActive(value: false);
			directorManager.victim.actorRenderer.gameObject.SetActive(value: false);
		}
		else
		{
			Debug.LogError("无法获取对战中防守方的数据");
		}
		base.OnBehaviourPlay(playable, info);
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
		if (directorManager != null)
		{
			directorManager.victim.effectParent.gameObject.SetActive(value: true);
			directorManager.victim.battleInfoUI.gameObject.SetActive(value: true);
			directorManager.victim.actorRenderer.gameObject.SetActive(value: true);
		}
		else
		{
			Debug.LogError("无法获取对战中防守方的数据");
		}
		base.OnBehaviourPause(playable, info);
	}
}

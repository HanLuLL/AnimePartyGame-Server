using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.Scene;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class DiceLandComponent_Start : DiceLandComponent
{
	public DiceLandComponent_Start(int landId)
		: base(landId)
	{
		effectType = DiceLandEffectType.StopMoveAndStepOnEffect;
	}

	public override UniTask PassByEffect()
	{
		Debug.Log("经过地图格：FillingStation");
		return UniTask.CompletedTask;
	}

	public override UniTask StepOnEffect()
	{
		Game.GetSystem<BoardManager>().cardManager.UpdateCardShop();
		Debug.Log("踩中地图格：FillingStation");
		SimpleSingletonProvider<AudioManager>.inst.SendEvent(52, Game.GetController<SinglePlayerSceneController>().gameObject);
		return UniTask.CompletedTask;
	}
}

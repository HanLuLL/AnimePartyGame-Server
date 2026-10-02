using Core.Scene;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Mark;

public class MarkModeController
{
	public InteractionMode CurrentMode { get; private set; }

	public bool IsActive => IsInMode(InteractionMode.Marking);

	public bool IsInMode(InteractionMode mode)
	{
		return CurrentMode == mode;
	}

	public void EnterMarkMode()
	{
		if (CurrentMode != InteractionMode.Marking)
		{
			CurrentMode = InteractionMode.Marking;
			Debug.Log("进入标记状态");
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkEnter?.Dispatch();
			if ((object)BattleSceneController.inst?.freeObject != null)
			{
				BattleSceneController.inst.freeObject.UpdateStatus(FreeCameraStatus.MapSignal);
			}
		}
	}

	public void ExitMarkMode()
	{
		MarkInputConsume.Consume();
		if (CurrentMode != InteractionMode.Normal)
		{
			CurrentMode = InteractionMode.Normal;
			Debug.Log("离开标记状态");
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkExit?.Dispatch();
			if ((object)BattleSceneController.inst?.freeObject != null)
			{
				BattleSceneController.inst.freeObject.UpdateStatus();
			}
		}
	}
}

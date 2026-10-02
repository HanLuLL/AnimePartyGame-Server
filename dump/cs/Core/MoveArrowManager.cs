using System.Collections.Generic;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class MoveArrowManager : SimpleSingletonProvider<MoveArrowManager>
{
	private readonly List<MoveArrow> _arrowPool = new List<MoveArrow>(4);

	public List<MoveArrow> ArrowPool => _arrowPool;

	private Transform parent => BattleSceneController.inst.SceneUI.transform;

	private void CallFirstBtn()
	{
		if (_arrowPool.Count > 0 && _arrowPool[0] != null && _arrowPool[0].transform.gameObject.activeSelf)
		{
			((GButton)_arrowPool[0].GetComponentInChildren<UIPanel>().ui).onClick.Call();
		}
	}

	private async UniTask CreateMoveArrow(int standLandId, int targetLandId, long _actionSn)
	{
		if (!(await SimpleSingletonProvider<EffectManager>.inst.PlayById(11, Vector3.zero, Quaternion.identity, parent) is MoveArrow moveArrow))
		{
			Debug.LogError("无法通过id:11获取对应的特效");
			return;
		}
		moveArrow.UpdateArrow(standLandId, targetLandId, _actionSn);
		_arrowPool.Add(moveArrow);
		moveArrow.Arrow.onClick.Set(delegate(EventContext context)
		{
			CloseArrow();
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapType == 10)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMove(targetLandId);
			}
			else if (_actionSn != 0L)
			{
				RequestMove(context, _actionSn, targetLandId);
				_actionSn = 0L;
			}
		});
	}

	private async void RequestMove(EventContext context, long _actionSn, int _landId)
	{
		GButton btn = (GButton)context.sender;
		btn.onClick.Retain();
		BattlePlayerData currentPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetCurrentPlayer();
		if (currentPlayer != null && currentPlayer.CharacterInst != null)
		{
			await currentPlayer.CharacterInst.SwitchCamera();
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestMoveC2S(_actionSn, _landId);
		btn.onClick.Release();
	}

	public void CloseArrow()
	{
		for (int i = 0; i < _arrowPool.Count; i++)
		{
			if (_arrowPool[i] != null)
			{
				_arrowPool[i].ReleaseEffect();
			}
		}
		_arrowPool.Clear();
	}

	public bool IsSelectDir()
	{
		return _arrowPool.Count > 0;
	}

	public async UniTask DealMove(long _actionPlayerId, long _actionSn, bool ForceDir)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_actionPlayerId);
		if (playerData != null && !(playerData.CharacterInst == null))
		{
			await CreateArrow(_actionSn, _actionPlayerId, playerData.CharacterInst.standLand.Id, ForceDir ? (-1) : playerData.CharacterInst.fromLandId);
			if (playerData.characterType == CharacterType.Hero && playerData.CharacterInst.canStep > 0)
			{
				await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(playerData.CharacterInst, playerData.CharacterInst.canStep, ForceDir);
			}
		}
	}

	private async UniTask CreateArrow(long _actionSn, long playerId, int standLandId, int formLandId)
	{
		List<int> landIds = SimpleSingletonProvider<LandManager>.inst.GetLandById(standLandId).CanSelectedLandId(formLandId);
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			if (landIds.Count > 1)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(playerId, 11015);
			}
		}
		else if (landIds.Count > 1)
		{
			CloseArrow();
			for (int i = 0; i < landIds.Count; i++)
			{
				await CreateMoveArrow(standLandId, landIds[i], _actionSn);
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById.CharacterInst != null)
			{
				SimpleSingletonProvider<CameraManager>.inst.EnableFreeCamera(playerDataById.CharacterInst.GetCharacterCamera());
			}
			OperationTimer.ActionDownTime(_actionSn, 5027, CallFirstBtn);
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestMoveC2S(_actionSn, landIds[0]);
		}
	}

	public void ShowArrowEffect(int startLandId, int endLandId)
	{
		for (int i = 0; i < _arrowPool.Count; i++)
		{
			if (_arrowPool[i].StartLandId == startLandId && _arrowPool[i].EndLandId == endLandId)
			{
				_arrowPool[i].ShowArrowEffect();
			}
		}
	}

	public void ShowSuggestArrow(int startLandId, int endLandId, int curStar)
	{
		for (int i = 0; i < _arrowPool.Count; i++)
		{
			if (_arrowPool[i].StartLandId == startLandId && _arrowPool[i].EndLandId == endLandId)
			{
				_arrowPool[i].ShowSuggestEffect();
				_arrowPool[i].ShowUpgradeArrow(curStar);
			}
		}
	}

	public void CloseArrowEffect()
	{
		for (int i = 0; i < _arrowPool.Count; i++)
		{
			_arrowPool[i].CloseArrowEffect();
		}
	}

	public async UniTask DealTutorialMove(BattlePlayerData player, int standLandId, int targetLandId)
	{
		await CreateMoveArrow(standLandId, targetLandId, 0L);
		await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(player.CharacterInst, player.CharacterInst.canStep);
	}
}

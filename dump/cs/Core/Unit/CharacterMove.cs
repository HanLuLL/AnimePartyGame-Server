using System;
using System.Collections.Generic;
using System.Linq;
using Core.Camera;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class CharacterMove
{
	private readonly Character _owner;

	private UnitLand nextStandLand;

	private Queue<int> pathQueue;

	private readonly Dictionary<int, Effect> _walkEffects = new Dictionary<int, Effect>();

	private Effect walkSkinEffect;

	private readonly List<Effect> _walkDirections = new List<Effect>();

	private float _offset => SimpleSingletonProvider<GameLogicManager>.inst.battle.mapCharacterOffsetHeight;

	public CharacterMove(Character self)
	{
		_owner = self;
	}

	private async UniTask Move(bool _End, bool isCancel)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady)
		{
			return;
		}
		CharacterCamera vCamera = _owner.vCamera;
		Queue<int> queue = pathQueue;
		vCamera.SetMoveDamping(queue == null || queue.Count != 0);
		Queue<int> queue2 = pathQueue;
		if (queue2 != null && queue2.Count == 0)
		{
			if (_walkEffects.Count > 0)
			{
				foreach (var (_, effect2) in _walkEffects)
				{
					if (effect2 != null)
					{
						effect2.ReleaseEffect();
					}
				}
				_walkEffects.Clear();
			}
			if (_End || _owner.canStep == 0)
			{
				_owner.characterAnimator.Move(walk: false);
				await _owner.StopMove();
			}
			else if (!(_owner.showComponent is IMoveAnimRule moveAnimRule) || moveAnimRule.ShouldStopMoveAnim())
			{
				_owner.characterAnimator.Move(walk: false);
			}
			if (_End || _owner.canStep == 0 || _owner.standLand.IsNeedStop())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(_owner, willMove: false);
				if (_owner.player.characterType == CharacterType.Hero)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.showLandTip.Dispatch(_owner.standLand.Id);
					LandType landType = _owner.standLand.LandType;
					if (landType == LandType.Shop || landType == LandType.Pveshop)
					{
						SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.SHOP, _owner.player.Id);
					}
					else
					{
						landType = _owner.standLand.LandType;
						if (landType == LandType.Event || landType == LandType.Destiny || landType == LandType.Divination)
						{
							SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.EVENT, _owner.player.Id);
						}
					}
				}
				ShowWalkDirections(null).Forget();
				SimpleSingletonProvider<LandManager>.inst.GetLandById(_owner.standLand.Id).LandShowFlash();
				nextStandLand = null;
			}
			if (!_owner.standLand.IsNeedStop() && _owner.standLand.IsSelectedDir())
			{
				ShowWalkDirections(null).Forget();
			}
			return;
		}
		DeleteWalkDirection();
		if (isCancel)
		{
			if (pathQueue != null)
			{
				List<int> list = pathQueue.ToList();
				int num2 = list[list.Count - 1];
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(num2);
				if (pathQueue.Count > 1)
				{
					List<int> list2 = pathQueue.ToList();
					int landId = list2[list2.Count - 2];
					_owner.ResetFromLandId(landId);
				}
				if (_End || _owner.canStep == 0)
				{
					_owner.StopMove(moveAttr: false).Forget();
				}
				_owner.ResetStandLand(landById);
				Vector3 localPosition = landById.transform.localPosition;
				_owner.transform.position = new Vector3(localPosition.x, _offset, localPosition.z);
				SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapMove.Dispatch(_owner.player.Id, num2);
				_owner.characterAnimator.Move(walk: false);
				pathQueue = null;
			}
		}
		else
		{
			if (!_walkEffects.ContainsKey(8))
			{
				Effect value = await _owner.PlayCharacterEffect(8);
				_walkEffects.Add(8, value);
			}
			int moveVfxId = _owner.player.GetMoveEffectIdBySkin();
			if (moveVfxId > 0 && !_walkEffects.ContainsKey(moveVfxId))
			{
				Effect value2 = await _owner.PlayCharacterEffect(moveVfxId);
				_walkEffects.Add(moveVfxId, value2);
			}
			nextStandLand = GetNextLandId(pathQueue?.Dequeue() ?? 0);
			await MoveToPositionAsync(_End);
		}
	}

	private async UniTask MoveToPositionAsync(bool _End)
	{
		if (nextStandLand.Id != _owner.standLand.Id)
		{
			Transform characterObject = _owner.characterObject;
			float direction = GetDirection(nextStandLand.transform.position - _owner.transform.position, characterObject.localScale);
			Vector3 targetScale = new Vector3(direction, characterObject.localScale.y, characterObject.localScale.z);
			if (characterObject.localScale != targetScale)
			{
				CtsInfo rotateMoveCancelToken = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
				if (walkSkinEffect != null)
				{
					walkSkinEffect.transform.localScale = Vector3.zero;
				}
				await DOTweenAsyncExtensions.WithCancellation((Tween)characterObject.DOScale(targetScale, 1f / _owner.rotateSpeed), rotateMoveCancelToken.Token);
				UpdateCharacterScale(targetScale);
				if (walkSkinEffect != null)
				{
					walkSkinEffect.transform.localScale = Vector3.one;
				}
				if (rotateMoveCancelToken.IsCancellationRequested)
				{
					await Move(_End, isCancel: true);
					return;
				}
				SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(rotateMoveCancelToken);
			}
			if (await _owner.standLand.EnableHastenRoadManager(_owner, nextStandLand))
			{
				await Move(_End, isCancel: true);
				return;
			}
			float moveSpeedAdditionRate = _owner.showComponent.GetMoveSpeedAdditionRate();
			_owner.characterAnimator.Move(walk: true, moveSpeedAdditionRate);
			Vector3 targetPos = new Vector3(nextStandLand.X, _offset, nextStandLand.Z);
			CtsInfo moveCancelToken = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			float num = Vector3.Distance(targetPos, _owner.transform.position);
			await DOTweenAsyncExtensions.WithCancellation((Tween)_owner.transform.DOMove(targetPos, num / (_owner.movementSpeed * moveSpeedAdditionRate)).SetEase(Ease.Linear), moveCancelToken.Token);
			if (moveCancelToken.IsCancellationRequested)
			{
				await Move(_End, isCancel: true);
				return;
			}
			SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(moveCancelToken);
			OnMoveUpdate(nextStandLand);
			_owner.transform.position = targetPos;
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapMove.Dispatch(_owner.player.Id, nextStandLand.Id);
		}
		_owner.ResetStep(_owner.canStep - 1);
		_owner.ResetStandLand(nextStandLand);
		await SimpleSingletonProvider<GameLogicManager>.inst.action.TryShowMoveSkillEffect(_owner.player.Id, _owner.standLand.Id);
		await Move(_End, isCancel: false);
	}

	private void OnMoveUpdate(UnitLand unitLand)
	{
		if (!(unitLand == null))
		{
			SimpleSingletonProvider<EffectManager>.inst.PlayById(10, unitLand.transform.position, Quaternion.identity, unitLand.transform).Forget();
		}
	}

	private UnitLand GetNextLandId(int landId)
	{
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(landId);
		_owner.ResetFromLandId(Convert.ToInt32(_owner.standLand.Id));
		return landById;
	}

	public float GetDirection(Vector3 dir, Vector3 _scale)
	{
		Vector3 normalized = dir.normalized;
		if ((double)Mathf.Abs(Mathf.Abs(normalized.x) - Mathf.Abs(normalized.z)) < 0.1)
		{
			if (normalized.x > 0f)
			{
				return 0f - Mathf.Abs(_scale.x);
			}
			return Mathf.Abs(_scale.x);
		}
		if (!(normalized.x + normalized.z > 0f))
		{
			return Mathf.Abs(_scale.x);
		}
		return 0f - Mathf.Abs(_scale.x);
	}

	public float GetWalkDirection()
	{
		UnitLand landById = nextStandLand;
		if (landById == null)
		{
			int nextLandId = _owner.standLand.GetNextLandId(_owner.fromLandId);
			landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(nextLandId);
		}
		Vector3 normalized = (landById.transform.position - _owner.transform.position).normalized;
		float num = Vector2.Dot(new Vector2(1f, -1f), new Vector2(normalized.x, normalized.z));
		if ((double)Mathf.Abs(num) < 0.1)
		{
			if (normalized.z > 0f)
			{
				return -1f;
			}
			return 1f;
		}
		return (!(num >= 0f)) ? 1 : (-1);
	}

	public async UniTask ExecuteMove(Queue<int> _queue, bool _End)
	{
		pathQueue = null;
		if (_queue != null && _owner.canStep == 0)
		{
			_owner.ResetStep(_queue.Count);
		}
		pathQueue = _queue;
		await Move(_End, isCancel: false);
	}

	public void Dispose()
	{
		_walkDirections.Clear();
	}

	public void SendCharacter(int nodeId, RepeatedField<int> FrontIds)
	{
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(nodeId);
		Vector3 localPosition = landById.transform.localPosition;
		_owner.transform.position = new Vector3(localPosition.x, _offset, localPosition.z);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.minimapMove.Dispatch(_owner.player.Id, nodeId);
		SimpleSingletonProvider<LandManager>.inst.GetLandById(_owner.standLand.Id).LandCloseFlash();
		_owner.ResetStep(0);
		_owner.ResetStandLand(landById);
		_owner.ResetFromLandId(_owner.standLand.GetFromLandId(FrontIds));
		UnitLand landById2 = SimpleSingletonProvider<LandManager>.inst.GetLandById((_owner.fromLandId == -1) ? FrontIds[0] : _owner.fromLandId);
		if (landById2 != null)
		{
			Vector3 localPosition2 = landById2.transform.localPosition;
			float direction = GetDirection(localPosition - localPosition2, _owner.characterObject.localScale);
			Vector3 targetScale = new Vector3(direction, _owner.characterObject.localScale.y, _owner.characterObject.localScale.z);
			UpdateCharacterScale(targetScale);
		}
		_owner.gameObject.SetActiveEx(active: true);
		_owner.player.UpdateFrontIds(FrontIds);
		ShowWalkDirections(_owner.player.FrontIds).Forget();
		SimpleSingletonProvider<LandManager>.inst.GetLandById(_owner.standLand.Id).LandShowFlash();
	}

	private void UpdateCharacterScale(Vector3 targetScale)
	{
		Transform characterObject = _owner.characterObject;
		Vector3 localScale = (_owner.CharacterEffectParent.localScale = targetScale);
		characterObject.localScale = localScale;
	}

	public async UniTask ShowWalkDirections(List<int> frontIds)
	{
		DeleteWalkDirection();
		List<int> landIds = frontIds ?? _owner.standLand.CanSelectedLandId(_owner.fromLandId);
		if (landIds.Count > 0)
		{
			for (int i = 0; i < landIds.Count; i++)
			{
				Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(37, Vector3.zero, Quaternion.identity, _owner.EffectContainer, null, _owner.standLand.transform.localScale.x);
				int index = ((_owner.player.characterType == CharacterType.Monster) ? 4 : _owner.player.Slot);
				effect.SetColor(GameConfig.slotColor[index]);
				Vector3 vector = SimpleSingletonProvider<LandManager>.inst.GetLandById(landIds[i]).transform.localPosition - _owner.standLand.transform.localPosition;
				effect.transform.forward = new Vector3(vector.x, 0f, vector.z);
				_walkDirections.Add(effect);
			}
		}
	}

	private void DeleteWalkDirection()
	{
		if (_walkDirections.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < _walkDirections.Count; i++)
		{
			if (_walkDirections[i] != null)
			{
				_walkDirections[i].ReleaseEffect();
			}
		}
		_walkDirections.Clear();
	}
}

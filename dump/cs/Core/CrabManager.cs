using System;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core;

public class CrabManager : Core.Unit.Unit
{
	private PlayableDirector _director;

	public Action PlayAttrChange;

	[SerializeField]
	private TimelineAsset _firstPathTimelineAsset;

	[SerializeField]
	private TimelineAsset _secondPathTimelineAsset;

	[SerializeField]
	private TimelineAsset _attackTimelineAsset;

	[SerializeField]
	private TimelineAsset _downTimelineAsset;

	[SerializeField]
	private Animator _CrabAnimator;

	private int _currentStatus = 1;

	protected override void Awake()
	{
		_director = base.gameObject.GetComponent<PlayableDirector>();
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.CrabHit.AddListener(CrabHit);
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			RoomInfo localRoom = roomController.localRoom;
			if (localRoom != null && localRoom.info != null && roomController.localRoom.info.CampId != 0)
			{
				SetCrabStatus();
				PlayDown();
			}
			base.Awake();
		}
	}

	protected override void OnDestroy()
	{
		(SimpleSingletonProvider<GameLogicManager>.inst.battle?.signal)?.CrabHit.RemoveListener(CrabHit);
		base.OnDestroy();
	}

	private async UniTask<bool> Play(TimelineAsset timelineAsset, DirectorWrapMode mode)
	{
		_director.Play((PlayableAsset)(object)timelineAsset, mode);
		int millisecondsDelay = (int)Mathf.Ceil((float)_director.duration * 1000f);
		return await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay);
	}

	public async UniTask<bool> PlayMove(int statusId)
	{
		if (_currentStatus != statusId)
		{
			_currentStatus = statusId;
			bool result = false;
			switch (statusId)
			{
			case 0:
				result = await Play(_secondPathTimelineAsset, DirectorWrapMode.None);
				break;
			case 1:
				result = await Play(_firstPathTimelineAsset, DirectorWrapMode.None);
				break;
			}
			return result;
		}
		return true;
	}

	public async UniTask<bool> PlayAttack(Action attrChange)
	{
		PlayAttrChange = attrChange;
		return await Play(_attackTimelineAsset, DirectorWrapMode.None);
	}

	public void PlayDown()
	{
		Play(_downTimelineAsset, DirectorWrapMode.None).Forget();
	}

	public void CrabHit()
	{
		PlayAttrChange?.Invoke();
	}

	public void SetCrabStatus()
	{
		_CrabAnimator.SetBool("StateChange", true);
	}
}

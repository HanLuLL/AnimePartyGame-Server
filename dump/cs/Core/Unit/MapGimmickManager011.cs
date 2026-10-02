using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

public class MapGimmickManager011 : MapGimmickManager
{
	[SerializeField]
	public LandGroupManager LandGroupManager;

	[SerializeField]
	private TimelineAsset _Bridge_Up;

	[SerializeField]
	private TimelineAsset _Bridge_Down;

	private PlayableDirector _Director;

	private Animator _Animator;

	private bool _DirectorPlaying
	{
		get
		{
			if (_Director.state == PlayState.Playing)
			{
				return _Director.time < _Director.duration;
			}
			return false;
		}
	}

	protected override void Awake()
	{
		_Animator = GetComponent<Animator>();
		_Director = GetComponent<PlayableDirector>();
		Initialize();
		base.Awake();
	}

	public override void Initialize()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room == null)
		{
			return;
		}
		MapField<int, int> mapField = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.info.MapStatus;
		if (mapField == null || mapField.Count <= 0)
		{
			return;
		}
		_Director.Stop();
		foreach (KeyValuePair<int, int> item in mapField)
		{
			SwitchGimmick(item.Key, item.Value, wait: false).Forget();
		}
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
		LandGroupManager.SwitchStatus(groupId, statusId);
	}

	public override async UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		RefreshGimmickData(groupId, statusId);
		LandGroup landGroup = LandGroupManager.GetLandGroup(groupId);
		switch (groupId)
		{
		case 1:
			switch (statusId)
			{
			case 0:
			{
				if (wait)
				{
					await Play(_Bridge_Up, DirectorWrapMode.Hold);
				}
				Animator animator2 = _Animator;
				if (animator2 != null)
				{
					animator2.SetBool("Sink", false);
				}
				BattleLogic battle2 = SimpleSingletonProvider<GameLogicManager>.inst.battle;
				if (battle2 == null)
				{
					break;
				}
				for (int l = 0; l < battle2.PlayerDatas.Count; l++)
				{
					Character characterInst2 = battle2.PlayerDatas[l].CharacterInst;
					if (!(characterInst2 == null))
					{
						characterInst2.ShowWalkDirections(null);
					}
				}
				break;
			}
			case 1:
			{
				List<BattlePlayerData> _list = new List<BattlePlayerData>();
				BattleLogic battle = SimpleSingletonProvider<GameLogicManager>.inst.battle;
				if (battle != null)
				{
					for (int j = 0; j < battle.PlayerDatas.Count; j++)
					{
						Character characterInst = battle.PlayerDatas[j].CharacterInst;
						if (!(characterInst == null))
						{
							UnitLand standLand = characterInst.standLand;
							if (landGroup.Lands.Contains(standLand))
							{
								characterInst.transform.parent = standLand.transform;
								_list.Add(battle.PlayerDatas[j]);
							}
							else
							{
								characterInst.ShowWalkDirections(null);
							}
						}
					}
				}
				if (wait)
				{
					await Play(_Bridge_Down, DirectorWrapMode.Hold);
				}
				Animator animator = _Animator;
				if (animator != null)
				{
					animator.SetBool("Sink", true);
				}
				for (int k = 0; k < _list.Count; k++)
				{
					_list[k].CharacterInst.transform.parent = null;
				}
				break;
			}
			}
			break;
		case 2:
		case 3:
		{
			for (int i = 0; i < landGroup.Lands.Count; i++)
			{
				landGroup.Lands[i].RefreshDisplayByGimmick();
			}
			break;
		}
		}
	}

	private async UniTask Play(TimelineAsset _timelineAsset, DirectorWrapMode wrapMode = DirectorWrapMode.None)
	{
		foreach (PlayableBinding output in ((PlayableAsset)(object)_timelineAsset).outputs)
		{
			Object genericBinding = _Director.GetGenericBinding(output.sourceObject);
			if (genericBinding != null)
			{
				_Director.SetGenericBinding(output.sourceObject, genericBinding);
			}
		}
		_Director.Play((PlayableAsset)(object)_timelineAsset, wrapMode);
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => _DirectorPlaying);
	}

	public async void Test()
	{
		await SwitchGimmick(1, 1);
		await UniTask.Delay(3000);
		await SwitchGimmick(1, 0);
		await UniTask.Delay(3000);
		await SwitchGimmick(2, 1);
		await UniTask.Delay(3000);
		await SwitchGimmick(2, 0);
	}
}

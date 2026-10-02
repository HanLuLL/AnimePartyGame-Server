using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Core.Unit;

public class MapGimmickManager012 : MapGimmickManager
{
	private const int HeroId = 1020;

	private const int Chain_LinkId = 102001;

	private PlayableDirector _Director;

	[SerializeField]
	private TimelineAsset _GimmickTimeline;

	[SerializeField]
	public GameObject DoorInst;

	private BattlePlayerData TargetMonster;

	private SoulLinkEffect Chain_LinkEffect;

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
		_Director = GetComponent<PlayableDirector>();
		base.Awake();
	}

	public override void Initialize()
	{
		BattlePlayerData battlePlayerData = null;
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].player.Hero == null)
			{
				return;
			}
			if (playerDatas[i].player.Hero.HeroId == 1020)
			{
				battlePlayerData = playerDatas[i];
				break;
			}
		}
		if (battlePlayerData != null && battlePlayerData.Property != null && battlePlayerData.Property.HP.Value > 0 && battlePlayerData.CharacterInst != null)
		{
			TargetMonster = battlePlayerData;
			CreateChain_Link().Forget();
		}
	}

	protected void Update()
	{
		if (TargetMonster == null && SimpleSingletonProvider<GameLogicManager>.inst.battle != null)
		{
			Initialize();
		}
		if (TargetMonster != null && TargetMonster.Property != null && TargetMonster.Property.HP.Value == 0 && Chain_LinkEffect != null)
		{
			if (Chain_LinkEffect != null)
			{
				SimpleSingletonProvider<EffectManager>.inst.Stop(102001, Chain_LinkEffect.gameObject);
				Chain_LinkEffect = null;
			}
			SwitchGimmick(0, 0).Forget();
		}
	}

	private async UniTask CreateChain_Link()
	{
		if (Chain_LinkEffect == null)
		{
			Chain_LinkEffect = (await SimpleSingletonProvider<EffectManager>.inst.GetEffectInstance(102001, null)).GetComponent<SoulLinkEffect>();
		}
		if (TargetMonster.CharacterInst != null)
		{
			Chain_LinkEffect.UpdateTarget(TargetMonster.CharacterInst.gameObject, DoorInst);
			Debug.Log("成功创建保安锁链");
		}
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override async UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		if ((Object)(object)_GimmickTimeline != null)
		{
			await Play(_GimmickTimeline, DirectorWrapMode.Hold);
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
		_Director.Stop();
		Debug.Log("成功打开大门");
	}
}

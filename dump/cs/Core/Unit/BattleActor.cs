using System;
using System.Collections.Generic;
using System.Threading;
using Core.Scene;
using Core.Sprite;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityTimer;
using party.model;

namespace Core.Unit;

[RequireComponent(typeof(PlayableDirector))]
public class BattleActor : Unit
{
	private DirectorWrapMode directorWrapMode;

	private readonly Dictionary<string, PlayableBinding> _directorBindingDict = new Dictionary<string, PlayableBinding>();

	[SerializeField]
	public GameObject actorRenderer;

	[SerializeField]
	public Transform effectParent;

	[SerializeField]
	private SpriteShake spriteShake;

	public UIPanel battleInfoUI;

	public UIFight_PlayerInfo _UI;

	private OffsetCurveAsset offsetAsset;

	private Vector3 curPos;

	private float offsetTime;

	private float victimFrameMultiplier = 1f;

	private BattleResourceInfoConfigure _battleActionConfig;

	private List<TimelineAsset> _battleActionTimelines;

	private BattlePlayerData playerData;

	private List<Effect> buffEffects;

	private PKCampType campType;

	private CancellationTokenSource playTimeTscCancel;

	private Timer spriteShakeTimer;

	private Timer stopTimers;

	public PlayableDirector player { get; private set; }

	private bool DirectorPlaying
	{
		get
		{
			if (player.state == PlayState.Playing)
			{
				return directorWrapMode == DirectorWrapMode.None;
			}
			return false;
		}
	}

	protected override void Awake()
	{
		player = GetComponent<PlayableDirector>();
		actorRenderer = ((Component)(object)GetComponentInChildren<Animator>()).gameObject;
		effectParent = base.transform.Find("EffectParent").transform;
		spriteShake = actorRenderer.GetComponent<SpriteShake>();
		buffEffects = new List<Effect>();
	}

	public void RefreshUI(bool _isAttack, long _PlayerId)
	{
		battleInfoUI = GetComponentInChildren<UIPanel>();
		battleInfoUI.ui.displayObject.cachedTransform.localPosition = (_isAttack ? new Vector3(-292.5f, 459f, 0f) : new Vector3(-112.5f, 459f, 0f));
		battleInfoUI.ui.InvalidateBatchingState();
		_UI = battleInfoUI.ui as UIFight_PlayerInfo;
		_UI?.InitState(_isAttack, _PlayerId);
		SortingGroup component = battleInfoUI.GetComponent<SortingGroup>();
		if (component != null && playerData != null)
		{
			component.sortingOrder = playerData.player.standingPainting.BattleResConfig.InfoLayer;
		}
	}

	protected virtual void Play(TimelineAsset asset, DirectorWrapMode _directorWrapMode)
	{
		actorRenderer.transform.localScale = new Vector3(playerData.player.standingPainting.SkinScale[1], playerData.player.standingPainting.SkinScale[1], 1f);
		if (playTimeTscCancel != null)
		{
			playTimeTscCancel.Cancel();
			playTimeTscCancel.Dispose();
			playTimeTscCancel = null;
		}
		foreach (PlayableBinding output in ((PlayableAsset)(object)asset).outputs)
		{
			if (output.sourceObject is AnimationTrack)
			{
				player.SetGenericBinding(output.sourceObject, (UnityEngine.Object)actorRenderer);
				continue;
			}
			if (output.sourceObject is OffsetTrack)
			{
				player.SetGenericBinding(output.sourceObject, (UnityEngine.Object)this);
				continue;
			}
			UnityEngine.Object genericBinding = player.GetGenericBinding(output.sourceObject);
			if (genericBinding != null)
			{
				player.SetGenericBinding(output.sourceObject, genericBinding);
			}
		}
		directorWrapMode = _directorWrapMode;
		player.Play((PlayableAsset)(object)asset, _directorWrapMode);
		if (player.playableGraph.IsValid())
		{
			Playable rootPlayable = player.playableGraph.GetRootPlayable(0);
			if (rootPlayable.IsValid())
			{
				rootPlayable.SetSpeed(BattleConfig.RoleAnimatorSpeed);
			}
		}
	}

	public void SetBattleShowAsVictim(OffsetCurveAsset _asset, SpriteShakeAsset shakeAsset, float shakeDuration, float _victimDuration, float _victimFrameMultiplier, bool victimDead)
	{
		if ((object)shakeAsset != null)
		{
			spriteShake.StartShake(shakeAsset, shakeDuration);
		}
		offsetAsset = null;
		if (!victimDead)
		{
			Timer obj = spriteShakeTimer;
			if (obj != null)
			{
				obj.Cancel();
			}
			offsetAsset = _asset;
			curPos = base.transform.localPosition;
			victimFrameMultiplier = _victimFrameMultiplier;
			spriteShakeTimer = Timer.Register(0f, _victimDuration, (System.Action)delegate
			{
				victimFrameMultiplier = 1f;
			}, (System.Action)null, (System.Action)null, (System.Action)null, (System.Action)null, (Action<float>)null, (System.Action)null, false, -1f, false, (GameObject)null);
		}
	}

	protected void Update()
	{
		if ((object)BattleSceneController.inst != null && BattleSceneController.inst.directorManager.IsHit() && !(offsetAsset == null))
		{
			Vector3 zero = Vector3.zero;
			AnimationCurve xCurve = offsetAsset.xCurve;
			if (xCurve != null && xCurve.length > 0)
			{
				zero.x = offsetAsset.xCurve.Evaluate(offsetTime);
			}
			xCurve = offsetAsset.yCurve;
			if (xCurve != null && xCurve.length > 0)
			{
				zero.y = offsetAsset.yCurve.Evaluate(offsetTime);
			}
			xCurve = offsetAsset.zCurve;
			if (xCurve != null && xCurve.length > 0)
			{
				zero.z = offsetAsset.zCurve.Evaluate(offsetTime);
			}
			base.transform.localPosition = curPos + zero;
			offsetTime += victimFrameMultiplier * Time.deltaTime;
		}
	}

	public void UpdateTimeline(PKCampType _campType, BattlePlayerData _playerData, int initPosX)
	{
		campType = _campType;
		playerData = _playerData;
		base.transform.localPosition = new Vector3((float)initPosX * 0.001f, 0f, 0f);
		_battleActionConfig = _playerData.player.standingPainting.CharacterBattlleRes.GetTimelineAssetName();
		_battleActionTimelines = SimpleSingletonProvider<CharacterAssetManager>.inst.GetTimelineAsset(_playerData.player.Hero.HeroId);
		if (_campType == PKCampType.Attacker || _campType == PKCampType.Defender)
		{
			PlayIdle();
			switch (_campType)
			{
			case PKCampType.Attacker:
				SetSortingOrder(_battleActionConfig.AtkLayer);
				break;
			case PKCampType.Defender:
				SetSortingOrder(_battleActionConfig.DefLayer);
				break;
			}
		}
	}

	public void PlayIdle()
	{
		string text = null;
		if (campType == PKCampType.Attacker)
		{
			text = _battleActionConfig.BattleIdleAttack;
		}
		else
		{
			if (campType != PKCampType.Defender)
			{
				return;
			}
			text = _battleActionConfig.BattleIdleDefense;
		}
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(text);
		if (!((UnityEngine.Object)(object)battleActionTimeline == null))
		{
			Play(battleActionTimeline, DirectorWrapMode.Loop);
		}
	}

	public void PlayBattleIdleAttackEnd()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.BattleIdleattackEnd);
		if ((UnityEngine.Object)(object)battleActionTimeline == null)
		{
			PlayIdle();
		}
		else
		{
			Play(battleActionTimeline, DirectorWrapMode.Loop);
		}
	}

	public async UniTask<bool> PlayAttack()
	{
		MapField<int, string> attack = _battleActionConfig.Attack;
		if (attack == null || attack.Count == 0)
		{
			return false;
		}
		string text = TryGetAttackTimelineAsset(attack);
		if (text == null)
		{
			return false;
		}
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(text);
		if ((UnityEngine.Object)(object)battleActionTimeline == null)
		{
			return true;
		}
		Play(battleActionTimeline, DirectorWrapMode.None);
		return !(await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying));
	}

	private string TryGetAttackTimelineAsset(MapField<int, string> config)
	{
		if (playerData == null || playerData.CharacterInst == null)
		{
			return null;
		}
		return playerData.CharacterInst.showComponent.TryGetAttackTimelineAsset(config);
	}

	public async UniTask PlayHit()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.Hurt);
		if (!((UnityEngine.Object)(object)battleActionTimeline == null))
		{
			Play(battleActionTimeline, DirectorWrapMode.None);
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
			PlayIdle();
		}
	}

	public async UniTask PlayDodge()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.Dodge);
		if ((UnityEngine.Object)(object)battleActionTimeline == null)
		{
			return;
		}
		if (player.state == PlayState.Playing && player.playableAsset != null)
		{
			PlayableAsset playableAsset = player.playableAsset;
			TimelineAsset val = (TimelineAsset)(object)((playableAsset is TimelineAsset) ? playableAsset : null);
			if (val != null && ((UnityEngine.Object)(object)val).name == ((UnityEngine.Object)(object)battleActionTimeline).name)
			{
				return;
			}
		}
		Play(battleActionTimeline, DirectorWrapMode.None);
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
		PlayIdle();
	}

	public void PlayDead()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.Dead);
		if (!((UnityEngine.Object)(object)battleActionTimeline == null))
		{
			Play(battleActionTimeline, DirectorWrapMode.Loop);
		}
	}

	public void PlayWin()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.Win);
		if (!((UnityEngine.Object)(object)battleActionTimeline == null))
		{
			Play(battleActionTimeline, DirectorWrapMode.Loop);
		}
	}

	public async UniTask PlayMove()
	{
		TimelineAsset moveStartTimelineAsset = GetBattleActionTimeline(_battleActionConfig.Movestart);
		if ((UnityEngine.Object)(object)moveStartTimelineAsset != null)
		{
			Play(moveStartTimelineAsset, DirectorWrapMode.None);
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
			TimelineAsset val = (TimelineAsset)player.playableAsset;
			if (val != null && ((UnityEngine.Object)(object)val).name != ((UnityEngine.Object)(object)moveStartTimelineAsset).name)
			{
				return;
			}
		}
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.Move);
		if (!((UnityEngine.Object)(object)battleActionTimeline == null))
		{
			Play(battleActionTimeline, DirectorWrapMode.Loop);
		}
	}

	public async void PlayFabfareAttack()
	{
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.BattleFanfareAttack);
		if ((UnityEngine.Object)(object)battleActionTimeline != null)
		{
			Play(battleActionTimeline, DirectorWrapMode.None);
			playTimeTscCancel?.Dispose();
			playTimeTscCancel = new CancellationTokenSource();
			if (!(await UniTask.WaitWhile(() => DirectorPlaying, PlayerLoopTiming.Update, playTimeTscCancel.Token).SuppressCancellationThrow()))
			{
				PlayIdle();
			}
			playTimeTscCancel = null;
		}
	}

	public async UniTask<bool> PlayChainHit()
	{
		if (campType == PKCampType.ChainAttacker)
		{
			base.gameObject.SetActiveEx(active: true);
		}
		TimelineAsset battleActionTimeline = GetBattleActionTimeline(_battleActionConfig.BattleChainAttack);
		if ((UnityEngine.Object)(object)battleActionTimeline == null)
		{
			return true;
		}
		Play(battleActionTimeline, DirectorWrapMode.None);
		bool result = await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => DirectorPlaying);
		if (campType == PKCampType.ChainAttacker)
		{
			base.gameObject.SetActiveEx(active: false);
		}
		return result;
	}

	public string GetHitDirect()
	{
		return _battleActionConfig.HitDirect;
	}

	public string GetDetachDirect()
	{
		return _battleActionConfig.DetachDirect;
	}

	public string GetApproachDirect()
	{
		return _battleActionConfig.ApproachDirect;
	}

	private TimelineAsset GetBattleActionTimeline(string timelineName)
	{
		return _battleActionTimelines?.Find((TimelineAsset x) => ((UnityEngine.Object)(object)x).name == timelineName);
	}

	public void PlayStopped(float duration, float speedMultiplier)
	{
		Timer obj = stopTimers;
		if (obj != null)
		{
			obj.Cancel();
		}
		if (!player.playableGraph.IsValid())
		{
			return;
		}
		Playable playable = player.playableGraph.GetRootPlayable(0);
		if (playable.IsValid())
		{
			float originalSpeed = (float)playable.GetSpeed();
			float speed = originalSpeed * speedMultiplier;
			ChangeTimelineSpeed(playable, speed);
			Timer val = Timer.Register(0f, duration, (System.Action)delegate
			{
				ChangeTimelineSpeed(playable, originalSpeed);
			}, (System.Action)null, (System.Action)delegate
			{
				ChangeTimelineSpeed(playable, originalSpeed);
			}, (System.Action)null, (System.Action)null, (Action<float>)null, (System.Action)null, false, -1f, false, (GameObject)null);
			stopTimers = val;
		}
	}

	private void ChangeTimelineSpeed(Playable playable, float speed)
	{
		if (playable.IsValid())
		{
			playable.SetSpeed(speed);
		}
		ChangeTargetAnimationSpeed(speed);
	}

	private void ChangeTargetAnimationSpeed(float targetSpeed)
	{
		BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
		if (!(battleShowDirector == null))
		{
			PKElementAnimationTarget[] componentsInChildren = battleShowDirector.GetComponentsInChildren<PKElementAnimationTarget>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ChangeAnimatorSpeed(targetSpeed);
			}
		}
	}

	public void ClearState()
	{
		offsetTime = 0f;
		offsetAsset = null;
	}

	public async UniTask CreateBuffEffect(Dictionary<long, Buff> _buffs)
	{
		foreach (KeyValuePair<long, Buff> _buff in _buffs)
		{
			BuffInfoConfigure buffConfigure = _buff.Value.BuffId.GetBuffConfigure();
			if (buffConfigure.IsShowEffectDuringPK && buffConfigure.EffectID != 0)
			{
				Effect item = await SimpleSingletonProvider<EffectManager>.inst.PlayById(buffConfigure.EffectID, Vector3.zero, Quaternion.identity, effectParent, null, 0.25f);
				buffEffects.Add(item);
			}
		}
	}

	public void DestroyBuffEffect()
	{
		if (buffEffects == null || buffEffects.Count == 0)
		{
			return;
		}
		for (int i = 0; i < buffEffects.Count; i++)
		{
			if (buffEffects[i] != null)
			{
				buffEffects[i].ReleaseEffect();
			}
		}
		buffEffects.Clear();
	}

	public int GetSortingOrder()
	{
		if (actorRenderer != null)
		{
			return actorRenderer.GetComponent<SpriteRenderer>().sortingOrder;
		}
		return 0;
	}

	public void SetSortingOrder(int order)
	{
		if (actorRenderer != null)
		{
			actorRenderer.GetComponent<SpriteRenderer>().sortingOrder = order;
		}
	}

	public void ClosePlatform()
	{
		_battleActionTimelines = null;
		PlayableDirector obj = player;
		if (obj != null)
		{
			obj.Stop();
		}
	}
}

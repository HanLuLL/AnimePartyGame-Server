using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SinglePlayer.GamePlay.Character;

public class UnitView : MonoBehaviour, IUnitView
{
	protected CharacterLogic _owner;

	protected Animator _animator;

	protected PlayableDirector _director;

	private readonly int _hit = Animator.StringToHash("Hit");

	private readonly int _die = Animator.StringToHash("Die");

	private const string Die = "Die";

	protected float _radius;

	private Dictionary<string, float> _animationClipLengthDict = new Dictionary<string, float>();

	public Transform HitRoot { get; private set; }

	public float Radius => _radius;

	private void Awake()
	{
		HitRoot = base.transform.Find("HitRoot");
	}

	public virtual void Initialize(CharacterLogic owner)
	{
		_owner = owner;
		_animator = GetComponentInChildren<Animator>();
		_director = GetComponentInChildren<PlayableDirector>();
		InitAnimationClipData();
	}

	public virtual void Dispose()
	{
	}

	public bool IsDead()
	{
		return _owner.IsDead();
	}

	private void InitAnimationClipData()
	{
		if ((UnityEngine.Object)(object)_animator == null || (UnityEngine.Object)(object)_animator.runtimeAnimatorController == null)
		{
			Debug.LogError("UnitView " + base.gameObject.name + " 不存在 Animator组件");
			return;
		}
		AnimationClip[] animationClips = _animator.runtimeAnimatorController.animationClips;
		foreach (AnimationClip val in animationClips)
		{
			_animationClipLengthDict.Add(((UnityEngine.Object)(object)val).name, val.length);
		}
	}

	protected float GetAnimationClipLength(string animationName)
	{
		if (string.IsNullOrEmpty(animationName))
		{
			return 0f;
		}
		return _animationClipLengthDict.GetValueOrDefault(animationName, 0f);
	}

	protected async UniTask PlayAnimationAsync(string animationName, CancellationToken cancellationToken)
	{
		if (!string.IsNullOrEmpty(animationName))
		{
			_animator.Play(animationName, 0);
			await UniTask.Delay(TimeSpan.FromSeconds(GetAnimationClipLength(animationName)), ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
		}
	}

	private void InitPlayable()
	{
		_director = GetComponentInChildren<PlayableDirector>();
	}

	public async UniTask Play(string key)
	{
		if (!string.IsNullOrEmpty(key))
		{
			await Play(await Game.GetSystem<SinglePlayerAssetsHelper>().characterAssetManager.GetTimelineAsset(key));
		}
	}

	private async UniTask Play(TimelineAsset timeline)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			return;
		}
		foreach (TrackAsset outputTrack in timeline.GetOutputTracks())
		{
			if (outputTrack is AnimationTrack)
			{
				_director.SetGenericBinding((UnityEngine.Object)(object)outputTrack, (UnityEngine.Object)(object)_animator);
			}
		}
		_director.Play((PlayableAsset)(object)timeline);
		await UniTask.WaitForSeconds((float)((PlayableAsset)(object)timeline).duration, ignoreTimeScale: false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
	}

	public void PlayHit()
	{
	}

	public void PlayDamage()
	{
	}

	public async UniTask OnStarChange(int changeStar)
	{
		_owner.GetViewStar().Value += changeStar;
		await UniTask.CompletedTask;
	}

	public async UniTask OnHPChange(int changeHP)
	{
		if (IsDead())
		{
			return;
		}
		int value = _owner.GetViewHP().Value + changeHP;
		value = Mathf.Clamp(value, 0, _owner.Property.MaxHP);
		int num = value - _owner.GetViewHP().Value;
		_owner.GetViewHP().Value = value;
		if (num < 0)
		{
			_owner.HpChange.Dispatch(changeHP);
			if (_owner.GetViewHP().Value <= 0)
			{
				await HandleDeath();
			}
			else
			{
				_animator.SetTrigger(_hit);
			}
		}
	}

	public async UniTask OnGoldChange(int changeGold)
	{
		_owner.GetViewGold().Value += changeGold;
		await UniTask.CompletedTask;
	}

	public async UniTask OnATKChange(int changeAtk)
	{
		_owner.GetViewATK().Value += changeAtk;
		await UniTask.CompletedTask;
	}

	public async UniTask OnDEFChange(int changeDef)
	{
		_owner.GetViewDEF().Value += changeDef;
		await UniTask.CompletedTask;
	}

	private async UniTask HandleDeath()
	{
		CancellationTokenSource cts = Game.GetSystem<GamePlayManager>().CancellationTokenSource;
		await PlayAnimationAsync("Die", cts.Token);
		if (!cts.IsCancellationRequested)
		{
			OnDeath();
		}
	}

	protected virtual void OnDeath()
	{
	}

	private void OnDrawGizmos()
	{
		if (HitRoot == null)
		{
			HitRoot = base.transform.Find("HitRoot");
		}
		if (!(HitRoot == null))
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(HitRoot.position, _radius);
		}
	}

	public Vector3 GetPosition()
	{
		return base.transform.position;
	}
}

using System;
using Core.Mark;
using Core.Scene;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Core.Unit;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimator : Unit, IMarkTarget
{
	public Animator animator;

	public Character owner;

	public Material _spriteMaterial;

	private const string _walk = "Walk";

	private const string _walk_Back = "Walk-Back";

	private const string _die = "Die";

	private const string _hospitalized = "Hospitalized";

	private const string _sleep = "Sleep";

	private const string _show = "Show";

	private const string _talent = "Talent";

	private const string _attack = "Attack";

	private const string _dodge = "Dodge";

	private const string _cheer = "Cheer";

	private const string _cry = "Cry";

	private const string _lose = "Lose";

	private const string _hit = "Hit";

	private const string _hit_01 = "Hit_01";

	private const string _hit_02 = "Hit_02";

	private const string _eat = "Eat";

	private const string _vomit = "Vomit";

	private const string _electricShock = "Electricshock";

	private TweenerCore<Vector3, Vector3, VectorOptions> scaleTweener;

	private const float HideScaleThreshold = 0.01f;

	private float ScaleParam;

	private bool _grayed;

	private const string _GRAY_KEYWORD = "COLOR_GRAY";

	private PlayableGraph playableGraph;

	private bool hoverWait;

	private bool _isPaused;

	private float _currentMultiplier = 1f;

	private const string _MARK_KEYWORD = "_OUTLINE_GLOW";

	private bool _marked;

	private CharacterOutlineController _characterOutlineController;

	public float defaultAnimationScale
	{
		get
		{
			return ScaleParam;
		}
		set
		{
			if (!Mathf.Approximately(value, ScaleParam))
			{
				ScaleParam = value;
				UpdateAnimationObjectScale(value);
			}
		}
	}

	public bool grayed
	{
		get
		{
			return _grayed;
		}
		set
		{
			if (_grayed != value && !(_spriteMaterial == null))
			{
				_grayed = value;
				if (_grayed)
				{
					_spriteMaterial.EnableKeyword("COLOR_GRAY");
				}
				else
				{
					_spriteMaterial.DisableKeyword("COLOR_GRAY");
				}
			}
		}
	}

	public bool Marked
	{
		get
		{
			return _marked;
		}
		set
		{
			if (_marked != value && !(_characterOutlineController == null))
			{
				_marked = value;
				_characterOutlineController.SetOutline(_marked, new Color(1.7411011f, 0.6051623f, 0f, 1f)).Forget();
			}
		}
	}

	bool IMarkTarget.HoverWait => false;

	protected override void Awake()
	{
		base.Awake();
		animator = base.transform.GetComponent<Animator>();
		_spriteMaterial = base.transform.GetComponent<SpriteRenderer>().material;
		_characterOutlineController = base.transform.GetComponent<CharacterOutlineController>();
	}

	public void InitBaseComponent(Character _character, float _defaultScale)
	{
		owner = _character;
		defaultAnimationScale = _defaultScale;
	}

	private int StringToHash(string animName)
	{
		return Animator.StringToHash(animName);
	}

	private async UniTask WaitForAnimationComplete(string animName)
	{
		if ((UnityEngine.Object)(object)animator == null)
		{
			return;
		}
		AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
		foreach (AnimationClip val in animationClips)
		{
			if (((UnityEngine.Object)(object)val).name == animName)
			{
				if (animator.speed <= 0f)
				{
					break;
				}
				float num = val.length / animator.speed;
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(num));
			}
		}
	}

	private bool IsHasParam(string _param)
	{
		if ((UnityEngine.Object)(object)animator == null)
		{
			return false;
		}
		for (int i = 0; i < animator.parameterCount; i++)
		{
			if (animator.parameters[i].name == _param)
			{
				return true;
			}
		}
		return false;
	}

	public async void Move(bool walk, float addition = 1f, int forceWalkDir = 0)
	{
		if (walk)
		{
			await Sleep(sleep: false);
		}
		if (owner == null)
		{
			return;
		}
		IMoveAnimRule moveAnimRule = owner.showComponent as IMoveAnimRule;
		string text = moveAnimRule?.GetWalkAnimeName() ?? "Walk";
		string text2 = moveAnimRule?.GetWalkBackAnimeName() ?? "Walk-Back";
		int num = StringToHash(text);
		int num2 = StringToHash(text2);
		if (!walk)
		{
			ApplyAnimatorSpeed();
			if (moveAnimRule == null || !moveAnimRule.KeepMoveAnim())
			{
				if (IsHasParam(text) && animator.GetBool(num))
				{
					animator.SetBool(num, false);
				}
				if (IsHasParam(text2) && animator.GetBool(num2))
				{
					animator.SetBool(num2, false);
				}
			}
			return;
		}
		ApplyAnimatorSpeed(addition);
		if (((forceWalkDir != 0) ? ((float)forceWalkDir) : owner.GetWalkDir()) < 0f)
		{
			if (IsHasParam(text))
			{
				if (IsHasParam(text2) && animator.GetBool(num2))
				{
					animator.SetBool(num2, false);
				}
				animator.SetBool(num, true);
			}
		}
		else
		{
			if (IsHasParam(text) && animator.GetBool(num))
			{
				animator.SetBool(num, false);
			}
			animator.SetBool(num2, true);
		}
	}

	public bool GetDieAnimeStatus()
	{
		if (!IsHasParam("Die"))
		{
			return false;
		}
		return animator.GetBool(StringToHash("Die"));
	}

	public async UniTask Die(bool die)
	{
		Move(walk: false);
		if (IsHasParam("Die"))
		{
			if (die)
			{
				animator.SetBool(StringToHash("Die"), true);
				await WaitForAnimationComplete("Die");
			}
			else if (animator.GetBool(StringToHash("Die")))
			{
				animator.SetBool(StringToHash("Die"), false);
				await Show();
			}
		}
	}

	public void Hospitalized(bool hospitalized)
	{
		if (IsHasParam("Hospitalized"))
		{
			ApplyAnimatorSpeed();
			if (owner != null)
			{
				owner.signal.statusIcon.Dispatch(hospitalized, owner.standLand.GetLandIcon());
			}
			animator.SetBool(StringToHash("Hospitalized"), hospitalized);
		}
	}

	public async UniTask Sleep(bool sleep)
	{
		if (IsHasParam("Sleep"))
		{
			ApplyAnimatorSpeed();
			if (sleep)
			{
				animator.SetBool(StringToHash("Sleep"), true);
				await WaitForAnimationComplete("Sleep");
			}
			else if (animator.GetBool(StringToHash("Sleep")))
			{
				animator.SetBool(StringToHash("Sleep"), false);
			}
		}
	}

	public async UniTask Show()
	{
		if (IsHasParam("Show"))
		{
			ApplyAnimatorSpeed();
			animator.SetTrigger(StringToHash("Show"));
			await WaitForAnimationComplete("Show");
		}
	}

	public bool GetAnimeStatus(string animeName)
	{
		if (!IsHasParam(animeName))
		{
			return false;
		}
		return animator.GetBool(StringToHash(animeName));
	}

	public void SetAnime(string animeName, bool status)
	{
		if (IsHasParam(animeName))
		{
			ApplyAnimatorSpeed();
			animator.SetBool(StringToHash(animeName), status);
		}
	}

	public async void TriggerAnime(string animeName)
	{
		if (IsHasParam(animeName))
		{
			switch (animeName)
			{
			case "Walk":
			case "Walk-Back":
				Move(walk: true);
				break;
			case "Die":
				await Die(die: true);
				break;
			case "Hospitalized":
				Hospitalized(hospitalized: true);
				break;
			default:
				ApplyAnimatorSpeed();
				animator.SetTrigger(animeName);
				break;
			}
		}
	}

	public void UpdateAnimationObjectScale(float scale)
	{
		KillScaleAnimation();
		base.transform.localScale = new Vector3(scale, scale, 1f);
	}

	public void DoScaleAnimation(Vector3 scale, float duration, Action onComplete = null)
	{
		KillScaleAnimation();
		if (duration > 0f)
		{
			scaleTweener = base.transform.DOScale(scale, duration).OnComplete(delegate
			{
				onComplete?.Invoke();
			}).SetEase(Ease.Linear);
		}
		else
		{
			base.transform.localScale = scale;
			onComplete?.Invoke();
		}
	}

	private void KillScaleAnimation()
	{
		if (scaleTweener != null)
		{
			scaleTweener.Kill();
			scaleTweener = null;
		}
	}

	public bool IsHide()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return true;
		}
		Vector3 localScale = base.transform.localScale;
		if (!(Mathf.Abs(localScale.x) <= 0.01f))
		{
			return Mathf.Abs(localScale.y) <= 0.01f;
		}
		return true;
	}

	public void Trigger109Skill(string offset)
	{
		Vector3 offsetVector = Vector3.zero;
		if (!string.IsNullOrEmpty(offset))
		{
			string[] array = offset.Split(';');
			if (array.Length != 0)
			{
				string text = array[0];
				if (base.transform.parent.localScale.x > 0f && array.Length > 1)
				{
					text = array[1];
				}
				string[] array2 = text.Split(',');
				float x = ((array2.Length != 0) ? float.Parse(array2[0]) : 0f);
				float y = ((array2.Length > 1) ? float.Parse(array2[1]) : 0f);
				float z = ((array2.Length > 2) ? float.Parse(array2[2]) : 0f);
				offsetVector = new Vector3(x, y, z);
			}
		}
		if (owner.skill.skillId == 10901)
		{
			(owner.skill as Skill_10901)?.TriggerSkillShow(offsetVector);
		}
		else if (owner.skill.skillId == 10902)
		{
			(owner.skill as Skill_10902)?.TriggerSkillShow(offsetVector);
		}
		else if (owner.skill.skillId == 10903)
		{
			(owner.skill as Skill_10903)?.TriggerSkillShow(offsetVector);
		}
	}

	public void PlayAnimationByPlayable(AnimationClip clip)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if ((UnityEngine.Object)(object)animator == null)
		{
			animator = GetComponent<Animator>();
		}
		if ((UnityEngine.Object)(object)animator == null)
		{
			Debug.LogError("PlayAnimationByPlayable failed: Animator not found.");
			return;
		}
		if (playableGraph.IsValid())
		{
			playableGraph.Destroy();
		}
		AnimationPlayableUtilities.PlayClip(animator, clip, ref playableGraph).SetSpeed<AnimationClipPlayable>(1.2000000476837158);
	}

	protected override void OnDestroy()
	{
		if (playableGraph.IsValid())
		{
			playableGraph.Destroy();
		}
		base.OnDestroy();
	}

	private void ApplyAnimatorSpeed(float multiplier = 1f)
	{
		_currentMultiplier = multiplier;
		animator.speed = (_isPaused ? 0f : (BattleConfig.RoleAnimatorSpeed * multiplier));
	}

	private void RefreshAnimatorSpeed()
	{
		ApplyAnimatorSpeed(_currentMultiplier);
	}

	protected virtual void OnEnable()
	{
		(SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session?.Signal.speedChanged)?.AddListener(RefreshAnimatorSpeed);
	}

	protected virtual void OnDisable()
	{
		(SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session?.Signal.speedChanged)?.RemoveListener(RefreshAnimatorSpeed);
	}

	public void PauseAnimation()
	{
		_isPaused = true;
		ApplyAnimatorSpeed();
	}

	public void ResumeAnimation()
	{
		_isPaused = false;
		ApplyAnimatorSpeed();
	}

	public void OnMarkHoverEnter()
	{
		Marked = true;
	}

	public void OnMarkHoverExit()
	{
		Marked = false;
	}

	public void OnMarkSelected()
	{
		Marked = false;
		if (!(owner == null) && owner.player != null)
		{
			SimpleSingletonProvider<UIManager>.inst.expression.ShowPlayerMenu(this, owner.player.Id);
		}
	}

	public void TriggerHoverConfirmed()
	{
	}

	public Vector2 GetPosition()
	{
		if (base.transform == null)
		{
			return Vector2.zero;
		}
		UnityEngine.Camera camera = BattleSceneController.inst?.mainCamera;
		if (camera == null)
		{
			return Vector2.zero;
		}
		return camera.WorldToScreenPoint(base.transform.position);
	}
}

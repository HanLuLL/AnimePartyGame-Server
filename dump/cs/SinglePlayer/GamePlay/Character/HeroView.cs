using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class HeroView : UnitView
{
	private const string _walkJump = "WalkJump";

	private const string _walkJumpStart = "WalkJumpStart";

	private const string _walkJumpStop = "WalkJumpStop";

	private const string _walkSpeedMultiplier = "WalkSpeedMultiplier";

	private readonly int _jumpStop = Animator.StringToHash("WalkJumpStop");

	public override void Initialize(CharacterLogic owner)
	{
		base.Initialize(owner);
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private float GetAnimationClipLength1(string clipName)
	{
		AnimationClip val = _animator.runtimeAnimatorController.animationClips.FirstOrDefault((AnimationClip x) => ((Object)(object)x).name == clipName);
		if (!((Object)(object)val != null))
		{
			return 1f;
		}
		return val.length;
	}

	private int StringToHash(string animName)
	{
		return Animator.StringToHash(animName);
	}

	private bool IsHasParam(string _param)
	{
		if ((Object)(object)_animator == null)
		{
			return false;
		}
		for (int i = 0; i < _animator.parameterCount; i++)
		{
			if (_animator.parameters[i].name == _param)
			{
				return true;
			}
		}
		return false;
	}

	private void SetWalkSpeed(string anime, float moveTime)
	{
		if (IsHasParam("WalkSpeedMultiplier"))
		{
			float num = GetAnimationClipLength(anime) / moveTime;
			int num2 = StringToHash("WalkSpeedMultiplier");
			_animator.SetFloat(num2, num);
		}
	}

	public void PlayWalkJump(bool walk, float moveTime)
	{
		SetWalkSpeed("WalkJump", moveTime);
		if (IsHasParam("WalkJump"))
		{
			int num = StringToHash("WalkJump");
			_animator.SetBool(num, walk);
		}
	}

	public async UniTask PlayWalkStart()
	{
		PlayWalkJump(walk: false, 1f);
		if (IsHasParam("WalkJumpStart"))
		{
			CancellationTokenSource cancellationTokenSource = Game.GetSystem<GamePlayManager>().CancellationTokenSource;
			await PlayAnimationAsync("WalkJumpStart", cancellationTokenSource.Token);
		}
	}

	public void PlayWalkStop()
	{
		PlayWalkJump(walk: false, 1f);
		if (IsHasParam("WalkJumpStop"))
		{
			_animator.SetTrigger(_jumpStop);
		}
	}
}

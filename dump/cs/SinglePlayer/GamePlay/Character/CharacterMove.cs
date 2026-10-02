using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SinglePlayer.GamePlay.Map;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class CharacterMove : MonoBehaviour
{
	private Hero _owner;

	public float MoveSpeed = 20f;

	public float MoveOffsetY = 2f;

	public void Initialize(Hero owner)
	{
		_owner = owner;
		HeroProperty heroProperty = (HeroProperty)_owner.Property;
		Land landById = Game.GetModel<GameData>().MapData.GetLandById(heroProperty.StandLandId);
		base.transform.position = landById.transform.position + Vector3.up * MoveOffsetY;
		List<int> nextLandIds = heroProperty.NextLandIds;
		if (nextLandIds != null && nextLandIds.Count > 0)
		{
			Land landById2 = Game.GetModel<GameData>().MapData.GetLandById(heroProperty.NextLandIds[0]);
			base.transform.rotation = GetDirection(landById2.transform.position, base.transform.position);
		}
	}

	public async UniTask<bool> Move(Land targetSlot, float moveTime)
	{
		Vector3 vector = targetSlot.transform.position + Vector3.up * MoveOffsetY;
		Quaternion targetQuaternion = GetDirection(vector, base.transform.position);
		float num = Mathf.DeltaAngle(base.transform.eulerAngles.y, targetQuaternion.eulerAngles.y);
		if (Mathf.Abs(num) > 70f)
		{
			float y = base.transform.eulerAngles.y + num + Mathf.Sign(num) * 360f;
			ShortcutExtensions.DORotate(endValue: new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z), target: base.transform, duration: moveTime, mode: RotateMode.FastBeyond360).OnComplete(delegate
			{
				base.transform.rotation = targetQuaternion;
			});
		}
		else
		{
			base.transform.DORotateQuaternion(targetQuaternion, moveTime * 0.5f);
		}
		CtsInfo moveCancelToken = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		await DOTweenAsyncExtensions.WithCancellation((Tween)base.transform.DOMove(vector, moveTime).SetEase(Ease.Linear), moveCancelToken.Token);
		if (moveCancelToken.IsCancellationRequested)
		{
			return false;
		}
		SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(moveCancelToken);
		return true;
	}

	private Quaternion GetDirection(Vector3 target, Vector3 stand)
	{
		Vector3 vector = target - stand;
		vector.y = 0f;
		return Quaternion.AngleAxis(Mathf.Atan2(vector.x, vector.z) * 57.29578f, Vector3.up);
	}

	public float GetWalkDirection(Vector3 dir)
	{
		Vector3 normalized = (dir - base.transform.position).normalized;
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
}

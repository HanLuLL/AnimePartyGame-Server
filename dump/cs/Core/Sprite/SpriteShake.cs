using UnityEngine;

namespace Core.Sprite;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteShake : MonoBehaviour
{
	private bool _enableSpriteShake;

	private float _spriteShakeElapseTime;

	private float _shakeDuration;

	private SpriteShakeAsset _spriteShakeAsset;

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Update()
	{
		TryShakeSprite();
	}

	private void TryShakeSprite()
	{
		if (_enableSpriteShake && _spriteShakeAsset != null)
		{
			if (_spriteShakeElapseTime > _shakeDuration)
			{
				StopShake();
				return;
			}
			_spriteShakeElapseTime += Time.deltaTime;
			_transform.localPosition = _spriteShakeAsset.GetPosition(_spriteShakeElapseTime, _shakeDuration);
		}
	}

	public void StartShake(SpriteShakeAsset shakeAsset, float shakeDuration)
	{
		StopShake();
		_enableSpriteShake = true;
		_spriteShakeElapseTime = 0f;
		_shakeDuration = shakeDuration;
		_spriteShakeAsset = shakeAsset;
	}

	public void StopShake()
	{
		_enableSpriteShake = false;
		_transform.localPosition = Vector3.zero;
	}
}

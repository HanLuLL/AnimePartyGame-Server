using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic;

[ExecuteInEditMode]
public class CircleWind : MonoBehaviour
{
	[SerializeField]
	[FormerlySerializedAs("radius")]
	private float boomRadius;

	[SerializeField]
	[FormerlySerializedAs("strength")]
	private float boomStrength;

	[SerializeField]
	private float radius;

	[SerializeField]
	private float strength;

	[Header("下列参数控制动画的整体播放效果")]
	[SerializeField]
	private float radiusScale = 1f;

	[SerializeField]
	private float strengthScale = 1f;

	[SerializeField]
	[Range(0.1f, 4f)]
	private float durationScale = 1f;

	private Animator animator;

	public void FillCircleWindwBuffer(float[] buffer, int start)
	{
		buffer[start] = base.transform.position.x;
		buffer[start + 1] = base.transform.position.z;
		buffer[start + 2] = boomRadius * radiusScale;
		buffer[start + 3] = boomStrength * strengthScale;
		buffer[start + 4] = radius * radiusScale;
		buffer[start + 5] = strength * strengthScale;
	}

	private void OnEnable()
	{
		WindController.Instance?.AddCircleWind(this);
		UpdateAnimatorSpeed();
	}

	private void OnValidate()
	{
		UpdateAnimatorSpeed();
	}

	private void OnDisable()
	{
		WindController.Instance?.RemoveCircleWind(this);
	}

	private void UpdateAnimatorSpeed()
	{
		animator = GetComponent<Animator>();
		if ((Object)(object)animator != null && Application.isPlaying)
		{
			animator.speed = 1f / durationScale;
		}
	}
}

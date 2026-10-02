using System;
using UnityEngine;

namespace Core.Sprite;

[Serializable]
[CreateAssetMenu(menuName = "角色/位移曲线配置", fileName = "New OffsetCurveAsset.asset", order = 2)]
public class OffsetCurveAsset : ScriptableObject
{
	[SerializeField]
	private AnimationCurve _xCurve;

	[SerializeField]
	private AnimationCurve _yCurve;

	[SerializeField]
	private AnimationCurve _zCurve;

	public AnimationCurve xCurve => _xCurve;

	public AnimationCurve yCurve => _yCurve;

	public AnimationCurve zCurve => _zCurve;
}

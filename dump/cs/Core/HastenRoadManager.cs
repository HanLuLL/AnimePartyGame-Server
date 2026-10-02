using System;
using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools;
using UnityEngine;

namespace Core;

[Serializable]
public class HastenRoadManager : MonoBehaviour
{
	[Header("通道")]
	[Header("开始和结束节点")]
	[SerializeField]
	public UnitLand StartLand;

	[SerializeField]
	public UnitLand EndLand;

	[Header("组件")]
	[SerializeField]
	public List<HastenRoadComponent> Components;

	[Header("转向事件")]
	[SerializeField]
	public float TurnTime = 0.2f;

	[SerializeField]
	public float TurnInterval = 0.05f;

	private Vector3 _InitDirection;

	private Vector3 _CurrentDirection;

	private Sequence _Sequence;

	[Header("组件")]
	[Space(8f)]
	[Header("颜色")]
	[SerializeField]
	public Color BackwardArrowBaseColor;

	[ColorUsage(true, true)]
	[SerializeField]
	public Color BackwardArrowEmissionColor;

	[SerializeField]
	public Color BackwardParticleBaseColor;

	[ColorUsage(true, true)]
	[SerializeField]
	public Color BackwardParticleEmissionColor;

	[Header("下坠")]
	[SerializeField]
	public float FallDistance = -8f;

	[SerializeField]
	public float FallTime = 0.2f;

	[SerializeField]
	public float ComponentRadius = 8f;

	[SerializeField]
	public Ease FallEase = Ease.OutElastic;

	[ColorUsage(true, true)]
	[SerializeField]
	public Color ForwardParticleFlashColor;

	[ColorUsage(true, true)]
	[SerializeField]
	public Color BackwardParticleFlashColor;

	public Transform MovingTransform;

	[HideInInspector]
	public Color ComponentFlashColor;

	private void Awake()
	{
		_InitDirection = (EndLand.transform.localPosition - StartLand.transform.localPosition).normalized;
		_CurrentDirection = _InitDirection;
		ComponentFlashColor = ForwardParticleFlashColor;
		InitSequence();
		StartLand.RegisterHastenRoad(this, EndLand.Id);
		EndLand.RegisterHastenRoad(this, StartLand.Id);
	}

	private void InitSequence()
	{
		_Sequence = DOTween.Sequence();
		_Sequence.Append(Components[0].transform.DOLocalRotate(new Vector3(0f, 0f, 180f), TurnTime));
		SetMaterialColor(0f, Components[0]);
		for (int i = 1; i < Components.Count; i++)
		{
			_Sequence.Insert(TurnInterval * (float)i, Components[i].transform.DOLocalRotate(new Vector3(0f, 0f, 180f), TurnTime));
			SetMaterialColor(TurnInterval * (float)i, Components[i]);
		}
		_Sequence.Pause();
		_Sequence.SetAutoKill(autoKillOnCompletion: false);
	}

	private void SetMaterialColor(float startTime, HastenRoadComponent component)
	{
		_Sequence.Insert(startTime, component.particle.GetComponent<MeshRenderer>().material.DOColor(BackwardParticleBaseColor, "_BaseColor", TurnTime));
		_Sequence.Insert(startTime, component.particle.GetComponent<MeshRenderer>().material.DOColor(BackwardParticleEmissionColor, "_EmissionColor", TurnTime));
		_Sequence.Insert(startTime, component.arrow.GetComponent<MeshRenderer>().material.DOColor(BackwardArrowBaseColor, "_BaseColor", TurnTime));
		_Sequence.Insert(startTime, component.arrow.GetComponent<MeshRenderer>().material.DOColor(BackwardArrowEmissionColor, "_EmissionColor", TurnTime));
	}

	public async UniTask<bool> Turn(Vector3 targetDir)
	{
		if (Components.Count == 0 || _Sequence == null)
		{
			return false;
		}
		float num = Vector3.Dot(_InitDirection, targetDir.normalized);
		ComponentFlashColor = ((num < 0f) ? BackwardParticleFlashColor : ForwardParticleFlashColor);
		float num2 = Vector3.Dot(_CurrentDirection, targetDir.normalized);
		_CurrentDirection = targetDir;
		if (num2 < 0f)
		{
			Debug.Log("当前相反，需要转向");
			if (num < 0f)
			{
				_Sequence.PlayForward();
			}
			else
			{
				_Sequence.PlayBackwards();
			}
			bool result = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay((int)(_Sequence.Duration() * 1000f));
			Debug.Log("转向完成，开始移动");
			return result;
		}
		return false;
	}

	private void OnDestroy()
	{
		_Sequence?.Kill();
		_Sequence = null;
	}
}

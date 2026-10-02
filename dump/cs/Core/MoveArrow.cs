using System;
using System.Collections.Generic;
using Core.Unit;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using FairyGUI;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class MoveArrow : GameEffect
{
	public int StartLandId;

	public int EndLandId;

	public Transform DirectionArrow;

	public Transform SuggestArrow;

	[SerializeField]
	public List<Transform> UpgradeStars;

	private TweenerCore<Vector3, Vector3, VectorOptions> _ArrowTweener;

	private UIButton_MoveArrow _Arrow;

	private bool _suggestSelect;

	public UIButton_MoveArrow Arrow
	{
		get
		{
			if (_Arrow == null)
			{
				_Arrow = (UIButton_MoveArrow)GetComponentInChildren<UIPanel>().ui;
			}
			return _Arrow;
		}
	}

	public override void Play(string _effectPoolKey, Action action)
	{
		_suggestSelect = false;
		base.Play(_effectPoolKey, action);
	}

	protected override void Dispose()
	{
		CloseArrowEffect();
		base.Dispose();
	}

	public void UpdateArrow(int standLandId, int targetLandId, long _actionSn)
	{
		StartLandId = standLandId;
		EndLandId = targetLandId;
		Transform transform = SimpleSingletonProvider<LandManager>.inst.GetLandById(standLandId).transform;
		Vector3 forward = SimpleSingletonProvider<LandManager>.inst.GetLandById(targetLandId).transform.position - transform.position;
		forward.y = 0f;
		Vector3 vector = forward.normalized * 20f;
		Vector3 position = transform.position + new Vector3(vector.x, 10f, vector.z);
		base.transform.position = position;
		base.transform.forward = forward;
		CloseArrowEffect();
		if (Arrow == null)
		{
			return;
		}
		Arrow.onRollOut.Set((EventCallback0)delegate
		{
			_ArrowTweener?.Pause();
			if (DirectionArrow.gameObject.activeSelf)
			{
				_ArrowTweener = DirectionArrow.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutQuint);
			}
			if (SuggestArrow.gameObject.activeSelf)
			{
				_ArrowTweener = SuggestArrow.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutQuint);
			}
		});
		Arrow.onRollOver.Set((EventCallback0)delegate
		{
			_ArrowTweener?.Pause();
			if (DirectionArrow.gameObject.activeSelf)
			{
				_ArrowTweener = DirectionArrow.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.InOutQuint);
			}
			if (SuggestArrow.gameObject.activeSelf)
			{
				_ArrowTweener = SuggestArrow.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.InOutQuint);
			}
		});
	}

	public void ShowArrowEffect()
	{
		ShowDirectionArrow();
		if (DirectionArrow != null)
		{
			ParticleColorController[] componentsInChildren = DirectionArrow.GetComponentsInChildren<ParticleColorController>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetMaterialColor(1);
				componentsInChildren[i].SetGradient(1);
			}
		}
	}

	public void ShowSuggestEffect()
	{
		_suggestSelect = true;
		ShowSuggestArrow();
	}

	public void CloseArrowEffect()
	{
		if (_suggestSelect)
		{
			ShowSuggestArrow();
		}
		else
		{
			ShowDirectionArrow();
		}
		if (DirectionArrow != null)
		{
			ParticleColorController[] componentsInChildren = DirectionArrow.GetComponentsInChildren<ParticleColorController>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetMaterialColor(0);
				componentsInChildren[i].SetGradient(0);
			}
		}
	}

	private void ShowSuggestArrow()
	{
		_ArrowTweener?.Pause();
		if (DirectionArrow != null)
		{
			DirectionArrow.transform.localScale = Vector3.zero;
			DirectionArrow.gameObject.SetActiveEx(active: false);
		}
		if (SuggestArrow != null)
		{
			SuggestArrow.transform.localScale = Vector3.one;
			SuggestArrow.gameObject.SetActiveEx(active: true);
		}
	}

	private void ShowDirectionArrow()
	{
		_ArrowTweener?.Pause();
		if (DirectionArrow != null)
		{
			DirectionArrow.transform.localScale = Vector3.one;
			DirectionArrow.gameObject.SetActiveEx(active: true);
		}
		if (SuggestArrow != null)
		{
			SuggestArrow.transform.localScale = Vector3.zero;
			SuggestArrow.gameObject.SetActiveEx(active: false);
		}
	}

	public void ShowUpgradeArrow(int targetStar)
	{
		for (int i = 0; i < UpgradeStars.Count; i++)
		{
			Transform transform = UpgradeStars[i];
			ParticleColorController[] componentsInChildren = transform.GetComponentsInChildren<ParticleColorController>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].transform.localScale = Vector3.one;
				componentsInChildren[j].SetGradient((i >= targetStar) ? 1 : 0);
			}
			((Behaviour)(object)transform.GetComponent<Animator>()).enabled = i == targetStar;
		}
	}
}

using System;
using Core.Mark;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Render.Runtime;
using Tools;
using UI;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public class PlatformFrame : Unit, IMarkTarget
{
	[SerializeField]
	public GameObject Stamp;

	[SerializeField]
	public GameObject PolySurface;

	[SerializeField]
	public Color BaseColor = Color.white;

	[SerializeField]
	[ColorUsage(false, true)]
	public Color EmissionColor = Color.white;

	private Renderer _StampRenderer;

	private Color _StampInitColor;

	private Effect _LandTipEffect;

	private bool _effectLoadLock;

	private Renderer StampRenderer
	{
		get
		{
			if (_StampRenderer == null)
			{
				_StampRenderer = Stamp.GetComponent<Renderer>();
				_StampInitColor = _StampRenderer.material.GetColor(ShaderConstant._EmissionColor);
			}
			return _StampRenderer;
		}
	}

	bool IMarkTarget.HoverWait => false;

	private void Start()
	{
		StampRenderer.material.SetColor(ShaderConstant._BaseColor, BaseColor);
		StampRenderer.material.SetColor(ShaderConstant._EmissionColor, EmissionColor);
	}

	public void SetStampUV(float ux, float uy)
	{
		StampRenderer.material.SetTextureOffset(ShaderConstant._BaseMap, new Vector2(ux * 0.001f, uy * 0.001f));
	}

	public void SetStampTex(Texture tex)
	{
		StampRenderer.material.SetTexture(ShaderConstant._BaseMap, tex);
	}

	public void SetStampEmissionTex(Texture tex)
	{
		StampRenderer.material.SetTexture(ShaderConstant._EmissionMap, tex);
	}

	public void SetStampIntensity(bool enable)
	{
		if (enable)
		{
			StampRenderer.material.EnableKeyword("_EMISSION");
		}
		else
		{
			StampRenderer.material.DisableKeyword("_EMISSION");
		}
	}

	private void OnMouseDown()
	{
		OnTriggerLandTipStart();
	}

	private void OnMouseUp()
	{
		OnTriggerLandTipEnd();
	}

	private void OnTriggerLandTipStart()
	{
		CoroutineManager.CoroutineState coroutineState = SimpleSingletonProvider<LandManager>.inst._coroutineState;
		if (coroutineState != null && coroutineState.Running)
		{
			SimpleSingletonProvider<LandManager>.inst._coroutineState.Stop();
		}
		if (!Stage.isTouchOnUI)
		{
			SimpleSingletonProvider<LandManager>.inst._coroutineState = MonoSingletonProvider<CoroutineManager>.inst.CreateInvoke(1f, ShowLandTip);
			SimpleSingletonProvider<LandManager>.inst._coroutineState.Start();
		}
	}

	private async void OnTriggerLandTipEnd()
	{
		CoroutineManager.CoroutineState coroutineState = SimpleSingletonProvider<LandManager>.inst._coroutineState;
		if (coroutineState != null && coroutineState.Running)
		{
			SimpleSingletonProvider<LandManager>.inst._coroutineState.Stop();
		}
		await UniTask.WaitForSeconds(0.5f);
		HideLandTip();
	}

	private async void ShowLandTip()
	{
		UnitLand componentInParent = GetComponentInParent<UnitLand>();
		if (!(componentInParent == null))
		{
			SimpleSingletonProvider<UIManager>.inst.BattleLandTip.ShowLandTips(componentInParent.LandType);
			await ShowLandSelectedEffect(componentInParent);
		}
	}

	private async UniTask ShowLandSelectedEffect(UnitLand land)
	{
		if (!(_LandTipEffect != null) && !_effectLoadLock)
		{
			_effectLoadLock = true;
			_LandTipEffect = await land.PlayById(8000100, Vector3.zero, Quaternion.identity);
			_effectLoadLock = false;
		}
	}

	private void HideLandTip()
	{
		SimpleSingletonProvider<UIManager>.inst.BattleLandTip.HideLandTips();
		_LandTipEffect?.ReleaseEffect();
		_LandTipEffect = null;
	}

	private async UniTask HideLandSelectedEffect()
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => !_effectLoadLock);
		HideLandTip();
	}

	public void OnMarkHoverEnter()
	{
		UnitLand componentInParent = GetComponentInParent<UnitLand>();
		if (!(componentInParent == null))
		{
			ShowLandSelectedEffect(componentInParent).Forget();
		}
	}

	public void OnMarkHoverExit()
	{
		HideLandSelectedEffect().Forget();
	}

	public void OnMarkSelected()
	{
		HideLandTip();
		UnitLand componentInParent = GetComponentInParent<UnitLand>();
		if (!(componentInParent == null))
		{
			SimpleSingletonProvider<UIManager>.inst.expression.ShowLandMenu(this, componentInParent);
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

using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public class GuideWindow : BaseWindow
{
	private GuideInfo GuideInfo;

	private GObject _maskTarget;

	private bool _maskNeedTransparent;

	private bool _maskIsRect;

	private bool _maskIgnorePivot;

	private Vector2 _lastMaskPosition;

	private Vector2 _lastMaskSize;

	private GObject _arrowTarget;

	private float _arrowOffsetX;

	private float _arrowOffsetY;

	private float _arrowRotate;

	private bool _arrowFlip;

	private bool _arrowIgnorePivot;

	private Vector2 _lastArrowPosition;

	public GuideWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIGuideWindow.CreateInstance();
		base.OnInit();
		GRoot.inst.onSizeChanged.Add(OnRootSizeChanged);
	}

	private void OnRootSizeChanged()
	{
		if (_maskTarget != null && _maskTarget.onStage)
		{
			RefreshGuideMaskByTarget();
		}
		if (_arrowTarget != null && _arrowTarget.onStage)
		{
			RefreshGuideArrowByTarget();
		}
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (base.contentPane is UIGuideWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnShown()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.graph_TransparentMask.onClick.Add(TriggerNext);
			uIGuideWindow.com_Dialog.btn_Sure.onClick.Add(SureDialog);
			base.OnShown();
		}
	}

	protected override void OnHide()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.graph_TransparentMask.onClick.Remove(TriggerNext);
			uIGuideWindow.com_Dialog.btn_Sure.onClick.Remove(SureDialog);
			base.OnHide();
		}
	}

	private void SureDialog()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.com_Dialog.btn_Sure.onClick.Retain();
			GuideInfo.TriggerNext();
			uIGuideWindow.com_Dialog.btn_Sure.onClick.Release();
		}
	}

	public void TriggerNext()
	{
		if (GuideInfo == null)
		{
			Hide();
		}
		else if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.graph_TransparentMask.onClick.Retain();
			GuideInfo.TriggerNext();
		}
	}

	public async UniTask ShowTransparent()
	{
		if (!base.isShowing)
		{
			await TryShowAsync();
		}
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.graph_TransparentMask.visible = true;
			uIGuideWindow.graph_TransparentMask.onClick.Retain();
			uIGuideWindow.com_Dialog.visible = false;
			uIGuideWindow.com_GuideMask.visible = false;
			StopEffect();
		}
	}

	public async void TryShow(GuideInfo guideInfo, bool canNext)
	{
		GuideInfo = guideInfo;
		if (!base.isShowing)
		{
			await TryShowAsync();
		}
		StopEffect();
		UpdateTransparentMaskStatus(canNext);
		UpdateDialogStatus();
	}

	private async void UpdateTransparentMaskStatus(bool canNext)
	{
		if (!(base.contentPane is UIGuideWindow uIGuideWindow))
		{
			return;
		}
		if (GuideInfo.currentGuide.TutorialId != 0)
		{
			uIGuideWindow.graph_TransparentMask.onClick.Retain();
			uIGuideWindow.graph_TransparentMask.visible = true;
			await SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(GuideInfo.currentGuide.TutorialConfigure.MobileTutorialId);
			return;
		}
		if (canNext)
		{
			uIGuideWindow.graph_TransparentMask.onClick.Release();
			uIGuideWindow.graph_TransparentMask.visible = true;
		}
		else
		{
			uIGuideWindow.graph_TransparentMask.visible = false;
		}
		uIGuideWindow.graph_Arrow.visible = false;
		uIGuideWindow.com_Arrow.visible = false;
	}

	public void ForceShowDialog()
	{
		if (base.contentPane is UIGuideWindow)
		{
			UpdateDialogStatus();
		}
	}

	private void UpdateDialogStatus()
	{
		if (!(base.contentPane is UIGuideWindow uIGuideWindow))
		{
			return;
		}
		if (GuideInfo.currentGuide.DialogId == 0)
		{
			uIGuideWindow.com_Dialog.visible = false;
			return;
		}
		uIGuideWindow.com_Dialog.visible = true;
		GuideDialogConfigure dialogConfigure = GuideInfo.currentGuide.DialogConfigure;
		uIGuideWindow.com_Dialog.btn_Sure.visible = dialogConfigure.ShowButton;
		if (dialogConfigure.ShowButton)
		{
			uIGuideWindow.graph_TransparentMask.visible = false;
			uIGuideWindow.com_Dialog.btn_Sure.onClick.Release();
		}
		uIGuideWindow.com_Dialog.txt_Explain.text = dialogConfigure.MobDialogId.GetLocal(UIStringType.Dialog);
		uIGuideWindow.com_Dialog.loader_Icon.icon = dialogConfigure.RoleSprite;
		if (float.Parse(dialogConfigure.Dir.Split(',')[0]) > 860f)
		{
			uIGuideWindow.com_Dialog.SetXY(uIGuideWindow.width - uIGuideWindow.com_Dialog.width, 0f);
		}
		else
		{
			uIGuideWindow.com_Dialog.SetXY(0f, 0f);
		}
	}

	public async UniTask ShowGuideMask(Vector2 _pos, float _width, float _height, bool _needTransparentMask, bool isRect)
	{
		await TryShowAsync();
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.graph_TransparentMask.visible = _needTransparentMask;
			if (_needTransparentMask)
			{
				uIGuideWindow.graph_TransparentMask.onClick.Release();
			}
			uIGuideWindow.com_GuideMask.graph_RectMask.SetSize(_width, _height);
			uIGuideWindow.com_GuideMask.graph_RectMask.SetXY(_pos.x, _pos.y);
			uIGuideWindow.com_GuideMask.graph_CircularMask.SetSize(_width, _height);
			uIGuideWindow.com_GuideMask.graph_CircularMask.SetXY(_pos.x, _pos.y);
			if (isRect)
			{
				uIGuideWindow.com_GuideMask.graph_RectMask.visible = true;
				uIGuideWindow.com_GuideMask.graph_CircularMask.visible = false;
				uIGuideWindow.com_GuideMask.mask = uIGuideWindow.com_GuideMask.graph_RectMask.displayObject;
			}
			else
			{
				uIGuideWindow.com_GuideMask.graph_RectMask.visible = false;
				uIGuideWindow.com_GuideMask.graph_CircularMask.visible = true;
				uIGuideWindow.com_GuideMask.mask = uIGuideWindow.com_GuideMask.graph_CircularMask.displayObject;
			}
			uIGuideWindow.com_GuideMask.visible = true;
		}
	}

	public async UniTask ShowGuideMaskByTarget(GObject target, bool needTransparentMask, bool isRect)
	{
		await ShowGuideMaskByTarget(target, needTransparentMask, isRect, ignorePivot: false);
	}

	public async UniTask ShowGuideMaskByTarget(GObject target, bool needTransparentMask, bool isRect, bool ignorePivot)
	{
		_maskTarget = target;
		_maskNeedTransparent = needTransparentMask;
		_maskIsRect = isRect;
		_maskIgnorePivot = ignorePivot;
		_lastMaskPosition = Vector2.positiveInfinity;
		_lastMaskSize = Vector2.zero;
		await RefreshGuideMaskByTargetAsync();
	}

	private async UniTask RefreshGuideMaskByTargetAsync()
	{
		await TryShowAsync();
		RefreshGuideMaskByTarget();
	}

	private void RefreshGuideMaskByTarget()
	{
		if (_maskTarget != null && _maskTarget.onStage && base.contentPane is UIGuideWindow uIGuideWindow)
		{
			Vector2 vector = _maskTarget.size;
			Vector2 pt = _maskTarget.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			if (!_maskIgnorePivot)
			{
				pt -= new Vector2(vector.x * _maskTarget.pivotX, vector.y * _maskTarget.pivotY);
			}
			uIGuideWindow.graph_TransparentMask.visible = _maskNeedTransparent;
			if (_maskNeedTransparent)
			{
				uIGuideWindow.graph_TransparentMask.onClick.Release();
			}
			uIGuideWindow.com_GuideMask.graph_RectMask.SetSize(vector.x, vector.y);
			uIGuideWindow.com_GuideMask.graph_RectMask.SetXY(pt.x, pt.y);
			uIGuideWindow.com_GuideMask.graph_CircularMask.SetSize(vector.x, vector.y);
			uIGuideWindow.com_GuideMask.graph_CircularMask.SetXY(pt.x, pt.y);
			if (_maskIsRect)
			{
				uIGuideWindow.com_GuideMask.graph_RectMask.visible = true;
				uIGuideWindow.com_GuideMask.graph_CircularMask.visible = false;
				uIGuideWindow.com_GuideMask.mask = uIGuideWindow.com_GuideMask.graph_RectMask.displayObject;
			}
			else
			{
				uIGuideWindow.com_GuideMask.graph_RectMask.visible = false;
				uIGuideWindow.com_GuideMask.graph_CircularMask.visible = true;
				uIGuideWindow.com_GuideMask.mask = uIGuideWindow.com_GuideMask.graph_CircularMask.displayObject;
			}
			uIGuideWindow.com_GuideMask.visible = true;
		}
	}

	public void HideGuide()
	{
		HideGuideMask();
		Hide();
	}

	public void HideGuideMask()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.com_GuideMask.visible = false;
			uIGuideWindow.graph_TransparentMask.onClick.Release();
			_maskTarget = null;
			_arrowTarget = null;
			StopEffect();
		}
	}

	public void HideMask()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.com_GuideMask.visible = false;
			uIGuideWindow.graph_TransparentMask.visible = false;
			StopEffect();
		}
	}

	public async void ShowCardArrow()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(24.GetEffectDataConfigure().EffectName, uIGuideWindow.graph_Arrow, 5f);
		}
	}

	private void StopEffect()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uIGuideWindow.graph_Arrow);
		}
	}

	public void ShowGuideArrow(Vector2 _pos, float offsetX, float offsetY, float rotate = 0f)
	{
		_arrowTarget = null;
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.com_Arrow.SetXY(_pos.x + offsetX, _pos.y + offsetY);
			uIGuideWindow.com_Arrow.visible = true;
			uIGuideWindow.com_Arrow.rotation = rotate;
		}
	}

	public void ShowGuideArrowByTarget(GObject target, float offsetX, float offsetY, float rotate = 0f)
	{
		ShowGuideArrowByTarget(target, offsetX, offsetY, rotate, ignorePivot: false);
	}

	public void ShowGuideArrowByTarget(GObject target, float offsetX, float offsetY, float rotate, bool ignorePivot)
	{
		_arrowTarget = target;
		_arrowOffsetX = offsetX;
		_arrowOffsetY = offsetY;
		_arrowRotate = rotate;
		_arrowFlip = false;
		_arrowIgnorePivot = ignorePivot;
		_lastArrowPosition = Vector2.positiveInfinity;
		RefreshGuideArrowByTarget();
	}

	public void ShowGuideArrowRotateByTarget(GObject target, float offsetX, float offsetY, float rotate = 180f)
	{
		ShowGuideArrowRotateByTarget(target, offsetX, offsetY, rotate, ignorePivot: false);
	}

	public void ShowGuideArrowRotateByTarget(GObject target, float offsetX, float offsetY, float rotate, bool ignorePivot)
	{
		_arrowTarget = target;
		_arrowOffsetX = offsetX;
		_arrowOffsetY = offsetY;
		_arrowRotate = rotate;
		_arrowFlip = true;
		_arrowIgnorePivot = ignorePivot;
		_lastArrowPosition = Vector2.positiveInfinity;
		RefreshGuideArrowByTarget();
	}

	private void RefreshGuideArrowByTarget()
	{
		if (_arrowTarget != null && _arrowTarget.onStage && base.contentPane is UIGuideWindow uIGuideWindow)
		{
			Vector2 vector = _arrowTarget.size;
			Vector2 pt = _arrowTarget.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			if (!_arrowIgnorePivot)
			{
				pt -= new Vector2(vector.x * _arrowTarget.pivotX, vector.y * _arrowTarget.pivotY);
			}
			uIGuideWindow.com_Arrow.SetXY(pt.x + _arrowOffsetX, pt.y + _arrowOffsetY);
			uIGuideWindow.com_Arrow.visible = true;
			uIGuideWindow.com_Arrow.rotation = _arrowRotate;
			uIGuideWindow.com_Arrow.scaleX = (_arrowFlip ? (-0.5f) : 0.5f);
		}
	}

	public void HideDialog()
	{
		if (base.contentPane is UIGuideWindow uIGuideWindow)
		{
			uIGuideWindow.com_Dialog.visible = false;
		}
	}
}

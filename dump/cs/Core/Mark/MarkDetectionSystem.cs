using Core.Scene;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Mark;

public class MarkDetectionSystem
{
	private IMarkTarget _currentTarget;

	private float _hoverTimer;

	private const float HoverDelay = 0.5f;

	private bool _hoverTriggered;

	public void OnEnable()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkExit?.AddListener(SelectedCurrentTarget);
	}

	public void OnDisable()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkExit?.RemoveListener(SelectedCurrentTarget);
	}

	public void Update()
	{
		Detect();
	}

	private void Detect()
	{
		IMarkTarget markTarget = DetectTarget();
		if (markTarget != _currentTarget)
		{
			ExitCurrentHover();
			_currentTarget = markTarget;
			EnterNewHover(markTarget);
		}
		else if (_currentTarget != null && _currentTarget.HoverWait)
		{
			_hoverTimer += Time.deltaTime;
			if (!_hoverTriggered && _hoverTimer >= 0.5f && _currentTarget.HoverWait)
			{
				_hoverTriggered = true;
				TriggerHoverConfirmed(_currentTarget);
			}
		}
	}

	private void EnterNewHover(IMarkTarget target)
	{
		_hoverTimer = 0f;
		_hoverTriggered = false;
		if (target != null)
		{
			target.OnMarkHoverEnter();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverEnter?.Dispatch();
			if (target.HoverWait)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowWaiting?.Dispatch();
			}
		}
	}

	private void ExitCurrentHover()
	{
		if (_currentTarget != null)
		{
			_currentTarget?.OnMarkHoverExit();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverExit?.Dispatch();
		}
		_hoverTimer = 0f;
		_hoverTriggered = false;
	}

	private void TriggerHoverConfirmed(IMarkTarget target)
	{
		if (target != null)
		{
			target.TriggerHoverConfirmed();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal.HideWaiting?.Dispatch();
		}
	}

	private void SelectedCurrentTarget()
	{
		if (_currentTarget != null)
		{
			_currentTarget.OnMarkSelected();
			_currentTarget = null;
		}
	}

	private IMarkTarget DetectTarget()
	{
		IMarkTarget markTarget = DetectUI();
		if (markTarget != null)
		{
			return markTarget;
		}
		return DetectWorld();
	}

	private IMarkTarget DetectUI()
	{
		for (GObject gObject = GRoot.inst.touchTarget; gObject != null; gObject = gObject.parent)
		{
			if (gObject is IMarkTarget result)
			{
				return result;
			}
		}
		return null;
	}

	private IMarkTarget DetectWorld()
	{
		Ray ray = BattleSceneController.inst.mainCamera.ScreenPointToRay(Input.mousePosition);
		int mask = LayerMask.GetMask("Character", "MapLand");
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(ray, ref val, 999f, mask))
		{
			return ((Component)(object)((RaycastHit)(ref val)).collider).GetComponentInChildren<IMarkTarget>();
		}
		return null;
	}
}

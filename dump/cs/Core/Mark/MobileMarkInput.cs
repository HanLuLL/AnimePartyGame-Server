using UnityEngine;

namespace Core.Mark;

public class MobileMarkInput : IMarkInput
{
	private readonly MarkModeController _markModeController;

	private bool _isMarkDragging;

	public MobileMarkInput(MarkModeController markModeController)
	{
		_markModeController = markModeController;
	}

	public void EnterMark()
	{
		if (!_isMarkDragging)
		{
			_isMarkDragging = true;
			_markModeController.EnterMarkMode();
		}
	}

	public void ExitMark()
	{
		if (_isMarkDragging)
		{
			_isMarkDragging = false;
			_markModeController.ExitMarkMode();
		}
	}

	public void Update()
	{
		if (_markModeController.IsActive && Input.touchCount == 0)
		{
			ExitMark();
		}
	}

	public void ToggleMode()
	{
		if (_markModeController.CurrentMode == InteractionMode.Marking)
		{
			ExitMark();
		}
		else
		{
			EnterMark();
		}
	}
}

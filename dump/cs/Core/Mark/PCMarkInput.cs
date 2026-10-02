using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Mark;

public class PCMarkInput : IMarkInput
{
	private readonly MarkModeController _markModeController;

	public PCMarkInput(MarkModeController markModeController)
	{
		_markModeController = markModeController;
	}

	public void EnterMark()
	{
		_markModeController.EnterMarkMode();
	}

	public void ExitMark()
	{
		_markModeController.ExitMarkMode();
	}

	public void Update()
	{
		CheckKeyboard();
	}

	private void CheckKeyboard()
	{
		if (_markModeController.IsActive && Input.GetMouseButtonUp(0))
		{
			ExitMark();
		}
		bool keyDown = Input.GetKeyDown(KeyCode.LeftAlt);
		bool keyDown2 = Input.GetKeyDown(KeyCode.G);
		if (keyDown || keyDown2)
		{
			ToggleMode();
		}
	}

	public void ToggleMode()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && !roomInfo.IsSingleGameModel() && !roomInfo.IsPVP())
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
}

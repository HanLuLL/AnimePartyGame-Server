using UnityEngine;

namespace FairyGUI;

public class InputEvent
{
	public float x { get; internal set; }

	public float y { get; internal set; }

	public KeyCode keyCode { get; internal set; }

	public char character { get; internal set; }

	public EventModifiers modifiers { get; internal set; }

	public float mouseWheelDelta { get; internal set; }

	public int touchId { get; internal set; }

	public int button { get; internal set; }

	public int clickCount { get; internal set; }

	public float holdTime { get; internal set; }

	public Vector2 position => new Vector2(x, y);

	public bool isDoubleClick
	{
		get
		{
			if (clickCount > 1)
			{
				return button == 0;
			}
			return false;
		}
	}

	public bool ctrlOrCmd
	{
		get
		{
			if (!ctrl)
			{
				return command;
			}
			return true;
		}
	}

	public bool ctrl
	{
		get
		{
			if (!Input.GetKey(KeyCode.LeftControl))
			{
				return Input.GetKey(KeyCode.RightControl);
			}
			return true;
		}
	}

	public bool shift
	{
		get
		{
			if (!Input.GetKey(KeyCode.LeftShift))
			{
				return Input.GetKey(KeyCode.RightShift);
			}
			return true;
		}
	}

	public bool alt
	{
		get
		{
			if (!Input.GetKey(KeyCode.LeftAlt))
			{
				return Input.GetKey(KeyCode.RightAlt);
			}
			return true;
		}
	}

	public bool command
	{
		get
		{
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				if (!Input.GetKey(KeyCode.LeftMeta))
				{
					return Input.GetKey(KeyCode.RightMeta);
				}
				return true;
			}
			return false;
		}
	}

	public InputEvent()
	{
		touchId = -1;
		x = 0f;
		y = 0f;
		clickCount = 0;
		keyCode = KeyCode.None;
		character = '\0';
		modifiers = (EventModifiers)0;
		mouseWheelDelta = 0f;
	}
}

using UnityEngine;

namespace FairyGUI;

public static class CursorConfig
{
	private static Texture2D _CursorDownUT;

	private static Texture2D _CursorUpUT;

	private static Texture2D _CrosshairUT;

	public static CursorType cursorType = CursorType.Mouse;

	public static Texture2D CursorDownUT
	{
		get
		{
			if ((object)_CursorDownUT == null)
			{
				_CursorDownUT = Resources.Load<Texture2D>("Cursors/cursor_down");
			}
			return _CursorDownUT;
		}
	}

	public static Texture2D CursorUpUT
	{
		get
		{
			if ((object)_CursorUpUT == null)
			{
				_CursorUpUT = Resources.Load<Texture2D>("Cursors/cursor_up");
			}
			return _CursorUpUT;
		}
	}

	public static Texture2D CrosshairUT
	{
		get
		{
			if ((object)_CrosshairUT == null)
			{
				_CrosshairUT = Resources.Load<Texture2D>("Cursors/cursor_down");
			}
			return _CrosshairUT;
		}
	}

	public static void SetCursorType(CursorType Type)
	{
		cursorType = Type;
	}
}

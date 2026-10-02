using UnityEngine;

namespace FairyGUI;

public class TouchScreenKeyboard : IKeyboard
{
	private UnityEngine.TouchScreenKeyboard _keyboard;

	public bool done
	{
		get
		{
			if (_keyboard != null && _keyboard.status != UnityEngine.TouchScreenKeyboard.Status.Done && _keyboard.status != UnityEngine.TouchScreenKeyboard.Status.Canceled)
			{
				return _keyboard.status == UnityEngine.TouchScreenKeyboard.Status.LostFocus;
			}
			return true;
		}
	}

	public bool supportsCaret => false;

	public string GetInput()
	{
		if (_keyboard != null)
		{
			string text = _keyboard.text;
			if (done)
			{
				_keyboard = null;
			}
			return text;
		}
		return null;
	}

	public void Open(string text, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int keyboardType, bool hideInput)
	{
		if (_keyboard == null)
		{
			UnityEngine.TouchScreenKeyboard.hideInput = hideInput;
			_keyboard = UnityEngine.TouchScreenKeyboard.Open(text, (TouchScreenKeyboardType)keyboardType, autocorrection, multiline, secure, alert, textPlaceholder);
		}
	}

	public void Close()
	{
		if (_keyboard != null)
		{
			_keyboard.active = false;
			_keyboard = null;
		}
	}
}

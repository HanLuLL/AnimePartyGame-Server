using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

internal class TextInputHistory
{
	private static TextInputHistory _inst;

	private List<string> _undoBuffer;

	private List<string> _redoBuffer;

	private string _currentText;

	private InputTextField _textField;

	private bool _lock;

	private int _changedFrame;

	public const int maxHistoryLength = 5;

	public static TextInputHistory inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new TextInputHistory();
			}
			return _inst;
		}
	}

	public TextInputHistory()
	{
		_undoBuffer = new List<string>();
		_redoBuffer = new List<string>();
	}

	public void StartRecord(InputTextField textField)
	{
		_undoBuffer.Clear();
		_redoBuffer.Clear();
		_textField = textField;
		_lock = false;
		_currentText = textField.text;
		_changedFrame = 0;
	}

	public void MarkChanged(InputTextField textField)
	{
		if (_textField != textField || _lock)
		{
			return;
		}
		string text = _textField.text;
		if (_currentText == text)
		{
			return;
		}
		if (_changedFrame != Time.frameCount)
		{
			_changedFrame = Time.frameCount;
			_undoBuffer.Add(_currentText);
			if (_undoBuffer.Count > 5)
			{
				_undoBuffer.RemoveAt(0);
			}
		}
		else
		{
			int count = _undoBuffer.Count;
			if (count > 0 && text == _undoBuffer[count - 1])
			{
				_undoBuffer.RemoveAt(count - 1);
			}
		}
		_currentText = text;
	}

	public void StopRecord(InputTextField textField)
	{
		if (_textField == textField)
		{
			_undoBuffer.Clear();
			_redoBuffer.Clear();
			_textField = null;
			_currentText = null;
		}
	}

	public void Undo(InputTextField textField)
	{
		if (_textField == textField && _undoBuffer.Count != 0)
		{
			string text = _undoBuffer[_undoBuffer.Count - 1];
			_undoBuffer.RemoveAt(_undoBuffer.Count - 1);
			_redoBuffer.Add(_currentText);
			_lock = true;
			int caretPosition = _textField.caretPosition;
			_textField.text = text;
			int num = text.Length - _currentText.Length;
			if (num < 0)
			{
				_textField.caretPosition = caretPosition + num;
			}
			_currentText = text;
			_lock = false;
		}
	}

	public void Redo(InputTextField textField)
	{
		if (_textField == textField && _redoBuffer.Count != 0)
		{
			string text = _redoBuffer[_redoBuffer.Count - 1];
			_redoBuffer.RemoveAt(_redoBuffer.Count - 1);
			_undoBuffer.Add(_currentText);
			_lock = true;
			int caretPosition = _textField.caretPosition;
			_textField.text = text;
			int num = text.Length - _currentText.Length;
			if (num > 0)
			{
				_textField.caretPosition = caretPosition + num;
			}
			_currentText = text;
			_lock = false;
		}
	}
}

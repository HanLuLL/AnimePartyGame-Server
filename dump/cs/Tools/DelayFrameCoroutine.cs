using System;
using System.Collections;

namespace Tools;

public class DelayFrameCoroutine : IEnumerator
{
	private readonly int _delayFrame;

	private readonly Action _action;

	private int _currentFrame;

	public object Current => _currentFrame;

	public DelayFrameCoroutine(int delayFrame, Action action)
	{
		_delayFrame = delayFrame;
		_action = action;
		_currentFrame = _delayFrame;
	}

	public bool MoveNext()
	{
		_currentFrame--;
		if (_currentFrame == -1)
		{
			_action();
		}
		return _currentFrame > -1;
	}

	public void Reset()
	{
		_currentFrame = _delayFrame;
	}
}

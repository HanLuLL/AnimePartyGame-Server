using System;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearDisplay : GearBase
{
	private int _visible;

	private uint _displayLockToken;

	public string[] pages { get; set; }

	public bool connected
	{
		get
		{
			if (_controller != null)
			{
				return _visible > 0;
			}
			return true;
		}
	}

	public GearDisplay(GObject owner)
		: base(owner)
	{
		_displayLockToken = 1u;
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
	}

	protected override void Init()
	{
		pages = null;
	}

	public override void Apply()
	{
		_displayLockToken++;
		if (_displayLockToken == 0)
		{
			_displayLockToken = 1u;
		}
		if (pages == null || pages.Length == 0 || Array.IndexOf(pages, _controller.selectedPageId) != -1)
		{
			_visible = 1;
		}
		else
		{
			_visible = 0;
		}
	}

	public override void UpdateState()
	{
	}

	public uint AddLock()
	{
		_visible++;
		return _displayLockToken;
	}

	public void ReleaseLock(uint token)
	{
		if (token == _displayLockToken)
		{
			_visible--;
		}
	}
}

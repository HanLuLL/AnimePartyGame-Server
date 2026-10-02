using System;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearDisplay2 : GearBase
{
	public int condition;

	private int _visible;

	public string[] pages { get; set; }

	public GearDisplay2(GObject owner)
		: base(owner)
	{
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

	public bool Evaluate(bool connected)
	{
		bool flag = _controller == null || _visible > 0;
		if (condition == 0)
		{
			return flag && connected;
		}
		return flag || connected;
	}
}

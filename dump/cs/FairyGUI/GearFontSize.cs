using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearFontSize : GearBase
{
	private Dictionary<string, int> _storage;

	private int _default;

	public GearFontSize(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = ((GTextField)_owner).textFormat.size;
		_storage = new Dictionary<string, int>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		if (pageId == null)
		{
			_default = buffer.ReadInt();
		}
		else
		{
			_storage[pageId] = buffer.ReadInt();
		}
	}

	public override void Apply()
	{
		_owner._gearLocked = true;
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		TextFormat textFormat = ((GTextField)_owner).textFormat;
		textFormat.size = value;
		((GTextField)_owner).textFormat = textFormat;
		_owner._gearLocked = false;
	}

	public override void UpdateState()
	{
		_storage[_controller.selectedPageId] = ((GTextField)_owner).textFormat.size;
	}
}

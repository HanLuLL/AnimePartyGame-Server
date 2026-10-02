using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearText : GearBase
{
	private Dictionary<string, string> _storage;

	private string _default;

	public GearText(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = _owner.text;
		_storage = new Dictionary<string, string>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		if (pageId == null)
		{
			_default = buffer.ReadS();
		}
		else
		{
			_storage[pageId] = buffer.ReadS();
		}
	}

	public override void Apply()
	{
		_owner._gearLocked = true;
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		_owner.text = value;
		_owner._gearLocked = false;
	}

	public override void UpdateState()
	{
		_storage[_controller.selectedPageId] = _owner.text;
	}
}

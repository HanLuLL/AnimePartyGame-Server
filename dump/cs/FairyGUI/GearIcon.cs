using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearIcon : GearBase
{
	private Dictionary<string, string> _storage;

	private string _default;

	public GearIcon(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = _owner.icon;
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
		_owner.icon = value;
		_owner._gearLocked = false;
	}

	public override void UpdateState()
	{
		_storage[_controller.selectedPageId] = _owner.icon;
	}
}

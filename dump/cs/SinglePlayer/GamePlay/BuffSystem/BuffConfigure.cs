using System.Collections.Generic;

namespace SinglePlayer.GamePlay.BuffSystem;

public class BuffConfigure
{
	public uint Id { get; set; }

	public uint Delay { get; set; }

	public uint Duration { get; set; }

	public uint Interval { get; set; }

	public BuffTriggerType TriggerType { get; set; }

	public Dictionary<int, object> Effects { get; set; }

	public BuffAddType AddType { get; set; }

	public uint MaxLayer { get; set; }

	public int Name { get; set; }

	public int Description { get; set; }
}

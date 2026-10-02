using System;

namespace SinglePlayer.GamePlay.BuffSystem;

public sealed class RoundTimer
{
	public System.Action Completed;

	public System.Action Cancelled;

	public Action<uint> IntervalExecute;

	public System.Action Start;

	public uint Duration { get; set; }

	public uint Interval { get; set; }

	public uint Delay { get; set; }

	public uint StartRound { get; set; }

	public uint EndRound { get; set; }

	public uint TickCount { get; set; }
}

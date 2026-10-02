using System;

namespace GameLogic.Replay;

public readonly struct ReplayFrame
{
	public int Index { get; }

	public int CmdId { get; }

	public byte[] Payload { get; }

	public bool HasPayload => Payload.Length != 0;

	public int PayloadLength => Payload.Length;

	public ReplayFrame(int index, int cmdId, byte[] payload)
	{
		Index = index;
		CmdId = cmdId;
		Payload = payload ?? Array.Empty<byte>();
	}
}

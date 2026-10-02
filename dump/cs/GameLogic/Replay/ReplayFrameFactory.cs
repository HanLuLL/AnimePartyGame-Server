using Core;
using Core.Net;
using UnityEngine;

namespace GameLogic.Replay;

public static class ReplayFrameFactory
{
	private const string LogPrefix = "[ReplayFrameFactory]";

	public static Frame CreateS2CFrame(ReplayFrame replayFrame)
	{
		if (replayFrame.CmdId <= 0)
		{
			Debug.LogError(string.Format("{0} 构造帧失败：非法 cmdId={1}, index={2}。", "[ReplayFrameFactory]", replayFrame.CmdId, replayFrame.Index));
			return null;
		}
		byte[] payload = replayFrame.Payload;
		ByteBuf byteBuf = new ByteBuf(35 + payload.Length);
		byteBuf.WriteInt(payload.Length);
		byteBuf.WriteLong(0L);
		byteBuf.WriteShort((short)replayFrame.CmdId);
		byteBuf.WriteByte((byte)GameSettings.VER1);
		byteBuf.WriteByte((byte)GameSettings.VER2);
		byteBuf.WriteByte((byte)GameSettings.VER3);
		byteBuf.WriteLong(0L);
		byteBuf.WriteLong(replayFrame.Index);
		byteBuf.WriteShort(0);
		if (payload.Length != 0)
		{
			byteBuf.WriteBytes(new ByteBuf(payload), payload.Length);
		}
		byteBuf.ReaderIndex(0);
		return Frame.AnalyzeInfoFromBuf(byteBuf);
	}
}

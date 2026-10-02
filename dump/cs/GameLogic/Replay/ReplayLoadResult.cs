using System.Collections.Generic;
using party.protocol;

namespace GameLogic.Replay;

public class ReplayLoadResult
{
	public bool Success { get; set; }

	public string ErrorCode { get; set; }

	public string ErrorMessage { get; set; }

	public ReplayPackage Package { get; set; }

	public RunningGameS2C RunningGame { get; set; }

	public int RebuildFrameIndex { get; set; } = -1;

	public int PlaybackStartFrameIndex { get; set; } = -1;

	public GameFinishS2C GameFinish { get; set; }

	public int GameFinishFrameIndex { get; set; } = -1;

	public List<string> Warnings { get; } = new List<string>();

	public void AddWarning(string warning)
	{
		if (!string.IsNullOrEmpty(warning))
		{
			Warnings.Add(warning);
		}
	}
}

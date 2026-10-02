using System;
using System.Collections.Generic;
using FairyGUI;

namespace Core.Audio;

public static class BGMHelper
{
	private static int currentBGMEventID;

	public static void TryPlayBGM(int newBGMEventID, Action<int, int> onPlayStarted = null)
	{
		if (newBGMEventID != 0L && currentBGMEventID != newBGMEventID)
		{
			currentBGMEventID = newBGMEventID;
			Stage.inst.PlayOneShotSound(newBGMEventID);
		}
	}

	public static void TryBattleRoleVoice(int voiceID, Action<int, int> onPlayStarted = null)
	{
		if (voiceID != 0L)
		{
			Stage.inst.PlayOneShotSound(voiceID);
		}
	}

	public static List<uint> PlayBGMInBattle()
	{
		return new List<uint>();
	}

	public static void Clear()
	{
		currentBGMEventID = 0;
	}
}

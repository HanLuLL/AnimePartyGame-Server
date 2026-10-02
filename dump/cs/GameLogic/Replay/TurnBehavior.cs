using System;

namespace GameLogic.Replay;

[Flags]
public enum TurnBehavior
{
	NONE = 0,
	TRANSFER_GOLD = 1,
	LEVEL_UP = 2,
	KILL_MONSTER = 4
}

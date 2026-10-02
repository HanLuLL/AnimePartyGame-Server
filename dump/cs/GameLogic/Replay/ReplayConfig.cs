namespace GameLogic.Replay;

public static class ReplayConfig
{
	public const GameSpeedType REPLAY_BASE_SPEED_TYPE = GameSpeedType.Fast;

	public const int FRAME_DELAY_MS = 16;

	public const int JUMP_DELAY_MS = 1000;

	public static readonly float[] PLAY_SPEED_SEQUENCE = new float[4] { 1f, 2f, 4f, 0.5f };
}

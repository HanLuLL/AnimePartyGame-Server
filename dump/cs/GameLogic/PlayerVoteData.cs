using Tools;

namespace GameLogic;

public class PlayerVoteData
{
	public BattlePlayerData PlayerData;

	public CharacterHandle SelectVoteCharacterInfo;

	public int Point;

	public bool Affirm;

	public AssistVoteStatus VoteStatus;

	public long PlayerId
	{
		get
		{
			if (PlayerData != null)
			{
				return PlayerData.player.Id;
			}
			return 0L;
		}
		set
		{
			PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(value);
		}
	}

	public int SelectId
	{
		get
		{
			if (SelectVoteCharacterInfo == null)
			{
				return 0;
			}
			return SelectVoteCharacterInfo.Id;
		}
		set
		{
			if (SelectVoteCharacterInfo == null || SelectVoteCharacterInfo.Id != value)
			{
				SelectVoteCharacterInfo = new CharacterHandle(value);
			}
		}
	}
}

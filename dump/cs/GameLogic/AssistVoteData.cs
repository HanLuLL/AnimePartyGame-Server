using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace GameLogic;

public class AssistVoteData
{
	public readonly Dictionary<long, PlayerVoteData> PlayerVoteDict = new Dictionary<long, PlayerVoteData>();

	public void UpdateVoteData(long playerId, int selectId, int point, bool affirm, AssistVoteStatus voteStatus)
	{
		PlayerVoteData playerVoteData = GetPlayerVoteData(playerId);
		playerVoteData.Point = point;
		playerVoteData.SelectId = selectId;
		playerVoteData.Affirm = affirm;
		playerVoteData.VoteStatus = voteStatus;
	}

	public void UpdateVotePoint(long playerId, int point, AssistVoteStatus voteStatus)
	{
		PlayerVoteData playerVoteData = GetPlayerVoteData(playerId);
		playerVoteData.Point = point;
		playerVoteData.VoteStatus = voteStatus;
	}

	public PlayerVoteData GetPlayerVoteData(long playerId)
	{
		if (playerId == 0L)
		{
			Debug.LogError("尝试获取投票数据时 PlayerId is 0 ,存在问题");
		}
		if (!PlayerVoteDict.TryGetValue(playerId, out var value))
		{
			value = new PlayerVoteData
			{
				PlayerId = playerId
			};
			PlayerVoteDict.Add(playerId, value);
		}
		return value;
	}

	public PlayerVoteData GetSelfVoteData()
	{
		return GetPlayerVoteData(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
	}
}

using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class PlayerCareer
{
	public long playerId;

	private string _nick;

	public int _LV;

	public (string, bool) _LabelData;

	public string _HeadIcon;

	public int StandingPainting;

	public List<int> acheveIds;

	public bool isShowData;

	public bool isShowFight;

	public List<BattleShortRecord> Records;

	public ShowPlayerStatistics statistics;

	public FightRecordDetail recordDetail;

	public int praiseNum;

	public readonly List<long> AccuseList = new List<long>();

	private List<BattleShortRecord> _replayRecord = new List<BattleShortRecord>();

	public string GetNick(bool showRemark = false)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(playerId, _nick, showRemark);
	}

	public void UpdateData(GetShowPlayerS2C model)
	{
		playerId = model.ShowData.PlayerId;
		StandingPainting = model.ShowData.StandingPainting;
		DealAchieveIds(model.ShowData.AchieveId);
		isShowData = model.ShowData.IsShowData;
		isShowFight = model.ShowData.IsShowFight;
		DealRecords(model.ShowData.Record);
		statistics = model.ShowData.Statistics;
		praiseNum = model.ShowData.PraiseNum;
	}

	public void UpdateNick(string nick)
	{
		_nick = nick;
	}

	public void UpdateBaseInfo(string nick, int lv, string headIcon, (string, bool) labelData)
	{
		_nick = nick;
		_LV = lv;
		_HeadIcon = headIcon;
		_LabelData = labelData;
	}

	private void DealAchieveIds(RepeatedField<int> showDataAchieveId)
	{
		int num = 6;
		acheveIds = new List<int>(num) { 0, 0, 0, 0, 0, 0 };
		for (int i = 0; i < num; i++)
		{
			if (showDataAchieveId.Count > i)
			{
				acheveIds[i] = showDataAchieveId[i];
			}
		}
	}

	private void DealRecords(RepeatedField<ShowPlayerShortFight> showDataRecords)
	{
		Records = new List<BattleShortRecord>();
		for (int i = 0; i < showDataRecords.Count; i++)
		{
			BattleShortRecord item = new BattleShortRecord(showDataRecords[i], BattleShortRecordType.Battle);
			Records.Add(item);
		}
		Records.Sort((BattleShortRecord x, BattleShortRecord y) => (x.Time <= y.Time) ? 1 : (-1));
	}

	public void UpdateStandingPainting(int _StandingPainting)
	{
		StandingPainting = _StandingPainting;
	}

	public void UpdateAchieveIds(int index, int achieveId)
	{
		if (acheveIds.Count > index)
		{
			acheveIds[index] = achieveId;
		}
	}

	public void UpdateShowDataStatus(bool _isShowData)
	{
		isShowData = _isShowData;
	}

	public void UpdateShowFightStatus(bool _isShowFight)
	{
		isShowFight = _isShowFight;
	}

	public void UpdateFightRecordDetail(RepeatedField<PlayerFightData> modelRecordData)
	{
		recordDetail = new FightRecordDetail(modelRecordData);
	}

	public void AccusePlayer(long _AccusePlayerId)
	{
		if (_AccusePlayerId != 0L && !AccuseList.Contains(_AccusePlayerId))
		{
			AccuseList.Add(_AccusePlayerId);
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestAccuseC2S(_AccusePlayerId);
		}
	}

	public List<BattleShortRecord> UpdateReplayRecord(List<BattleShortRecord> replayRecords)
	{
		_replayRecord.Clear();
		_replayRecord.AddRange(replayRecords);
		_replayRecord.Sort((BattleShortRecord x, BattleShortRecord y) => (x.Time <= y.Time) ? 1 : (-1));
		return _replayRecord;
	}

	public bool RemoveReplayRecord(string replayId)
	{
		if (string.IsNullOrEmpty(replayId))
		{
			return false;
		}
		return _replayRecord.RemoveAll((BattleShortRecord x) => x.ReplayId == replayId) > 0;
	}
}

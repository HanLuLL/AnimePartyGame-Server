using System.Collections.Generic;
using Google.Protobuf.Collections;
using UI;
using party.model;

namespace GameLogic;

public class FightRecordDetail
{
	public List<RecordRank> rankData;

	public FightRecordDetail(RepeatedField<PlayerFightData> _FightRecords)
	{
		rankData = new List<RecordRank>(4);
		for (int i = 0; i < _FightRecords.Count; i++)
		{
			string local = StaticConfigure.Player.CoverNames[i].CoverNameID.GetLocal(UIStringType.Player);
			RecordRank item = new RecordRank(_FightRecords[i], local);
			rankData.Add(item);
		}
		rankData.Sort(ComparerByRank);
	}

	private int ComparerByRank(RecordRank x, RecordRank y)
	{
		return -y.data.Rank.CompareTo(x.data.Rank);
	}
}

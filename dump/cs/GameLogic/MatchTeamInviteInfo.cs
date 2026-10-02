using Core.Net;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class MatchTeamInviteInfo
{
	public long PlayerId;

	public long TeamId;

	public long Time;

	public string Nick;

	public int LV;

	private int _headPropId;

	private int _labelPropId;

	public string HeadURL => ((_headPropId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[1] : _headPropId).GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();

	public (string, bool) Label => ((_labelPropId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[2] : _labelPropId).GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();

	public MatchTeamInviteInfo(MatchTeamInviteNotify model)
	{
		LV = model.Lv;
		Nick = model.Name;
		PlayerId = model.PlayerId;
		TeamId = model.TeamId;
		Time = (long)MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForMilliseconds();
		_labelPropId = model.Background;
		_headPropId = model.HeadIcon;
	}
}

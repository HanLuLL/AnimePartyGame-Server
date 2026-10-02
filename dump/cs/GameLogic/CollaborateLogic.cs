using System;
using Core.Net;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace GameLogic;

public class CollaborateLogic
{
	public CollaborationInfoConfigure TryGetCollaboration()
	{
		foreach (CollaborationInfoConfigure info in StaticConfigure.Collaboration.Infos)
		{
			if (TimeHelper.ValidityTime(info.BeginTime, info.EndTime))
			{
				return info;
			}
		}
		return null;
	}

	public string GetCutDown(Timestamp EndTime)
	{
		if ((object)EndTime == null)
		{
			return null;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime endTime = EndTime.ToDateTime();
		return TimeHelper.RefreshTimeText(1052, 1053, serverTime, endTime);
	}
}

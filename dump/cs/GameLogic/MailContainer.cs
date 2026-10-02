using System.Collections.Generic;
using party.model;

namespace GameLogic;

public class MailContainer
{
	private readonly Dictionary<int, MailData> mailDict = new Dictionary<int, MailData>();

	public void TryAdd(party.model.MailData data)
	{
		MailData value = new MailData(data);
		TryRemove(data.Id);
		mailDict.Add(data.Id, value);
	}

	public void TryRemove(int mailId)
	{
		if (mailDict.ContainsKey(mailId))
		{
			mailDict.Remove(mailId);
		}
	}

	public void UpdataStatus(int mailId)
	{
		if (mailDict.TryGetValue(mailId, out var value))
		{
			value.UpdateFinishStatus();
		}
	}

	public MailData TryGetMail(int mailId)
	{
		return mailDict.GetValueOrDefault(mailId);
	}

	public List<MailData> TryGetMails()
	{
		List<MailData> list = new List<MailData>();
		foreach (KeyValuePair<int, MailData> item in mailDict)
		{
			if (item.Value.Available)
			{
				list.Add(item.Value);
			}
		}
		list.Sort(CompareTo);
		return list;
	}

	private int CompareTo(MailData x, MailData y)
	{
		int num = x.IsFinish().CompareTo(y.IsFinish());
		if (num != 0)
		{
			return num;
		}
		num = -x.CollectedStatus.CompareTo(y.CollectedStatus);
		if (num != 0)
		{
			return num;
		}
		num = -x.StartTime.CompareTo(y.StartTime);
		if (num != 0)
		{
			return num;
		}
		return -x.Id.CompareTo(y.Id);
	}

	public bool MailStatus()
	{
		foreach (KeyValuePair<int, MailData> item in mailDict)
		{
			if (!item.Value.IsFinish())
			{
				return true;
			}
		}
		return false;
	}

	public bool MailRewardStatus()
	{
		foreach (KeyValuePair<int, MailData> item in mailDict)
		{
			if (item.Value.IsNeedGetReward())
			{
				return true;
			}
		}
		return false;
	}
}

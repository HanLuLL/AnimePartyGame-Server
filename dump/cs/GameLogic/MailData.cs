using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class MailData
{
	private readonly int _id;

	private readonly string _title;

	private readonly string _context;

	private readonly string _sendName;

	private bool _isRead;

	private bool _isGetReward;

	private readonly long _createTime;

	private readonly long _startTime;

	private readonly string _formatTime;

	private readonly List<KeyValuePair<int, int>> _rewards;

	private readonly long _deadlineTime;

	private bool _collectedStatus;

	public int Id => _id;

	public string Title => _title;

	public string Context => _context;

	public string SendName => _sendName;

	public long CreateTime => _createTime;

	public long StartTime => _startTime;

	public string FormatTime => _formatTime;

	public List<KeyValuePair<int, int>> Rewards => _rewards;

	public bool CollectedStatus => _collectedStatus;

	public string DeadTimeDesc
	{
		get
		{
			if (_deadlineTime != 0L && _deadlineTime > _startTime)
			{
				TimeSpan timeSpan = (_deadlineTime * 1000).StampMillisecondsToDateTime() - MonoSingletonProvider<NetManager>.inst.ServerTime;
				if (timeSpan.TotalDays > 1.0)
				{
					return string.Format(1040.GetLocal(UIStringType.Message), timeSpan.Days);
				}
				if (timeSpan.TotalHours > 1.0)
				{
					return string.Format(1041.GetLocal(UIStringType.Message), timeSpan.Hours);
				}
				if (timeSpan.TotalMinutes > 0.0)
				{
					return string.Format(1042.GetLocal(UIStringType.Message), timeSpan.Minutes);
				}
			}
			return "";
		}
	}

	public bool Available
	{
		get
		{
			int num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
			if (_deadlineTime != 0L && num > _deadlineTime)
			{
				return false;
			}
			if (_startTime != 0L && num < _startTime)
			{
				return false;
			}
			return true;
		}
	}

	public MailData(party.model.MailData _data)
	{
		_id = _data.Id;
		_isRead = _data.IsRead;
		_isGetReward = _data.IsGetReward;
		_createTime = _data.CreateTime;
		if (_data.StartTime < 0)
		{
			_startTime = 1785412361L;
		}
		else
		{
			_startTime = _data.StartTime;
		}
		_deadlineTime = _data.ExpireTime;
		_rewards = SimpleSingletonProvider<GameLogicManager>.inst.bag.PropItemsSort(_data.Rewards.ToList());
		_collectedStatus = _data.IsStarMail;
		_formatTime = (((_startTime == 0L) ? _data.CreateTime : _startTime) * 1000).StampMillisecondsToDateTime().ToUIDateTime_YMDHM();
		try
		{
			MailByServer content = JsonUtility.FromJson<MailByServer>(_data.Context);
			_context = GetLocal(content);
		}
		catch (Exception ex)
		{
			Debug.Log("邮件内容文本格式错误，无法解析" + ex);
			_context = _data.Context;
		}
		try
		{
			MailByServer content2 = JsonUtility.FromJson<MailByServer>(_data.Title);
			_title = GetLocal(content2);
		}
		catch (Exception ex2)
		{
			Debug.Log("邮件标题文本格式错误，无法解析" + ex2);
			_title = _data.Title;
		}
		try
		{
			MailByServer content3 = JsonUtility.FromJson<MailByServer>(_data.SendName);
			_sendName = GetLocal(content3);
		}
		catch (Exception ex3)
		{
			Debug.Log("邮件发送人文本格式错误，无法解析" + ex3);
			_sendName = _data.SendName;
		}
	}

	public bool IsFinish()
	{
		if (!Available)
		{
			return true;
		}
		if (_rewards == null || _rewards.Count == 0)
		{
			return _isRead;
		}
		return _isGetReward;
	}

	public bool IsNeedRead()
	{
		return !_isRead;
	}

	public bool IsNeedGetReward()
	{
		if (_rewards != null && _rewards.Count > 0 && !_isGetReward)
		{
			return Available;
		}
		return false;
	}

	public void UpdateIsReadStatus()
	{
		_isRead = true;
	}

	public void UpdateFinishStatus()
	{
		_isGetReward = true;
		UpdateIsReadStatus();
	}

	private string GetLocal(MailByServer content)
	{
		return GameSettings.GetDataForLanguage(content._English, content._Japanese, content._Simplified, content._Traditional) + content._Item;
	}

	public void UpdateCollectedStatus(bool status)
	{
		_collectedStatus = status;
	}
}

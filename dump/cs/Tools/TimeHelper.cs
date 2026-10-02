using System;
using Core.Net;
using Google.Protobuf.WellKnownTypes;
using UI;

namespace Tools;

public static class TimeHelper
{
	public static string GetDailyTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime endTime = ((serverTime.Hour >= 4) ? serverTime.Date.AddDays(1.0).AddHours(4.0) : serverTime.Date.AddHours(4.0));
		return RefreshTimeText(1012, 1013, serverTime, endTime);
	}

	public static string GetWeeklyTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime dateTime = serverTime.AddDays(0 - serverTime.DayOfWeek + 1).Date.AddHours(4.0);
		if (serverTime > dateTime)
		{
			dateTime = dateTime.AddDays(7.0);
		}
		return RefreshTimeText(1012, 1013, serverTime, dateTime);
	}

	public static string GetMonthlyTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		return RefreshTimeText(endTime: (serverTime.Day != 1 || serverTime.Hour >= 4) ? serverTime.AddMonths(1).Date.AddDays(1 - serverTime.Day).AddHours(4.0) : new DateTime(serverTime.Year, serverTime.Month, 1).AddHours(4.0), messageId1: 1012, messageId2: 1013, nowTime: serverTime);
	}

	public static string RefreshTimeText(int messageId1, int messageId2, DateTime nowTime, DateTime endTime)
	{
		if (nowTime > endTime)
		{
			return "";
		}
		TimeSpan timeSpan = endTime - nowTime;
		if (timeSpan.Days > 0)
		{
			return string.Format(messageId1.GetLocal(UIStringType.Message), timeSpan.Days.ToString().PadLeft(2, '0'), (timeSpan.Hours + 1).ToString().PadLeft(2, '0'));
		}
		return string.Format(messageId2.GetLocal(UIStringType.Message), timeSpan.Hours.ToString().PadLeft(2, '0'), timeSpan.Minutes.ToString().PadLeft(2, '0'));
	}

	public static bool ValidityTime(Timestamp _beginTime, Timestamp _endTime)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if ((object)_beginTime == null)
		{
			if (_endTime == null || serverTime < _endTime.ToDateTime())
			{
				return true;
			}
			return false;
		}
		if (_endTime == null)
		{
			if (serverTime > _beginTime.ToDateTime())
			{
				return true;
			}
		}
		else if (serverTime > _beginTime.ToDateTime() && serverTime < _endTime.ToDateTime())
		{
			return true;
		}
		return false;
	}

	public static bool ValidityTime(DateTime _beginTime, DateTime _endTime)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (serverTime > _beginTime && serverTime < _endTime)
		{
			return true;
		}
		return false;
	}

	public static bool ValidityTime(long _beginTime, long _endTime)
	{
		int num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		if (num > _beginTime && num < _endTime)
		{
			return true;
		}
		return false;
	}

	public static string GetDurationText(Timestamp BeginTime, Timestamp EndTime, bool OnlyDuration = false)
	{
		if ((object)BeginTime == null || (object)EndTime == null)
		{
			return "";
		}
		DateTime beginTime = BeginTime.ToDateTime();
		DateTime endTime = EndTime.ToDateTime();
		return GetDurationText(beginTime, endTime, OnlyDuration);
	}

	public static string GetDurationText(DateTime beginTime, DateTime endTime, bool OnlyDuration = false)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (serverTime > endTime)
		{
			return "";
		}
		if ((endTime - serverTime).TotalDays > 365.0)
		{
			return "";
		}
		if ((endTime - serverTime).Days > 0 || OnlyDuration)
		{
			return $"{beginTime.Month:D2}.{beginTime.Day:D2}-{endTime.Month:D2}.{endTime.Day:D2}";
		}
		return RefreshTimeText(1033, 1034, serverTime, endTime);
	}

	public static string GetDurationText(long BeginTime, long EndTime, bool OnlyDuration = false)
	{
		DateTime beginTime = ((int)BeginTime).StampToDateTime();
		DateTime endTime = ((int)EndTime).StampToDateTime();
		return GetDurationText(beginTime, endTime, OnlyDuration);
	}
}

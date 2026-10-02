using System;
using System.Text;
using Core;
using Core.Net;
using UnityEngine;

namespace Tools;

public static class TimeUtils
{
	public static readonly TimeSpan HttpTimeOut = new TimeSpan(0, 0, 15);

	public static readonly DateTime OriginUtcDT = new DateTime(1970, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc);

	public static readonly DateTime OriginLocalDT = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

	public static readonly DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;

	private static readonly StringBuilder _sbs = new StringBuilder(50);

	public const double FRAME_TIME_DOUBLE = 1.0 / 60.0;

	public const double FrameRate = 60.0;

	public const int MilliPerFrame = 17;

	public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

	public static DateTime fourHoursForCurrentDateTime
	{
		get
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			return new DateTime(serverTime.Year, serverTime.Month, serverTime.Day, 4, 0, 0, 0);
		}
	}

	public static DateTime StampMillisecondsToDateTime(this long timeStamp)
	{
		DateTime originUtcDT = OriginUtcDT;
		return originUtcDT.AddMilliseconds(timeStamp);
	}

	public static DateTime StampToDateTime(this int timeStamp)
	{
		DateTime originUtcDT = OriginUtcDT;
		return originUtcDT.AddSeconds(timeStamp);
	}

	public static ulong DateTimeToStampForMilliseconds(this DateTime time)
	{
		return (ulong)(time - OriginUtcDT).TotalMilliseconds;
	}

	public static int DateTimeToStampForSeconds(this DateTime time)
	{
		return (int)(time - OriginUtcDT).TotalSeconds;
	}

	public static string ToUIDateTime_YMDHM(this DateTime time)
	{
		string text = null;
		if (GameSettings.languageType == LanguageType.English)
		{
			return $"{time.Month:D2}/{time.Day:D2}/{time.Year:D} {time.Hour:D2}:{time.Minute:D2}";
		}
		return $"{time.Year:D}-{time.Month:D2}-{time.Day:D2} {time.Hour:D2}:{time.Minute:D2}";
	}

	public static string TransTimeSecondIntToString(this uint second)
	{
		StringBuilder stringBuilder = new StringBuilder();
		uint num = second / 3600;
		uint num2 = second % 3600 / 60;
		uint num3 = second % 60;
		if (num < 10)
		{
			stringBuilder.Append("0");
			stringBuilder.Append(num.ToString());
		}
		else
		{
			stringBuilder.Append(num.ToString());
		}
		stringBuilder.Append(":");
		if (num2 < 10)
		{
			stringBuilder.Append("0");
			stringBuilder.Append(num2.ToString());
		}
		else
		{
			stringBuilder.Append(num2.ToString());
		}
		stringBuilder.Append(":");
		if (num3 < 10)
		{
			stringBuilder.Append("0");
			stringBuilder.Append(num3.ToString());
		}
		else
		{
			stringBuilder.Append(num3.ToString());
		}
		return stringBuilder.ToString();
	}
}

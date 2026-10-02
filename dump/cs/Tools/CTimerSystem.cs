using System.Collections.Generic;
using UnityEngine;

namespace Tools;

public class CTimerSystem
{
	public bool IsRealTimeTick = true;

	private uint timerIDGen = 1u;

	private Dictionary<uint, List<CTimerHandle>> m_TimerAxis;

	private Dictionary<int, List<uint>> m_TimerDict;

	private Dictionary<uint, CTimerHandle> m_AllTimers;

	private uint m_unLastCheckTick;

	private uint m_unInitializeTime;

	private uint m_unTimerAxisSize;

	private const int gs_MAX_TIME_AXIS_LENGTH = 720000;

	private const int gs_DEFAULT_CHECK_FREQUENCY = 16;

	private const int gs_DEFAULT_TIME_GRID = 64;

	public void Create()
	{
		m_unTimerAxisSize = 175u;
		m_TimerAxis = new Dictionary<uint, List<CTimerHandle>>();
		m_TimerDict = new Dictionary<int, List<uint>>();
		m_AllTimers = new Dictionary<uint, CTimerHandle>();
		m_unInitializeTime = __GetTickTime();
		m_unLastCheckTick = m_unInitializeTime;
	}

	public void Destroy()
	{
		if (m_TimerDict != null)
		{
			m_TimerDict.Clear();
			m_TimerAxis.Clear();
			m_AllTimers.Clear();
		}
	}

	public uint CreateTimer(uint uIntervalTime, DelITimerHander del, uint uCallTime = uint.MaxValue, string strInfo = "")
	{
		uint uTimerID = GenATimeID();
		if (del == null)
		{
			return 0u;
		}
		if (uIntervalTime == 0)
		{
			uIntervalTime = 1u;
		}
		int hashCode = del.GetHashCode();
		if (m_TimerDict.TryGetValue(hashCode, out var value))
		{
			if (value.Exists((uint p) => p == uTimerID))
			{
				return uTimerID;
			}
			value.Add(uTimerID);
		}
		else
		{
			value = new List<uint> { uTimerID };
			m_TimerDict.Add(hashCode, value);
		}
		CTimerHandle cTimerHandle = new CTimerHandle
		{
			m_Handle = del,
			m_unTimerID = uTimerID,
			m_unCallTimes = uCallTime,
			m_unIntervalTime = uIntervalTime,
			m_unLastCallTick = m_unLastCheckTick,
			m_nHandleHashCode = hashCode
		};
		if (!string.IsNullOrEmpty(strInfo))
		{
			cTimerHandle.m_strDebugInfo = strInfo;
		}
		cTimerHandle.m_unTimerGridIndex = (cTimerHandle.m_unLastCallTick + cTimerHandle.m_unIntervalTime - m_unInitializeTime) / 64 % m_unTimerAxisSize;
		if (!m_TimerAxis.TryGetValue(cTimerHandle.m_unTimerGridIndex, out var value2))
		{
			value2 = new List<CTimerHandle>();
			m_TimerAxis.Add(cTimerHandle.m_unTimerGridIndex, value2);
		}
		value2?.Add(cTimerHandle);
		m_AllTimers.Add(uTimerID, cTimerHandle);
		return uTimerID;
	}

	public void DestroyTimer(uint uTimerID)
	{
		if (!m_AllTimers.TryGetValue(uTimerID, out var value))
		{
			return;
		}
		m_AllTimers.Remove(uTimerID);
		DelITimerHander handle = value.m_Handle;
		if (handle == null)
		{
			return;
		}
		int hashCode = handle.GetHashCode();
		if ((!m_TimerDict.TryGetValue(hashCode, out var value2) || value2.Exists((uint p) => p == uTimerID)) && value2 != null)
		{
			value2.Remove(uTimerID);
			if (value2.Count == 0)
			{
				m_TimerDict.Remove(hashCode);
			}
		}
	}

	public void UpdateTimer()
	{
		uint num = __GetTickTime();
		if (num - m_unLastCheckTick < 16)
		{
			return;
		}
		uint num2 = (m_unLastCheckTick - m_unInitializeTime) / 64 % m_unTimerAxisSize;
		uint num3 = (num - m_unInitializeTime) / 64 % m_unTimerAxisSize;
		m_unLastCheckTick = num;
		uint num4 = num2;
		do
		{
			__UpdateByKey(num4, num);
			if (num4 != num3)
			{
				num4 = (num4 + 1) % m_unTimerAxisSize;
				continue;
			}
			break;
		}
		while (num4 != num3);
	}

	private uint GenATimeID()
	{
		timerIDGen++;
		return timerIDGen % int.MaxValue + 1;
	}

	private uint __GetTickTime()
	{
		if (IsRealTimeTick)
		{
			return (uint)(1000f * Time.unscaledTime);
		}
		return (uint)(1000f * Time.time);
	}

	private void __UpdateByKey(uint uKey, uint unCurTick)
	{
		if (m_TimerAxis.TryGetValue(uKey, out var value) && (value == null || value.Count == 0))
		{
			m_TimerAxis.Remove(uKey);
			value = null;
		}
		if (value == null)
		{
			return;
		}
		for (int num = value.Count - 1; num >= 0; num--)
		{
			CTimerHandle cTimerHandle = value[num];
			if (!__CheckByHashCode(cTimerHandle.m_nHandleHashCode, cTimerHandle.m_unTimerID))
			{
				value.RemoveAt(num);
			}
			else if (m_unLastCheckTick - cTimerHandle.m_unLastCallTick >= cTimerHandle.m_unIntervalTime)
			{
				uint num2 = __GetTickTime();
				cTimerHandle.m_Handle?.Invoke(cTimerHandle.m_unTimerID);
				if (cTimerHandle == value[num])
				{
					uint num3 = __GetTickTime() - num2;
					if (num3 > 64)
					{
						_ = 64;
					}
					cTimerHandle.m_unLastCallTick = unCurTick;
					cTimerHandle.m_unCallTimes--;
					if (cTimerHandle.m_unCallTimes == 0)
					{
						DestroyTimer(cTimerHandle.m_unTimerID);
					}
					else
					{
						uint num4 = (cTimerHandle.m_unLastCallTick + cTimerHandle.m_unIntervalTime - m_unInitializeTime) / 64 % m_unTimerAxisSize;
						if (cTimerHandle.m_unTimerGridIndex != num4)
						{
							cTimerHandle.m_unTimerGridIndex = num4;
							value.RemoveAt(num);
							if (!m_TimerAxis.TryGetValue(cTimerHandle.m_unTimerGridIndex, out var value2))
							{
								value2 = new List<CTimerHandle>();
								m_TimerAxis.Add(cTimerHandle.m_unTimerGridIndex, value2);
							}
							value2?.Add(cTimerHandle);
						}
					}
				}
				else
				{
					value.RemoveAt(num);
				}
			}
		}
	}

	private bool __CheckByHashCode(int nHashCode, uint uTimerID)
	{
		if (m_TimerDict.TryGetValue(nHashCode, out var value) && value.Exists((uint p) => p == uTimerID))
		{
			return true;
		}
		return false;
	}
}

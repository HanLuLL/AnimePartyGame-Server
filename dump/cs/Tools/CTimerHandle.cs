namespace Tools;

internal class CTimerHandle
{
	public DelITimerHander m_Handle;

	public int m_nHandleHashCode;

	public uint m_unTimerID;

	public uint m_unIntervalTime;

	public uint m_unCallTimes;

	public uint m_unLastCallTick;

	public uint m_unTimerGridIndex;

	public string m_strDebugInfo;

	public CTimerHandle()
	{
		m_Handle = null;
		m_nHandleHashCode = 0;
		m_unTimerID = 0u;
		m_unIntervalTime = 0u;
		m_unCallTimes = 0u;
		m_unLastCallTick = 0u;
		m_unTimerGridIndex = 0u;
		m_strDebugInfo = null;
	}
}

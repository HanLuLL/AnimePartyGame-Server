using System;
using Tools;
using UnityEngine;

namespace Core.Net;

public class RPCAsyncResult : CustomYieldInstruction
{
	internal object resultParam0;

	internal object resultParam1;

	internal ByteBuf param;

	internal bool isCompleted;

	internal Delegate callBack;

	internal long UPSN;

	internal int CMDID;

	internal ProtolcalType protoType = ProtolcalType.Ahead;

	internal Frame frame;

	internal bool isTimeOut;

	internal uint timerID;

	internal long sendTime;

	internal long receiveTime;

	public readonly Signal OnFinishedOnly = new Signal();

	public readonly Signal<RPCAsyncResult> OnFinished = new Signal<RPCAsyncResult>();

	public int errId;

	public override bool keepWaiting => !isCompleted;

	internal RPCAsyncResult()
	{
	}

	internal RPCAsyncResult(Delegate callBack)
	{
		this.callBack = callBack;
	}

	internal void Finish()
	{
		isCompleted = true;
		OnFinishedOnly.Dispatch();
		OnFinished.Dispatch(this);
	}

	internal void Clear()
	{
		isCompleted = true;
		OnFinishedOnly.RemoveAllListeners();
		OnFinished.RemoveAllListeners();
	}

	public void UpdataData(Frame _frame, int _cmd, long _upsn, int _err)
	{
		frame = _frame;
		CMDID = _cmd;
		UPSN = _upsn;
		errId = _err;
	}
}

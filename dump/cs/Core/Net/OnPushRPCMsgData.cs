namespace Core.Net;

internal class OnPushRPCMsgData
{
	internal Frame frame { get; }

	internal RPCAsyncResult callAction { get; }

	internal int CMDID { get; }

	internal long UPSN { get; }

	internal long DOWNSN { get; }

	internal ByteBuf PARAM { get; }

	internal int errId { get; }

	internal OnPushRPCMsgData(Frame _frame, RPCAsyncResult _callAction = null)
	{
		frame = _frame;
		CMDID = frame.CMDID;
		UPSN = frame.UPSN;
		DOWNSN = frame.DOWNSN;
		PARAM = frame.GetContent();
		errId = frame.ERR;
		if (_callAction != null)
		{
			callAction = _callAction;
			callAction.UpdataData(_frame, CMDID, UPSN, _frame.ERR);
		}
	}
}

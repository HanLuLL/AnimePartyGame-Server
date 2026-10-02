using Google.Protobuf.Reflection;
using UnityEngine;

public enum ServerErrorDealType
{
	[InspectorName("不处理")]
	[OriginalName("ServerErrorDealType_None")]
	None,
	[InspectorName("断线后重发")]
	[OriginalName("ServerErrorDealType_CloseNetAndResend")]
	CloseNetAndResend,
	[InspectorName("断线重连")]
	[OriginalName("ServerErrorDealType_ReconnectNet")]
	ReconnectNet,
	[InspectorName("回到登录")]
	[OriginalName("ServerErrorDealType_ReturnToLogin")]
	ReturnToLogin,
	[InspectorName("重启游戏")]
	[OriginalName("ServerErrorDealType_RebootGame")]
	RebootGame,
	[InspectorName("刷新界面")]
	[OriginalName("ServerErrorDealType_RefreshUI")]
	RefreshUi,
	[InspectorName("关闭游戏")]
	[OriginalName("ServerErrorDealType_QuitGame")]
	QuitGame,
	[InspectorName("排队重连")]
	[OriginalName("ServerErrorDealType_QueueReconnect")]
	QueueReconnect,
	[InspectorName("服务器繁忙")]
	[OriginalName("ServerErrorDealType_ServerBusy")]
	ServerBusy
}

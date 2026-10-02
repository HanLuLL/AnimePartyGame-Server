using Google.Protobuf.Reflection;
using UnityEngine;

public enum CreditActionType
{
	[InspectorName("无")]
	[OriginalName("CreditActionType_None")]
	None,
	[InspectorName("选人阶段秒退")]
	[OriginalName("CreditActionType_DraftDodge")]
	DraftDodge,
	[InspectorName("游戏内中途认输")]
	[OriginalName("CreditActionType_SurrenderIngame")]
	SurrenderIngame,
	[InspectorName("断线超3分钟未重连")]
	[OriginalName("CreditActionType_DisconnectTimeout")]
	DisconnectTimeout,
	[InspectorName("强制认输")]
	[OriginalName("CreditActionType_ForceSurrender")]
	ForceSurrender,
	[InspectorName("恶意退出且未重连")]
	[OriginalName("CreditActionType_ForceQuit")]
	ForceQuit,
	[InspectorName("每日首次登录")]
	[OriginalName("CreditActionType_DailyLoginClean")]
	DailyLoginClean,
	[InspectorName("正常完成一局对局")]
	[OriginalName("CreditActionType_FinishMatch")]
	FinishMatch,
	[InspectorName("GM工具")]
	[OriginalName("CreditActionType_GM")]
	Gm,
	[InspectorName("每周首次登录")]
	[OriginalName("CreditActionType_WeeklyFirstLogin")]
	WeeklyFirstLogin
}

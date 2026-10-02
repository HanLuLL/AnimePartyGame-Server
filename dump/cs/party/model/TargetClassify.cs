using Google.Protobuf.Reflection;

namespace party.model;

public enum TargetClassify
{
	[OriginalName("Normal")]
	Normal,
	[OriginalName("SubGold")]
	SubGold,
	[OriginalName("AddGold")]
	AddGold,
	[OriginalName("SubHp")]
	SubHp,
	[OriginalName("AddHp")]
	AddHp
}

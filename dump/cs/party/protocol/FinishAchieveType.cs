using Google.Protobuf.Reflection;

namespace party.protocol;

public enum FinishAchieveType
{
	[OriginalName("None")]
	None,
	[OriginalName("KillCount")]
	KillCount,
	[OriginalName("TotalDamage")]
	TotalDamage,
	[OriginalName("PkDamageMax")]
	PkDamageMax,
	[OriginalName("TotalDie")]
	TotalDie,
	[OriginalName("TotalInjured")]
	TotalInjured,
	[OriginalName("TotalTrap")]
	TotalTrap
}

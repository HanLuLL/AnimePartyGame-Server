using Google.Protobuf.Reflection;

namespace party.protocol;

public enum AuthType
{
	[OriginalName("Dev")]
	Dev,
	[OriginalName("Steam")]
	Steam,
	[OriginalName("TapTap")]
	TapTap,
	[OriginalName("Abroad")]
	Abroad,
	[OriginalName("China")]
	China
}

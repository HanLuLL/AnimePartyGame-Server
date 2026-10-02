using Google.Protobuf.Reflection;

namespace party.model;

public enum BuffFrom
{
	[OriginalName("none")]
	None,
	[OriginalName("self")]
	Self,
	[OriginalName("land")]
	Land,
	[OriginalName("room")]
	Room
}

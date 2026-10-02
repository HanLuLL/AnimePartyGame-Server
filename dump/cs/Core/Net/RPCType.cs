namespace Core.Net;

public enum RPCType
{
	none = 0,
	FromHost = 1,
	FromClient = 2,
	FromeClientCallBack = 4,
	FromHostCall = 8,
	Proto = 0x10
}

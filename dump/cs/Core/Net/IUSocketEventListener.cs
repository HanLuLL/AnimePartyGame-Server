namespace Core.Net;

public interface IUSocketEventListener
{
	void USocketEvent_OnMessage(USocketGroup group, Frame frame);

	void USocketEvent_OnClose(USocketGroup group, bool serverClose);

	void USocketEvent_OnIdle(USocketGroup group);

	void USocketEvent_OnConnect(USocketGroup group, bool success);

	void USocketEvent_OnTimeOut(USocketGroup group);

	void USocketEvent_OnError(USocketGroup group, string errMsg);
}

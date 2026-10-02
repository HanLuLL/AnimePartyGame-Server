using UnityEngine;

namespace Core.Net;

public class USocketGroup
{
	public IUSocketEventListener EventListener;

	public USocket[] Sockets;

	public int ThreadSleepTime = 50;

	public void AddListener(IUSocketEventListener l)
	{
		EventListener = l;
	}

	public void RemoveListener(IUSocketEventListener l)
	{
		EventListener = null;
	}

	public bool Connect(string ip, int port, bool isAsyc = true, int timeout = -1)
	{
		Sockets = new USocket[1];
		USocket uSocket = new USocket
		{
			Group = this
		};
		Sockets[0] = uSocket;
		Debug.Log($"Connecting：{ip},{port}");
		return uSocket.Connect(ip, port, isAsyc, timeout);
	}

	public void AllClose()
	{
		Debug.Log("Close Socket");
		for (int i = 0; i < Sockets.Length; i++)
		{
			Sockets[i]?.Close();
		}
	}

	public void Send(Frame f)
	{
		Sockets[0].Send(f);
	}

	public bool IsAllSocketConnected()
	{
		if (Sockets == null)
		{
			return false;
		}
		for (int i = 0; i < Sockets.Length; i++)
		{
			if (Sockets[i] == null || Sockets[i].status != USocket.eSTATUS.STATUS_CONNECTED)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsAllSocketClosed()
	{
		if (Sockets == null)
		{
			return true;
		}
		for (int i = 0; i < Sockets.Length; i++)
		{
			if (Sockets[i] != null && Sockets[i].status != USocket.eSTATUS.STATUS_CLOSED)
			{
				return false;
			}
		}
		return true;
	}

	public void USocket_OnMessage(USocket socket, Frame f)
	{
		EventListener?.USocketEvent_OnMessage(this, f);
	}

	public void USocket_OnClose(USocket socket, bool b)
	{
		EventListener?.USocketEvent_OnClose(this, b);
	}

	public void USocket_OnIdle(USocket socket)
	{
		EventListener?.USocketEvent_OnIdle(this);
	}

	public void USocket_OnConnect(USocket socket, bool b)
	{
		for (int i = 0; i < Sockets.Length; i++)
		{
			USocket uSocket = Sockets[i];
			if (uSocket == null || uSocket.status == USocket.eSTATUS.STATUS_INIT || uSocket.status == USocket.eSTATUS.STATUS_CONNECTING)
			{
				return;
			}
		}
		for (int j = 0; j < Sockets.Length; j++)
		{
			if (Sockets[j].status == USocket.eSTATUS.STATUS_CLOSED)
			{
				EventListener?.USocketEvent_OnConnect(this, success: false);
				return;
			}
		}
		EventListener?.USocketEvent_OnConnect(this, b);
	}

	public void USocket_OnTimeOut(USocket socket)
	{
		EventListener?.USocketEvent_OnTimeOut(this);
	}

	public void USocket_OnError(USocket socket, string s)
	{
		EventListener?.USocketEvent_OnError(this, s);
	}
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Tools;
using UnityEngine;

namespace Core.Net;

public class USocket
{
	public enum eSTATUS
	{
		STATUS_INIT,
		STATUS_CONNECTING,
		STATUS_CONNECTED,
		STATUS_CLOSED
	}

	public USocketGroup Group;

	private Socket clientSocket;

	private readonly bool asycRead = true;

	private readonly bool asycSend = true;

	private readonly ConcurrentQueue<Frame> _SendBuff = new ConcurrentQueue<Frame>();

	private bool serverClose = true;

	public int TimeOut = 100;

	public int ConnectTimeout = 5000;

	private readonly ByteBuf tempbuffer;

	private Frame receiveFrame;

	public string IP { get; private set; }

	public int PORT { get; private set; }

	public string LocalIP { get; private set; }

	public int LocalPORT { get; private set; }

	public eSTATUS status { get; private set; }

	public USocket()
	{
		tempbuffer = new ByteBuf(3276800);
	}

	public USocket(Protocal protocal)
	{
		tempbuffer = new ByteBuf(20480);
	}

	public bool Connect(string ip, int port, bool isAsyc = true, int timeout = -1)
	{
		status = eSTATUS.STATUS_CONNECTING;
		IP = ip;
		PORT = port;
		if (!isAsyc)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(ip);
			if (hostAddresses.Length != 0)
			{
				GenClientSocket(hostAddresses[0].AddressFamily == AddressFamily.InterNetworkV6);
				clientSocket.Connect(IP, PORT);
				connected(null);
				return clientSocket.Connected;
			}
			return false;
		}
		Dns.BeginGetHostAddresses(ip, StartConnectAsyc, this);
		return true;
	}

	private void GenClientSocket(bool isIPV6)
	{
		clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		clientSocket.NoDelay = true;
		LingerOption lingerState = new LingerOption(enable: false, 0);
		clientSocket.LingerState = lingerState;
	}

	private void StartConnectAsyc(IAsyncResult asyncConnect)
	{
		if (asyncConnect != null)
		{
			try
			{
				IPAddress[] array = Dns.EndGetHostAddresses(asyncConnect);
				if (array.Length != 0)
				{
					GenClientSocket(array[0].AddressFamily == AddressFamily.InterNetworkV6);
				}
				Thread thread = new Thread(ConnectingThread);
				thread.IsBackground = true;
				thread.Start();
				return;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				connected(null);
				return;
			}
		}
		connected(null);
	}

	private void ConnectingThread()
	{
		IAsyncResult asyncResult = clientSocket.BeginConnect(IP, PORT, connected, this);
		asyncResult.AsyncWaitHandle.WaitOne(ConnectTimeout, exitContext: true);
		if (!asyncResult.IsCompleted)
		{
			clientSocket.Close();
			return;
		}
		Socket socket = clientSocket;
		if (socket != null && socket.Connected)
		{
			try
			{
				status = eSTATUS.STATUS_CONNECTED;
				clientSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, optionValue: true);
				if (clientSocket.LocalEndPoint is IPEndPoint iPEndPoint)
				{
					LocalIP = iPEndPoint.Address.ToString();
					LocalPORT = iPEndPoint.Port;
				}
				Group.USocket_OnConnect(this, b: true);
				if (asycRead)
				{
					StartAysncReceive();
				}
				if (!asycRead || !asycSend)
				{
					Thread thread = new Thread(socketRun);
					thread.IsBackground = true;
					thread.Start();
				}
				return;
			}
			catch (Exception ex)
			{
				ConnectedFailed();
				Debug.LogError(ex.Message);
				return;
			}
		}
		ConnectedFailed();
	}

	public void Close(bool serverClose = false)
	{
		try
		{
			Socket socket = clientSocket;
			if (socket != null && socket.Connected)
			{
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
				clientSocket.Dispose();
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		this.serverClose = serverClose;
		status = eSTATUS.STATUS_CLOSED;
		Group.USocket_OnClose(this, serverClose);
		Debug.Log("Close:" + IP);
	}

	private void connected(IAsyncResult asyncConnect)
	{
		Socket socket = clientSocket;
		if (socket != null && socket.Connected)
		{
			try
			{
				if (asyncConnect != null)
				{
					clientSocket.EndConnect(asyncConnect);
				}
				return;
			}
			catch (Exception ex)
			{
				status = eSTATUS.STATUS_CLOSED;
				Group.USocket_OnConnect(this, b: false);
				Debug.LogError(ex.Message);
				return;
			}
		}
		ConnectedFailed();
	}

	private void ConnectedFailed()
	{
		status = eSTATUS.STATUS_CLOSED;
		Group.USocket_OnConnect(this, b: false);
	}

	public void Send(Frame frame)
	{
		if (asycSend)
		{
			ISend(frame);
		}
		else
		{
			_SendBuff.Enqueue(frame);
		}
	}

	private void socketRun()
	{
		while (status == eSTATUS.STATUS_CONNECTED)
		{
			Socket socket;
			if (!asycRead)
			{
				socket = clientSocket;
				if (socket != null && socket.Connected && clientSocket.Poll(TimeOut, SelectMode.SelectRead))
				{
					try
					{
						ByteBuf byteBuf = new ByteBuf(35);
						if (clientSocket.Receive(byteBuf.GetRaw(), 35, SocketFlags.None) > 0)
						{
							int num = byteBuf.GetInt(0);
							ByteBuf byteBuf2 = new ByteBuf(num);
							clientSocket.Receive(byteBuf2.GetRaw(), num, SocketFlags.None);
							Frame frame = Frame.AnalyzeInfoFromBuf(byteBuf, byteBuf2);
							if (frame == null)
							{
								break;
							}
							Group.USocket_OnMessage(this, frame);
						}
						else
						{
							Debug.Log("长时间未操作，连接关闭");
							Close(serverClose: true);
						}
					}
					catch (Exception ex)
					{
						Group.USocket_OnError(this, ex.StackTrace + ex.Message);
						Close(serverClose: true);
					}
				}
			}
			if (!asycSend)
			{
				socket = clientSocket;
				if (socket != null && socket.Connected && clientSocket.Poll(TimeOut, SelectMode.SelectWrite))
				{
					try
					{
						Monitor.Enter(_SendBuff);
						while (_SendBuff.Count > 0 && status == eSTATUS.STATUS_CONNECTED)
						{
							Frame frame2 = _SendBuff.Dequeue();
							ByteBuf data = frame2.GetData();
							data.MarkReaderIndex();
							data.MarkWriterIndex();
							byte[] raw = data.GetRaw();
							clientSocket.Send(raw, 0, frame2.FrameLen, SocketFlags.None);
							if (frame2.IsSendAutoConsumed)
							{
								frame2.Consumed();
							}
						}
						Monitor.Exit(_SendBuff);
					}
					catch (Exception ex2)
					{
						Group.USocket_OnError(this, ex2.StackTrace + ex2.Message);
						Close(serverClose: true);
					}
				}
			}
			socket = clientSocket;
			if (socket != null && socket.Connected && clientSocket.Poll(TimeOut, SelectMode.SelectError))
			{
				Group.USocket_OnError(this, "TIME OUT");
				Close(serverClose: true);
			}
			if (Group.ThreadSleepTime != 0)
			{
				Thread.Sleep(Group.ThreadSleepTime);
			}
		}
	}

	public IAsyncResult ISend(Frame frame)
	{
		ByteBuf data = frame.GetData();
		data.MarkReaderIndex();
		data.MarkWriterIndex();
		try
		{
			byte[] raw = data.GetRaw();
			return clientSocket.BeginSend(raw, data.ReaderIndex(), data.ReadableBytes(), SocketFlags.None, sended, data);
		}
		catch (Exception ex)
		{
			Group.USocket_OnError(this, ex.StackTrace + ex.Message);
			if (!clientSocket.Connected)
			{
				Close();
			}
			return null;
		}
	}

	private void sended(IAsyncResult ar)
	{
		ByteBuf obj = (ByteBuf)ar.AsyncState;
		obj.ReaderIndex(obj.WriterIndex());
		clientSocket.EndSend(ar);
	}

	private void StartAysncReceive()
	{
		try
		{
			clientSocket.BeginReceive(tempbuffer.GetRaw(), tempbuffer.WriterIndex(), tempbuffer.Capacity(), SocketFlags.None, OnReceive, clientSocket);
		}
		catch (Exception ex)
		{
			if (!clientSocket.Connected)
			{
				Close(serverClose: true);
				if (MonoSingletonProvider<NetManager>.inst.closeType != CloseType.ServerKick)
				{
					Group.USocket_OnError(this, ex.StackTrace + ex.Message);
				}
			}
		}
	}

	private void OnReceive(IAsyncResult ar)
	{
		try
		{
			int num = ((Socket)ar.AsyncState).EndReceive(ar);
			if (num == 0)
			{
				Close(serverClose: true);
				Debug.LogWarning("Remote server closed connection");
			}
			else if (clientSocket != null && clientSocket.Connected)
			{
				tempbuffer.WriterIndex(tempbuffer.WriterIndex() + num);
				DealBuffer();
				clientSocket.BeginReceive(tempbuffer.GetRaw(), tempbuffer.WriterIndex(), tempbuffer.Capacity() - tempbuffer.WriterIndex(), SocketFlags.None, OnReceive, clientSocket);
			}
		}
		catch (Exception ex)
		{
			Close(serverClose: true);
			if (MonoSingletonProvider<NetManager>.inst.closeType != CloseType.ServerKick)
			{
				Group.USocket_OnError(this, ex.StackTrace + ex.Message);
			}
		}
	}

	private void DealBuffer()
	{
		int num = tempbuffer.WriterIndex();
		if (num < 4)
		{
			return;
		}
		int num2 = tempbuffer.GetInt(0);
		if (num >= num2 + 35)
		{
			ByteBuf byteBuf = new ByteBuf(num2 + 35);
			byteBuf.WriteBytes(tempbuffer, num2 + 35);
			int num3 = num - byteBuf.Capacity();
			Array.Copy(tempbuffer.GetRaw(), byteBuf.Capacity(), tempbuffer.GetRaw(), 0, num3);
			tempbuffer.WriterIndex(num3);
			receiveFrame = Frame.AnalyzeInfoFromBuf(byteBuf);
			if (receiveFrame != null)
			{
				Group.USocket_OnMessage(this, receiveFrame);
				DealBuffer();
			}
			else
			{
				Debug.LogError("生成的Frame为空，显然存在错误");
			}
		}
		if (tempbuffer.Capacity() < num2 + 35)
		{
			Debug.LogError($"接受缓冲区：{tempbuffer.Capacity()} 容量不够满足有效字节数据{num2 + 35}， 进行双倍扩容");
			tempbuffer.Capacity(tempbuffer.Capacity() * 2);
		}
	}
}

using System.Text;
using Google.Protobuf;
using UnityEngine;

namespace Core.Net;

public class Frame
{
	public const int HeadLength = 35;

	public int LENGTH;

	public long SESSIONID;

	public int CMDID;

	public int VER1;

	public int VER2;

	public int VER3;

	public long UPSN;

	public long DOWNSN;

	public int ERR;

	protected ByteBuf msgBody;

	protected bool end;

	public int DealTimeTick;

	public bool IsConsumed;

	public bool IsSendAutoConsumed;

	public int FrameLen { get; private set; }

	protected Frame()
	{
		DealTimeTick = NetHelper.GetEnvironmentTickCount();
		IsConsumed = false;
		end = false;
		CMDID = 0;
		UPSN = 0L;
	}

	public Frame(int len)
	{
		DealTimeTick = NetHelper.GetEnvironmentTickCount();
		len = ((len >= 0) ? len : 0);
		msgBody = new ByteBuf(len + 35);
		FrameLen = len + 35;
		msgBody.WriterIndex(0);
		IsConsumed = false;
		end = false;
		CMDID = 0;
		UPSN = 0L;
	}

	public Frame PutBool(bool b)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteByte((byte)(b ? 1 : 0));
		}
		return this;
	}

	public Frame PutByte(byte c)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteByte(c);
		}
		return this;
	}

	public Frame PutBytes(ByteBuf src)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteBytes(src);
		}
		return this;
	}

	public Frame PutBytes(byte[] src)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteBytes(src);
		}
		return this;
	}

	public Frame PutBytes(byte[] src, int readIndex, short len)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteBytes(src, readIndex, len);
		}
		return this;
	}

	public Frame PutFloat(float s)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteFloat(s);
		}
		return this;
	}

	public Frame PutLong(long s)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteLong(s);
		}
		return this;
	}

	public Frame PutInt(int s)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteInt(s);
		}
		return this;
	}

	public Frame PutShort(int s)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteShort((short)s);
		}
		return this;
	}

	public ByteBuf GetData()
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		return msgBody;
	}

	public Frame SetStatusToResend()
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (end)
		{
			msgBody.ResetReaderIndex();
			msgBody.ResetWriterIndex();
			FrameLen = 0;
			end = false;
		}
		return this;
	}

	public ByteBuf GetContent()
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (end)
		{
			return msgBody;
		}
		return null;
	}

	public void Consumed()
	{
		if (msgBody != null)
		{
			msgBody.StoreData();
			msgBody = null;
			IsConsumed = true;
		}
	}

	public bool IsEnd()
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		return end;
	}

	public Frame PutVector2(Vector2 v2)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteVector2(v2);
		}
		return this;
	}

	public Frame PutVector3(Vector3 v3)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteVector3(v3);
		}
		return this;
	}

	public Frame PutVector4(Vector4 v4)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteVector4(v4);
		}
		return this;
	}

	public Frame PutQuaternion(Quaternion quation)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteQuaternion(quation);
		}
		return this;
	}

	public Frame PutUTF8(string s)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteUTF8(s);
		}
		return this;
	}

	public Frame PutString(string s, byte[] ks)
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s.ToCharArray());
			msgBody.WriteShort((short)bytes.Length);
			xor(bytes, ks);
			msgBody.WriteBytes(bytes);
		}
		return this;
	}

	public Frame PutObject<T>(T obj) where T : IMessage<T>
	{
		if (IsConsumed)
		{
			Debug.LogError("包内容已经被消耗");
		}
		if (!end)
		{
			msgBody.WriteObject(obj);
		}
		return this;
	}

	public static Frame AnalyzeInfoFromBuf(ByteBuf _head, ByteBuf _payload)
	{
		_head.ReaderIndex(0);
		_head.WriterIndex(35);
		Frame result = new Frame
		{
			LENGTH = _head.ReadInt(),
			SESSIONID = _head.ReadLong(),
			CMDID = _head.ReadShort(),
			VER1 = _head.ReadByte(),
			VER2 = _head.ReadByte(),
			VER3 = _head.ReadByte(),
			UPSN = _head.ReadLong(),
			DOWNSN = _head.ReadLong(),
			ERR = _head.ReadShort(),
			msgBody = _payload,
			end = true
		};
		_head = null;
		return result;
	}

	public static Frame AnalyzeInfoFromBuf(ByteBuf src)
	{
		return new Frame
		{
			LENGTH = src.ReadInt(),
			SESSIONID = src.ReadLong(),
			CMDID = src.ReadShort(),
			VER1 = src.ReadByte(),
			VER2 = src.ReadByte(),
			VER3 = src.ReadByte(),
			UPSN = src.ReadLong(),
			DOWNSN = src.ReadLong(),
			ERR = src.ReadShort(),
			msgBody = src,
			end = true
		};
	}

	public static void xor(byte[] bs, byte[] ks)
	{
		if (ks != null && ks.Length > 0)
		{
			for (int i = 0; i < bs.Length; i++)
			{
				bs[i] ^= ks[i % ks.Length];
			}
		}
	}
}

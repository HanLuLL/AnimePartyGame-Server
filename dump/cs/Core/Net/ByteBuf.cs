using System;
using System.IO;
using System.Text;
using Google.Protobuf;
using ICSharpCode.SharpZipLib.Checksums;
using Tools;
using UnityEngine;

namespace Core.Net;

public class ByteBuf
{
	private int byteLen;

	private byte[] data;

	private int writerIndex;

	private int markReader;

	private int markWriter;

	private static readonly Crc32 crc32 = new Crc32();

	private int readerIndex { get; set; }

	public bool IsReadable { get; private set; }

	public bool IsWriteable { get; private set; }

	public ByteBuf(int capacity, bool usePool = false)
	{
		byteLen = capacity;
		data = SimpleSingletonProvider<ByteArrayPool>.inst.GetItem(byteLen);
		readerIndex = 0;
		writerIndex = 0;
		markReader = 0;
		markWriter = 0;
		IsReadable = true;
		IsWriteable = true;
	}

	public ByteBuf(byte[] buf)
	{
		if (buf == null)
		{
			buf = new byte[100];
		}
		byteLen = buf.Length;
		data = buf;
		readerIndex = 0;
		writerIndex = 0;
		markReader = 0;
		markWriter = 0;
		IsReadable = true;
		IsWriteable = true;
	}

	public int Capacity()
	{
		return byteLen;
	}

	public ByteBuf Capacity(int nc)
	{
		if (nc > byteLen)
		{
			byte[] sourceArray = data;
			data = SimpleSingletonProvider<ByteArrayPool>.inst.GetItem(nc);
			Array.Copy(sourceArray, data, byteLen);
			byteLen = nc;
		}
		return this;
	}

	public byte[] GetRaw()
	{
		return data;
	}

	public ByteBuf Clear()
	{
		readerIndex = 0;
		writerIndex = 0;
		markReader = 0;
		markWriter = 0;
		return this;
	}

	public void StoreData()
	{
		SimpleSingletonProvider<ByteArrayPool>.inst.StoreItem(data);
	}

	public ByteBuf Copy()
	{
		ByteBuf byteBuf = new ByteBuf(byteLen);
		Array.Copy(data, byteBuf.data, byteLen);
		byteBuf.readerIndex = readerIndex;
		byteBuf.writerIndex = writerIndex;
		byteBuf.markReader = markReader;
		byteBuf.markWriter = markWriter;
		return byteBuf;
	}

	private byte[] Flip(byte[] bytes)
	{
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	private byte[] Flip(byte[] bytes, int index, int len)
	{
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(bytes, index, len);
		}
		return bytes;
	}

	public byte[] GetAllBytesCopy()
	{
		byte[] array = new byte[writerIndex];
		Array.Copy(data, array, writerIndex);
		return array;
	}

	public ByteBuf SetByte(int index, byte value)
	{
		if (index < byteLen)
		{
			data[index] = value;
		}
		return this;
	}

	public ByteBuf SetBytes(int index, byte[] src, int from, int len)
	{
		if (index + len <= len)
		{
			Array.Copy(src, from, data, index, len);
		}
		return this;
	}

	public ByteBuf SetIndex(int readerIndex, int writerIndex)
	{
		if (readerIndex >= 0 && readerIndex <= writerIndex && writerIndex <= byteLen)
		{
			this.readerIndex = readerIndex;
			this.writerIndex = writerIndex;
		}
		return this;
	}

	public ByteBuf SetInt(int index, int value)
	{
		if (index + 4 <= byteLen)
		{
			data[index++] = (byte)((value >> 24) & 0xFF);
			data[index++] = (byte)((value >> 16) & 0xFF);
			data[index++] = (byte)((value >> 8) & 0xFF);
			data[index++] = (byte)(value & 0xFF);
		}
		return this;
	}

	public ByteBuf SetLong(int index, long value)
	{
		if (index + 8 <= byteLen)
		{
			data[index++] = (byte)((value >> 56) & 0xFF);
			data[index++] = (byte)((value >> 48) & 0xFF);
			data[index++] = (byte)((value >> 40) & 0xFF);
			data[index++] = (byte)((value >> 32) & 0xFF);
			data[index++] = (byte)((value >> 24) & 0xFF);
			data[index++] = (byte)((value >> 16) & 0xFF);
			data[index++] = (byte)((value >> 8) & 0xFF);
			data[index++] = (byte)(value & 0xFF);
		}
		return this;
	}

	public ByteBuf SetShort(int index, short value)
	{
		if (index + 2 <= byteLen)
		{
			data[index++] = (byte)((value >> 8) & 0xFF);
			data[index++] = (byte)(value & 0xFF);
		}
		return this;
	}

	public ByteBuf SkipBytes(int length)
	{
		if (readerIndex + length <= writerIndex)
		{
			readerIndex += length;
		}
		return this;
	}

	public int WritableBytes()
	{
		return byteLen - writerIndex;
	}

	public ByteBuf WriteByte(byte value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		Capacity(writerIndex + 1);
		data[writerIndex++] = value;
		return this;
	}

	public ByteBuf WriteBool(bool b)
	{
		if (!IsWriteable)
		{
			return this;
		}
		Capacity(writerIndex + 1);
		data[writerIndex++] = (byte)(b ? 1 : 0);
		return this;
	}

	public ByteBuf WriteInt(int value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		Capacity(writerIndex + 4);
		data[writerIndex++] = (byte)((value >> 24) & 0xFF);
		data[writerIndex++] = (byte)((value >> 16) & 0xFF);
		data[writerIndex++] = (byte)((value >> 8) & 0xFF);
		data[writerIndex++] = (byte)(value & 0xFF);
		return this;
	}

	public ByteBuf WriteLong(long value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		Capacity(writerIndex + 8);
		data[writerIndex++] = (byte)((value >> 56) & 0xFF);
		data[writerIndex++] = (byte)((value >> 48) & 0xFF);
		data[writerIndex++] = (byte)((value >> 40) & 0xFF);
		data[writerIndex++] = (byte)((value >> 32) & 0xFF);
		data[writerIndex++] = (byte)((value >> 24) & 0xFF);
		data[writerIndex++] = (byte)((value >> 16) & 0xFF);
		data[writerIndex++] = (byte)((value >> 8) & 0xFF);
		data[writerIndex++] = (byte)(value & 0xFF);
		return this;
	}

	public ByteBuf WriteShort(short value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		Capacity(writerIndex + 2);
		data[writerIndex++] = (byte)((value >> 8) & 0xFF);
		data[writerIndex++] = (byte)(value & 0xFF);
		return this;
	}

	public ByteBuf WriteFloat(float value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		int value2 = SingleToInt32Bits(value);
		WriteInt(value2);
		return this;
	}

	public ByteBuf WriteVector2(Vector2 v2)
	{
		for (int i = 0; i < 2; i++)
		{
			WriteFloat(v2[i]);
		}
		return this;
	}

	public ByteBuf WriteVector3(Vector3 v3)
	{
		for (int i = 0; i < 3; i++)
		{
			WriteFloat(v3[i]);
		}
		return this;
	}

	public ByteBuf WriteVector4(Vector4 v4)
	{
		for (int i = 0; i < 4; i++)
		{
			WriteFloat(v4[i]);
		}
		return this;
	}

	public ByteBuf WriteQuaternion(Quaternion q4)
	{
		for (int i = 0; i < 4; i++)
		{
			WriteFloat(q4[i]);
		}
		return this;
	}

	public ByteBuf WriteBytes(ByteBuf src)
	{
		if (!IsWriteable)
		{
			return this;
		}
		short num = (short)(src.writerIndex - src.readerIndex);
		if (num >= 0)
		{
			Capacity(writerIndex + num + 2);
			WriteShort(num);
			if (num > 0)
			{
				Array.Copy(src.data, src.readerIndex, data, writerIndex, num);
				writerIndex += num;
				src.readerIndex += num;
			}
		}
		return this;
	}

	public ByteBuf WriteBytes(ByteBuf src, int len)
	{
		if (!IsWriteable)
		{
			return this;
		}
		if (len > 0)
		{
			Array.Copy(src.data, 0, data, writerIndex, len);
			writerIndex += len;
		}
		return this;
	}

	public ByteBuf WriteBytes(byte[] src)
	{
		if (!IsWriteable)
		{
			return this;
		}
		if (src == null)
		{
			WriteShort(0);
			return this;
		}
		short num = (short)src.Length;
		WriteShort(num);
		Capacity(writerIndex + num);
		if (num > 0)
		{
			Array.Copy(src, 0, data, writerIndex, num);
			writerIndex += num;
		}
		return this;
	}

	public ByteBuf WriteBytes(byte[] src, int off, short len)
	{
		if (!IsWriteable)
		{
			return this;
		}
		if (len > 0)
		{
			Capacity(writerIndex + len);
			Array.Copy(src, off, data, writerIndex, len);
			writerIndex += len;
		}
		return this;
	}

	public bool ReadBool()
	{
		return ReadByte() != 0;
	}

	public byte ReadByte()
	{
		if (IsReadable && readerIndex < writerIndex)
		{
			return data[readerIndex++];
		}
		return 0;
	}

	public byte[] ReadBytes(int len)
	{
		if (IsReadable && len > 0 && readerIndex + len - 1 < writerIndex)
		{
			byte[] array = new byte[len];
			for (int i = 0; i < len; i++)
			{
				array[i] = ReadByte();
			}
			return array;
		}
		return Array.Empty<byte>();
	}

	public byte[] ReadBytes()
	{
		short len = ReadShort();
		return ReadBytes(len);
	}

	public ByteBuf ReadByteBuf()
	{
		short num = ReadShort();
		if (num > 0 && readerIndex + num - 1 < writerIndex)
		{
			return new ByteBuf(data)
			{
				readerIndex = readerIndex,
				writerIndex = writerIndex,
				markReader = markReader,
				markWriter = markWriter,
				IsWriteable = false,
				IsReadable = true
			};
		}
		return null;
	}

	public int ReadInt()
	{
		if (IsReadable && readerIndex + 3 < writerIndex)
		{
			return (int)((data[readerIndex++] << 24) & 0xFF000000u) | ((data[readerIndex++] << 16) & 0xFF0000) | ((data[readerIndex++] << 8) & 0xFF00) | (data[readerIndex++] & 0xFF);
		}
		return 0;
	}

	public long ReadLong()
	{
		if (IsReadable && readerIndex + 7 < writerIndex)
		{
			return (long)((((ulong)data[readerIndex++] << 56) & 0xFF00000000000000uL) | (((ulong)data[readerIndex++] << 48) & 0xFF000000000000L) | (((ulong)data[readerIndex++] << 40) & 0xFF0000000000L) | (((ulong)data[readerIndex++] << 32) & 0xFF00000000L) | (((ulong)data[readerIndex++] << 24) & 0xFF000000u) | (((ulong)data[readerIndex++] << 16) & 0xFF0000) | (((ulong)data[readerIndex++] << 8) & 0xFF00) | ((ulong)data[readerIndex++] & 0xFFuL));
		}
		return 0L;
	}

	public short ReadShort()
	{
		if (IsReadable && readerIndex + 1 < writerIndex)
		{
			byte num = data[readerIndex++];
			int num2 = data[readerIndex++] & 0xFF;
			return (short)(((num << 8) & 0xFF00) | num2);
		}
		return 0;
	}

	public unsafe static int SingleToInt32Bits(float value)
	{
		return *(int*)(&value);
	}

	public unsafe static float Int32BitsToSingle(int value)
	{
		return *(float*)(&value);
	}

	public float ReadFloat()
	{
		if (IsReadable && readerIndex + 3 < writerIndex)
		{
			return Int32BitsToSingle(ReadInt());
		}
		return 0f;
	}

	public Vector2 ReadVector2()
	{
		Vector2 result = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			result[i] = ReadFloat();
		}
		return result;
	}

	public Vector3 ReadVector3()
	{
		Vector3 result = default(Vector3);
		for (int i = 0; i < 3; i++)
		{
			result[i] = ReadFloat();
		}
		return result;
	}

	public Vector4 ReadVector4()
	{
		Vector4 result = default(Vector4);
		for (int i = 0; i < 4; i++)
		{
			result[i] = ReadFloat();
		}
		return result;
	}

	public Quaternion ReadQuaternion()
	{
		Quaternion result = default(Quaternion);
		for (int i = 0; i < 4; i++)
		{
			result[i] = ReadFloat();
		}
		return result;
	}

	public int ReadableBytes()
	{
		return writerIndex - readerIndex;
	}

	public byte GetByte(int index)
	{
		if (index >= byteLen)
		{
			return 0;
		}
		return data[index];
	}

	public int GetInt(int index)
	{
		if (index + 3 < byteLen)
		{
			return (data[index] << 24) | (data[index + 1] << 16) | (data[index + 2] << 8) | data[index + 3];
		}
		return 0;
	}

	public short GetShort(int index)
	{
		if (index + 1 < byteLen)
		{
			short num = (short)(data[index] << 8);
			short num2 = data[index + 1];
			return (short)(num | num2);
		}
		return 0;
	}

	public ByteBuf WriteObject<T>(T obj) where T : IMessage<T>
	{
		if (!IsWriteable)
		{
			return this;
		}
		using MemoryStream memoryStream = ((obj == null) ? new MemoryStream() : new MemoryStream(obj.ToByteArray()));
		int num = (int)memoryStream.Length;
		Capacity(writerIndex + num);
		Array.Copy(memoryStream.ToArray(), 0, data, writerIndex, num);
		writerIndex += num;
		return this;
	}

	public T ReadObject<T>() where T : IMessage<T>, new()
	{
		_ = IsReadable;
		using MemoryStream input = new MemoryStream(data, readerIndex, byteLen - readerIndex, writable: false);
		MessageParser<T> messageParser = new MessageParser<T>(() => new T());
		readerIndex = byteLen;
		return messageParser.ParseFrom(input);
	}

	public static T ReadObject<T>(byte[] _params) where T : IMessage<T>, new()
	{
		using MemoryStream input = new MemoryStream(_params, 0, _params.Length, writable: false);
		return new MessageParser<T>(() => new T()).ParseFrom(input);
	}

	public string ReadUTF8()
	{
		_ = IsReadable;
		short num = ReadShort();
		byte[] array = new byte[num];
		Array.Copy(data, readerIndex, array, 0, num);
		readerIndex += num;
		return Encoding.UTF8.GetString(array);
	}

	public ByteBuf WriteUTF8(string value)
	{
		if (!IsWriteable)
		{
			return this;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(value.ToCharArray());
		int num = bytes.Length;
		Capacity(writerIndex + num + 2);
		WriteShort((short)num);
		Array.Copy(bytes, 0, data, writerIndex, num);
		writerIndex += num;
		return this;
	}

	public int WriterIndex()
	{
		return writerIndex;
	}

	public ByteBuf WriterIndex(int writerIndex)
	{
		if (writerIndex >= readerIndex && writerIndex <= byteLen)
		{
			this.writerIndex = writerIndex;
		}
		return this;
	}

	public int ReaderIndex()
	{
		return readerIndex;
	}

	public ByteBuf ReaderIndex(int readerIndex)
	{
		if (readerIndex <= writerIndex)
		{
			this.readerIndex = readerIndex;
		}
		return this;
	}

	public ByteBuf ResetReaderIndex()
	{
		if (markReader <= writerIndex)
		{
			readerIndex = markReader;
		}
		return this;
	}

	public ByteBuf ResetWriterIndex()
	{
		if (markWriter >= readerIndex)
		{
			writerIndex = markWriter;
		}
		return this;
	}

	public int MarkReaderIndex()
	{
		markReader = readerIndex;
		return readerIndex;
	}

	public int MarkWriterIndex()
	{
		markWriter = writerIndex;
		return writerIndex;
	}

	public int MaxWritableBytes()
	{
		return byteLen - writerIndex;
	}

	public bool HasInts(int num)
	{
		return readerIndex + 4 * num - 1 < writerIndex;
	}

	public bool HasBytes(int num)
	{
		return readerIndex + num - 1 < writerIndex;
	}

	public int CalculateCheckSum(int startIndex, int len)
	{
		if (data == null || data.Length == 0)
		{
			return 0;
		}
		crc32.Reset();
		crc32.Update(data, startIndex, len);
		return (int)crc32.Value;
	}

	public void Xor(int startIndex, int endIndex)
	{
		if (data != null && endIndex <= data.Length && startIndex >= 0 && startIndex < endIndex)
		{
			int num = 0;
			int num2 = startIndex;
			while (num2 < endIndex)
			{
				num %= EnOrDecode.keyTable.Length;
				byte b = EnOrDecode.keyTable[num];
				data[num2] ^= b;
				num2++;
				num++;
			}
		}
	}
}

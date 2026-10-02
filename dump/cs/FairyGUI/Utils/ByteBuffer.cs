using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FairyGUI.Utils;

public class ByteBuffer
{
	public bool littleEndian;

	public string[] stringTable;

	public int version;

	private int _pointer;

	private int _offset;

	private int _length;

	private byte[] _data;

	private static byte[] temp = new byte[8];

	private static List<GPathPoint> helperPoints = new List<GPathPoint>();

	public int position
	{
		get
		{
			return _pointer;
		}
		set
		{
			_pointer = value;
		}
	}

	public int length => _length;

	public bool bytesAvailable => _pointer < _length;

	public byte[] buffer
	{
		get
		{
			return _data;
		}
		set
		{
			_data = value;
			_pointer = 0;
			_offset = 0;
			_length = _data.Length;
		}
	}

	public ByteBuffer(byte[] data, int offset = 0, int length = -1)
	{
		_data = data;
		_pointer = 0;
		_offset = offset;
		if (length < 0)
		{
			_length = data.Length - offset;
		}
		else
		{
			_length = length;
		}
		littleEndian = false;
	}

	public int Skip(int count)
	{
		_pointer += count;
		return _pointer;
	}

	public byte ReadByte()
	{
		return _data[_offset + _pointer++];
	}

	public byte[] ReadBytes(byte[] output, int destIndex, int count)
	{
		if (count > _length - _pointer)
		{
			throw new ArgumentOutOfRangeException();
		}
		Array.Copy(_data, _offset + _pointer, output, destIndex, count);
		_pointer += count;
		return output;
	}

	public byte[] ReadBytes(int count)
	{
		if (count > _length - _pointer)
		{
			throw new ArgumentOutOfRangeException();
		}
		byte[] array = new byte[count];
		Array.Copy(_data, _offset + _pointer, array, 0, count);
		_pointer += count;
		return array;
	}

	public ByteBuffer ReadBuffer()
	{
		int num = ReadInt();
		ByteBuffer result = new ByteBuffer(_data, _pointer, num)
		{
			stringTable = stringTable,
			version = version
		};
		_pointer += num;
		return result;
	}

	public char ReadChar()
	{
		return (char)ReadShort();
	}

	public bool ReadBool()
	{
		bool result = _data[_offset + _pointer] == 1;
		_pointer++;
		return result;
	}

	public short ReadShort()
	{
		int num = _offset + _pointer;
		_pointer += 2;
		if (littleEndian)
		{
			return (short)(_data[num] | (_data[num + 1] << 8));
		}
		return (short)((_data[num] << 8) | _data[num + 1]);
	}

	public ushort ReadUshort()
	{
		return (ushort)ReadShort();
	}

	public int ReadInt()
	{
		int num = _offset + _pointer;
		_pointer += 4;
		if (littleEndian)
		{
			return _data[num] | (_data[num + 1] << 8) | (_data[num + 2] << 16) | (_data[num + 3] << 24);
		}
		return (_data[num] << 24) | (_data[num + 1] << 16) | (_data[num + 2] << 8) | _data[num + 3];
	}

	public uint ReadUint()
	{
		return (uint)ReadInt();
	}

	public float ReadFloat()
	{
		int num = _offset + _pointer;
		_pointer += 4;
		if (littleEndian == BitConverter.IsLittleEndian)
		{
			return BitConverter.ToSingle(_data, num);
		}
		temp[3] = _data[num];
		temp[2] = _data[num + 1];
		temp[1] = _data[num + 2];
		temp[0] = _data[num + 3];
		return BitConverter.ToSingle(temp, 0);
	}

	public long ReadLong()
	{
		int num = _offset + _pointer;
		_pointer += 8;
		if (littleEndian)
		{
			int num2 = _data[num] | (_data[num + 1] << 8) | (_data[num + 2] << 16) | (_data[num + 3] << 24);
			int num3 = _data[num + 4] | (_data[num + 5] << 8) | (_data[num + 6] << 16) | (_data[num + 7] << 24);
			return (uint)num2 | ((long)num3 << 32);
		}
		int num4 = (_data[num] << 24) | (_data[num + 1] << 16) | (_data[num + 2] << 8) | _data[num + 3];
		return (uint)((_data[num + 4] << 24) | (_data[num + 5] << 16) | (_data[num + 6] << 8) | _data[num + 7]) | ((long)num4 << 32);
	}

	public double ReadDouble()
	{
		int num = _offset + _pointer;
		_pointer += 8;
		if (littleEndian == BitConverter.IsLittleEndian)
		{
			return BitConverter.ToDouble(_data, num);
		}
		temp[7] = _data[num];
		temp[6] = _data[num + 1];
		temp[5] = _data[num + 2];
		temp[4] = _data[num + 3];
		temp[3] = _data[num + 4];
		temp[2] = _data[num + 5];
		temp[1] = _data[num + 6];
		temp[0] = _data[num + 7];
		return BitConverter.ToSingle(temp, 0);
	}

	public string ReadString()
	{
		ushort num = ReadUshort();
		string result = Encoding.UTF8.GetString(_data, _offset + _pointer, num);
		_pointer += num;
		return result;
	}

	public string ReadString(int len)
	{
		string result = Encoding.UTF8.GetString(_data, _offset + _pointer, len);
		_pointer += len;
		return result;
	}

	public string ReadS()
	{
		int num = ReadUshort();
		return num switch
		{
			65534 => null, 
			65533 => string.Empty, 
			_ => stringTable[num], 
		};
	}

	public string[] ReadSArray(int cnt)
	{
		string[] array = new string[cnt];
		for (int i = 0; i < cnt; i++)
		{
			array[i] = ReadS();
		}
		return array;
	}

	public List<GPathPoint> ReadPath()
	{
		helperPoints.Clear();
		int num = ReadInt();
		if (num == 0)
		{
			return helperPoints;
		}
		for (int i = 0; i < num; i++)
		{
			GPathPoint.CurveType curveType = (GPathPoint.CurveType)ReadByte();
			switch (curveType)
			{
			case GPathPoint.CurveType.Bezier:
				helperPoints.Add(new GPathPoint(new Vector3(ReadFloat(), ReadFloat(), 0f), new Vector3(ReadFloat(), ReadFloat(), 0f)));
				break;
			case GPathPoint.CurveType.CubicBezier:
				helperPoints.Add(new GPathPoint(new Vector3(ReadFloat(), ReadFloat(), 0f), new Vector3(ReadFloat(), ReadFloat(), 0f), new Vector3(ReadFloat(), ReadFloat(), 0f)));
				break;
			default:
				helperPoints.Add(new GPathPoint(new Vector3(ReadFloat(), ReadFloat(), 0f), curveType));
				break;
			}
		}
		return helperPoints;
	}

	public void WriteS(string value)
	{
		int num = ReadUshort();
		if (num != 65534 && num != 65533)
		{
			stringTable[num] = value;
		}
	}

	public Color ReadColor()
	{
		int num = _offset + _pointer;
		byte r = _data[num];
		byte g = _data[num + 1];
		byte b = _data[num + 2];
		byte a = _data[num + 3];
		_pointer += 4;
		return new Color32(r, g, b, a);
	}

	public bool Seek(int indexTablePos, int blockIndex)
	{
		int pointer = _pointer;
		_pointer = indexTablePos;
		int num = _data[_offset + _pointer++];
		if (blockIndex < num)
		{
			int num2;
			if (_data[_offset + _pointer++] == 1)
			{
				_pointer += 2 * blockIndex;
				num2 = ReadShort();
			}
			else
			{
				_pointer += 4 * blockIndex;
				num2 = ReadInt();
			}
			if (num2 > 0)
			{
				_pointer = indexTablePos + num2;
				return true;
			}
			_pointer = pointer;
			return false;
		}
		_pointer = pointer;
		return false;
	}
}

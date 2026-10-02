namespace FairyGUI.Utils;

public class ZipReader
{
	public class ZipEntry
	{
		public string name;

		public int compress;

		public uint crc;

		public int size;

		public int sourceSize;

		public int offset;

		public bool isDirectory;
	}

	private ByteBuffer _stream;

	private int _entryCount;

	private int _pos;

	private int _index;

	public int entryCount => _entryCount;

	public ZipReader(byte[] data)
	{
		_stream = new ByteBuffer(data);
		_stream.littleEndian = true;
		int num = _stream.length - 22;
		_stream.position = num + 10;
		_entryCount = _stream.ReadShort();
		_stream.position = num + 16;
		_pos = _stream.ReadInt();
	}

	public bool GetNextEntry(ZipEntry entry)
	{
		if (_index >= _entryCount)
		{
			return false;
		}
		_stream.position = _pos + 28;
		int num = _stream.ReadUshort();
		int num2 = _stream.ReadUshort() + _stream.ReadUshort();
		_stream.position = _pos + 46;
		string text = _stream.ReadString(num);
		text = (entry.name = text.Replace("\\", "/"));
		if (text[text.Length - 1] == '/')
		{
			entry.isDirectory = true;
			entry.compress = 0;
			entry.crc = 0u;
			entry.size = (entry.sourceSize = 0);
			entry.offset = 0;
		}
		else
		{
			entry.isDirectory = false;
			_stream.position = _pos + 10;
			entry.compress = _stream.ReadUshort();
			_stream.position = _pos + 16;
			entry.crc = _stream.ReadUint();
			entry.size = _stream.ReadInt();
			entry.sourceSize = _stream.ReadInt();
			_stream.position = _pos + 42;
			entry.offset = _stream.ReadInt() + 30 + num;
		}
		_pos += 46 + num + num2;
		_index++;
		return true;
	}

	public byte[] GetEntryData(ZipEntry entry)
	{
		byte[] array = new byte[entry.size];
		if (entry.size > 0)
		{
			_stream.position = entry.offset;
			_stream.ReadBytes(array, 0, entry.size);
		}
		return array;
	}
}

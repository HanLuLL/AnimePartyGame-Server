namespace Core.Net;

public class ByteBufHelper
{
	public static int ReadInt(byte[] bs, int startIndex)
	{
		if (startIndex + 3 < bs.Length)
		{
			return (int)((bs[startIndex] << 24) & 0xFF000000u) | ((bs[startIndex + 1] << 16) & 0xFF0000) | ((bs[startIndex + 2] << 8) & 0xFF00) | (bs[startIndex + 3] & 0xFF);
		}
		return 0;
	}
}

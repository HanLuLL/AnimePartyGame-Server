using FairyGUI.Utils;

namespace FairyGUI;

public class PixelHitTestData
{
	public int pixelWidth;

	public float scale;

	public byte[] pixels;

	public int pixelsLength;

	public int pixelsOffset;

	public void Load(ByteBuffer ba)
	{
		ba.ReadInt();
		pixelWidth = ba.ReadInt();
		scale = 1f / (float)(int)ba.ReadByte();
		pixels = ba.buffer;
		pixelsLength = ba.ReadInt();
		pixelsOffset = ba.position;
		ba.Skip(pixelsLength);
	}
}

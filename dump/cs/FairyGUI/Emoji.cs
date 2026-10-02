namespace FairyGUI;

public class Emoji
{
	public string url;

	public int width;

	public int height;

	public Emoji(string url, int width, int height)
	{
		this.url = url;
		this.width = width;
		this.height = height;
	}

	public Emoji(string url)
	{
		this.url = url;
	}
}

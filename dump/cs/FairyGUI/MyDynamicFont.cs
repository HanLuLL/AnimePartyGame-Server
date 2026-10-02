namespace FairyGUI;

public class MyDynamicFont : DynamicFont
{
	public MyDynamicFont(string fontName)
	{
		name = fontName;
	}

	public void AsyncLoad(string key)
	{
		base.nativeFont = SystemConfig.GetFontAsset(key);
	}

	public override void Dispose()
	{
		base.Dispose();
	}
}

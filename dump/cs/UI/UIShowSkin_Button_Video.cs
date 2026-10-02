using CriWare.CriMana;
using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIShowSkin_Button_Video : GButton
{
	private Player playerVideo;

	public GGraph loader_Video;

	public const string URL = "ui://zfulrgf7qdq52";

	public void AddEvent()
	{
		base.onChanged.Add(ChangeVideoStatus);
	}

	public void Close()
	{
		if (loader_Video != null)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_Video);
		}
		base.onChanged.Remove(ChangeVideoStatus);
	}

	private void ChangeVideoStatus()
	{
		if (base.selected)
		{
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Pause(false);
			}
		}
		else
		{
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Pause(true);
			}
		}
	}

	public async void RefreshVideo(string VideoKey)
	{
		base.touchable = false;
		base.selected = false;
		if (!string.IsNullOrEmpty(VideoKey))
		{
			playerVideo = await SimpleSingletonProvider<CriMovieManager>.inst.Play(VideoKey, loader_Video, delegate(Player criPlayer, int status)
			{
				criPlayer.Pause(true);
			}, delegate(Player criPlayer, int status)
			{
				criPlayer.Start();
				base.selected = false;
			}, null, 5);
			base.touchable = true;
		}
		else
		{
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Stop();
			}
		}
	}

	public static UIShowSkin_Button_Video CreateInstance()
	{
		return (UIShowSkin_Button_Video)UIPackage.CreateObject("ShowSkin", "ShowSkin_Button_Video");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Video = (GGraph)GetChildAt(1);
	}
}

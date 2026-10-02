using CriWare.CriMana;
using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIFashion_Button_Video : GButton
{
	private Player playerVideo;

	public GGraph loader_Video;

	public const string URL = "ui://dl889m5qdx3g4y";

	public void AddEvent()
	{
		base.onChanged.Add(ChangeVideoStatus);
	}

	public void RemoveEvent()
	{
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
			CommonUIManager.TryAddVideoGraph(UIType.Panel, 21, loader_Video);
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Pause(true);
			}
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
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Stop();
			}
		}
	}

	public void CloseVideo()
	{
		playerVideo = null;
	}

	public static UIFashion_Button_Video CreateInstance()
	{
		return (UIFashion_Button_Video)UIPackage.CreateObject("Fashion", "Fashion_Button_Video");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Video = (GGraph)GetChildAt(1);
	}
}

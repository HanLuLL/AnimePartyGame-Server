using CriWare.CriMana;
using FairyGUI;
using FairyGUI.Utils;
using Tools;
using UnityEngine;

namespace UI;

public class UIFashion_Com_video : GComponent
{
	private Player playerVideo;

	public GGraph loader_Video;

	public const string URL = "ui://dl889m5qu6bm50";

	public async void RefreshVideo(string videoKey)
	{
		if (string.IsNullOrEmpty(videoKey))
		{
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Stop();
			}
			return;
		}
		CloseVideo();
		ExpandVideoOutsideVisibleArea();
		CommonUIManager.TryAddVideoGraph(UIType.Panel, 21, loader_Video);
		Player obj2 = playerVideo;
		if (obj2 != null)
		{
			obj2.Stop();
		}
		playerVideo = await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoKey, loader_Video);
	}

	public void CloseVideo()
	{
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_Video);
		playerVideo = null;
	}

	private void ExpandVideoOutsideVisibleArea()
	{
		float num = Mathf.Max(1f, base.width);
		float num2 = Mathf.Max(1f, base.height);
		float num3 = num * 2304f / 1920f;
		float num4 = num2 * 1200f / 1080f;
		loader_Video.SetSize(num3, num4);
		float num5 = (num - num3) * 0.5f;
		float num6 = (num2 - num4) * 0.5f;
		loader_Video.xy = new Vector2(num5, num6);
	}

	public static UIFashion_Com_video CreateInstance()
	{
		return (UIFashion_Com_video)UIPackage.CreateObject("Fashion", "Fashion_Com_video");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Video = (GGraph)GetChildAt(1);
	}
}

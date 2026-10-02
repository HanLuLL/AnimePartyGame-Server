using System.Collections.Generic;
using System.Reflection;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace Core;

public class GuideInfo
{
	public int guideId;

	public readonly List<GuideInfoConfigureItem> GuideInfos;

	public int runningIndex;

	public bool NextStatus => GuideInfos.Count > runningIndex + 1;

	public GuideInfoConfigureItem currentGuide => GuideInfos[runningIndex];

	public GuideInfo(int _guideId, GuideInfoConfigure _guideInfo)
	{
		guideId = _guideId;
		RepeatedField<GuideInfoConfigureItem> guideInfoConfigureItems = _guideInfo.GuideInfoConfigureItems;
		GuideInfos = new List<GuideInfoConfigureItem>(guideInfoConfigureItems.Count);
		for (int i = 0; i < guideInfoConfigureItems.Count; i++)
		{
			GuideInfos.Add(guideInfoConfigureItems[i]);
		}
	}

	public void StartGuide()
	{
		runningIndex = 0;
		TriggerGuide();
	}

	public void TriggerNext()
	{
		if (currentGuide.IsEnd)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
			return;
		}
		if (!NextStatus)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.TriggerNextGuide(guideId);
			return;
		}
		runningIndex++;
		TriggerGuide();
	}

	private void TriggerGuide()
	{
		if (GuideInfos.Count > runningIndex)
		{
			MethodInfo method = typeof(GuideEvent).GetMethod($"GuideEvent_{guideId}_{currentGuide.StepId}");
			SimpleSingletonProvider<UIManager>.inst.guide.TryShow(this, (object)method == null);
			method?.Invoke(null, null);
			SimpleSingletonProvider<WebServerManager>.inst.PostGuideRecord(guideId, GuideInfos[runningIndex].StepId);
		}
	}
}

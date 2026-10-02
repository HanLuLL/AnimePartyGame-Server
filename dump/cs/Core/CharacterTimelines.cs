using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace Core;

public class CharacterTimelines
{
	private AsyncOperationHandle<IList<Object>> Handle;

	private readonly Dictionary<string, TimelineAsset> timelineMaps = new Dictionary<string, TimelineAsset>();

	public CharacterTimelines(AsyncOperationHandle<IList<Object>> handle)
	{
		Handle = handle;
	}

	public void Add(TimelineAsset timeline)
	{
		if (timelineMaps.ContainsKey(((Object)(object)timeline).name))
		{
			timelineMaps.Remove(((Object)(object)timeline).name);
		}
		timelineMaps.TryAdd(((Object)(object)timeline).name, timeline);
	}

	public void Clear()
	{
		timelineMaps.Clear();
		if (Handle.IsValid())
		{
			Addressables.Release(Handle);
		}
	}

	public TimelineAsset GetTimelineAsset(string _Name)
	{
		if (!timelineMaps.TryGetValue(_Name, out var value) && !string.IsNullOrEmpty(_Name))
		{
			string text = string.Join(',', timelineMaps.Keys.ToList());
			Debug.LogError("无法通过key:" + _Name + " 获取Timeline资产, 当前资产：" + text);
		}
		return value;
	}
}

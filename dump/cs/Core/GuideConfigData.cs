using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace Core;

public class GuideConfigData
{
	public readonly Dictionary<int, GuideInfo> GuideDict;

	public GuideConfigData()
	{
		RepeatedField<GuideInfoConfigure> infos = StaticConfigure.Guide.Infos;
		GuideDict = new Dictionary<int, GuideInfo>(infos.Count);
		for (int i = 0; i < infos.Count; i++)
		{
			if (!GuideDict.ContainsKey(infos[i].GuideId))
			{
				GuideDict.TryAdd(infos[i].GuideId, new GuideInfo(infos[i].GuideId, infos[i]));
			}
		}
	}
}

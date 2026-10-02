using UnityEngine;

namespace Core.Net;

public enum ServerType
{
	[InspectorName("开发服")]
	DEV = 0,
	[InspectorName("电蝉服")]
	Dianchan = 1,
	[InspectorName("马服")]
	Ma = 3,
	[InspectorName("老胡服")]
	LaoHu = 4,
	[InspectorName("老段服")]
	LaoDuan = 5,
	[InspectorName("本地服")]
	Local = 6,
	[InspectorName("自定义")]
	Custom = 7
}

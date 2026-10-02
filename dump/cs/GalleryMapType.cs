using Google.Protobuf.Reflection;
using UnityEngine;

public enum GalleryMapType
{
	[InspectorName("无")]
	[OriginalName("GalleryMapType_None")]
	None,
	[InspectorName("PVE图鉴")]
	[OriginalName("GalleryMapType_GalleryMapPVE")]
	GalleryMapPve,
	[InspectorName("PVP图鉴")]
	[OriginalName("GalleryMapType_GalleryMapPVP")]
	GalleryMapPvp,
	[InspectorName("特殊图鉴")]
	[OriginalName("GalleryMapType_GalleryMapSpecial")]
	GalleryMapSpecial
}

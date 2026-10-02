using System;

namespace FairyGUI;

[Flags]
public enum MaterialFlags
{
	Clipped = 1,
	SoftClipped = 2,
	StencilTest = 4,
	AlphaMask = 8,
	Grayed = 0x10,
	ColorFilter = 0x20
}

using UnityEngine;

namespace FairyGUI;

public interface ITextColorGear : IColorGear
{
	Color strokeColor { get; set; }
}

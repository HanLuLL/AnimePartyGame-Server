using System;
using UnityEngine;

namespace FairyGUI;

public class DisplayObjectInfo : MonoBehaviour
{
	[NonSerialized]
	public DisplayObject displayObject;

	private void OnDestroy()
	{
		if (displayObject != null)
		{
			displayObject._flags |= DisplayObject.Flags.GameObjectDisposed;
		}
	}
}

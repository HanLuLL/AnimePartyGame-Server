using System;
using UnityEngine;

namespace FairyGUI;

public class NAudioClip
{
	public static Action<AudioClip> CustomDestroyMethod;

	public DestroyMethod destroyMethod;

	public AudioClip nativeClip;

	public NAudioClip(AudioClip audioClip)
	{
		nativeClip = audioClip;
	}

	public void Unload()
	{
		if ((UnityEngine.Object)(object)nativeClip == null)
		{
			return;
		}
		if (destroyMethod == DestroyMethod.Unload)
		{
			Resources.UnloadAsset((UnityEngine.Object)(object)nativeClip);
		}
		else if (destroyMethod == DestroyMethod.Destroy)
		{
			UnityEngine.Object.DestroyImmediate((UnityEngine.Object)(object)nativeClip, allowDestroyingAssets: true);
		}
		else if (destroyMethod == DestroyMethod.Custom)
		{
			if (CustomDestroyMethod == null)
			{
				Debug.LogWarning("NAudioClip.CustomDestroyMethod must be set to handle DestroyMethod.Custom");
			}
			else
			{
				CustomDestroyMethod(nativeClip);
			}
		}
		nativeClip = null;
	}

	public void Reload(AudioClip audioClip)
	{
		if ((UnityEngine.Object)(object)nativeClip != null && (UnityEngine.Object)(object)nativeClip != (UnityEngine.Object)(object)audioClip)
		{
			Unload();
		}
		nativeClip = audioClip;
	}
}

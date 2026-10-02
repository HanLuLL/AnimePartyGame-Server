using UnityEngine;

public static class PlatformTarget
{
	public static bool IsMobileTarget => IsMobilePlatform();

	private static bool IsMobilePlatform()
	{
		RuntimePlatform platform = Application.platform;
		return platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer;
	}
}

using Tools;
using UI;
using UnityEngine;

namespace Core.Scene;

public abstract class BaseSceneController : MonoBehaviour
{
	private float _TryGMStartTime;

	private float _TryGMCurTime;

	private readonly float _TryGMInterval = 1f;

	private int currentScreenWidth;

	private int currentScreenHeight;

	protected virtual void Awake()
	{
		Screen.sleepTimeout = -1;
		Application.runInBackground = true;
		SimpleSingletonProvider<SceneManager>.inst.currentType.Value = GetSceneType();
		currentScreenWidth = Screen.width;
		currentScreenHeight = Screen.height;
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
		if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight)
		{
			currentScreenWidth = Screen.width;
			currentScreenHeight = Screen.height;
			GameSettings.InitScreenSetting();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BnSdkManager.Instance.ExitSdk();
		}
		if (!GMConfig._Enable)
		{
			return;
		}
		if (Input.touchCount == 2)
		{
			if (Time.time - _TryGMStartTime > _TryGMInterval && !SimpleSingletonProvider<UIManager>.inst.GM.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.GM.Show();
			}
		}
		else
		{
			_TryGMStartTime = Time.time;
		}
	}

	protected virtual void OnDestroy()
	{
	}

	protected virtual void OnApplicationPause(bool pause)
	{
	}

	protected virtual void OnApplicationFocus(bool focus)
	{
	}

	protected abstract SceneType GetSceneType();
}

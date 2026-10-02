using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class TimeProtector : MonoBehaviour
{
	private static TimeProtector instance;

	[SerializeField]
	private float maxSlowDuration = 30f;

	[SerializeField]
	private float maxSlowDuration_CE = 10f;

	[SerializeField]
	private float timeScaleThreshold = 0.8f;

	[SerializeField]
	private bool isTestOnEditor;

	[SerializeField]
	private float slowDurationForView;

	private float slowStartTime;

	private bool isCEOpen;

	public static void Create()
	{
		if (instance == null)
		{
			new GameObject("TimeProtector").AddComponent<TimeProtector>();
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	private void Update()
	{
		bool isConnected = MonoSingletonProvider<NetManager>.inst.IsConnected;
		bool flag = IsDevelopmentBuild();
		bool flag2 = IsEditor();
		bool flag3 = HackerConfig.IsValid();
		if (!isConnected || flag3 || ((flag2 || flag) && (!flag2 || !isTestOnEditor)))
		{
			slowStartTime = Time.realtimeSinceStartup;
		}
		else
		{
			TimeScaleCheck();
		}
	}

	private async UniTask KickPlayer()
	{
		MonoSingletonProvider<NetManager>.inst.CloseServerByKick();
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(9).Forget();
		await UniTask.DelayFrame(100);
		await SimpleSingletonProvider<GameManager>.inst.ReLogin();
	}

	private void TimeScaleCheck()
	{
		if (Time.timeScale < timeScaleThreshold)
		{
			float slowDuration = GetSlowDuration();
			float num = (isCEOpen ? maxSlowDuration_CE : maxSlowDuration);
			if (slowDuration > num)
			{
				slowStartTime = Time.realtimeSinceStartup;
				KickPlayer().Forget();
			}
		}
		else
		{
			slowStartTime = Time.realtimeSinceStartup;
		}
	}

	public float GetSlowDuration()
	{
		return Time.realtimeSinceStartup - slowStartTime;
	}

	public bool IsDevelopmentBuild()
	{
		return false;
	}

	public bool IsEditor()
	{
		return false;
	}
}

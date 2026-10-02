using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class GameManager : SimpleSingletonProvider<GameManager>
{
	public string ActionVideoName;

	public bool needLoadBg;

	public bool IsLogined
	{
		get
		{
			if (MonoSingletonProvider<NetManager>.hasInstance)
			{
				return MonoSingletonProvider<NetManager>.inst.IsConnected;
			}
			return false;
		}
	}

	public static async void GameStart()
	{
		await SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Login, "Login");
	}

	public async UniTask ReLogin()
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			BattleSceneController.inst?.UnLoadPreLoadCharacterResources();
		}
		Clear();
		await SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Login, "Login");
	}

	public void RestartGame()
	{
		Clear();
	}

	public void Clear()
	{
		SimpleSingletonProvider<UIManager>.inst.UnLoadGlobalUI();
		SimpleSingletonProvider<UIManager>.inst.Clear();
		NetManager.IsReturningToLogin = true;
		MonoSingletonProvider<NetManager>.inst.Clear();
		MonoSingletonProvider<NetManager>.inst.DestroyNetInst();
		MonoSingletonProvider<CoroutineManager>.inst.DestroyCoroutineInst();
		SimpleSingletonProvider<GameLogicManager>.inst.initialize = false;
		SimpleSingletonProvider<GameLogicManager>.inst.Clear();
		needLoadBg = true;
	}

	public void CloseGame()
	{
		Clear();
		MonoSingletonProvider<NetManager>.inst.Clear();
		MonoSingletonProvider<NetManager>.inst.DestroyNetInst();
		Application.Quit();
	}
}

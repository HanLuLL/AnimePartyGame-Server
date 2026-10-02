using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class TestStory : MonoBehaviour
{
	public StoryData StoryData;

	public async void Start()
	{
		await StaticConfigure.InitAsync();
		GameSettings.InitSetting();
		await SimpleSingletonProvider<UIManager>.inst.AsyncInit();
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_UI);
		SimpleSingletonProvider<UIManager>.inst.story.TryShowStory(StoryData, null).Forget();
	}
}

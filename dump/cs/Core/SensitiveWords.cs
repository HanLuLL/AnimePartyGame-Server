using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public static class SensitiveWords
{
	public static async UniTask<string> DealSensitiveWord(string text)
	{
		return await UniTask.FromResult(text.DealSensitiveWord());
	}

	public static async UniTask<bool> Valid(string text)
	{
		return await UniTask.FromResult(DFAAlgorithm.Valid(text));
	}
}

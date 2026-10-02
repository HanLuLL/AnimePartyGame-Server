using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay;

public class GMData : IModel, IInitialize, IDispose
{
	public int? DicePoint { get; private set; }

	public bool LockGameProgress { get; private set; }

	public int Score { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
	}

	public void Dispose()
	{
		DicePoint = null;
	}

	public void SetDicePoint(int value)
	{
		DicePoint = value;
		if (value < 0)
		{
			DicePoint = null;
		}
	}

	public void SetLockGameProgress(bool value)
	{
		LockGameProgress = value;
	}

	public void SetScore(int value)
	{
		Score = value;
	}
}

using Tools;

namespace Core.Tutorial;

public class TutorialGlobalSignal : IModel, IDispose
{
	public Signal GameStartBefore { get; private set; } = new Signal();

	public Signal GameStart { get; private set; } = new Signal();

	public Signal GameStartAfter { get; private set; } = new Signal();

	public Signal GameEndBefore { get; private set; } = new Signal();

	public Signal GameEnd { get; private set; } = new Signal();

	public Signal GameEndAfter { get; private set; } = new Signal();

	public Signal RoundStartBefore { get; private set; } = new Signal();

	public Signal RoundStart { get; private set; } = new Signal();

	public Signal RoundStartAfter { get; private set; } = new Signal();

	public Signal RoundEndBefore { get; private set; } = new Signal();

	public Signal RoundEnd { get; private set; } = new Signal();

	public Signal RoundEndAfter { get; private set; } = new Signal();

	public void Initialize()
	{
	}

	public void Dispose()
	{
		Dispose_GamePlay();
	}

	private void Dispose_GamePlay()
	{
		GameStartBefore.RemoveAllListeners();
		GameStart.RemoveAllListeners();
		GameStartAfter.RemoveAllListeners();
		GameEndBefore.RemoveAllListeners();
		GameEnd.RemoveAllListeners();
		GameEndAfter.RemoveAllListeners();
		RoundStartBefore.RemoveAllListeners();
		RoundStart.RemoveAllListeners();
		RoundStartAfter.RemoveAllListeners();
		RoundEndBefore.RemoveAllListeners();
		RoundEnd.RemoveAllListeners();
		RoundEndAfter.RemoveAllListeners();
	}
}

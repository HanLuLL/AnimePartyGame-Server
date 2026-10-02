using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Build;

namespace SinglePlayer.GamePlay;

public class BoardManager : ISystem, IInitialize, IDispose
{
	public readonly BoardGameManager gameManager = new BoardGameManager();

	public readonly BoardCardManager cardManager = new BoardCardManager();

	public readonly BoardCharacterManager characterManager = new BoardCharacterManager();

	public readonly BoardMissionManager missionManager = new BoardMissionManager();

	public readonly BuildingManager buildingManager = new BuildingManager();

	public readonly BoardFoundationManager foundationManager = new BoardFoundationManager();

	public readonly BoardRelicManager relicManager = new BoardRelicManager();

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		gameManager.Initialize();
		cardManager.Initialize();
		characterManager.Initialize();
		missionManager.Initialize();
		buildingManager.Initialize();
		foundationManager.Initialize();
		relicManager.Initialize();
	}

	public void Dispose()
	{
		gameManager.Dispose();
		cardManager.Dispose();
		characterManager.Dispose();
		missionManager.Dispose();
		buildingManager.Dispose();
		foundationManager.Dispose();
		relicManager.Dispose();
	}

	public bool CheckGold(int price, bool showMsg = true)
	{
		if (Game.GetModel<GameData>().heroProperty.Gold.Value < price)
		{
			return false;
		}
		return true;
	}

	public void UploadStateData()
	{
		GameStatus gameStatus = Game.GetModel<GameData>().GetGameStatus();
		Game.GetModel<GameData>().UploadDataToServer(gameStatus);
	}
}

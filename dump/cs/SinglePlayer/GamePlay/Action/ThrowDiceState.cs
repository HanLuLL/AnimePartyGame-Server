using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace SinglePlayer.GamePlay.Action;

public class ThrowDiceState : PlayerActionState
{
	public ThrowDiceState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
		await ResetOperateData();
		Game.GetSystem<BoardManager>().gameManager.GenerateDicePoint();
		BoardCharacterManager characterManager = Game.GetSystem<BoardManager>().characterManager;
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is SinglePlayerPanel singlePlayerPanel)
		{
			await singlePlayerPanel.PlayDiceEffect();
		}
		Game.GetSystem<BoardManager>().buildingManager.DoEffect();
		await characterManager.PlayAttributeShow();
		_fsm.OnActionFinished();
	}

	private async UniTask ResetOperateData()
	{
		int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
		if (Game.GetModel<GameData>().MapData.GetLandTypeById(standLandId) == SinglePlayerLandType.Start)
		{
			Game.GetSystem<BoardManager>().cardManager.CloseShop();
		}
		Game.GetSystem<BoardManager>().foundationManager.StopDevelopLand();
		await Game.GetController<CameraController>().ResetFreeCameraPosition();
	}
}

using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;

namespace SinglePlayer.GamePlay.Build;

public abstract class BuildingOperateBaseState : IState
{
	protected BuildingController _buildingController;

	protected MonoBehaviourManager _monoBehaviourManager;

	protected MapData _mapData;

	protected PlayerActionFSM _playerActionFSM;

	public virtual void Init()
	{
		_buildingController = Game.GetController<BuildingController>();
		_monoBehaviourManager = Game.GetSystem<MonoBehaviourManager>();
		_mapData = Game.GetModel<GameData>().MapData;
		_playerActionFSM = Game.GetSystem<PlayerActionFSM>();
	}

	public virtual void Enter()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void Exit()
	{
	}
}

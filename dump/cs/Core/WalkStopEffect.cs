using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

public class WalkStopEffect : GameEffect
{
	[SerializeField]
	public GameObject SelectedEffect;

	private UnitLand _SelectedLand;

	private StopEffectData _StopData;

	private UnityEngine.Camera _MainCamera => BattleSceneController.inst.mainCamera;

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0) || _StopData == null)
		{
			return;
		}
		UnitLand unitLand = RaycastLand();
		if (!(unitLand == null) && unitLand.Id == _StopData.Terminal)
		{
			if ((object)_SelectedLand != null && _SelectedLand.Id == unitLand.Id)
			{
				SimpleSingletonProvider<RoadLineManager>.inst.CloseAllDirections();
			}
			else
			{
				TryShowDirections(unitLand).Forget();
			}
		}
	}

	private UnitLand RaycastLand()
	{
		if (!Stage.isTouchOnUI && (object)_MainCamera != null)
		{
			Ray ray = _MainCamera.ScreenPointToRay(Input.mousePosition);
			int num = 1 << LayerMask.NameToLayer("MapLand");
			RaycastHit val = default(RaycastHit);
			Physics.Raycast(ray, ref val, 999f, num);
			if ((Object)(object)((RaycastHit)(ref val)).collider != null)
			{
				Transform parent = ((Component)(object)((RaycastHit)(ref val)).collider).transform.parent;
				if (parent != null)
				{
					UnitLand component = parent.GetComponent<UnitLand>();
					if (component != null && !component.Disable)
					{
						return component;
					}
				}
			}
		}
		return null;
	}

	public void ReadyRoadLineData(int slot, StopEffectData data)
	{
		ParticleColorController[] componentsInChildren = GetComponentsInChildren<ParticleColorController>();
		if (componentsInChildren != null && componentsInChildren.Length != 0)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetGradient(slot);
			}
		}
		ParticleColorController[] componentsInChildren2 = SelectedEffect.GetComponentsInChildren<ParticleColorController>();
		if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
		{
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].SetGradient(slot);
			}
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.curPlayerId == SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID())
		{
			_StopData = data;
			TryAgainShowDirections().Forget();
		}
	}

	public async UniTask TryAgainShowDirections()
	{
		await TryShowDirections(_SelectedLand);
	}

	private async UniTask TryShowDirections(UnitLand curLand)
	{
		if (!(curLand == null) && curLand.Id == _StopData.Terminal && _StopData != null)
		{
			SimpleSingletonProvider<RoadLineManager>.inst.CloseAllDirections();
			_SelectedLand = curLand;
			SelectedEffect.SetActive(value: true);
			await _StopData.ShowTerminalPath();
		}
	}

	protected override void Dispose()
	{
		_SelectedLand = null;
		base.Dispose();
	}

	public void CloseAllDirections()
	{
		_SelectedLand = null;
		if (SelectedEffect != null)
		{
			SelectedEffect.SetActive(value: false);
		}
	}
}

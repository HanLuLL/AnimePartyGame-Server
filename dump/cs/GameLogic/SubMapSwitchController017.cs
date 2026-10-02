using System.Collections.Generic;
using Core.Unit;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameLogic;

public class SubMapSwitchController017 : SubMapSwitchController
{
	[SerializeField]
	private Vector3 vfx2LocalPosition;

	private float timePoint0 = 4f;

	private float timePoint1 = 1.13f;

	private List<PolySurfaceMaterialController> surfaceControllers = new List<PolySurfaceMaterialController>();

	[SerializeField]
	private List<Material> polySurfaceMats = new List<Material>();

	private void Awake()
	{
		FindPolySurfaces();
	}

	public override void StartSubMapSwitch(int fromId, int toId)
	{
		base.StartSubMapSwitch(fromId, toId);
		int childCount = vfxRoot.childCount;
		for (int i = 0; i < childCount; i++)
		{
			vfxRoot.GetChild(i).gameObject.SetActive(i == fromId);
		}
		Material srcFromMat = polySurfaceMats[fromId];
		Material srcToMat = polySurfaceMats[toId];
		foreach (PolySurfaceMaterialController surfaceController in surfaceControllers)
		{
			surfaceController.OnStartSubMapSwitch(srcFromMat, srcToMat);
		}
	}

	public override void TriggerEvent(float lastFrameTime, float currentFrameTime, int fromId, int toId)
	{
		if (lastFrameTime < timePoint0 && currentFrameTime >= timePoint0 && fromId == 0 && toId == 1)
		{
			SubMap subMap = subMapList[currentFromId];
			subMapList[currentToId].SetStencil(0, CompareFunction.Disabled);
			subMap.SetStencil(0, CompareFunction.Disabled);
			subMap.MapRoot.gameObject.SetActive(value: false);
			foreach (PolySurfaceMaterialController surfaceController in surfaceControllers)
			{
				surfaceController.OnSwitchFinish();
			}
		}
		if (lastFrameTime < timePoint1 && currentFrameTime >= timePoint1 && fromId == 1 && toId == 0)
		{
			SubMap subMap2 = subMapList[currentFromId];
			subMapList[currentToId].SetStencil(0, CompareFunction.Disabled);
			subMap2.SetStencil(0, CompareFunction.Disabled);
			subMap2.MapRoot.gameObject.SetActive(value: false);
			foreach (PolySurfaceMaterialController surfaceController2 in surfaceControllers)
			{
				surfaceController2.OnSwitchFinish();
			}
		}
		if (fromId == 1 && toId == 0)
		{
			Camera main = Camera.main;
			if ((bool)main)
			{
				Vector3 position = main.transform.TransformPoint(vfx2LocalPosition);
				vfxRoot.GetChild(1).position = position;
			}
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		foreach (PolySurfaceMaterialController surfaceController in surfaceControllers)
		{
			surfaceController.OnDestory();
		}
	}

	public void FindPolySurfaces()
	{
		surfaceControllers.Clear();
		PlatformFrame[] array = Object.FindObjectsOfType<PlatformFrame>(includeInactive: true);
		for (int i = 0; i < array.Length; i++)
		{
			GameObject polySurface = array[i].PolySurface;
			if (polySurface != null)
			{
				surfaceControllers.Add(new PolySurfaceMaterialController(polySurface));
			}
		}
	}
}

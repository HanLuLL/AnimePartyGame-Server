using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class MapGimmickManager007 : MapGimmickManager
{
	[SerializeField]
	public LandGroupManager LandGroupManager;

	[SerializeField]
	public GameObject[] DreamEffects;

	private List<int> _showEffectObjIndex = new List<int>();

	protected override void Awake()
	{
		Initialize();
		base.Awake();
	}

	public override void Initialize()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room == null)
		{
			return;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		MapField<int, int> mapField = curRoomInfo?.info.MapStatus;
		if (mapField != null && mapField.Count > 0)
		{
			foreach (KeyValuePair<int, int> item in mapField)
			{
				SwitchGimmick(item.Key, item.Value, wait: false).Forget();
			}
		}
		_showEffectObjIndex.Clear();
		if (curRoomInfo != null && curRoomInfo.IsTerms)
		{
			using (IEnumerator<int> enumerator2 = curRoomInfo.RoomTerms.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					switch (enumerator2.Current)
					{
					case 60006:
						_showEffectObjIndex.Add(1);
						break;
					case 60007:
						_showEffectObjIndex.Add(2);
						break;
					case 60013:
						_showEffectObjIndex.Add(3);
						break;
					}
				}
			}
			int num = 0;
			if (_showEffectObjIndex.Count > 0)
			{
				num = _showEffectObjIndex[UnityEngine.Random.Range(0, _showEffectObjIndex.Count)];
			}
			for (int i = 0; i < DreamEffects.Length; i++)
			{
				DreamEffects[i].SetActive(i == num);
			}
		}
		else
		{
			DreamEffects[0].SetActive(value: true);
			DreamEffects[1].SetActive(value: false);
			DreamEffects[2].SetActive(value: false);
			DreamEffects[3].SetActive(value: false);
		}
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
		LandGroupManager.SwitchStatus(groupId, statusId);
	}

	public override async UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		RefreshGimmickData(groupId, statusId);
		LandGroup landGroup = LandGroupManager.GetLandGroup(groupId);
		for (int i = 0; i < landGroup.Lands.Count; i++)
		{
			landGroup.Lands[i].RefreshDisplayByGimmick();
		}
		await UniTask.CompletedTask;
	}
}

using System.Collections.Generic;
using FairyGUI;
using Tools;

namespace UI;

public class TrainingLevelPanel : BasePanel<UITrainingLevelPanel>
{
	private List<int> PVPMapIds;

	private List<int> PVEMapIds;

	private UICom_CreateRoom Com_CreateRoom => base.ui.com_CreateRoom as UICom_CreateRoom;

	public TrainingLevelPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UITrainingLevelPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Maps.itemRenderer = RendererMapSelect;
		Com_CreateRoom.CreateRoom_InitComponents();
	}

	private void RendererMapSelect(int index, GObject item)
	{
		UITrainingLevel_Map_Item btn_Map = item as UITrainingLevel_Map_Item;
		if (btn_Map == null)
		{
			return;
		}
		MapInfoConfigure mapInfo = null;
		if (index < PVPMapIds.Count)
		{
			mapInfo = PVPMapIds[index].GetMapDataConfigure();
			btn_Map.modeType.selectedIndex = 0;
		}
		else if (index - PVPMapIds.Count < PVEMapIds.Count)
		{
			mapInfo = PVEMapIds[index - PVPMapIds.Count].GetMapDataConfigure();
			btn_Map.modeType.selectedIndex = 1;
		}
		if (mapInfo == null)
		{
			return;
		}
		btn_Map.loader_Map.url = mapInfo.MapSceneImage;
		btn_Map.txt_Description.text = mapInfo.MapDescription.GetLocal(UIStringType.Map);
		btn_Map.txt_MapName.text = mapInfo.MapName.GetLocal(UIStringType.Map);
		btn_Map.visible = false;
		btn_Map.Cut_in.Play(1, 0.01f * (float)index, delegate
		{
			btn_Map.visible = true;
		}, null);
		btn_Map.onClick.Set((EventCallback0)delegate
		{
			btn_Map.onClick.Retain();
			base.ui.tab.selectedIndex = 1;
			MapModeType mapModeType = ((btn_Map.modeType.selectedIndex == 0) ? MapModeType.PracticePvp : MapModeType.PracticePve);
			Com_CreateRoom.CreateRoom_Refresh(mapModeType, mapInfo.Id, delegate
			{
				base.ui.tab.selectedIndex = 0;
			});
			btn_Map.onClick.Release();
		});
	}

	public override void Refresh()
	{
		base.Refresh();
		PVPMapIds = new List<int>();
		PVEMapIds = new List<int>();
		ReadyMapId(MapModeType.PracticePvp, PVPMapIds);
		ReadyMapId(MapModeType.PracticePve, PVEMapIds);
		base.ui.list_Maps.numItems = PVPMapIds.Count + PVEMapIds.Count;
	}

	private void ReadyMapId(MapModeType type, List<int> mapIds)
	{
		if (!StaticConfigure.GameMode.InfoDict.TryGetValue((int)type, out var value) || !TimeHelper.ValidityTime(value.BeginTime, value.EndTime))
		{
			return;
		}
		foreach (int item in value.MapID)
		{
			MapInfoConfigure mapDataConfigure = item.GetMapDataConfigure();
			if (mapDataConfigure != null && TimeHelper.ValidityTime(mapDataConfigure.BeginTimeMap, mapDataConfigure.EndTimeMap))
			{
				mapIds.Add(item);
			}
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		Com_CreateRoom.CreateRoom_AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		Com_CreateRoom.CreateRoom_RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		Com_CreateRoom.CreateRoom_AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		Com_CreateRoom.CreateRoom_RemoveListener();
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			Com_CreateRoom.CreateRoom_Close();
		}
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}
}

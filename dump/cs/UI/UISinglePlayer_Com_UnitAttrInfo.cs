using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Character;
using UnityEngine;

namespace UI;

public class UISinglePlayer_Com_UnitAttrInfo : GComponent
{
	public GList list_AttrChange;

	public const string URL = "ui://mi9vm3w0as5tq48";

	public IUnitView UnitView { get; set; }

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (UnitView != null)
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(UnitView.GetPosition());
			vector.y = (float)Screen.height - vector.y;
			base.xy = base.parent.GlobalToLocal(vector);
			base.visible = true;
		}
	}

	public void Init(IUnitView view)
	{
		UnitView = view;
	}

	public void ShowAttrInfo(AttributeChangeInfo message)
	{
		if (!(UnitView is BuildingView))
		{
			return;
		}
		GObject item = list_AttrChange.AddItemFromPool();
		if (!(item is UISinglePlayer_Com_AttrTip uISinglePlayer_Com_AttrTip))
		{
			return;
		}
		if (uISinglePlayer_Com_AttrTip.InitBuildingAttrChange(message))
		{
			uISinglePlayer_Com_AttrTip.visible = true;
			uISinglePlayer_Com_AttrTip.showAttr.Play(delegate
			{
				list_AttrChange.RemoveChildToPool(item);
			});
		}
		else
		{
			list_AttrChange.RemoveChildToPool(item);
		}
	}

	public void ShowAttrInfo(PropertyType type, int value)
	{
		if (!(UnitView is UnitView))
		{
			return;
		}
		GObject item = list_AttrChange.AddItemFromPool();
		if (item is UISinglePlayer_Com_AttrTip uISinglePlayer_Com_AttrTip)
		{
			uISinglePlayer_Com_AttrTip.visible = true;
			uISinglePlayer_Com_AttrTip.InitCharacterAttrChange(type, value);
			uISinglePlayer_Com_AttrTip.showAttr.Play(delegate
			{
				list_AttrChange.RemoveChildToPool(item);
			});
		}
	}

	public static UISinglePlayer_Com_UnitAttrInfo CreateInstance()
	{
		return (UISinglePlayer_Com_UnitAttrInfo)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_UnitAttrInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_AttrChange = (GList)GetChildAt(0);
	}
}

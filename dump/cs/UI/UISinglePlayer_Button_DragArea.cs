using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Card;
using UnityEngine;

namespace UI;

public class UISinglePlayer_Button_DragArea : GComponent
{
	public Card CurrentCard;

	private Vector2 _pressDownPosition;

	public UISinglePlayer_Com_CardItemSlot com_Item;

	public const string URL = "ui://mi9vm3w0ap9hc";

	public static UISinglePlayer_Button_DragArea Create()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0ap9hc", typeof(UISinglePlayer_Button_DragArea));
		UISinglePlayer_Button_DragArea uISinglePlayer_Button_DragArea = CreateInstance();
		uISinglePlayer_Button_DragArea.visible = false;
		uISinglePlayer_Button_DragArea.sortingOrder = 999;
		uISinglePlayer_Button_DragArea.touchable = false;
		return uISinglePlayer_Button_DragArea;
	}

	public void ShowCreateInfo(int CardUID)
	{
		_pressDownPosition = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
		base.position = _pressDownPosition;
		CurrentCard = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(CardUID);
		if (CurrentCard != null)
		{
			com_Item.Refresh(CurrentCard, CardItemSlotType.None);
		}
		base.visible = true;
		com_Item.status.selectedIndex = 2;
	}

	public void HideInfo()
	{
		base.visible = false;
		GRoot.inst.HidePopup(this);
	}

	public static UISinglePlayer_Button_DragArea CreateInstance()
	{
		return (UISinglePlayer_Button_DragArea)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Button_DragArea");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Item = (UISinglePlayer_Com_CardItemSlot)GetChildAt(0);
	}
}

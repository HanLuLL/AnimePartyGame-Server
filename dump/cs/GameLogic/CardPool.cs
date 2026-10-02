using System.Collections.Generic;
using FairyGUI;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Pool;

namespace GameLogic;

public static class CardPool
{
	private static readonly int defaultSize = 8;

	private static readonly int maxSize = 14;

	private static ObjectPool<UIHandCard_Button_Card> pool;

	public static ShowCardStatus ShowCardStatus;

	public static void Initialize(bool collectionCheck = true)
	{
		pool = new ObjectPool<UIHandCard_Button_Card>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck, defaultSize, maxSize);
	}

	public static UIHandCard_Button_Card OnCreatePoolItem()
	{
		UIHandCard_Button_Card uIHandCard_Button_Card = UIHandCard_Button_Card.CreateInstance();
		uIHandCard_Button_Card.displayObject.cachedTransform.gameObject.SetActiveEx(active: false);
		uIHandCard_Button_Card.SetXY(0f, Screen.height);
		return uIHandCard_Button_Card;
	}

	public static void OnGetPoolItem(UIHandCard_Button_Card btn_Card)
	{
		btn_Card.displayObject.cachedTransform.gameObject.SetActiveEx(active: true);
	}

	public static void OnReleasePoolItem(UIHandCard_Button_Card btn_Card)
	{
		btn_Card.displayObject.cachedTransform.gameObject.SetActiveEx(active: false);
		btn_Card.SetXY(0f, Screen.height);
	}

	public static void OnDestroyPoolItem(UIHandCard_Button_Card btn_Card)
	{
		btn_Card.Dispose();
	}

	public static UIHandCard_Button_Card Get(GComponent cardParent)
	{
		UIHandCard_Button_Card uIHandCard_Button_Card = pool.Get();
		uIHandCard_Button_Card.IsRelease = false;
		cardParent.AddChild(uIHandCard_Button_Card);
		return uIHandCard_Button_Card;
	}

	public static void Release(UIHandCard_Button_Card _item)
	{
		if (_item == null)
		{
			Debug.LogError("HandCard item is null");
			return;
		}
		if (_item.IsRelease)
		{
			Debug.LogError("_item is release, dont pool release");
			return;
		}
		_item.parent?.RemoveChild(_item);
		_item.Release();
		pool.Release(_item);
	}

	public static void Clear()
	{
		pool.Clear();
	}

	public static void ReleaseAllCard(List<UIHandCard_Button_Card> _list)
	{
		ShowCardStatus = ShowCardStatus.None;
		if (_list == null || _list.Count == 0)
		{
			return;
		}
		foreach (UIHandCard_Button_Card item in _list)
		{
			Release(item);
		}
		_list.Clear();
	}

	public static void SetOnClickEvent(UIHandCard_Button_Card _item, EventCallback1 callback)
	{
		_item.onClick.Set(callback);
	}

	public static void SetDragEvent(UIHandCard_Button_Card _item, EventCallback1 DragStartEvent, EventCallback1 DragEndEvent, EventCallback1 DragMoveEvent)
	{
		_item.onDragStart.Set(DragStartEvent);
		_item.onDragEnd.Set(DragEndEvent);
		_item.onDragMove.Set(DragMoveEvent);
	}

	public static void ChangeCardZoomStatus(List<UIHandCard_Button_Card> cardItemList, bool isZoom)
	{
		ShowCardStatus = (isZoom ? ShowCardStatus.Display : ShowCardStatus.None);
		for (int i = 0; i < cardItemList.Count; i++)
		{
			cardItemList[i].DisplayZoom(isZoom);
		}
	}

	public static void ZoomOutCard(List<UIHandCard_Button_Card> cardItemList)
	{
		for (int i = 0; i < cardItemList.Count; i++)
		{
			cardItemList[i].ZoomOutCard(isPlayAni: true);
		}
	}
}

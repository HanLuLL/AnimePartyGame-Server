using System.Collections;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class AsyncCreationHelper
{
	private class DisplayListItem
	{
		public PackageItem packageItem;

		public ObjectType type;

		public int childCount;

		public int listItemCount;

		public DisplayListItem(PackageItem pi, ObjectType type)
		{
			packageItem = pi;
			this.type = type;
		}
	}

	public static void CreateObject(PackageItem item, UIPackage.CreateObjectCallback callback)
	{
		Timers.inst.StartCoroutine(_CreateObject(item, callback));
	}

	private static IEnumerator _CreateObject(PackageItem item, UIPackage.CreateObjectCallback callback)
	{
		Stats.LatestObjectCreation = 0;
		Stats.LatestGraphicsCreation = 0;
		float frameTime = UIConfig.frameTimeForAsyncUIConstruction;
		List<DisplayListItem> itemList = new List<DisplayListItem>();
		DisplayListItem displayListItem = new DisplayListItem(item, ObjectType.Component);
		displayListItem.childCount = CollectComponentChildren(item, itemList);
		itemList.Add(displayListItem);
		int cnt = itemList.Count;
		List<GObject> objectPool = new List<GObject>(cnt);
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		bool flag = false;
		for (int i = 0; i < cnt; i++)
		{
			displayListItem = itemList[i];
			if (displayListItem.packageItem != null)
			{
				GObject gObject = UIObjectFactory.NewObject(displayListItem.packageItem);
				objectPool.Add(gObject);
				UIPackage._constructing++;
				if (displayListItem.packageItem.type == PackageItemType.Component)
				{
					int num = objectPool.Count - displayListItem.childCount - 1;
					((GComponent)gObject).ConstructFromResource(objectPool, num);
					objectPool.RemoveRange(num, displayListItem.childCount);
				}
				else
				{
					gObject.ConstructFromResource();
				}
				UIPackage._constructing--;
			}
			else
			{
				GObject gObject = UIObjectFactory.NewObject(displayListItem.type);
				objectPool.Add(gObject);
				if (displayListItem.type == ObjectType.List && displayListItem.listItemCount > 0)
				{
					int num2 = objectPool.Count - displayListItem.listItemCount - 1;
					for (int j = 0; j < displayListItem.listItemCount; j++)
					{
						((GList)gObject).itemPool.ReturnObject(objectPool[j + num2]);
					}
					objectPool.RemoveRange(num2, displayListItem.listItemCount);
				}
			}
			if (i % 5 == 0 && Time.realtimeSinceStartup - realtimeSinceStartup >= frameTime)
			{
				yield return null;
				realtimeSinceStartup = Time.realtimeSinceStartup;
				flag = true;
			}
		}
		if (!flag)
		{
			yield return null;
		}
		callback(objectPool[0]);
	}

	private static int CollectComponentChildren(PackageItem item, List<DisplayListItem> list)
	{
		ByteBuffer rawData = item.rawData;
		rawData.Seek(0, 2);
		int num = rawData.ReadShort();
		for (int i = 0; i < num; i++)
		{
			int num2 = rawData.ReadShort();
			int position = rawData.position;
			rawData.Seek(position, 0);
			ObjectType objectType = (ObjectType)rawData.ReadByte();
			string text = rawData.ReadS();
			string text2 = rawData.ReadS();
			rawData.position = position;
			DisplayListItem displayListItem;
			if (text != null)
			{
				PackageItem packageItem = ((text2 == null) ? item.owner : UIPackage.GetById(text2))?.GetItem(text);
				displayListItem = new DisplayListItem(packageItem, objectType);
				if (packageItem != null && packageItem.type == PackageItemType.Component)
				{
					displayListItem.childCount = CollectComponentChildren(packageItem, list);
				}
			}
			else
			{
				displayListItem = new DisplayListItem(null, objectType);
				if (objectType == ObjectType.List)
				{
					displayListItem.listItemCount = CollectListChildren(rawData, list);
				}
			}
			list.Add(displayListItem);
			rawData.position = position + num2;
		}
		return num;
	}

	private static int CollectListChildren(ByteBuffer buffer, List<DisplayListItem> list)
	{
		buffer.Seek(buffer.position, 8);
		string text = buffer.ReadS();
		int num = 0;
		int num2 = buffer.ReadShort();
		for (int i = 0; i < num2; i++)
		{
			int num3 = buffer.ReadShort();
			num3 += buffer.position;
			string text2 = buffer.ReadS();
			if (text2 == null)
			{
				text2 = text;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				PackageItem itemByURL = UIPackage.GetItemByURL(text2);
				if (itemByURL != null)
				{
					DisplayListItem displayListItem = new DisplayListItem(itemByURL, itemByURL.objectType);
					if (itemByURL.type == PackageItemType.Component)
					{
						displayListItem.childCount = CollectComponentChildren(itemByURL, list);
					}
					list.Add(displayListItem);
					num++;
				}
			}
			buffer.position = num3;
		}
		return num;
	}
}

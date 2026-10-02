using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class PackageItem
{
	public UIPackage owner;

	public PackageItemType type;

	public ObjectType objectType;

	public string id;

	public string name;

	public int width;

	public int height;

	public string file;

	public bool exported;

	public NTexture texture;

	public ByteBuffer rawData;

	public string[] branches;

	public string[] highResolution;

	public Rect? scale9Grid;

	public bool scaleByTile;

	public int tileGridIndice;

	public PixelHitTestData pixelHitTestData;

	public float interval;

	public float repeatDelay;

	public bool swing;

	public MovieClip.Frame[] frames;

	public bool translated;

	public UIObjectFactory.GComponentCreator extensionCreator;

	public BitmapFont bitmapFont;

	public NAudioClip audioClip;

	public Vector2 skeletonAnchor;

	public object skeletonAsset;

	public object Load()
	{
		return owner.GetItemAsset(this);
	}

	public PackageItem getBranch()
	{
		if (branches != null && owner._branchIndex != -1)
		{
			string text = branches[owner._branchIndex];
			if (text != null)
			{
				return owner.GetItem(text);
			}
		}
		return this;
	}

	public PackageItem getHighResolution()
	{
		if (highResolution != null && GRoot.contentScaleLevel > 0)
		{
			int num = GRoot.contentScaleLevel - 1;
			if (num >= highResolution.Length)
			{
				num = highResolution.Length - 1;
			}
			string text = highResolution[num];
			if (text != null)
			{
				return owner.GetItem(text);
			}
		}
		return this;
	}
}

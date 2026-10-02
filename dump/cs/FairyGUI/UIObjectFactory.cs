using System;
using System.Collections.Generic;

namespace FairyGUI;

public class UIObjectFactory
{
	public delegate GComponent GComponentCreator();

	public delegate GLoader GLoaderCreator();

	private static Dictionary<string, GComponentCreator> packageItemExtensions = new Dictionary<string, GComponentCreator>();

	private static GLoaderCreator loaderCreator;

	public static void SetPackageItemExtension(string url, Type type)
	{
		SetPackageItemExtension(url, () => (GComponent)Activator.CreateInstance(type));
	}

	public static void SetPackageItemExtension(string url, GComponentCreator creator)
	{
		if (url == null)
		{
			throw new Exception("Invaild url: " + url);
		}
		PackageItem itemByURL = UIPackage.GetItemByURL(url);
		if (itemByURL != null)
		{
			itemByURL.extensionCreator = creator;
		}
		packageItemExtensions[url] = creator;
	}

	public static void SetLoaderExtension(Type type)
	{
		loaderCreator = () => (GLoader)Activator.CreateInstance(type);
	}

	public static void SetLoaderExtension(GLoaderCreator creator)
	{
		loaderCreator = creator;
	}

	internal static void ResolvePackageItemExtension(PackageItem pi)
	{
		if (!packageItemExtensions.TryGetValue("ui://" + pi.owner.id + pi.id, out pi.extensionCreator) && !packageItemExtensions.TryGetValue("ui://" + pi.owner.name + "/" + pi.name, out pi.extensionCreator))
		{
			pi.extensionCreator = null;
		}
	}

	public static void Clear()
	{
		packageItemExtensions.Clear();
		loaderCreator = null;
	}

	public static GObject NewObject(PackageItem pi, Type userClass = null)
	{
		GObject gObject;
		if (pi.type == PackageItemType.Component)
		{
			if (userClass != null)
			{
				Stats.LatestObjectCreation++;
				gObject = (GComponent)Activator.CreateInstance(userClass);
			}
			else if (pi.extensionCreator != null)
			{
				Stats.LatestObjectCreation++;
				gObject = pi.extensionCreator();
			}
			else
			{
				gObject = NewObject(pi.objectType);
			}
		}
		else
		{
			gObject = NewObject(pi.objectType);
		}
		if (gObject != null)
		{
			gObject.packageItem = pi;
		}
		return gObject;
	}

	public static GObject NewObject(ObjectType type)
	{
		Stats.LatestObjectCreation++;
		switch (type)
		{
		case ObjectType.Image:
			return new GImage();
		case ObjectType.MovieClip:
			return new GMovieClip();
		case ObjectType.Component:
			return new GComponent();
		case ObjectType.Text:
			return new GTextField();
		case ObjectType.RichText:
			return new GRichTextField();
		case ObjectType.InputText:
			return new GTextInput();
		case ObjectType.Group:
			return new GGroup();
		case ObjectType.List:
			return new GList();
		case ObjectType.Graph:
			return new GGraph();
		case ObjectType.Loader:
			if (loaderCreator != null)
			{
				return loaderCreator();
			}
			return new GLoader();
		case ObjectType.Button:
			return new GButton();
		case ObjectType.Label:
			return new GLabel();
		case ObjectType.ProgressBar:
			return new GProgressBar();
		case ObjectType.Slider:
			return new GSlider();
		case ObjectType.ScrollBar:
			return new GScrollBar();
		case ObjectType.ComboBox:
			return new GComboBox();
		case ObjectType.Tree:
			return new GTree();
		case ObjectType.Loader3D:
			return new GLoader3D();
		default:
			return null;
		}
	}
}

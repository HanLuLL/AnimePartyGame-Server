using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using FairyGUI.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FairyGUI;

public class UIPackage
{
	public delegate object LoadResource(string name, string extension, Type type, out DestroyMethod destroyMethod);

	public delegate void LoadResourceAsync(string name, string extension, Type type, PackageItem item);

	public delegate void CreateObjectCallback(GObject result);

	private class AtlasSprite
	{
		public PackageItem atlas;

		public Rect rect;

		public Vector2 offset;

		public Vector2 originalSize;

		public bool rotated;
	}

	private readonly Dictionary<string, Texture> _textures;

	public static bool unloadBundleByFGUI = true;

	private List<PackageItem> _items;

	private Dictionary<string, PackageItem> _itemsById;

	private Dictionary<string, PackageItem> _itemsByName;

	private Dictionary<string, string>[] _dependencies;

	private string _assetPath;

	private string[] _branches;

	internal int _branchIndex;

	private AssetBundle _resBundle;

	private string _customId;

	private bool _fromBundle;

	private LoadResource _loadFunc;

	private LoadResourceAsync _loadAsyncFunc;

	private Dictionary<string, AtlasSprite> _sprites;

	private static Dictionary<string, UIPackage> _packageInstById = new Dictionary<string, UIPackage>();

	private static Dictionary<string, UIPackage> _packageInstByName = new Dictionary<string, UIPackage>();

	private static List<UIPackage> _packageList = new List<UIPackage>();

	private static string _branch;

	private static Dictionary<string, string> _vars = new Dictionary<string, string>();

	internal static int _constructing;

	public const string URL_PREFIX = "ui://";

	private static LoadResource _loadFromResourcesPath = delegate(string name, string extension, Type type, out DestroyMethod destroyMethod)
	{
		destroyMethod = DestroyMethod.Unload;
		return Resources.Load(name, type);
	};

	public string id { get; private set; }

	public string name { get; private set; }

	public static string branch
	{
		get
		{
			return _branch;
		}
		set
		{
			_branch = value;
			bool flag = string.IsNullOrEmpty(_branch);
			Dictionary<string, UIPackage>.Enumerator enumerator = _packageInstById.GetEnumerator();
			while (enumerator.MoveNext())
			{
				UIPackage value2 = enumerator.Current.Value;
				if (flag)
				{
					value2._branchIndex = -1;
				}
				else if (value2._branches != null)
				{
					value2._branchIndex = Array.IndexOf(value2._branches, value);
				}
			}
			enumerator.Dispose();
		}
	}

	public string assetPath => _assetPath;

	public string customId
	{
		get
		{
			return _customId;
		}
		set
		{
			if (_customId != null)
			{
				_packageInstById.Remove(_customId);
			}
			_customId = value;
			if (_customId != null)
			{
				_packageInstById[_customId] = this;
			}
		}
	}

	public AssetBundle resBundle => _resBundle;

	public Dictionary<string, string>[] dependencies => _dependencies;

	public static event Action<PackageItem> onReleaseResource;

	public static async UniTask<UIPackage> AddPackageAsync(string packageName, string assetNamePrefix)
	{
		if (_packageInstByName.TryGetValue(packageName, out var value))
		{
			return value;
		}
		if (_packageInstById.TryGetValue(packageName, out value))
		{
			return value;
		}
		UIPackage pkg = new UIPackage();
		AsyncOperationHandle<TextAsset> textAssetHandle = Addressables.LoadAssetAsync<TextAsset>(packageName + "_fui");
		TextAsset obj = await textAssetHandle;
		if (obj == null)
		{
			throw new Exception("FairyGUI: Cannot load ui package in '" + packageName + "'");
		}
		Addressables.Release(textAssetHandle);
		ByteBuffer buffer = new ByteBuffer(obj.bytes);
		pkg._loadFunc = delegate(string name, string _, Type _, out DestroyMethod destroyMethod)
		{
			destroyMethod = DestroyMethod.Custom;
			string key = packageName + "_" + name;
			pkg._textures.TryGetValue(key, out var value3);
			return value3;
		};
		if (!pkg.LoadPackage(buffer, assetNamePrefix))
		{
			return null;
		}
		_packageInstById[pkg.id] = pkg;
		_packageInstByName[pkg.name] = pkg;
		_packageList.Add(pkg);
		foreach (PackageItem item in pkg._items)
		{
			if (item.type == PackageItemType.Atlas)
			{
				string atlasLoadKey = packageName + "_" + Path.GetFileNameWithoutExtension(item.file);
				if (!pkg._textures.ContainsKey(atlasLoadKey))
				{
					Texture value2 = await Addressables.LoadAssetAsync<Texture>(atlasLoadKey);
					pkg._textures.Add(atlasLoadKey, value2);
				}
			}
		}
		return pkg;
	}

	public static bool ExistPackage(string packageIdOrName)
	{
		if (_packageInstByName.ContainsKey(packageIdOrName))
		{
			return true;
		}
		if (_packageInstById.ContainsKey(packageIdOrName))
		{
			return true;
		}
		return false;
	}

	public static async UniTask<UIPackage> AddPackageFromFileAsync(string packageName, string assetNamePrefix)
	{
		if (_packageInstByName.TryGetValue(packageName, out var value))
		{
			return value;
		}
		if (_packageInstById.TryGetValue(packageName, out value))
		{
			return value;
		}
		byte[] data = await ReadExternalFuiBytes(packageName);
		UIPackage pkg = new UIPackage();
		ByteBuffer buffer = new ByteBuffer(data);
		pkg._loadFunc = delegate(string name, string _, Type _, out DestroyMethod destroyMethod)
		{
			destroyMethod = DestroyMethod.Destroy;
			string key = packageName + "_" + name;
			pkg._textures.TryGetValue(key, out var value2);
			return value2;
		};
		if (!pkg.LoadPackage(buffer, assetNamePrefix))
		{
			return null;
		}
		_packageInstById[pkg.id] = pkg;
		_packageInstByName[pkg.name] = pkg;
		_packageList.Add(pkg);
		foreach (PackageItem item in pkg._items)
		{
			if (item.type != PackageItemType.Atlas)
			{
				continue;
			}
			string atlasLoadKey = packageName + "_" + Path.GetFileNameWithoutExtension(item.file);
			if (!pkg._textures.ContainsKey(atlasLoadKey))
			{
				Texture texture = await ReadExternalAtlasTexture(atlasLoadKey);
				if (texture != null)
				{
					pkg._textures.Add(atlasLoadKey, texture);
				}
			}
		}
		return pkg;
	}

	private static async UniTask<byte[]> ReadExternalFuiBytes(string packageName)
	{
		string fileName = packageName + "_fui.bytes";
		string localFile = GetLocalFile(fileName);
		if (!string.IsNullOrEmpty(localFile))
		{
			UnityWebRequest request = UnityWebRequest.Get(localFile);
			await request.SendWebRequest();
			if ((int)request.result == 1)
			{
				return request.downloadHandler.data;
			}
		}
		throw new Exception("FairyGUI: Cannot load external ui package '" + packageName + "' (作弊文件目录均未找到 " + fileName + ")");
	}

	private static async UniTask<Texture> ReadExternalAtlasTexture(string atlasLoadKey)
	{
		string localFile = GetLocalFile(atlasLoadKey + ".png");
		if (string.IsNullOrEmpty(localFile))
		{
			return null;
		}
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(localFile);
		await request.SendWebRequest();
		if ((int)request.result != 1)
		{
			return null;
		}
		return ((DownloadHandlerTexture)request.downloadHandler).texture;
	}

	private static string GetLocalFile(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return null;
		}
		string text = null;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.Android:
			text = "file://" + Application.persistentDataPath;
			if (!File.Exists(Path.Combine(Application.persistentDataPath, fileName)))
			{
				return null;
			}
			break;
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
			text = Application.streamingAssetsPath;
			if (!File.Exists(Path.Combine(Application.streamingAssetsPath, fileName)))
			{
				return null;
			}
			break;
		default:
			Debug.LogError($"当前平台错误：{Application.platform}");
			return null;
		}
		if (text != null)
		{
			return Path.Combine(text, fileName);
		}
		return null;
	}

	public UIPackage()
	{
		_items = new List<PackageItem>();
		_itemsById = new Dictionary<string, PackageItem>();
		_itemsByName = new Dictionary<string, PackageItem>();
		_sprites = new Dictionary<string, AtlasSprite>();
		_branchIndex = -1;
		_textures = new Dictionary<string, Texture>();
	}

	public static string GetVar(string key)
	{
		if (_vars.TryGetValue(key, out var value))
		{
			return value;
		}
		return null;
	}

	public static void SetVar(string key, string value)
	{
		if (value == null)
		{
			_vars.Remove(key);
		}
		else
		{
			_vars[key] = value;
		}
	}

	public static UIPackage GetById(string id)
	{
		if (_packageInstById.TryGetValue(id, out var value))
		{
			return value;
		}
		return null;
	}

	public static UIPackage GetByName(string name)
	{
		if (_packageInstByName.TryGetValue(name, out var value))
		{
			return value;
		}
		return null;
	}

	public static UIPackage AddPackage(AssetBundle bundle)
	{
		return AddPackage(bundle, bundle, null);
	}

	public static UIPackage AddPackage(AssetBundle desc, AssetBundle res)
	{
		return AddPackage(desc, res, null);
	}

	public static UIPackage AddPackage(AssetBundle desc, AssetBundle res, string mainAssetName)
	{
		byte[] array = null;
		if (!string.IsNullOrEmpty(mainAssetName))
		{
			TextAsset textAsset = desc.LoadAsset<TextAsset>(mainAssetName);
			if (textAsset != null)
			{
				array = textAsset.bytes;
			}
		}
		else
		{
			string[] allAssetNames = desc.GetAllAssetNames();
			string value = "_fui";
			string[] array2 = allAssetNames;
			foreach (string text in array2)
			{
				if (text.IndexOf(value) != -1)
				{
					TextAsset textAsset2 = desc.LoadAsset<TextAsset>(text);
					if (textAsset2 != null)
					{
						array = textAsset2.bytes;
						mainAssetName = Path.GetFileNameWithoutExtension(text);
						break;
					}
				}
			}
		}
		if (array == null)
		{
			throw new Exception("FairyGUI: no package found in this bundle.");
		}
		if (desc != res)
		{
			desc.Unload(unloadAllLoadedObjects: true);
		}
		ByteBuffer buffer = new ByteBuffer(array);
		UIPackage uIPackage = new UIPackage();
		uIPackage._resBundle = res;
		uIPackage._fromBundle = true;
		int num = mainAssetName.IndexOf("_fui");
		if (num != -1)
		{
			mainAssetName = mainAssetName.Substring(0, num);
		}
		if (!uIPackage.LoadPackage(buffer, mainAssetName))
		{
			return null;
		}
		_packageInstById[uIPackage.id] = uIPackage;
		_packageInstByName[uIPackage.name] = uIPackage;
		_packageList.Add(uIPackage);
		return uIPackage;
	}

	public static UIPackage AddPackage(string descFilePath)
	{
		if (descFilePath.StartsWith("Assets/"))
		{
			Debug.LogWarning("FairyGUI: failed to load package in '" + descFilePath + "'");
			return null;
		}
		return AddPackage(descFilePath, _loadFromResourcesPath);
	}

	public static UIPackage AddPackage(string assetPath, LoadResource loadFunc)
	{
		if (_packageInstById.ContainsKey(assetPath))
		{
			return _packageInstById[assetPath];
		}
		DestroyMethod destroyMethod;
		TextAsset obj = (TextAsset)loadFunc(assetPath + "_fui", ".bytes", typeof(TextAsset), out destroyMethod);
		if (obj == null)
		{
			if (Application.isPlaying)
			{
				throw new Exception("FairyGUI: Cannot load ui package in '" + assetPath + "'");
			}
			Debug.LogWarning("FairyGUI: Cannot load ui package in '" + assetPath + "'");
		}
		ByteBuffer buffer = new ByteBuffer(obj.bytes);
		UIPackage uIPackage = new UIPackage();
		uIPackage._loadFunc = loadFunc;
		uIPackage._assetPath = assetPath;
		if (!uIPackage.LoadPackage(buffer, assetPath))
		{
			return null;
		}
		_packageInstById[uIPackage.id] = uIPackage;
		_packageInstByName[uIPackage.name] = uIPackage;
		_packageInstById[assetPath] = uIPackage;
		_packageList.Add(uIPackage);
		return uIPackage;
	}

	public static UIPackage AddPackage(byte[] descData, string assetNamePrefix, LoadResource loadFunc)
	{
		ByteBuffer buffer = new ByteBuffer(descData);
		UIPackage uIPackage = new UIPackage();
		uIPackage._loadFunc = loadFunc;
		if (!uIPackage.LoadPackage(buffer, assetNamePrefix))
		{
			return null;
		}
		_packageInstById[uIPackage.id] = uIPackage;
		_packageInstByName[uIPackage.name] = uIPackage;
		_packageList.Add(uIPackage);
		return uIPackage;
	}

	public static UIPackage AddPackage(byte[] descData, string assetNamePrefix, LoadResourceAsync loadFunc)
	{
		ByteBuffer buffer = new ByteBuffer(descData);
		UIPackage uIPackage = new UIPackage();
		uIPackage._loadAsyncFunc = loadFunc;
		if (!uIPackage.LoadPackage(buffer, assetNamePrefix))
		{
			return null;
		}
		_packageInstById[uIPackage.id] = uIPackage;
		_packageInstByName[uIPackage.name] = uIPackage;
		_packageList.Add(uIPackage);
		return uIPackage;
	}

	public static void RemovePackage(string packageIdOrName)
	{
		UIPackage value = null;
		if (!_packageInstById.TryGetValue(packageIdOrName, out value) && !_packageInstByName.TryGetValue(packageIdOrName, out value))
		{
			throw new Exception("FairyGUI: '" + packageIdOrName + "' is not a valid package id or name.");
		}
		value.Dispose();
		_packageInstById.Remove(value.id);
		if (value._customId != null)
		{
			_packageInstById.Remove(value._customId);
		}
		if (value._assetPath != null)
		{
			_packageInstById.Remove(value._assetPath);
		}
		_packageInstByName.Remove(value.name);
		_packageList.Remove(value);
	}

	public static void RemoveAllPackages()
	{
		if (_packageInstById.Count > 0)
		{
			UIPackage[] array = _packageList.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
		}
		_packageList.Clear();
		_packageInstById.Clear();
		_packageInstByName.Clear();
	}

	public static List<UIPackage> GetPackages()
	{
		return _packageList;
	}

	public static GObject CreateObject(string pkgName, string resName)
	{
		return GetByName(pkgName)?.CreateObject(resName);
	}

	public static GObject CreateObject(string pkgName, string resName, Type userClass)
	{
		return GetByName(pkgName)?.CreateObject(resName, userClass);
	}

	public static GObject CreateObjectFromURL(string url)
	{
		PackageItem itemByURL = GetItemByURL(url);
		return itemByURL?.owner.CreateObject(itemByURL, null);
	}

	public static GObject CreateObjectFromURL(string url, Type userClass)
	{
		PackageItem itemByURL = GetItemByURL(url);
		return itemByURL?.owner.CreateObject(itemByURL, userClass);
	}

	public static void CreateObjectAsync(string pkgName, string resName, CreateObjectCallback callback)
	{
		UIPackage byName = GetByName(pkgName);
		if (byName != null)
		{
			byName.CreateObjectAsync(resName, callback);
		}
		else
		{
			Debug.LogError("FairyGUI: package not found - " + pkgName);
		}
	}

	public static void CreateObjectFromURL(string url, CreateObjectCallback callback)
	{
		PackageItem itemByURL = GetItemByURL(url);
		if (itemByURL != null)
		{
			AsyncCreationHelper.CreateObject(itemByURL, callback);
		}
		else
		{
			Debug.LogError("FairyGUI: resource not found - " + url);
		}
	}

	public static object GetItemAsset(string pkgName, string resName)
	{
		return GetByName(pkgName)?.GetItemAsset(resName);
	}

	public static object GetItemAssetByURL(string url)
	{
		PackageItem itemByURL = GetItemByURL(url);
		return itemByURL?.owner.GetItemAsset(itemByURL);
	}

	public static string GetItemURL(string pkgName, string resName)
	{
		UIPackage byName = GetByName(pkgName);
		if (byName == null)
		{
			return null;
		}
		if (!byName._itemsByName.TryGetValue(resName, out var value))
		{
			return null;
		}
		return "ui://" + byName.id + value.id;
	}

	public static PackageItem GetItemByURL(string url)
	{
		if (url == null)
		{
			return null;
		}
		int num = url.IndexOf("//");
		if (num == -1)
		{
			return null;
		}
		int num2 = url.IndexOf('/', num + 2);
		if (num2 == -1)
		{
			if (url.Length > 13)
			{
				UIPackage byId = GetById(url.Substring(5, 8));
				if (byId != null)
				{
					string itemId = url.Substring(13);
					return byId.GetItem(itemId);
				}
			}
		}
		else
		{
			UIPackage byName = GetByName(url.Substring(num + 2, num2 - num - 2));
			if (byName != null)
			{
				string itemName = url.Substring(num2 + 1);
				return byName.GetItemByName(itemName);
			}
		}
		return null;
	}

	public static string NormalizeURL(string url)
	{
		if (url == null)
		{
			return null;
		}
		int num = url.IndexOf("//");
		if (num == -1)
		{
			return null;
		}
		int num2 = url.IndexOf('/', num + 2);
		if (num2 == -1)
		{
			return url;
		}
		string pkgName = url.Substring(num + 2, num2 - num - 2);
		string resName = url.Substring(num2 + 1);
		return GetItemURL(pkgName, resName);
	}

	public static void SetStringsSource(XML source)
	{
		TranslationHelper.LoadFromXML(source);
	}

	private bool LoadPackage(ByteBuffer buffer, string assetNamePrefix)
	{
		if (buffer.ReadUint() != 1179080009)
		{
			if (Application.isPlaying)
			{
				throw new Exception("FairyGUI: old package format found in '" + assetNamePrefix + "'");
			}
			Debug.LogWarning("FairyGUI: old package format found in '" + assetNamePrefix + "'");
			return false;
		}
		buffer.version = buffer.ReadInt();
		bool flag = buffer.version >= 2;
		buffer.ReadBool();
		id = buffer.ReadString();
		name = buffer.ReadString();
		if (_packageInstById.TryGetValue(id, out var value) && name != value.name)
		{
			Debug.LogWarning("FairyGUI: Package conflicts, '" + name + "' and '" + value.name + "'");
		}
		buffer.Skip(20);
		int position = buffer.position;
		buffer.Seek(position, 4);
		int num = buffer.ReadInt();
		string[] array = new string[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = buffer.ReadString();
		}
		buffer.stringTable = array;
		if (buffer.Seek(position, 5))
		{
			num = buffer.ReadInt();
			for (int j = 0; j < num; j++)
			{
				int num2 = buffer.ReadUshort();
				int len = buffer.ReadInt();
				array[num2] = buffer.ReadString(len);
			}
		}
		buffer.Seek(position, 0);
		num = buffer.ReadShort();
		_dependencies = new Dictionary<string, string>[num];
		for (int k = 0; k < num; k++)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("id", buffer.ReadS());
			dictionary.Add("name", buffer.ReadS());
			_dependencies[k] = dictionary;
		}
		bool flag2 = false;
		if (flag)
		{
			num = buffer.ReadShort();
			if (num > 0)
			{
				_branches = buffer.ReadSArray(num);
				if (!string.IsNullOrEmpty(_branch))
				{
					_branchIndex = Array.IndexOf(_branches, _branch);
				}
			}
			flag2 = num > 0;
		}
		buffer.Seek(position, 1);
		string text;
		if (assetNamePrefix.Length > 0)
		{
			text = Path.GetDirectoryName(assetNamePrefix);
			if (text.Length > 0)
			{
				text += "/";
			}
			assetNamePrefix += "_";
		}
		else
		{
			text = string.Empty;
		}
		num = buffer.ReadShort();
		PackageItem value2;
		for (int l = 0; l < num; l++)
		{
			int num3 = buffer.ReadInt();
			num3 += buffer.position;
			value2 = new PackageItem();
			value2.owner = this;
			value2.type = (PackageItemType)buffer.ReadByte();
			value2.id = buffer.ReadS();
			value2.name = buffer.ReadS();
			buffer.ReadS();
			value2.file = buffer.ReadS();
			value2.exported = buffer.ReadBool();
			value2.width = buffer.ReadInt();
			value2.height = buffer.ReadInt();
			switch (value2.type)
			{
			case PackageItemType.Image:
				value2.objectType = ObjectType.Image;
				switch (buffer.ReadByte())
				{
				case 1:
				{
					Rect value3 = new Rect
					{
						x = buffer.ReadInt(),
						y = buffer.ReadInt(),
						width = buffer.ReadInt(),
						height = buffer.ReadInt()
					};
					value2.scale9Grid = value3;
					value2.tileGridIndice = buffer.ReadInt();
					break;
				}
				case 2:
					value2.scaleByTile = true;
					break;
				}
				buffer.ReadBool();
				break;
			case PackageItemType.MovieClip:
				buffer.ReadBool();
				value2.objectType = ObjectType.MovieClip;
				value2.rawData = buffer.ReadBuffer();
				break;
			case PackageItemType.Font:
				value2.rawData = buffer.ReadBuffer();
				break;
			case PackageItemType.Component:
			{
				int num4 = buffer.ReadByte();
				if (num4 > 0)
				{
					value2.objectType = (ObjectType)num4;
				}
				else
				{
					value2.objectType = ObjectType.Component;
				}
				value2.rawData = buffer.ReadBuffer();
				UIObjectFactory.ResolvePackageItemExtension(value2);
				break;
			}
			case PackageItemType.Sound:
			case PackageItemType.Atlas:
			case PackageItemType.Misc:
				value2.file = assetNamePrefix + value2.file;
				break;
			case PackageItemType.Spine:
			case PackageItemType.DragoneBones:
				value2.file = text + value2.file;
				value2.skeletonAnchor.x = buffer.ReadFloat();
				value2.skeletonAnchor.y = buffer.ReadFloat();
				break;
			}
			if (flag)
			{
				string text2 = buffer.ReadS();
				if (text2 != null)
				{
					value2.name = text2 + "/" + value2.name;
				}
				int num5 = buffer.ReadByte();
				if (num5 > 0)
				{
					if (flag2)
					{
						value2.branches = buffer.ReadSArray(num5);
					}
					else
					{
						_itemsById[buffer.ReadS()] = value2;
					}
				}
				int num6 = buffer.ReadByte();
				if (num6 > 0)
				{
					value2.highResolution = buffer.ReadSArray(num6);
				}
			}
			_items.Add(value2);
			_itemsById[value2.id] = value2;
			if (value2.name != null)
			{
				_itemsByName[value2.name] = value2;
			}
			buffer.position = num3;
		}
		buffer.Seek(position, 2);
		num = buffer.ReadShort();
		for (int m = 0; m < num; m++)
		{
			int num7 = buffer.ReadShort();
			num7 += buffer.position;
			string key = buffer.ReadS();
			value2 = _itemsById[buffer.ReadS()];
			AtlasSprite atlasSprite = new AtlasSprite();
			atlasSprite.atlas = value2;
			atlasSprite.rect.x = buffer.ReadInt();
			atlasSprite.rect.y = buffer.ReadInt();
			atlasSprite.rect.width = buffer.ReadInt();
			atlasSprite.rect.height = buffer.ReadInt();
			atlasSprite.rotated = buffer.ReadBool();
			if (flag && buffer.ReadBool())
			{
				atlasSprite.offset.x = buffer.ReadInt();
				atlasSprite.offset.y = buffer.ReadInt();
				atlasSprite.originalSize.x = buffer.ReadInt();
				atlasSprite.originalSize.y = buffer.ReadInt();
			}
			else if (atlasSprite.rotated)
			{
				atlasSprite.originalSize.x = atlasSprite.rect.height;
				atlasSprite.originalSize.y = atlasSprite.rect.width;
			}
			else
			{
				atlasSprite.originalSize.x = atlasSprite.rect.width;
				atlasSprite.originalSize.y = atlasSprite.rect.height;
			}
			_sprites[key] = atlasSprite;
			buffer.position = num7;
		}
		if (buffer.Seek(position, 3))
		{
			num = buffer.ReadShort();
			for (int n = 0; n < num; n++)
			{
				int num8 = buffer.ReadInt();
				num8 += buffer.position;
				if (_itemsById.TryGetValue(buffer.ReadS(), out value2) && value2.type == PackageItemType.Image)
				{
					value2.pixelHitTestData = new PixelHitTestData();
					value2.pixelHitTestData.Load(buffer);
				}
				buffer.position = num8;
			}
		}
		if (!Application.isPlaying)
		{
			_items.Sort(ComparePackageItem);
		}
		return true;
	}

	private static int ComparePackageItem(PackageItem p1, PackageItem p2)
	{
		if (p1.name != null && p2.name != null)
		{
			return p1.name.CompareTo(p2.name);
		}
		return 0;
	}

	public void LoadAllAssets()
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			GetItemAsset(_items[i]);
		}
	}

	public void UnloadAssets()
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			PackageItem packageItem = _items[i];
			if (packageItem.type == PackageItemType.Atlas)
			{
				if (packageItem.texture != null)
				{
					packageItem.texture.Unload();
				}
			}
			else if (packageItem.type == PackageItemType.Sound && packageItem.audioClip != null)
			{
				packageItem.audioClip.Unload();
			}
		}
		if (unloadBundleByFGUI && _resBundle != null)
		{
			_resBundle.Unload(unloadAllLoadedObjects: true);
			_resBundle = null;
		}
	}

	public void ReloadAssets()
	{
		if (_fromBundle)
		{
			throw new Exception("FairyGUI: new bundle must be passed to this function");
		}
		ReloadAssets(null);
	}

	public void ReloadAssets(AssetBundle resBundle)
	{
		_resBundle = resBundle;
		_fromBundle = _resBundle != null;
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			PackageItem packageItem = _items[i];
			if (packageItem.type == PackageItemType.Atlas)
			{
				if (packageItem.texture != null && packageItem.texture.nativeTexture == null)
				{
					LoadAtlas(packageItem);
				}
			}
			else if (packageItem.type == PackageItemType.Sound && packageItem.audioClip != null && (UnityEngine.Object)(object)packageItem.audioClip.nativeClip == null)
			{
				LoadSound(packageItem);
			}
		}
	}

	private void Dispose()
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			PackageItem packageItem = _items[i];
			if (packageItem.type == PackageItemType.Atlas)
			{
				if (packageItem.texture != null)
				{
					packageItem.texture.Dispose();
					packageItem.texture = null;
				}
			}
			else if (packageItem.type == PackageItemType.Sound && packageItem.audioClip != null)
			{
				packageItem.audioClip.Unload();
				packageItem.audioClip = null;
			}
		}
		_items.Clear();
		_textures.Clear();
		if (unloadBundleByFGUI && _resBundle != null)
		{
			_resBundle.Unload(unloadAllLoadedObjects: true);
			_resBundle = null;
		}
	}

	public GObject CreateObject(string resName)
	{
		if (!_itemsByName.TryGetValue(resName, out var value))
		{
			Debug.LogError("FairyGUI: resource not found - " + resName + " in " + name);
			return null;
		}
		return CreateObject(value, null);
	}

	public GObject CreateObject(string resName, Type userClass)
	{
		if (!_itemsByName.TryGetValue(resName, out var value))
		{
			Debug.LogError("FairyGUI: resource not found - " + resName + " in " + name);
			return null;
		}
		return CreateObject(value, userClass);
	}

	public void CreateObjectAsync(string resName, CreateObjectCallback callback)
	{
		if (!_itemsByName.TryGetValue(resName, out var value))
		{
			Debug.LogError("FairyGUI: resource not found - " + resName + " in " + name);
		}
		else
		{
			AsyncCreationHelper.CreateObject(value, callback);
		}
	}

	private GObject CreateObject(PackageItem item, Type userClass)
	{
		Stats.LatestObjectCreation = 0;
		Stats.LatestGraphicsCreation = 0;
		GetItemAsset(item);
		GObject gObject = UIObjectFactory.NewObject(item, userClass);
		if (gObject == null)
		{
			return null;
		}
		_constructing++;
		gObject.ConstructFromResource();
		_constructing--;
		return gObject;
	}

	public object GetItemAsset(string resName)
	{
		if (!_itemsByName.TryGetValue(resName, out var value))
		{
			Debug.LogError("FairyGUI: Resource not found - " + resName + " in " + name);
			return null;
		}
		return GetItemAsset(value);
	}

	public List<PackageItem> GetItems()
	{
		return _items;
	}

	public PackageItem GetItem(string itemId)
	{
		if (_itemsById.TryGetValue(itemId, out var value))
		{
			return value;
		}
		return null;
	}

	public PackageItem GetItemByName(string itemName)
	{
		if (_itemsByName.TryGetValue(itemName, out var value))
		{
			return value;
		}
		return null;
	}

	public object GetItemAsset(PackageItem item)
	{
		switch (item.type)
		{
		case PackageItemType.Image:
			if (item.texture == null)
			{
				LoadImage(item);
			}
			return item.texture;
		case PackageItemType.Atlas:
			if (item.texture == null)
			{
				LoadAtlas(item);
			}
			return item.texture;
		case PackageItemType.Sound:
			if (item.audioClip == null)
			{
				LoadSound(item);
			}
			return item.audioClip;
		case PackageItemType.Font:
			if (item.bitmapFont == null)
			{
				LoadFont(item);
			}
			return item.bitmapFont;
		case PackageItemType.MovieClip:
			if (item.frames == null)
			{
				LoadMovieClip(item);
			}
			return item.frames;
		case PackageItemType.Component:
			return item.rawData;
		case PackageItemType.Misc:
			return LoadBinary(item);
		case PackageItemType.Spine:
			if (item.skeletonAsset == null)
			{
				LoadSpine(item);
			}
			return item.skeletonAsset;
		case PackageItemType.DragoneBones:
			if (item.skeletonAsset == null)
			{
				LoadDragonBones(item);
			}
			return item.skeletonAsset;
		default:
			return null;
		}
	}

	public void SetItemAsset(PackageItem item, object asset, DestroyMethod destroyMethod)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		PackageItemType type = item.type;
		if (type <= PackageItemType.Atlas)
		{
			switch (type)
			{
			case PackageItemType.Atlas:
				if (item.texture == null)
				{
					item.texture = new NTexture(null, new Rect(0f, 0f, item.width, item.height));
				}
				item.texture.Reload((Texture)asset, null);
				item.texture.destroyMethod = destroyMethod;
				break;
			case PackageItemType.Sound:
				if (item.audioClip == null)
				{
					item.audioClip = new NAudioClip(null);
				}
				item.audioClip.Reload((AudioClip)asset);
				item.audioClip.destroyMethod = destroyMethod;
				break;
			}
		}
		else if (type != PackageItemType.Spine)
		{
			_ = 10;
		}
	}

	private void LoadAtlas(PackageItem item)
	{
		string extension = Path.GetExtension(item.file);
		string text = item.file.Substring(0, item.file.Length - extension.Length);
		if (_loadAsyncFunc != null)
		{
			_loadAsyncFunc(text, extension, typeof(Texture), item);
			if (item.texture == null)
			{
				item.texture = new NTexture(null, new Rect(0f, 0f, item.width, item.height));
			}
			item.texture.destroyMethod = DestroyMethod.None;
			return;
		}
		Texture texture = null;
		Texture alphaTexture = null;
		DestroyMethod destroyMethod;
		if (_fromBundle)
		{
			if (_resBundle != null)
			{
				texture = _resBundle.LoadAsset<Texture>(text);
			}
			else
			{
				Debug.LogWarning("FairyGUI: bundle already unloaded.");
			}
			destroyMethod = DestroyMethod.None;
		}
		else
		{
			texture = (Texture)_loadFunc(text, extension, typeof(Texture), out destroyMethod);
		}
		if (texture == null)
		{
			Debug.LogWarning("FairyGUI: texture '" + item.file + "' not found in " + name);
		}
		else if (!(texture is Texture2D))
		{
			Debug.LogWarning("FairyGUI: settings for '" + item.file + "' is wrong! Correct values are: (Texture Type=Default, Texture Shape=2D)");
			texture = null;
		}
		else if (((Texture2D)texture).mipmapCount > 1)
		{
			Debug.LogWarning("FairyGUI: settings for '" + item.file + "' is wrong! Correct values are: (Generate Mip Maps=unchecked)");
		}
		if (texture != null)
		{
			text += "!a";
			if (_fromBundle)
			{
				if (_resBundle != null)
				{
					alphaTexture = _resBundle.LoadAsset<Texture2D>(text);
				}
			}
			else
			{
				alphaTexture = (Texture2D)_loadFunc(text, extension, typeof(Texture2D), out destroyMethod);
			}
		}
		if (texture == null)
		{
			texture = NTexture.CreateEmptyTexture();
			destroyMethod = DestroyMethod.Destroy;
		}
		if (item.texture == null)
		{
			item.texture = new NTexture(texture, alphaTexture, (float)texture.width / (float)item.width, (float)texture.height / (float)item.height);
			item.texture.onRelease += delegate
			{
				if (UIPackage.onReleaseResource != null)
				{
					UIPackage.onReleaseResource(item);
				}
			};
		}
		else
		{
			item.texture.Reload(texture, alphaTexture);
		}
		item.texture.destroyMethod = destroyMethod;
	}

	private void LoadImage(PackageItem item)
	{
		if (_sprites.TryGetValue(item.id, out var value))
		{
			NTexture nTexture = (NTexture)GetItemAsset(value.atlas);
			if ((float)nTexture.width == value.rect.width && (float)nTexture.height == value.rect.height)
			{
				item.texture = nTexture;
			}
			else
			{
				item.texture = new NTexture(nTexture, value.rect, value.rotated, value.originalSize, value.offset);
			}
		}
		else
		{
			item.texture = NTexture.Empty;
		}
	}

	private void LoadSound(PackageItem item)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		string extension = Path.GetExtension(item.file);
		string text = item.file.Substring(0, item.file.Length - extension.Length);
		if (_loadAsyncFunc != null)
		{
			_loadAsyncFunc(text, extension, typeof(AudioClip), item);
			if (item.audioClip == null)
			{
				item.audioClip = new NAudioClip(null);
			}
			item.audioClip.destroyMethod = DestroyMethod.None;
			return;
		}
		AudioClip audioClip = null;
		DestroyMethod destroyMethod;
		if (_fromBundle)
		{
			if (_resBundle != null)
			{
				audioClip = _resBundle.LoadAsset<AudioClip>(text);
			}
			destroyMethod = DestroyMethod.None;
		}
		else
		{
			audioClip = (AudioClip)_loadFunc(text, extension, typeof(AudioClip), out destroyMethod);
		}
		if (item.audioClip == null)
		{
			item.audioClip = new NAudioClip(audioClip);
		}
		else
		{
			item.audioClip.Reload(audioClip);
		}
		item.audioClip.destroyMethod = destroyMethod;
	}

	private byte[] LoadBinary(PackageItem item)
	{
		string extension = Path.GetExtension(item.file);
		string text = item.file.Substring(0, item.file.Length - extension.Length);
		if (_resBundle != null)
		{
			TextAsset textAsset = _resBundle.LoadAsset<TextAsset>(text);
			if (textAsset != null)
			{
				return textAsset.bytes;
			}
			return null;
		}
		DestroyMethod destroyMethod;
		object obj = _loadFunc(text, extension, typeof(TextAsset), out destroyMethod);
		if (obj == null)
		{
			return null;
		}
		if (obj is byte[])
		{
			return (byte[])obj;
		}
		return ((TextAsset)obj).bytes;
	}

	private void LoadMovieClip(PackageItem item)
	{
		ByteBuffer rawData = item.rawData;
		rawData.Seek(0, 0);
		item.interval = (float)rawData.ReadInt() / 1000f;
		item.swing = rawData.ReadBool();
		item.repeatDelay = (float)rawData.ReadInt() / 1000f;
		rawData.Seek(0, 1);
		int num = rawData.ReadShort();
		item.frames = new MovieClip.Frame[num];
		Rect rect = default(Rect);
		for (int i = 0; i < num; i++)
		{
			int num2 = rawData.ReadShort();
			num2 += rawData.position;
			MovieClip.Frame frame = new MovieClip.Frame();
			rect.x = rawData.ReadInt();
			rect.y = rawData.ReadInt();
			rect.width = rawData.ReadInt();
			rect.height = rawData.ReadInt();
			frame.addDelay = (float)rawData.ReadInt() / 1000f;
			string text = rawData.ReadS();
			if (text != null && _sprites.TryGetValue(text, out var value))
			{
				frame.texture = new NTexture((NTexture)GetItemAsset(value.atlas), value.rect, value.rotated, new Vector2(item.width, item.height), rect.position);
			}
			item.frames[i] = frame;
			rawData.position = num2;
		}
	}

	private void LoadFont(PackageItem item)
	{
		BitmapFont bitmapFont = new BitmapFont();
		bitmapFont.name = "ui://" + id + item.id;
		item.bitmapFont = bitmapFont;
		ByteBuffer rawData = item.rawData;
		rawData.Seek(0, 0);
		bool flag = rawData.ReadBool();
		bitmapFont.canTint = rawData.ReadBool();
		bitmapFont.resizable = rawData.ReadBool();
		bitmapFont.hasChannel = rawData.ReadBool();
		int num = rawData.ReadInt();
		int num2 = rawData.ReadInt();
		int lineHeight = rawData.ReadInt();
		float num3 = 1f;
		float num4 = 1f;
		NTexture nTexture = null;
		AtlasSprite value = null;
		if (flag && _sprites.TryGetValue(item.id, out value))
		{
			nTexture = (NTexture)GetItemAsset(value.atlas);
			num3 = nTexture.root.uvRect.width / (float)nTexture.width;
			num4 = nTexture.root.uvRect.height / (float)nTexture.height;
		}
		rawData.Seek(0, 1);
		int num5 = rawData.ReadInt();
		for (int i = 0; i < num5; i++)
		{
			int num6 = rawData.ReadShort();
			num6 += rawData.position;
			BitmapFont.BMGlyph bMGlyph = new BitmapFont.BMGlyph();
			char ch = rawData.ReadChar();
			bitmapFont.AddChar(ch, bMGlyph);
			string key = rawData.ReadS();
			int num7 = rawData.ReadInt();
			int num8 = rawData.ReadInt();
			int num9 = rawData.ReadInt();
			int num10 = rawData.ReadInt();
			int num11 = rawData.ReadInt();
			int num12 = rawData.ReadInt();
			bMGlyph.advance = rawData.ReadInt();
			bMGlyph.channel = rawData.ReadByte();
			if (bMGlyph.channel == 1)
			{
				bMGlyph.channel = 2;
			}
			else if (bMGlyph.channel == 2)
			{
				bMGlyph.channel = 1;
			}
			else if (bMGlyph.channel == 4)
			{
				bMGlyph.channel = 0;
			}
			else if (bMGlyph.channel == 8)
			{
				bMGlyph.channel = 3;
			}
			if (flag)
			{
				if (value.rotated)
				{
					bMGlyph.uv[0] = new Vector2(((float)(num8 + num12) + value.rect.x) * num3, 1f - (value.rect.yMax - (float)num7) * num4);
					bMGlyph.uv[1] = new Vector2(bMGlyph.uv[0].x - (float)num12 * num3, bMGlyph.uv[0].y);
					bMGlyph.uv[2] = new Vector2(bMGlyph.uv[1].x, bMGlyph.uv[0].y + (float)num11 * num4);
					bMGlyph.uv[3] = new Vector2(bMGlyph.uv[0].x, bMGlyph.uv[2].y);
				}
				else
				{
					bMGlyph.uv[0] = new Vector2(((float)num7 + value.rect.x) * num3, 1f - ((float)(num8 + num12) + value.rect.y) * num4);
					bMGlyph.uv[1] = new Vector2(bMGlyph.uv[0].x, bMGlyph.uv[0].y + (float)num12 * num4);
					bMGlyph.uv[2] = new Vector2(bMGlyph.uv[0].x + (float)num11 * num3, bMGlyph.uv[1].y);
					bMGlyph.uv[3] = new Vector2(bMGlyph.uv[2].x, bMGlyph.uv[0].y);
				}
				bMGlyph.lineHeight = lineHeight;
				bMGlyph.x = num9;
				bMGlyph.y = num10;
				bMGlyph.width = num11;
				bMGlyph.height = num12;
			}
			else
			{
				if (_itemsById.TryGetValue(key, out var value2))
				{
					value2 = value2.getBranch();
					num11 = value2.width;
					num12 = value2.height;
					value2 = value2.getHighResolution();
					GetItemAsset(value2);
					value2.texture.GetUV(bMGlyph.uv);
					num3 = (float)num11 / (float)value2.width;
					num4 = (float)num12 / (float)value2.height;
					bMGlyph.x = (float)num9 + value2.texture.offset.x * num3;
					bMGlyph.y = (float)num10 + value2.texture.offset.y * num4;
					bMGlyph.width = (float)value2.texture.width * num3;
					bMGlyph.height = (float)value2.texture.height * num4;
					if (nTexture == null)
					{
						nTexture = value2.texture.root;
					}
				}
				if (num == 0)
				{
					num = num12;
				}
				if (bMGlyph.advance == 0)
				{
					if (num2 == 0)
					{
						bMGlyph.advance = num9 + num11;
					}
					else
					{
						bMGlyph.advance = num2;
					}
				}
				bMGlyph.lineHeight = ((num10 < 0) ? num12 : (num10 + num12));
				if (bMGlyph.lineHeight < num)
				{
					bMGlyph.lineHeight = num;
				}
			}
			rawData.position = num6;
		}
		bitmapFont.size = num;
		bitmapFont.mainTexture = nTexture;
		if (!bitmapFont.hasChannel)
		{
			bitmapFont.shader = ShaderConfig.imageShader;
		}
	}

	private void LoadSpine(PackageItem item)
	{
		Debug.LogWarning("To enable Spine support, add script define symbol: FAIRYGUI_SPINE");
	}

	private void LoadDragonBones(PackageItem item)
	{
		Debug.LogWarning("To enable DragonBones support, add script define symbol: FAIRYGUI_DRAGONBONES");
	}
}

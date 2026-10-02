using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

[AddComponentMenu("FairyGUI/UI Config")]
public class UIConfig : MonoBehaviour
{
	public enum ConfigKey
	{
		DefaultFont = 0,
		ButtonSound = 1,
		ButtonSoundVolumeScale = 2,
		HorizontalScrollBar = 3,
		VerticalScrollBar = 4,
		DefaultScrollStep = 5,
		DefaultScrollBarDisplay = 6,
		DefaultScrollTouchEffect = 7,
		DefaultScrollBounceEffect = 8,
		TouchScrollSensitivity = 9,
		WindowModalWaiting = 10,
		GlobalModalWaiting = 11,
		PopupMenu = 12,
		PopupMenu_seperator = 13,
		LoaderErrorSign = 14,
		TooltipsWin = 15,
		DefaultComboBoxVisibleItemCount = 16,
		TouchDragSensitivity = 17,
		ClickDragSensitivity = 18,
		ModalLayerColor = 19,
		RenderingTextBrighterOnDesktop = 20,
		AllowSoftnessOnTopOrLeftSide = 21,
		InputCaretSize = 22,
		InputHighlightColor = 23,
		EnhancedTextOutlineEffect = 24,
		DepthSupportForPaintingMode = 25,
		RichTextRowVerticalAlign = 26,
		Branch = 27,
		PleaseSelect = 100
	}

	[Serializable]
	public class ConfigValue
	{
		public bool valid;

		public string s;

		public int i;

		public float f;

		public bool b;

		public Color c;

		public void Reset()
		{
			valid = false;
			s = null;
			i = 0;
			f = 0f;
			b = false;
			c = Color.black;
		}
	}

	public delegate NAudioClip SoundLoader(string url);

	public static string defaultFont = "JingNanBoBoHei";

	[Obsolete("No use anymore")]
	public static bool renderingTextBrighterOnDesktop = true;

	public static string windowModalWaiting;

	public static string globalModalWaiting;

	public static Color modalLayerColor = new Color(0f, 0f, 0f, 0.4f);

	public static NAudioClip buttonSound;

	public static float buttonSoundVolumeScale = 1f;

	public static string horizontalScrollBar;

	public static string verticalScrollBar;

	public static float defaultScrollStep = 20f;

	public static float defaultScrollDecelerationRate = 0.967f;

	public static ScrollBarDisplayType defaultScrollBarDisplay = ScrollBarDisplayType.Default;

	public static bool defaultScrollTouchEffect = true;

	public static bool defaultScrollBounceEffect = true;

	public static string popupMenu;

	public static string popupMenu_seperator;

	public static string loaderErrorSign;

	public static string tooltipsWin;

	public static int defaultComboBoxVisibleItemCount = 10;

	public static int touchScrollSensitivity = 20;

	public static int touchDragSensitivity = 10;

	public static int clickDragSensitivity = 2;

	public static bool allowSoftnessOnTopOrLeftSide = true;

	public static bool bringWindowToFrontOnClick = true;

	public static float inputCaretSize = 1f;

	public static Color inputHighlightColor = new Color32(byte.MaxValue, 223, 141, 128);

	public static float frameTimeForAsyncUIConstruction = 0.002f;

	public static bool depthSupportForPaintingMode = true;

	public static bool enhancedTextOutlineEffect = false;

	[Obsolete("No use anymore.")]
	public static VertAlignType richTextRowVerticalAlign = VertAlignType.Bottom;

	public static bool makePixelPerfect = false;

	public List<ConfigValue> Items = new List<ConfigValue>();

	public List<string> PreloadPackages = new List<string>();

	public static SoundLoader soundLoader = null;

	private void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		foreach (string preloadPackage in PreloadPackages)
		{
			UIPackage.AddPackage(preloadPackage);
		}
		Load();
	}

	public void Load()
	{
		int count = Items.Count;
		for (int i = 0; i < count; i++)
		{
			ConfigValue configValue = Items[i];
			if (!configValue.valid)
			{
				continue;
			}
			switch ((ConfigKey)i)
			{
			case ConfigKey.ButtonSound:
				if (Application.isPlaying)
				{
					buttonSound = UIPackage.GetItemAssetByURL(configValue.s) as NAudioClip;
				}
				break;
			case ConfigKey.ButtonSoundVolumeScale:
				buttonSoundVolumeScale = configValue.f;
				break;
			case ConfigKey.ClickDragSensitivity:
				clickDragSensitivity = configValue.i;
				break;
			case ConfigKey.DefaultComboBoxVisibleItemCount:
				defaultComboBoxVisibleItemCount = configValue.i;
				break;
			case ConfigKey.DefaultFont:
				defaultFont = configValue.s;
				break;
			case ConfigKey.DefaultScrollBarDisplay:
				defaultScrollBarDisplay = (ScrollBarDisplayType)configValue.i;
				break;
			case ConfigKey.DefaultScrollBounceEffect:
				defaultScrollBounceEffect = configValue.b;
				break;
			case ConfigKey.DefaultScrollStep:
				defaultScrollStep = configValue.i;
				break;
			case ConfigKey.DefaultScrollTouchEffect:
				defaultScrollTouchEffect = configValue.b;
				break;
			case ConfigKey.GlobalModalWaiting:
				globalModalWaiting = configValue.s;
				break;
			case ConfigKey.HorizontalScrollBar:
				horizontalScrollBar = configValue.s;
				break;
			case ConfigKey.LoaderErrorSign:
				loaderErrorSign = configValue.s;
				break;
			case ConfigKey.ModalLayerColor:
				modalLayerColor = configValue.c;
				break;
			case ConfigKey.PopupMenu:
				popupMenu = configValue.s;
				break;
			case ConfigKey.PopupMenu_seperator:
				popupMenu_seperator = configValue.s;
				break;
			case ConfigKey.TooltipsWin:
				tooltipsWin = configValue.s;
				break;
			case ConfigKey.TouchDragSensitivity:
				touchDragSensitivity = configValue.i;
				break;
			case ConfigKey.TouchScrollSensitivity:
				touchScrollSensitivity = configValue.i;
				break;
			case ConfigKey.VerticalScrollBar:
				verticalScrollBar = configValue.s;
				break;
			case ConfigKey.WindowModalWaiting:
				windowModalWaiting = configValue.s;
				break;
			case ConfigKey.AllowSoftnessOnTopOrLeftSide:
				allowSoftnessOnTopOrLeftSide = configValue.b;
				break;
			case ConfigKey.InputCaretSize:
				inputCaretSize = configValue.i;
				break;
			case ConfigKey.InputHighlightColor:
				inputHighlightColor = configValue.c;
				break;
			case ConfigKey.DepthSupportForPaintingMode:
				depthSupportForPaintingMode = configValue.b;
				break;
			case ConfigKey.EnhancedTextOutlineEffect:
				enhancedTextOutlineEffect = configValue.b;
				break;
			case ConfigKey.Branch:
				UIPackage.branch = configValue.s;
				break;
			}
		}
	}

	public static void SetDefaultValue(ConfigKey key, ConfigValue value)
	{
		switch (key)
		{
		case ConfigKey.ButtonSoundVolumeScale:
			value.f = 1f;
			break;
		case ConfigKey.ClickDragSensitivity:
			value.i = 2;
			break;
		case ConfigKey.DefaultComboBoxVisibleItemCount:
			value.i = 10;
			break;
		case ConfigKey.DefaultScrollBarDisplay:
			value.i = 0;
			break;
		case ConfigKey.DefaultScrollTouchEffect:
		case ConfigKey.DefaultScrollBounceEffect:
			value.b = true;
			break;
		case ConfigKey.DefaultScrollStep:
			value.i = 25;
			break;
		case ConfigKey.ModalLayerColor:
			value.c = new Color(0f, 0f, 0f, 0.4f);
			break;
		case ConfigKey.RenderingTextBrighterOnDesktop:
			value.b = true;
			break;
		case ConfigKey.TouchDragSensitivity:
			value.i = 10;
			break;
		case ConfigKey.TouchScrollSensitivity:
			value.i = 20;
			break;
		case ConfigKey.InputCaretSize:
			value.i = 1;
			break;
		case ConfigKey.InputHighlightColor:
			value.c = new Color32(byte.MaxValue, 223, 141, 128);
			break;
		case ConfigKey.DepthSupportForPaintingMode:
			value.b = false;
			break;
		case ConfigKey.Branch:
			value.s = "";
			break;
		case ConfigKey.HorizontalScrollBar:
		case ConfigKey.VerticalScrollBar:
		case ConfigKey.WindowModalWaiting:
		case ConfigKey.GlobalModalWaiting:
		case ConfigKey.PopupMenu:
		case ConfigKey.PopupMenu_seperator:
		case ConfigKey.LoaderErrorSign:
		case ConfigKey.TooltipsWin:
		case ConfigKey.AllowSoftnessOnTopOrLeftSide:
		case ConfigKey.EnhancedTextOutlineEffect:
		case ConfigKey.RichTextRowVerticalAlign:
			break;
		}
	}

	public static void ClearResourceRefs()
	{
		defaultFont = "";
		buttonSound = null;
		globalModalWaiting = null;
		horizontalScrollBar = null;
		loaderErrorSign = null;
		popupMenu = null;
		popupMenu_seperator = null;
		tooltipsWin = null;
		verticalScrollBar = null;
		windowModalWaiting = null;
		UIPackage.branch = null;
	}

	public void ApplyModifiedProperties()
	{
		EMRenderSupport.Reload();
	}
}

using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public abstract class BaseWindow : Window, IUISource
{
	public bool isAdapter;

	private GTweener fadeTweener;

	private const float fadeDuration = 0.3f;

	private GLoader _bgLoader;

	public UIWindowConfigure config { get; }

	public string fileName => config.PackageName;

	public bool loaded { get; set; }

	public bool initialized { get; private set; }

	public Signal onClose { get; private set; }

	protected virtual bool isGeneralFadeIn => false;

	protected virtual bool isGeneralFadeOut => false;

	public GLoader BgLoader
	{
		get
		{
			if (_bgLoader == null)
			{
				_bgLoader = new GLoader();
				base.contentPane.AddChildAt(_bgLoader, 0);
				_bgLoader.MakeFullScreen();
				_bgLoader.AddRelation(base.contentPane, RelationType.Height);
				_bgLoader.AddRelation(base.contentPane, RelationType.Width);
				_bgLoader.touchable = false;
				_bgLoader.fill = FillType.ScaleFree;
			}
			return _bgLoader;
		}
	}

	protected BaseWindow(UIWindowType type)
	{
		initialized = false;
		isAdapter = false;
		config = UIHelper.GetWindowConfig(type);
		AddUISource(this);
		onClose = new Signal();
	}

	protected override void OnInit()
	{
		MakeAdapterScreen();
		base.sortingOrder = config.Layer;
		initialized = true;
	}

	private void MakeAdapterScreen()
	{
		if (!base.isDisposed)
		{
			if (isAdapter)
			{
				AddRelation(GRoot.inst, RelationType.Height);
				AddRelation(GRoot.inst, RelationType.Center_Center);
				int num = Mathf.CeilToInt(Screen.safeArea.width / GRoot.contentScaleFactor);
				SetSize(num, GRoot.inst.height);
			}
			else
			{
				SetSize(GRoot.inst.width, GRoot.inst.height);
				AddRelation(GRoot.inst, RelationType.Size);
			}
			Center();
		}
	}

	public virtual async void Load(UILoadCallback callback)
	{
		await UIPackage.AddPackageAsync(fileName, string.Empty);
		loaded = true;
		callback?.Invoke();
	}

	public override void Dispose()
	{
		if (initialized && !string.IsNullOrEmpty(fileName))
		{
			UIPackage.RemovePackage(fileName);
		}
		base.Dispose();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (SimpleSingletonProvider<UIManager>.inst.propUpWindows.TryPeek(out var result))
		{
			result.LoseFocus();
		}
		SimpleSingletonProvider<UIManager>.inst.propUpWindows.Push(this);
		ResumeFocus();
		Stage.inst.onStageResized.Add(OnStageResized);
		if (isGeneralFadeIn)
		{
			GeneralFadeIn();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (SimpleSingletonProvider<UIManager>.inst.propUpWindows.TryPeek(out var result) && result == this)
		{
			SimpleSingletonProvider<UIManager>.inst.propUpWindows.Pop();
			LoseFocus();
		}
		onClose.Dispatch();
		if (SimpleSingletonProvider<UIManager>.inst.propUpWindows.TryPeek(out var result2))
		{
			result2.ResumeFocus();
		}
		CommonUIManager.StopVideo(UIType.Window, (int)config.WindowType);
		Stage.inst.onStageResized.Remove(OnStageResized);
	}

	protected override void DoHideAnimation()
	{
		if (isGeneralFadeOut)
		{
			GeneralFadeOut();
		}
		else
		{
			HideImmediately();
		}
	}

	public void ResumeFocus()
	{
	}

	public void LoseFocus()
	{
	}

	protected virtual void OnKeyDown(EventContext context)
	{
	}

	protected virtual async void OnStageResized()
	{
		await UniTask.DelayFrame(2);
		MakeAdapterScreen();
	}

	public virtual bool TryHide()
	{
		Hide();
		return true;
	}

	protected void GeneralFadeOut()
	{
		if (fadeTweener != null && !fadeTweener._killed)
		{
			fadeTweener.Kill(complete: true);
			fadeTweener = null;
		}
		base.contentPane.touchableChildes = false;
		fadeTweener = base.contentPane.TweenFade(0f, 0.3f).OnComplete((GTweenCallback)delegate
		{
			base.contentPane.touchableChildes = true;
			base.contentPane.alpha = 1f;
			HideImmediately();
		});
	}

	protected void GeneralFadeIn()
	{
		if (fadeTweener != null && !fadeTweener._killed)
		{
			fadeTweener.Kill(complete: true);
			fadeTweener = null;
		}
		base.contentPane.alpha = 0f;
		base.contentPane.touchableChildes = false;
		fadeTweener = base.contentPane.TweenFade(1f, 0.3f).OnComplete((GTweenCallback)delegate
		{
			base.contentPane.touchableChildes = true;
		});
	}
}

using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;

namespace UI;

public class BattleHintWindow : BaseWindow
{
	public BattleHintWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattleHintWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIBattleHintWindow uIBattleHintWindow)
		{
			uIBattleHintWindow.hintType.selectedIndex = 0;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
	}

	public async UniTask<bool> ShowDragonPlaceTreasure()
	{
		await TryShowAsync();
		if (!(base.contentPane is UIBattleHintWindow uIBattleHintWindow))
		{
			return false;
		}
		uIBattleHintWindow.hintType.selectedIndex = 1;
		uIBattleHintWindow.com_DragonPalaceTreasure.txt_BattleInfo.text = 11024.GetLocal(UIStringType.Message);
		Stage.inst.PlayOneShotSound(10);
		uIBattleHintWindow.com_DragonPalaceTreasure.Cut_in.Play();
		bool result = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
		CloseHint();
		return result;
	}

	public void CloseHint()
	{
		SimpleSingletonProvider<UIManager>.inst.UnLoadBattleHint();
	}
}

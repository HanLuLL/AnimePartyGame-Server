using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;

namespace UI;

public class BattlePreMonsterWindow : BattleMonsterWindow
{
	public BattlePreMonsterWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattlePreMonsterWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			ShowPopup();
		}
		await WaitInitialized();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIBattlePreMonsterWindow uIBattlePreMonsterWindow)
		{
			uIBattlePreMonsterWindow.txt_Title.text = 1000007.GetLocal(UIStringType.GUI);
		}
	}

	public async UniTask PreviewMonsterTarget(RepeatedField<long> _playerIds)
	{
		await TryShow();
		if (base.contentPane is UIBattlePreMonsterWindow uIBattlePreMonsterWindow)
		{
			RefreshMonster(uIBattlePreMonsterWindow.list_Monster, _playerIds, 0);
		}
	}
}

using System;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class RoomTermsWindow : BaseWindow
{
	private RepeatedField<int> _termIds;

	private Action _onHideCallback;

	public RoomTermsWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRoomTermsWindow.CreateInstance();
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
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			uIRoomTermsWindow.com_FullScreenTip.list_terms.itemRenderer = RendererTerm;
			uIRoomTermsWindow.com_WinScreen.list_terms.itemRenderer = RendererTerm;
			uIRoomTermsWindow.com_WinScreen.close_btn.onClick.Add(base.Hide);
			uIRoomTermsWindow.com_WinScreen.bg_btn.onClick.Add(base.Hide);
			uIRoomTermsWindow.com_AddTermWindow.btn_close.onClick.Add(base.Hide);
			uIRoomTermsWindow.com_WinScreen.btn_nextPage.onClick.Add(NextPage);
			uIRoomTermsWindow.com_WinScreen.btn_prePage.onClick.Add(PrePage);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			uIRoomTermsWindow.com_FullScreenTip.list_terms.itemRenderer = null;
			uIRoomTermsWindow.com_WinScreen.list_terms.itemRenderer = null;
			uIRoomTermsWindow.com_WinScreen.close_btn.onClick.Remove(base.Hide);
			uIRoomTermsWindow.com_WinScreen.bg_btn.onClick.Remove(base.Hide);
			uIRoomTermsWindow.com_AddTermWindow.btn_close.onClick.Remove(base.Hide);
			uIRoomTermsWindow.com_WinScreen.btn_nextPage.onClick.Remove(NextPage);
			uIRoomTermsWindow.com_WinScreen.btn_prePage.onClick.Remove(PrePage);
			_onHideCallback?.Invoke();
			_onHideCallback = null;
		}
	}

	private void NextPage()
	{
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			SetCurrentPage(uIRoomTermsWindow.com_WinScreen.list_terms.scrollPane.currentPageX + 1);
		}
	}

	private void PrePage()
	{
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			SetCurrentPage(uIRoomTermsWindow.com_WinScreen.list_terms.scrollPane.currentPageX - 1);
		}
	}

	private void RendererTerm(int index, GObject item)
	{
		RendererTermById(_termIds[index], item);
	}

	private void RendererTermById(int termId, GObject item)
	{
		if (item is UIRoomTerms_TermItem uIRoomTerms_TermItem)
		{
			MutatorInfoConfigure mutatorInfoConfigure = termId.GetMutatorInfoConfigure();
			if (mutatorInfoConfigure != null)
			{
				uIRoomTerms_TermItem.txt_name.text = mutatorInfoConfigure.NameID.GetLocal(UIStringType.Mutator);
				uIRoomTerms_TermItem.com_content.txt_content.text = mutatorInfoConfigure.DescId.GetLocal(UIStringType.Mutator);
				uIRoomTerms_TermItem.com_content.touchable = uIRoomTerms_TermItem.com_content.txt_content.height > uIRoomTerms_TermItem.com_content.height;
				uIRoomTerms_TermItem.mutatorType.selectedIndex = (int)mutatorInfoConfigure.MutatorType[0];
				uIRoomTerms_TermItem.img_termIcon.url = mutatorInfoConfigure.Icon;
			}
		}
	}

	public async UniTask ShowFullScreenTerms(RepeatedField<int> termIds)
	{
		await TryShowAsync();
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			uIRoomTermsWindow.showType.selectedIndex = 0;
			_termIds = termIds;
			uIRoomTermsWindow.com_FullScreenTip.list_terms.numItems = _termIds.Count;
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(StaticGlobalData.GAME_MUTATOR_SHOW_TIME);
			Hide();
		}
	}

	public async UniTask ShowWinScreenTerms(RepeatedField<int> termIds, Action onHideCallback)
	{
		await TryShowAsync();
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			uIRoomTermsWindow.showType.selectedIndex = 1;
			_termIds = termIds;
			uIRoomTermsWindow.com_WinScreen.list_terms.scrollPane.touchEffect = false;
			uIRoomTermsWindow.com_WinScreen.list_terms.numItems = _termIds.Count;
			SetCurrentPage(0, init: true);
			_onHideCallback = onHideCallback;
		}
	}

	public async UniTask ShowAddTremWindow(int termId)
	{
		await TryShowAsync();
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			uIRoomTermsWindow.showType.selectedIndex = 2;
			RendererTermById(termId, uIRoomTermsWindow.com_AddTermWindow.com_term);
			int num = termId.GetMutatorInfoConfigure().MutatorType[0] switch
			{
				MutatorType.VeryGood => 1100001, 
				MutatorType.Good => 1100001, 
				MutatorType.QuiteGood => 1100002, 
				MutatorType.Middle => 1100003, 
				MutatorType.QuiteBad => 1100004, 
				MutatorType.Bad => 1100005, 
				MutatorType.VeryBad => 1100005, 
				_ => 0, 
			};
			uIRoomTermsWindow.com_AddTermWindow.txt_desc.text = num.GetLocal(UIStringType.GUI);
		}
	}

	private void SetCurrentPage(int pageIndex, bool init = false)
	{
		if (base.contentPane is UIRoomTermsWindow uIRoomTermsWindow)
		{
			int num = (int)Math.Ceiling((float)uIRoomTermsWindow.com_WinScreen.list_terms.numItems / 4f);
			pageIndex = Math.Clamp(pageIndex, 0, num - 1);
			uIRoomTermsWindow.com_WinScreen.btn_nextPage.visible = pageIndex < num - 1;
			uIRoomTermsWindow.com_WinScreen.btn_prePage.visible = pageIndex > 0;
			uIRoomTermsWindow.com_WinScreen.list_terms.scrollPane.SetCurrentPageX(pageIndex, !init);
		}
	}
}

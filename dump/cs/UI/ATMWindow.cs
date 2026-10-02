using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class ATMWindow : BaseWindow
{
	private readonly List<BattlePlayerData> battlePlayers = new List<BattlePlayerData>();

	private UIATM_Button_RoleHeadshot selectedItem;

	private readonly List<UIATM_Button_RoleHeadshot> playerItems = new List<UIATM_Button_RoleHeadshot>();

	private Action CardShopAction => SimpleSingletonProvider<GameLogicManager>.inst.land.CardShopAction;

	private PVEShopBuyC2S PVEShopData => SimpleSingletonProvider<GameLogicManager>.inst.land.PVEShopData;

	public ATMWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIATMWindow.CreateInstance();
		base.OnInit();
	}

	private void OnHideControllerChange()
	{
		if (base.contentPane is UIATMWindow uIATMWindow)
		{
			base.BgLoader.visible = uIATMWindow.Hide.selectedIndex == 0;
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIATMWindow uIATMWindow)
		{
			uIATMWindow.Cut_in.Play();
			uIATMWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uIATMWindow.Hide.selectedIndex = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.BattlePlayer);
			uIATMWindow.btn_Leave.onClick.Add(OnRequestLeaveATM);
			uIATMWindow.btn_ATM.onClick.Add(OnRequestTransferATM);
			uIATMWindow.btn_Transfer.onClick.Add(TryOpenShop);
			uIATMWindow.list_Players.itemRenderer = RendererTargetPlayers;
			uIATMWindow.list_Players.onClickItem.Add(OnClickTargetPlayer);
			uIATMWindow.Hide.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIATMWindow uIATMWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.BattlePlayer);
			for (int i = 0; i < playerItems.Count; i++)
			{
				SimpleSingletonProvider<GameObjectManager>.inst.Stop(playerItems[i].effect);
			}
			uIATMWindow.btn_Leave.onClick.Remove(OnRequestLeaveATM);
			uIATMWindow.btn_ATM.onClick.Remove(OnRequestTransferATM);
			uIATMWindow.btn_Transfer.onClick.Remove(TryOpenShop);
			uIATMWindow.list_Players.onClickItem.Remove(OnClickTargetPlayer);
			uIATMWindow.Hide.onChanged.Remove(OnHideControllerChange);
		}
	}

	private async UniTask TryShow()
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

	public async UniTask ShowATM()
	{
		battlePlayers.Clear();
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].characterType == CharacterType.Hero && CardShopAction.PlayerId != playerDatas[i].player.Id)
			{
				battlePlayers.Add(playerDatas[i]);
			}
		}
		selectedItem = null;
		playerItems.Clear();
		await TryShow();
		if (base.contentPane is UIATMWindow uIATMWindow)
		{
			uIATMWindow.list_Players.numItems = battlePlayers.Count;
			uIATMWindow.txt_Token.text = PVEShopData.AssistGold.ToString();
			uIATMWindow.btn_ATM.touchable = PVEShopData.AssistPlayer == 0;
			uIATMWindow.btn_ATM.grayed = !uIATMWindow.btn_ATM.touchable;
			uIATMWindow.btn_Leave.onClick.Release();
			uIATMWindow.btn_ATM.onClick.Release();
		}
	}

	private void OnRequestLeaveATM()
	{
		if (CardShopAction != null && CardShopAction.Sn != 0L && base.contentPane is UIATMWindow uIATMWindow)
		{
			uIATMWindow.btn_Leave.onClick.Retain();
			uIATMWindow.btn_ATM.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestPVEShopBuyC2S(CardShopAction.Sn, new List<int>(), 0L, isClose: true);
		}
	}

	private void OnRequestTransferATM()
	{
		if (CardShopAction == null || CardShopAction.Sn == 0L || selectedItem == null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UIATMWindow win = gComponent as UIATMWindow;
		if (win != null)
		{
			win.btn_Leave.onClick.Retain();
			win.btn_ATM.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestPVEShopBuyC2S(CardShopAction.Sn, new List<int>(), battlePlayers[(int)selectedItem.data].player.Id).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_ATM.touchable = false;
				win.btn_ATM.grayed = !win.btn_ATM.touchable;
			});
		}
	}

	private async void RendererTargetPlayers(int index, GObject item)
	{
		if (item is UIATM_Button_RoleHeadshot _item)
		{
			BattlePlayerData playerdata = battlePlayers[index];
			((UICom_PlayerInfo)_item.com_playerInfo).RefreshData(playerdata);
			if (StaticConfigure.Effect.InfoDict.TryGetValue(28, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, _item.effect, 17f);
			}
			_item.selected = false;
			_item.effect.visible = false;
			_item.data = index;
			playerItems.Add(_item);
		}
	}

	private void OnClickTargetPlayer(EventContext context)
	{
		if (context.data is UIATM_Button_RoleHeadshot uIATM_Button_RoleHeadshot && (selectedItem == null || selectedItem != uIATM_Button_RoleHeadshot))
		{
			if (selectedItem != null)
			{
				selectedItem.effect.visible = false;
			}
			uIATM_Button_RoleHeadshot.effect.visible = true;
			selectedItem = uIATM_Button_RoleHeadshot;
		}
	}

	public async void TryOpenShop()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIATMWindow win)
		{
			win.btn_Transfer.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.landShop.ShowCardShop(1);
			Hide();
			win.btn_Transfer.onClick.Release();
		}
	}
}

using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;

namespace UI;

public class GameInfoWindow : BaseWindow
{
	public GameInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIGameInfoWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShow()
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
		if (base.contentPane is UIGameInfoWindow uIGameInfoWindow)
		{
			uIGameInfoWindow.showPing.selectedIndex = 1;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIGameInfoWindow uIGameInfoWindow)
		{
			uIGameInfoWindow.showPing.selectedIndex = 0;
		}
	}

	protected override void OnUpdate()
	{
		if (base.isShowing)
		{
			UpdatePing();
		}
		base.OnUpdate();
	}

	private void UpdatePing()
	{
		if (base.contentPane is UIGameInfoWindow uIGameInfoWindow)
		{
			uIGameInfoWindow.com_Ping.txt_Ping.text = MonoSingletonProvider<NetManager>.inst.PingTime.ToString();
			Controller ePingState = uIGameInfoWindow.com_Ping.ePingState;
			ePingState.selectedIndex = NetManager.currentPingState switch
			{
				NetManager.ePingState.GOOD => 0, 
				NetManager.ePingState.Normal => 1, 
				NetManager.ePingState.Worse => 2, 
				NetManager.ePingState.OutOfTime => 3, 
				_ => 3, 
			};
		}
	}
}

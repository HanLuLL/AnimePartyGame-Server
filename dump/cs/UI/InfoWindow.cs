using Cysharp.Threading.Tasks;
using Tools;

namespace UI;

public class InfoWindow : BaseWindow
{
	public InfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIInfoWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIInfoWindow uIInfoWindow)
		{
			uIInfoWindow.mohu.onClick.Add(base.Hide);
			uIInfoWindow.btn_Sure.onClick.Add(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIInfoWindow uIInfoWindow)
		{
			uIInfoWindow.mohu.onClick.Remove(base.Hide);
			uIInfoWindow.btn_Sure.onClick.Remove(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
		}
	}

	public async void ShowUserAgreements()
	{
		await TryShow();
		_ = base.contentPane is UIInfoWindow;
	}

	public async void ShowPrivacyPolicys()
	{
		await TryShow();
		_ = base.contentPane is UIInfoWindow;
	}

	public async void ShowPaymentServicesAct()
	{
		await TryShow();
		_ = base.contentPane is UIInfoWindow;
	}

	public async void ShowSpecifiedCommercialTransactions()
	{
		await TryShow();
		_ = base.contentPane is UIInfoWindow;
	}

	public async void ShowLoginAgeTip()
	{
		await TryShow();
		if (base.contentPane is UIInfoWindow uIInfoWindow)
		{
			uIInfoWindow.txt_Title.text = "《吉星派对》游戏适龄提示";
			uIInfoWindow.com_Content.title = "1、本游戏是一款二次元休闲派对类游戏，适用于年满12周岁及以上的用户，建议未成年人在家长监护下使用游戏产品。\n\n2、本游戏角色采用卡通形象，含有轻度竞技元素，但无暴力、血腥表现，仅存在角色失败后的滑稽退场特效。\n支持好友组队联机功能，设有基础交流功能，仅开放局内玩家快捷消息和表情交流，没有给予文字和语音的社\n交系统，游戏内配备关键词过滤机制，防止不当文字信息。\n\n3、游戏中有用户实名认证系统，认证为未成年人的用户将接受以下管理：\n未成年人在线时长限制（根据2021年8月通知调整）\n仅在周五、周六、周日和法定节假日每日20时至21时，向未成年人提供1小时游戏服务。其余时间未成年人\n将无法登录游戏。\n◆关闭游戏内游客账号登录功能，不再向未实名注册和登录的用户提供任何形式的游戏服务。\n未成年人付费服务限制（遵循2019年11月通知设置，无调整）\n为规范未成年人付费服务，对未成年人用户付费服务实行以下限制：\n8周岁以上未满16周岁的用户，单次充值金额不得超过50元人民币，每月充值金额累计不得超过200元人民币\n◆16周岁以上未满18周岁的用户，单次充值金额不得超过100元人民币，每月充值金额累计不得超过400元人民币\n\n4、本游戏以桌游派对为核心玩法，有助于锻炼玩家的独立思考和分析解决问题的能力。游戏玩法简单易懂，画面\n精美，致力于以出色的视听和游玩效果带给玩家积极愉悦的情绪体验，增强玩家的自信心。";
		}
	}
}

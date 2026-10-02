using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class ExpressionListWindow : BaseWindow
{
	private readonly List<BattleMessage> _messages = new List<BattleMessage>();

	public ExpressionListWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIExpressionListWindow.CreateInstance();
		base.OnInit();
		if (base.contentPane is UIExpressionListWindow uIExpressionListWindow)
		{
			uIExpressionListWindow.list_Plaform.x = uIExpressionListWindow.width - SimpleSingletonProvider<UIManager>.inst.expression.GetOpenExprWidth();
		}
	}

	public async UniTask TryShowAsync()
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
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.Signal.battleMessage.AddListener(OnBattleMessage);
		_ = base.contentPane is UIExpressionListWindow;
	}

	protected override void OnHide()
	{
		_messages.Clear();
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.Signal.battleMessage.RemoveListener(OnBattleMessage);
		base.OnHide();
		_ = base.contentPane is UIExpressionListWindow;
	}

	private void OnBattleMessage(BattleMessage msg)
	{
		AddMessage(msg);
	}

	public void AddChat(BattlePlayerData playerData, MessageType type, int messageId)
	{
		BattleMessage messageData = new BattleMessage(type, playerData, messageId);
		AddMessage(messageData);
	}

	public void AddMessage(BattleMessage messageData)
	{
		if (messageData != null)
		{
			_messages.Add(messageData);
			RefreshPlatform();
		}
	}

	private void RefreshPlatform()
	{
		if (!(base.contentPane is UIExpressionListWindow uIExpressionListWindow))
		{
			return;
		}
		int num = _messages.Count;
		int num2 = 0;
		while (num > 0)
		{
			num2 += _messages[num - 1].UIWeight;
			if (num2 > 8)
			{
				break;
			}
			num--;
		}
		_messages.RemoveRange(0, num);
		for (int i = 0; i < num; i++)
		{
			GObject gObject = uIExpressionListWindow.list_Plaform.RemoveChildAt(0);
			if (gObject is UIExpression_Com_ChatItem uIExpression_Com_ChatItem)
			{
				uIExpression_Com_ChatItem.Close();
			}
			if (gObject is UIExpression_Com_ExpressionItem uIExpression_Com_ExpressionItem)
			{
				uIExpression_Com_ExpressionItem.Close();
			}
			uIExpressionListWindow.list_Plaform.RemoveChildToPool(gObject);
		}
		List<BattleMessage> messages = _messages;
		BattleMessage battleMessage = messages[messages.Count - 1];
		GObject item = uIExpressionListWindow.list_Plaform.AddItemFromPool(GetListItemResource(battleMessage.MsgType));
		RenderListItem(battleMessage, item);
	}

	private string GetListItemResource(MessageType type)
	{
		if (type == MessageType.EXPRESSION)
		{
			return "ui://bdqipkfgnriba";
		}
		return "ui://bdqipkfggbr1l";
	}

	private void RenderListItem(BattleMessage msg, GObject item)
	{
		if (item is UIExpression_Com_ChatItem uIExpression_Com_ChatItem)
		{
			uIExpression_Com_ChatItem.RefreshInfo(msg);
		}
		if (item is UIExpression_Com_ExpressionItem uIExpression_Com_ExpressionItem)
		{
			uIExpression_Com_ExpressionItem.RefreshInfo(msg);
		}
	}
}

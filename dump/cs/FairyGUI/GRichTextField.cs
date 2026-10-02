using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GRichTextField : GTextField
{
	public const string Store_RechargeDouYinPayPath = "UT_Recharge_DouYin_Pay";

	public string CURRENCY_SYMBOLS => GetCurrencySymbols();

	public RichTextField richTextField { get; private set; }

	public Dictionary<uint, Emoji> emojies
	{
		get
		{
			return richTextField.emojies;
		}
		set
		{
			richTextField.emojies = value;
		}
	}

	public void AddCurrencySymbols(string str)
	{
		text = CURRENCY_SYMBOLS + " " + str;
	}

	private string GetCurrencySymbols()
	{
		return "￥";
	}

	protected override void CreateDisplayObject()
	{
		richTextField = new RichTextField();
		richTextField.gOwner = this;
		base.displayObject = richTextField;
		_textField = richTextField.textField;
	}

	protected override void SetTextFieldText()
	{
		string text = _text;
		if (_templateVars != null)
		{
			text = ParseTemplate(text);
		}
		_textField.maxWidth = maxWidth;
		if (_ubbEnabled)
		{
			richTextField.htmlText = UBBParser.inst.Parse(text);
		}
		else
		{
			richTextField.htmlText = text;
		}
	}
}

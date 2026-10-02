using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using UnityEngine;

namespace UI;

public class UISinglePlayer_Com_BuildingLevel : GComponent
{
	private BuildingView _buildingView;

	private Card _card;

	private const float startX = 16f;

	private const float endX = 126f;

	private List<UISinglePlayer_Com_LevelLine> lines = new List<UISinglePlayer_Com_LevelLine>();

	private GObjectPool _pool;

	public Controller type;

	public UISinglePlayer_Com_BuildingLevelSlider com_Slider;

	public GTextField txt_Level;

	public const string URL = "ui://mi9vm3w0dajbq8d";

	protected override void CreateDisplayObject()
	{
		base.CreateDisplayObject();
		_pool = new GObjectPool(base.displayObject.cachedTransform);
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (!(_buildingView == null))
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(_buildingView.UIInfoRoot.transform.position);
			vector.y = (float)Screen.height - vector.y;
			base.xy = base.parent.GlobalToLocal(vector);
		}
	}

	public void Refresh(BuildingView buildingView)
	{
		_buildingView = buildingView;
		_card = buildingView.BuildingBase.Card;
		Show();
	}

	public void Refresh(Card card)
	{
		_card = card;
		Show();
	}

	public void Show()
	{
		base.visible = true;
		int upgradeExp = _card.GetUpgradeExp();
		txt_Level.text = _card.Level.Value.ToString();
		HideLines();
		if (_card.Level.Value != Card.MaxLevel())
		{
			int num = upgradeExp;
			float num2 = 110f / (float)num;
			for (int i = 0; i < num; i++)
			{
				UISinglePlayer_Com_LevelLine fromPool = GetFromPool();
				fromPool.x = 16f + (float)i * num2;
				fromPool.y = 5f;
				com_Slider.AddChild(fromPool);
				lines.Add(fromPool);
			}
			SetSliderValue(_card.Exp.Value);
		}
		else
		{
			com_Slider.img_Slider.width = 126f;
		}
		SetPosition();
	}

	public void Hide()
	{
		base.visible = false;
		HideLines();
	}

	private void SetPosition()
	{
		if (!(_buildingView == null))
		{
			Vector2 vector = UIHelper.World2Local(Camera.main, _buildingView.GetPosition());
			SetXY(vector.x, vector.y);
		}
	}

	private void SetSliderValue(int v)
	{
		int upgradeExp = _card.GetUpgradeExp();
		float num = 110f / (float)upgradeExp;
		if (v == 0)
		{
			com_Slider.img_Slider.width = 0f;
		}
		else
		{
			com_Slider.img_Slider.width = 16f + (float)v * num;
		}
	}

	private void HideLines()
	{
		foreach (UISinglePlayer_Com_LevelLine line in lines)
		{
			com_Slider.RemoveChild(line);
			ReturnToPool(line);
		}
		lines.Clear();
	}

	private UISinglePlayer_Com_LevelLine GetFromPool()
	{
		UISinglePlayer_Com_LevelLine uISinglePlayer_Com_LevelLine = _pool.GetObject("ui://mi9vm3w0dajbq8c") as UISinglePlayer_Com_LevelLine;
		if (uISinglePlayer_Com_LevelLine != null)
		{
			uISinglePlayer_Com_LevelLine.visible = true;
		}
		return uISinglePlayer_Com_LevelLine;
	}

	private void ReturnToPool(GObject obj)
	{
		_pool.ReturnObject(obj);
		obj.visible = false;
	}

	public static UISinglePlayer_Com_BuildingLevel CreateInstance()
	{
		return (UISinglePlayer_Com_BuildingLevel)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_BuildingLevel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_Slider = (UISinglePlayer_Com_BuildingLevelSlider)GetChildAt(0);
		txt_Level = (GTextField)GetChildAt(2);
	}
}

using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic.PlotTree;
using Tools;
using UnityEngine;

namespace UI.Extend;

public class Story_PerformLoader
{
	public int PerformId;

	private readonly UIStory_Com_PerformItem comPerform = UIStory_Com_PerformItem.CreateInstance();

	private readonly float tweenDuration = 0.2f;

	private GTweener _MoveTween;

	private GTweener _ScaleTween;

	private GTweener _AlphaTween;

	private GTweener _DelayTween;

	private GComponent _Parent;

	public async UniTask Refresh(DialogNode.DialogPerform performData, GComponent parent, float wScale, float hScale)
	{
		_Parent = parent;
		_Parent.AddChild(comPerform);
		if (PerformId != performData.id)
		{
			PerformId = performData.id;
			comPerform.visible = false;
		}
		comPerform.SetPivot(0.5f, 0.5f);
		comPerform.SetXY(performData.StartPosition.x * wScale, performData.StartPosition.y * hScale);
		comPerform.SetScale(performData.StartScale, performData.StartScale);
		comPerform.alpha = performData.StartAlpha;
		string texture = GetUITexture(performData);
		if (!string.IsNullOrWhiteSpace(texture))
		{
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(texture, null, null);
		}
		if (!string.IsNullOrEmpty(performData.emoji))
		{
			await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(performData.emoji, null, null);
		}
		if (!string.IsNullOrWhiteSpace(texture))
		{
			comPerform.loader_Skin.url = texture;
			comPerform.loader_Skin.image.graphics.flip = (performData.Flip ? FlipType.Horizontal : FlipType.None);
			comPerform.loader_Skin.visible = true;
		}
		else
		{
			comPerform.loader_Skin.visible = false;
		}
		if (!string.IsNullOrEmpty(performData.emoji))
		{
			comPerform.loader_Expression.url = performData.emoji;
			comPerform.loader_Expression.image.graphics.flip = (performData.Flip ? FlipType.Horizontal : FlipType.None);
			if (performData.Flip)
			{
				comPerform.loader_Expression.SetXY(performData.emojiFlipOffset.x, performData.emojiFlipOffset.y);
			}
			else
			{
				comPerform.loader_Expression.SetXY(performData.emojiOffset.x, performData.emojiOffset.y);
			}
			comPerform.loader_Expression.visible = true;
		}
		else
		{
			comPerform.loader_Expression.visible = false;
		}
		if (performData.Delay > 0f)
		{
			_DelayTween = GTween.DelayedCall(performData.Delay).OnComplete((GTweenCallback)delegate
			{
				StartPerform(performData, wScale, hScale);
			});
		}
		else
		{
			StartPerform(performData, wScale, hScale);
		}
		comPerform.visible = true;
	}

	private void StartPerform(DialogNode.DialogPerform performData, float wScale, float hScale)
	{
		Vector2 endPos = new Vector2(performData.EndPosition.x * wScale, performData.EndPosition.y * hScale);
		if (!performData.StartPosition.Equals(performData.EndPosition))
		{
			_MoveTween = comPerform.TweenMove(endPos, tweenDuration).OnComplete((GTweenCallback)delegate
			{
				comPerform.SetXY(endPos.x, endPos.y);
			});
		}
		if (!performData.StartScale.Equals(performData.EndScale))
		{
			_ScaleTween = comPerform.TweenScale(new Vector2(performData.EndScale, performData.EndScale), tweenDuration).OnComplete((GTweenCallback)delegate
			{
				comPerform.SetScale(performData.EndScale, performData.EndScale);
			});
		}
		if (!performData.StartAlpha.Equals(performData.EndAlpha))
		{
			_AlphaTween = comPerform.TweenFade(performData.EndAlpha, tweenDuration).OnComplete((GTweenCallback)delegate
			{
				comPerform.alpha = performData.EndAlpha;
			});
		}
	}

	public void Stop()
	{
		_MoveTween?.Kill(complete: true);
		_ScaleTween?.Kill(complete: true);
		_AlphaTween?.Kill(complete: true);
		_DelayTween?.Kill();
		_MoveTween = null;
		_ScaleTween = null;
		_AlphaTween = null;
		_DelayTween = null;
	}

	public void HideLoader()
	{
		comPerform.visible = false;
	}

	public void Dispose()
	{
		Stop();
		_Parent.RemoveChild(comPerform, dispose: true);
	}

	private string GetUITexture(DialogNode.DialogPerform performData)
	{
		if (string.IsNullOrEmpty(performData.SfwUITexture))
		{
			return performData.UITexture;
		}
		if (!GameSettings.angelMode)
		{
			return performData.UITexture;
		}
		return performData.SfwUITexture;
	}
}

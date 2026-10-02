using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Runtime;

public static class PipelineHelper
{
	private const string renderFeaturesListFieldName = "m_RendererFeatures";

	private const string renderDataListFieldName = "m_RendererDataList";

	public static ScriptableRendererData[] GetRenderDataList(UniversalRenderPipelineAsset asset)
	{
		FieldInfo field = typeof(UniversalRenderPipelineAsset).GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
		if (field != null)
		{
			return (ScriptableRendererData[])field.GetValue(asset);
		}
		throw new Exception("Reflection failed on field \"m_RendererDataList\" from class \"UniversalRenderPipelineAsset\". URP API likely changed");
	}

	public static void RemoveRenderFeature<T>(ScriptableRendererData renderer) where T : ScriptableRendererFeature
	{
		if ((UnityEngine.Object)(object)renderer == null)
		{
			return;
		}
		FieldInfo field = typeof(ScriptableRendererData).GetField("m_RendererFeatures", BindingFlags.Instance | BindingFlags.NonPublic);
		List<ScriptableRendererFeature> list = (List<ScriptableRendererFeature>)field.GetValue(renderer);
		T val = default(T);
		foreach (ScriptableRendererFeature item in list)
		{
			T val2 = (T)(object)((item is T) ? item : null);
			if (val2 != null)
			{
				val = val2;
			}
		}
		if ((UnityEngine.Object)(object)val != null)
		{
			list.Remove((ScriptableRendererFeature)(object)val);
		}
		field.SetValue(renderer, list);
		typeof(ScriptableRendererData).GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(renderer, null);
		Debug.Log("<b>" + ((UnityEngine.Object)val).name + "</b> was added to the <i>" + ((UnityEngine.Object)(object)renderer).name + "</i> renderer");
	}

	public static bool TryGetTargetRenderer(UniversalRenderPipelineAsset asset, int index, out ScriptableRendererData renderer)
	{
		bool result = false;
		renderer = null;
		if ((UnityEngine.Object)(object)asset == null)
		{
			asset = UniversalRenderPipeline.asset;
		}
		if ((bool)(UnityEngine.Object)(object)asset)
		{
			ScriptableRendererData[] renderDataList = GetRenderDataList(asset);
			if (index < 0 || index >= renderDataList.Length)
			{
				index = 0;
			}
			if (index >= 0 && index < renderDataList.Length)
			{
				renderer = renderDataList[index];
				result = true;
			}
		}
		return result;
	}

	public static bool RenderFeatureAdded<T>(ScriptableRendererData renderer)
	{
		foreach (ScriptableRendererFeature rendererFeature in renderer.rendererFeatures)
		{
			if (!((UnityEngine.Object)(object)rendererFeature == null) && ((object)rendererFeature).GetType() == typeof(T))
			{
				return true;
			}
		}
		return false;
	}

	public static T AddPlanarReflection<T>(ScriptableRendererData renderer, string name = "") where T : ScriptableRendererFeature
	{
		T val = ScriptableObject.CreateInstance<T>();
		((UnityEngine.Object)val).name = ((name == string.Empty) ? typeof(T).ToString() : name);
		FieldInfo field = typeof(ScriptableRendererData).GetField("m_RendererFeatures", BindingFlags.Instance | BindingFlags.NonPublic);
		List<ScriptableRendererFeature> list = (List<ScriptableRendererFeature>)field.GetValue(renderer);
		list.Add((ScriptableRendererFeature)(object)val);
		field.SetValue(renderer, list);
		typeof(ScriptableRendererData).GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(renderer, null);
		Debug.Log("<b>" + ((UnityEngine.Object)val).name + "</b> was added to the <i>" + ((UnityEngine.Object)(object)renderer).name + "</i> renderer");
		return val;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools;

public class ReflectionHelper
{
	private static readonly Dictionary<string, Type> _classNameToTypeDict = new Dictionary<string, Type>();

	private static T GetClassInstance<T>(string className) where T : class
	{
		T result = null;
		try
		{
			result = GetTypeByClassName(className).GetConstructor(Type.EmptyTypes).Invoke(null) as T;
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError("无法被反射不存在的类:" + className + ", 内置异常:" + ex.Message);
		}
		return result;
	}

	public static Type GetTypeByClassName(string className)
	{
		if (_classNameToTypeDict.TryGetValue(className, out var value))
		{
			return value;
		}
		Type type = Type.GetType(className);
		_classNameToTypeDict[className] = type;
		return type;
	}

	public static void Clear()
	{
		_classNameToTypeDict.Clear();
	}
}

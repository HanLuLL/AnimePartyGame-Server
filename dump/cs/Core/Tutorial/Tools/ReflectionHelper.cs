using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.Tutorial.Tools;

public static class ReflectionHelper
{
	private static readonly Dictionary<string, Type> _classNameToTypeDict = new Dictionary<string, Type>();

	public static T GetClassInstance<T>(string className) where T : class
	{
		try
		{
			Type typeByClassName = GetTypeByClassName(className);
			if (typeByClassName == null)
			{
				Debug.LogError("未找到类型: " + className);
				return null;
			}
			ConstructorInfo constructor = typeByClassName.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				Debug.LogError(className + " 没有无参构造函数");
				return null;
			}
			return constructor.Invoke(null) as T;
		}
		catch (Exception ex)
		{
			Debug.LogError("无法被反射不存在的类:" + className + ", 内置异常:" + ex.Message);
		}
		return null;
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

using UnityEngine;

namespace Tools;

public enum CompareFunction
{
	[InspectorName("禁用")]
	Disabled,
	[InspectorName("始终为假")]
	Never,
	[InspectorName("小于")]
	Less,
	[InspectorName("等于")]
	Equal,
	[InspectorName("小于等于")]
	LessEqual,
	[InspectorName("大于")]
	Greater,
	[InspectorName("不等于")]
	NotEqual,
	[InspectorName("大于等于")]
	GreaterEqual,
	[InspectorName("始终为真")]
	Always
}

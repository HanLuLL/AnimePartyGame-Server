using UnityEngine;

namespace GameLogic;

public enum RelationType
{
	NONE,
	[InspectorName("好友")]
	FRIEND,
	[InspectorName("陌生人")]
	STRANGERS,
	[InspectorName("拉黑——陌生人——厌恶")]
	DISLIKE,
	[InspectorName("拉黑——好友——关系恶化")]
	DETERIORATE
}

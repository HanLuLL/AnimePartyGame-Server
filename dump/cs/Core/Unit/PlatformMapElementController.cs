using Tools;
using UnityEngine;

namespace Core.Unit;

public class PlatformMapElementController : Unit
{
	[SerializeField]
	private GameObject ElementParent_PC;

	[SerializeField]
	private GameObject ElementParent_Mobile;

	protected override void Awake()
	{
		bool isMobilePlatform = Application.isMobilePlatform;
		ElementParent_PC.SetActiveEx(!isMobilePlatform);
		ElementParent_Mobile.SetActiveEx(isMobilePlatform);
	}
}

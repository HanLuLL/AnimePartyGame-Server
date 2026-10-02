using FairyGUI.Utils;

namespace FairyGUI;

public class ChangePageAction : ControllerAction
{
	public string objectId;

	public string controllerName;

	public string targetPage;

	protected override void Enter(Controller controller)
	{
		if (string.IsNullOrEmpty(controllerName))
		{
			return;
		}
		GComponent gComponent = (string.IsNullOrEmpty(objectId) ? controller.parent : (controller.parent.GetChildById(objectId) as GComponent));
		if (gComponent == null)
		{
			return;
		}
		Controller controller2 = gComponent.GetController(controllerName);
		if (controller2 == null || controller2 == controller || controller2.changing)
		{
			return;
		}
		if (targetPage == "~1")
		{
			if (controller.selectedIndex < controller2.pageCount)
			{
				controller2.selectedIndex = controller.selectedIndex;
			}
		}
		else if (targetPage == "~2")
		{
			controller2.selectedPage = controller.selectedPage;
		}
		else
		{
			controller2.selectedPageId = targetPage;
		}
	}

	public override void Setup(ByteBuffer buffer)
	{
		base.Setup(buffer);
		objectId = buffer.ReadS();
		controllerName = buffer.ReadS();
		targetPage = buffer.ReadS();
	}
}

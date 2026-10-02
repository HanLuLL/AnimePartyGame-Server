namespace FairyGUI;

internal class GearLookValue
{
	public float alpha;

	public float rotation;

	public bool grayed;

	public bool touchable;

	public GearLookValue(float alpha, float rotation, bool grayed, bool touchable)
	{
		this.alpha = alpha;
		this.rotation = rotation;
		this.grayed = grayed;
		this.touchable = touchable;
	}
}

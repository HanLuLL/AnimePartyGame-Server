namespace UI;

public class Model_UIActivityDice_Grid : BaseModel<UIActivityDice_Grid>
{
	public DiceActivityDataConfigureItem ItemData;

	public int TitleId;

	public Model_UIActivityDice_Grid(UIActivityDice_Grid _com)
		: base(_com)
	{
	}

	public override void Refresh()
	{
		com.txt_id.text = TitleId.ToString();
		com.loader_icon.url = ItemData.Icon;
	}
}

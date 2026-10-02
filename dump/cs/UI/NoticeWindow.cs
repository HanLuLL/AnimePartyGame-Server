namespace UI;

public class NoticeWindow : BaseWindow
{
	public NoticeWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UINoticeWindow.CreateInstance();
		base.OnInit();
	}
}

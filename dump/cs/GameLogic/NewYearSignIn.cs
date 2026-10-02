using Tools;
using UI;

namespace GameLogic;

public class NewYearSignIn : HomeMessage
{
	public override async void ShowMessage()
	{
		int? signInID = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInID();
		if (signInID.HasValue && SimpleSingletonProvider<GameLogicManager>.inst.signIn.CanSignIn(signInID.Value) && !SimpleSingletonProvider<UIManager>.inst.signInWindow.isShowing)
		{
			await SimpleSingletonProvider<UIManager>.inst.signInWindow.Open(signInID.Value);
		}
	}
}

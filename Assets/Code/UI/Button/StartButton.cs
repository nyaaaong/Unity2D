public class StartButton : ButtonBase
{
	protected override void ButtonEvent()
	{
		LoadingManager.LoadScene("MainScene");
	}
}

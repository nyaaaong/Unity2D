public class StartButton : ButtonBase
{
	protected override void ButtonEvent()
	{
		LoadingScene.LoadScene("MainScene");
	}
}

using UnityEditor;
using UnityEngine;

public class ExitButton : ButtonBase
{
	protected override void ButtonEvent()
	{
		// 에디터인 경우는 Application.Quit()를 해도 종료되지 않기 때문에 EditorApplication.isPlaying = false 로 플레이를 중지시킨다.
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}

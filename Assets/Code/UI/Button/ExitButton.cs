using UnityEditor;
using UnityEngine;

public class ExitButton : ButtonBase
{
	protected override void ButtonEvent()
	{
		// �������� ���� Application.Quit()�� �ص� ������� �ʱ� ������ EditorApplication.isPlaying = false �� �÷��̸� ������Ų��.
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}

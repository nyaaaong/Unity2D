using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		// ��������ȭ ����
		QualitySettings.vSyncCount = 0;

		// �ִ� �������� 75�� ����
		Application.targetFrameRate = 75;
	}
}

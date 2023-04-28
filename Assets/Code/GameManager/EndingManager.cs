using System.Collections;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
	[SerializeField]
	private float m_ShowTimer = 2f;

	private float m_Timer = 0f;

	private void Exit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	private void StartShowTimer()
	{
		StartCoroutine(UpdateShow());
	}

	private IEnumerator UpdateShow()
	{
		while (true)
		{
			yield return null;

			m_Timer += Time.deltaTime;

			if (m_Timer >= m_ShowTimer)
			{
				UIManager.FadeIn();
				UIManager.SetFadeInFunc(Exit);

				yield break;
			}
		}
	}

	private IEnumerator Init()
	{
		while (!UIManager.IsCompleteInit())
		{
			yield return null;
		}

		UIManager.FadeOut();
		UIManager.SetFadeOutFunc(StartShowTimer);
	}

	private void Start()
	{
		StartCoroutine(Init());
	}

	private void Awake()
	{
		AudioManager.PlayBGM(BGM_Type.Ending);
	}
}

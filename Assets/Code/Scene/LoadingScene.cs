using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScene : Global
{
	[SerializeField]
	private float m_DelayTime = 1f;

	private static string m_NextScene;
	private AsyncOperation m_OP = null;
	private float m_Time = 0f;

	public static void LoadScene(string nextScene)
	{
		m_NextScene = nextScene;

		SceneManager.LoadScene("LoadingScene");
	}

	IEnumerator Loading()
	{
		m_OP = SceneManager.LoadSceneAsync(m_NextScene);

		// 다음 씬이 준비되면 자동으로 넘어가지 않게 한다
		m_OP.allowSceneActivation = false;

		while (!m_OP.isDone)
		{
			yield return null;

			// unscaledDeltaTime는 timeScale에 영향받지 않는 deltaTime을 의미한다
			m_Time += Time.unscaledDeltaTime;

			if (m_Time >= m_DelayTime)
			{
				m_OP.allowSceneActivation = true;

				yield break;
			}
		}
	}

	private void Start()
	{
		StartCoroutine(Loading());
	}
}

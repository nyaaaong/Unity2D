using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
	private void LoadEndingScene()
	{
		SceneManager.LoadScene("EndingScene");
		AudioManager.StopBGM();
	}

	private IEnumerator Init()
	{
		while (!UIManager.IsCompleteInit())
		{
			yield return null;
		}

		UIManager.FadeOut();
		UIManager.SetFadeInFunc(LoadEndingScene);
	}

	private void Start()
	{
		StartCoroutine(Init());
	}
}

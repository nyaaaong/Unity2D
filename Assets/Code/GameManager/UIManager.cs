using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Global
{
	[SerializeField]
	private GameObject m_HealthBarPrefeb = null;
	[SerializeField]
	private float m_FadeSpeed = 1f;

	static private UIManager m_Inst = null;

	private GameObject m_HealthBarObj = null;
	private GameObject m_FadeObj = null;
	private Scene m_Scene;

	private Fade m_Fade = null;

	public static float FadeSpeed { get { return m_Inst.m_FadeSpeed; } }

	public static void SetFadeInFunc(FadeCompleteFunc func)
	{
		if (m_Inst.m_Fade == null)
			Debug.LogError("if (m_Inst.m_Fade == null)");

		m_Inst.m_Fade.SetFadeInFunc(func);
	}

	public static void SetFadeOutFunc(FadeCompleteFunc func)
	{
		if (m_Inst.m_Fade == null)
			Debug.LogError("if (m_Inst.m_Fade == null)");

		m_Inst.m_Fade.SetFadeOutFunc(func);
	}

	public static bool IsCompleteInit()
	{
		if (m_Inst.m_FadeObj != null)
			return true;

		else
			return false;
	}

	public static void FadeIn()
	{
		m_Inst.m_FadeObj.SetActive(true);
		m_Inst.m_Fade.FadeIn();
	}

	public static void FadeOut()
	{
		m_Inst.m_FadeObj.SetActive(true);
		m_Inst.m_Fade.FadeOut();
	}

	public static void EnableHealthBar(bool isEnable)
	{
		if (isEnable)
			m_Inst.m_HealthBarObj.SetActive(true);

		else
		{
			m_Inst.m_HealthBarObj.SetActive(false);
			Destroy(m_Inst.m_HealthBarObj);
		}
	}

	private void HealthBarInit()
	{
		m_HealthBarObj = Instantiate(m_HealthBarPrefeb);

		if (m_Inst.m_HealthBarObj == null)
			Debug.LogError("if (m_Inst.m_HealthBarObj == null)");

		m_HealthBarObj.SetActive(false);
	}

	private void FadeInit()
	{		
		m_FadeObj = GameObject.FindGameObjectWithTag("Fade");

		if (m_FadeObj == null)
			Debug.LogError("if (m_Inst.m_FadeObj == null)");

		m_FadeObj.GetComponent<Canvas>().enabled = true;

		m_Fade = m_FadeObj.transform.GetChild(0).GetComponent<Fade>();

		if (m_Fade == null)
			Debug.LogError("if (m_Fade == null)");
	}

	private IEnumerator MainSceneCheck()
	{
		while (true)
		{
			yield return null;

			m_Scene = SceneManager.GetActiveScene();

			if (m_Scene.name == "MainScene")
			{
				StopCoroutine(EndingSceneCheck());

				HealthBarInit();
				FadeInit();

				yield break;
			}
		}
	}

	private IEnumerator EndingSceneCheck()
	{
		while (true)
		{
			yield return null;

			m_Scene = SceneManager.GetActiveScene();

			if (m_Scene.name == "EndingScene")
			{
				StopCoroutine(MainSceneCheck());

				FadeInit();

				yield break;
			}
		}
	}

	private void Start()
	{
		StartCoroutine(MainSceneCheck());
		StartCoroutine(EndingSceneCheck());
	}

	private void Awake()
	{
		m_Inst = this;

		m_Scene = SceneManager.GetActiveScene();

		if (m_Scene.name == "MainScene")
		{
			if (m_HealthBarPrefeb == null)
				Debug.LogError("if (m_HealthBarPrefeb == null)");
		}
	}
}

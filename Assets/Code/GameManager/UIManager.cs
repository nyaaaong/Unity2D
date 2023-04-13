using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_HealthBarPrefeb = null;
	[SerializeField]
	private GameObject m_FadePrefeb = null;
	[SerializeField]
	private float m_FadeSpeed = 1f;

	static private UIManager m_Inst = null;

	private GameObject m_HealthBarObj = null;
	private GameObject m_FadeObj = null;

	private Fade m_Fade = null;

	public static float FadeSpeed { get { return m_Inst.m_FadeSpeed; } }

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

	private void Awake()
	{
		m_Inst = this;

		if (m_HealthBarPrefeb == null)
			Debug.LogError("if (m_HealthBarPrefeb == null)");

		if (m_FadePrefeb == null)
			Debug.LogError("if (m_FadePrefeb == null)");

		m_HealthBarObj = Instantiate(m_HealthBarPrefeb);

		if (m_Inst.m_HealthBarObj == null)
			Debug.LogError("if (m_Inst.m_HealthBarObj == null)");

		m_HealthBarObj.SetActive(false);


		m_FadeObj = Instantiate(m_FadePrefeb);

		if (m_Inst.m_FadeObj == null)
			Debug.LogError("if (m_Inst.m_FadeObj == null)");

		m_FadeObj.SetActive(false);
		
		m_Fade = m_FadeObj.transform.GetChild(0).GetComponent<Fade>();

		if (m_Fade == null)
			Debug.LogError("if (m_Fade == null)");
	}
}

using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_HealthBarPrefeb = null;

	static private UIManager m_Inst = null;

	private GameObject m_HealthBarObj = null;

	public static void EnableHealthBar(bool isEnable)
	{
		if (m_Inst.m_HealthBarObj == null)
			Debug.LogError("if (m_Inst.m_HealthBarObj == null)");

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

		m_HealthBarObj = Instantiate(m_HealthBarPrefeb);
		m_HealthBarObj.SetActive(false);
	}
}

using UnityEngine;

public class Global : MonoBehaviour
{
	[SerializeField]
	private Camera      m_MainCamera = null;
	[SerializeField]
	private float       m_EffectVolume = 1.0f;
	[SerializeField]
	private float       m_BGMVolume = 1.0f;

	private static Global  m_Inst = null;
	private GameObject  m_Player = null;
	private Vector2     m_P2MDist = Vector2.zero;
	private Vector2     m_T2PDist = Vector2.zero;
	private float       m_P2MAngle = 0.0f;
	private float       m_T2PAngle = 0.0f;

	public static GameObject Player { get { return m_Inst.m_Player; } }
	public static float P2MAngle { get { return m_Inst.m_P2MAngle; } }
	public static float EffectVolume { get { return m_Inst.m_EffectVolume; } }

	public static float T2PAngle(Vector3 targetPos)
	{
		m_Inst.m_T2PDist = m_Inst.m_Player.transform.position - m_Inst.m_MainCamera.ScreenToWorldPoint(targetPos);

		m_Inst.m_T2PAngle = Mathf.Atan2(m_Inst.m_T2PDist.y, m_Inst.m_T2PDist.x) * Mathf.Rad2Deg;

		return m_Inst.m_T2PAngle;
	}

	private void Awake()
	{
		m_Inst = this;

		m_Player = GameObject.Find("Player");
	}

	private void Update()
	{
		m_P2MDist = m_MainCamera.ScreenToWorldPoint(Input.mousePosition) - m_Player.transform.position;

		m_P2MAngle = Mathf.Atan2(m_P2MDist.y, m_P2MDist.x) * Mathf.Rad2Deg;
	}
}

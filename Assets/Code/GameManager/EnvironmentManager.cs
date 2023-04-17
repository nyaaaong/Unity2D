using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_PortalOutObj = null;

	static private EnvironmentManager m_Inst = null;
	private bool m_UsedPortal = false;
	private Vector3 m_PortalOutPos = Vector3.zero;

	static public void PlayerMovePortalOut()
	{
		if (m_Inst.m_UsedPortal)
			return;

		m_Inst.m_UsedPortal = true;
		CharacterManager.Player.MovePos = m_Inst.m_PortalOutPos;
	}

	private void Awake()
	{
		m_Inst = this;

		if (m_PortalOutObj == null)
			Debug.LogError("if (m_PortalOutObj == null)");

		m_PortalOutPos = m_PortalOutObj.transform.position;
	}
}

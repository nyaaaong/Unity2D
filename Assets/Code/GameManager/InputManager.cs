using UnityEngine;

public class InputManager : Global
{
	[SerializeField]
	private Camera m_MainCamera = null;

	static private InputManager m_Inst = null;

	private Vector2 m_MouseWorldPos = Vector2.zero;
	private Vector2 m_MouseScreenPos = Vector2.zero;

	static public Vector2 MouseScreenPos { get { return m_Inst.m_MouseScreenPos; } }
	static public Vector2 MouseWorldPos { get { return m_Inst.m_MouseWorldPos; } }

	private void Awake()
	{
		m_Inst = this;

		if (m_MainCamera == null)
			Debug.LogError("if (m_MainCamera == null)");
	}

	protected override void MiddleUpdate()
	{
		base.MiddleUpdate();

		m_MouseWorldPos = Input.mousePosition;
		m_MouseScreenPos = m_MainCamera.ScreenToWorldPoint(m_MouseWorldPos);
	}
}

using UnityEngine;

public class Shadow : Global
{
	[SerializeField]
	private Character m_Base = null;

	private SpriteRenderer m_SR = null;
	private Color m_tempColor = Color.white;

	private void Awake()
	{
		if (m_Base == null)
			Debug.LogError("if (m_Base == null)");

		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR == null)
			Debug.LogError("if (m_SR == null)");

		m_tempColor = m_SR.color;
	}

	protected override void AfterUpdate()
	{
		base.AfterUpdate();

		if (m_Base.DeathAnimProc)
		{
			m_tempColor.a = m_Base.Color.a;

			m_SR.color = m_tempColor;
		}
	}
}

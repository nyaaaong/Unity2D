using UnityEngine;

public class PortalOut : Global
{
	private SpriteRenderer m_SR = null;

	private void Awake()
	{
		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR == null)
			Debug.LogError("if (m_SR == null)");

		m_SR.enabled = false;
	}
}
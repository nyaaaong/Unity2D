using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
	[SerializeField]
	private Character   m_Base = null;

	private SpriteRenderer  m_SR = null;
	private Color   m_tempColor = Color.white;

	private void Awake()
	{
		if (m_Base is null)
			Debug.LogError("if (m_Base is null)");

		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR is null)
			Debug.LogError("if (m_SR is null)");

		m_tempColor = m_SR.color;
	}

	void Update()
	{
		if (m_Base.DeathAnimProc)
		{
			m_tempColor.a = m_Base.Color.a;

			m_SR.color = m_tempColor;
		}
	}
}

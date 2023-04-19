using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : Global
{
	private float m_HPAmount = 1f;
	private float m_HPMax = 1f;
	private Image m_HealthImg = null;
	private Boss m_Boss = null;

	private void Awake()
	{
		m_HealthImg = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

		if (m_HealthImg == null)
			Debug.LogError("if (m_HealthImg == null)");

		m_Boss = CharacterManager.Boss.GetComponent<Boss>();

		if (m_Boss == null)
			Debug.LogError("if (m_Boss == null)");
	}

	private void Start()
	{
		m_HPMax = m_Boss.HPMax;

		if (m_HPMax == 0)
			Debug.LogError("if (m_HPMax == 0)");
	}

	protected override void BeforeUpdate()
	{
		base.BeforeUpdate();

		if (m_Boss.HP > 0f)
			m_HPAmount = m_Boss.HP / m_HPMax;

		else
			m_HPAmount = 0f;

		m_HealthImg.fillAmount = m_HPAmount;
	}
}

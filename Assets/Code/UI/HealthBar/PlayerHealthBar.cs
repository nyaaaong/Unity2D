using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
	private float m_HPAmount = 1f;
	private float m_HPMax = 1f;
	private Image m_HealthImg = null;
	private Player m_Player = null;

	private void Awake()
	{
		m_HealthImg = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

		if (m_HealthImg == null)
			Debug.LogError("if (m_HealthImg == null)");

		m_Player = CharacterManager.Player;
	}

	private void Start()
	{
		m_HPMax = m_Player.HPMax;

		if (m_HPMax == 0)
			Debug.LogError("if (m_HPMax == 0)");
	}

	private void FixedUpdate()
	{
		if (m_Player.HP > 0f)
			m_HPAmount = m_Player.HP / m_HPMax;

		else
			m_HPAmount = 0f;

		m_HealthImg.fillAmount = m_HPAmount;
	}
}

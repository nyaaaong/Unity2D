using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Character
{
	private void InputCheck()
	{
		m_InputX = Input.GetAxisRaw("Horizontal");
		m_InputY = Input.GetAxisRaw("Vertical");

		m_Animator.SetFloat("InputX", m_InputX);
		m_Animator.SetFloat("InputY", m_InputY);

		m_Move = m_InputX == 0f && m_InputY == 0f ? false : true;

		if (m_Status == Character_Status.Dodge)
		{
			m_MovePos.x = m_InputXPrev;
			m_MovePos.y = m_InputYPrev;
			m_MovePos.Normalize();

			m_MovePos *= m_DodgeSpeed;
		}

		else
		{
			m_MovePos.x = m_InputX;
			m_MovePos.y = m_InputY;
			m_MovePos.Normalize();

			m_MovePos *= m_Info.m_MoveSpeed;
		}
	}

	private void Move()
{
		InputCheck();

		m_Rig.velocity = m_MovePos;
	}

	private void Dodge()
	{
		if (Input.GetMouseButtonDown((int)Mouse_Click.Right))
		{
			if (m_KeyLock || !m_Move)
				return;

			m_Status = Character_Status.Dodge;

			m_NoHit = true;
			m_Move = true;
			m_KeyLock = true;
			m_HideWeapon = true;

			m_InputXPrev = m_InputX;
			m_InputYPrev = m_InputY;

			PlaySound(m_DodgeClip);
		}
	}

	private void MoveKeyCheck()
	{
		Move();
		Dodge();
	}
}

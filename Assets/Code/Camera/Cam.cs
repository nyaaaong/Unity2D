using System;
using UnityEngine;

public class Cam : Global
{
	[Serializable]
	private class DIR
	{
		public float UP = 0f;
		public float DOWN = 0f;
		public float LEFT = 0f;
		public float RIGHT = 0f;
	}

	[SerializeField]
	private float m_Speed = 2f;
	[SerializeField]
	private DIR m_PlayerOffset = null;
	[SerializeField]
	private Transform m_Border = null;
	[SerializeField]
	private GameObject m_PlayerUIObj = null;

	private Camera m_Cam = null;
	private float m_CamZ = 0f;
	private Vector3 m_Pos = Vector2.zero;
	private Vector3 m_NextPos = Vector2.zero;
	private Vector3 m_Res = Vector3.zero;

	private Vector2 m_Dir = Vector2.zero;
	private Vector2 m_Dist = Vector2.zero;

	private Vector2 m_PlayerLT = Vector2.zero;
	private Vector2 m_PlayerRB = Vector2.zero;
	private float m_P2SDist = 0f;

	private Vector2 m_hRS = Vector2.zero;   // Half Resolution
	private Vector2 m_ScreenLT = Vector2.zero;
	private Vector2 m_ScreenRB = Vector2.zero;
	private Vector2 m_Center = Vector2.zero;

	private Vector2 m_BorderLT = Vector2.zero;
	private Vector2 m_BorderRB = Vector2.zero;
	private float m_BorderDist = 0f;

	private Vector2 m_MouseScreenPos = Vector2.zero;

	private void Calc()
	{
		m_Res = Vector3.Lerp(transform.position, m_NextPos, Time.deltaTime);

		m_PlayerLT.x = m_Pos.x - m_PlayerOffset.LEFT;
		m_PlayerLT.y = m_Pos.y + m_PlayerOffset.UP;

		m_PlayerRB.x = m_Pos.x + m_PlayerOffset.RIGHT;
		m_PlayerRB.y = m_Pos.y - m_PlayerOffset.DOWN;

		//
		m_hRS.x = m_Cam.aspect * m_Cam.orthographicSize;
		m_hRS.y = m_Cam.orthographicSize;

		m_ScreenLT = m_Res + new Vector3(-m_hRS.x, m_hRS.y, 0);
		m_ScreenRB = m_Res + new Vector3(m_hRS.x, -m_hRS.y, 0);
	}

	private void PlayerCheck()
	{
		if (m_ScreenLT.x > m_PlayerLT.x)
		{
			m_P2SDist = m_PlayerLT.x - m_ScreenLT.x;
			m_Res.x += m_P2SDist;
		}

		else if (m_ScreenRB.x < m_PlayerRB.x)
		{
			m_P2SDist = m_PlayerRB.x - m_ScreenRB.x;
			m_Res.x += m_P2SDist;
		}

		if (m_ScreenLT.y < m_PlayerLT.y)
		{
			m_P2SDist = m_PlayerLT.y - m_ScreenLT.y;
			m_Res.y += m_P2SDist;
		}

		else if (m_ScreenRB.y > m_PlayerRB.y)
		{
			m_P2SDist = m_PlayerRB.y - m_ScreenRB.y;
			m_Res.y += m_P2SDist;
		}
	}

	private void BoundaryCheck()
	{
		if (m_BorderLT.x > m_ScreenLT.x)
		{
			m_BorderDist = m_BorderLT.x - m_ScreenLT.x;
			m_Res.x += m_BorderDist;
		}

		else if (m_BorderRB.x < m_ScreenRB.x)
		{
			m_BorderDist = m_BorderRB.x - m_ScreenRB.x;
			m_Res.x += m_BorderDist;
		}

		if (m_BorderLT.y < m_ScreenLT.y)
		{
			m_BorderDist = m_BorderLT.y - m_ScreenLT.y;
			m_Res.y += m_BorderDist;
		}

		else if (m_BorderRB.y > m_ScreenRB.y)
		{
			m_BorderDist = m_BorderRB.y - m_ScreenRB.y;
			m_Res.y += m_BorderDist;
		}
	}

	private void BorderCheck()
	{
		PlayerCheck();
		BoundaryCheck();

		transform.position = m_Res;
	}

	private void FollowMouse()
	{
		m_MouseScreenPos = InputManager.MouseScreenPos;

		m_Dist.x = m_MouseScreenPos.x - m_Center.x;
		m_Dist.y = m_MouseScreenPos.y - m_Center.y;

		m_Dir.x = m_Dist.x / m_Center.x;
		m_Dir.y = m_Dist.y / m_Center.y;

		m_NextPos = m_Pos + new Vector3(m_Dir.x, m_Dir.y, 0f) * m_Speed;
	}

	private void Awake()
	{
		m_Cam = Camera.main;

		m_Center.x = Screen.width * 0.5f;
		m_Center.y = Screen.height * 0.5f;

		m_CamZ = gameObject.transform.position.z;

		if (m_Border == null)
			Debug.LogError("if (m_Border == null)");

		m_BorderLT.x = m_Border.position.x - m_Border.localScale.x * 0.5f;
		m_BorderLT.y = m_Border.position.y + m_Border.localScale.y * 0.5f;

		m_BorderRB.x = m_Border.position.x + m_Border.localScale.x * 0.5f;
		m_BorderRB.y = m_Border.position.y - m_Border.localScale.y * 0.5f;

		if (m_PlayerUIObj == null)
			Debug.LogError("if (m_PlayerUIObj == null)");
	}

	protected override void AfterUpdate()
	{
		base.AfterUpdate();

		m_Pos = CharacterManager.PlayerPos3D;
		m_Pos.z = m_CamZ;

		Calc();
		FollowMouse();
		BorderCheck();
	}
}

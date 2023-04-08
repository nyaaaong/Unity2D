using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cam : MonoBehaviour
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
	private Camera m_Cam = null;
	[SerializeField]
	private float m_Speed = 2f;
	[SerializeField]
	private DIR m_PlayerOffset = null;
	[SerializeField]
	private Transform m_Border = null;

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

	private AudioSource m_Audio = null;
	private AudioClip m_BGMTitle = null;
	private AudioClip m_BGMMain = null;
	private AudioClip m_BGMBoss = null;
	private AudioClip m_BGMBossClear = null;
	private AudioClip m_BGMEnding = null;
	private BGM_Type m_BGMType = BGM_Type.None;

	public void PlayBGM(BGM_Type type)
	{
		if (m_BGMType == type)
			return;

		m_BGMType = type;
		m_Audio.Stop();

		switch (m_BGMType)
		{
			case BGM_Type.Title:
				m_Audio.clip = m_BGMTitle;
				break;
			case BGM_Type.Main:
				m_Audio.clip = m_BGMMain;
				break;
			case BGM_Type.Boss:
				m_Audio.clip = m_BGMBoss;
				break;
			case BGM_Type.Boss_Clear:
				m_Audio.clip = m_BGMBossClear;
				break;
			case BGM_Type.Ending:
				m_Audio.clip = m_BGMEnding;
				break;
		}

		m_Audio.Play();
	}

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
		m_Dist.x = Input.mousePosition.x - m_Center.x;
		m_Dist.y = Input.mousePosition.y - m_Center.y;

		m_Dir.x = m_Dist.x / m_Center.x;
		m_Dir.y = m_Dist.y / m_Center.y;

		m_NextPos = m_Pos + new Vector3(m_Dir.x, m_Dir.y, 0f) * m_Speed;
	}

	private void FollowPlayerCenter()
	{
		m_Pos = Global.Player.RigidBodyPos;
		m_Pos.z = m_CamZ;

		Calc();
	}

	private void Awake()
	{
		if (m_Cam == null)
			Debug.LogError("if (m_Cam == null)");

		m_Center.x = Screen.width * 0.5f;
		m_Center.y = Screen.height * 0.5f;

		m_CamZ = gameObject.transform.position.z;

		if (m_Border == null)
			Debug.LogError("if (m_Border == null)");

		m_BorderLT.x = m_Border.position.x - m_Border.localScale.x * 0.5f;
		m_BorderLT.y = m_Border.position.y + m_Border.localScale.y * 0.5f;

		m_BorderRB.x = m_Border.position.x + m_Border.localScale.x * 0.5f;
		m_BorderRB.y = m_Border.position.y - m_Border.localScale.y * 0.5f;

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio == null)
			Debug.LogError("if (m_Audio == null)");

		m_Audio.volume = Global.BGMVolume;
		m_Audio.loop = true;

		m_BGMTitle = Global.BGMTitle;
		m_BGMMain = Global.BGMMain;
		m_BGMBoss = Global.BGMBoss;
		m_BGMBossClear = Global.BGMBossClear;
		m_BGMEnding = Global.BGMBossEnding;
	}

	private void FixedUpdate()
	{
		FollowPlayerCenter();
		FollowMouse();

		BorderCheck();
	}

	private void LateUpdate()
	{
	}
}

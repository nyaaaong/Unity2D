using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private float				m_MoveSpeed = 10.0f;
	[SerializeField]
	private string				m_DefaultAnimName = "";
	[SerializeField]
	private bool                m_DebugHasPistol = false;

	private const int			UP = (int)Player_Dir.Up;
	private const int			LEFT = (int)Player_Dir.Left;
	private const int			RIGHT = (int)Player_Dir.Right;
	private const int			DOWN = (int)Player_Dir.Down;
	private const int			END = (int)Player_Dir.End;

	private float				m_deltaTime = 0.0f;
	private float               m_P2MAngle = 0.0f;
	private Animator			m_Animator = null;
	private Player_Status		m_Status = Player_Status.Idle;
	private Player_WeaponSlot   m_Slot = Player_WeaponSlot.End;
	private bool[]				m_Dir = null;
	private bool[]				m_KeyDown = null;
	private bool[]              m_HasWeapon = null;
	private bool				m_Move = false;
	private string				m_AnimName = "";
	private string				m_PrevAnimName = "";
	private Vector2				m_P2MPos = Vector2.zero;

	private void Awake()
	{
		m_Animator = GetComponent<Animator>();

		if (m_Animator == null)
			Debug.LogError("if (m_Animator == null)");

		m_AnimName = m_DefaultAnimName;
		m_PrevAnimName = m_DefaultAnimName;

		m_Dir = new bool[END];
		m_KeyDown = new bool[END];
		m_HasWeapon = new bool[(int)Player_WeaponSlot.End];

		m_HasWeapon[(int)Player_WeaponSlot.Pistol] = m_DebugHasPistol;

		m_Dir[RIGHT] = true;
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		m_deltaTime = Time.deltaTime;

		if (m_Slot != Player_WeaponSlot.End)
		{
			m_P2MPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

			m_P2MAngle = Mathf.Atan2(m_P2MPos.y, m_P2MPos.x) * Mathf.Rad2Deg;
		}

		MoveKeyCheck();
		WeaponKeyCheck();
		AnimCheck();
	}
}

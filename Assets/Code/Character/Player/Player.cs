using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private bool				m_DebugHasAllWeap = false;

	private static Player		m_Inst = null;
	private float				m_P2MAngle = 0.0f;
	private bool[]				m_Dir = null;
	private bool[]				m_KeyDown = null;
	private bool[]				m_HasWeapon = null;

	private const int           UP = (int)Player_Dir.Up;
	private const int           LEFT = (int)Player_Dir.Left;
	private const int           RIGHT = (int)Player_Dir.Right;
	private const int           DOWN = (int)Player_Dir.Down;
	private const int           END = (int)Player_Dir.End;

	private void Awake()
	{
		m_Inst = this;

		m_Animator = GetComponent<Animator>();

		if (m_Animator == null)
			Debug.LogError("if (m_Animator == null)");

		m_Dir = new bool[END];
		m_KeyDown = new bool[END];
		m_HasWeapon = new bool[(int)Weapon_Type.End];

		if (m_DebugHasAllWeap)
		{
			for (int i = 0; i < (int)Weapon_Type.End; ++i)
			{
				m_HasWeapon[i] = true;
			}
		}

		m_Dir[RIGHT] = true;
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		if (m_WeapType != Weapon_Type.End)
			m_P2MAngle = Global.P2MAngle;

		MoveKeyCheck();
		WeaponKeyCheck();
		AnimCheck();
	}
}

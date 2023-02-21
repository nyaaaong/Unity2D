using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
	private float       m_deltaTime = 0.0f;

	private Animator    m_Animator = null;
	[SerializeField]
	private float       m_MoveSpeed = 10.0f;

	private Player_Status   m_Status = Player_Status.Idle;

	private bool[]          m_Dir = null;
	private bool[]          m_KeyDown = null;
	private bool            m_Move = false;

	const int               UP = (int)Player_Dir.Up;
	const int               LEFT = (int)Player_Dir.Left;
	const int               RIGHT = (int)Player_Dir.Right;
	const int               DOWN = (int)Player_Dir.Down;
	const int               END = (int)Player_Dir.End;

	private string          m_AnimName = "";
	private string          m_PrevAnimName = "";
	[SerializeField]
	private string          m_DefaultAnimName = "";

	private void Awake()
	{
		m_Animator = GetComponent<Animator>();

		if (m_Animator == null)
			Debug.LogError("if (m_Animator == null)");

		m_AnimName = m_DefaultAnimName;
		m_PrevAnimName = m_DefaultAnimName;

		m_Dir = new bool[END];
		m_KeyDown = new bool[END];

		m_Dir[RIGHT] = true;
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		m_deltaTime = Time.deltaTime;

		KeyCheck();
		AnimCheck();
	}
}

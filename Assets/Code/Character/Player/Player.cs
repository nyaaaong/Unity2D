using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private Animator    m_Animator = null;
	[SerializeField]
	private float		m_MoveSpeed = 10.0f;
		
	private Player_Status	m_Status = Player_Status.Idle;
	private Player_Dir		m_Dir = Player_Dir.DownRight;
	private bool            m_Move = false;
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
	}

	private void Update()
	{
		KeyCheck();
		AnimCheck();
	}
}

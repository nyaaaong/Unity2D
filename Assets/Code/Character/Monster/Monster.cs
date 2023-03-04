using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	[SerializeField]
	protected bool  m_DebugFire = false;

	protected delegate void Pattern();
	protected List<Pattern>    m_PatternList = null;

	private void PatternExec()
	{
		int idx = m_PatternList.Count;

		if (idx == 0)
			return;

		idx = Random.Range(0, idx);

		m_PatternList[idx]();
	}

	private void HandCheck()
	{
		if (m_TargetAngle > 90.0f || m_TargetAngle < -90.0f)
			m_HandDir = Weapon_Hand.Left;

		else
			m_HandDir = Weapon_Hand.Right;
	}

	protected override void Awake()
	{
		base.Awake();

		m_PatternList = new List<Pattern>();

		m_Fire = m_DebugFire;
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		Global.E2PData(this);

		HandCheck();
		PatternExec();
	}
}

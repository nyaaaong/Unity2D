using UnityEngine;

public class ShotgunKin : Monster
{
	[SerializeField]
	private bool m_Blue = false;

	protected override void DeathAnimEvent()
	{
		if (m_Blue)
			m_SpreadBullet = true;
	}

	protected override void DeathAnimCheck()
	{
		if (m_Death && !m_DeathAnimProc)
		{
			m_Animator.SetTrigger("Death");

			m_DeathAnimProc = true;
		}
	}

	protected override void Awake()
	{
		base.Awake();

		m_UseAlpha = false;

		m_PatternList.Add(MovePattern);
	}
}

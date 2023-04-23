public class Bandana : Monster
{
	// m_Fire = true일 때 누적될 시간
	private float m_FireAccTime = 0f;
	private float m_FireAccTimeMax = 1f;

	// m_Fire = false 의 지속 시간
	private float m_ReloadTime = 0f;
	private float m_ReloadTimeMax = 2f;

	private bool m_Reload = false;
	private bool m_FirstShoot = true; // 몬스터가 처음 총을 쏘는 시점

	protected override void RangeCheck()
	{
		if (!m_Boss)
		{
			if (m_IsWall || m_TargetDist > m_WeapRange)
			{
				m_FollowPlayer = true;
				m_Fire = false;
				m_Reload = false;

				m_FireAccTime = 0f;
				m_ReloadTime = 0f;
			}

			else if (!m_IsWall && m_TargetDist <= m_WeapRange)
			{
				m_FollowPlayer = false;

				if (m_FirstShoot)
				{
					m_FirstShoot = false;
					m_Fire = true;
				}

				if (!m_Reload)
				{
					m_FireAccTime += m_deltaTime;

					if (m_FireAccTime >= m_FireAccTimeMax)
					{
						m_FireAccTime = 0f;
						m_Reload = true;
						m_Fire = false;
					}
				}

				else
				{
					m_ReloadTime += m_deltaTime;

					if (m_ReloadTime >= m_ReloadTimeMax)
					{
						m_ReloadTime = 0f;
						m_Reload = false;
						m_Fire = true;
					}
				}
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();

		m_PatternList.Add(MovePattern);
	}
}

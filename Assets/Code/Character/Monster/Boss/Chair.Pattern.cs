public partial class Chair : Monster
{
	private void ChangeBulletSpeed()
	{
		if (!m_P3)
			m_Bullet.Speed = m_WeapInfo.m_FireSpeed;

		else
			m_Bullet.Speed = m_P3Speed;
	}

	private void FireBullet(float angle, float firstDist = 0f)
	{
		m_NewAngle = m_TargetAngle + angle;

		m_NewBulletObj = Instantiate(m_EnemyBullet);
		m_NewBulletObj.name = "Bullet";

		m_NewBullet = m_NewBulletObj.GetComponent<Bullet>();
		m_NewBullet.SetInfo(m_Bullet);
		m_NewBullet.Dir = ConvertDir(m_NewAngle);

		if (firstDist != 0f)
		{
			// 타겟 기준이 아닌 몬스터 기준이기 때문에 기존 총알 위치였던 RigidBodyPos에 m_FirstDist만큼 더해준다.
			m_BulletPos = m_NewBullet.Dir * firstDist;
			m_BulletPos.z = 0f;
			m_NewBullet.transform.position += m_BulletPos;
		}
	}

	private void BulletSetting(bool useTargetDir)
	{
		if (useTargetDir)
		{
			m_BulletPos = RigidBodyPos3D + m_TargetDir * m_WeapInfo.m_FirstDist;
			m_Bullet.Dir = m_TargetDir;
		}

		else
			m_BulletPos = m_Pos;

		m_BulletPos.z = -5.0f;

		m_Bullet.transform.position = m_BulletPos;
		m_Bullet.SetInfo(m_WeapInfo);
	}

	// 일반적으로 패턴 자체가 완전히 끝나면 실행될 함수
	private void PatternEnd()
	{
		m_PatternProc = false;
		m_PatternDelay = true;

		ChangeAnim("Idle");

		m_BossComponent.Idle();
	}

	private void Pattern1()
	{
		m_PatternProc = true;
		m_P1 = true;

		ChangeAnim("Pattern1");

		m_BossComponent.Pattern1();
	}

	// 부채꼴 발사
	private void Pattern1Progress()
	{
		if (m_P1)
		{
			m_P1 = false;

			BulletSetting(true);

			// 총알과 총알 사이의 각도 간격
			m_P1AngleSteps = m_P1BulletAngle / (m_P1Bullets - 1);

			// 총알의 각도
			float bulletAngle = m_P1BulletAngle * -0.5f;

			for (int i = 0; i < m_P1Bullets; ++i)
			{
				m_Angle = bulletAngle + i * m_P1AngleSteps;

				FireBullet(m_Angle);
			}

			PlaySoundOneShot(m_Pattern1Audio);

			PatternEnd();
		}
	}

	private void Pattern2()
	{
		m_PatternProc = true;
		m_P2 = true;

		ChangeAnim("Pattern2");

		m_BossComponent.Pattern2();
	}

	// 16방향 발사
	private void Pattern2Progress()
	{
		if (m_P2)
		{
			m_P2 = false;

			BulletSetting(false);

			m_Angle = 0.0f;

			for (int i = 0; i < m_P2Bullets; ++i)
			{
				FireBullet(m_Angle, m_WeapInfo.m_FirstDist);

				m_Angle += m_P2AngleSteps;
			}

			PlaySoundOneShot(m_Pattern2Audio);

			PatternEnd();
		}
	}

	private void Pattern3()
	{
		m_PatternProc = true;

		ChangeAnim("Pattern3_Start");

		m_BossComponent.Pattern3_Start();
	}

	private void Pattern3Start()
	{
		m_P3 = true;

		BulletSetting(false);

		m_BossComponent.SetEnable(false);
		ChangeAnim("Pattern3_Progress");

		ChangeBulletSpeed();
	}

	private void Pattern3Progress()
	{
		if (m_P3)
		{
			m_P3Time += m_deltaTime;

			if (!m_IsPlayedP3Audio)
			{
				m_IsPlayedP3Audio = true;

				PlaySoundOneShot(m_Pattern3Audio);
			}

			if (!m_P3Change)
			{
				m_P3Change = true;

				if (m_P3Dir == Boss_Pattern3_Dir.Normal)
					m_Angle = 0f;

				else
					m_Angle = m_P3AngleSteps * 0.5f;

				for (int i = 0; i < m_P3Bullets; ++i)
				{
					FireBullet(m_Angle, m_WeapInfo.m_FirstDist);

					m_Angle += m_P3AngleSteps;
				}
			}

			else
			{
				m_P3ChangeTime += m_deltaTime;

				if (m_P3ChangeTime >= m_P3ChangeTimeMax)
				{
					m_P3ChangeTime = 0f;
					m_P3Change = false;

					m_P3Dir = m_P3Dir == Boss_Pattern3_Dir.Normal ? Boss_Pattern3_Dir.Cross : Boss_Pattern3_Dir.Normal;
				}
			}

			if (m_P3Time >= m_Pattern3TimeMax)
			{
				m_P3Time = 0f;
				m_P3 = false;
				m_P3EndNeedUpdate = true;
				m_IsPlayedP3Audio = false;

				ChangeBulletSpeed();

				m_BossComponent.SetEnable(true);
				ChangeAnim("Pattern3_End");

				m_BossComponent.Pattern3_End();
			}
		}
	}

	private void Pattern3End()
	{
		ChangeAnim("Idle");

		m_BossComponent.Idle();
	}

	private void Pattern3EndUpdate()
	{
		if (m_P3EndNeedUpdate)
		{
			if (!m_BossComponent.PlayPattern3End())
			{
				m_P3EndNeedUpdate = false;

				PatternEnd();
			}
		}
	}

	private void PatternProgress()
	{
		Pattern1Progress();
		Pattern2Progress();
		Pattern3Progress();

		Pattern3EndUpdate();
	}
}
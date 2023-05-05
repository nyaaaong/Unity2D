using UnityEngine;

public partial class Chair	:	Monster
{
	private class ChairBody
	{
		public Vector2 m_LeftTop = Vector2.zero;
		public Vector2 m_RightBottom = Vector2.zero;
		public Vector2 m_BodyHalfSize = Vector2.zero;

		public void SetBodySize(Vector2 bodyFullSize)
		{
			m_BodyHalfSize = bodyFullSize * 0.5f;
		}
	}

	[SerializeField]
	private GameObject m_Explosion = null;

	[SerializeField]
	private float m_Pattern3TimeMax = 3f;

	private ChairBody m_Body = null;
	private AudioClip m_Pattern1Audio = null;
	private AudioClip m_Pattern2Audio = null;
	private AudioClip m_Pattern3Audio = null;
	private AudioClip m_DieAudio = null;
	private GameObject m_EnemyBullet = null;
	private GameObject m_NewBulletObj = null;
	private GameObject m_BossObj = null;
	private Bullet m_NewBullet = null;
	private Bullet m_Bullet = null;
	private WeaponInfo m_WeapInfo = null;
	private Boss m_BossComponent = null;
	private Vector3 m_BulletPos = Vector3.zero;
	private float m_Angle = 0.0f;
	private float m_NewAngle = 0f;
	private bool m_CreateExp = false;
	private float m_ExpTime = 0f;
	private float m_ExpTimeMax = 0.4f; // 폭발 이펙트가 생성될 주기
	private float m_ExpDurTime = 0f;
	private float m_ExpDurTimeMax = 3f; // 폭발 이펙트를 생성할 총 시간

	private bool m_P1 = false;
	private const int m_P1Bullets = 5;
	private float m_P1BulletAngle = 90f; // 전체 총알의 부채꼴 각도
	private float m_P1AngleSteps = 0f;

	private bool m_P2 = false;
	private const int m_P2Bullets = 32; // 패턴2의 총알 수
	private const float m_P2AngleSteps = 360f / m_P2Bullets; // 패턴2의 각 총알 각도

	private bool m_P3 = false;
	private bool m_P3EndNeedUpdate = false;
	private bool m_P3Change = false;
	private bool m_IsPlayedP3Audio = false;
	private float m_P3Time = 0f;
	private float m_P3ChangeTime = 0f;
	private float m_P3ChangeTimeMax = 0.2f; // 총알 방향이 바뀔 간격 속도
	private float m_P3AngleSteps = 0f;
	private float m_P3Speed = 0f;
	private float m_P3SpeedMultiplier = 0.6f; // 총알 속도
	private int m_P3Bullets = 0;
	private Boss_Pattern3_Dir m_P3Dir = Boss_Pattern3_Dir.Normal;

	protected override void DestroyObject()
	{
		base.DestroyObject();

		UIManager.FadeIn();
	}

	protected override void TargetFound()
	{
		UIManager.EnableHealthBar(true);
	}

	private void ExplosionTimer()
	{
		if (m_CreateExp)
		{
			m_ExpDurTime += m_deltaTime;

			if (m_ExpDurTime >= m_ExpDurTimeMax)
				m_CreateExp = false;

			else
			{
				m_ExpTime += m_deltaTime;

				if (m_ExpTime >= m_ExpTimeMax)
				{
					m_ExpTime = 0f;

					ExplosionRandom();
				}
			}
		}
	}

	private void CalcBodyPos()
	{
		m_Body.m_LeftTop = new Vector2(m_Pos.x - m_Body.m_BodyHalfSize.x, m_Pos.y + m_Body.m_BodyHalfSize.y);
		m_Body.m_RightBottom = new Vector2(m_Pos.x + m_Body.m_BodyHalfSize.x, m_Pos.y - m_Body.m_BodyHalfSize.y);
	}

	private void ExplosionRandom()
	{
		Vector3 ExpPos = new Vector2();

		int Count = Random.Range(1, 3);

		for (int i = 0; i < Count; ++i)
		{
			ExpPos.x = Random.Range(m_Body.m_LeftTop.x, m_Body.m_RightBottom.x);
			ExpPos.y = Random.Range(m_Body.m_LeftTop.y, m_Body.m_RightBottom.y);

			GameObject newExplosion = Instantiate(m_Explosion);
			newExplosion.transform.position = ExpPos;
		}
	}

	public void DestroyChair()
	{
		Destroy();
	}

	protected override void DeathAnimCheck()
	{
		if (m_Death && !m_DeathAnimProc)
		{
			ChangeAnim("Death");

			CalcBodyPos();

			m_DeathAnimProc = true;
			m_CreateExp = true;

			m_BossComponent.SetEnable(true);
			m_BossComponent.Die();

			UIManager.EnableHealthBar(false);
		}
	}

	protected override void PlayDeathAudio()
	{
		PlaySoundOneShot(m_DieAudio);
	}

	protected override void Awake()
	{
		base.Awake();

		m_Boss = true;

		m_WeapInfo = WeaponManager.BossWeapInfo;
		m_EnemyBullet = WeaponManager.EnemyBullet;

		Transform bossTrs = null;
		int ChildMax = transform.childCount;

		for (int i = 0; i < ChildMax; ++i)
		{
			bossTrs = transform.GetChild(i);

			if (bossTrs.CompareTag("Boss"))
				m_BossObj = bossTrs.gameObject;
		}

		if (m_BossObj == null)
			Debug.LogError("if (m_BossObj == null)");

		m_BossComponent = m_BossObj.GetComponent<Boss>();

		if (m_BossComponent == null)
			Debug.LogError("if (m_BossComponent == null)");

		m_PatternList.Add(Pattern1);
		m_PatternList.Add(Pattern2);
		m_PatternList.Add(Pattern3);

		m_P3Bullets = m_P2Bullets;
		m_P3AngleSteps = m_P2AngleSteps;

		m_Pattern1Audio = AudioManager.BossPattern1;
		m_Pattern2Audio = AudioManager.BossPattern2;
		m_Pattern3Audio = AudioManager.BossPattern3;
		m_DieAudio = AudioManager.BossDie;

		m_PatternDelayTimeMax = 3f;

		if (m_Explosion == null)
			Debug.LogError("if (m_Explosion == null)");

		m_Body = new ChairBody();
		m_Body.SetBodySize(m_Collider[0].bounds.size);
	}

	protected override void Start()
	{
		base.Start();

		WeaponManager.SetBossWeapInfo(m_WeapInfo);

		m_Bullet = m_EnemyBullet.GetComponent<Bullet>();

		m_Bullet.Owner = Bullet_Owner.Monster;
		m_Bullet.SetInfo(m_WeapInfo);

		m_FireTime = m_WeapInfo.m_FireRate;

		m_P3Speed = m_WeapInfo.m_FireSpeed * m_P3SpeedMultiplier;
	}

	protected override void AfterUpdate()
	{
		base.AfterUpdate();

		if (!m_Death)
			PatternProgress();

		else
			ExplosionTimer();
	}
}
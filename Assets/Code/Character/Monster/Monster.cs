using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	[SerializeField]
	protected bool  m_DebugFire = false;

	protected delegate void Pattern();
	protected List<Pattern>    m_PatternList = null;

	protected AudioClip m_HitEffectAudio = null;
	protected AudioClip m_DeathEffectAudio = null;
	protected AudioClip[] m_DeathAudio = null;

	protected bool m_UseAlpha = false;

	private bool	m_Destroy = false;
	private float	m_Alpha = 1.0f;
	private float	m_FadeTime = 1.0f; // 사라질 시간
	private Color	m_Color;

	protected virtual void PlayDeathAudio()
	{
		int PlayIdx = Random.Range(0, 2);

		PlaySoundOneShot(m_DeathAudio[PlayIdx]);
	}

	public override void SetDamage(float dmg)
	{
		base.SetDamage(dmg);

		if (m_Death)
		{
			PlaySoundOneShot(m_DeathEffectAudio);
			PlayDeathAudio();
		}

		else
			PlaySoundOneShot(m_HitEffectAudio);
	}

	private void DestroyCheck()
	{
		if (m_Destroy)
		{
			if (m_UseAlpha)
			{
				m_Color.a -= m_Alpha * m_deltaTime;

				if (m_Color.a < 0.0f)
					m_Color.a = 0.0f;

				m_SR.color = m_Color;

				if (m_Color.a <= 0.0f)
					Destroy(gameObject);
			}

			else
				Destroy(gameObject);
		}
	}

	private void Destroy()
	{
		m_Destroy = true;
	}

	protected virtual void DeathAnimEvent()
	{
	}

	protected virtual void DeathAnimEndEvent()
	{
		Destroy();
	}

	protected virtual void DeathAnimCheck()
	{
		if (m_Death && !m_DeathAnimProc)
		{
			if (m_HandDir == Weapon_Hand.Left)
				m_Animator.SetTrigger("Death_Left");

			else
				m_Animator.SetTrigger("Death_Right");

			m_DeathAnimProc = true;
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Death || m_NoHit)
			return;

		if (collision.tag == "Bullet")
		{
			Bullet  bullet = collision.gameObject.GetComponent<Bullet>();

			if (bullet is null)
				Debug.LogError("if (bullet is null)");

			if (bullet.Owner == Bullet_Owner.Player)
			{
				SetDamage(bullet.Damage);

				m_HitAnim = true;
				m_HitAnimTime = 0.0f;
			}
		}
	}

	private void HitAnimCheck()
	{
		if (m_HitAnim)
		{
			if (m_SR.color == Color.white)
				m_SR.color = Color.red;

			else
			{
				m_HitAnimTime += m_deltaTime;

				if (m_HitAnimTime >= m_HitAnimTimeMax)
				{
					m_HitAnimTime = 0.0f;
					m_SR.color = Color.white;

					m_HitAnim = false;
				}
			}
		}
	}

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

		m_Color = m_SR.color;
		
		m_Alpha /= m_FadeTime;
	}

	protected override void Start()
	{
		base.Start();

		m_HitEffectAudio = Global.HitEffectAudio;
		m_DeathEffectAudio = Global.DeathEffectAudio;
		m_DeathAudio = Global.DeathAudio;
	}

	protected override void Update()
	{
		base.Update();

		Global.E2PData(this);

		HandCheck();
		PatternExec();
		HitAnimCheck();
		DeathAnimCheck();
		DestroyCheck();
	}
}

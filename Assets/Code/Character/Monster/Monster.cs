using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Monster : Character
{
	[SerializeField]
	protected bool m_DebugFire = false;

	protected delegate void Pattern();
	protected List<Pattern> m_PatternList = null;

	protected AudioClip m_HitEffectAudio = null;
	protected AudioClip m_DeathEffectAudio = null;
	protected AudioClip[] m_DeathAudio = null;

	protected bool m_UseAlpha = true;

	private bool m_Destroy = false;
	private float m_Alpha = 1.0f;
	private float m_FadeTime = 2.0f; // 사라질 시간
	private Color m_Color;

	private float m_Percent = 0.0f;
	private bool m_CreateItem = false;

	protected float m_UpdateDist = 3f;

	protected bool m_PatternProc = false;
	protected bool m_PatternDelay = false;
	protected float m_PatternDelayTime = 2f;
	protected float m_PatternDelayTimeMax = 2f;

	protected float m_MoveTime = 0f;
	protected float m_MoveTimeMax = 1f;
	protected Monster_Dir m_MoveDir = Monster_Dir.End;
	protected Vector2 m_Dir = Vector2.zero;
	protected bool m_MovePattern = false;

	protected float m_FirePatternTime = 0f;
	protected float m_FirePatternTimeMax = 0f;
	protected bool m_FirePattern = false;

	protected bool m_FollowPlayer = false;

	protected bool m_Boss = false;
	protected float m_FollowDist = 12f; // 보스와 플레이어의 사이 거리, 만약 이 거리를 빠져나가면 보스가 추적한다

	public bool PlayAnim(string name)
	{
		AnimatorStateInfo curAnim = m_Animator.GetCurrentAnimatorStateInfo(0);

		if (!curAnim.IsName(name))
			return false;

		else if (curAnim.normalizedTime < 1)
			return false;

		return true;
	}

	protected void ChangeAnim(string anim)
	{
		if (!m_Boss)
		{
			if (m_HandDir == Weapon_Hand.Left)
				m_AnimName = anim + "_Left";

			else
				m_AnimName = anim + "_Right";
		}

		else
			m_AnimName = anim;

		if (m_PrevAnimName != m_AnimName) // 이걸 하지 않으면 애니메이션이 굉장히 빨라진다.
		{
			m_Animator.SetTrigger(m_AnimName);

			m_PrevAnimName = m_AnimName;
		}
	}

	protected void MovePattern()
	{
		m_PatternProc = true;
		m_MovePattern = true;

		m_MoveDir = (Monster_Dir)Random.Range(0, (int)Monster_Dir.End);
		m_MoveTimeMax = Random.Range(0.5f, 1.5f);

		ChangeAnim("Walk");
	}

	protected void RangeCheck()
	{
		if (!m_Boss)
		{
			if (m_IsWall || m_TargetDist > m_WeapRange)
			{
				m_FollowPlayer = true;
				m_Fire = false;
			}

			else
			{
				m_FollowPlayer = false;
				m_Fire = true;
			}
		}

		else
		{
			if (m_TargetDist > m_FollowDist)
				m_FollowPlayer = true;

			else
				m_FollowPlayer = false;
		}
	}

	protected void FollowPlayer()
	{
		if (m_FollowPlayer)
		{
			m_Rig.velocity = m_TargetDir * m_Info.m_MoveSpeed;

			if (!m_Boss)
				ChangeAnim("Walk");
		}

		else
		{
			m_Rig.velocity = Vector2.zero;

			if (!m_Boss)
				ChangeAnim("Idle");
		}
	}

	protected void UpdateMovePattern()
	{
		if (m_MovePattern)
		{
			m_MoveTime += m_deltaTime;

			switch (m_MoveDir)
			{
				case Monster_Dir.Up:
					m_Dir = Vector2.up;
					break;
				case Monster_Dir.Left:
					m_Dir = Vector2.left;
					break;
				case Monster_Dir.Right:
					m_Dir = Vector2.right;
					break;
				case Monster_Dir.Down:
					m_Dir = Vector2.down;
					break;
				case Monster_Dir.UpLeft:
					m_Dir = Vector2.up + Vector2.left;
					break;
				case Monster_Dir.UpRight:
					m_Dir = Vector2.up + Vector2.right;
					break;
				case Monster_Dir.DownLeft:
					m_Dir = Vector2.down + Vector2.left;
					break;
				case Monster_Dir.DownRight:
					m_Dir = Vector2.down + Vector2.right;
					break;
			}

			m_Rig.velocity = m_Dir * m_Info.m_MoveSpeed;

			if (m_MoveTime >= m_MoveTimeMax)
			{
				m_PatternDelay = true;

				m_PatternProc = false;
				m_MovePattern = false;

				m_MoveTime = 0f;
				m_Rig.velocity = Vector2.zero;

				ChangeAnim("Idle");
			}
		}
	}

	private void CreateItem()
	{
		if (m_CreateItem)
			return;

		m_CreateItem = true;

		m_Percent = Random.Range(0.0f, 100.0f);

		if (m_Percent <= Global.LootRate)
			ItemManager.CreateItem(transform.position);
	}

	protected virtual void PlayDeathAudio()
	{
		int PlayIdx = Random.Range(0, 2);

		PlaySoundOneShot(m_DeathAudio[PlayIdx]);
	}

	public override void SetDamage(float dmg)
	{
		base.SetDamage(dmg);

		m_HitAnim = true;
		m_HitAnimTime = 0.0f;

		if (m_Death)
		{
			StopSound();

			if (!m_Boss)
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
			if (!m_Boss)
				CreateItem();

			if (m_UseAlpha)
			{
				m_Color = m_SR.color;
				m_Color.a -= m_Alpha;

				if (m_Color.a < 0.0f)
					m_Color.a = 0.0f;

				m_SR.color = m_Color;

				if (m_Color.a <= 0.0f)
					DestroyObject();
			}

			else
				DestroyObject();
		}
	}

	private void DestroyObject()
	{
		int Size = transform.childCount;

		for (int i = 0; i < Size; ++i)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		Destroy(gameObject);
	}

	protected void Destroy()
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
			ChangeAnim("Death");

			m_DeathAnimProc = true;
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

	private void PatternDelay()
	{
		m_PatternDelayTime += m_deltaTime;

		if (m_PatternDelayTime >= m_PatternDelayTimeMax)
		{
			m_PatternDelay = false;
			m_PatternDelayTime = 0f;
		}
	}

	private void PatternExec()
	{
		if (m_PatternProc)
			return;

		int idx = m_PatternList.Count;

		if (idx == 0)
			return;

		idx = Random.Range(0, idx);

		m_PatternList[idx]();
	}

	private void UpdatePattern()
	{
		UpdateMovePattern();
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

		m_Alpha = m_deltaTime / m_FadeTime;

		m_UpdateDist = Global.UpdateDist;
	}

	protected override void Start()
	{
		base.Start();

		m_HitEffectAudio = Global.HitEffectAudio;
		m_DeathEffectAudio = Global.DeathEffectAudio;

		if (!m_Boss)
			m_DeathAudio = Global.DeathAudio;

		else
			m_PatternDelayTime = 0f;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		Global.E2PData(this);

		RangeCheck();
	}

	protected override void Update()
	{
		base.Update();

		if (m_UpdateDist >= m_TargetDist || m_Update)
		{
			m_Update = true;

			if (!m_Boss)
			{
				FollowPlayer();

				if (!m_FollowPlayer)
				{
					if (m_PatternDelay)
						PatternDelay();

					else
					{
						PatternExec();
						UpdatePattern();
					}
				}

				HandCheck();
			}

			else
			{
				if (m_PatternDelay)
				{
					PatternDelay();
					FollowPlayer(); // 패턴이 실행중이지 않을 때만 플레이어 추적
				}

				else
				{
					PatternExec();
					UpdatePattern();
				}
			}

			HitAnimCheck();
			DeathAnimCheck();
			DestroyCheck();
		}
	}
}

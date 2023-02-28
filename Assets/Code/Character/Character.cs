using System;
using UnityEngine;

#pragma warning disable 414

[Serializable]
public class CharInfo
{
	public float m_HP = 100.0f;
	private float m_HPMax = 100.0f;
	public float m_MoveSpeed = 1.0f;

	public float HPMax { get { return m_HPMax; } }

	public void Init()
	{
		m_HPMax = m_HP;
	}
}

public class Character : MonoBehaviour
{
	[SerializeField]
	protected CharInfo              m_Info = new CharInfo();

	protected float                 m_deltaTime = 0.0f;
	protected Character_Status      m_Status = Character_Status.Idle;
	protected Weapon_Hand           m_HandDir = Weapon_Hand.None;   // 어느 쪽 손을 보일 것 인지
	protected Weapon_RenderOrder    m_WeapRenderOrder = Weapon_RenderOrder.Front;   // 캐릭터 기준 총이 보여질지 가려질지
	protected Weapon_Type_Player           m_WeapType = Weapon_Type_Player.End;
	protected string                m_AnimName = "";
	protected string                m_PrevAnimName = "";
	protected bool                  m_Move = false;
	protected bool                  m_NoHit = false;
	protected bool                  m_HideWeapon = false;
	protected bool                  m_Death = false;
	protected bool                  m_Fire = false;
	protected Animator              m_Animator = null;
	protected AudioSource           m_Audio = null;

	public Weapon_Hand HandDir { get { return m_HandDir; } }
	public Weapon_RenderOrder WeapRenderOrder { get { return m_WeapRenderOrder; } }
	public Weapon_Type_Player Type { get { return m_WeapType; } }
	public bool NoHit { get { return m_NoHit; } }
	public bool HideWeapon { get { return m_HideWeapon; } }
	public bool Death { get { return m_Death; } }
	public bool Fire { get { return m_Fire; } }


	public virtual void SetDamage(float dmg)
	{
		if (m_Death)
			return;

		m_Info.m_HP -= dmg;

		if (m_Info.m_HP <= 0.0f)
		{
			m_Info.m_HP = 0.0f;
			m_Death = true;
		}
	}

	public virtual void Heal(float value)
	{
		if (m_Death)
			return;

		m_Info.m_HP += value;

		if (m_Info.m_HP > m_Info.HPMax)
			m_Info.m_HP = m_Info.HPMax;
	}

	protected void PlaySound(AudioClip clip, bool isLoop = false)
	{
		if (clip is null)
			Debug.LogError("if (clip is null)");

		m_Audio.clip = clip;
		m_Audio.loop = isLoop;
		m_Audio.Play();
	}

	protected virtual void Awake()
	{
		m_Animator = GetComponent<Animator>();

		if (m_Animator is null)
			Debug.LogError("if (m_Animator is null)");

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio is null)
			Debug.LogError("if (m_Audio is null)");

		m_Info.Init();
	}

	protected virtual void Start()
	{
		m_Audio.volume = Global.EffectVolume;
	}

	protected virtual void Update()
	{
		m_deltaTime = Time.deltaTime;
	}
}

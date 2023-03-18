using System;
using UnityEngine;

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
	protected CharInfo                              m_Info = new CharInfo();

	protected float                                 m_deltaTime = 0.0f;
	protected float                                 m_TargetAngle = 0.0f;
	protected float                                 m_FireTime = 0.0f;
	protected float                                 m_HitAnimTime = 0.0f;
	protected float                                 m_HitAnimTimeMax = 0.1f;
	protected Character_Status                      m_Status = Character_Status.Idle;
	protected Weapon_Hand                           m_HandDir = Weapon_Hand.None;       // 어느 쪽 손을 보일 것 인지
	protected Weapon_RenderOrder                    m_WeapRenderOrder = Weapon_RenderOrder.Front;       // 캐릭터 기준 총이 보여질지 가려질지
	protected Weapon_Type_Player                    m_WeapType = Weapon_Type_Player.End;
	protected string                                m_AnimName = "";
	protected string                                m_PrevAnimName = "";
	protected bool                                  m_Move = false;
	protected bool                                  m_NoHit = false;
	protected bool                                  m_HideWeapon = false;
	protected bool                                  m_Death = false;
	protected bool                                  m_Fire = false;
	protected bool                                  m_SpreadBullet = false; // ShotgunKin_B 전용
	protected bool                                  m_HitAnim = false;
	protected bool                                  m_DeathAnimProc = false;
	protected Animator                              m_Animator = null;
	protected AudioSource                           m_Audio = null;
	protected SpriteRenderer                        m_SR = null;
	protected Rigidbody2D                           m_Rig = null;
	protected Vector3                               m_TargetDir = Vector3.zero;

	public Color Color { get { return m_SR.color; } }
	public bool SpreadBullet { get { return m_SpreadBullet; } set { m_SpreadBullet = value; } }
	public Weapon_Hand HandDir { get { return m_HandDir; } }
	public Weapon_RenderOrder WeapRenderOrder { get { return m_WeapRenderOrder; } }
	public Weapon_Type_Player Type { get { return m_WeapType; } }
	public bool NoHit { get { return m_NoHit; } }
	public bool HideWeapon { get { return m_HideWeapon; } }
	public bool Death { get { return m_Death; } }
	public bool Fire { get { return m_Fire; } }
	public float TargetAngle { get { return m_TargetAngle; } set { m_TargetAngle = value; } }
	public Vector3 TargetDir { get { return m_TargetDir; } set { m_TargetDir = value; } }
	public float FireTime { get { return m_FireTime; } set { m_FireTime = value; } }
	public bool Visible { get { return m_SR.enabled; } }
	public bool DeathAnimProc { get { return m_DeathAnimProc; } }
	public Vector2 RigidBodyPos { get { return m_Rig.position; } }
	public bool IsMove { get { return m_Move; } }

	public virtual void SetDamage(float dmg)
	{
		if (m_Death || m_NoHit)
			return;

		m_Info.m_HP -= dmg;

		if (m_Info.m_HP <= 0.0f)
		{
			m_Info.m_HP = 0.0f;
			m_Death = true;
			m_Fire = false;
		}
	}

	protected void PlaySound(AudioClip clip, bool isLoop = false)
	{
		if (clip == null)
			Debug.LogError("if (clip == null)");

		if (m_Audio.clip != clip)
		{
			m_Audio.clip = clip;
			m_Audio.loop = isLoop;
		}

		m_Audio.Play();
	}

	protected void PlaySoundOneShot(AudioClip clip)
	{
		m_Audio.PlayOneShot(clip);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Death || m_NoHit)
			return;
	}

	protected virtual void Awake()
	{
		m_Animator = GetComponent<Animator>();

		if (m_Animator == null)
			Debug.LogError("if (m_Animator == null)");

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio == null)
			Debug.LogError("if (m_Audio == null)");

		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR == null)
			Debug.LogError("if (m_SR == null)");

		m_Rig = GetComponent<Rigidbody2D>();

		if (m_Rig == null)
			Debug.LogError("if (m_rigid  == null)");

		m_Info.Init();
	}

	protected virtual void Start()
	{
		m_Audio.volume = Global.EffectVolume;
	}

	protected virtual void Update()
	{
		m_deltaTime = Time.deltaTime;

		m_FireTime += m_deltaTime;
	}

	protected virtual void FixedUpdate()
	{

	}

	protected virtual void LateUpdate()
	{

	}
}

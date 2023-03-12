using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private bool							m_DebugHasAllWeap = false;
	[SerializeField]
	private float							m_DodgeSpeed = 5.0f;
	[SerializeField]
	AudioClip								m_DodgeClip = null;
	[SerializeField]
	AudioClip                               m_WeapLootClip = null;
	[SerializeField]
	AudioClip                               m_HeartLootClip = null;
	[SerializeField]
	private float                           m_HealValue = 10.0f;

	private bool[]							m_Dir = null;
	private bool[]							m_LastDir = null;
	private bool[]							m_KeyDown = null;
	private bool[]							m_HasWeapon = null;
	private bool							m_KeyLock = false;
	private bool							m_WeaponChange = false;
	private float							m_BlinkTime = 0.0f;
	private float							m_BlinkTimeMax = 0.1f;

	private const int						UP = (int)Player_Dir.Up;
	private const int						LEFT = (int)Player_Dir.Left;
	private const int						RIGHT = (int)Player_Dir.Right;
	private const int						DOWN = (int)Player_Dir.Down;
	private const int						END = (int)Player_Dir.End;

	public bool WeaponChange { get { return m_WeaponChange; } set { m_WeaponChange = value; } }

	public bool HasWeaponAll()
	{
		int Size = (int)Weapon_Type_Player.End;

		for (int i = 0; i < Size; ++i)
		{
			if (!m_HasWeapon[i])
				return false;
		}

		return true;
	}

	public bool HasWeapon(int type)
	{
		return m_HasWeapon[type];
	}
	
	public void AddWeapon(Weapon_Type_Player type)
	{
		m_HasWeapon[(int)type] = true;

		PlaySoundOneShot(m_WeapLootClip);
	}

	public virtual void AddHeart()
	{
		if (m_Death)
			return;

		m_Info.m_HP += m_HealValue;

		if (m_Info.m_HP > m_Info.HPMax)
			m_Info.m_HP = m_Info.HPMax;

		PlaySoundOneShot(m_HeartLootClip);
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Death || m_NoHit)
			return;

		if (collision.tag == "Bullet")
		{
			Bullet	bullet = collision.gameObject.GetComponent<Bullet>();

			if (bullet == null)
				Debug.LogError("if (bullet == null)");

			if (bullet.Owner == Bullet_Owner.Monster)
			{
				SetDamage(bullet.Damage);

				m_NoHit = true;
				m_HitAnim = true;
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();

		if (m_DodgeClip == null)
			Debug.LogError("if (m_DodgeClip == null)");

		if (m_WeapLootClip == null)
			Debug.LogError("if (m_WeapLootClip == null)");

		if (m_HeartLootClip == null)
			Debug.LogError("if (m_HeartLootClip == null)");

		m_Dir = new bool[END];
		m_LastDir = new bool[END];
		m_KeyDown = new bool[END];
		m_HasWeapon = new bool[(int)Weapon_Type_Player.End];

		m_HasWeapon[(int)Weapon_Type_Player.Pistol] = true; // 기본으로 권총 장착

		/* 디버그용 */
		if (m_DebugHasAllWeap)
		{
			for (int i = 0; i < (int)Weapon_Type_Player.End; ++i)
			{
				m_HasWeapon[i] = true;
			}
		}

		m_Dir[RIGHT] = true;
		m_LastDir[RIGHT] = true;

		m_HitAnimTimeMax = 2.0f;
	}

	protected override void Update()
	{
		base.Update();

		MoveKeyCheck();
		WeaponKeyCheck();
		AnimCheck();
		HitAnimCheck();
	}
}

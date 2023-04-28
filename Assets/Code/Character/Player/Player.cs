using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private bool m_DebugHasAllWeap = false;
	[SerializeField]
	AudioClip m_DodgeClip = null;
	[SerializeField]
	AudioClip m_WeapLootClip = null;
	[SerializeField]
	AudioClip m_HeartLootClip = null;
	[SerializeField]
	private float m_HealValue = 10.0f;
	[SerializeField]
	private bool m_BossStart = false;

	private Player_Status m_Status = Player_Status.Idle;
	private bool[] m_Dir = null;
	private bool[] m_HasWeapon = null;
	private bool m_KeyLock = false;
	private bool m_WeaponChange = false;
	private bool m_DodgeEnd = false; // 닷지가 끝나기 직전
	private float m_DodgeSpeed = 5.0f;
	private float m_InputX = 0.0f;
	private float m_InputXPrev = 0.0f;
	private float m_InputY = 0.0f;
	private float m_InputYPrev = 0.0f;
	private float m_BlinkTime = 0.0f;
	private float m_BlinkTimeMax = 0.1f;
	private Vector3 m_MovePos = Vector3.zero;
	private Vector2 m_P2MDist = Vector2.zero;

	private bool UP { get { return m_InputYPrev == 1.0f; } }
	private bool DOWN { get { return m_InputYPrev == -1.0f; } }
	private bool LEFT { get { return m_InputXPrev == -1.0f; } }
	private bool RIGHT { get { return m_InputXPrev == 1.0f; } }

	public bool WeaponChange { get { return m_WeaponChange; } set { m_WeaponChange = value; } }
	public Vector3 MovePos { set { gameObject.transform.position = value; } }

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

	public bool HasWeapon(Item_Type type)
	{
		int idx = 0;

		switch (type)
		{
			case Item_Type.Rifle:
				idx = (int)Weapon_Type_Player.Rifle;
				break;
			case Item_Type.Sniper:
				idx = (int)Weapon_Type_Player.Sniper;
				break;
		}

		return m_HasWeapon[idx];
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

	public override void SetDamage(float dmg)
	{
		base.SetDamage(dmg);

		m_NoHit = true;
		m_HitAnim = true;
	}

	private void Calc()
	{
		m_P2MDist = InputManager.MouseScreenPos - m_Rig.position;
		m_TargetDir = m_P2MDist.normalized;
		m_TargetAngle = Mathf.Atan2(m_P2MDist.y, m_P2MDist.x) * Mathf.Rad2Deg;
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

		m_Dir = new bool[(int)Player_Dir.End];
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

		m_Dir[(int)Player_Dir.Right] = true;

		m_HitAnimTimeMax = 2.0f;

		Vector3 playerPos = new Vector3();

		if (m_BossStart)
		{
			playerPos.x = 66.58f;
			playerPos.y = 1.63f;
		}

		gameObject.transform.position = playerPos;

		m_DodgeSpeed = m_Info.m_MoveSpeed * 1.3f;
	}

	protected override void Start()
	{
		base.Start();

		EquipWeapon(Weapon_Type_Player.Pistol);

		AudioManager.PlayBGM(BGM_Type.Main);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		Move();
	}

	protected override void BeforeUpdate()
	{
		base.BeforeUpdate();

		InputCheck();
		Dodge();
	}

	protected override void MiddleUpdate()
	{
		base.MiddleUpdate();

		if (!m_Death)
		{
			Calc();
			WeaponKeyCheck();
			AnimCheck();
			HitAnimCheck();
		}
	}
}

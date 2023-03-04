using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private bool                m_DebugHasAllWeap = false;
	[SerializeField]
	private float               m_DodgeSpeed = 5.0f;
	[SerializeField]
	AudioClip                   m_DodgeClip = null;

	private bool[]              m_Dir = null;
	private bool[]              m_LastDir = null;
	private bool[]              m_KeyDown = null;
	private bool[]              m_HasWeapon = null;
	private bool                m_KeyLock = false;
	private bool                m_WeaponChange = false;

	private const int           UP = (int)Player_Dir.Up;
	private const int           LEFT = (int)Player_Dir.Left;
	private const int           RIGHT = (int)Player_Dir.Right;
	private const int           DOWN = (int)Player_Dir.Down;
	private const int           END = (int)Player_Dir.End;

	public bool WeaponChange { get { return m_WeaponChange; } set { m_WeaponChange = value; } }

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Death)
			return;

		if (collision.tag == "Bullet")
		{
			Bullet  bullet = collision.gameObject.GetComponent<Bullet>();

			if (bullet is null)
				Debug.LogError("if (bullet is null)");

			if (bullet.Owner == Bullet_Owner.Monster)
			{
				SetDamage(bullet.Damage);
				// 히트 애니메이션 (잠시동안 틴트 빨간색)
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();

		m_Dir = new bool[END];
		m_LastDir = new bool[END];
		m_KeyDown = new bool[END];
		m_HasWeapon = new bool[(int)Weapon_Type_Player.End];

		if (m_DebugHasAllWeap)
		{
			for (int i = 0; i < (int)Weapon_Type_Player.End; ++i)
			{
				m_HasWeapon[i] = true;
			}
		}

		m_Dir[RIGHT] = true;
		m_LastDir[RIGHT] = true;
	}

	protected override void Update()
	{
		base.Update();

		MoveKeyCheck();
		WeaponKeyCheck();
		AnimCheck();
	}
}

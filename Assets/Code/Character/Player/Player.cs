using UnityEngine;

public partial class Player : Character
{
	[SerializeField]
	private bool                m_DebugHasAllWeap = false;
	[SerializeField]
	private float               m_DodgeSpeed = 5.0f;
	[SerializeField]
	AudioClip                   m_DodgeClip = null;

	private float               m_P2MAngle = 0.0f;
	private bool[]              m_Dir = null;
	private bool[]              m_LastDir = null;
	private bool[]              m_KeyDown = null;
	private bool[]              m_HasWeapon = null;
	private bool                m_KeyLock = false;

	private const int           UP = (int)Player_Dir.Up;
	private const int           LEFT = (int)Player_Dir.Left;
	private const int           RIGHT = (int)Player_Dir.Right;
	private const int           DOWN = (int)Player_Dir.Down;
	private const int           END = (int)Player_Dir.End;

	protected override void Awake()
	{
		base.Awake();

		m_Dir = new bool[END];
		m_LastDir = new bool[END];
		m_KeyDown = new bool[END];
		m_HasWeapon = new bool[(int)Weapon_Type.End];

		if (m_DebugHasAllWeap)
		{
			for (int i = 0; i < (int)Weapon_Type.End; ++i)
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

		if (m_WeapType != Weapon_Type.End)
			m_P2MAngle = Global.P2MAngle;

		MoveKeyCheck();
		WeaponKeyCheck();
		AnimCheck();
	}
}

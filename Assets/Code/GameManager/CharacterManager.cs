using UnityEngine;

public class CharacterManager : Global
{
	[SerializeField]
	private float m_UpdateDist = 10f;
	[SerializeField]
	private GameObject m_Boss = null;

	static private CharacterManager m_Inst = null;

	private Player m_Player = null;
	private Vector2 m_PlayerPos = Vector2.zero;
	private Vector3 m_PlayerPos3D = Vector3.zero;
	private Vector2 m_EnemyPos = Vector2.zero;
	private Vector2 m_E2PDist = Vector2.zero;

	public static float UpdateDist { get { return m_Inst.m_UpdateDist; } }
	public static Vector3 PlayerPos3D { get { return m_Inst.m_PlayerPos3D; } }
	public static GameObject Boss { get { return m_Inst.m_Boss; } }
	public static Player Player { get { return m_Inst.m_Player; } }

	public static bool PlayerHasWeapon(Item_Type type)
	{
		return m_Inst.m_Player.HasWeapon(type);
	}

	public static bool PlayerHasWeaponAll()
	{
		return m_Inst.m_Player.HasWeaponAll();
	}

	public static void PlayerAddWeapon(Weapon_Type_Player type)
	{
		m_Inst.m_Player.AddWeapon(type);
	}

	public static void PlayerAddHeart()
	{
		m_Inst.m_Player.AddHeart();
	}

	public static void E2PData(Character enemy)
	{
		m_Inst.m_EnemyPos = enemy.RigidBodyPos;
		m_Inst.m_E2PDist = m_Inst.m_PlayerPos - m_Inst.m_EnemyPos;

		enemy.TargetPos = m_Inst.m_PlayerPos;
		enemy.TargetDir = m_Inst.m_E2PDist.normalized;
		enemy.TargetAngle = Mathf.Atan2(enemy.TargetDir.y, enemy.TargetDir.x) * Mathf.Rad2Deg;
		enemy.TargetDist = Vector2.Distance(m_Inst.m_PlayerPos, m_Inst.m_EnemyPos);
	}

	private void Awake()
	{
		m_Inst = this;

		GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");

		if (PlayerObj == null)
			Debug.LogError("if (PlayerObj == null)");

		m_Player = PlayerObj.GetComponent<Player>();

		if (m_Player == null)
			Debug.LogError("if (m_Player == null)");

		if (m_Boss == null)
			Debug.LogError("if (m_Boss == null)");
	}

	protected override void BeforeUpdate()
	{
		base.BeforeUpdate();

		m_PlayerPos3D = m_Player.RigidBodyPos3D;
		m_PlayerPos = m_PlayerPos3D;
	}
}

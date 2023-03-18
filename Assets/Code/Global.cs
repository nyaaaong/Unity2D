using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class WeaponInfo
{
	[SerializeField]
	public string m_Name;
	public float m_Damage;
	public float m_FireRate;
	public float m_FireSpeed;
	public float m_FireRange;
	public float m_FirstDist;
	public bool m_Pierce;
}

public class Global : MonoBehaviour
{
	[SerializeField]
	private Camera				m_MainCamera = null;
	[SerializeField]
	private float				m_EffectVolume = 1.0f;
	[SerializeField]
	private float				m_BGMVolume = 1.0f;

	[SerializeField]
	private WeaponInfo[]		m_WeapInfo = new WeaponInfo[(int)Weapon_Type_Player.End];
	[SerializeField]
	private WeaponInfo[]		m_WeapMonsterInfo = new WeaponInfo[(int)Weapon_Type_Monster.End];
	[SerializeField]
	private GameObject          m_PlayerBullet = null;
	[SerializeField]
	private GameObject          m_EnemyBullet = null;

	[SerializeField]
	private AudioClip m_HitEffectAudio = null;
	[SerializeField]
	private AudioClip m_DeathEffectAudio = null;
	[SerializeField]
	private AudioClip[] m_DeathAudio = null;

	[SerializeField]
	private float   m_LootRate = 20.0f;

	[SerializeField]
	private Tilemap m_TileMap = null;

	private static Global	m_Inst = null;
	private GameObject		m_PlayerObj = null;
	private Player			m_Player = null;
	private Vector2			m_P2MDist = Vector2.zero;
	private Vector2			m_E2PDist = Vector2.zero;
	private Vector2         m_MousePos = Vector2.zero;
	private Cam             m_Cam = null;

	public static AudioClip HitEffectAudio { get { return m_Inst.m_HitEffectAudio; } }
	public static AudioClip DeathEffectAudio { get { return m_Inst.m_DeathEffectAudio; } }
	public static AudioClip[] DeathAudio { get { return m_Inst.m_DeathAudio; } }
	public static float EffectVolume { get { return m_Inst.m_EffectVolume; } }
	public static Player Player { get { return m_Inst.m_Player; } }
	public static WeaponInfo Pistol { get { return m_Inst.m_WeapInfo[(int)Weapon_Type_Player.Pistol]; } }
	public static float LootRate { get { return m_Inst.m_LootRate; } }
	public static GameObject PlayerBullet { get { return m_Inst.m_PlayerBullet; } }
	public static GameObject EnemyBullet { get { return m_Inst.m_EnemyBullet; } }
	public static Vector2 MousePos { get { return m_Inst.m_MousePos; } }
	public static Cam Camera { get { return m_Inst.m_Cam; } }
	public static Tilemap TileMap { get { return m_Inst.m_TileMap; } }

	public static Vector2 ConvertDir(float angle)
	{
		angle *= Mathf.Deg2Rad;

		return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
	}

	public static void SetWeaponInfo(in WeaponInfo info, Weapon_Type_Player type)
	{
		info.m_Damage = m_Inst.m_WeapInfo[(int)type].m_Damage;
		info.m_FireRate = m_Inst.m_WeapInfo[(int)type].m_FireRate;
		info.m_FireSpeed = m_Inst.m_WeapInfo[(int)type].m_FireSpeed;
		info.m_FireRange = m_Inst.m_WeapInfo[(int)type].m_FireRange;
		info.m_FirstDist = m_Inst.m_WeapInfo[(int)type].m_FirstDist;
		info.m_Pierce = m_Inst.m_WeapInfo[(int)type].m_Pierce;
	}

	public static void SetWeaponInfo(in WeaponInfo info, Weapon_Type_Monster type)
	{
		info.m_Damage = m_Inst.m_WeapMonsterInfo[(int)type].m_Damage;
		info.m_FireRate = m_Inst.m_WeapMonsterInfo[(int)type].m_FireRate;
		info.m_FireSpeed = m_Inst.m_WeapMonsterInfo[(int)type].m_FireSpeed;
		info.m_FireRange = m_Inst.m_WeapMonsterInfo[(int)type].m_FireRange;
		info.m_FirstDist = m_Inst.m_WeapMonsterInfo[(int)type].m_FirstDist;
		info.m_Pierce = m_Inst.m_WeapMonsterInfo[(int)type].m_Pierce;
	}

	public static void E2PData(Character enemy)
	{
		m_Inst.m_E2PDist = m_Inst.m_Player.transform.position - enemy.transform.position;

		enemy.TargetDir = m_Inst.m_E2PDist.normalized;
		enemy.TargetAngle = Mathf.Atan2(m_Inst.m_E2PDist.y, m_Inst.m_E2PDist.x) * Mathf.Rad2Deg;
	}

	private void Awake()
	{
		m_Inst = this;

		m_PlayerObj = GameObject.Find("Player");

		if (m_PlayerObj == null)
			Debug.LogError("if (m_PlayerObj == null)");

		m_Player = m_PlayerObj.GetComponent<Player>();

		if (m_Player == null)
			Debug.LogError("if (m_Player == null)");

		if (m_WeapInfo.Length > (int)Weapon_Type_Player.End)
			Debug.LogError("if (m_WeapInfo.Length > Weapon_Type_Player.End)");

		if (m_WeapMonsterInfo.Length > (int)Weapon_Type_Monster.End)
			Debug.LogError("if (m_WeapMonsterInfo.Length > Weapon_Type_Monster.End)");

		if (m_HitEffectAudio == null)
			Debug.LogError("if (m_HitEffectAudio == null)");

		if (m_DeathEffectAudio == null)
			Debug.LogError("if (m_DeathEffectAudio == null)");

		if (m_DeathAudio == null)
			Debug.LogError("if (m_DeathAudio == null)");

		if (m_PlayerBullet == null)
			Debug.LogError("if (m_PlayerBullet == null)");

		if (m_EnemyBullet == null)
			Debug.LogError("if (m_EnemyBullet == null)");

		m_Cam = m_MainCamera.GetComponent<Cam>();

		if (m_Cam == null)
			Debug.Log("if (m_Cam == null)");

		if (m_TileMap == null)
			Debug.Log("if (m_TileMap == null)");
	}

	private void Update()
	{
		m_MousePos = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
		//Debug.Log("m_MousePos : " + m_MousePos);
		m_P2MDist = m_MousePos - m_Player.RigidBodyPos;
		m_Player.TargetDir = m_P2MDist.normalized;

		m_Player.TargetAngle = Mathf.Atan2(m_P2MDist.y, m_P2MDist.x) * Mathf.Rad2Deg;
	}
}

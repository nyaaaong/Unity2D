using System;
using UnityEngine;

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

public class WeaponManager : Global
{
	[SerializeField]
	private WeaponInfo[] m_WeapInfo = new WeaponInfo[(int)Weapon_Type_Player.End];
	[SerializeField]
	private WeaponInfo[] m_WeapMonsterInfo = new WeaponInfo[(int)Weapon_Type_Monster.End];
	[SerializeField]
	private WeaponInfo m_WeapBossInfo = null;
	[SerializeField]
	private GameObject m_PlayerBullet = null;
	[SerializeField]
	private GameObject m_EnemyBullet = null;

	static private WeaponManager m_Inst = null;

	public static WeaponInfo Pistol { get { return m_Inst.m_WeapInfo[(int)Weapon_Type_Player.Pistol]; } }
	public static WeaponInfo BossWeapInfo { get { return m_Inst.m_WeapBossInfo; } }
	public static GameObject PlayerBullet { get { return m_Inst.m_PlayerBullet; } }
	public static GameObject EnemyBullet { get { return m_Inst.m_EnemyBullet; } }

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

	public static void SetBossWeapInfo(in WeaponInfo info)
	{
		info.m_Damage = m_Inst.m_WeapBossInfo.m_Damage;
		info.m_FireRate = m_Inst.m_WeapBossInfo.m_FireRate;
		info.m_FireSpeed = m_Inst.m_WeapBossInfo.m_FireSpeed;
		info.m_FireRange = m_Inst.m_WeapBossInfo.m_FireRange;
		info.m_FirstDist = m_Inst.m_WeapBossInfo.m_FirstDist;
		info.m_Pierce = m_Inst.m_WeapBossInfo.m_Pierce;
	}

	private void Awake()
	{
		m_Inst = this;

		if (m_WeapInfo.Length > (int)Weapon_Type_Player.End)
			Debug.LogError("if (m_WeapInfo.Length > Weapon_Type_Player.End)");

		if (m_WeapMonsterInfo.Length > (int)Weapon_Type_Monster.End)
			Debug.LogError("if (m_WeapMonsterInfo.Length > Weapon_Type_Monster.End)");

		if (m_PlayerBullet == null)
			Debug.LogError("if (m_PlayerBullet == null)");

		if (m_EnemyBullet == null)
			Debug.LogError("if (m_EnemyBullet == null)");

		if (m_Inst.m_WeapBossInfo == null)
			Debug.LogError("if (m_Inst.m_WeapBossInfo == null)");
	}
}

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

#pragma warning disable 0414 // 사용하지 않은 변수 Warning 알림 제거

public class Global : MonoBehaviour
{
	[SerializeField]
	private Camera      m_MainCamera = null;
	[SerializeField]
	private float       m_EffectVolume = 1.0f;
	[SerializeField]
	private float       m_BGMVolume = 1.0f;
	[SerializeField]
	private WeaponInfo[]    m_WeapInfo = null;
	[SerializeField]
	private WeaponInfo[]    m_WeapMonsterInfo = null;

	private static Global  m_Inst = null;
	private GameObject  m_PlayerObj = null;
	private Player      m_Player = null;
	private Vector2     m_P2MDist = Vector2.zero;
	private Vector2     m_E2PDist = Vector2.zero;

	public static float EffectVolume { get { return m_Inst.m_EffectVolume; } }
	public static Player Player { get { return m_Inst.m_Player; } }	
	public static WeaponInfo Pistol { get { return m_Inst.m_WeapInfo[(int)Weapon_Type_Player.Pistol]; } }

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
		info.m_OffsetY = m_Inst.m_WeapInfo[(int)type].m_OffsetY;
	}

	public static void SetWeaponInfo(in WeaponInfo info, Weapon_Type_Monster type)
	{
		info.m_Damage = m_Inst.m_WeapMonsterInfo[(int)type].m_Damage;
		info.m_FireRate = m_Inst.m_WeapMonsterInfo[(int)type].m_FireRate;
		info.m_FireSpeed = m_Inst.m_WeapMonsterInfo[(int)type].m_FireSpeed;
		info.m_FireRange = m_Inst.m_WeapMonsterInfo[(int)type].m_FireRange;
		info.m_FirstDist = m_Inst.m_WeapMonsterInfo[(int)type].m_FirstDist;
		info.m_OffsetY = m_Inst.m_WeapMonsterInfo[(int)type].m_OffsetY;
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

		if (m_PlayerObj is null)
			Debug.LogError("if (m_PlayerObj is null)");

		m_Player = m_PlayerObj.GetComponent<Player>();

		if (m_Player is null)
			Debug.LogError("if (m_Player is null)");

		if (m_WeapInfo.Length > (int)Weapon_Type_Player.End)
			Debug.LogError("if (m_WeapInfo.Length > Weapon_Type_Player.End)");

		if (m_WeapMonsterInfo.Length > (int)Weapon_Type_Monster.End)
			Debug.LogError("if (m_WeapMonsterInfo.Length > Weapon_Type_Monster.End)");
	}

	private void Update()
	{
		m_P2MDist = m_MainCamera.ScreenToWorldPoint(Input.mousePosition) - m_Player.transform.position;
		m_Player.TargetDir = m_P2MDist.normalized;

		m_Player.TargetAngle = Mathf.Atan2(m_P2MDist.y, m_P2MDist.x) * Mathf.Rad2Deg;
	}
}

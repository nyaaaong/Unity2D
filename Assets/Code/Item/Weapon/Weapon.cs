using UnityEngine;
using System;

[Serializable]
public class WeaponInfo
{
	public float m_Damage;
	public float m_FireRate;
	public float m_FireSpeed;
	public float m_FireRange;
	public float m_FirstDist;
	public float m_OffsetY;
}

public class Weapon : MonoBehaviour
{
	[SerializeField]
	private Hand            m_Hand = null;
	[SerializeField]
	private Weapon_Owner    m_Owner = Weapon_Owner.Player;
	[SerializeField]
	private Weapon_Type_Player     m_WeapType = Weapon_Type_Player.End;
	[SerializeField]
	private Weapon_Type_Monster     m_WeapTypeMonster = Weapon_Type_Monster.End;
	[SerializeField]
	private GameObject      m_PlayerBullet = null;
	[SerializeField]
	private GameObject      m_MonsterBullet = null;

	private Character       m_Base = null;
	private GameObject      m_NewBulletObj = null;
	private Bullet          m_NewBullet = null;
	private Bullet          m_Bullet = null;
	private SpriteRenderer  m_HandSR = null;
	private SpriteRenderer  m_SR = null;
	private AudioSource     m_Audio = null;
	private WeaponInfo      m_Info = null;
	private Weapon_Hand     m_HandSpriteDir = Weapon_Hand.Right;
	private float           m_TargetAngle = 0.0f;
	private Vector3         m_TargetDir = Vector3.zero;
	private Vector3         m_Rot = Vector3.zero;
	private Vector3         m_BulletPos = Vector3.zero;

	private void Fire()
	{
		if (m_Base.Fire)
		{
			m_BulletPos = transform.position + (m_TargetDir * m_Info.m_FirstDist);
			m_BulletPos.y += m_Info.m_OffsetY;

			m_Bullet.transform.position = m_BulletPos;
			m_Bullet.Dir = m_TargetDir;
			m_Bullet.SetInfo(m_Info);

			switch (m_Owner)
			{
				case Weapon_Owner.Player:
					m_NewBulletObj = Instantiate(m_PlayerBullet);
					break;
				case Weapon_Owner.Monster:
					m_NewBulletObj = Instantiate(m_MonsterBullet);
					break;
			}

			m_NewBulletObj.name = "Bullet";
			m_NewBullet = m_NewBulletObj.GetComponent<Bullet>();
			m_NewBullet.SetInfo(m_Bullet);

			m_Audio.Play();
		}
	}

	private bool WeapTypeCheck()
	{
		return m_Hand.WeapType == m_WeapType;
	}

	private void SpriteCheck()
	{
		m_SR.enabled = m_HandSR.enabled;

		if (m_SR.enabled && m_Owner == Weapon_Owner.Player)
			m_SR.enabled = WeapTypeCheck();
	}

	private void Calc()
	{
		/*
		오른쪽 무기:	m_TargetAngle == -90
		왼쪽 무기:	m_TargetAngle == -90 ~ 180 -> -90
					m_TargetAngle == -91 ~ -180 -> +270
		*/
		switch (m_Owner)
		{
			case Weapon_Owner.Player:
				m_TargetAngle = Global.P2MAngle;
				m_TargetDir = Global.P2MDir;
				break;
			case Weapon_Owner.Monster:
				break;
		}

		switch (m_HandSpriteDir)
		{
			case Weapon_Hand.Right:
				m_Rot.z = m_TargetAngle - 90.0f;
				break;
			case Weapon_Hand.Left:
				m_Rot.z = m_TargetAngle >= -90.0f ? m_TargetAngle - 90.0f : m_TargetAngle + 270.0f;
				break;
		}

		transform.localEulerAngles = m_Rot;

		m_SR.sortingOrder = m_HandSR.sortingOrder;
	}

	private void Awake()
	{
		if (m_Hand is null)
			Debug.LogError("if (m_Hand is null)");

		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR is null)
			Debug.LogError("if (m_SR is null)");

		m_HandSR = m_Hand.GetComponent<SpriteRenderer>();

		if (m_HandSR is null)
			Debug.LogError("if (m_HandSR is null)");

		if (m_PlayerBullet is null)
			Debug.LogError("if (m_PlayerBullet is null)");

		if (m_MonsterBullet is null)
			Debug.LogError("if (m_MonsterBullet is null)");

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio is null)
			Debug.LogError("if (m_Audio is null)");

		m_Info = new WeaponInfo();
	}

	private void Start()
	{
		m_Base = m_Hand.Base;

		if (m_Base is null)
			Debug.LogError("if (m_Base is null)");

		switch (m_Owner)
		{
			case Weapon_Owner.Player:
				m_HandSpriteDir = m_Hand.HandSpriteDir;
				m_Bullet = m_PlayerBullet.GetComponent<Bullet>();
				m_Bullet.Owner = Bullet_Owner.Player;
				break;
			case Weapon_Owner.Monster:
				m_Bullet = m_MonsterBullet.GetComponent<Bullet>();
				m_Bullet.Owner = Bullet_Owner.Monster;
				break;
		}

		if (m_Bullet is null)
			Debug.LogError("if (m_Bullet is null)");

		m_Audio.volume = Global.EffectVolume;

		if (m_WeapTypeMonster != Weapon_Type_Monster.End)
			Global.SetWeaponInfo(m_Info, m_WeapTypeMonster);

		else
			Global.SetWeaponInfo(m_Info, m_WeapType);

		m_Bullet.SetInfo(m_Info);

		Global.Player.FireTime = m_Info.m_FireRate;
	}

	private void Update()
	{
		SpriteCheck();
		//WeaponChangeCheck();

		if (m_SR.enabled)
		{
			Calc();

			if (m_Base.Fire)
			{
				if (Global.Player.FireTime >= m_Info.m_FireRate)
				{
					Global.Player.FireTime = 0.0f;

					Fire();
				}
			}
		}
	}
}

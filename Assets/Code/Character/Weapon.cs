using UnityEngine;
using System;

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

	private void SpreadBulletCheck()
	{
		if (m_Base.SpreadBullet)
		{
			m_Base.SpreadBullet = false;

			m_Bullet.transform.position = m_Base.transform.position;
			m_Bullet.Dir = Vector3.zero;
			m_Bullet.SetInfo(m_Info, m_Base);

			float angle = 0.0f;

			for (int i = 0; i < 12; ++i)
			{
				m_NewBulletObj = Instantiate(m_MonsterBullet);

				m_NewBulletObj.name = "Bullet";
				m_NewBullet = m_NewBulletObj.GetComponent<Bullet>();
				m_NewBullet.SetInfo(m_Bullet);
				m_NewBullet.Dir = Global.ConvertDir(angle);
				angle += 30.0f;
			}

			m_Audio.Play();
			return;
		}
	}

	private void Fire()
	{
		if (m_Base.Fire)
		{
			if (m_Base.FireTime >= m_Info.m_FireRate)
			{
				m_Base.FireTime = 0.0f;

				m_BulletPos = transform.position + (m_TargetDir * m_Info.m_FirstDist);
				m_BulletPos.y += m_Info.m_OffsetY;

				m_Bullet.transform.position = m_BulletPos;
				m_Bullet.Dir = m_TargetDir;
				m_Bullet.SetInfo(m_Info, m_Base);

				switch (m_Owner)
				{
					case Weapon_Owner.Player:
						m_NewBulletObj = Instantiate(m_PlayerBullet);
						break;
					case Weapon_Owner.Monster:
						if (m_WeapTypeMonster == Weapon_Type_Monster.Shotgun)
						{
							float angle = -20.0f;
							float newAngle = 0.0f;
							for (int i = 0; i < 5; ++i)
							{
								m_NewBulletObj = Instantiate(m_MonsterBullet);

								m_NewBulletObj.name = "Bullet";
								m_NewBullet = m_NewBulletObj.GetComponent<Bullet>();
								m_NewBullet.SetInfo(m_Bullet);
								newAngle = m_TargetAngle + angle;
								m_NewBullet.Dir = Global.ConvertDir(newAngle);
								angle += 10.0f;
							}

							m_Audio.Play();
							return;
						}

						else
							m_NewBulletObj = Instantiate(m_MonsterBullet);
						break;
				}

				m_NewBulletObj.name = "Bullet";
				m_NewBullet = m_NewBulletObj.GetComponent<Bullet>();
				m_NewBullet.SetInfo(m_Bullet);

				m_Audio.Play();
			}
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
		m_TargetAngle = m_Base.TargetAngle;
		m_TargetDir = m_Base.TargetDir;

		if (m_Owner == Weapon_Owner.Player)
		{
			switch (m_HandSpriteDir)
			{
				case Weapon_Hand.Right:
					m_Rot.z = m_TargetAngle - 90.0f;
					break;
				case Weapon_Hand.Left:
					m_Rot.z = m_TargetAngle >= -90.0f ? m_TargetAngle - 90.0f : m_TargetAngle + 270.0f;
					break;
			}
		}

		else
		{
			m_Rot.z = m_TargetAngle;

			switch (m_WeapTypeMonster)
			{
				case Weapon_Type_Monster.Pistol:
					if (m_HandSpriteDir == Weapon_Hand.Left)
						m_Rot.z -= 180.0f;
					break;
				case Weapon_Type_Monster.Rifle:
				case Weapon_Type_Monster.Shotgun:
					m_Rot.z -= 90.0f;
					break;
			}
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

		m_HandSpriteDir = m_Hand.HandSpriteDir;

		switch (m_Owner)
		{
			case Weapon_Owner.Player:
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

		m_Bullet.SetInfo(m_Info, m_Base);

		m_Base.FireTime = m_Info.m_FireRate;
	}

	private void Update()
	{
		SpriteCheck();

		if (m_SR.enabled)
		{
			Calc();
			Fire();
		}

		SpreadBulletCheck();
	}
}

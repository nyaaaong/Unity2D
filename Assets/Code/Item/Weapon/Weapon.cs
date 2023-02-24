using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField]
	private Hand			m_Hand = null;
	[SerializeField]
	private Weapon_Owner	m_Owner = Weapon_Owner.Player;
	[SerializeField]
	private Weapon_Type		m_WeapType = Weapon_Type.End;

	private Weapon_Hand		m_HandSpriteDir = Weapon_Hand.Right;
	private SpriteRenderer	m_HandSR = null;
	private SpriteRenderer	m_SR = null;
	private float			m_TargetAngle = 0.0f;
	private Vector3			m_Rot = Vector3.zero;

	private void Awake()
	{
		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR == null)
			Debug.LogError("if (m_SR == null)");

		m_HandSR = m_Hand.GetComponent<SpriteRenderer>();

		if (m_HandSR == null)
			Debug.LogError("if (m_HandSR == null)");

		switch (m_Owner)
		{
			case Weapon_Owner.Player:
				m_HandSpriteDir = m_Hand.HandSpriteDir;
				break;
			case Weapon_Owner.Monster:
				break;
		}

	}

	private void Update()
	{
		m_SR.enabled = m_HandSR.enabled;

		if (m_SR.enabled)
		{
			/*
			오른쪽 무기:	m_TargetAngle == -90
			왼쪽 무기:	m_TargetAngle == -90 ~ 180 -> -90
						m_TargetAngle == -91 ~ -180 -> +270
			*/
			switch (m_Owner)
			{
				case Weapon_Owner.Player:
					m_SR.enabled = m_Hand.WeapType == m_WeapType;
					m_TargetAngle = Global.P2MAngle;
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
	}
}

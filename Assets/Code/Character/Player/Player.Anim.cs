﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player : Character
{
	private void CheckAnimDir()
	{
		if (m_WeapType == Weapon_Type.End)
		{
			if (m_Dir[UP])
			{
				if (m_Dir[LEFT])
					m_AnimName += "LeftUp";

				else if (m_Dir[RIGHT])
					m_AnimName += "RightUp";

				else
					m_AnimName += "Up";
			}

			else if (m_Dir[DOWN])
			{
				if (m_Dir[LEFT])
					m_AnimName += "LeftDown";

				else if (m_Dir[RIGHT])
					m_AnimName += "RightDown";

				else
					m_AnimName += "Down";
			}

			else if (m_Dir[LEFT])
				m_AnimName += "Left";

			else if (m_Dir[RIGHT])
				m_AnimName += "Right";
		}

		else
		{
			if (m_P2MAngle == 180.0f || (m_P2MAngle < 180.0f && m_P2MAngle > 105.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Left;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "LeftUp";
			}

			else if (m_P2MAngle == 105.0f || (m_P2MAngle < 105.0f && m_P2MAngle > 75.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Left;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "Up";
			}

			else if (m_P2MAngle == 75.0f || (m_P2MAngle < 75.0f && m_P2MAngle > 0.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Right;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "RightUp";
			}

			else if (m_P2MAngle == 0.0f || (m_P2MAngle < 0.0f && m_P2MAngle > -75.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Right;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "RightDown";
			}

			else if (m_P2MAngle == -75.0f || (m_P2MAngle < -75.0f && m_P2MAngle > -105.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Right;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "Down";
			}

			else if (m_P2MAngle == -105.0f || (m_P2MAngle < -105.0f && m_P2MAngle > -180.0f))
			{
				if (m_WeapType != Weapon_Type.End)
					m_HandDir = Weapon_Hand.Left;

				else
					m_HandDir = Weapon_Hand.None;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "LeftDown";
			}
		}
	}

	private void AnimCheck()
	{
		switch (m_Status)
		{
			case Character_Status.Idle:
				m_AnimName = "Idle_";
				CheckAnimDir();
				break;
			case Character_Status.Walk:
				m_AnimName = "Walk_";
				CheckAnimDir();
				break;
			case Character_Status.End:
				Debug.LogError("case Character_Status.End:");
				break;
		}

		if (m_AnimName == "Idle_" || m_AnimName == "Walk_")
			Debug.LogError("m_AnimName == Idle_ || m_AnimName == Walk_");

		if (m_PrevAnimName != m_AnimName) // 이걸 하지 않으면 애니메이션이 굉장히 빨라진다.
		{
			m_Animator.SetTrigger(m_AnimName);

			m_PrevAnimName = m_AnimName;
		}
	}
}
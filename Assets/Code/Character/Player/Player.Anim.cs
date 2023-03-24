using UnityEngine;

public partial class Player : Character
{
	private void AnimDirCheck()
	{
		if (m_WeapType == Weapon_Type_Player.End)
			Debug.LogError("if (m_WeapType == Weapon_Type_Player.End)");

		if (m_Status == Player_Status.Dodge)
		{
			if (UP)
			{
				if (LEFT)
					m_AnimName += "LeftUp";

				else if (RIGHT)
					m_AnimName += "RightUp";

				else
					m_AnimName += "Up";
			}

			else if (DOWN)
			{
				if (LEFT)
					m_AnimName += "LeftDown";

				else if (RIGHT)
					m_AnimName += "RightDown";

				else
					m_AnimName += "Down";
			}

			else if (LEFT)
				m_AnimName += "Left";

			else if (RIGHT)
				m_AnimName += "Right";
		}

		else
		{
			if (m_TargetAngle == 180.0f || (m_TargetAngle < 180.0f && m_TargetAngle > 105.0f))
			{
				m_HandDir = Weapon_Hand.Left;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "LeftUp";
			}

			else if (m_TargetAngle == 105.0f || (m_TargetAngle < 105.0f && m_TargetAngle > 75.0f))
			{
				m_HandDir = Weapon_Hand.Left;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "Up";
			}

			else if (m_TargetAngle == 75.0f || (m_TargetAngle < 75.0f && m_TargetAngle > 0.0f))
			{
				m_HandDir = Weapon_Hand.Right;

				m_WeapRenderOrder = Weapon_RenderOrder.Back;

				m_AnimName += "RightUp";
			}

			else if (m_TargetAngle == 0.0f || (m_TargetAngle < 0.0f && m_TargetAngle > -75.0f))
			{
				m_HandDir = Weapon_Hand.Right;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "RightDown";
			}

			else if (m_TargetAngle == -75.0f || (m_TargetAngle < -75.0f && m_TargetAngle > -105.0f))
			{
				m_HandDir = Weapon_Hand.Right;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "Down";
			}

			else if (m_TargetAngle == -105.0f || (m_TargetAngle < -105.0f && m_TargetAngle > -180.0f))
			{
				m_HandDir = Weapon_Hand.Left;

				m_WeapRenderOrder = Weapon_RenderOrder.Front;

				m_AnimName += "LeftDown";
			}
		}
	}

	private void DodgeAnimEnd()
	{
		m_Status = Player_Status.Idle;

		m_NoHit = false;
		m_Move = false;
		m_KeyLock = false;
		m_HideWeapon = false;
	}

	private void AnimCheck()
	{
		switch (m_Status)
		{
			case Player_Status.Idle:
				m_AnimName = "Idle_";
				break;
			case Player_Status.Walk:
				m_AnimName = "Walk_";
				break;
			case Player_Status.Dodge:
				m_AnimName = "Dodge_";
				break;
			case Player_Status.End:
				Debug.LogError("case Player_Status.End:");
				break;
		}

		AnimDirCheck();

		if (m_AnimName == "Idle_" || m_AnimName == "Walk_" || m_AnimName == "Dodge_")
			Debug.LogError("m_AnimName == Idle_ || m_AnimName == Walk_ || m_AnimName == Dodge_");

		if (m_PrevAnimName != m_AnimName) // 이걸 하지 않으면 애니메이션이 굉장히 빨라진다.
		{
			m_Animator.SetTrigger(m_AnimName);

			m_PrevAnimName = m_AnimName;
		}
	}

	private void HitAnimCheck()
	{
		if (m_HitAnim)
		{
			m_HitAnimTime += m_deltaTime;
			m_BlinkTime += m_deltaTime;

			if (m_BlinkTime >= m_BlinkTimeMax)
			{
				m_BlinkTime = 0.0f;
				m_SR.enabled = !m_SR.enabled;
			}

			if (m_HitAnimTime >= m_HitAnimTimeMax)
			{
				m_HitAnimTime = 0.0f;
				m_BlinkTime = 0.0f;
				m_SR.enabled = true;

				m_HitAnim = false;
				m_NoHit = false;
			}
		}
	}
}
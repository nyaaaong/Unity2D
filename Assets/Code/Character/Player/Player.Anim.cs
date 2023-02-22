using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player : Character
{
	private void CheckAnimDir()
	{
		if (m_Slot == Player_WeaponSlot.End)
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
				m_AnimName += "LeftUp";

			else if (m_P2MAngle == 105.0f || (m_P2MAngle < 105.0f && m_P2MAngle > 75.0f))
				m_AnimName += "Up";

			else if (m_P2MAngle == 75.0f || (m_P2MAngle < 75.0f && m_P2MAngle > 0.0f))
				m_AnimName += "RightUp";

			else if (m_P2MAngle == 0.0f || (m_P2MAngle < 0.0f && m_P2MAngle > -75.0f))
				m_AnimName += "RightDown";

			else if (m_P2MAngle == -75.0f || (m_P2MAngle < -75.0f && m_P2MAngle > -105.0f))
				m_AnimName += "Down";

			else if (m_P2MAngle == -105.0f || (m_P2MAngle < -105.0f && m_P2MAngle > -180.0f))
				m_AnimName += "LeftDown";
		}
	}

	private void AnimCheck()
	{
		switch (m_Status)
		{
			case Player_Status.Idle:
				m_AnimName = "Idle_";
				CheckAnimDir();
				break;
			case Player_Status.Walk:
				m_AnimName = "Walk_";
				CheckAnimDir();
				break;
			case Player_Status.End:
				Debug.LogError("case Player_Status.End:");
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
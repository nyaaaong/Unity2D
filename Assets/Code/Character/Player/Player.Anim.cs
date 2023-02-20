using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player : Character
{
	private string CheckAnimDir()
	{
		string AnimName = "";

		switch (m_Dir)
		{
			case Player_Dir.Up:
				AnimName = "Up";
				break;
			case Player_Dir.UpLeft:
				AnimName = "LeftUp";
				break;
			case Player_Dir.UpRight:
				AnimName = "RightUp";
				break;
			case Player_Dir.Left:
				AnimName = "Left";
				break;
			case Player_Dir.Right:
				AnimName = "Right";
				break;
			case Player_Dir.Down:
				AnimName = "Down";
				break;
			case Player_Dir.DownLeft:
				AnimName = "LeftDown";
				break;
			case Player_Dir.DownRight:
				AnimName = "RightDown";
				break;
		}

		return AnimName;
	}

	private void AnimCheck()
	{
		if (m_Status == Player_Status.Walk && m_Move == false)
			m_Status = Player_Status.Idle;

		else if (m_Status == Player_Status.Idle && m_Move == true)
			m_Status = Player_Status.Walk;

		switch (m_Status)
		{
			case Player_Status.Idle:
				m_AnimName = "Idle_" + CheckAnimDir();				
				break;
			case Player_Status.Walk:
				m_AnimName = "Walk_" + CheckAnimDir();
				break;
			case Player_Status.End:
				m_AnimName = m_DefaultAnimName;
				break;
		}

		if (m_PrevAnimName != m_AnimName)
		{
			m_Animator.SetTrigger(m_AnimName);

			m_PrevAnimName = m_AnimName;
		}
	}
}
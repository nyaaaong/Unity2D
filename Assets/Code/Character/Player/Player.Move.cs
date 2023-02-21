using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player : Character
{
	private void KeyDownCheck(int dir)
	{
		if (m_KeyDown[dir])
		{
			KeyCode key = 0;

			switch ((Player_Dir)dir)
			{
				case Player_Dir.Up:
					key = KeyCode.W;
					break;
				case Player_Dir.Left:
					key = KeyCode.A;
					break;
				case Player_Dir.Right:
					key = KeyCode.D;
					break;
				case Player_Dir.Down:
					key = KeyCode.S;
					break;
			}

			if (Input.GetKeyUp(key))
				m_KeyDown[dir] = false;
		}
	}

	private void MoveDirCheck()
	{
		KeyDownCheck(UP);
		KeyDownCheck(DOWN);
		KeyDownCheck(LEFT);
		KeyDownCheck(RIGHT);

		if (m_Move)
		{
			if (!m_KeyDown[UP] &&
				!m_KeyDown[DOWN] &&
				!m_KeyDown[LEFT] &&
				!m_KeyDown[RIGHT])
			{
				m_Move = false;
				m_Status = Player_Status.Idle;
			}
		}
	}

	private void ResetDirCheck()
	{
		if (m_Move)
		{
			int maxIdx = END;

			for (int i = 0; i < maxIdx; ++i)
			{
				if (!m_KeyDown[i])
				{
					m_Dir[i] = false;
				}
			}
		}
	}

	private void Move(int dir)
	{
		KeyCode key = 0;
		Vector3 Movedir = Vector3.zero;

		switch ((Player_Dir)dir)
		{
			case Player_Dir.Up:
				key = KeyCode.W;
				Movedir = Vector3.up;
				break;
			case Player_Dir.Left:
				key = KeyCode.A;
				Movedir = Vector3.left;
				break;
			case Player_Dir.Right:
				key = KeyCode.D;
				Movedir = Vector3.right;
				break;
			case Player_Dir.Down:
				key = KeyCode.S;
				Movedir = Vector3.down;
				break;
		}

		if (Input.GetKeyDown(key))
		{
			m_KeyDown[dir] = true;

			m_Dir[dir] = true;
			m_Status = Player_Status.Walk;
			m_Move = true;
		}

		if (m_KeyDown[dir])
			transform.position += Movedir * m_MoveSpeed * m_deltaTime;
	}

	private void KeyCheck()
	{
		Move(UP);
		Move(LEFT);
		Move(RIGHT);
		Move(DOWN);

		MoveDirCheck();
		ResetDirCheck();
	}
}

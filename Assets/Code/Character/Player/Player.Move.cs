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

		if (m_Move && m_Status != Character_Status.Dodge)
		{
			if (!m_KeyDown[UP] &&
				!m_KeyDown[DOWN] &&
				!m_KeyDown[LEFT] &&
				!m_KeyDown[RIGHT])
			{
				m_Move = false;
				m_Status = Character_Status.Idle;
			}
		}
	}

	private void ResetDirCheck()
	{
		if (m_Move && m_Status != Character_Status.Dodge)
		{
			int maxIdx = END;

			for (int i = 0; i < maxIdx; ++i)
			{
				m_LastDir[i] = m_Dir[i];

				if (!m_KeyDown[i])
					m_Dir[i] = false;
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

		if (Input.GetKey(key))
		{
			m_Dir[dir] = true;
			m_KeyDown[dir] = true;

			if (!m_KeyLock)
			{
				m_Status = Character_Status.Walk;
				m_Move = true;

				transform.position += Movedir * m_Info.m_MoveSpeed * m_deltaTime;
			}
		}
	}

	private void Dodge()
	{
		if (Input.GetMouseButtonDown((int)Mouse_Click.Right))
		{
			m_Status = Character_Status.Dodge;

			m_NoHit = true;
			m_Move = true;
			m_KeyLock = true;
			m_HideWeapon = true;

			PlaySound(m_DodgeClip);
		}
	}

	private void DodgeCheck()
	{
		if (m_Status == Character_Status.Dodge)
		{
			Vector3 Movedir = Vector3.zero;
			bool		Cross = false;

			if (m_LastDir[UP])
			{
				Movedir = Vector3.up;

				if (m_LastDir[LEFT])
				{
					Cross = true;
					Movedir += Vector3.left;
				}

				else if (m_LastDir[RIGHT])
				{
					Cross = true;
					Movedir += Vector3.right;
				}
			}

			else if (m_LastDir[DOWN])
			{
				Movedir = Vector3.down;

				if (m_LastDir[LEFT])
				{
					Cross = true;
					Movedir += Vector3.left;
				}

				else if (m_LastDir[RIGHT])
				{
					Cross = true;
					Movedir += Vector3.right;
				}
			}

			else if (m_LastDir[LEFT])
				Movedir = Vector3.left;

			else if (m_LastDir[RIGHT])
				Movedir = Vector3.right;

			if (Cross)
				transform.position += Movedir * m_DodgeSpeed * 0.75f * m_deltaTime;

			else
				transform.position += Movedir * m_DodgeSpeed * m_deltaTime;
		}
	}

	private void MoveKeyCheck()
	{
		Dodge();

		Move(UP);
		Move(LEFT);
		Move(RIGHT);
		Move(DOWN);

		DodgeCheck();
		MoveDirCheck();
		ResetDirCheck();
	}
}

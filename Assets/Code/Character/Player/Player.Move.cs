using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player	: Character
{
	private bool IsPressUp()
	{
		if (Input.GetKey(KeyCode.W))
			return true;

		return false;
	}

	private bool IsPressDown()
	{
		if (Input.GetKey(KeyCode.S))
			return true;

		return false;
	}

	private bool IsPressLeft()
	{
		if (Input.GetKey(KeyCode.A))
			return true;

		return false;
	}

	private bool IsPressRight()
	{
		if (Input.GetKey(KeyCode.D))
			return true;

		return false;
	}

	private void Up()
	{
		if (IsPressUp())
		{
			if (IsPressDown())
			{
				m_Dir = Player_Dir.End;
				m_Move = false;
				return;
			}

			if (IsPressLeft())
				m_Dir = Player_Dir.UpLeft;

			else if (IsPressRight())
				m_Dir = Player_Dir.UpRight;

			else
				m_Dir = Player_Dir.Up;

			m_Move = true;
		}
	}

	private void Down()
	{
		if (IsPressDown())
		{
			if (IsPressUp())
			{
				m_Dir = Player_Dir.End;
				m_Move = false;
				return;
			}

			if (IsPressLeft())
				m_Dir = Player_Dir.DownLeft;

			else if (IsPressRight())
				m_Dir = Player_Dir.DownRight;

			else
				m_Dir = Player_Dir.Down;

			m_Move = true;
		}
	}

	private void Left()
	{
		if (IsPressLeft())
		{
			if (IsPressRight())
			{
				m_Dir = Player_Dir.End;
				m_Move = false;
				return;
			}

			if (!IsPressUp() && !IsPressDown())
				m_Dir = Player_Dir.Left;

			m_Move = true;
		}
	}

	private void Right()
	{
		if (IsPressRight())
		{
			if (IsPressLeft())
			{
				m_Dir = Player_Dir.End;
				m_Move = false;
				return;
			}

			if (!IsPressUp() && !IsPressDown())
				m_Dir = Player_Dir.Right;

			m_Move = true;
		}
	}

	private void DirCheck()
	{
		Up();
		Down();
		Left();
		Right();
	}

	private void MoveCheck()
	{
		if (!IsPressUp() && !IsPressDown() && !IsPressLeft() && !IsPressRight())
			m_Move = false;
	}

	private void Move()
	{
		if (!m_Move)
			return;

		switch (m_Dir)
		{
			case Player_Dir.Up:
				transform.position += Vector3.up * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.UpLeft:
				transform.position += Vector3.up * m_MoveSpeed * Time.deltaTime;
				transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.UpRight:
				transform.position += Vector3.up * m_MoveSpeed * Time.deltaTime;
				transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.Left:
				transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.Right:
				transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.Down:
				transform.position += Vector3.down * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.DownLeft:
				transform.position += Vector3.down * m_MoveSpeed * Time.deltaTime;
				transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
				break;
			case Player_Dir.DownRight:
				transform.position += Vector3.down * m_MoveSpeed * Time.deltaTime;
				transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
				break;
		}

	}

	private void KeyCheck()
	{
		DirCheck();
		MoveCheck();

		Move();
	}
}

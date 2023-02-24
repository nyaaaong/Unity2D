using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Character_Status
{
	Idle,
	Walk,
	End
}

public enum Player_Dir
{
	Up,
	Left,
	Right,
	Down,
	End
}

public enum Weapon_Type
{
	Pistol,
	Rifle,
	Sniper,
	End
}

public enum Weapon_Hand
{
	None,
	Right,
	Left
}

public enum Weapon_Owner
{
	Player,
	Monster
}

public enum Weapon_RenderOrder
{
	Front = 1,
	Back = -1
}
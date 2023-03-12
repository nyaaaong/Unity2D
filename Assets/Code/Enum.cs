public enum Character_Status
{
	Idle,
	Walk,
	Dodge,
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

public enum Weapon_Type_Player
{
	Pistol,
	Rifle,
	Sniper,
	End
}

public enum Weapon_Type_Monster
{
	Pistol,
	Rifle,
	Shotgun,
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

public enum Mouse_Click
{
	Left,
	Right
}

public enum Bullet_Owner
{
	Player,
	Monster
}

public enum Item_Type
{
	Rifle,
	Sniper,
	Heart,
	End
}
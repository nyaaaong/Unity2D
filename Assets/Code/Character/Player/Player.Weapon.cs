using UnityEngine;

public partial class Player : Character
{
	private void EquipWeapon(Weapon_Type_Player slot)
	{
		if (m_HasWeapon[(int)slot])
		{
			m_WeapType = slot;

			m_WeaponChange = true;
		}

		else
			m_WeapType = Weapon_Type_Player.End;
	}

	private void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			EquipWeapon(Weapon_Type_Player.Pistol);

		else if (Input.GetKeyDown(KeyCode.Alpha2))
			EquipWeapon(Weapon_Type_Player.Rifle);

		else if (Input.GetKeyDown(KeyCode.Alpha3))
			EquipWeapon(Weapon_Type_Player.Sniper);
	}

	private void FireCheck()
	{
		if (m_WeapType == Weapon_Type_Player.End)
			return;

		if (Input.GetMouseButton((int)Mouse_Click.Left))
			m_Fire = true;

		else
			m_Fire = false;
}

	private void WeaponKeyCheck()
	{
		ChangeWeapon();
		FireCheck();
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player	:	Character
{
	private void EquipWeapon(Weapon_Type slot)
	{
		if (m_HasWeapon[(int)slot])
			m_WeapType = slot;

		else
			m_WeapType = Weapon_Type.End;
	}

	private void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			EquipWeapon(Weapon_Type.Pistol);

		else if (Input.GetKeyDown(KeyCode.Alpha2))
			EquipWeapon(Weapon_Type.Rifle);

		else if (Input.GetKeyDown(KeyCode.Alpha3))
			EquipWeapon(Weapon_Type.Sniper);
	}

	private void WeaponKeyCheck()
	{
		ChangeWeapon();
	}
}
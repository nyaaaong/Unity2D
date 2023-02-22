using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player
{
	private void EquipWeapon(Player_WeaponSlot slot)
	{
		if (m_HasWeapon[(int)slot])
			m_Slot = slot;

		else
			m_Slot = Player_WeaponSlot.End;
	}

	private void ChangeWeapon()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			EquipWeapon(Player_WeaponSlot.Pistol);

		else if (Input.GetKeyDown(KeyCode.Alpha2))
			EquipWeapon(Player_WeaponSlot.Rifle);

		else if (Input.GetKeyDown(KeyCode.Alpha3))
			EquipWeapon(Player_WeaponSlot.Sniper);
	}

	private void WeaponKeyCheck()
	{
		ChangeWeapon();
	}
}
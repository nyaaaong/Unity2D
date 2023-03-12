using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rifle	:	Item
{
	protected override void LootingEvent()
	{
		Global.Player.AddWeapon(Weapon_Type_Player.Rifle);

		base.LootingEvent();
	}

	protected override void Awake()
	{
		base.Awake();

		m_Type = Item_Type.Rifle;
	}
}
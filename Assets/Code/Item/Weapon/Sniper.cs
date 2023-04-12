﻿public class Sniper : Item
{
	protected override void LootingEvent()
	{
		CharacterManager.PlayerAddWeapon(Weapon_Type_Player.Sniper);

		base.LootingEvent();
	}

	protected override void Awake()
	{
		base.Awake();

		m_Type = Item_Type.Sniper;
	}
}
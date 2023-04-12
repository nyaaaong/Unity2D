public class Rifle : Item
{
	protected override void LootingEvent()
	{
		CharacterManager.PlayerAddWeapon(Weapon_Type_Player.Rifle);

		base.LootingEvent();
	}

	protected override void Awake()
	{
		base.Awake();

		m_Type = Item_Type.Rifle;
	}
}
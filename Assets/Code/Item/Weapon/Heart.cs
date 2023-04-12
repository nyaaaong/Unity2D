public class Heart : Item
{
	protected override void LootingEvent()
	{
		CharacterManager.PlayerAddHeart();

		base.LootingEvent();
	}

	protected override void Awake()
	{
		base.Awake();

		m_Type = Item_Type.Heart;
	}
}
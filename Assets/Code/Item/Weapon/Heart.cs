public class Heart : Item
{
	protected override void LootingEvent()
	{
		Global.Player.AddHeart();

		base.LootingEvent();
	}

	protected override void Awake()
	{
		base.Awake();

		m_Type = Item_Type.Heart;
	}
}
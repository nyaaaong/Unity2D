public class Bandana : Monster
{
	protected override void Awake()
	{
		base.Awake();

		m_PatternList.Add(MovePattern);
	}
}

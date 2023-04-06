public class Bandana : Monster
{

	protected override void Awake()
	{
		base.Awake();

		m_PatternList.Add(MovePattern);
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}
}

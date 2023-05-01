using UnityEngine;

public class Hand : Global
{
	[SerializeField]
	private GameObject m_BaseObj = null;
	[SerializeField]
	private Weapon_Hand m_HandSpriteDir = Weapon_Hand.Right;
	[SerializeField]
	private Weapon_Owner m_Owner = Weapon_Owner.Player;

	private Character m_Base = null;
	private SpriteRenderer m_SR = null;

	public Character Base { get { return m_Base; } }
	public Weapon_Hand HandSpriteDir { get { return m_HandSpriteDir; } }
	public Weapon_Type_Player WeapType { get { return m_Base.Type; } }

	private void Awake()
	{
		m_SR = GetComponent<SpriteRenderer>();

		if (m_SR == null)
			Debug.LogError("m_SR = GetComponent<SpriteRenderer>();");

		if (m_BaseObj == null)
			Debug.LogError("if (m_BaseObj == null)");

		m_Base = m_BaseObj.GetComponent<Character>();

		if (m_Base == null)
			Debug.LogError("m_Base = m_BaseObj.GetComponent<Character>();");
	}

	protected override void AfterUpdate()
	{
		base.AfterUpdate();

		if (m_Owner == Weapon_Owner.Monster && !m_Base.IsUpdate)
			return;

		if (!m_Base.Visible || m_Base.DeathAnimProc || m_Base.HandDir != m_HandSpriteDir || m_Base.HideWeapon || (m_Owner == Weapon_Owner.Player && WeapType == Weapon_Type_Player.End))
			m_SR.enabled = false;

		else
			m_SR.enabled = true;

		m_SR.sortingOrder = (int)m_Base.WeapRenderOrder;
	}
}

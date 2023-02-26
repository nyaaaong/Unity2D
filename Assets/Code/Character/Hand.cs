using UnityEngine;

public class Hand : MonoBehaviour
{
	[SerializeField]
	private GameObject  m_BaseObj = null;
	[SerializeField]
	private Weapon_Hand m_HandSpriteDir = Weapon_Hand.Right;

	private Character   m_Base = null;
	private SpriteRenderer  m_SR = null;

	public Weapon_Hand HandSpriteDir { get { return m_HandSpriteDir; } }
	public Weapon_RenderOrder WeapRenderOrder { get { return m_Base.WeapRenderOrder; } }
	public Weapon_Type WeapType { get { return m_Base.Type; } }

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

	private void Update()
	{
		if (m_Base.HandDir != m_HandSpriteDir || m_Base.HandDir == Weapon_Hand.None || m_Base.HideWeapon)
			m_SR.enabled = false;

		else
			m_SR.enabled = true;

		m_SR.sortingOrder = (int)WeapRenderOrder;
	}
}

using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField]
	protected float               m_MoveSpeed = 1.0f;

	protected float					m_deltaTime = 0.0f;
	protected Character_Status		m_Status = Character_Status.Idle;
	protected Weapon_Hand			m_HandDir = Weapon_Hand.None;   // 어느 쪽 손을 보일 것 인지
	protected Weapon_RenderOrder	m_WeapRenderOrder = Weapon_RenderOrder.Front;   // 캐릭터 기준 총이 보여질지 가려질지
	protected Weapon_Type			m_WeapType = Weapon_Type.End;
	protected string				m_AnimName = "";
	protected string				m_PrevAnimName = "";
	protected bool					m_Move = false;
	protected bool                  m_NoHit = false;
	protected bool					m_HideWeapon = false;
	protected Animator				m_Animator = null;

	public Weapon_Hand HandDir { get { return m_HandDir; } }
	public Weapon_RenderOrder WeapRenderOrder { get { return m_WeapRenderOrder; } }
	public Weapon_Type Type { get { return m_WeapType; } }
	public bool NoHit{ get{ return m_NoHit; } }
	public bool HideWeapon { get { return m_HideWeapon; } }

	private void Awake()
	{

	}

	private void Update()
	{
		m_deltaTime = Time.deltaTime;

	}
}

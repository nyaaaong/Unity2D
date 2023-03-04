using UnityEngine;

#pragma warning disable 0414 // 사용하지 않은 변수 Warning 알림 제거

public class Bullet : MonoBehaviour
{
	private Vector3         m_Dir = Vector2.zero;
	private float           m_Damage = 1.0f;
	private float           m_Speed = 1.0f;
	private float           m_Range = 1.0f;
	private float           m_AccRange = 0.0f;
	private float           m_MoveDist = 0.0f;
	private float           m_FirstDist = 5.0f; // 발사할 때 어디서부터 시작될 지
	private Bullet_Owner    m_Owner = Bullet_Owner.Player;
	private Animator        m_Anim = null;
	private CircleCollider2D    m_Collider = null;
	private bool            m_Destroy = false;

	public Bullet_Owner Owner { get { return m_Owner; } set { m_Owner = value; } }
	public Vector3 Dir { get { return m_Dir; } set { m_Dir = value; } }
	public float Damage { get { return m_Damage; } }

	public void SetInfo(in WeaponInfo info)
	{
		m_Damage = info.m_Damage;
		m_Speed = info.m_FireSpeed;
		m_Range = info.m_FireRange;
		m_FirstDist = info.m_FirstDist;
	}

	public void SetInfo(Bullet bullet)
	{
		m_Dir = bullet.m_Dir;
		m_Damage = bullet.m_Damage;
		m_Speed = bullet.m_Speed;
		m_Range = bullet.m_Range;
		m_MoveDist = bullet.m_MoveDist;
		m_FirstDist = bullet.m_FirstDist;
		m_Owner = bullet.m_Owner;
	}

	private void Destroy()
	{
		GameObject.Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Destroy)
			return;

		else if (collision.tag == "Bullet")
			return;

		m_Destroy = true;

		if (m_Owner == Bullet_Owner.Player)
		{
			if (collision.tag == "Monster")
			{
				m_Anim.SetTrigger("Hit");
				return;
			}
		}

		else
		{
			if (collision.tag == "Player")
			{
				m_Anim.SetTrigger("Hit");
				return;
			}
		}

		m_Anim.SetTrigger("NoHit");
	}

	private void Awake()
	{
		m_Anim = GetComponent<Animator>();

		if (m_Anim is null)
			Debug.LogError("if (m_Anim is null)");

		m_Collider = GetComponent<CircleCollider2D>();

		if (m_Collider is null)
			Debug.LogError("if (m_Collider is null)");
	}

	private void Update()
	{
		if (m_AccRange >= m_Range && !m_Destroy)
		{
			m_Destroy = true;

			m_Anim.SetTrigger("NoHit");
		}

		else if (!m_Destroy)
		{
			m_MoveDist = m_Speed * Time.deltaTime;
			m_AccRange += m_MoveDist;

			transform.position += m_Dir * m_MoveDist;
		}
	}
}

using UnityEngine;

#pragma warning disable 0414 // 사용하지 않은 변수 Warning 알림 제거

public class Bullet : MonoBehaviour
{
	private Vector3					m_Dir = Vector2.zero;
	private float						m_Damage = 1.0f;
	private float						m_Speed = 1.0f;
	private float						m_Range = 1.0f;
	private float						m_AccRange = 0.0f;
	private float						m_MoveDist = 0.0f;
	private float						m_FirstDist = 5.0f; // 발사할 때 어디서부터 시작될 지
	private Bullet_Owner		m_Owner = Bullet_Owner.Player;
	private Animator				m_Anim = null;
	private CircleCollider2D		m_Collider = null;
	private Character				m_Target = null;
	private bool						m_Destroy = false;
	private bool						m_Pierce = false;

	public Bullet_Owner Owner { get { return m_Owner; } set { m_Owner = value; } }
	public Vector3 Dir { get { return m_Dir; } set { m_Dir = value; } }
	public float Damage { get { return m_Damage; } }

	public void SetInfo(in WeaponInfo info)
	{
		m_Damage = info.m_Damage;
		m_Speed = info.m_FireSpeed;
		m_Range = info.m_FireRange;
		m_FirstDist = info.m_FirstDist;
		m_Pierce = info.m_Pierce;
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
		m_Pierce = bullet.m_Pierce;
	}

	private void Destroy()
	{
		GameObject.Destroy(gameObject);
	}

	private void HitAnim()
	{
		m_Destroy = true;
		m_Anim.SetTrigger("Hit");
	}

	private void NoHitAnim()
	{
		m_Destroy = true;
		m_Anim.SetTrigger("Hit");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Destroy)
			return;

		else if (collision.tag == "Bullet")
			return;

		if (m_Owner == Bullet_Owner.Player) // 플레이어 총기
		{
			if (collision.tag == "Monster")
			{
				m_Target = collision.gameObject.GetComponent<Character>();

				if (m_Target == null)
					Debug.LogError("if (m_Target == null)");

				if (m_Target.Death)
					return;

				else if (!m_Pierce)
					HitAnim();
			}

			else if (collision.tag == "Player")
				return;

			else // 오브젝트 충돌
				NoHitAnim();
		}

		else if (m_Owner == Bullet_Owner.Monster) // 몬스터 총기
		{
			if (collision.tag == "Player")
			{
				Player player = collision.gameObject.GetComponent<Player>();

				if (player.NoHit)
					return;

				HitAnim();
			}

			else if (collision.tag == "Monster")
				return;

			else // 오브젝트 충돌
				NoHitAnim();
		}
	}

	private void Awake()
	{
		m_Anim = GetComponent<Animator>();

		if (m_Anim == null)
			Debug.LogError("if (m_Anim == null)");

		m_Collider = GetComponent<CircleCollider2D>();

		if (m_Collider == null)
			Debug.LogError("if (m_Collider == null)");
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
			if (m_Dir == Vector3.zero)
				Debug.LogError("if (m_Dir == Vector3.zero)");

			m_MoveDist = m_Speed * Time.deltaTime;
			m_AccRange += m_MoveDist;

			transform.position += m_Dir * m_MoveDist;
		}
	}
}

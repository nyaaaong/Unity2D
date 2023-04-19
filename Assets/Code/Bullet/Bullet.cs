using UnityEngine;

public class Bullet : Global
{
	private Vector3 m_Dir = Vector2.zero;
	private float m_Damage = 1.0f;
	private float m_Speed = 1.0f;
	private float m_Range = 1.0f;
	private float m_AccRange = 0.0f;
	private float m_FirstDist = 5.0f; // 발사할 때 어디서부터 시작될 지
	private Bullet_Owner m_Owner = Bullet_Owner.Player;
	private Animator m_Anim = null;
	private CircleCollider2D m_Collider = null;
	private Character m_Target = null;
	private Rigidbody2D m_Rig = null;
	private bool m_Destroy = false;
	private bool m_Pierce = false;
	private bool m_HitAnim = false;

	public Bullet_Owner Owner { get { return m_Owner; } set { m_Owner = value; } }
	public Vector3 Dir { get { return m_Dir; } set { m_Dir = value; } }
	public float Damage { get { return m_Damage; } }
	public float Speed { set { m_Speed = value; } }

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
		m_FirstDist = bullet.m_FirstDist;
		m_Owner = bullet.m_Owner;
		m_Pierce = bullet.m_Pierce;
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

	private void HitAnim()
	{
		m_Destroy = true;
		m_HitAnim = true;
		m_Rig.velocity = Vector3.zero;

		m_Anim.SetTrigger("Hit");
	}

	private void NoHitAnim()
	{
		m_Destroy = true;
		m_HitAnim = true;
		m_Rig.velocity = Vector3.zero;

		m_Anim.SetTrigger("NoHit");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Destroy)
			return;

		else if (collision.CompareTag("Bullet"))
			return;

		if (m_Owner == Bullet_Owner.Player) // 플레이어 총기
		{
			if (collision.CompareTag("Monster") ||
				collision.CompareTag("Boss"))
			{
				m_Target = collision.gameObject.GetComponent<Character>();

				if (m_Target == null)
					Debug.LogError("if (m_Target == null)");

				if (m_Target.Death)
					return;

				m_Target.SetDamage(m_Damage);

				if (!m_Pierce)
					HitAnim();
			}

			else if (collision.CompareTag("Player"))
				return;

			else if (collision.CompareTag("Wall"))
				NoHitAnim();
		}

		else if (m_Owner == Bullet_Owner.Monster) // 몬스터 총기
		{
			if (collision.CompareTag("Player"))
			{
				m_Target = collision.gameObject.GetComponent<Player>();

				if (m_Target.NoHit)
				{
					NoHitAnim();
					return;
				}

				m_Target.SetDamage(m_Damage);
				HitAnim();
			}

			else if (collision.CompareTag("Monster"))
				return;

			else if (collision.CompareTag("Wall"))
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

		m_Rig = GetComponent<Rigidbody2D>();

		if (m_Rig == null)
			Debug.LogError("if (m_Rig == null)");
	}

	protected override void MiddleUpdate()
	{
		base.MiddleUpdate();

		if (m_AccRange >= m_Range && !m_Destroy && !m_HitAnim)
			NoHitAnim();

		else if (!m_Destroy && !m_HitAnim)
		{
			if (m_Dir == Vector3.zero)
				Debug.LogError("if (m_Dir == Vector3.zero)");

			m_AccRange += m_Speed * Time.deltaTime;
		}
	}

	protected override void FixedUpdate()
	{
		if (!m_Destroy && !m_HitAnim)
			m_Rig.velocity = m_Dir * m_Speed;
	}
}

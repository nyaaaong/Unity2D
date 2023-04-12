using UnityEngine;

public class Boss : Monster
{
	private Chair m_Chair = null;
	private Renderer m_Renderer = null;
	private string m_Idle = "Idle";
	private string m_Pattern1 = "Pattern1";
	private string m_Pattern2 = "Pattern2";
	private string m_Pattern3_Start = "Pattern3_Start";
	private string m_Pattern3_End = "Pattern3_End";
	private string m_Die = "Death";
	private string m_Die_End = "Death_End";
	private bool m_DieAnim = false;
	private bool m_DieEndAnim = false;
	private float m_DieAnimTime = 0f;
	private float m_DieEndAnimTime = 0f;
	private float m_DieAnimTimeMax = 3f;
	private float m_DieEndAnimTimeMax = 1f;

	private void DieAnimCheck()
	{
		if (m_DieAnim)
		{
			m_DieAnimTime += m_deltaTime;

			if (m_DieAnimTime >= m_DieAnimTimeMax)
			{
				m_DieAnim = false;
				m_DieEndAnim = true;

				ChangeAnim(m_Die_End);
			}
		}
	}

	private void DieEndAnimCheck()
	{
		if (m_DieEndAnim)
		{
			m_DieEndAnimTime += m_deltaTime;

			if (m_DieEndAnimTime >= m_DieEndAnimTimeMax)
			{
				m_DieEndAnim = false;

				Destroy();
				m_Chair.DestroyChair();
			}
		}
	}

	protected override void DeathAnimCheck()
	{
		if (m_Death && !m_DeathAnimProc)
		{
			m_Animator.SetTrigger(m_Die);

			m_DieAnim = true;
			m_DeathAnimProc = true;
		}
	}

	public void SetEnable(bool enable)
	{
		m_Renderer.enabled = enable;
	}

	public bool PlayPattern3Start()
	{
		return PlayAnim(m_Pattern3_Start);
	}

	public bool PlayPattern3End()
	{
		return PlayAnim(m_Pattern3_End);
	}

	public void Die()
	{
		ChangeAnim(m_Die);
	}

	public void Pattern1()
	{
		ChangeAnim(m_Pattern1);
	}

	public void Pattern2()
	{
		ChangeAnim(m_Pattern2);
	}

	public void Pattern3_Start()
	{
		ChangeAnim(m_Pattern3_Start);
	}

	public void Pattern3_End()
	{
		ChangeAnim(m_Pattern3_End);
	}

	public void Idle()
	{
		ChangeAnim(m_Idle);
	}

	protected override void Awake()
	{
		base.Awake();

		m_Boss = true;

		m_Chair = transform.parent.GetComponent<Chair>();

		if (m_Chair == null)
			Debug.LogError("if (m_Chair == null)");

		m_Renderer = GetComponent<Renderer>();

		if (m_Renderer == null)
			Debug.LogError("if (m_Renderer == null)");
	}

	protected override void Start()
	{
		base.Start();

		m_Info = m_Chair.CharInfo;
	}

	protected override void Update()
	{
		base.Update();

		if (!m_Death)
		{
			m_Info.m_HP = m_Chair.HP;
			m_Death = m_Chair.Death;
			m_SR.color = m_Chair.IsRed ? Color.red : Color.white;
		}

		DieAnimCheck();
		DieEndAnimCheck();
	}
}

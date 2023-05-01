using UnityEngine;

public class Global : MonoBehaviour
{
	protected float m_deltaTime = 0.0f;

	public Vector2 ConvertDir(float angle)
	{
		Vector2 Result;

		angle *= Mathf.Deg2Rad;

		Result = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		Result.Normalize();

		return Result;
	}

	protected virtual void BeforeUpdate()
	{
		m_deltaTime = Time.deltaTime;
	}

	protected virtual void AfterUpdate()
	{

	}

	protected virtual void FixedUpdate()
	{
		m_deltaTime = Time.deltaTime;
	}

	protected virtual void Update()
	{
		BeforeUpdate();
		AfterUpdate();
	}
}

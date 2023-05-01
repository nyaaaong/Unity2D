using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public delegate void FadeCompleteFunc();

public class Fade : Global
{
	private FadeCompleteFunc m_FadeInFunc = null;
	private FadeCompleteFunc m_FadeOutFunc = null;
	private Image m_Image = null;
	private float m_FadeSpeed = 1f;
	private float m_Alpha = 0f;
	private bool m_FadeInComplete = false;
	private bool m_FadeOutComplete = false;

	public void SetFadeInFunc(FadeCompleteFunc func)
	{
		m_FadeInFunc = func;
	}

	public void SetFadeOutFunc(FadeCompleteFunc func)
	{
		m_FadeOutFunc = func;
	}

	public void FadeIn()
	{
		StopAllCoroutines();
		StartCoroutine(FadeInLoop());
	}

	public void FadeOut()
	{
		StopAllCoroutines();
		StartCoroutine(FadeOutLoop());
	}

	private IEnumerator FadeInLoop()
	{
		while (m_Image.color.a < 1f)
		{
			m_Alpha = m_Image.color.a + Time.deltaTime * m_FadeSpeed;
			m_Alpha = m_Alpha > 1f ? 1f : m_Alpha;
			m_Image.color = new Color(0f, 0f, 0f, m_Alpha);
			yield return null;
		}

		m_FadeInComplete = true;

		if (m_FadeInFunc != null)
			m_FadeInFunc();
	}

	private IEnumerator FadeOutLoop()
	{
		while (m_Image.color.a > 0f)
		{
			m_Alpha = m_Image.color.a - Time.deltaTime * m_FadeSpeed;
			m_Alpha = m_Alpha < 0f ? 0f : m_Alpha;
			m_Image.color = new Color(0f, 0f, 0f, m_Alpha);
			yield return null;
		}

		m_FadeOutComplete = true;

		if (m_FadeOutFunc != null)
			m_FadeOutFunc();
	}

	private void Awake()
	{
		m_Image = GetComponent<Image>();

		if (m_Image == null)
			Debug.LogError("if (m_Image == null)");

		m_FadeSpeed = UIManager.FadeSpeed;
	}

	protected override void AfterUpdate()
	{
		base.AfterUpdate();

		if (m_FadeInComplete || m_FadeOutComplete)
		{
			StopAllCoroutines();

			transform.parent.gameObject.SetActive(false);

			m_FadeInComplete = false;
			m_FadeOutComplete = false;
		}
	}
}

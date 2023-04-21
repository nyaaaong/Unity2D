using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBase : Global, IPointerEnterHandler // IPointerEnterHandler는 OnPointerEnter를 사용하기 위한 인터페이스
{
	private Button m_Button = null;

	public void ButtonClicked()
	{
		m_Button.interactable = false;
	}

	private void Awake()
	{
		m_Button = GetComponent<Button>();

		if (m_Button == null)
			Debug.LogError("if (m_Button == null)");

		// 클릭 이벤트 등록
		m_Button.onClick.AddListener(OnPress);
	}

	// 마우스를 버튼에 대면 효과음 출력
	public void OnPointerEnter(PointerEventData eventData)
	{
		TitleSceneManager.PlayHighlight();
	}

	private void OnPress()
	{
		// 버튼을 누르면 모든 버튼을 비활성화 시킨다.
		TitleSceneManager.ButtonClicked();

		TitleSceneManager.PlayPress();

		StartCoroutine(PlayingSoundCheck());
	}

	protected virtual void ButtonEvent()
	{
	}

	private IEnumerator PlayingSoundCheck()
	{
		yield return new WaitForSeconds(TitleSceneManager.PressLength());

		ButtonEvent();
	}
}
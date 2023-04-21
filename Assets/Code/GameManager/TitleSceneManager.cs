using UnityEngine;

public class TitleSceneManager : Global
{
	[SerializeField]
	private GameObject m_GameManager = null;
	[SerializeField]
	private GameObject m_StartButtonObj = null;
	[SerializeField]
	private GameObject m_ExitButtonObj = null;

	static private TitleSceneManager m_Inst = null;

	private AudioClip m_UIMenuHighlight = null;
	private AudioClip m_UIMenuPress = null;
	private AudioSource m_Audio = null;
	private ButtonBase m_StartButton = null;
	private ButtonBase m_ExitButton = null;

	static public void ButtonClicked()
	{
		m_Inst.m_StartButton.ButtonClicked();
		m_Inst.m_ExitButton.ButtonClicked();
	}

	static public bool IsPlaying()
	{
		return m_Inst.m_Audio.isPlaying;
	}

	static public void PlayHighlight()
	{
		m_Inst.m_Audio.PlayOneShot(m_Inst.m_UIMenuHighlight);
	}

	static public void PlayPress()
	{
		m_Inst.m_Audio.PlayOneShot(m_Inst.m_UIMenuPress);
	}

	static public float PressLength()
	{
		return m_Inst.m_UIMenuPress.length;
	}

	private void Awake()
	{
		m_Inst = this;

		if (m_GameManager == null)
			Debug.LogError("if (m_GameManager == null)");

		DontDestroyOnLoad(m_GameManager);

		if (m_StartButtonObj == null)
			Debug.LogError("if (m_StartButtonObj == null)");

		m_StartButton = m_StartButtonObj.GetComponent<ButtonBase>();

		if (m_StartButton == null)
			Debug.LogError("if (m_StartButton == null)");

		if (m_ExitButtonObj == null)
			Debug.LogError("if (m_ExitButtonObj == null)");

		m_ExitButton = m_ExitButtonObj.GetComponent<ButtonBase>();

		if (m_ExitButton == null)
			Debug.LogError("if (m_ExitButton == null)");

		m_UIMenuHighlight = AudioManager.UIMenuHighlight;
		m_UIMenuPress = AudioManager.UIMenuPress;

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio == null)
			Debug.LogError("if (m_Audio == null)");

		m_Audio.volume = AudioManager.EffectVolume;
	}

	private void Start()
	{
		AudioManager.PlayBGM(BGM_Type.Title);
	}
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private float m_EffectVolume = 0.1f;
	[SerializeField]
	private float m_BGMVolume = 0.05f;
	[SerializeField]
	private AudioClip m_BGMTitle = null;
	[SerializeField]
	private AudioClip m_BGMMain = null;
	[SerializeField]
	private AudioClip m_BGMBoss = null;
	[SerializeField]
	private AudioClip m_BGMBossClear = null;
	[SerializeField]
	private AudioClip m_BGMEnding = null;
	[SerializeField]
	private AudioClip m_HitEffectAudio = null;
	[SerializeField]
	private AudioClip m_DeathEffectAudio = null;
	[SerializeField]
	private AudioClip[] m_DeathAudio = null;
	[SerializeField]
	private AudioClip m_BossPattern1 = null;
	[SerializeField]
	private AudioClip m_BossPattern2 = null;
	[SerializeField]
	private AudioClip m_BossPattern3 = null;
	[SerializeField]
	private AudioClip m_BossDie = null;

	static private AudioManager m_Inst = null;

	private AudioSource m_Audio = null;
	private BGM_Type m_BGMType = BGM_Type.None;

	public static float EffectVolume { get { return m_Inst.m_EffectVolume; } }
	public static float BGMVolume { get { return m_Inst.m_BGMVolume; } }
	public static AudioClip HitEffectAudio { get { return m_Inst.m_HitEffectAudio; } }
	public static AudioClip DeathEffectAudio { get { return m_Inst.m_DeathEffectAudio; } }
	public static AudioClip[] DeathAudio { get { return m_Inst.m_DeathAudio; } }
	public static AudioClip BossPattern1 { get { return m_Inst.m_BossPattern1; } }
	public static AudioClip BossPattern2 { get { return m_Inst.m_BossPattern2; } }
	public static AudioClip BossPattern3 { get { return m_Inst.m_BossPattern3; } }
	public static AudioClip BossDie { get { return m_Inst.m_BossDie; } }

	static public void PlayBGM(BGM_Type type)
	{
		if (m_Inst.m_BGMType == type)
			return;

		m_Inst.m_BGMType = type;
		m_Inst.m_Audio.Stop();

		switch (m_Inst.m_BGMType)
		{
			case BGM_Type.Title:
				m_Inst.m_Audio.clip = m_Inst.m_BGMTitle;
				break;
			case BGM_Type.Main:
				m_Inst.m_Audio.clip = m_Inst.m_BGMMain;
				break;
			case BGM_Type.Boss:
				m_Inst.m_Audio.clip = m_Inst.m_BGMBoss;
				break;
			case BGM_Type.Boss_Clear:
				m_Inst.m_Audio.clip = m_Inst.m_BGMBossClear;
				break;
			case BGM_Type.Ending:
				m_Inst.m_Audio.clip = m_Inst.m_BGMEnding;
				break;
		}

		m_Inst.m_Audio.Play();
	}

	private void Awake()
	{
		m_Inst = this;

		m_Audio = GetComponent<AudioSource>();

		if (m_Audio == null)
			Debug.LogError("if (m_Audio == null)");

		if (m_BGMTitle == null)
			Debug.LogError("if (m_BGMTitle == null)");

		if (m_BGMMain == null)
			Debug.LogError("if (m_BGMMain == null)");

		if (m_BGMBoss == null)
			Debug.LogError("if (m_BGMBoss == null)");

		if (m_BGMBossClear == null)
			Debug.LogError("if (m_BGMBossClear == null)");

		if (m_BGMEnding == null)
			Debug.LogError("if (m_BGMEnding == null)");

		if (m_HitEffectAudio == null)
			Debug.LogError("if (m_HitEffectAudio == null)");

		if (m_DeathEffectAudio == null)
			Debug.LogError("if (m_DeathEffectAudio == null)");

		if (m_DeathAudio == null)
			Debug.LogError("if (m_DeathAudio == null)");

		if (m_BossPattern1 == null)
			Debug.LogError("if (m_BossPattern1 == null)");

		if (m_BossPattern2 == null)
			Debug.LogError("if (m_BossPattern2 == null)");

		if (m_BossPattern3 == null)
			Debug.LogError("if (m_BossPattern3 == null)");

		if (m_BossDie == null)
			Debug.LogError("if (m_BossDie == null)");

		m_Audio.volume = AudioManager.BGMVolume;
		m_Audio.loop = true;
	}
}

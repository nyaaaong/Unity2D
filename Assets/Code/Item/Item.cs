using UnityEngine;

public class Item : MonoBehaviour
{
	private AudioSource m_Audio = null;
	protected Item_Type		m_Type = Item_Type.End;

	private float   m_deltaTime = 0.0f;
	private bool    m_DropAnim = true;
	private bool    m_FirstAnim = true;
	private bool    m_SecondAnim = false;
	private bool    m_ThirdAnim = false;

	private float   m_DropHeight = 0.0f;
	private float   m_DropHeightMax = 0.0f;
	private float   m_DropSecondHeightMax = 0.0f;
	private float   m_DropSpeed = 0.0f;

	public Item_Type Type { get { return m_Type; } }

	protected virtual void LootingEvent()
	{
		GameObject effect = Instantiate(ItemManager.LootEffectPrefeb);
		effect.transform.position = transform.position;
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player")
			return;

		LootingEvent();
	}

	protected virtual void Awake()
	{
		m_Audio = GetComponent<AudioSource>();

		if (m_Audio == null)
			Debug.LogError("if (m_Audio == null)");

		m_Audio.volume = Global.EffectVolume;

		m_DropSpeed = ItemManager.DropSpeed;
		m_DropHeightMax = ItemManager.DropHeight;
		m_DropSecondHeightMax = ItemManager.DropSecondHeight;
	}

	protected virtual void Update()
	{
		if (m_DropAnim)
		{
			m_deltaTime = Time.deltaTime;

			if (m_FirstAnim) // ¹Ù´Ú±îÁö ¶³¾îÁø´Ù
			{
				m_DropHeight += m_DropSpeed * m_deltaTime;
				transform.position += Vector3.down * m_DropSpeed * m_deltaTime;

				if (m_DropHeight >= m_DropHeightMax)
				{
					m_FirstAnim = false;
					m_SecondAnim = true;

					m_DropHeight = 0.0f;
				}
			}

			else if (m_SecondAnim) // ¾à°£ Æ¢¾î¿À¸¥´Ù
			{
				m_DropHeight += m_DropSpeed * m_deltaTime;
				transform.position += Vector3.up * m_DropSpeed * m_deltaTime;

				if (m_DropHeight >= m_DropSecondHeightMax)
				{
					m_SecondAnim = false;
					m_ThirdAnim = true;

					m_DropHeight = 0.0f;
				}
			}

			else if (m_ThirdAnim) // ´Ù½Ã ¹Ù´Ú±îÁö ¶³¾îÁø´Ù
			{
				m_DropHeight += m_DropSpeed * m_deltaTime;
				transform.position += Vector3.down * m_DropSpeed * m_deltaTime;

				if (m_DropHeight >= m_DropSecondHeightMax)
				{
					m_SecondAnim = false;
					m_ThirdAnim = false;

					m_DropAnim = false;
				}
			}
		}
	}
}

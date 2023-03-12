using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField]
	private float	m_DropHeight = 1.0f;
	[SerializeField]
	private GameObject	m_LootEffectPrefeb = null;

	private bool	m_DropAnim = true;
	protected Item_Type		m_Type = Item_Type.End;

	public Item_Type Type { get { return m_Type; } }

	protected virtual void LootingEvent()
	{
		GameObject effect = Instantiate(m_LootEffectPrefeb);
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
		if (m_LootEffectPrefeb == null)
			Debug.LogError("if (m_LootEffectPrefeb == null)");
	}

	protected virtual void Update()
	{
		if (m_DropAnim)
		{

		}
	}
}

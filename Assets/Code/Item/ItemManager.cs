
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	[SerializeField]
	private GameObject  m_RiflePrefeb = null;
	[SerializeField]
	private GameObject  m_SniperPrefeb = null;
	[SerializeField]
	private GameObject  m_HeartPrefeb = null;
	[SerializeField]
	private GameObject  m_LootEffectPrefeb = null;
	[SerializeField]
	private float       m_DropOffsetY = 0.5f;
	[SerializeField]
	private float   m_DropHeight = 1.0f;
	[SerializeField]
	private float   m_DropSpeed = 5.0f;

	private static ItemManager m_Inst = null;
	private GameObject[]    m_Item = null;
	private float   m_DropSecondHeight = 0.0f;

	private bool    m_DroppedRifle = false;
	private bool    m_DroppedSniper = false;

	public static GameObject LootEffectPrefeb { get { return m_Inst.m_LootEffectPrefeb; } }
	public static float DropHeight { get { return m_Inst.m_DropHeight; } }
	public static float DropSecondHeight { get { return m_Inst.m_DropSecondHeight; } }
	public static float DropSpeed { get { return m_Inst.m_DropSpeed; } }

	private static bool IsDroppedWeap(Item_Type type)
	{
		switch (type)
		{
			case Item_Type.Rifle:
				return m_Inst.m_DroppedRifle;
			case Item_Type.Sniper:
				return m_Inst.m_DroppedSniper;
			case Item_Type.Heart:
			case Item_Type.End:
				Debug.LogError("잘못 입력함");
				break;
		}

		return false;
	}

	private static bool IsDroppedWeapAll()
	{
		return m_Inst.m_DroppedRifle && m_Inst.m_DroppedSniper;
	}

	public static void CreateItem(Vector3 pos)
	{
		int idx = (int)Item_Type.Heart;
		Item_Type   type = Item_Type.Heart;

		if (!Global.Player.HasWeaponAll() || !IsDroppedWeapAll())
		{
			bool Loop = false;

			do
			{
				idx = Random.Range(0, (int)Item_Type.End);
				type = (Item_Type)idx;
				Loop = true;

				switch (type)
				{
					case Item_Type.Rifle:
						if (!Global.Player.HasWeapon(type) && !IsDroppedWeap(Item_Type.Rifle))
						{
							Loop = false;
							m_Inst.m_DroppedRifle = true;
						}
						break;
					case Item_Type.Sniper:
						if (!Global.Player.HasWeapon(type) && !IsDroppedWeap(Item_Type.Sniper))
						{
							Loop = false;
							m_Inst.m_DroppedSniper = true;
						}
						break;
					case Item_Type.Heart:
						Loop = false;
						break;
				}

			} while (Loop);
		}
		
		GameObject item = Instantiate(m_Inst.m_Item[idx].gameObject);

		Vector3 resPos = pos;
		resPos.y += m_Inst.m_DropOffsetY;
		resPos.z = -1.0f;

		item.transform.position = resPos;
	}

	public void Awake()
	{
		if (m_RiflePrefeb == null)
			Debug.LogError("if (m_RiflePrefeb == null)");

		if (m_SniperPrefeb == null)
			Debug.LogError("if (m_SniperPrefeb == null)");

		if (m_HeartPrefeb == null)
			Debug.LogError("if (m_HeartPrefeb == null)");

		if (m_LootEffectPrefeb == null)
			Debug.LogError("if (m_LootEffectPrefeb == null)");

		m_Item = new GameObject[(int)Item_Type.End];

		m_Item[(int)Item_Type.Rifle] = m_RiflePrefeb;
		m_Item[(int)Item_Type.Sniper] = m_SniperPrefeb;
		m_Item[(int)Item_Type.Heart] = m_HeartPrefeb;

		m_Inst = this;

		m_DropSecondHeight = m_DropHeight * 0.65f;
	}

	public void Update()
	{

	}
}
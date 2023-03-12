
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	private Item[]	m_Item = null;

	public void CreateItem(Vector3 pos)
	{
		int idx = 0;

		do
		{
			idx = Random.Range(0, (int)Item_Type.End);

			if (Global.Player.HasWeaponAll())
				break;

		} while (Global.Player.HasWeapon(idx));
		

		GameObject item = Instantiate(m_Item[idx].gameObject);
		item.transform.position = pos;
	}

	public void Awake()
	{
		m_Item[(int)Item_Type.Rifle] = new Rifle();
		m_Item[(int)Item_Type.Sniper] = new Sniper();
		m_Item[(int)Item_Type.Heart] = new Heart();
	}

	public void Update()
	{

	}
}
using UnityEngine;

public class LootEffect : MonoBehaviour
{
	private void EffectAnimEnd()
	{
		Destroy(gameObject);
	}
}
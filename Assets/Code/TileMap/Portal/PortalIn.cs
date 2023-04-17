using UnityEngine;

public class PortalIn : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			EnvironmentManager.PlayerMovePortalOut();
	}
}

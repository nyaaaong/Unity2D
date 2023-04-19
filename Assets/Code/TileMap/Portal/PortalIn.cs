using UnityEngine;

public class PortalIn : Global
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			EnvironmentManager.PlayerMovePortalOut();
	}
}

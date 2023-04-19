using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : Global
{
	protected override void MiddleUpdate()
	{
		base.MiddleUpdate();

		if (Input.GetKeyDown(KeyCode.Space))
			LoadingScene.LoadScene("PlayScene");
	}
}

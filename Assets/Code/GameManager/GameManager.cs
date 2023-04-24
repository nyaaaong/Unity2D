using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		// 수직동기화 해제
		QualitySettings.vSyncCount = 0;

		// 최대 프레임을 75로 제한
		Application.targetFrameRate = 75;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
	private Global m_Inst = null;

	private void Awake()
	{
		m_Inst = this;
	}
}

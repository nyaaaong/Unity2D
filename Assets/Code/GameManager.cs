using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager	:	MonoBehaviour
{
	private static GameManager	m_Inst	= null;
	private ItemManager m_ItemManager = null;

	private static void Awake()
	{
		m_Inst = new GameManager();
		m_Inst.m_ItemManager = new ItemManager();
	}

	public static GameManager GetInst()
	{
		return m_Inst;
	}
}
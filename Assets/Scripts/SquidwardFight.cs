using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class SquidwardFight : MonoBehaviour
{
	// Token: 0x06000131 RID: 305 RVA: 0x0000730C File Offset: 0x0000550C
	private void Start()
	{
		if (ObjectiveManager.currentObjective != 1)
		{
			Object.Destroy(base.gameObject);
		}
	}
}

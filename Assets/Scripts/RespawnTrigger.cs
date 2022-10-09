using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class RespawnTrigger : MonoBehaviour
{
	// Token: 0x06000109 RID: 265 RVA: 0x00006C64 File Offset: 0x00004E64
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !PlayerMovement.dead)
		{
			PlayerMovement.dead = true;
			GameManager.Get().ReloadLevel();
		}
	}
}

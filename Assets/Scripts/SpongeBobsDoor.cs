using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class SpongeBobsDoor : MonoBehaviour
{
	// Token: 0x0600012D RID: 301 RVA: 0x00007284 File Offset: 0x00005484
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = true;
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000729A File Offset: 0x0000549A
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = false;
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000072B0 File Offset: 0x000054B0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.canUse && !this.used)
		{
			if (this.outdoor)
			{
				GameManager.Get().LoadLevel(Level.SPONGEBOBSHOUSE);
			}
			else
			{
				GameManager.Get().LoadLevel(Level.BIKINIBOTTOM);
			}
			AudioManager.Get().Play("GenericDoorOpen");
			this.used = true;
		}
	}

	// Token: 0x0400014C RID: 332
	public bool outdoor;

	// Token: 0x0400014D RID: 333
	private bool canUse;

	// Token: 0x0400014E RID: 334
	private bool used;
}

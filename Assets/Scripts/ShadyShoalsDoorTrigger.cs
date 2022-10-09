using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class ShadyShoalsDoorTrigger : MonoBehaviour
{
	// Token: 0x0600011F RID: 287 RVA: 0x0000706F File Offset: 0x0000526F
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = true;
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007085 File Offset: 0x00005285
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = false;
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000709C File Offset: 0x0000529C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.canUse && !this.used)
		{
			if (this.outdoor)
			{
				GameManager.Get().LoadLevel(Level.SHADYSHOALS);
			}
			else
			{
				GameManager.Get().LoadLevel(Level.BIKINIBOTTOM);
			}
			AudioManager.Get().Play("GenericDoorOpen");
			this.used = true;
		}
	}

	// Token: 0x04000143 RID: 323
	public bool outdoor;

	// Token: 0x04000144 RID: 324
	private bool canUse;

	// Token: 0x04000145 RID: 325
	private bool used;
}

using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class BankDoor : MonoBehaviour
{
	// Token: 0x06000059 RID: 89 RVA: 0x00003B90 File Offset: 0x00001D90
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = true;
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003BA6 File Offset: 0x00001DA6
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = false;
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003BBC File Offset: 0x00001DBC
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.canUse && !this.used)
		{
			if (this.outdoor)
			{
				GameManager.Get().LoadLevel(Level.BANK);
			}
			else
			{
				GameManager.Get().LoadLevel(Level.BIKINIBOTTOM);
			}
			AudioManager.Get().Play("GenericDoorOpen");
			this.used = true;
		}
	}

	// Token: 0x0400005A RID: 90
	public bool outdoor;

	// Token: 0x0400005B RID: 91
	private bool canUse;

	// Token: 0x0400005C RID: 92
	private bool used;
}

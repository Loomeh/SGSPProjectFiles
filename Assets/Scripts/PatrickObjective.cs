using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class PatrickObjective : MonoBehaviour
{
	// Token: 0x060000E1 RID: 225 RVA: 0x00005A43 File Offset: 0x00003C43
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.patrick.patrickTalkCheck = true;
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00005A5E File Offset: 0x00003C5E
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.patrick.patrickTalkCheck = false;
		}
	}

	// Token: 0x040000F9 RID: 249
	public Patrick patrick;
}

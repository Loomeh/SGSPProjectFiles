using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class DamageTrigger : MonoBehaviour
{
	// Token: 0x0600007C RID: 124 RVA: 0x00003E82 File Offset: 0x00002082
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerMovement.UpdateHealth(-25f);
		}
	}
}

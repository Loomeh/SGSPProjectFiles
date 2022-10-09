using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class FPSDemoReactivator : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x000023F0 File Offset: 0x000005F0
	private void Start()
	{
		base.InvokeRepeating("Reactivate", this.StartDelay, this.TimeDelayToReactivate);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002409 File Offset: 0x00000609
	private void Reactivate()
	{
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0400000B RID: 11
	public float StartDelay;

	// Token: 0x0400000C RID: 12
	public float TimeDelayToReactivate = 3f;
}

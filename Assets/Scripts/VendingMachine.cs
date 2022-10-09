using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class VendingMachine : MonoBehaviour
{
	// Token: 0x0600015A RID: 346 RVA: 0x00007A24 File Offset: 0x00005C24
	public void Shot()
	{
		Object.Instantiate<GameObject>(this.kelpBarPrefab, this.kelpBarSpawnPosition.position, this.kelpBarSpawnPosition.rotation);
	}

	// Token: 0x0400016F RID: 367
	public GameObject kelpBarPrefab;

	// Token: 0x04000170 RID: 368
	public Transform kelpBarSpawnPosition;
}

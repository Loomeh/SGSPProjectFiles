using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class DeactivateLootTrigger : MonoBehaviour
{
	// Token: 0x0600007E RID: 126 RVA: 0x00003E9B File Offset: 0x0000209B
	public void DeactivateLootText()
	{
		HUDManager.Get().itemGetText.gameObject.SetActive(false);
	}
}

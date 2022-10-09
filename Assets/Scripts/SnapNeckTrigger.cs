using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class SnapNeckTrigger : MonoBehaviour
{
	// Token: 0x06000129 RID: 297 RVA: 0x000071E4 File Offset: 0x000053E4
	private void LateUpdate()
	{
		if (this.inRange && Input.GetKeyDown(KeyCode.E) && !TextManager.Get().talking)
		{
			HUDManager.Get().ineractText.gameObject.SetActive(false);
			ObjectiveManager.Get().CompleteObjective();
			SaveManager.Save();
			AudioManager.Get().Play("NeckSnap");
			this.patrick.Shot();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00007258 File Offset: 0x00005458
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = true;
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000726E File Offset: 0x0000546E
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = false;
		}
	}

	// Token: 0x0400014A RID: 330
	public Patrick patrick;

	// Token: 0x0400014B RID: 331
	private bool inRange;
}

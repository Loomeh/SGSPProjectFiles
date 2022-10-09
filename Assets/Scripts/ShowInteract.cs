using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class ShowInteract : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x0000716C File Offset: 0x0000536C
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			HUDManager.Get().ineractText.gameObject.SetActive(true);
			HUDManager.Get().ineractText.text = this.button + " - " + this.action;
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x000071C0 File Offset: 0x000053C0
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			HUDManager.Get().ineractText.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000148 RID: 328
	public string button;

	// Token: 0x04000149 RID: 329
	public string action;
}

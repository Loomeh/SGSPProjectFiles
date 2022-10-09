using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class TalkTrigger : MonoBehaviour
{
	// Token: 0x06000139 RID: 313 RVA: 0x00007449 File Offset: 0x00005649
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canTalk = true;
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000745F File Offset: 0x0000565F
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.talking = false;
			this.canTalk = false;
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000747C File Offset: 0x0000567C
	private void Update()
	{
		if (this.canTalk && Input.GetKeyDown(KeyCode.E) && !this.talking)
		{
			this.talking = true;
			HUDManager.Get().ineractText.gameObject.SetActive(false);
			TextManager.Get().Talk(this.text);
		}
	}

	// Token: 0x04000154 RID: 340
	public string text;

	// Token: 0x04000155 RID: 341
	private bool canTalk;

	// Token: 0x04000156 RID: 342
	private bool talking;
}

using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class LootTrigger : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00004DC4 File Offset: 0x00002FC4
	private void Update()
	{
		if (this.inRange && Input.GetKeyDown(KeyCode.E) && !TextManager.Get().talking && PlayerMovement.isGrounded)
		{
			PlayerMovement.UpdateMoney(this.value);
			AudioManager.Get().Play("BubblePop");
			HUDManager.Get().itemGetText.text = "+" + this.amount + " " + this.item;
			HUDManager.Get().itemGetText.gameObject.SetActive(true);
			HUDManager.Get().ineractText.gameObject.SetActive(false);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00004E78 File Offset: 0x00003078
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = true;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00004E8E File Offset: 0x0000308E
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = false;
		}
	}

	// Token: 0x040000C3 RID: 195
	public float value;

	// Token: 0x040000C4 RID: 196
	public string amount;

	// Token: 0x040000C5 RID: 197
	public string item;

	// Token: 0x040000C6 RID: 198
	private bool inRange;
}

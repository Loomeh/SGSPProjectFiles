using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class TextTrigger : MonoBehaviour
{
	// Token: 0x06000144 RID: 324 RVA: 0x00007627 File Offset: 0x00005827
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && !TextManager.Get().talking && PlayerMovement.isGrounded)
		{
			TextManager.Get().Talk(this.text);
		}
	}

	// Token: 0x0400015E RID: 350
	public string text;
}

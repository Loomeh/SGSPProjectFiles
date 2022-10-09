using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class DoorTrigger : MonoBehaviour
{
	// Token: 0x06000086 RID: 134 RVA: 0x00004066 File Offset: 0x00002266
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = true;
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000407C File Offset: 0x0000227C
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = false;
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004092 File Offset: 0x00002292
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.canUse)
		{
			this.door.Use();
		}
	}

	// Token: 0x0400007C RID: 124
	public Door door;

	// Token: 0x0400007D RID: 125
	private bool canUse;
}

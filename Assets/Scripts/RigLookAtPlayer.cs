using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// Token: 0x02000044 RID: 68
public class RigLookAtPlayer : MonoBehaviour
{
	// Token: 0x0600010B RID: 267 RVA: 0x00006C8C File Offset: 0x00004E8C
	private void Update()
	{
		if (this.playerInRadius)
		{
			if (this.lookAtPoint.position != this.targetPos)
			{
				this.lookAtPoint.position = Vector3.Lerp(this.lookAtPoint.position, this.targetPos, 3f * Time.deltaTime);
				return;
			}
		}
		else if (this.lookAtPoint.position != this.defaultLookAtPoint.position)
		{
			this.lookAtPoint.position = Vector3.Lerp(this.lookAtPoint.position, this.defaultLookAtPoint.position, 3f * Time.deltaTime);
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00006D34 File Offset: 0x00004F34
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.playerInRadius = true;
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00006D4A File Offset: 0x00004F4A
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.targetPos = other.transform.position + this.offset;
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00006D75 File Offset: 0x00004F75
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.playerInRadius = false;
		}
	}

	// Token: 0x04000134 RID: 308
	public RigBuilder rig;

	// Token: 0x04000135 RID: 309
	public Transform lookAtPoint;

	// Token: 0x04000136 RID: 310
	public Transform defaultLookAtPoint;

	// Token: 0x04000137 RID: 311
	public Vector3 offset;

	// Token: 0x04000138 RID: 312
	private Vector3 targetPos;

	// Token: 0x04000139 RID: 313
	private bool playerInRadius;
}

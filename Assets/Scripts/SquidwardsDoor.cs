using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class SquidwardsDoor : MonoBehaviour
{
	// Token: 0x06000133 RID: 307 RVA: 0x00007321 File Offset: 0x00005521
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.canUse = true;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00007337 File Offset: 0x00005537
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.used = false;
			this.canUse = false;
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00007354 File Offset: 0x00005554
	private void Start()
	{
		if (this.outdoor && ObjectiveManager.currentObjective == 1)
		{
			this.openInteractText.SetActive(false);
			this.kickDownInteractText.SetActive(true);
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00007380 File Offset: 0x00005580
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.canUse)
		{
			this.used = true;
			if (this.outdoor)
			{
				if (ObjectiveManager.currentObjective == 0)
				{
					TextManager.Get().Talk("It's locked.;It's quiet inside there.; You hear a shotgun click.");
				}
				else
				{
					GameManager.Get().LoadLevel(Level.SQUIDWARDSHOUSE);
				}
				if (ObjectiveManager.currentObjective == 0)
				{
					AudioManager.Get().Play("DoorLocked");
					return;
				}
				if (ObjectiveManager.currentObjective == 1)
				{
					AudioManager.Get().Play("DoorKickDown");
					return;
				}
				AudioManager.Get().Play("GenericDoorOpen");
				return;
			}
			else
			{
				AudioManager.Get().Play("GenericDoorOpen");
				GameManager.Get().LoadLevel(Level.BIKINIBOTTOM);
			}
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000742F File Offset: 0x0000562F
	public void SwapText()
	{
		this.openInteractText.SetActive(false);
		this.kickDownInteractText.SetActive(true);
	}

	// Token: 0x0400014F RID: 335
	public GameObject openInteractText;

	// Token: 0x04000150 RID: 336
	public GameObject kickDownInteractText;

	// Token: 0x04000151 RID: 337
	public bool outdoor;

	// Token: 0x04000152 RID: 338
	private bool canUse;

	// Token: 0x04000153 RID: 339
	private bool used;
}

using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class Patrick : MonoBehaviour
{
	// Token: 0x060000DC RID: 220 RVA: 0x000058BC File Offset: 0x00003ABC
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x000058E8 File Offset: 0x00003AE8
	private void Start()
	{
		this.SetRigidBodyState(true);
		if (ObjectiveManager.currentObjective > 4 && ObjectiveManager.currentObjective < 13)
		{
			Object.Destroy(base.gameObject);
		}
		if (ObjectiveManager.currentObjective == 2 || ObjectiveManager.currentObjective == 3)
		{
			this.normalTextTrigger.SetActive(false);
			this.objectiveTextTrigger.SetActive(true);
			return;
		}
		if (ObjectiveManager.currentObjective == 4)
		{
			this.normalTextTrigger.SetActive(false);
			this.neckSnapTrigger.SetActive(true);
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00005964 File Offset: 0x00003B64
	private void Update()
	{
		if (!this.objectiveTalked && Input.GetKeyDown(KeyCode.E) && this.patrickTalkCheck && ObjectiveManager.currentObjective == 3)
		{
			this.objectiveTextTrigger.SetActive(false);
			this.neckSnapTrigger.SetActive(true);
			Debug.Log("NECK SNAP ACTIVE");
			ObjectiveManager.Get().CompleteObjective();
			this.objectiveTalked = true;
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x000059C8 File Offset: 0x00003BC8
	public void Shot()
	{
		if (ObjectiveManager.currentObjective == 3 || ObjectiveManager.currentObjective == 4 || this.dead)
		{
			return;
		}
		this.dead = true;
		this.SetRigidBodyState(false);
		this.deathSound.Play();
		this.lootTrigger.SetActive(true);
		this.normalTextTrigger.SetActive(false);
		this.objectiveTextTrigger.SetActive(false);
		this.objectiveTextTrigger.SetActive(false);
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x040000F0 RID: 240
	public GameObject rootBone;

	// Token: 0x040000F1 RID: 241
	public GameObject normalTextTrigger;

	// Token: 0x040000F2 RID: 242
	public GameObject objectiveTextTrigger;

	// Token: 0x040000F3 RID: 243
	public GameObject neckSnapTrigger;

	// Token: 0x040000F4 RID: 244
	public GameObject lootTrigger;

	// Token: 0x040000F5 RID: 245
	public AudioSource deathSound;

	// Token: 0x040000F6 RID: 246
	public bool patrickTalkCheck;

	// Token: 0x040000F7 RID: 247
	private bool objectiveTalked;

	// Token: 0x040000F8 RID: 248
	private bool dead;
}

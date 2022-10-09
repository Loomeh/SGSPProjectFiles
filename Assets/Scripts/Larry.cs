using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class Larry : MonoBehaviour
{
	// Token: 0x060000B4 RID: 180 RVA: 0x00004CF8 File Offset: 0x00002EF8
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00004D23 File Offset: 0x00002F23
	private void Start()
	{
		this.SetRigidBodyState(true);
		if (ObjectiveManager.currentObjective < 14 && ObjectiveManager.currentObjective > 6)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000255D File Offset: 0x0000075D
	private void Update()
	{
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00004D48 File Offset: 0x00002F48
	public void Shot()
	{
		if (ObjectiveManager.currentObjective == 5)
		{
			ObjectiveManager.Get().CompleteObjective();
		}
		Thug[] array = this.thugs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Attack();
		}
		this.SetRigidBodyState(false);
		this.dead = true;
		this.deathSound.Play();
		this.lootTrigger.SetActive(true);
		this.textTrigger.SetActive(false);
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x040000BD RID: 189
	public GameObject rootBone;

	// Token: 0x040000BE RID: 190
	public GameObject textTrigger;

	// Token: 0x040000BF RID: 191
	public GameObject lootTrigger;

	// Token: 0x040000C0 RID: 192
	public AudioSource deathSound;

	// Token: 0x040000C1 RID: 193
	public Thug[] thugs;

	// Token: 0x040000C2 RID: 194
	private bool dead;
}

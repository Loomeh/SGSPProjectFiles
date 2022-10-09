using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class Thug : MonoBehaviour
{
	// Token: 0x06000146 RID: 326 RVA: 0x00007664 File Offset: 0x00005864
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000768F File Offset: 0x0000588F
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
		Thug.thugsKilled = 0;
		this.SetRigidBodyState(true);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000076B0 File Offset: 0x000058B0
	private void Update()
	{
		if (this.attack && PlayerMovement.health > 0f && !this.dead)
		{
			base.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(this.player.transform.position - base.transform.position), 5f * Time.deltaTime).eulerAngles.y, base.transform.rotation.eulerAngles.z);
			this.shootTimer -= Time.deltaTime;
			if (this.shootTimer <= 0f)
			{
				PlayerMovement.UpdateHealth(-10f);
				this.shootSoundEffect.Play();
				this.shootTimer = 0.89999f;
			}
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000077B0 File Offset: 0x000059B0
	public void Shot()
	{
		if (this.dead || !this.attack)
		{
			return;
		}
		this.dead = true;
		this.SetRigidBodyState(false);
		this.deathSound.Play();
		this.lootTrigger.SetActive(true);
		base.GetComponent<Animator>().enabled = false;
		Thug.thugsKilled++;
		if (Thug.thugsKilled == 3 && ObjectiveManager.currentObjective == 6)
		{
			ObjectiveManager.Get().CompleteObjective();
			SaveManager.Save();
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000782A File Offset: 0x00005A2A
	public void Attack()
	{
		this.attack = true;
		base.GetComponent<Animator>().SetTrigger("shoot");
	}

	// Token: 0x0400015F RID: 351
	public static int thugsKilled;

	// Token: 0x04000160 RID: 352
	public GameObject rootBone;

	// Token: 0x04000161 RID: 353
	public GameObject lootTrigger;

	// Token: 0x04000162 RID: 354
	public AudioSource deathSound;

	// Token: 0x04000163 RID: 355
	public AudioSource shootSoundEffect;

	// Token: 0x04000164 RID: 356
	private GameObject player;

	// Token: 0x04000165 RID: 357
	private bool attack;

	// Token: 0x04000166 RID: 358
	private bool dead;

	// Token: 0x04000167 RID: 359
	private float shootTimer;
}

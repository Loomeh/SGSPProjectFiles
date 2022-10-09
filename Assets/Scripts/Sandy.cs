using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class Sandy : MonoBehaviour
{
	// Token: 0x06000110 RID: 272 RVA: 0x00006D8C File Offset: 0x00004F8C
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00006DB7 File Offset: 0x00004FB7
	private void Start()
	{
		this.SetRigidBodyState(true);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000255D File Offset: 0x0000075D
	private void Update()
	{
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00006DC0 File Offset: 0x00004FC0
	public void Shot()
	{
		if (this.dead)
		{
			return;
		}
		if (ObjectiveManager.currentObjective == 9)
		{
			base.StartCoroutine(this.FinishObjective());
		}
		this.SetRigidBodyState(false);
		this.dead = true;
		this.deathSound.Play();
		this.lootTrigger.SetActive(true);
		this.textTrigger.SetActive(false);
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00006E29 File Offset: 0x00005029
	private IEnumerator FinishObjective()
	{
		yield return new WaitForSeconds(3f);
		TextManager.Get().Talk("Incoming shellphone call from Mr. Krabs.;Ahoy laddy,;I've got reports of some narcs downtown that need to be dealt with ASAP.;While you're down there, I need you to get some funds out of me bank account.;Forcefully...;The banks been holding them for 'validation'.;Get to it!;Nothing stands between me and me money.");
		while (TextManager.Get().talking)
		{
			yield return null;
		}
		ObjectiveManager.Get().CompleteObjective();
		SaveManager.Save();
		yield break;
	}

	// Token: 0x0400013A RID: 314
	public GameObject rootBone;

	// Token: 0x0400013B RID: 315
	public GameObject lootTrigger;

	// Token: 0x0400013C RID: 316
	public GameObject textTrigger;

	// Token: 0x0400013D RID: 317
	public AudioSource deathSound;

	// Token: 0x0400013E RID: 318
	private bool dead;
}

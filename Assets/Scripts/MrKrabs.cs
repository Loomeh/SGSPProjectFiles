using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class MrKrabs : MonoBehaviour
{
	// Token: 0x060000C1 RID: 193 RVA: 0x00004F58 File Offset: 0x00003158
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00004F83 File Offset: 0x00003183
	private void Start()
	{
		this.SetRigidBodyState(true);
		if (ObjectiveManager.currentObjective == 14)
		{
			this.interactRadius.SetActive(false);
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00004FA4 File Offset: 0x000031A4
	private void Update()
	{
		if (this.canTalk && Input.GetKeyDown(KeyCode.E) && !this.dead)
		{
			switch (ObjectiveManager.currentObjective)
			{
			case 0:
				base.StartCoroutine(this.MrKrabsFirstTalk());
				return;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
				TextManager.Get().Talk("Get to work boyo!;Or I'll keelhaul ya!");
				return;
			case 8:
				base.StartCoroutine(this.MrKrabsSecondTalk());
				return;
			case 9:
			case 10:
			case 11:
				TextManager.Get().Talk("Arghhh!;What did I tell ya laddy?;Time is money!;MY MONEY!");
				return;
			case 12:
				base.StartCoroutine(this.MrKrabsThirdTalk());
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000505D File Offset: 0x0000325D
	public void Shot()
	{
		if (this.dead)
		{
			return;
		}
		this.dead = true;
		this.SetRigidBodyState(false);
		this.deathSound.Play();
		this.lootTrigger.SetActive(true);
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005099 File Offset: 0x00003299
	private IEnumerator MrKrabsFirstTalk()
	{
		TextManager.Get().Talk("Argh, me boy.;You know what day it is...;Debt collecting day.;I've got a list of people I need you to \"have a chat with\".;Don't let me down, boyo.;Argh argh argh argh argh.;NOW GET TO WORK!");
		while (TextManager.Get().talking)
		{
			yield return null;
		}
		ObjectiveManager.Get().CompleteObjective();
		GameObject.FindGameObjectWithTag("SquidwardWarp").GetComponent<SquidwardsDoor>().SwapText();
		SaveManager.Save();
		yield break;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000050A1 File Offset: 0x000032A1
	private IEnumerator MrKrabsSecondTalk()
	{
		TextManager.Get().Talk("Shut the door laddy.;That squirrel friend of yours...;She's stealing me business!;Thieving bilge rat.;Execute her, or I'll keelhaul you too!");
		while (TextManager.Get().talking)
		{
			yield return null;
		}
		ObjectiveManager.Get().CompleteObjective();
		SaveManager.Save();
		yield break;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000050A9 File Offset: 0x000032A9
	private IEnumerator MrKrabsThirdTalk()
	{
		TextManager.Get().Talk("The sweet smell of money...;What?;Hand it over Sponge Boy Me Bob!;...;Arghhhh...;So be it.;I'll take you out too.;Me own employee.;In me true form...");
		while (TextManager.Get().talking)
		{
			yield return null;
		}
		ObjectiveManager.currentObjective++;
		GameManager.Get().LoadLevel(Level.BOSSFIGHT);
		yield break;
	}

	// Token: 0x040000CA RID: 202
	public GameObject interactRadius;

	// Token: 0x040000CB RID: 203
	public GameObject rootBone;

	// Token: 0x040000CC RID: 204
	public GameObject lootTrigger;

	// Token: 0x040000CD RID: 205
	public AudioSource deathSound;

	// Token: 0x040000CE RID: 206
	private bool dead;

	// Token: 0x040000CF RID: 207
	[HideInInspector]
	public bool canTalk;
}

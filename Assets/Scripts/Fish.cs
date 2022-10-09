using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200002D RID: 45
public class Fish : MonoBehaviour
{
	// Token: 0x06000092 RID: 146 RVA: 0x0000437C File Offset: 0x0000257C
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000255D File Offset: 0x0000075D
	public void SetColliderState(bool state)
	{
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000043A7 File Offset: 0x000025A7
	private void Start()
	{
		this.SetRigidBodyState(true);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000043B0 File Offset: 0x000025B0
	private void Update()
	{
		if (this.wander)
		{
			if (this.wandering)
			{
				this.walkTimer -= Time.deltaTime;
				if (this.agent.remainingDistance <= 1f || this.walkTimer <= 0f)
				{
					this.wandering = false;
					return;
				}
			}
			else
			{
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(new Vector3(Random.Range(this.territory.GetComponent<BoxCollider>().bounds.min.x, this.territory.GetComponent<BoxCollider>().bounds.max.x), Random.Range(this.territory.GetComponent<BoxCollider>().bounds.min.y, this.territory.GetComponent<BoxCollider>().bounds.max.y), Random.Range(this.territory.GetComponent<BoxCollider>().bounds.min.z, this.territory.GetComponent<BoxCollider>().bounds.max.z)), out navMeshHit, 10000f, -1);
				this.wanderPos = navMeshHit.position;
				this.agent.SetDestination(this.wanderPos);
				this.walkTimer = 45f;
				this.wandering = true;
			}
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004510 File Offset: 0x00002710
	public void Shot()
	{
		if (!this.dead)
		{
			if (this.objectiveType == ObjectiveType.SHADYSHOALS && ObjectiveManager.currentObjective == 7)
			{
				GameManager.Get().shadyShoalsKills++;
				if (GameManager.Get().shadyShoalsKills == 4)
				{
					ObjectiveManager.Get().CompleteObjective();
					SaveManager.Save();
				}
				else
				{
					HUDManager.Get().objectiveText.gameObject.SetActive(false);
					if (4 - GameManager.Get().shadyShoalsKills == 1)
					{
						HUDManager.Get().objectiveText.text = "Kill 1 more pedestrian";
					}
					else
					{
						HUDManager.Get().objectiveText.text = "Kill " + (4 - GameManager.Get().shadyShoalsKills) + " more pedestrians";
					}
					HUDManager.Get().objectiveText.gameObject.SetActive(true);
				}
			}
			else if (this.objectiveType == ObjectiveType.DOWNTOWN && ObjectiveManager.currentObjective == 10)
			{
				GameManager.Get().downtownKills++;
				if (GameManager.Get().downtownKills == 10)
				{
					ObjectiveManager.Get().CompleteObjective();
					SaveManager.Save();
				}
				else
				{
					HUDManager.Get().objectiveText.gameObject.SetActive(false);
					if (10 - GameManager.Get().downtownKills == 1)
					{
						HUDManager.Get().objectiveText.text = "Kill 1 more citizen";
					}
					else
					{
						HUDManager.Get().objectiveText.text = "Kill " + (10 - GameManager.Get().downtownKills) + " more citizens";
					}
					HUDManager.Get().objectiveText.gameObject.SetActive(true);
				}
			}
			else if (this.objectiveType == ObjectiveType.BANK && ObjectiveManager.currentObjective == 11)
			{
				GameManager.Get().bankKills++;
				if (GameManager.Get().bankKills == 5)
				{
					ObjectiveManager.Get().CompleteObjective();
					SaveManager.Save();
				}
				else
				{
					HUDManager.Get().objectiveText.gameObject.SetActive(false);
					if (5 - GameManager.Get().bankKills == 1)
					{
						HUDManager.Get().objectiveText.text = "Kill 1 more citizen";
					}
					else
					{
						HUDManager.Get().objectiveText.text = "Kill " + (5 - GameManager.Get().bankKills) + " more citizens";
					}
					HUDManager.Get().objectiveText.gameObject.SetActive(true);
				}
			}
			this.SetRigidBodyState(false);
			if (this.agent != null)
			{
				this.agent.isStopped = true;
			}
			if (this.doTheSponge != null)
			{
				this.doTheSponge.Play();
			}
			if (this.man)
			{
				if (this.leg)
				{
					this.myLegSound.Play();
				}
				else
				{
					this.maleDeathSounds[Random.Range(0, this.maleDeathSounds.Length)].Play();
				}
			}
			else
			{
				this.femaleDeathSounds[Random.Range(0, this.femaleDeathSounds.Length)].Play();
			}
			base.GetComponent<Animator>().enabled = false;
			if (this.textTrigger != null)
			{
				this.textTrigger.SetActive(false);
			}
			this.lootTrigger.SetActive(true);
			this.dead = true;
		}
	}

	// Token: 0x04000089 RID: 137
	public GameObject rootBone;

	// Token: 0x0400008A RID: 138
	public GameObject textTrigger;

	// Token: 0x0400008B RID: 139
	public GameObject lootTrigger;

	// Token: 0x0400008C RID: 140
	private bool dead;

	// Token: 0x0400008D RID: 141
	public ObjectiveType objectiveType;

	// Token: 0x0400008E RID: 142
	[HideInInspector]
	public bool leg;

	// Token: 0x0400008F RID: 143
	public bool man;

	// Token: 0x04000090 RID: 144
	public NavMeshAgent agent;

	// Token: 0x04000091 RID: 145
	public bool wander;

	// Token: 0x04000092 RID: 146
	public GameObject territory;

	// Token: 0x04000093 RID: 147
	public bool wandering;

	// Token: 0x04000094 RID: 148
	public Vector3 wanderPos;

	// Token: 0x04000095 RID: 149
	private float walkTimer;

	// Token: 0x04000096 RID: 150
	public AudioSource[] maleDeathSounds;

	// Token: 0x04000097 RID: 151
	public AudioSource[] femaleDeathSounds;

	// Token: 0x04000098 RID: 152
	public AudioSource myLegSound;

	// Token: 0x04000099 RID: 153
	public AudioSource doTheSponge;
}

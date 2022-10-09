using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class JellyFish : MonoBehaviour
{
	// Token: 0x060000A6 RID: 166 RVA: 0x00004A68 File Offset: 0x00002C68
	public void SetRigidBodyState(bool state)
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = state;
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00004A93 File Offset: 0x00002C93
	private void Start()
	{
		this.SetRigidBodyState(true);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00004A9C File Offset: 0x00002C9C
	private void Update()
	{
		if (!this.dead)
		{
			if (this.wandering)
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, this.wanderPos, 1f * Time.deltaTime);
				if (base.transform.position == this.wanderPos)
				{
					this.wandering = false;
					return;
				}
			}
			else
			{
				this.wanderPos = new Vector3(Random.Range(this.jellyFishArea.GetComponent<BoxCollider>().bounds.min.x, this.jellyFishArea.GetComponent<BoxCollider>().bounds.max.x), Random.Range(this.jellyFishArea.GetComponent<BoxCollider>().bounds.min.y, this.jellyFishArea.GetComponent<BoxCollider>().bounds.max.y), Random.Range(this.jellyFishArea.GetComponent<BoxCollider>().bounds.min.z, this.jellyFishArea.GetComponent<BoxCollider>().bounds.max.z));
				this.wandering = true;
			}
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00004BDA File Offset: 0x00002DDA
	public void Shot()
	{
		if (this.dead)
		{
			return;
		}
		base.GetComponent<Animator>().enabled = false;
		this.SetRigidBodyState(false);
		this.lootTrigger.SetActive(true);
		this.dead = true;
	}

	// Token: 0x040000B4 RID: 180
	public GameObject jellyFishArea;

	// Token: 0x040000B5 RID: 181
	public GameObject rootBone;

	// Token: 0x040000B6 RID: 182
	public GameObject lootTrigger;

	// Token: 0x040000B7 RID: 183
	private bool dead;

	// Token: 0x040000B8 RID: 184
	private bool wandering;

	// Token: 0x040000B9 RID: 185
	private Vector3 wanderPos;
}

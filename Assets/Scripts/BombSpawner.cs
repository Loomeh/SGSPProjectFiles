using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class BombSpawner : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x00003C9B File Offset: 0x00001E9B
	private void Start()
	{
		this.explosion.Stop();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003CA8 File Offset: 0x00001EA8
	private void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer <= 0f)
		{
			Object.Instantiate<GameObject>(this.bombPrefab, this.bombSpawnTransform.position, this.bombSpawnTransform.rotation).GetComponent<Bomb>().explosion = this.explosion;
			this.timer = 30f;
		}
	}

	// Token: 0x0400005E RID: 94
	public GameObject bombPrefab;

	// Token: 0x0400005F RID: 95
	public Transform bombSpawnTransform;

	// Token: 0x04000060 RID: 96
	public ParticleSystem explosion;

	// Token: 0x04000061 RID: 97
	private float timer;
}

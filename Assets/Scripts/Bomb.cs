using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class Bomb : MonoBehaviour
{
	// Token: 0x06000060 RID: 96 RVA: 0x00003C18 File Offset: 0x00001E18
	private void Update()
	{
		if (base.transform.localScale.x != 3608.794f)
		{
			base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, new Vector3(3608.794f, 3608.794f, 3608.794f), 6000f * Time.deltaTime);
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003C76 File Offset: 0x00001E76
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("MrKrabsTrueForm"))
		{
			this.explosion.Play();
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400005D RID: 93
	public ParticleSystem explosion;
}

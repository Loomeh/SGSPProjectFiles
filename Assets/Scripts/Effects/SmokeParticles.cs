using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000075 RID: 117
	public class SmokeParticles : MonoBehaviour
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00009B99 File Offset: 0x00007D99
		private void Start()
		{
			base.GetComponent<AudioSource>().clip = this.extinguishSounds[Random.Range(0, this.extinguishSounds.Length)];
			base.GetComponent<AudioSource>().Play();
		}

		// Token: 0x0400020E RID: 526
		public AudioClip[] extinguishSounds;
	}
}

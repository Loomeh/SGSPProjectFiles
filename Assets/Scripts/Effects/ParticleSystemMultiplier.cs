using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000074 RID: 116
	public class ParticleSystemMultiplier : MonoBehaviour
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00009B04 File Offset: 0x00007D04
		private void Start()
		{
			foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.startSizeMultiplier *= this.multiplier;
				main.startSpeedMultiplier *= this.multiplier;
				main.startLifetimeMultiplier *= Mathf.Lerp(this.multiplier, 1f, 0.5f);
				particleSystem.Clear();
				particleSystem.Play();
			}
		}

		// Token: 0x0400020D RID: 525
		public float multiplier = 1f;
	}
}

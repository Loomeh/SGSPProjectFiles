using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000071 RID: 113
	public class ExtinguishableParticleSystem : MonoBehaviour
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00009857 File Offset: 0x00007A57
		private void Start()
		{
			this.m_Systems = base.GetComponentsInChildren<ParticleSystem>();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009868 File Offset: 0x00007A68
		public void Extinguish()
		{
			ParticleSystem[] systems = this.m_Systems;
			for (int i = 0; i < systems.Length; i++)
			{
				systems[i].emission.enabled = false;
			}
		}

		// Token: 0x04000202 RID: 514
		public float multiplier = 1f;

		// Token: 0x04000203 RID: 515
		private ParticleSystem[] m_Systems;
	}
}

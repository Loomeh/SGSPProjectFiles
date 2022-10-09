using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000073 RID: 115
	public class Hose : MonoBehaviour
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00009A24 File Offset: 0x00007C24
		private void Update()
		{
			this.m_Power = Mathf.Lerp(this.m_Power, Input.GetMouseButton(0) ? this.maxPower : this.minPower, Time.deltaTime * this.changeSpeed);
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.systemRenderer.enabled = !this.systemRenderer.enabled;
			}
			foreach (ParticleSystem particleSystem in this.hoseWaterSystems)
			{
				particleSystem.main.startSpeed = this.m_Power;
				particleSystem.emission.enabled = (this.m_Power > this.minPower * 1.1f);
			}
		}

		// Token: 0x04000207 RID: 519
		public float maxPower = 20f;

		// Token: 0x04000208 RID: 520
		public float minPower = 5f;

		// Token: 0x04000209 RID: 521
		public float changeSpeed = 5f;

		// Token: 0x0400020A RID: 522
		public ParticleSystem[] hoseWaterSystems;

		// Token: 0x0400020B RID: 523
		public Renderer systemRenderer;

		// Token: 0x0400020C RID: 524
		private float m_Power;
	}
}

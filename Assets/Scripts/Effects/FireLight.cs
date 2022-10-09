using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000072 RID: 114
	public class FireLight : MonoBehaviour
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000098AE File Offset: 0x00007AAE
		private void Start()
		{
			this.m_Rnd = Random.value * 100f;
			this.m_Light = base.GetComponent<Light>();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000098D0 File Offset: 0x00007AD0
		private void Update()
		{
			if (this.m_Burning)
			{
				this.m_Light.intensity = 2f * Mathf.PerlinNoise(this.m_Rnd + Time.time, this.m_Rnd + 1f + Time.time * 1f);
				float x = Mathf.PerlinNoise(this.m_Rnd + 0f + Time.time * 2f, this.m_Rnd + 1f + Time.time * 2f) - 0.5f;
				float y = Mathf.PerlinNoise(this.m_Rnd + 2f + Time.time * 2f, this.m_Rnd + 3f + Time.time * 2f) - 0.5f;
				float z = Mathf.PerlinNoise(this.m_Rnd + 4f + Time.time * 2f, this.m_Rnd + 5f + Time.time * 2f) - 0.5f;
				base.transform.localPosition = Vector3.up + new Vector3(x, y, z) * 1f;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000099FD File Offset: 0x00007BFD
		public void Extinguish()
		{
			this.m_Burning = false;
			this.m_Light.enabled = false;
		}

		// Token: 0x04000204 RID: 516
		private float m_Rnd;

		// Token: 0x04000205 RID: 517
		private bool m_Burning = true;

		// Token: 0x04000206 RID: 518
		private Light m_Light;
	}
}

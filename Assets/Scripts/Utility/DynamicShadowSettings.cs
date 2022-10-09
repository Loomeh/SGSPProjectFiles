using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005E RID: 94
	public class DynamicShadowSettings : MonoBehaviour
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000806F File Offset: 0x0000626F
		private void Start()
		{
			this.m_OriginalStrength = this.sunLight.shadowStrength;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008084 File Offset: 0x00006284
		private void Update()
		{
			Ray ray = new Ray(Camera.main.transform.position, -Vector3.up);
			float num = base.transform.position.y;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				num = raycastHit.distance;
			}
			if (Mathf.Abs(num - this.m_SmoothHeight) > 1f)
			{
				this.m_SmoothHeight = Mathf.SmoothDamp(this.m_SmoothHeight, num, ref this.m_ChangeSpeed, this.adaptTime);
			}
			float num2 = Mathf.InverseLerp(this.minHeight, this.maxHeight, this.m_SmoothHeight);
			QualitySettings.shadowDistance = Mathf.Lerp(this.minShadowDistance, this.maxShadowDistance, num2);
			this.sunLight.shadowBias = Mathf.Lerp(this.minShadowBias, this.maxShadowBias, 1f - (1f - num2) * (1f - num2));
			this.sunLight.shadowStrength = Mathf.Lerp(this.m_OriginalStrength, 0f, num2);
		}

		// Token: 0x04000194 RID: 404
		public Light sunLight;

		// Token: 0x04000195 RID: 405
		public float minHeight = 10f;

		// Token: 0x04000196 RID: 406
		public float minShadowDistance = 80f;

		// Token: 0x04000197 RID: 407
		public float minShadowBias = 1f;

		// Token: 0x04000198 RID: 408
		public float maxHeight = 1000f;

		// Token: 0x04000199 RID: 409
		public float maxShadowDistance = 10000f;

		// Token: 0x0400019A RID: 410
		public float maxShadowBias = 0.1f;

		// Token: 0x0400019B RID: 411
		public float adaptTime = 1f;

		// Token: 0x0400019C RID: 412
		private float m_SmoothHeight;

		// Token: 0x0400019D RID: 413
		private float m_ChangeSpeed;

		// Token: 0x0400019E RID: 414
		private float m_OriginalStrength = 1f;
	}
}

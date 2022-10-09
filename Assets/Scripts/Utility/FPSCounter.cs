using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000060 RID: 96
	[RequireComponent(typeof(Text))]
	public class FPSCounter : MonoBehaviour
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00008280 File Offset: 0x00006480
		private void Start()
		{
			this.m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
			this.m_Text = base.GetComponent<Text>();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000082A0 File Offset: 0x000064A0
		private void Update()
		{
			this.m_FpsAccumulator++;
			if (Time.realtimeSinceStartup > this.m_FpsNextPeriod)
			{
				this.m_CurrentFps = (int)((float)this.m_FpsAccumulator / 0.5f);
				this.m_FpsAccumulator = 0;
				this.m_FpsNextPeriod += 0.5f;
				this.m_Text.text = string.Format("{0} FPS", this.m_CurrentFps);
			}
		}

		// Token: 0x040001A5 RID: 421
		private const float fpsMeasurePeriod = 0.5f;

		// Token: 0x040001A6 RID: 422
		private int m_FpsAccumulator;

		// Token: 0x040001A7 RID: 423
		private float m_FpsNextPeriod;

		// Token: 0x040001A8 RID: 424
		private int m_CurrentFps;

		// Token: 0x040001A9 RID: 425
		private const string display = "{0} FPS";

		// Token: 0x040001AA RID: 426
		private Text m_Text;
	}
}

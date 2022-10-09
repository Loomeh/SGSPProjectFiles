using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005A RID: 90
	public class AutoMoveAndRotate : MonoBehaviour
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00007BF3 File Offset: 0x00005DF3
		private void Start()
		{
			this.m_LastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007C00 File Offset: 0x00005E00
		private void Update()
		{
			float d = Time.deltaTime;
			if (this.ignoreTimescale)
			{
				d = Time.realtimeSinceStartup - this.m_LastRealTime;
				this.m_LastRealTime = Time.realtimeSinceStartup;
			}
			base.transform.Translate(this.moveUnitsPerSecond.value * d, this.moveUnitsPerSecond.space);
			base.transform.Rotate(this.rotateDegreesPerSecond.value * d, this.moveUnitsPerSecond.space);
		}

		// Token: 0x0400017B RID: 379
		public AutoMoveAndRotate.Vector3andSpace moveUnitsPerSecond;

		// Token: 0x0400017C RID: 380
		public AutoMoveAndRotate.Vector3andSpace rotateDegreesPerSecond;

		// Token: 0x0400017D RID: 381
		public bool ignoreTimescale;

		// Token: 0x0400017E RID: 382
		private float m_LastRealTime;

		// Token: 0x020000CB RID: 203
		[Serializable]
		public class Vector3andSpace
		{
			// Token: 0x04000432 RID: 1074
			public Vector3 value;

			// Token: 0x04000433 RID: 1075
			public Space space = Space.Self;
		}
	}
}

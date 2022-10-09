using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class LerpControlledBob
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000835A File Offset: 0x0000655A
		public float Offset()
		{
			return this.m_Offset;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008362 File Offset: 0x00006562
		public IEnumerator DoBobCycle()
		{
			float t = 0f;
			while (t < this.BobDuration)
			{
				this.m_Offset = Mathf.Lerp(0f, this.BobAmount, t / this.BobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			t = 0f;
			while (t < this.BobDuration)
			{
				this.m_Offset = Mathf.Lerp(this.BobAmount, 0f, t / this.BobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			this.m_Offset = 0f;
			yield break;
		}

		// Token: 0x040001AD RID: 429
		public float BobDuration;

		// Token: 0x040001AE RID: 430
		public float BobAmount;

		// Token: 0x040001AF RID: 431
		private float m_Offset;
	}
}

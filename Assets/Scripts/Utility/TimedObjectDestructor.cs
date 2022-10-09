using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200006A RID: 106
	public class TimedObjectDestructor : MonoBehaviour
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00008AF3 File Offset: 0x00006CF3
		private void Awake()
		{
			base.Invoke("DestroyNow", this.m_TimeOut);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008B06 File Offset: 0x00006D06
		private void DestroyNow()
		{
			if (this.m_DetachChildren)
			{
				base.transform.DetachChildren();
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x040001CF RID: 463
		[SerializeField]
		private float m_TimeOut = 1f;

		// Token: 0x040001D0 RID: 464
		[SerializeField]
		private bool m_DetachChildren;
	}
}

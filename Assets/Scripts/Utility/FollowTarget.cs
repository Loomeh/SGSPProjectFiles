using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000061 RID: 97
	public class FollowTarget : MonoBehaviour
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00008315 File Offset: 0x00006515
		private void LateUpdate()
		{
			base.transform.position = this.target.position + this.offset;
		}

		// Token: 0x040001AB RID: 427
		public Transform target;

		// Token: 0x040001AC RID: 428
		public Vector3 offset = new Vector3(0f, 7.5f, 0f);
	}
}

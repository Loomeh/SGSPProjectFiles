using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A7 RID: 167
	public abstract class PivotBasedCameraRig : AbstractTargetFollower
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0001064B File Offset: 0x0000E84B
		protected virtual void Awake()
		{
			this.m_Cam = base.GetComponentInChildren<Camera>().transform;
			this.m_Pivot = this.m_Cam.parent;
		}

		// Token: 0x0400039A RID: 922
		protected Transform m_Cam;

		// Token: 0x0400039B RID: 923
		protected Transform m_Pivot;

		// Token: 0x0400039C RID: 924
		protected Vector3 m_LastTargetPosition;
	}
}

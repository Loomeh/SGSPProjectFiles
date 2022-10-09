using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005B RID: 91
	public class CameraRefocus
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00007C81 File Offset: 0x00005E81
		public CameraRefocus(Camera camera, Transform parent, Vector3 origCameraPos)
		{
			this.m_OrigCameraPos = origCameraPos;
			this.Camera = camera;
			this.Parent = parent;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007C9E File Offset: 0x00005E9E
		public void ChangeCamera(Camera camera)
		{
			this.Camera = camera;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007CA7 File Offset: 0x00005EA7
		public void ChangeParent(Transform parent)
		{
			this.Parent = parent;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public void GetFocusPoint()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.Parent.transform.position + this.m_OrigCameraPos, this.Parent.transform.forward, out raycastHit, 100f))
			{
				this.Lookatpoint = raycastHit.point;
				this.m_Refocus = true;
				return;
			}
			this.m_Refocus = false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007D12 File Offset: 0x00005F12
		public void SetFocusPoint()
		{
			if (this.m_Refocus)
			{
				this.Camera.transform.LookAt(this.Lookatpoint);
			}
		}

		// Token: 0x0400017F RID: 383
		public Camera Camera;

		// Token: 0x04000180 RID: 384
		public Vector3 Lookatpoint;

		// Token: 0x04000181 RID: 385
		public Transform Parent;

		// Token: 0x04000182 RID: 386
		private Vector3 m_OrigCameraPos;

		// Token: 0x04000183 RID: 387
		private bool m_Refocus;
	}
}

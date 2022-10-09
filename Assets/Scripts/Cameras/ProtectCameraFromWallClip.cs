using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A8 RID: 168
	public class ProtectCameraFromWallClip : MonoBehaviour
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00010677 File Offset: 0x0000E877
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0001067F File Offset: 0x0000E87F
		public bool protecting { get; private set; }

		// Token: 0x0600033C RID: 828 RVA: 0x00010688 File Offset: 0x0000E888
		private void Start()
		{
			this.m_Cam = base.GetComponentInChildren<Camera>().transform;
			this.m_Pivot = this.m_Cam.parent;
			this.m_OriginalDist = this.m_Cam.localPosition.magnitude;
			this.m_CurrentDist = this.m_OriginalDist;
			this.m_RayHitComparer = new ProtectCameraFromWallClip.RayHitComparer();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000106E8 File Offset: 0x0000E8E8
		private void LateUpdate()
		{
			float num = this.m_OriginalDist;
			this.m_Ray.origin = this.m_Pivot.position + this.m_Pivot.forward * this.sphereCastRadius;
			this.m_Ray.direction = -this.m_Pivot.forward;
			Collider[] array = Physics.OverlapSphere(this.m_Ray.origin, this.sphereCastRadius);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].isTrigger && (!(array[i].attachedRigidbody != null) || !array[i].attachedRigidbody.CompareTag(this.dontClipTag)))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.m_Ray.origin = this.m_Ray.origin + this.m_Pivot.forward * this.sphereCastRadius;
				this.m_Hits = Physics.RaycastAll(this.m_Ray, this.m_OriginalDist - this.sphereCastRadius);
			}
			else
			{
				this.m_Hits = Physics.SphereCastAll(this.m_Ray, this.sphereCastRadius, this.m_OriginalDist + this.sphereCastRadius);
			}
			Array.Sort(this.m_Hits, this.m_RayHitComparer);
			float num2 = float.PositiveInfinity;
			for (int j = 0; j < this.m_Hits.Length; j++)
			{
				if (this.m_Hits[j].distance < num2 && !this.m_Hits[j].collider.isTrigger && (!(this.m_Hits[j].collider.attachedRigidbody != null) || !this.m_Hits[j].collider.attachedRigidbody.CompareTag(this.dontClipTag)))
				{
					num2 = this.m_Hits[j].distance;
					num = -this.m_Pivot.InverseTransformPoint(this.m_Hits[j].point).z;
					flag2 = true;
				}
			}
			if (flag2)
			{
				Debug.DrawRay(this.m_Ray.origin, -this.m_Pivot.forward * (num + this.sphereCastRadius), Color.red);
			}
			this.protecting = flag2;
			this.m_CurrentDist = Mathf.SmoothDamp(this.m_CurrentDist, num, ref this.m_MoveVelocity, (this.m_CurrentDist > num) ? this.clipMoveTime : this.returnTime);
			this.m_CurrentDist = Mathf.Clamp(this.m_CurrentDist, this.closestDistance, this.m_OriginalDist);
			this.m_Cam.localPosition = -Vector3.forward * this.m_CurrentDist;
		}

		// Token: 0x0400039D RID: 925
		public float clipMoveTime = 0.05f;

		// Token: 0x0400039E RID: 926
		public float returnTime = 0.4f;

		// Token: 0x0400039F RID: 927
		public float sphereCastRadius = 0.1f;

		// Token: 0x040003A0 RID: 928
		public bool visualiseInEditor;

		// Token: 0x040003A1 RID: 929
		public float closestDistance = 0.5f;

		// Token: 0x040003A3 RID: 931
		public string dontClipTag = "Player";

		// Token: 0x040003A4 RID: 932
		private Transform m_Cam;

		// Token: 0x040003A5 RID: 933
		private Transform m_Pivot;

		// Token: 0x040003A6 RID: 934
		private float m_OriginalDist;

		// Token: 0x040003A7 RID: 935
		private float m_MoveVelocity;

		// Token: 0x040003A8 RID: 936
		private float m_CurrentDist;

		// Token: 0x040003A9 RID: 937
		private Ray m_Ray;

		// Token: 0x040003AA RID: 938
		private RaycastHit[] m_Hits;

		// Token: 0x040003AB RID: 939
		private ProtectCameraFromWallClip.RayHitComparer m_RayHitComparer;

		// Token: 0x020000F2 RID: 242
		public class RayHitComparer : IComparer
		{
			// Token: 0x0600044E RID: 1102 RVA: 0x000138CC File Offset: 0x00011ACC
			public int Compare(object x, object y)
			{
				return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			}
		}
	}
}

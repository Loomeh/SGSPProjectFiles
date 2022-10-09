using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x020000AA RID: 170
	public class Camera2DFollow : MonoBehaviour
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00010B5C File Offset: 0x0000ED5C
		private void Start()
		{
			this.m_LastTargetPosition = this.target.position;
			this.m_OffsetZ = (base.transform.position - this.target.position).z;
			base.transform.parent = null;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010BAC File Offset: 0x0000EDAC
		private void Update()
		{
			float x = (this.target.position - this.m_LastTargetPosition).x;
			if (Mathf.Abs(x) > this.lookAheadMoveThreshold)
			{
				this.m_LookAheadPos = this.lookAheadFactor * Vector3.right * Mathf.Sign(x);
			}
			else
			{
				this.m_LookAheadPos = Vector3.MoveTowards(this.m_LookAheadPos, Vector3.zero, Time.deltaTime * this.lookAheadReturnSpeed);
			}
			Vector3 vector = this.target.position + this.m_LookAheadPos + Vector3.forward * this.m_OffsetZ;
			Vector3 position = Vector3.SmoothDamp(base.transform.position, vector, ref this.m_CurrentVelocity, this.damping);
			base.transform.position = position;
			this.m_LastTargetPosition = this.target.position;
		}

		// Token: 0x040003B3 RID: 947
		public Transform target;

		// Token: 0x040003B4 RID: 948
		public float damping = 1f;

		// Token: 0x040003B5 RID: 949
		public float lookAheadFactor = 3f;

		// Token: 0x040003B6 RID: 950
		public float lookAheadReturnSpeed = 0.5f;

		// Token: 0x040003B7 RID: 951
		public float lookAheadMoveThreshold = 0.1f;

		// Token: 0x040003B8 RID: 952
		private float m_OffsetZ;

		// Token: 0x040003B9 RID: 953
		private Vector3 m_LastTargetPosition;

		// Token: 0x040003BA RID: 954
		private Vector3 m_CurrentVelocity;

		// Token: 0x040003BB RID: 955
		private Vector3 m_LookAheadPos;
	}
}

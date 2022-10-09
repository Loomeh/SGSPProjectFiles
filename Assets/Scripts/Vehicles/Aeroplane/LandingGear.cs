using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000098 RID: 152
	public class LandingGear : MonoBehaviour
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000E108 File Offset: 0x0000C308
		private void Start()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_Animator = base.GetComponent<Animator>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E130 File Offset: 0x0000C330
		private void Update()
		{
			if (this.m_State == LandingGear.GearState.Lowered && this.m_Plane.Altitude > this.raiseAtAltitude && this.m_Rigidbody.velocity.y > 0f)
			{
				this.m_State = LandingGear.GearState.Raised;
			}
			if (this.m_State == LandingGear.GearState.Raised && this.m_Plane.Altitude < this.lowerAtAltitude && this.m_Rigidbody.velocity.y < 0f)
			{
				this.m_State = LandingGear.GearState.Lowered;
			}
			this.m_Animator.SetInteger("GearState", (int)this.m_State);
		}

		// Token: 0x0400030A RID: 778
		public float raiseAtAltitude = 40f;

		// Token: 0x0400030B RID: 779
		public float lowerAtAltitude = 40f;

		// Token: 0x0400030C RID: 780
		private LandingGear.GearState m_State = LandingGear.GearState.Lowered;

		// Token: 0x0400030D RID: 781
		private Animator m_Animator;

		// Token: 0x0400030E RID: 782
		private Rigidbody m_Rigidbody;

		// Token: 0x0400030F RID: 783
		private AeroplaneController m_Plane;

		// Token: 0x020000EE RID: 238
		private enum GearState
		{
			// Token: 0x040004B2 RID: 1202
			Raised = -1,
			// Token: 0x040004B3 RID: 1203
			Lowered = 1
		}
	}
}

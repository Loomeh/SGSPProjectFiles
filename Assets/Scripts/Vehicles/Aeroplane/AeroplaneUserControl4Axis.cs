using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000096 RID: 150
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl4Axis : MonoBehaviour
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000DE98 File Offset: 0x0000C098
		private void Awake()
		{
			this.m_Aeroplane = base.GetComponent<AeroplaneController>();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Mouse X");
			float axis2 = CrossPlatformInputManager.GetAxis("Mouse Y");
			this.m_AirBrakes = CrossPlatformInputManager.GetButton("Fire1");
			this.m_Yaw = CrossPlatformInputManager.GetAxis("Horizontal");
			this.m_Throttle = CrossPlatformInputManager.GetAxis("Vertical");
			this.m_Aeroplane.Move(axis, axis2, this.m_Yaw, this.m_Throttle, this.m_AirBrakes);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000DF1C File Offset: 0x0000C11C
		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			float num = roll * this.maxRollAngle * 0.017453292f;
			float num2 = pitch * this.maxPitchAngle * 0.017453292f;
			roll = Mathf.Clamp(num - this.m_Aeroplane.RollAngle, -1f, 1f);
			pitch = Mathf.Clamp(num2 - this.m_Aeroplane.PitchAngle, -1f, 1f);
		}

		// Token: 0x040002FE RID: 766
		public float maxRollAngle = 80f;

		// Token: 0x040002FF RID: 767
		public float maxPitchAngle = 80f;

		// Token: 0x04000300 RID: 768
		private AeroplaneController m_Aeroplane;

		// Token: 0x04000301 RID: 769
		private float m_Throttle;

		// Token: 0x04000302 RID: 770
		private bool m_AirBrakes;

		// Token: 0x04000303 RID: 771
		private float m_Yaw;
	}
}

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000095 RID: 149
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl2Axis : MonoBehaviour
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000DD89 File Offset: 0x0000BF89
		private void Awake()
		{
			this.m_Aeroplane = base.GetComponent<AeroplaneController>();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool button = CrossPlatformInputManager.GetButton("Fire1");
			float throttleInput = (float)(button ? -1 : 1);
			this.m_Aeroplane.Move(axis, axis2, 0f, throttleInput, button);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			float num = roll * this.maxRollAngle * 0.017453292f;
			float num2 = pitch * this.maxPitchAngle * 0.017453292f;
			roll = Mathf.Clamp(num - this.m_Aeroplane.RollAngle, -1f, 1f);
			pitch = Mathf.Clamp(num2 - this.m_Aeroplane.PitchAngle, -1f, 1f);
			float num3 = throttle * 0.5f + 0.5f;
			throttle = Mathf.Clamp(num3 - this.m_Aeroplane.Throttle, -1f, 1f);
		}

		// Token: 0x040002FB RID: 763
		public float maxRollAngle = 80f;

		// Token: 0x040002FC RID: 764
		public float maxPitchAngle = 80f;

		// Token: 0x040002FD RID: 765
		private AeroplaneController m_Aeroplane;
	}
}

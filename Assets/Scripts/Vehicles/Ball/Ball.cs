using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
	// Token: 0x02000099 RID: 153
	public class Ball : MonoBehaviour
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			base.GetComponent<Rigidbody>().maxAngularVelocity = this.m_MaxAngularVelocity;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E20C File Offset: 0x0000C40C
		public void Move(Vector3 moveDirection, bool jump)
		{
			if (this.m_UseTorque)
			{
				this.m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0f, -moveDirection.x) * this.m_MovePower);
			}
			else
			{
				this.m_Rigidbody.AddForce(moveDirection * this.m_MovePower);
			}
			if (Physics.Raycast(base.transform.position, -Vector3.up, 1f) && jump)
			{
				this.m_Rigidbody.AddForce(Vector3.up * this.m_JumpPower, ForceMode.Impulse);
			}
		}

		// Token: 0x04000310 RID: 784
		[SerializeField]
		private float m_MovePower = 5f;

		// Token: 0x04000311 RID: 785
		[SerializeField]
		private bool m_UseTorque = true;

		// Token: 0x04000312 RID: 786
		[SerializeField]
		private float m_MaxAngularVelocity = 25f;

		// Token: 0x04000313 RID: 787
		[SerializeField]
		private float m_JumpPower = 2f;

		// Token: 0x04000314 RID: 788
		private const float k_GroundRayLength = 1f;

		// Token: 0x04000315 RID: 789
		private Rigidbody m_Rigidbody;
	}
}

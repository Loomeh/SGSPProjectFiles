using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008A RID: 138
	public class CarSelfRighting : MonoBehaviour
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000CA23 File Offset: 0x0000AC23
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000CA34 File Offset: 0x0000AC34
		private void Update()
		{
			if (base.transform.up.y > 0f || this.m_Rigidbody.velocity.magnitude > this.m_VelocityThreshold)
			{
				this.m_LastOkTime = Time.time;
			}
			if (Time.time > this.m_LastOkTime + this.m_WaitTime)
			{
				this.RightCar();
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CA98 File Offset: 0x0000AC98
		private void RightCar()
		{
			base.transform.position += Vector3.up;
			base.transform.rotation = Quaternion.LookRotation(base.transform.forward);
		}

		// Token: 0x040002A1 RID: 673
		[SerializeField]
		private float m_WaitTime = 3f;

		// Token: 0x040002A2 RID: 674
		[SerializeField]
		private float m_VelocityThreshold = 1f;

		// Token: 0x040002A3 RID: 675
		private float m_LastOkTime;

		// Token: 0x040002A4 RID: 676
		private Rigidbody m_Rigidbody;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008C RID: 140
	public class Mudguard : MonoBehaviour
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000CB39 File Offset: 0x0000AD39
		private void Start()
		{
			this.m_OriginalRotation = base.transform.localRotation;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		private void Update()
		{
			base.transform.localRotation = this.m_OriginalRotation * Quaternion.Euler(0f, this.carController.CurrentSteerAngle, 0f);
		}

		// Token: 0x040002A6 RID: 678
		public CarController carController;

		// Token: 0x040002A7 RID: 679
		private Quaternion m_OriginalRotation;
	}
}

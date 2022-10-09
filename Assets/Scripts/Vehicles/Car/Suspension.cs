using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008E RID: 142
	public class Suspension : MonoBehaviour
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		private void Start()
		{
			this.m_TargetOriginalPosition = this.wheel.transform.localPosition;
			this.m_Origin = base.transform.localPosition;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
		private void Update()
		{
			base.transform.localPosition = this.m_Origin + (this.wheel.transform.localPosition - this.m_TargetOriginalPosition);
		}

		// Token: 0x040002A9 RID: 681
		public GameObject wheel;

		// Token: 0x040002AA RID: 682
		private Vector3 m_TargetOriginalPosition;

		// Token: 0x040002AB RID: 683
		private Vector3 m_Origin;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000084 RID: 132
	public class BrakeLight : MonoBehaviour
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000B7B0 File Offset: 0x000099B0
		private void Start()
		{
			this.m_Renderer = base.GetComponent<Renderer>();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B7BE File Offset: 0x000099BE
		private void Update()
		{
			this.m_Renderer.enabled = (this.car.BrakeInput > 0f);
		}

		// Token: 0x04000250 RID: 592
		public CarController car;

		// Token: 0x04000251 RID: 593
		private Renderer m_Renderer;
	}
}

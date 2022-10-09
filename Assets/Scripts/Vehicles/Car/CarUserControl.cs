using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008B RID: 139
	[RequireComponent(typeof(CarController))]
	public class CarUserControl : MonoBehaviour
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000CAEE File Offset: 0x0000ACEE
		private void Awake()
		{
			this.m_Car = base.GetComponent<CarController>();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			float axis3 = CrossPlatformInputManager.GetAxis("Jump");
			this.m_Car.Move(axis, axis2, axis2, axis3);
		}

		// Token: 0x040002A5 RID: 677
		private CarController m_Car;
	}
}

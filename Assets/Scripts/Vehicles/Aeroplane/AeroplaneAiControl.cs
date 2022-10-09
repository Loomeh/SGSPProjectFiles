using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000090 RID: 144
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneAiControl : MonoBehaviour
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000CD73 File Offset: 0x0000AF73
		private void Awake()
		{
			this.m_AeroplaneController = base.GetComponent<AeroplaneController>();
			this.m_RandomPerlin = Random.Range(0f, 100f);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000CD96 File Offset: 0x0000AF96
		public void Reset()
		{
			this.m_TakenOff = false;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		private void FixedUpdate()
		{
			if (this.m_Target != null)
			{
				Vector3 position = this.m_Target.position + base.transform.right * (Mathf.PerlinNoise(Time.time * this.m_LateralWanderSpeed, this.m_RandomPerlin) * 2f - 1f) * this.m_LateralWanderDistance;
				Vector3 vector = base.transform.InverseTransformPoint(position);
				float num = Mathf.Atan2(vector.x, vector.z);
				float num2 = (Mathf.Clamp(-Mathf.Atan2(vector.y, vector.z), -this.m_MaxClimbAngle * 0.017453292f, this.m_MaxClimbAngle * 0.017453292f) - this.m_AeroplaneController.PitchAngle) * this.m_PitchSensitivity;
				float num3 = Mathf.Clamp(num, -this.m_MaxRollAngle * 0.017453292f, this.m_MaxRollAngle * 0.017453292f);
				float num4 = 0f;
				float num5 = 0f;
				if (!this.m_TakenOff)
				{
					if (this.m_AeroplaneController.Altitude > this.m_TakeoffHeight)
					{
						this.m_TakenOff = true;
					}
				}
				else
				{
					num4 = num;
					num5 = -(this.m_AeroplaneController.RollAngle - num3) * this.m_RollSensitivity;
				}
				float num6 = 1f + this.m_AeroplaneController.ForwardSpeed * this.m_SpeedEffect;
				num5 *= num6;
				num2 *= num6;
				num4 *= num6;
				this.m_AeroplaneController.Move(num5, num2, num4, 0.5f, false);
				return;
			}
			this.m_AeroplaneController.Move(0f, 0f, 0f, 0f, false);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CF42 File Offset: 0x0000B142
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
		}

		// Token: 0x040002B4 RID: 692
		[SerializeField]
		private float m_RollSensitivity = 0.2f;

		// Token: 0x040002B5 RID: 693
		[SerializeField]
		private float m_PitchSensitivity = 0.5f;

		// Token: 0x040002B6 RID: 694
		[SerializeField]
		private float m_LateralWanderDistance = 5f;

		// Token: 0x040002B7 RID: 695
		[SerializeField]
		private float m_LateralWanderSpeed = 0.11f;

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		private float m_MaxClimbAngle = 45f;

		// Token: 0x040002B9 RID: 697
		[SerializeField]
		private float m_MaxRollAngle = 45f;

		// Token: 0x040002BA RID: 698
		[SerializeField]
		private float m_SpeedEffect = 0.01f;

		// Token: 0x040002BB RID: 699
		[SerializeField]
		private float m_TakeoffHeight = 20f;

		// Token: 0x040002BC RID: 700
		[SerializeField]
		private Transform m_Target;

		// Token: 0x040002BD RID: 701
		private AeroplaneController m_AeroplaneController;

		// Token: 0x040002BE RID: 702
		private float m_RandomPerlin;

		// Token: 0x040002BF RID: 703
		private bool m_TakenOff;
	}
}

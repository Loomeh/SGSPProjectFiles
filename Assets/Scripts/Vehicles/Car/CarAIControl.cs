using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000085 RID: 133
	[RequireComponent(typeof(CarController))]
	public class CarAIControl : MonoBehaviour
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000B7DD File Offset: 0x000099DD
		private void Awake()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_RandomPerlin = Random.value * 100f;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B808 File Offset: 0x00009A08
		private void FixedUpdate()
		{
			if (this.m_Target == null || !this.m_Driving)
			{
				this.m_CarController.Move(0f, 0f, -1f, 1f);
				return;
			}
			Vector3 to = base.transform.forward;
			if (this.m_Rigidbody.velocity.magnitude > this.m_CarController.MaxSpeed * 0.1f)
			{
				to = this.m_Rigidbody.velocity;
			}
			float num = this.m_CarController.MaxSpeed;
			switch (this.m_BrakeCondition)
			{
			case CarAIControl.BrakeCondition.TargetDirectionDifference:
			{
				float b = Vector3.Angle(this.m_Target.forward, to);
				float a = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t = Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, Mathf.Max(a, b));
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t);
				break;
			}
			case CarAIControl.BrakeCondition.TargetDistance:
			{
				Vector3 vector = this.m_Target.position - base.transform.position;
				float b2 = Mathf.InverseLerp(this.m_CautiousMaxDistance, 0f, vector.magnitude);
				float value = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t2 = Mathf.Max(Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, value), b2);
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t2);
				break;
			}
			}
			Vector3 vector2 = this.m_Target.position;
			if (Time.time < this.m_AvoidOtherCarTime)
			{
				num *= this.m_AvoidOtherCarSlowdown;
				vector2 += this.m_Target.right * this.m_AvoidPathOffset;
			}
			else
			{
				vector2 += this.m_Target.right * (Mathf.PerlinNoise(Time.time * this.m_LateralWanderSpeed, this.m_RandomPerlin) * 2f - 1f) * this.m_LateralWanderDistance;
			}
			float num2 = (num < this.m_CarController.CurrentSpeed) ? this.m_BrakeSensitivity : this.m_AccelSensitivity;
			float num3 = Mathf.Clamp((num - this.m_CarController.CurrentSpeed) * num2, -1f, 1f);
			num3 *= 1f - this.m_AccelWanderAmount + Mathf.PerlinNoise(Time.time * this.m_AccelWanderSpeed, this.m_RandomPerlin) * this.m_AccelWanderAmount;
			Vector3 vector3 = base.transform.InverseTransformPoint(vector2);
			float steering = Mathf.Clamp(Mathf.Atan2(vector3.x, vector3.z) * 57.29578f * this.m_SteerSensitivity, -1f, 1f) * Mathf.Sign(this.m_CarController.CurrentSpeed);
			this.m_CarController.Move(steering, num3, num3, 0f);
			if (this.m_StopWhenTargetReached && vector3.magnitude < this.m_ReachTargetThreshold)
			{
				this.m_Driving = false;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BB38 File Offset: 0x00009D38
		private void OnCollisionStay(Collision col)
		{
			if (col.rigidbody != null)
			{
				CarAIControl component = col.rigidbody.GetComponent<CarAIControl>();
				if (component != null)
				{
					this.m_AvoidOtherCarTime = Time.time + 1f;
					if (Vector3.Angle(base.transform.forward, component.transform.position - base.transform.position) < 90f)
					{
						this.m_AvoidOtherCarSlowdown = 0.5f;
					}
					else
					{
						this.m_AvoidOtherCarSlowdown = 1f;
					}
					Vector3 vector = base.transform.InverseTransformPoint(component.transform.position);
					float f = Mathf.Atan2(vector.x, vector.z);
					this.m_AvoidPathOffset = this.m_LateralWanderDistance * -Mathf.Sign(f);
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BC06 File Offset: 0x00009E06
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
			this.m_Driving = true;
		}

		// Token: 0x04000252 RID: 594
		[SerializeField]
		[Range(0f, 1f)]
		private float m_CautiousSpeedFactor = 0.05f;

		// Token: 0x04000253 RID: 595
		[SerializeField]
		[Range(0f, 180f)]
		private float m_CautiousMaxAngle = 50f;

		// Token: 0x04000254 RID: 596
		[SerializeField]
		private float m_CautiousMaxDistance = 100f;

		// Token: 0x04000255 RID: 597
		[SerializeField]
		private float m_CautiousAngularVelocityFactor = 30f;

		// Token: 0x04000256 RID: 598
		[SerializeField]
		private float m_SteerSensitivity = 0.05f;

		// Token: 0x04000257 RID: 599
		[SerializeField]
		private float m_AccelSensitivity = 0.04f;

		// Token: 0x04000258 RID: 600
		[SerializeField]
		private float m_BrakeSensitivity = 1f;

		// Token: 0x04000259 RID: 601
		[SerializeField]
		private float m_LateralWanderDistance = 3f;

		// Token: 0x0400025A RID: 602
		[SerializeField]
		private float m_LateralWanderSpeed = 0.1f;

		// Token: 0x0400025B RID: 603
		[SerializeField]
		[Range(0f, 1f)]
		private float m_AccelWanderAmount = 0.1f;

		// Token: 0x0400025C RID: 604
		[SerializeField]
		private float m_AccelWanderSpeed = 0.1f;

		// Token: 0x0400025D RID: 605
		[SerializeField]
		private CarAIControl.BrakeCondition m_BrakeCondition = CarAIControl.BrakeCondition.TargetDistance;

		// Token: 0x0400025E RID: 606
		[SerializeField]
		private bool m_Driving;

		// Token: 0x0400025F RID: 607
		[SerializeField]
		private Transform m_Target;

		// Token: 0x04000260 RID: 608
		[SerializeField]
		private bool m_StopWhenTargetReached;

		// Token: 0x04000261 RID: 609
		[SerializeField]
		private float m_ReachTargetThreshold = 2f;

		// Token: 0x04000262 RID: 610
		private float m_RandomPerlin;

		// Token: 0x04000263 RID: 611
		private CarController m_CarController;

		// Token: 0x04000264 RID: 612
		private float m_AvoidOtherCarTime;

		// Token: 0x04000265 RID: 613
		private float m_AvoidOtherCarSlowdown;

		// Token: 0x04000266 RID: 614
		private float m_AvoidPathOffset;

		// Token: 0x04000267 RID: 615
		private Rigidbody m_Rigidbody;

		// Token: 0x020000E8 RID: 232
		public enum BrakeCondition
		{
			// Token: 0x04000499 RID: 1177
			NeverBrake,
			// Token: 0x0400049A RID: 1178
			TargetDirectionDifference,
			// Token: 0x0400049B RID: 1179
			TargetDistance
		}
	}
}

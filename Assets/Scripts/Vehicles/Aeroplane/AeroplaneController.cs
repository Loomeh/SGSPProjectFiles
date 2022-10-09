using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000093 RID: 147
	[RequireComponent(typeof(Rigidbody))]
	public class AeroplaneController : MonoBehaviour
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000D449 File Offset: 0x0000B649
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000D451 File Offset: 0x0000B651
		public float Altitude { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000D45A File Offset: 0x0000B65A
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000D462 File Offset: 0x0000B662
		public float Throttle { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000D46B File Offset: 0x0000B66B
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000D473 File Offset: 0x0000B673
		public bool AirBrakes { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000D47C File Offset: 0x0000B67C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000D484 File Offset: 0x0000B684
		public float ForwardSpeed { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D48D File Offset: 0x0000B68D
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000D495 File Offset: 0x0000B695
		public float EnginePower { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000D49E File Offset: 0x0000B69E
		public float MaxEnginePower
		{
			get
			{
				return this.m_MaxEnginePower;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public float RollAngle { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public float PitchAngle { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		public float RollInput { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D4D9 File Offset: 0x0000B6D9
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000D4E1 File Offset: 0x0000B6E1
		public float PitchInput { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D4EA File Offset: 0x0000B6EA
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000D4F2 File Offset: 0x0000B6F2
		public float YawInput { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D4FB File Offset: 0x0000B6FB
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000D503 File Offset: 0x0000B703
		public float ThrottleInput { get; private set; }

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D50C File Offset: 0x0000B70C
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_OriginalDrag = this.m_Rigidbody.drag;
			this.m_OriginalAngularDrag = this.m_Rigidbody.angularDrag;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				WheelCollider[] componentsInChildren = base.transform.GetChild(i).GetComponentsInChildren<WheelCollider>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].motorTorque = 0.18f;
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D58C File Offset: 0x0000B78C
		public void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, bool airBrakes)
		{
			this.RollInput = rollInput;
			this.PitchInput = pitchInput;
			this.YawInput = yawInput;
			this.ThrottleInput = throttleInput;
			this.AirBrakes = airBrakes;
			this.ClampInputs();
			this.CalculateRollAndPitchAngles();
			this.AutoLevel();
			this.CalculateForwardSpeed();
			this.ControlThrottle();
			this.CalculateDrag();
			this.CaluclateAerodynamicEffect();
			this.CalculateLinearForces();
			this.CalculateTorque();
			this.CalculateAltitude();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		private void ClampInputs()
		{
			this.RollInput = Mathf.Clamp(this.RollInput, -1f, 1f);
			this.PitchInput = Mathf.Clamp(this.PitchInput, -1f, 1f);
			this.YawInput = Mathf.Clamp(this.YawInput, -1f, 1f);
			this.ThrottleInput = Mathf.Clamp(this.ThrottleInput, -1f, 1f);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D678 File Offset: 0x0000B878
		private void CalculateRollAndPitchAngles()
		{
			Vector3 forward = base.transform.forward;
			forward.y = 0f;
			if (forward.sqrMagnitude > 0f)
			{
				forward.Normalize();
				Vector3 vector = base.transform.InverseTransformDirection(forward);
				this.PitchAngle = Mathf.Atan2(vector.y, vector.z);
				Vector3 direction = Vector3.Cross(Vector3.up, forward);
				Vector3 vector2 = base.transform.InverseTransformDirection(direction);
				this.RollAngle = Mathf.Atan2(vector2.y, vector2.x);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D708 File Offset: 0x0000B908
		private void AutoLevel()
		{
			this.m_BankedTurnAmount = Mathf.Sin(this.RollAngle);
			if (this.RollInput == 0f)
			{
				this.RollInput = -this.RollAngle * this.m_AutoRollLevel;
			}
			if (this.PitchInput == 0f)
			{
				this.PitchInput = -this.PitchAngle * this.m_AutoPitchLevel;
				this.PitchInput -= Mathf.Abs(this.m_BankedTurnAmount * this.m_BankedTurnAmount * this.m_AutoTurnPitch);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D790 File Offset: 0x0000B990
		private void CalculateForwardSpeed()
		{
			Vector3 vector = base.transform.InverseTransformDirection(this.m_Rigidbody.velocity);
			this.ForwardSpeed = Mathf.Max(0f, vector.z);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		private void ControlThrottle()
		{
			if (this.m_Immobilized)
			{
				this.ThrottleInput = -0.5f;
			}
			this.Throttle = Mathf.Clamp01(this.Throttle + this.ThrottleInput * Time.deltaTime * this.m_ThrottleChangeSpeed);
			this.EnginePower = this.Throttle * this.m_MaxEnginePower;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D824 File Offset: 0x0000BA24
		private void CalculateDrag()
		{
			float num = this.m_Rigidbody.velocity.magnitude * this.m_DragIncreaseFactor;
			this.m_Rigidbody.drag = (this.AirBrakes ? ((this.m_OriginalDrag + num) * this.m_AirBrakesEffect) : (this.m_OriginalDrag + num));
			this.m_Rigidbody.angularDrag = this.m_OriginalAngularDrag * this.ForwardSpeed;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D890 File Offset: 0x0000BA90
		private void CaluclateAerodynamicEffect()
		{
			if (this.m_Rigidbody.velocity.magnitude > 0f)
			{
				this.m_AeroFactor = Vector3.Dot(base.transform.forward, this.m_Rigidbody.velocity.normalized);
				this.m_AeroFactor *= this.m_AeroFactor;
				Vector3 velocity = Vector3.Lerp(this.m_Rigidbody.velocity, base.transform.forward * this.ForwardSpeed, this.m_AeroFactor * this.ForwardSpeed * this.m_AerodynamicEffect * Time.deltaTime);
				this.m_Rigidbody.velocity = velocity;
				this.m_Rigidbody.rotation = Quaternion.Slerp(this.m_Rigidbody.rotation, Quaternion.LookRotation(this.m_Rigidbody.velocity, base.transform.up), this.m_AerodynamicEffect * Time.deltaTime);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D988 File Offset: 0x0000BB88
		private void CalculateLinearForces()
		{
			Vector3 vector = Vector3.zero;
			vector += this.EnginePower * base.transform.forward;
			Vector3 normalized = Vector3.Cross(this.m_Rigidbody.velocity, base.transform.right).normalized;
			float num = Mathf.InverseLerp(this.m_ZeroLiftSpeed, 0f, this.ForwardSpeed);
			float d = this.ForwardSpeed * this.ForwardSpeed * this.m_Lift * num * this.m_AeroFactor;
			vector += d * normalized;
			this.m_Rigidbody.AddForce(vector);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		private void CalculateTorque()
		{
			Vector3 a = Vector3.zero;
			a += this.PitchInput * this.m_PitchEffect * base.transform.right;
			a += this.YawInput * this.m_YawEffect * base.transform.up;
			a += -this.RollInput * this.m_RollEffect * base.transform.forward;
			a += this.m_BankedTurnAmount * this.m_BankedTurnEffect * base.transform.up;
			this.m_Rigidbody.AddTorque(a * this.ForwardSpeed * this.m_AeroFactor);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		private void CalculateAltitude()
		{
			Ray ray = new Ray(base.transform.position - Vector3.up * 10f, -Vector3.up);
			RaycastHit raycastHit;
			this.Altitude = (Physics.Raycast(ray, out raycastHit) ? (raycastHit.distance + 10f) : base.transform.position.y);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public void Immobilize()
		{
			this.m_Immobilized = true;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000DB69 File Offset: 0x0000BD69
		public void Reset()
		{
			this.m_Immobilized = false;
		}

		// Token: 0x040002D0 RID: 720
		[SerializeField]
		private float m_MaxEnginePower = 40f;

		// Token: 0x040002D1 RID: 721
		[SerializeField]
		private float m_Lift = 0.002f;

		// Token: 0x040002D2 RID: 722
		[SerializeField]
		private float m_ZeroLiftSpeed = 300f;

		// Token: 0x040002D3 RID: 723
		[SerializeField]
		private float m_RollEffect = 1f;

		// Token: 0x040002D4 RID: 724
		[SerializeField]
		private float m_PitchEffect = 1f;

		// Token: 0x040002D5 RID: 725
		[SerializeField]
		private float m_YawEffect = 0.2f;

		// Token: 0x040002D6 RID: 726
		[SerializeField]
		private float m_BankedTurnEffect = 0.5f;

		// Token: 0x040002D7 RID: 727
		[SerializeField]
		private float m_AerodynamicEffect = 0.02f;

		// Token: 0x040002D8 RID: 728
		[SerializeField]
		private float m_AutoTurnPitch = 0.5f;

		// Token: 0x040002D9 RID: 729
		[SerializeField]
		private float m_AutoRollLevel = 0.2f;

		// Token: 0x040002DA RID: 730
		[SerializeField]
		private float m_AutoPitchLevel = 0.2f;

		// Token: 0x040002DB RID: 731
		[SerializeField]
		private float m_AirBrakesEffect = 3f;

		// Token: 0x040002DC RID: 732
		[SerializeField]
		private float m_ThrottleChangeSpeed = 0.3f;

		// Token: 0x040002DD RID: 733
		[SerializeField]
		private float m_DragIncreaseFactor = 0.001f;

		// Token: 0x040002E9 RID: 745
		private float m_OriginalDrag;

		// Token: 0x040002EA RID: 746
		private float m_OriginalAngularDrag;

		// Token: 0x040002EB RID: 747
		private float m_AeroFactor;

		// Token: 0x040002EC RID: 748
		private bool m_Immobilized;

		// Token: 0x040002ED RID: 749
		private float m_BankedTurnAmount;

		// Token: 0x040002EE RID: 750
		private Rigidbody m_Rigidbody;

		// Token: 0x040002EF RID: 751
		private WheelCollider[] m_WheelColliders;
	}
}

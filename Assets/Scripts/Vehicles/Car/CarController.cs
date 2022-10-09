using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000089 RID: 137
	public class CarController : MonoBehaviour
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000C0FF File Offset: 0x0000A2FF
		public bool Skidding { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000C108 File Offset: 0x0000A308
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000C110 File Offset: 0x0000A310
		public float BrakeInput { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000C119 File Offset: 0x0000A319
		public float CurrentSteerAngle
		{
			get
			{
				return this.m_SteerAngle;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000C124 File Offset: 0x0000A324
		public float CurrentSpeed
		{
			get
			{
				return this.m_Rigidbody.velocity.magnitude * 2.2369363f;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C14A File Offset: 0x0000A34A
		public float MaxSpeed
		{
			get
			{
				return this.m_Topspeed;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000C152 File Offset: 0x0000A352
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000C15A File Offset: 0x0000A35A
		public float Revs { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000C163 File Offset: 0x0000A363
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000C16B File Offset: 0x0000A36B
		public float AccelInput { get; private set; }

		// Token: 0x06000272 RID: 626 RVA: 0x0000C174 File Offset: 0x0000A374
		private void Start()
		{
			this.m_WheelMeshLocalRotations = new Quaternion[4];
			for (int i = 0; i < 4; i++)
			{
				this.m_WheelMeshLocalRotations[i] = this.m_WheelMeshes[i].transform.localRotation;
			}
			this.m_WheelColliders[0].attachedRigidbody.centerOfMass = this.m_CentreOfMassOffset;
			this.m_MaxHandbrakeTorque = float.MaxValue;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_CurrentTorque = this.m_FullTorqueOverAllWheels - this.m_TractionControl * this.m_FullTorqueOverAllWheels;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C200 File Offset: 0x0000A400
		private void GearChanging()
		{
			float num = Mathf.Abs(this.CurrentSpeed / this.MaxSpeed);
			float num2 = 1f / (float)CarController.NoOfGears * (float)(this.m_GearNum + 1);
			float num3 = 1f / (float)CarController.NoOfGears * (float)this.m_GearNum;
			if (this.m_GearNum > 0 && num < num3)
			{
				this.m_GearNum--;
			}
			if (num > num2 && this.m_GearNum < CarController.NoOfGears - 1)
			{
				this.m_GearNum++;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C288 File Offset: 0x0000A488
		private static float CurveFactor(float factor)
		{
			return 1f - (1f - factor) * (1f - factor);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C083 File Offset: 0x0000A283
		private static float ULerp(float from, float to, float value)
		{
			return (1f - value) * from + value * to;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private void CalculateGearFactor()
		{
			float num = 1f / (float)CarController.NoOfGears;
			float b = Mathf.InverseLerp(num * (float)this.m_GearNum, num * (float)(this.m_GearNum + 1), Mathf.Abs(this.CurrentSpeed / this.MaxSpeed));
			this.m_GearFactor = Mathf.Lerp(this.m_GearFactor, b, Time.deltaTime * 5f);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C304 File Offset: 0x0000A504
		private void CalculateRevs()
		{
			this.CalculateGearFactor();
			float num = (float)this.m_GearNum / (float)CarController.NoOfGears;
			float from = CarController.ULerp(0f, this.m_RevRangeBoundary, CarController.CurveFactor(num));
			float to = CarController.ULerp(this.m_RevRangeBoundary, 1f, num);
			this.Revs = CarController.ULerp(from, to, this.m_GearFactor);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C364 File Offset: 0x0000A564
		public void Move(float steering, float accel, float footbrake, float handbrake)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 position;
				Quaternion rotation;
				this.m_WheelColliders[i].GetWorldPose(out position, out rotation);
				this.m_WheelMeshes[i].transform.position = position;
				this.m_WheelMeshes[i].transform.rotation = rotation;
			}
			steering = Mathf.Clamp(steering, -1f, 1f);
			accel = (this.AccelInput = Mathf.Clamp(accel, 0f, 1f));
			footbrake = (this.BrakeInput = -1f * Mathf.Clamp(footbrake, -1f, 0f));
			handbrake = Mathf.Clamp(handbrake, 0f, 1f);
			this.m_SteerAngle = steering * this.m_MaximumSteerAngle;
			this.m_WheelColliders[0].steerAngle = this.m_SteerAngle;
			this.m_WheelColliders[1].steerAngle = this.m_SteerAngle;
			this.SteerHelper();
			this.ApplyDrive(accel, footbrake);
			this.CapSpeed();
			if (handbrake > 0f)
			{
				float brakeTorque = handbrake * this.m_MaxHandbrakeTorque;
				this.m_WheelColliders[2].brakeTorque = brakeTorque;
				this.m_WheelColliders[3].brakeTorque = brakeTorque;
			}
			this.CalculateRevs();
			this.GearChanging();
			this.AddDownForce();
			this.CheckForWheelSpin();
			this.TractionControl();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		private void CapSpeed()
		{
			float num = this.m_Rigidbody.velocity.magnitude;
			SpeedType speedType = this.m_SpeedType;
			if (speedType != SpeedType.MPH)
			{
				if (speedType != SpeedType.KPH)
				{
					return;
				}
				num *= 3.6f;
				if (num > this.m_Topspeed)
				{
					this.m_Rigidbody.velocity = this.m_Topspeed / 3.6f * this.m_Rigidbody.velocity.normalized;
				}
			}
			else
			{
				num *= 2.2369363f;
				if (num > this.m_Topspeed)
				{
					this.m_Rigidbody.velocity = this.m_Topspeed / 2.2369363f * this.m_Rigidbody.velocity.normalized;
					return;
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C55C File Offset: 0x0000A75C
		private void ApplyDrive(float accel, float footbrake)
		{
			switch (this.m_CarDriveType)
			{
			case CarDriveType.FrontWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 2f);
				this.m_WheelColliders[0].motorTorque = (this.m_WheelColliders[1].motorTorque = motorTorque);
				break;
			}
			case CarDriveType.RearWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 2f);
				this.m_WheelColliders[2].motorTorque = (this.m_WheelColliders[3].motorTorque = motorTorque);
				break;
			}
			case CarDriveType.FourWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 4f);
				for (int i = 0; i < 4; i++)
				{
					this.m_WheelColliders[i].motorTorque = motorTorque;
				}
				break;
			}
			}
			for (int j = 0; j < 4; j++)
			{
				if (this.CurrentSpeed > 5f && Vector3.Angle(base.transform.forward, this.m_Rigidbody.velocity) < 50f)
				{
					this.m_WheelColliders[j].brakeTorque = this.m_BrakeTorque * footbrake;
				}
				else if (footbrake > 0f)
				{
					this.m_WheelColliders[j].brakeTorque = 0f;
					this.m_WheelColliders[j].motorTorque = -this.m_ReverseTorque * footbrake;
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C69C File Offset: 0x0000A89C
		private void SteerHelper()
		{
			for (int i = 0; i < 4; i++)
			{
				WheelHit wheelHit;
				this.m_WheelColliders[i].GetGroundHit(out wheelHit);
				if (wheelHit.normal == Vector3.zero)
				{
					return;
				}
			}
			if (Mathf.Abs(this.m_OldRotation - base.transform.eulerAngles.y) < 10f)
			{
				Quaternion rotation = Quaternion.AngleAxis((base.transform.eulerAngles.y - this.m_OldRotation) * this.m_SteerHelper, Vector3.up);
				this.m_Rigidbody.velocity = rotation * this.m_Rigidbody.velocity;
			}
			this.m_OldRotation = base.transform.eulerAngles.y;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C758 File Offset: 0x0000A958
		private void AddDownForce()
		{
			this.m_WheelColliders[0].attachedRigidbody.AddForce(-base.transform.up * this.m_Downforce * this.m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		private void CheckForWheelSpin()
		{
			for (int i = 0; i < 4; i++)
			{
				WheelHit wheelHit;
				this.m_WheelColliders[i].GetGroundHit(out wheelHit);
				if (Mathf.Abs(wheelHit.forwardSlip) >= this.m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= this.m_SlipLimit)
				{
					this.m_WheelEffects[i].EmitTyreSmoke();
					if (!this.AnySkidSoundPlaying())
					{
						this.m_WheelEffects[i].PlayAudio();
					}
				}
				else
				{
					if (this.m_WheelEffects[i].PlayingAudio)
					{
						this.m_WheelEffects[i].StopAudio();
					}
					this.m_WheelEffects[i].EndSkidTrail();
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C858 File Offset: 0x0000AA58
		private void TractionControl()
		{
			switch (this.m_CarDriveType)
			{
			case CarDriveType.FrontWheelDrive:
			{
				WheelHit wheelHit;
				this.m_WheelColliders[0].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				this.m_WheelColliders[1].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				return;
			}
			case CarDriveType.RearWheelDrive:
			{
				WheelHit wheelHit;
				this.m_WheelColliders[2].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				this.m_WheelColliders[3].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				return;
			}
			case CarDriveType.FourWheelDrive:
				for (int i = 0; i < 4; i++)
				{
					WheelHit wheelHit;
					this.m_WheelColliders[i].GetGroundHit(out wheelHit);
					this.AdjustTorque(wheelHit.forwardSlip);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C920 File Offset: 0x0000AB20
		private void AdjustTorque(float forwardSlip)
		{
			if (forwardSlip >= this.m_SlipLimit && this.m_CurrentTorque >= 0f)
			{
				this.m_CurrentTorque -= 10f * this.m_TractionControl;
				return;
			}
			this.m_CurrentTorque += 10f * this.m_TractionControl;
			if (this.m_CurrentTorque > this.m_FullTorqueOverAllWheels)
			{
				this.m_CurrentTorque = this.m_FullTorqueOverAllWheels;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C990 File Offset: 0x0000AB90
		private bool AnySkidSoundPlaying()
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.m_WheelEffects[i].PlayingAudio)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000281 RID: 641
		[SerializeField]
		private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive;

		// Token: 0x04000282 RID: 642
		[SerializeField]
		private WheelCollider[] m_WheelColliders = new WheelCollider[4];

		// Token: 0x04000283 RID: 643
		[SerializeField]
		private GameObject[] m_WheelMeshes = new GameObject[4];

		// Token: 0x04000284 RID: 644
		[SerializeField]
		private WheelEffects[] m_WheelEffects = new WheelEffects[4];

		// Token: 0x04000285 RID: 645
		[SerializeField]
		private Vector3 m_CentreOfMassOffset;

		// Token: 0x04000286 RID: 646
		[SerializeField]
		private float m_MaximumSteerAngle;

		// Token: 0x04000287 RID: 647
		[Range(0f, 1f)]
		[SerializeField]
		private float m_SteerHelper;

		// Token: 0x04000288 RID: 648
		[Range(0f, 1f)]
		[SerializeField]
		private float m_TractionControl;

		// Token: 0x04000289 RID: 649
		[SerializeField]
		private float m_FullTorqueOverAllWheels;

		// Token: 0x0400028A RID: 650
		[SerializeField]
		private float m_ReverseTorque;

		// Token: 0x0400028B RID: 651
		[SerializeField]
		private float m_MaxHandbrakeTorque;

		// Token: 0x0400028C RID: 652
		[SerializeField]
		private float m_Downforce = 100f;

		// Token: 0x0400028D RID: 653
		[SerializeField]
		private SpeedType m_SpeedType;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		private float m_Topspeed = 200f;

		// Token: 0x0400028F RID: 655
		[SerializeField]
		private static int NoOfGears = 5;

		// Token: 0x04000290 RID: 656
		[SerializeField]
		private float m_RevRangeBoundary = 1f;

		// Token: 0x04000291 RID: 657
		[SerializeField]
		private float m_SlipLimit;

		// Token: 0x04000292 RID: 658
		[SerializeField]
		private float m_BrakeTorque;

		// Token: 0x04000293 RID: 659
		private Quaternion[] m_WheelMeshLocalRotations;

		// Token: 0x04000294 RID: 660
		private Vector3 m_Prevpos;

		// Token: 0x04000295 RID: 661
		private Vector3 m_Pos;

		// Token: 0x04000296 RID: 662
		private float m_SteerAngle;

		// Token: 0x04000297 RID: 663
		private int m_GearNum;

		// Token: 0x04000298 RID: 664
		private float m_GearFactor;

		// Token: 0x04000299 RID: 665
		private float m_OldRotation;

		// Token: 0x0400029A RID: 666
		private float m_CurrentTorque;

		// Token: 0x0400029B RID: 667
		private Rigidbody m_Rigidbody;

		// Token: 0x0400029C RID: 668
		private const float k_ReversingThreshold = 0.01f;
	}
}

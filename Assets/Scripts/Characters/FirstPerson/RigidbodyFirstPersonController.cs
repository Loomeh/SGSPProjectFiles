using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x020000A1 RID: 161
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class RigidbodyFirstPersonController : MonoBehaviour
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000F747 File Offset: 0x0000D947
		public Vector3 Velocity
		{
			get
			{
				return this.m_RigidBody.velocity;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000F754 File Offset: 0x0000D954
		public bool Grounded
		{
			get
			{
				return this.m_IsGrounded;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000F75C File Offset: 0x0000D95C
		public bool Jumping
		{
			get
			{
				return this.m_Jumping;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000F764 File Offset: 0x0000D964
		public bool Running
		{
			get
			{
				return this.movementSettings.Running;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000F771 File Offset: 0x0000D971
		private void Start()
		{
			this.m_RigidBody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.mouseLook.Init(base.transform, this.cam.transform);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F7A7 File Offset: 0x0000D9A7
		private void Update()
		{
			this.RotateView();
			if (CrossPlatformInputManager.GetButtonDown("Jump") && !this.m_Jump)
			{
				this.m_Jump = true;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		private void FixedUpdate()
		{
			this.GroundCheck();
			Vector2 input = this.GetInput();
			if ((Mathf.Abs(input.x) > 1E-45f || Mathf.Abs(input.y) > 1E-45f) && (this.advancedSettings.airControl || this.m_IsGrounded))
			{
				Vector3 vector = this.cam.transform.forward * input.y + this.cam.transform.right * input.x;
				vector = Vector3.ProjectOnPlane(vector, this.m_GroundContactNormal).normalized;
				vector.x *= this.movementSettings.CurrentTargetSpeed;
				vector.z *= this.movementSettings.CurrentTargetSpeed;
				vector.y *= this.movementSettings.CurrentTargetSpeed;
				if (this.m_RigidBody.velocity.sqrMagnitude < this.movementSettings.CurrentTargetSpeed * this.movementSettings.CurrentTargetSpeed)
				{
					this.m_RigidBody.AddForce(vector * this.SlopeMultiplier(), ForceMode.Impulse);
				}
			}
			if (this.m_IsGrounded)
			{
				this.m_RigidBody.drag = 5f;
				if (this.m_Jump)
				{
					this.m_RigidBody.drag = 0f;
					this.m_RigidBody.velocity = new Vector3(this.m_RigidBody.velocity.x, 0f, this.m_RigidBody.velocity.z);
					this.m_RigidBody.AddForce(new Vector3(0f, this.movementSettings.JumpForce, 0f), ForceMode.Impulse);
					this.m_Jumping = true;
				}
				if (!this.m_Jumping && Mathf.Abs(input.x) < 1E-45f && Mathf.Abs(input.y) < 1E-45f && this.m_RigidBody.velocity.magnitude < 1f)
				{
					this.m_RigidBody.Sleep();
				}
			}
			else
			{
				this.m_RigidBody.drag = 0f;
				if (this.m_PreviouslyGrounded && !this.m_Jumping)
				{
					this.StickToGroundHelper();
				}
			}
			this.m_Jump = false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		private float SlopeMultiplier()
		{
			float time = Vector3.Angle(this.m_GroundContactNormal, Vector3.up);
			return this.movementSettings.SlopeCurveModifier.Evaluate(time);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		private void StickToGroundHelper()
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position, this.m_Capsule.radius * (1f - this.advancedSettings.shellOffset), Vector3.down, out raycastHit, this.m_Capsule.height / 2f - this.m_Capsule.radius + this.advancedSettings.stickToGroundHelperDistance, -1, QueryTriggerInteraction.Ignore) && Mathf.Abs(Vector3.Angle(raycastHit.normal, Vector3.up)) < 85f)
			{
				this.m_RigidBody.velocity = Vector3.ProjectOnPlane(this.m_RigidBody.velocity, raycastHit.normal);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		private Vector2 GetInput()
		{
			Vector2 vector = new Vector2
			{
				x = CrossPlatformInputManager.GetAxis("Horizontal"),
				y = CrossPlatformInputManager.GetAxis("Vertical")
			};
			this.movementSettings.UpdateDesiredTargetSpeed(vector);
			return vector;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000FB44 File Offset: 0x0000DD44
		private void RotateView()
		{
			if (Mathf.Abs(Time.timeScale) < 1E-45f)
			{
				return;
			}
			float y = base.transform.eulerAngles.y;
			this.mouseLook.LookRotation(base.transform, this.cam.transform);
			if (this.m_IsGrounded || this.advancedSettings.airControl)
			{
				Quaternion rotation = Quaternion.AngleAxis(base.transform.eulerAngles.y - y, Vector3.up);
				this.m_RigidBody.velocity = rotation * this.m_RigidBody.velocity;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		private void GroundCheck()
		{
			this.m_PreviouslyGrounded = this.m_IsGrounded;
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position, this.m_Capsule.radius * (1f - this.advancedSettings.shellOffset), Vector3.down, out raycastHit, this.m_Capsule.height / 2f - this.m_Capsule.radius + this.advancedSettings.groundCheckDistance, -1, QueryTriggerInteraction.Ignore))
			{
				this.m_IsGrounded = true;
				this.m_GroundContactNormal = raycastHit.normal;
			}
			else
			{
				this.m_IsGrounded = false;
				this.m_GroundContactNormal = Vector3.up;
			}
			if (!this.m_PreviouslyGrounded && this.m_IsGrounded && this.m_Jumping)
			{
				this.m_Jumping = false;
			}
		}

		// Token: 0x04000368 RID: 872
		public Camera cam;

		// Token: 0x04000369 RID: 873
		public RigidbodyFirstPersonController.MovementSettings movementSettings = new RigidbodyFirstPersonController.MovementSettings();

		// Token: 0x0400036A RID: 874
		public MouseLook mouseLook = new MouseLook();

		// Token: 0x0400036B RID: 875
		public RigidbodyFirstPersonController.AdvancedSettings advancedSettings = new RigidbodyFirstPersonController.AdvancedSettings();

		// Token: 0x0400036C RID: 876
		private Rigidbody m_RigidBody;

		// Token: 0x0400036D RID: 877
		private CapsuleCollider m_Capsule;

		// Token: 0x0400036E RID: 878
		private float m_YRotation;

		// Token: 0x0400036F RID: 879
		private Vector3 m_GroundContactNormal;

		// Token: 0x04000370 RID: 880
		private bool m_Jump;

		// Token: 0x04000371 RID: 881
		private bool m_PreviouslyGrounded;

		// Token: 0x04000372 RID: 882
		private bool m_Jumping;

		// Token: 0x04000373 RID: 883
		private bool m_IsGrounded;

		// Token: 0x020000EF RID: 239
		[Serializable]
		public class MovementSettings
		{
			// Token: 0x0600044A RID: 1098 RVA: 0x00013744 File Offset: 0x00011944
			public void UpdateDesiredTargetSpeed(Vector2 input)
			{
				if (input == Vector2.zero)
				{
					return;
				}
				if (input.x > 0f || input.x < 0f)
				{
					this.CurrentTargetSpeed = this.StrafeSpeed;
				}
				if (input.y < 0f)
				{
					this.CurrentTargetSpeed = this.BackwardSpeed;
				}
				if (input.y > 0f)
				{
					this.CurrentTargetSpeed = this.ForwardSpeed;
				}
				if (Input.GetKey(this.RunKey))
				{
					this.CurrentTargetSpeed *= this.RunMultiplier;
					this.m_Running = true;
					return;
				}
				this.m_Running = false;
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x000137E6 File Offset: 0x000119E6
			public bool Running
			{
				get
				{
					return this.m_Running;
				}
			}

			// Token: 0x040004B4 RID: 1204
			public float ForwardSpeed = 8f;

			// Token: 0x040004B5 RID: 1205
			public float BackwardSpeed = 4f;

			// Token: 0x040004B6 RID: 1206
			public float StrafeSpeed = 4f;

			// Token: 0x040004B7 RID: 1207
			public float RunMultiplier = 2f;

			// Token: 0x040004B8 RID: 1208
			public KeyCode RunKey = KeyCode.LeftShift;

			// Token: 0x040004B9 RID: 1209
			public float JumpForce = 30f;

			// Token: 0x040004BA RID: 1210
			public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(-90f, 1f),
				new Keyframe(0f, 1f),
				new Keyframe(90f, 0f)
			});

			// Token: 0x040004BB RID: 1211
			[HideInInspector]
			public float CurrentTargetSpeed = 8f;

			// Token: 0x040004BC RID: 1212
			private bool m_Running;
		}

		// Token: 0x020000F0 RID: 240
		[Serializable]
		public class AdvancedSettings
		{
			// Token: 0x040004BD RID: 1213
			public float groundCheckDistance = 0.01f;

			// Token: 0x040004BE RID: 1214
			public float stickToGroundHelperDistance = 0.5f;

			// Token: 0x040004BF RID: 1215
			public float slowDownRate = 20f;

			// Token: 0x040004C0 RID: 1216
			public bool airControl;

			// Token: 0x040004C1 RID: 1217
			[Tooltip("set it to 0.1 or more if you get stuck in wall")]
			public float shellOffset;
		}
	}
}

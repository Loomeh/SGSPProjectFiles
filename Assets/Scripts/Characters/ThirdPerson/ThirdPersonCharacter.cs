using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x0200009C RID: 156
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		private void Start()
		{
			this.m_Animator = base.GetComponent<Animator>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;
			this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			this.m_OrigGroundCheckDistance = this.m_GroundCheckDistance;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E53C File Offset: 0x0000C73C
		public void Move(Vector3 move, bool crouch, bool jump)
		{
			if (move.magnitude > 1f)
			{
				move.Normalize();
			}
			move = base.transform.InverseTransformDirection(move);
			this.CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, this.m_GroundNormal);
			this.m_TurnAmount = Mathf.Atan2(move.x, move.z);
			this.m_ForwardAmount = move.z;
			this.ApplyExtraTurnRotation();
			if (this.m_IsGrounded)
			{
				this.HandleGroundedMovement(crouch, jump);
			}
			else
			{
				this.HandleAirborneMovement();
			}
			this.ScaleCapsuleForCrouching(crouch);
			this.PreventStandingInLowHeadroom();
			this.UpdateAnimator(move);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		private void ScaleCapsuleForCrouching(bool crouch)
		{
			if (this.m_IsGrounded && crouch)
			{
				if (this.m_Crouching)
				{
					return;
				}
				this.m_Capsule.height = this.m_Capsule.height / 2f;
				this.m_Capsule.center = this.m_Capsule.center / 2f;
				this.m_Crouching = true;
				return;
			}
			else
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -1, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
					return;
				}
				this.m_Capsule.height = this.m_CapsuleHeight;
				this.m_Capsule.center = this.m_CapsuleCenter;
				this.m_Crouching = false;
				return;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		private void PreventStandingInLowHeadroom()
		{
			if (!this.m_Crouching)
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -1, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E768 File Offset: 0x0000C968
		private void UpdateAnimator(Vector3 move)
		{
			this.m_Animator.SetFloat("Forward", this.m_ForwardAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this.m_Animator.SetBool("OnGround", this.m_IsGrounded);
			if (!this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("Jump", this.m_Rigidbody.velocity.y);
			}
			float value = (float)((Mathf.Repeat(this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this.m_RunCycleLegOffset, 1f) < 0.5f) ? 1 : -1) * this.m_ForwardAmount;
			if (this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("JumpLeg", value);
			}
			if (this.m_IsGrounded && move.magnitude > 0f)
			{
				this.m_Animator.speed = this.m_AnimSpeedMultiplier;
				return;
			}
			this.m_Animator.speed = 1f;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E894 File Offset: 0x0000CA94
		private void HandleAirborneMovement()
		{
			Vector3 force = Physics.gravity * this.m_GravityMultiplier - Physics.gravity;
			this.m_Rigidbody.AddForce(force);
			this.m_GroundCheckDistance = ((this.m_Rigidbody.velocity.y < 0f) ? this.m_OrigGroundCheckDistance : 0.01f);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		private void HandleGroundedMovement(bool crouch, bool jump)
		{
			if (jump && !crouch && this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				this.m_Rigidbody.velocity = new Vector3(this.m_Rigidbody.velocity.x, this.m_JumpPower, this.m_Rigidbody.velocity.z);
				this.m_IsGrounded = false;
				this.m_Animator.applyRootMotion = false;
				this.m_GroundCheckDistance = 0.1f;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E978 File Offset: 0x0000CB78
		private void ApplyExtraTurnRotation()
		{
			float num = Mathf.Lerp(this.m_StationaryTurnSpeed, this.m_MovingTurnSpeed, this.m_ForwardAmount);
			base.transform.Rotate(0f, this.m_TurnAmount * num * Time.deltaTime, 0f);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public void OnAnimatorMove()
		{
			if (this.m_IsGrounded && Time.deltaTime > 0f)
			{
				Vector3 velocity = this.m_Animator.deltaPosition * this.m_MoveSpeedMultiplier / Time.deltaTime;
				velocity.y = this.m_Rigidbody.velocity.y;
				this.m_Rigidbody.velocity = velocity;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000EA28 File Offset: 0x0000CC28
		private void CheckGroundStatus()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out raycastHit, this.m_GroundCheckDistance))
			{
				this.m_GroundNormal = raycastHit.normal;
				this.m_IsGrounded = true;
				this.m_Animator.applyRootMotion = true;
				return;
			}
			this.m_IsGrounded = false;
			this.m_GroundNormal = Vector3.up;
			this.m_Animator.applyRootMotion = false;
		}

		// Token: 0x0400031E RID: 798
		[SerializeField]
		private float m_MovingTurnSpeed = 360f;

		// Token: 0x0400031F RID: 799
		[SerializeField]
		private float m_StationaryTurnSpeed = 180f;

		// Token: 0x04000320 RID: 800
		[SerializeField]
		private float m_JumpPower = 12f;

		// Token: 0x04000321 RID: 801
		[Range(1f, 4f)]
		[SerializeField]
		private float m_GravityMultiplier = 2f;

		// Token: 0x04000322 RID: 802
		[SerializeField]
		private float m_RunCycleLegOffset = 0.2f;

		// Token: 0x04000323 RID: 803
		[SerializeField]
		private float m_MoveSpeedMultiplier = 1f;

		// Token: 0x04000324 RID: 804
		[SerializeField]
		private float m_AnimSpeedMultiplier = 1f;

		// Token: 0x04000325 RID: 805
		[SerializeField]
		private float m_GroundCheckDistance = 0.1f;

		// Token: 0x04000326 RID: 806
		private Rigidbody m_Rigidbody;

		// Token: 0x04000327 RID: 807
		private Animator m_Animator;

		// Token: 0x04000328 RID: 808
		private bool m_IsGrounded;

		// Token: 0x04000329 RID: 809
		private float m_OrigGroundCheckDistance;

		// Token: 0x0400032A RID: 810
		private const float k_Half = 0.5f;

		// Token: 0x0400032B RID: 811
		private float m_TurnAmount;

		// Token: 0x0400032C RID: 812
		private float m_ForwardAmount;

		// Token: 0x0400032D RID: 813
		private Vector3 m_GroundNormal;

		// Token: 0x0400032E RID: 814
		private float m_CapsuleHeight;

		// Token: 0x0400032F RID: 815
		private Vector3 m_CapsuleCenter;

		// Token: 0x04000330 RID: 816
		private CapsuleCollider m_Capsule;

		// Token: 0x04000331 RID: 817
		private bool m_Crouching;
	}
}

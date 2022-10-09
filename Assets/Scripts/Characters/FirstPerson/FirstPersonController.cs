using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x0200009E RID: 158
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonController : MonoBehaviour
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000EC64 File Offset: 0x0000CE64
		private void Start()
		{
			this.m_CharacterController = base.GetComponent<CharacterController>();
			this.m_Camera = Camera.main;
			this.m_OriginalCameraPosition = this.m_Camera.transform.localPosition;
			this.m_FovKick.Setup(this.m_Camera);
			this.m_HeadBob.Setup(this.m_Camera, this.m_StepInterval);
			this.m_StepCycle = 0f;
			this.m_NextStep = this.m_StepCycle / 2f;
			this.m_Jumping = false;
			this.m_AudioSource = base.GetComponent<AudioSource>();
			this.m_MouseLook.Init(base.transform, this.m_Camera.transform);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000ED14 File Offset: 0x0000CF14
		private void Update()
		{
			this.RotateView();
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			if (!this.m_PreviouslyGrounded && this.m_CharacterController.isGrounded)
			{
				base.StartCoroutine(this.m_JumpBob.DoBobCycle());
				this.PlayLandingSound();
				this.m_MoveDir.y = 0f;
				this.m_Jumping = false;
			}
			if (!this.m_CharacterController.isGrounded && !this.m_Jumping && this.m_PreviouslyGrounded)
			{
				this.m_MoveDir.y = 0f;
			}
			this.m_PreviouslyGrounded = this.m_CharacterController.isGrounded;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EDC1 File Offset: 0x0000CFC1
		private void PlayLandingSound()
		{
			this.m_AudioSource.clip = this.m_LandSound;
			this.m_AudioSource.Play();
			this.m_NextStep = this.m_StepCycle + 0.5f;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		private void FixedUpdate()
		{
			float num;
			this.GetInput(out num);
			Vector3 vector = base.transform.forward * this.m_Input.y + base.transform.right * this.m_Input.x;
			RaycastHit raycastHit;
			Physics.SphereCast(base.transform.position, this.m_CharacterController.radius, Vector3.down, out raycastHit, this.m_CharacterController.height / 2f, -1, QueryTriggerInteraction.Ignore);
			vector = Vector3.ProjectOnPlane(vector, raycastHit.normal).normalized;
			this.m_MoveDir.x = vector.x * num;
			this.m_MoveDir.z = vector.z * num;
			if (this.m_CharacterController.isGrounded)
			{
				this.m_MoveDir.y = -this.m_StickToGroundForce;
				if (this.m_Jump)
				{
					this.m_MoveDir.y = this.m_JumpSpeed;
					this.PlayJumpSound();
					this.m_Jump = false;
					this.m_Jumping = true;
				}
			}
			else
			{
				this.m_MoveDir += Physics.gravity * this.m_GravityMultiplier * Time.fixedDeltaTime;
			}
			this.m_CollisionFlags = this.m_CharacterController.Move(this.m_MoveDir * Time.fixedDeltaTime);
			this.ProgressStepCycle(num);
			this.UpdateCameraPosition(num);
			this.m_MouseLook.UpdateCursorLock();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EF6A File Offset: 0x0000D16A
		private void PlayJumpSound()
		{
			this.m_AudioSource.clip = this.m_JumpSound;
			this.m_AudioSource.Play();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EF88 File Offset: 0x0000D188
		private void ProgressStepCycle(float speed)
		{
			if (this.m_CharacterController.velocity.sqrMagnitude > 0f && (this.m_Input.x != 0f || this.m_Input.y != 0f))
			{
				this.m_StepCycle += (this.m_CharacterController.velocity.magnitude + speed * (this.m_IsWalking ? 1f : this.m_RunstepLenghten)) * Time.fixedDeltaTime;
			}
			if (this.m_StepCycle <= this.m_NextStep)
			{
				return;
			}
			this.m_NextStep = this.m_StepCycle + this.m_StepInterval;
			this.PlayFootStepAudio();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F03C File Offset: 0x0000D23C
		private void PlayFootStepAudio()
		{
			if (!this.m_CharacterController.isGrounded)
			{
				return;
			}
			int num = Random.Range(1, this.m_FootstepSounds.Length);
			this.m_AudioSource.clip = this.m_FootstepSounds[num];
			this.m_AudioSource.PlayOneShot(this.m_AudioSource.clip);
			this.m_FootstepSounds[num] = this.m_FootstepSounds[0];
			this.m_FootstepSounds[0] = this.m_AudioSource.clip;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		private void UpdateCameraPosition(float speed)
		{
			if (!this.m_UseHeadBob)
			{
				return;
			}
			Vector3 localPosition;
			if (this.m_CharacterController.velocity.magnitude > 0f && this.m_CharacterController.isGrounded)
			{
				this.m_Camera.transform.localPosition = this.m_HeadBob.DoHeadBob(this.m_CharacterController.velocity.magnitude + speed * (this.m_IsWalking ? 1f : this.m_RunstepLenghten));
				localPosition = this.m_Camera.transform.localPosition;
				localPosition.y = this.m_Camera.transform.localPosition.y - this.m_JumpBob.Offset();
			}
			else
			{
				localPosition = this.m_Camera.transform.localPosition;
				localPosition.y = this.m_OriginalCameraPosition.y - this.m_JumpBob.Offset();
			}
			this.m_Camera.transform.localPosition = localPosition;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		private void GetInput(out float speed)
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool isWalking = this.m_IsWalking;
			this.m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
			speed = (this.m_IsWalking ? this.m_WalkSpeed : this.m_RunSpeed);
			this.m_Input = new Vector2(axis, axis2);
			if (this.m_Input.sqrMagnitude > 1f)
			{
				this.m_Input.Normalize();
			}
			if (this.m_IsWalking != isWalking && this.m_UseFovKick && this.m_CharacterController.velocity.sqrMagnitude > 0f)
			{
				base.StopAllCoroutines();
				base.StartCoroutine((!this.m_IsWalking) ? this.m_FovKick.FOVKickUp() : this.m_FovKick.FOVKickDown());
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F28F File Offset: 0x0000D48F
		private void RotateView()
		{
			this.m_MouseLook.LookRotation(base.transform, this.m_Camera.transform);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
			if (this.m_CollisionFlags == CollisionFlags.Below)
			{
				return;
			}
			if (attachedRigidbody == null || attachedRigidbody.isKinematic)
			{
				return;
			}
			attachedRigidbody.AddForceAtPosition(this.m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
		}

		// Token: 0x04000337 RID: 823
		[SerializeField]
		private bool m_IsWalking;

		// Token: 0x04000338 RID: 824
		[SerializeField]
		private float m_WalkSpeed;

		// Token: 0x04000339 RID: 825
		[SerializeField]
		private float m_RunSpeed;

		// Token: 0x0400033A RID: 826
		[SerializeField]
		[Range(0f, 1f)]
		private float m_RunstepLenghten;

		// Token: 0x0400033B RID: 827
		[SerializeField]
		private float m_JumpSpeed;

		// Token: 0x0400033C RID: 828
		[SerializeField]
		private float m_StickToGroundForce;

		// Token: 0x0400033D RID: 829
		[SerializeField]
		private float m_GravityMultiplier;

		// Token: 0x0400033E RID: 830
		[SerializeField]
		private MouseLook m_MouseLook;

		// Token: 0x0400033F RID: 831
		[SerializeField]
		private bool m_UseFovKick;

		// Token: 0x04000340 RID: 832
		[SerializeField]
		private FOVKick m_FovKick = new FOVKick();

		// Token: 0x04000341 RID: 833
		[SerializeField]
		private bool m_UseHeadBob;

		// Token: 0x04000342 RID: 834
		[SerializeField]
		private CurveControlledBob m_HeadBob = new CurveControlledBob();

		// Token: 0x04000343 RID: 835
		[SerializeField]
		private LerpControlledBob m_JumpBob = new LerpControlledBob();

		// Token: 0x04000344 RID: 836
		[SerializeField]
		private float m_StepInterval;

		// Token: 0x04000345 RID: 837
		[SerializeField]
		private AudioClip[] m_FootstepSounds;

		// Token: 0x04000346 RID: 838
		[SerializeField]
		private AudioClip m_JumpSound;

		// Token: 0x04000347 RID: 839
		[SerializeField]
		private AudioClip m_LandSound;

		// Token: 0x04000348 RID: 840
		private Camera m_Camera;

		// Token: 0x04000349 RID: 841
		private bool m_Jump;

		// Token: 0x0400034A RID: 842
		private float m_YRotation;

		// Token: 0x0400034B RID: 843
		private Vector2 m_Input;

		// Token: 0x0400034C RID: 844
		private Vector3 m_MoveDir = Vector3.zero;

		// Token: 0x0400034D RID: 845
		private CharacterController m_CharacterController;

		// Token: 0x0400034E RID: 846
		private CollisionFlags m_CollisionFlags;

		// Token: 0x0400034F RID: 847
		private bool m_PreviouslyGrounded;

		// Token: 0x04000350 RID: 848
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x04000351 RID: 849
		private float m_StepCycle;

		// Token: 0x04000352 RID: 850
		private float m_NextStep;

		// Token: 0x04000353 RID: 851
		private bool m_Jumping;

		// Token: 0x04000354 RID: 852
		private AudioSource m_AudioSource;
	}
}

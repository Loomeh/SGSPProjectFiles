using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x0200009D RID: 157
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class ThirdPersonUserControl : MonoBehaviour
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000EB13 File Offset: 0x0000CD13
		private void Start()
		{
			if (Camera.main != null)
			{
				this.m_Cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", base.gameObject);
			}
			this.m_Character = base.GetComponent<ThirdPersonCharacter>();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EB50 File Offset: 0x0000CD50
		private void Update()
		{
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool key = Input.GetKey(KeyCode.C);
			if (this.m_Cam != null)
			{
				this.m_CamForward = Vector3.Scale(this.m_Cam.forward, new Vector3(1f, 0f, 1f)).normalized;
				this.m_Move = axis2 * this.m_CamForward + axis * this.m_Cam.right;
			}
			else
			{
				this.m_Move = axis2 * Vector3.forward + axis * Vector3.right;
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				this.m_Move *= 0.5f;
			}
			this.m_Character.Move(this.m_Move, key, this.m_Jump);
			this.m_Jump = false;
		}

		// Token: 0x04000332 RID: 818
		private ThirdPersonCharacter m_Character;

		// Token: 0x04000333 RID: 819
		private Transform m_Cam;

		// Token: 0x04000334 RID: 820
		private Vector3 m_CamForward;

		// Token: 0x04000335 RID: 821
		private Vector3 m_Move;

		// Token: 0x04000336 RID: 822
		private bool m_Jump;
	}
}

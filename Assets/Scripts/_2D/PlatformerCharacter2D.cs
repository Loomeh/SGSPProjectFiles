using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x020000AD RID: 173
	public class PlatformerCharacter2D : MonoBehaviour
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		private void Awake()
		{
			this.m_GroundCheck = base.transform.Find("GroundCheck");
			this.m_CeilingCheck = base.transform.Find("CeilingCheck");
			this.m_Anim = base.GetComponent<Animator>();
			this.m_Rigidbody2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010F34 File Offset: 0x0000F134
		private void FixedUpdate()
		{
			this.m_Grounded = false;
			Collider2D[] array = Physics2D.OverlapCircleAll(this.m_GroundCheck.position, 0.2f, this.m_WhatIsGround);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].gameObject != base.gameObject)
				{
					this.m_Grounded = true;
				}
			}
			this.m_Anim.SetBool("Ground", this.m_Grounded);
			this.m_Anim.SetFloat("vSpeed", this.m_Rigidbody2D.velocity.y);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		public void Move(float move, bool crouch, bool jump)
		{
			if (!crouch && this.m_Anim.GetBool("Crouch") && Physics2D.OverlapCircle(this.m_CeilingCheck.position, 0.01f, this.m_WhatIsGround))
			{
				crouch = true;
			}
			this.m_Anim.SetBool("Crouch", crouch);
			if (this.m_Grounded || this.m_AirControl)
			{
				move = (crouch ? (move * this.m_CrouchSpeed) : move);
				this.m_Anim.SetFloat("Speed", Mathf.Abs(move));
				this.m_Rigidbody2D.velocity = new Vector2(move * this.m_MaxSpeed, this.m_Rigidbody2D.velocity.y);
				if (move > 0f && !this.m_FacingRight)
				{
					this.Flip();
				}
				else if (move < 0f && this.m_FacingRight)
				{
					this.Flip();
				}
			}
			if (this.m_Grounded && jump && this.m_Anim.GetBool("Ground"))
			{
				this.m_Grounded = false;
				this.m_Anim.SetBool("Ground", false);
				this.m_Rigidbody2D.AddForce(new Vector2(0f, this.m_JumpForce));
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011110 File Offset: 0x0000F310
		private void Flip()
		{
			this.m_FacingRight = !this.m_FacingRight;
			Vector3 localScale = base.transform.localScale;
			localScale.x *= -1f;
			base.transform.localScale = localScale;
		}

		// Token: 0x040003C5 RID: 965
		[SerializeField]
		private float m_MaxSpeed = 10f;

		// Token: 0x040003C6 RID: 966
		[SerializeField]
		private float m_JumpForce = 400f;

		// Token: 0x040003C7 RID: 967
		[Range(0f, 1f)]
		[SerializeField]
		private float m_CrouchSpeed = 0.36f;

		// Token: 0x040003C8 RID: 968
		[SerializeField]
		private bool m_AirControl;

		// Token: 0x040003C9 RID: 969
		[SerializeField]
		private LayerMask m_WhatIsGround;

		// Token: 0x040003CA RID: 970
		private Transform m_GroundCheck;

		// Token: 0x040003CB RID: 971
		private const float k_GroundedRadius = 0.2f;

		// Token: 0x040003CC RID: 972
		private bool m_Grounded;

		// Token: 0x040003CD RID: 973
		private Transform m_CeilingCheck;

		// Token: 0x040003CE RID: 974
		private const float k_CeilingRadius = 0.01f;

		// Token: 0x040003CF RID: 975
		private Animator m_Anim;

		// Token: 0x040003D0 RID: 976
		private Rigidbody2D m_Rigidbody2D;

		// Token: 0x040003D1 RID: 977
		private bool m_FacingRight = true;
	}
}

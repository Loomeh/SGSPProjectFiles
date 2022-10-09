using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000067 RID: 103
	public class SimpleMouseRotator : MonoBehaviour
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000085BF File Offset: 0x000067BF
		private void Start()
		{
			this.m_OriginalRotation = base.transform.localRotation;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000085D4 File Offset: 0x000067D4
		private void Update()
		{
			base.transform.localRotation = this.m_OriginalRotation;
			if (this.relative)
			{
				float num = CrossPlatformInputManager.GetAxis("Mouse X");
				float num2 = CrossPlatformInputManager.GetAxis("Mouse Y");
				if (this.m_TargetAngles.y > 180f)
				{
					this.m_TargetAngles.y = this.m_TargetAngles.y - 360f;
					this.m_FollowAngles.y = this.m_FollowAngles.y - 360f;
				}
				if (this.m_TargetAngles.x > 180f)
				{
					this.m_TargetAngles.x = this.m_TargetAngles.x - 360f;
					this.m_FollowAngles.x = this.m_FollowAngles.x - 360f;
				}
				if (this.m_TargetAngles.y < -180f)
				{
					this.m_TargetAngles.y = this.m_TargetAngles.y + 360f;
					this.m_FollowAngles.y = this.m_FollowAngles.y + 360f;
				}
				if (this.m_TargetAngles.x < -180f)
				{
					this.m_TargetAngles.x = this.m_TargetAngles.x + 360f;
					this.m_FollowAngles.x = this.m_FollowAngles.x + 360f;
				}
				this.m_TargetAngles.y = this.m_TargetAngles.y + num * this.rotationSpeed;
				this.m_TargetAngles.x = this.m_TargetAngles.x + num2 * this.rotationSpeed;
				this.m_TargetAngles.y = Mathf.Clamp(this.m_TargetAngles.y, -this.rotationRange.y * 0.5f, this.rotationRange.y * 0.5f);
				this.m_TargetAngles.x = Mathf.Clamp(this.m_TargetAngles.x, -this.rotationRange.x * 0.5f, this.rotationRange.x * 0.5f);
			}
			else
			{
				float num = Input.mousePosition.x;
				float num2 = Input.mousePosition.y;
				this.m_TargetAngles.y = Mathf.Lerp(-this.rotationRange.y * 0.5f, this.rotationRange.y * 0.5f, num / (float)Screen.width);
				this.m_TargetAngles.x = Mathf.Lerp(-this.rotationRange.x * 0.5f, this.rotationRange.x * 0.5f, num2 / (float)Screen.height);
			}
			this.m_FollowAngles = Vector3.SmoothDamp(this.m_FollowAngles, this.m_TargetAngles, ref this.m_FollowVelocity, this.dampingTime);
			base.transform.localRotation = this.m_OriginalRotation * Quaternion.Euler(-this.m_FollowAngles.x, this.m_FollowAngles.y, 0f);
		}

		// Token: 0x040001BF RID: 447
		public Vector2 rotationRange = new Vector3(70f, 70f);

		// Token: 0x040001C0 RID: 448
		public float rotationSpeed = 10f;

		// Token: 0x040001C1 RID: 449
		public float dampingTime = 0.2f;

		// Token: 0x040001C2 RID: 450
		public bool autoZeroVerticalOnMobile = true;

		// Token: 0x040001C3 RID: 451
		public bool autoZeroHorizontalOnMobile;

		// Token: 0x040001C4 RID: 452
		public bool relative = true;

		// Token: 0x040001C5 RID: 453
		private Vector3 m_TargetAngles;

		// Token: 0x040001C6 RID: 454
		private Vector3 m_FollowAngles;

		// Token: 0x040001C7 RID: 455
		private Vector3 m_FollowVelocity;

		// Token: 0x040001C8 RID: 456
		private Quaternion m_OriginalRotation;
	}
}

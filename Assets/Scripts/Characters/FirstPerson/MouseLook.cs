using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class MouseLook
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public void Init(Transform character, Transform camera)
		{
			this.m_CharacterTargetRot = character.localRotation;
			this.m_CameraTargetRot = camera.localRotation;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public void LookRotation(Transform character, Transform camera)
		{
			float y = CrossPlatformInputManager.GetAxis("Mouse X") * this.XSensitivity;
			float num = CrossPlatformInputManager.GetAxis("Mouse Y") * this.YSensitivity;
			this.m_CharacterTargetRot *= Quaternion.Euler(0f, y, 0f);
			this.m_CameraTargetRot *= Quaternion.Euler(-num, 0f, 0f);
			if (this.clampVerticalRotation)
			{
				this.m_CameraTargetRot = this.ClampRotationAroundXAxis(this.m_CameraTargetRot);
			}
			if (this.smooth)
			{
				character.localRotation = Quaternion.Slerp(character.localRotation, this.m_CharacterTargetRot, this.smoothTime * Time.deltaTime);
				camera.localRotation = Quaternion.Slerp(camera.localRotation, this.m_CameraTargetRot, this.smoothTime * Time.deltaTime);
			}
			else
			{
				character.localRotation = this.m_CharacterTargetRot;
				camera.localRotation = this.m_CameraTargetRot;
			}
			this.UpdateCursorLock();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public void SetCursorLock(bool value)
		{
			this.lockCursor = value;
			if (!this.lockCursor)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F5F5 File Offset: 0x0000D7F5
		public void UpdateCursorLock()
		{
			if (this.lockCursor)
			{
				this.InternalLockUpdate();
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000F608 File Offset: 0x0000D808
		private void InternalLockUpdate()
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				this.m_cursorIsLocked = false;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				this.m_cursorIsLocked = true;
			}
			if (this.m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				return;
			}
			if (!this.m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F660 File Offset: 0x0000D860
		private Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1f;
			float num = 114.59156f * Mathf.Atan(q.x);
			num = Mathf.Clamp(num, this.MinimumX, this.MaximumX);
			q.x = Mathf.Tan(0.008726646f * num);
			return q;
		}

		// Token: 0x0400035D RID: 861
		public float XSensitivity = 2f;

		// Token: 0x0400035E RID: 862
		public float YSensitivity = 2f;

		// Token: 0x0400035F RID: 863
		public bool clampVerticalRotation = true;

		// Token: 0x04000360 RID: 864
		public float MinimumX = -90f;

		// Token: 0x04000361 RID: 865
		public float MaximumX = 90f;

		// Token: 0x04000362 RID: 866
		public bool smooth;

		// Token: 0x04000363 RID: 867
		public float smoothTime = 5f;

		// Token: 0x04000364 RID: 868
		public bool lockCursor = true;

		// Token: 0x04000365 RID: 869
		private Quaternion m_CharacterTargetRot;

		// Token: 0x04000366 RID: 870
		private Quaternion m_CameraTargetRot;

		// Token: 0x04000367 RID: 871
		private bool m_cursorIsLocked = true;
	}
}

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A4 RID: 164
	public class FreeLookCam : PivotBasedCameraRig
	{
		// Token: 0x0600032D RID: 813 RVA: 0x000100C4 File Offset: 0x0000E2C4
		protected override void Awake()
		{
			base.Awake();
			Cursor.lockState = (this.m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None);
			Cursor.visible = !this.m_LockCursor;
			this.m_PivotEulers = this.m_Pivot.rotation.eulerAngles;
			this.m_PivotTargetRot = this.m_Pivot.transform.localRotation;
			this.m_TransformTargetRot = base.transform.localRotation;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010136 File Offset: 0x0000E336
		protected void Update()
		{
			this.HandleRotationMovement();
			if (this.m_LockCursor && Input.GetMouseButtonUp(0))
			{
				Cursor.lockState = (this.m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None);
				Cursor.visible = !this.m_LockCursor;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001016D File Offset: 0x0000E36D
		private void OnDisable()
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001017B File Offset: 0x0000E37B
		protected override void FollowTarget(float deltaTime)
		{
			if (this.m_Target == null)
			{
				return;
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.m_Target.position, deltaTime * this.m_MoveSpeed);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000101BC File Offset: 0x0000E3BC
		private void HandleRotationMovement()
		{
			if (Time.timeScale < 1E-45f)
			{
				return;
			}
			float axis = CrossPlatformInputManager.GetAxis("Mouse X");
			float axis2 = CrossPlatformInputManager.GetAxis("Mouse Y");
			this.m_LookAngle += axis * this.m_TurnSpeed;
			this.m_TransformTargetRot = Quaternion.Euler(0f, this.m_LookAngle, 0f);
			if (this.m_VerticalAutoReturn)
			{
				this.m_TiltAngle = ((axis2 > 0f) ? Mathf.Lerp(0f, -this.m_TiltMin, axis2) : Mathf.Lerp(0f, this.m_TiltMax, -axis2));
			}
			else
			{
				this.m_TiltAngle -= axis2 * this.m_TurnSpeed;
				this.m_TiltAngle = Mathf.Clamp(this.m_TiltAngle, -this.m_TiltMin, this.m_TiltMax);
			}
			this.m_PivotTargetRot = Quaternion.Euler(this.m_TiltAngle, this.m_PivotEulers.y, this.m_PivotEulers.z);
			if (this.m_TurnSmoothing > 0f)
			{
				this.m_Pivot.localRotation = Quaternion.Slerp(this.m_Pivot.localRotation, this.m_PivotTargetRot, this.m_TurnSmoothing * Time.deltaTime);
				base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, this.m_TransformTargetRot, this.m_TurnSmoothing * Time.deltaTime);
				return;
			}
			this.m_Pivot.localRotation = this.m_PivotTargetRot;
			base.transform.localRotation = this.m_TransformTargetRot;
		}

		// Token: 0x04000384 RID: 900
		[SerializeField]
		private float m_MoveSpeed = 1f;

		// Token: 0x04000385 RID: 901
		[Range(0f, 10f)]
		[SerializeField]
		private float m_TurnSpeed = 1.5f;

		// Token: 0x04000386 RID: 902
		[SerializeField]
		private float m_TurnSmoothing;

		// Token: 0x04000387 RID: 903
		[SerializeField]
		private float m_TiltMax = 75f;

		// Token: 0x04000388 RID: 904
		[SerializeField]
		private float m_TiltMin = 45f;

		// Token: 0x04000389 RID: 905
		[SerializeField]
		private bool m_LockCursor;

		// Token: 0x0400038A RID: 906
		[SerializeField]
		private bool m_VerticalAutoReturn;

		// Token: 0x0400038B RID: 907
		private float m_LookAngle;

		// Token: 0x0400038C RID: 908
		private float m_TiltAngle;

		// Token: 0x0400038D RID: 909
		private const float k_LookDistance = 100f;

		// Token: 0x0400038E RID: 910
		private Vector3 m_PivotEulers;

		// Token: 0x0400038F RID: 911
		private Quaternion m_PivotTargetRot;

		// Token: 0x04000390 RID: 912
		private Quaternion m_TransformTargetRot;
	}
}

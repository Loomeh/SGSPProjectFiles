using System;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x0200009F RID: 159
	public class HeadBob : MonoBehaviour
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000F33B File Offset: 0x0000D53B
		private void Start()
		{
			this.motionBob.Setup(this.Camera, this.StrideInterval);
			this.m_OriginalCameraPosition = this.Camera.transform.localPosition;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F36C File Offset: 0x0000D56C
		private void Update()
		{
			Vector3 localPosition;
			if (this.rigidbodyFirstPersonController.Velocity.magnitude > 0f && this.rigidbodyFirstPersonController.Grounded)
			{
				this.Camera.transform.localPosition = this.motionBob.DoHeadBob(this.rigidbodyFirstPersonController.Velocity.magnitude * (this.rigidbodyFirstPersonController.Running ? this.RunningStrideLengthen : 1f));
				localPosition = this.Camera.transform.localPosition;
				localPosition.y = this.Camera.transform.localPosition.y - this.jumpAndLandingBob.Offset();
			}
			else
			{
				localPosition = this.Camera.transform.localPosition;
				localPosition.y = this.m_OriginalCameraPosition.y - this.jumpAndLandingBob.Offset();
			}
			this.Camera.transform.localPosition = localPosition;
			if (!this.m_PreviouslyGrounded && this.rigidbodyFirstPersonController.Grounded)
			{
				base.StartCoroutine(this.jumpAndLandingBob.DoBobCycle());
			}
			this.m_PreviouslyGrounded = this.rigidbodyFirstPersonController.Grounded;
		}

		// Token: 0x04000355 RID: 853
		public Camera Camera;

		// Token: 0x04000356 RID: 854
		public CurveControlledBob motionBob = new CurveControlledBob();

		// Token: 0x04000357 RID: 855
		public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();

		// Token: 0x04000358 RID: 856
		public RigidbodyFirstPersonController rigidbodyFirstPersonController;

		// Token: 0x04000359 RID: 857
		public float StrideInterval;

		// Token: 0x0400035A RID: 858
		[Range(0f, 1f)]
		public float RunningStrideLengthen;

		// Token: 0x0400035B RID: 859
		private bool m_PreviouslyGrounded;

		// Token: 0x0400035C RID: 860
		private Vector3 m_OriginalCameraPosition;
	}
}

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
	// Token: 0x0200009A RID: 154
	public class BallUserControl : MonoBehaviour
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		private void Awake()
		{
			this.ball = base.GetComponent<Ball>();
			if (Camera.main != null)
			{
				this.cam = Camera.main.transform;
				return;
			}
			Debug.LogWarning("Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E30C File Offset: 0x0000C50C
		private void Update()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			this.jump = CrossPlatformInputManager.GetButton("Jump");
			if (this.cam != null)
			{
				this.camForward = Vector3.Scale(this.cam.forward, new Vector3(1f, 0f, 1f)).normalized;
				this.move = (axis2 * this.camForward + axis * this.cam.right).normalized;
				return;
			}
			this.move = (axis2 * Vector3.forward + axis * Vector3.right).normalized;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
		private void FixedUpdate()
		{
			this.ball.Move(this.move, this.jump);
			this.jump = false;
		}

		// Token: 0x04000316 RID: 790
		private Ball ball;

		// Token: 0x04000317 RID: 791
		private Vector3 move;

		// Token: 0x04000318 RID: 792
		private Transform cam;

		// Token: 0x04000319 RID: 793
		private Vector3 camForward;

		// Token: 0x0400031A RID: 794
		private bool jump;
	}
}

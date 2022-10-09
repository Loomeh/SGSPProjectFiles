using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class FOVKick
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000081EB File Offset: 0x000063EB
		public void Setup(Camera camera)
		{
			this.CheckStatus(camera);
			this.Camera = camera;
			this.originalFov = camera.fieldOfView;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008207 File Offset: 0x00006407
		private void CheckStatus(Camera camera)
		{
			if (camera == null)
			{
				throw new Exception("FOVKick camera is null, please supply the camera to the constructor");
			}
			if (this.IncreaseCurve == null)
			{
				throw new Exception("FOVKick Increase curve is null, please define the curve for the field of view kicks");
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008230 File Offset: 0x00006430
		public void ChangeCamera(Camera camera)
		{
			this.Camera = camera;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008239 File Offset: 0x00006439
		public IEnumerator FOVKickUp()
		{
			float t = Mathf.Abs((this.Camera.fieldOfView - this.originalFov) / this.FOVIncrease);
			while (t < this.TimeToIncrease)
			{
				this.Camera.fieldOfView = this.originalFov + this.IncreaseCurve.Evaluate(t / this.TimeToIncrease) * this.FOVIncrease;
				t += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008248 File Offset: 0x00006448
		public IEnumerator FOVKickDown()
		{
			float t = Mathf.Abs((this.Camera.fieldOfView - this.originalFov) / this.FOVIncrease);
			while (t > 0f)
			{
				this.Camera.fieldOfView = this.originalFov + this.IncreaseCurve.Evaluate(t / this.TimeToDecrease) * this.FOVIncrease;
				t -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			this.Camera.fieldOfView = this.originalFov;
			yield break;
		}

		// Token: 0x0400019F RID: 415
		public Camera Camera;

		// Token: 0x040001A0 RID: 416
		[HideInInspector]
		public float originalFov;

		// Token: 0x040001A1 RID: 417
		public float FOVIncrease = 3f;

		// Token: 0x040001A2 RID: 418
		public float TimeToIncrease = 1f;

		// Token: 0x040001A3 RID: 419
		public float TimeToDecrease = 1f;

		// Token: 0x040001A4 RID: 420
		public AnimationCurve IncreaseCurve;
	}
}

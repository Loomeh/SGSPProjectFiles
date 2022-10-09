using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class CurveControlledBob
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00007D34 File Offset: 0x00005F34
		public void Setup(Camera camera, float bobBaseInterval)
		{
			this.m_BobBaseInterval = bobBaseInterval;
			this.m_OriginalCameraPosition = camera.transform.localPosition;
			this.m_Time = this.Bobcurve[this.Bobcurve.length - 1].time;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007D80 File Offset: 0x00005F80
		public Vector3 DoHeadBob(float speed)
		{
			float x = this.m_OriginalCameraPosition.x + this.Bobcurve.Evaluate(this.m_CyclePositionX) * this.HorizontalBobRange;
			float y = this.m_OriginalCameraPosition.y + this.Bobcurve.Evaluate(this.m_CyclePositionY) * this.VerticalBobRange;
			this.m_CyclePositionX += speed * Time.deltaTime / this.m_BobBaseInterval;
			this.m_CyclePositionY += speed * Time.deltaTime / this.m_BobBaseInterval * this.VerticaltoHorizontalRatio;
			if (this.m_CyclePositionX > this.m_Time)
			{
				this.m_CyclePositionX -= this.m_Time;
			}
			if (this.m_CyclePositionY > this.m_Time)
			{
				this.m_CyclePositionY -= this.m_Time;
			}
			return new Vector3(x, y, 0f);
		}

		// Token: 0x04000184 RID: 388
		public float HorizontalBobRange = 0.33f;

		// Token: 0x04000185 RID: 389
		public float VerticalBobRange = 0.33f;

		// Token: 0x04000186 RID: 390
		public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(0.5f, 1f),
			new Keyframe(1f, 0f),
			new Keyframe(1.5f, -1f),
			new Keyframe(2f, 0f)
		});

		// Token: 0x04000187 RID: 391
		public float VerticaltoHorizontalRatio = 1f;

		// Token: 0x04000188 RID: 392
		private float m_CyclePositionX;

		// Token: 0x04000189 RID: 393
		private float m_CyclePositionY;

		// Token: 0x0400018A RID: 394
		private float m_BobBaseInterval;

		// Token: 0x0400018B RID: 395
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x0400018C RID: 396
		private float m_Time;
	}
}

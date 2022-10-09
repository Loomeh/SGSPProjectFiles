﻿using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A5 RID: 165
	public class HandHeldCam : LookatTarget
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00010374 File Offset: 0x0000E574
		protected override void FollowTarget(float deltaTime)
		{
			base.FollowTarget(deltaTime);
			float num = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed) - 0.5f;
			float num2 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed + 100f) - 0.5f;
			num *= this.m_BaseSwayAmount;
			num2 *= this.m_BaseSwayAmount;
			float num3 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed) - 0.5f + this.m_TrackingBias;
			float num4 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed + 100f) - 0.5f + this.m_TrackingBias;
			num3 *= -this.m_TrackingSwayAmount * this.m_FollowVelocity.x;
			num4 *= this.m_TrackingSwayAmount * this.m_FollowVelocity.y;
			base.transform.Rotate(num + num3, num2 + num4, 0f);
		}

		// Token: 0x04000391 RID: 913
		[SerializeField]
		private float m_SwaySpeed = 0.5f;

		// Token: 0x04000392 RID: 914
		[SerializeField]
		private float m_BaseSwayAmount = 0.5f;

		// Token: 0x04000393 RID: 915
		[SerializeField]
		private float m_TrackingSwayAmount = 0.5f;

		// Token: 0x04000394 RID: 916
		[Range(-1f, 1f)]
		[SerializeField]
		private float m_TrackingBias;
	}
}

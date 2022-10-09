using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A9 RID: 169
	public class TargetFieldOfView : AbstractTargetFollower
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000109E9 File Offset: 0x0000EBE9
		protected override void Start()
		{
			base.Start();
			this.m_BoundSize = TargetFieldOfView.MaxBoundsExtent(this.m_Target, this.m_IncludeEffectsInSize);
			this.m_Cam = base.GetComponentInChildren<Camera>();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010A14 File Offset: 0x0000EC14
		protected override void FollowTarget(float deltaTime)
		{
			float magnitude = (this.m_Target.position - base.transform.position).magnitude;
			float target = Mathf.Atan2(this.m_BoundSize, magnitude) * 57.29578f * this.m_ZoomAmountMultiplier;
			this.m_Cam.fieldOfView = Mathf.SmoothDamp(this.m_Cam.fieldOfView, target, ref this.m_FovAdjustVelocity, this.m_FovAdjustTime);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010A87 File Offset: 0x0000EC87
		public override void SetTarget(Transform newTransform)
		{
			base.SetTarget(newTransform);
			this.m_BoundSize = TargetFieldOfView.MaxBoundsExtent(newTransform, this.m_IncludeEffectsInSize);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		public static float MaxBoundsExtent(Transform obj, bool includeEffects)
		{
			Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>();
			Bounds bounds = default(Bounds);
			bool flag = false;
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer is TrailRenderer) && !(renderer is ParticleSystemRenderer))
				{
					if (!flag)
					{
						flag = true;
						bounds = renderer.bounds;
					}
					else
					{
						bounds.Encapsulate(renderer.bounds);
					}
				}
			}
			return Mathf.Max(new float[]
			{
				bounds.extents.x,
				bounds.extents.y,
				bounds.extents.z
			});
		}

		// Token: 0x040003AC RID: 940
		[SerializeField]
		private float m_FovAdjustTime = 1f;

		// Token: 0x040003AD RID: 941
		[SerializeField]
		private float m_ZoomAmountMultiplier = 2f;

		// Token: 0x040003AE RID: 942
		[SerializeField]
		private bool m_IncludeEffectsInSize;

		// Token: 0x040003AF RID: 943
		private float m_BoundSize;

		// Token: 0x040003B0 RID: 944
		private float m_FovAdjustVelocity;

		// Token: 0x040003B1 RID: 945
		private Camera m_Cam;

		// Token: 0x040003B2 RID: 946
		private Transform m_LastTarget;
	}
}

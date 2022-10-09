using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000092 RID: 146
	public class AeroplaneControlSurfaceAnimator : MonoBehaviour
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000D250 File Offset: 0x0000B450
		private void Start()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			foreach (AeroplaneControlSurfaceAnimator.ControlSurface controlSurface in this.m_ControlSurfaces)
			{
				controlSurface.originalLocalRotation = controlSurface.transform.localRotation;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D294 File Offset: 0x0000B494
		private void Update()
		{
			foreach (AeroplaneControlSurfaceAnimator.ControlSurface controlSurface in this.m_ControlSurfaces)
			{
				switch (controlSurface.type)
				{
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Aileron:
				{
					Quaternion rotation = Quaternion.Euler(controlSurface.amount * this.m_Plane.RollInput, 0f, 0f);
					this.RotateSurface(controlSurface, rotation);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Elevator:
				{
					Quaternion rotation2 = Quaternion.Euler(controlSurface.amount * -this.m_Plane.PitchInput, 0f, 0f);
					this.RotateSurface(controlSurface, rotation2);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Rudder:
				{
					Quaternion rotation3 = Quaternion.Euler(0f, controlSurface.amount * this.m_Plane.YawInput, 0f);
					this.RotateSurface(controlSurface, rotation3);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.RuddervatorNegative:
				{
					float num = this.m_Plane.YawInput - this.m_Plane.PitchInput;
					Quaternion rotation4 = Quaternion.Euler(0f, 0f, controlSurface.amount * num);
					this.RotateSurface(controlSurface, rotation4);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.RuddervatorPositive:
				{
					float num2 = this.m_Plane.YawInput + this.m_Plane.PitchInput;
					Quaternion rotation5 = Quaternion.Euler(0f, 0f, controlSurface.amount * num2);
					this.RotateSurface(controlSurface, rotation5);
					break;
				}
				}
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		private void RotateSurface(AeroplaneControlSurfaceAnimator.ControlSurface surface, Quaternion rotation)
		{
			Quaternion b = surface.originalLocalRotation * rotation;
			surface.transform.localRotation = Quaternion.Slerp(surface.transform.localRotation, b, this.m_Smoothing * Time.deltaTime);
		}

		// Token: 0x040002CD RID: 717
		[SerializeField]
		private float m_Smoothing = 5f;

		// Token: 0x040002CE RID: 718
		[SerializeField]
		private AeroplaneControlSurfaceAnimator.ControlSurface[] m_ControlSurfaces;

		// Token: 0x040002CF RID: 719
		private AeroplaneController m_Plane;

		// Token: 0x020000ED RID: 237
		[Serializable]
		public class ControlSurface
		{
			// Token: 0x040004AD RID: 1197
			public Transform transform;

			// Token: 0x040004AE RID: 1198
			public float amount;

			// Token: 0x040004AF RID: 1199
			public AeroplaneControlSurfaceAnimator.ControlSurface.Type type;

			// Token: 0x040004B0 RID: 1200
			[HideInInspector]
			public Quaternion originalLocalRotation;

			// Token: 0x020000F4 RID: 244
			public enum Type
			{
				// Token: 0x040004CC RID: 1228
				Aileron,
				// Token: 0x040004CD RID: 1229
				Elevator,
				// Token: 0x040004CE RID: 1230
				Rudder,
				// Token: 0x040004CF RID: 1231
				RuddervatorNegative,
				// Token: 0x040004D0 RID: 1232
				RuddervatorPositive
			}
		}
	}
}

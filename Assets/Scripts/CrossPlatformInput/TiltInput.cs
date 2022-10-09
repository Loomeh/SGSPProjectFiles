using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007F RID: 127
	public class TiltInput : MonoBehaviour
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000AF90 File Offset: 0x00009190
		private void OnEnable()
		{
			if (this.mapping.type == TiltInput.AxisMapping.MappingType.NamedAxis)
			{
				this.m_SteerAxis = new CrossPlatformInputManager.VirtualAxis(this.mapping.axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_SteerAxis);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AFC0 File Offset: 0x000091C0
		private void Update()
		{
			float value = 0f;
			if (Input.acceleration != Vector3.zero)
			{
				TiltInput.AxisOptions axisOptions = this.tiltAroundAxis;
				if (axisOptions != TiltInput.AxisOptions.ForwardAxis)
				{
					if (axisOptions == TiltInput.AxisOptions.SidewaysAxis)
					{
						value = Mathf.Atan2(Input.acceleration.z, -Input.acceleration.y) * 57.29578f + this.centreAngleOffset;
					}
				}
				else
				{
					value = Mathf.Atan2(Input.acceleration.x, -Input.acceleration.y) * 57.29578f + this.centreAngleOffset;
				}
			}
			float num = Mathf.InverseLerp(-this.fullTiltAngle, this.fullTiltAngle, value) * 2f - 1f;
			switch (this.mapping.type)
			{
			case TiltInput.AxisMapping.MappingType.NamedAxis:
				this.m_SteerAxis.Update(num);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionX:
				CrossPlatformInputManager.SetVirtualMousePositionX(num * (float)Screen.width);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionY:
				CrossPlatformInputManager.SetVirtualMousePositionY(num * (float)Screen.width);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionZ:
				CrossPlatformInputManager.SetVirtualMousePositionZ(num * (float)Screen.width);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B0BF File Offset: 0x000092BF
		private void OnDisable()
		{
			this.m_SteerAxis.Remove();
		}

		// Token: 0x04000235 RID: 565
		public TiltInput.AxisMapping mapping;

		// Token: 0x04000236 RID: 566
		public TiltInput.AxisOptions tiltAroundAxis;

		// Token: 0x04000237 RID: 567
		public float fullTiltAngle = 25f;

		// Token: 0x04000238 RID: 568
		public float centreAngleOffset;

		// Token: 0x04000239 RID: 569
		private CrossPlatformInputManager.VirtualAxis m_SteerAxis;

		// Token: 0x020000E4 RID: 228
		public enum AxisOptions
		{
			// Token: 0x0400048C RID: 1164
			ForwardAxis,
			// Token: 0x0400048D RID: 1165
			SidewaysAxis
		}

		// Token: 0x020000E5 RID: 229
		[Serializable]
		public class AxisMapping
		{
			// Token: 0x0400048E RID: 1166
			public TiltInput.AxisMapping.MappingType type;

			// Token: 0x0400048F RID: 1167
			public string axisName;

			// Token: 0x020000F3 RID: 243
			public enum MappingType
			{
				// Token: 0x040004C7 RID: 1223
				NamedAxis,
				// Token: 0x040004C8 RID: 1224
				MousePositionX,
				// Token: 0x040004C9 RID: 1225
				MousePositionY,
				// Token: 0x040004CA RID: 1226
				MousePositionZ
			}
		}
	}
}

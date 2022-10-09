using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007D RID: 125
	public class Joystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000AC94 File Offset: 0x00008E94
		private void OnEnable()
		{
			this.CreateVirtualAxes();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AC9C File Offset: 0x00008E9C
		private void Start()
		{
			this.m_StartPos = base.transform.position;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		private void UpdateVirtualAxes(Vector3 value)
		{
			Vector3 vector = this.m_StartPos - value;
			vector.y = -vector.y;
			vector /= (float)this.MovementRange;
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Update(-vector.x);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Update(vector.y);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AD1C File Offset: 0x00008F1C
		private void CreateVirtualAxes()
		{
			this.m_UseX = (this.axesToUse == Joystick.AxisOption.Both || this.axesToUse == Joystick.AxisOption.OnlyHorizontal);
			this.m_UseY = (this.axesToUse == Joystick.AxisOption.Both || this.axesToUse == Joystick.AxisOption.OnlyVertical);
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_HorizontalVirtualAxis);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_VerticalVirtualAxis);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public void OnDrag(PointerEventData data)
		{
			Vector3 zero = Vector3.zero;
			if (this.m_UseX)
			{
				int num = (int)(data.position.x - this.m_StartPos.x);
				num = Mathf.Clamp(num, -this.MovementRange, this.MovementRange);
				zero.x = (float)num;
			}
			if (this.m_UseY)
			{
				int num2 = (int)(data.position.y - this.m_StartPos.y);
				num2 = Mathf.Clamp(num2, -this.MovementRange, this.MovementRange);
				zero.y = (float)num2;
			}
			base.transform.position = new Vector3(this.m_StartPos.x + zero.x, this.m_StartPos.y + zero.y, this.m_StartPos.z + zero.z);
			this.UpdateVirtualAxes(base.transform.position);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000AE8E File Offset: 0x0000908E
		public void OnPointerUp(PointerEventData data)
		{
			base.transform.position = this.m_StartPos;
			this.UpdateVirtualAxes(this.m_StartPos);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000255D File Offset: 0x0000075D
		public void OnPointerDown(PointerEventData data)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000AEAD File Offset: 0x000090AD
		private void OnDisable()
		{
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Remove();
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Remove();
			}
		}

		// Token: 0x0400022C RID: 556
		public int MovementRange = 100;

		// Token: 0x0400022D RID: 557
		public Joystick.AxisOption axesToUse;

		// Token: 0x0400022E RID: 558
		public string horizontalAxisName = "Horizontal";

		// Token: 0x0400022F RID: 559
		public string verticalAxisName = "Vertical";

		// Token: 0x04000230 RID: 560
		private Vector3 m_StartPos;

		// Token: 0x04000231 RID: 561
		private bool m_UseX;

		// Token: 0x04000232 RID: 562
		private bool m_UseY;

		// Token: 0x04000233 RID: 563
		private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

		// Token: 0x04000234 RID: 564
		private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

		// Token: 0x020000E3 RID: 227
		public enum AxisOption
		{
			// Token: 0x04000488 RID: 1160
			Both,
			// Token: 0x04000489 RID: 1161
			OnlyHorizontal,
			// Token: 0x0400048A RID: 1162
			OnlyVertical
		}
	}
}

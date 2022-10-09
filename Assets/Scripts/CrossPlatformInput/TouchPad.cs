using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000080 RID: 128
	[RequireComponent(typeof(Image))]
	public class TouchPad : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000B0DF File Offset: 0x000092DF
		private void OnEnable()
		{
			this.CreateVirtualAxes();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B0E7 File Offset: 0x000092E7
		private void Start()
		{
			this.m_Image = base.GetComponent<Image>();
			this.m_Center = this.m_Image.transform.position;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B10C File Offset: 0x0000930C
		private void CreateVirtualAxes()
		{
			this.m_UseX = (this.axesToUse == TouchPad.AxisOption.Both || this.axesToUse == TouchPad.AxisOption.OnlyHorizontal);
			this.m_UseY = (this.axesToUse == TouchPad.AxisOption.Both || this.axesToUse == TouchPad.AxisOption.OnlyVertical);
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

		// Token: 0x06000221 RID: 545 RVA: 0x0000B195 File Offset: 0x00009395
		private void UpdateVirtualAxes(Vector3 value)
		{
			value = value.normalized;
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Update(value.x);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Update(value.y);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B1D2 File Offset: 0x000093D2
		public void OnPointerDown(PointerEventData data)
		{
			this.m_Dragging = true;
			this.m_Id = data.pointerId;
			if (this.controlStyle != TouchPad.ControlStyle.Absolute)
			{
				this.m_Center = data.position;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B200 File Offset: 0x00009400
		private void Update()
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (Input.touchCount >= this.m_Id + 1 && this.m_Id != -1)
			{
				if (this.controlStyle == TouchPad.ControlStyle.Swipe)
				{
					this.m_Center = this.m_PreviousTouchPos;
					this.m_PreviousTouchPos = Input.touches[this.m_Id].position;
				}
				Vector2 normalized = new Vector2(Input.touches[this.m_Id].position.x - this.m_Center.x, Input.touches[this.m_Id].position.y - this.m_Center.y).normalized;
				normalized.x *= this.Xsensitivity;
				normalized.y *= this.Ysensitivity;
				this.UpdateVirtualAxes(new Vector3(normalized.x, normalized.y, 0f));
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000B301 File Offset: 0x00009501
		public void OnPointerUp(PointerEventData data)
		{
			this.m_Dragging = false;
			this.m_Id = -1;
			this.UpdateVirtualAxes(Vector3.zero);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B31C File Offset: 0x0000951C
		private void OnDisable()
		{
			if (CrossPlatformInputManager.AxisExists(this.horizontalAxisName))
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.horizontalAxisName);
			}
			if (CrossPlatformInputManager.AxisExists(this.verticalAxisName))
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.verticalAxisName);
			}
		}

		// Token: 0x0400023A RID: 570
		public TouchPad.AxisOption axesToUse;

		// Token: 0x0400023B RID: 571
		public TouchPad.ControlStyle controlStyle;

		// Token: 0x0400023C RID: 572
		public string horizontalAxisName = "Horizontal";

		// Token: 0x0400023D RID: 573
		public string verticalAxisName = "Vertical";

		// Token: 0x0400023E RID: 574
		public float Xsensitivity = 1f;

		// Token: 0x0400023F RID: 575
		public float Ysensitivity = 1f;

		// Token: 0x04000240 RID: 576
		private Vector3 m_StartPos;

		// Token: 0x04000241 RID: 577
		private Vector2 m_PreviousDelta;

		// Token: 0x04000242 RID: 578
		private Vector3 m_JoytickOutput;

		// Token: 0x04000243 RID: 579
		private bool m_UseX;

		// Token: 0x04000244 RID: 580
		private bool m_UseY;

		// Token: 0x04000245 RID: 581
		private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

		// Token: 0x04000246 RID: 582
		private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

		// Token: 0x04000247 RID: 583
		private bool m_Dragging;

		// Token: 0x04000248 RID: 584
		private int m_Id = -1;

		// Token: 0x04000249 RID: 585
		private Vector2 m_PreviousTouchPos;

		// Token: 0x0400024A RID: 586
		private Vector3 m_Center;

		// Token: 0x0400024B RID: 587
		private Image m_Image;

		// Token: 0x020000E6 RID: 230
		public enum AxisOption
		{
			// Token: 0x04000491 RID: 1169
			Both,
			// Token: 0x04000492 RID: 1170
			OnlyHorizontal,
			// Token: 0x04000493 RID: 1171
			OnlyVertical
		}

		// Token: 0x020000E7 RID: 231
		public enum ControlStyle
		{
			// Token: 0x04000495 RID: 1173
			Absolute,
			// Token: 0x04000496 RID: 1174
			Relative,
			// Token: 0x04000497 RID: 1175
			Swipe
		}
	}
}

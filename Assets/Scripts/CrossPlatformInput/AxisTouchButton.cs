using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000079 RID: 121
	public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000A95C File Offset: 0x00008B5C
		private void OnEnable()
		{
			if (!CrossPlatformInputManager.AxisExists(this.axisName))
			{
				this.m_Axis = new CrossPlatformInputManager.VirtualAxis(this.axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_Axis);
			}
			else
			{
				this.m_Axis = CrossPlatformInputManager.VirtualAxisReference(this.axisName);
			}
			this.FindPairedButton();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A9AC File Offset: 0x00008BAC
		private void FindPairedButton()
		{
			AxisTouchButton[] array = Object.FindObjectsOfType(typeof(AxisTouchButton)) as AxisTouchButton[];
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].axisName == this.axisName && array[i] != this)
					{
						this.m_PairedWith = array[i];
					}
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AA08 File Offset: 0x00008C08
		private void OnDisable()
		{
			this.m_Axis.Remove();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AA18 File Offset: 0x00008C18
		public void OnPointerDown(PointerEventData data)
		{
			if (this.m_PairedWith == null)
			{
				this.FindPairedButton();
			}
			this.m_Axis.Update(Mathf.MoveTowards(this.m_Axis.GetValue, this.axisValue, this.responseSpeed * Time.deltaTime));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AA66 File Offset: 0x00008C66
		public void OnPointerUp(PointerEventData data)
		{
			this.m_Axis.Update(Mathf.MoveTowards(this.m_Axis.GetValue, 0f, this.responseSpeed * Time.deltaTime));
		}

		// Token: 0x04000221 RID: 545
		public string axisName = "Horizontal";

		// Token: 0x04000222 RID: 546
		public float axisValue = 1f;

		// Token: 0x04000223 RID: 547
		public float responseSpeed = 3f;

		// Token: 0x04000224 RID: 548
		public float returnToCentreSpeed = 3f;

		// Token: 0x04000225 RID: 549
		private AxisTouchButton m_PairedWith;

		// Token: 0x04000226 RID: 550
		private CrossPlatformInputManager.VirtualAxis m_Axis;
	}
}

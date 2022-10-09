using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput.PlatformSpecific
{
	// Token: 0x02000083 RID: 131
	public class StandaloneInput : VirtualInput
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000B773 File Offset: 0x00009973
		public override float GetAxis(string name, bool raw)
		{
			if (!raw)
			{
				return Input.GetAxis(name);
			}
			return Input.GetAxisRaw(name);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B785 File Offset: 0x00009985
		public override bool GetButton(string name)
		{
			return Input.GetButton(name);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B78D File Offset: 0x0000998D
		public override bool GetButtonDown(string name)
		{
			return Input.GetButtonDown(name);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B795 File Offset: 0x00009995
		public override bool GetButtonUp(string name)
		{
			return Input.GetButtonUp(name);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetButtonDown(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetButtonUp(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetAxisPositive(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetAxisNegative(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetAxisZero(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B79D File Offset: 0x0000999D
		public override void SetAxis(string name, float value)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B7A9 File Offset: 0x000099A9
		public override Vector3 MousePosition()
		{
			return Input.mousePosition;
		}
	}
}

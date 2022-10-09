using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000081 RID: 129
	public abstract class VirtualInput
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B389 File Offset: 0x00009589
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000B391 File Offset: 0x00009591
		public Vector3 virtualMousePosition { get; private set; }

		// Token: 0x06000229 RID: 553 RVA: 0x0000B39A File Offset: 0x0000959A
		public bool AxisExists(string name)
		{
			return this.m_VirtualAxes.ContainsKey(name);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public bool ButtonExists(string name)
		{
			return this.m_VirtualButtons.ContainsKey(name);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis axis)
		{
			if (this.m_VirtualAxes.ContainsKey(axis.name))
			{
				Debug.LogError("There is already a virtual axis named " + axis.name + " registered.");
				return;
			}
			this.m_VirtualAxes.Add(axis.name, axis);
			if (!axis.matchWithInputManager)
			{
				this.m_AlwaysUseVirtual.Add(axis.name);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B420 File Offset: 0x00009620
		public void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton button)
		{
			if (this.m_VirtualButtons.ContainsKey(button.name))
			{
				Debug.LogError("There is already a virtual button named " + button.name + " registered.");
				return;
			}
			this.m_VirtualButtons.Add(button.name, button);
			if (!button.matchWithInputManager)
			{
				this.m_AlwaysUseVirtual.Add(button.name);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B486 File Offset: 0x00009686
		public void UnRegisterVirtualAxis(string name)
		{
			if (this.m_VirtualAxes.ContainsKey(name))
			{
				this.m_VirtualAxes.Remove(name);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B4A3 File Offset: 0x000096A3
		public void UnRegisterVirtualButton(string name)
		{
			if (this.m_VirtualButtons.ContainsKey(name))
			{
				this.m_VirtualButtons.Remove(name);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
		{
			if (!this.m_VirtualAxes.ContainsKey(name))
			{
				return null;
			}
			return this.m_VirtualAxes[name];
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B4DE File Offset: 0x000096DE
		public void SetVirtualMousePositionX(float f)
		{
			this.virtualMousePosition = new Vector3(f, this.virtualMousePosition.y, this.virtualMousePosition.z);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B502 File Offset: 0x00009702
		public void SetVirtualMousePositionY(float f)
		{
			this.virtualMousePosition = new Vector3(this.virtualMousePosition.x, f, this.virtualMousePosition.z);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B526 File Offset: 0x00009726
		public void SetVirtualMousePositionZ(float f)
		{
			this.virtualMousePosition = new Vector3(this.virtualMousePosition.x, this.virtualMousePosition.y, f);
		}

		// Token: 0x06000233 RID: 563
		public abstract float GetAxis(string name, bool raw);

		// Token: 0x06000234 RID: 564
		public abstract bool GetButton(string name);

		// Token: 0x06000235 RID: 565
		public abstract bool GetButtonDown(string name);

		// Token: 0x06000236 RID: 566
		public abstract bool GetButtonUp(string name);

		// Token: 0x06000237 RID: 567
		public abstract void SetButtonDown(string name);

		// Token: 0x06000238 RID: 568
		public abstract void SetButtonUp(string name);

		// Token: 0x06000239 RID: 569
		public abstract void SetAxisPositive(string name);

		// Token: 0x0600023A RID: 570
		public abstract void SetAxisNegative(string name);

		// Token: 0x0600023B RID: 571
		public abstract void SetAxisZero(string name);

		// Token: 0x0600023C RID: 572
		public abstract void SetAxis(string name, float value);

		// Token: 0x0600023D RID: 573
		public abstract Vector3 MousePosition();

		// Token: 0x0400024D RID: 589
		protected Dictionary<string, CrossPlatformInputManager.VirtualAxis> m_VirtualAxes = new Dictionary<string, CrossPlatformInputManager.VirtualAxis>();

		// Token: 0x0400024E RID: 590
		protected Dictionary<string, CrossPlatformInputManager.VirtualButton> m_VirtualButtons = new Dictionary<string, CrossPlatformInputManager.VirtualButton>();

		// Token: 0x0400024F RID: 591
		protected List<string> m_AlwaysUseVirtual = new List<string>();
	}
}

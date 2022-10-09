using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007A RID: 122
	public class ButtonHandler : MonoBehaviour
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000255D File Offset: 0x0000075D
		private void OnEnable()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public void SetDownState()
		{
			CrossPlatformInputManager.SetButtonDown(this.Name);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		public void SetUpState()
		{
			CrossPlatformInputManager.SetButtonUp(this.Name);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000AAE2 File Offset: 0x00008CE2
		public void SetAxisPositiveState()
		{
			CrossPlatformInputManager.SetAxisPositive(this.Name);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000AAEF File Offset: 0x00008CEF
		public void SetAxisNeutralState()
		{
			CrossPlatformInputManager.SetAxisZero(this.Name);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public void SetAxisNegativeState()
		{
			CrossPlatformInputManager.SetAxisNegative(this.Name);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000255D File Offset: 0x0000075D
		public void Update()
		{
		}

		// Token: 0x04000227 RID: 551
		public string Name;
	}
}

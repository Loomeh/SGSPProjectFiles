using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007C RID: 124
	public class InputAxisScrollbar : MonoBehaviour
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000255D File Offset: 0x0000075D
		private void Update()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AC7A File Offset: 0x00008E7A
		public void HandleInput(float value)
		{
			CrossPlatformInputManager.SetAxis(this.axis, value * 2f - 1f);
		}

		// Token: 0x0400022B RID: 555
		public string axis;
	}
}

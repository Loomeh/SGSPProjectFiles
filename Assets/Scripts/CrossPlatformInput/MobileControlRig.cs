using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007E RID: 126
	[ExecuteInEditMode]
	public class MobileControlRig : MonoBehaviour
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000AEFB File Offset: 0x000090FB
		private void OnEnable()
		{
			this.CheckEnableControlRig();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AF03 File Offset: 0x00009103
		private void Start()
		{
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				GameObject gameObject = new GameObject("EventSystem");
				gameObject.AddComponent<EventSystem>();
				gameObject.AddComponent<StandaloneInputModule>();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000AF29 File Offset: 0x00009129
		private void CheckEnableControlRig()
		{
			this.EnableControlRig(false);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000AF34 File Offset: 0x00009134
		private void EnableControlRig(bool enabled)
		{
			foreach (object obj in base.transform)
			{
				((Transform)obj).gameObject.SetActive(enabled);
			}
		}
	}
}

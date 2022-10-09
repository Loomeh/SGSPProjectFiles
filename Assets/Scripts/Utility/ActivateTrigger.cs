using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000058 RID: 88
	public class ActivateTrigger : MonoBehaviour
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00007AB4 File Offset: 0x00005CB4
		private void DoActivateTrigger()
		{
			this.triggerCount--;
			if (this.triggerCount == 0 || this.repeatTrigger)
			{
				Object @object = this.target ?? base.gameObject;
				Behaviour behaviour = @object as Behaviour;
				GameObject gameObject = @object as GameObject;
				if (behaviour != null)
				{
					gameObject = behaviour.gameObject;
				}
				switch (this.action)
				{
				case ActivateTrigger.Mode.Trigger:
					if (gameObject != null)
					{
						gameObject.BroadcastMessage("DoActivateTrigger");
						return;
					}
					break;
				case ActivateTrigger.Mode.Replace:
					if (this.source != null && gameObject != null)
					{
						Object.Instantiate<GameObject>(this.source, gameObject.transform.position, gameObject.transform.rotation);
						Object.Destroy(gameObject);
						return;
					}
					break;
				case ActivateTrigger.Mode.Activate:
					if (gameObject != null)
					{
						gameObject.SetActive(true);
						return;
					}
					break;
				case ActivateTrigger.Mode.Enable:
					if (behaviour != null)
					{
						behaviour.enabled = true;
						return;
					}
					break;
				case ActivateTrigger.Mode.Animate:
					if (gameObject != null)
					{
						gameObject.GetComponent<Animation>().Play();
						return;
					}
					break;
				case ActivateTrigger.Mode.Deactivate:
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007BD5 File Offset: 0x00005DD5
		private void OnTriggerEnter(Collider other)
		{
			this.DoActivateTrigger();
		}

		// Token: 0x04000175 RID: 373
		public ActivateTrigger.Mode action = ActivateTrigger.Mode.Activate;

		// Token: 0x04000176 RID: 374
		public Object target;

		// Token: 0x04000177 RID: 375
		public GameObject source;

		// Token: 0x04000178 RID: 376
		public int triggerCount = 1;

		// Token: 0x04000179 RID: 377
		public bool repeatTrigger;

		// Token: 0x020000C8 RID: 200
		public enum Mode
		{
			// Token: 0x04000429 RID: 1065
			Trigger,
			// Token: 0x0400042A RID: 1066
			Replace,
			// Token: 0x0400042B RID: 1067
			Activate,
			// Token: 0x0400042C RID: 1068
			Enable,
			// Token: 0x0400042D RID: 1069
			Animate,
			// Token: 0x0400042E RID: 1070
			Deactivate
		}
	}
}

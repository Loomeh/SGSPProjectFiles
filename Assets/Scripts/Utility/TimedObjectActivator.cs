using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000069 RID: 105
	public class TimedObjectActivator : MonoBehaviour
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00008A28 File Offset: 0x00006C28
		private void Awake()
		{
			foreach (TimedObjectActivator.Entry entry in this.entries.entries)
			{
				switch (entry.action)
				{
				case TimedObjectActivator.Action.Activate:
					base.StartCoroutine(this.Activate(entry));
					break;
				case TimedObjectActivator.Action.Deactivate:
					base.StartCoroutine(this.Deactivate(entry));
					break;
				case TimedObjectActivator.Action.Destroy:
					Object.Destroy(entry.target, entry.delay);
					break;
				case TimedObjectActivator.Action.ReloadLevel:
					base.StartCoroutine(this.ReloadLevel(entry));
					break;
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008AB3 File Offset: 0x00006CB3
		private IEnumerator Activate(TimedObjectActivator.Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			entry.target.SetActive(true);
			yield break;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008AC2 File Offset: 0x00006CC2
		private IEnumerator Deactivate(TimedObjectActivator.Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			entry.target.SetActive(false);
			yield break;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008AD1 File Offset: 0x00006CD1
		private IEnumerator ReloadLevel(TimedObjectActivator.Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			yield break;
		}

		// Token: 0x040001CE RID: 462
		public TimedObjectActivator.Entries entries = new TimedObjectActivator.Entries();

		// Token: 0x020000D3 RID: 211
		public enum Action
		{
			// Token: 0x04000454 RID: 1108
			Activate,
			// Token: 0x04000455 RID: 1109
			Deactivate,
			// Token: 0x04000456 RID: 1110
			Destroy,
			// Token: 0x04000457 RID: 1111
			ReloadLevel,
			// Token: 0x04000458 RID: 1112
			Call
		}

		// Token: 0x020000D4 RID: 212
		[Serializable]
		public class Entry
		{
			// Token: 0x04000459 RID: 1113
			public GameObject target;

			// Token: 0x0400045A RID: 1114
			public TimedObjectActivator.Action action;

			// Token: 0x0400045B RID: 1115
			public float delay;
		}

		// Token: 0x020000D5 RID: 213
		[Serializable]
		public class Entries
		{
			// Token: 0x0400045C RID: 1116
			public TimedObjectActivator.Entry[] entries;
		}
	}
}

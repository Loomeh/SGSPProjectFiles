using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000065 RID: 101
	public class PlatformSpecificContent : MonoBehaviour
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000841C File Offset: 0x0000661C
		private void OnEnable()
		{
			this.CheckEnableContent();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008424 File Offset: 0x00006624
		private void CheckEnableContent()
		{
			if (this.m_BuildTargetGroup == PlatformSpecificContent.BuildTargetGroup.Mobile)
			{
				this.EnableContent(false);
				return;
			}
			this.EnableContent(true);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008440 File Offset: 0x00006640
		private void EnableContent(bool enabled)
		{
			if (this.m_Content.Length != 0)
			{
				foreach (GameObject gameObject in this.m_Content)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(enabled);
					}
				}
			}
			if (this.m_ChildrenOfThisObject)
			{
				foreach (object obj in base.transform)
				{
					((Transform)obj).gameObject.SetActive(enabled);
				}
			}
			if (this.m_MonoBehaviours.Length != 0)
			{
				MonoBehaviour[] monoBehaviours = this.m_MonoBehaviours;
				for (int i = 0; i < monoBehaviours.Length; i++)
				{
					monoBehaviours[i].enabled = enabled;
				}
			}
		}

		// Token: 0x040001B8 RID: 440
		[SerializeField]
		private PlatformSpecificContent.BuildTargetGroup m_BuildTargetGroup;

		// Token: 0x040001B9 RID: 441
		[SerializeField]
		private GameObject[] m_Content = new GameObject[0];

		// Token: 0x040001BA RID: 442
		[SerializeField]
		private MonoBehaviour[] m_MonoBehaviours = new MonoBehaviour[0];

		// Token: 0x040001BB RID: 443
		[SerializeField]
		private bool m_ChildrenOfThisObject;

		// Token: 0x020000D2 RID: 210
		private enum BuildTargetGroup
		{
			// Token: 0x04000451 RID: 1105
			Standalone,
			// Token: 0x04000452 RID: 1106
			Mobile
		}
	}
}

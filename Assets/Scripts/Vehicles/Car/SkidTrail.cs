using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008D RID: 141
	public class SkidTrail : MonoBehaviour
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000CB7E File Offset: 0x0000AD7E
		private IEnumerator Start()
		{
			for (;;)
			{
				yield return null;
				if (base.transform.parent.parent == null)
				{
					Object.Destroy(base.gameObject, this.m_PersistTime);
				}
			}
			yield break;
		}

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		private float m_PersistTime;
	}
}

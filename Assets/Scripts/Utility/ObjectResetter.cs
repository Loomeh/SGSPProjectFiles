using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000063 RID: 99
	public class ObjectResetter : MonoBehaviour
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00008374 File Offset: 0x00006574
		private void Start()
		{
			this.originalStructure = new List<Transform>(base.GetComponentsInChildren<Transform>());
			this.originalPosition = base.transform.position;
			this.originalRotation = base.transform.rotation;
			this.Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000083C0 File Offset: 0x000065C0
		public void DelayedReset(float delay)
		{
			base.StartCoroutine(this.ResetCoroutine(delay));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000083D0 File Offset: 0x000065D0
		public IEnumerator ResetCoroutine(float delay)
		{
			yield return new WaitForSeconds(delay);
			foreach (Transform transform in base.GetComponentsInChildren<Transform>())
			{
				if (!this.originalStructure.Contains(transform))
				{
					transform.parent = null;
				}
			}
			base.transform.position = this.originalPosition;
			base.transform.rotation = this.originalRotation;
			if (this.Rigidbody)
			{
				this.Rigidbody.velocity = Vector3.zero;
				this.Rigidbody.angularVelocity = Vector3.zero;
			}
			base.SendMessage("Reset");
			yield break;
		}

		// Token: 0x040001B0 RID: 432
		private Vector3 originalPosition;

		// Token: 0x040001B1 RID: 433
		private Quaternion originalRotation;

		// Token: 0x040001B2 RID: 434
		private List<Transform> originalStructure;

		// Token: 0x040001B3 RID: 435
		private Rigidbody Rigidbody;
	}
}

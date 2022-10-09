using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000070 RID: 112
	public class Explosive : MonoBehaviour
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000097FA File Offset: 0x000079FA
		private void Start()
		{
			this.m_ObjectResetter = base.GetComponent<ObjectResetter>();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009808 File Offset: 0x00007A08
		private IEnumerator OnCollisionEnter(Collision col)
		{
			if (base.enabled && col.contacts.Length != 0 && (Vector3.Project(col.relativeVelocity, col.contacts[0].normal).magnitude > this.detonationImpactVelocity || this.m_Exploded) && !this.m_Exploded)
			{
				Object.Instantiate<Transform>(this.explosionPrefab, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
				this.m_Exploded = true;
				base.SendMessage("Immobilize");
				if (this.reset)
				{
					this.m_ObjectResetter.DelayedReset(this.resetTimeDelay);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000981E File Offset: 0x00007A1E
		public void Reset()
		{
			this.m_Exploded = false;
		}

		// Token: 0x040001FB RID: 507
		public Transform explosionPrefab;

		// Token: 0x040001FC RID: 508
		public float detonationImpactVelocity = 10f;

		// Token: 0x040001FD RID: 509
		public float sizeMultiplier = 1f;

		// Token: 0x040001FE RID: 510
		public bool reset = true;

		// Token: 0x040001FF RID: 511
		public float resetTimeDelay = 10f;

		// Token: 0x04000200 RID: 512
		private bool m_Exploded;

		// Token: 0x04000201 RID: 513
		private ObjectResetter m_ObjectResetter;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200006F RID: 111
	public class ExplosionPhysicsForce : MonoBehaviour
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000097D8 File Offset: 0x000079D8
		private IEnumerator Start()
		{
			yield return null;
			float multiplier = base.GetComponent<ParticleSystemMultiplier>().multiplier;
			float num = 10f * multiplier;
			Collider[] array = Physics.OverlapSphere(base.transform.position, num);
			List<Rigidbody> list = new List<Rigidbody>();
			foreach (Collider collider in array)
			{
				if (collider.attachedRigidbody != null && !list.Contains(collider.attachedRigidbody))
				{
					list.Add(collider.attachedRigidbody);
				}
			}
			using (List<Rigidbody>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rigidbody rigidbody = enumerator.Current;
					rigidbody.AddExplosionForce(this.explosionForce * multiplier, base.transform.position, num, 1f * multiplier, ForceMode.Impulse);
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x040001FA RID: 506
		public float explosionForce = 4f;
	}
}

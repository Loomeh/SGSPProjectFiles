using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200006E RID: 110
	public class ExplosionFireAndDebris : MonoBehaviour
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000979D File Offset: 0x0000799D
		private IEnumerator Start()
		{
			float multiplier = base.GetComponent<ParticleSystemMultiplier>().multiplier;
			int num = 0;
			while ((float)num < (float)this.numDebrisPieces * multiplier)
			{
				Transform original = this.debrisPrefabs[Random.Range(0, this.debrisPrefabs.Length)];
				Vector3 position = base.transform.position + Random.insideUnitSphere * 3f * multiplier;
				Quaternion rotation = Random.rotation;
				Object.Instantiate<Transform>(original, position, rotation);
				num++;
			}
			yield return null;
			float num2 = 10f * multiplier;
			foreach (Collider collider in Physics.OverlapSphere(base.transform.position, num2))
			{
				if (this.numFires > 0)
				{
					Ray ray = new Ray(base.transform.position, collider.transform.position - base.transform.position);
					RaycastHit raycastHit;
					if (collider.Raycast(ray, out raycastHit, num2))
					{
						this.AddFire(collider.transform, raycastHit.point, raycastHit.normal);
						this.numFires--;
					}
				}
			}
			float num3 = 0f;
			while (this.numFires > 0 && num3 < num2)
			{
				RaycastHit raycastHit2;
				if (Physics.Raycast(new Ray(base.transform.position + Vector3.up, Random.onUnitSphere), out raycastHit2, num3))
				{
					this.AddFire(null, raycastHit2.point, raycastHit2.normal);
					this.numFires--;
				}
				num3 += num2 * 0.1f;
			}
			yield break;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000097AC File Offset: 0x000079AC
		private void AddFire(Transform t, Vector3 pos, Vector3 normal)
		{
			pos += normal * 0.5f;
			Object.Instantiate<Transform>(this.firePrefab, pos, Quaternion.identity).parent = t;
		}

		// Token: 0x040001F6 RID: 502
		public Transform[] debrisPrefabs;

		// Token: 0x040001F7 RID: 503
		public Transform firePrefab;

		// Token: 0x040001F8 RID: 504
		public int numDebrisPieces;

		// Token: 0x040001F9 RID: 505
		public int numFires;
	}
}

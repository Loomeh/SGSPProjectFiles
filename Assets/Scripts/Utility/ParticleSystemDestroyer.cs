using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000064 RID: 100
	public class ParticleSystemDestroyer : MonoBehaviour
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000083E6 File Offset: 0x000065E6
		private IEnumerator Start()
		{
			ParticleSystem[] systems = base.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in systems)
			{
				this.m_MaxLifetime = Mathf.Max(particleSystem.main.startLifetime.constant, this.m_MaxLifetime);
			}
			float stopTime = Time.time + Random.Range(this.minDuration, this.maxDuration);
			while (Time.time < stopTime && !this.m_EarlyStop)
			{
				yield return null;
			}
			Debug.Log("stopping " + base.name);
			ParticleSystem[] array = systems;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].emission.enabled = false;
			}
			base.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);
			yield return new WaitForSeconds(this.m_MaxLifetime);
			Object.Destroy(base.gameObject);
			yield break;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000083F5 File Offset: 0x000065F5
		public void Stop()
		{
			this.m_EarlyStop = true;
		}

		// Token: 0x040001B4 RID: 436
		public float minDuration = 8f;

		// Token: 0x040001B5 RID: 437
		public float maxDuration = 10f;

		// Token: 0x040001B6 RID: 438
		private float m_MaxLifetime;

		// Token: 0x040001B7 RID: 439
		private bool m_EarlyStop;
	}
}

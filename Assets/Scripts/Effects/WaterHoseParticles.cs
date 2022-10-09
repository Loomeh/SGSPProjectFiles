using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000076 RID: 118
	public class WaterHoseParticles : MonoBehaviour
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00009BC6 File Offset: 0x00007DC6
		private void Start()
		{
			this.m_ParticleSystem = base.GetComponent<ParticleSystem>();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009BD4 File Offset: 0x00007DD4
		private void OnParticleCollision(GameObject other)
		{
			int collisionEvents = this.m_ParticleSystem.GetCollisionEvents(other, this.m_CollisionEvents);
			for (int i = 0; i < collisionEvents; i++)
			{
				if (Time.time > WaterHoseParticles.lastSoundTime + 0.2f)
				{
					WaterHoseParticles.lastSoundTime = Time.time;
				}
				Rigidbody component = this.m_CollisionEvents[i].colliderComponent.GetComponent<Rigidbody>();
				if (component != null)
				{
					Vector3 velocity = this.m_CollisionEvents[i].velocity;
					component.AddForce(velocity * this.force, ForceMode.Impulse);
				}
				other.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0400020F RID: 527
		public static float lastSoundTime;

		// Token: 0x04000210 RID: 528
		public float force = 1f;

		// Token: 0x04000211 RID: 529
		private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();

		// Token: 0x04000212 RID: 530
		private ParticleSystem m_ParticleSystem;
	}
}

using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200008F RID: 143
	[RequireComponent(typeof(AudioSource))]
	public class WheelEffects : MonoBehaviour
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		public bool skidding { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000CBFA File Offset: 0x0000ADFA
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000CC02 File Offset: 0x0000AE02
		public bool PlayingAudio { get; private set; }

		// Token: 0x06000296 RID: 662 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		private void Start()
		{
			this.skidParticles = base.transform.root.GetComponentInChildren<ParticleSystem>();
			if (this.skidParticles == null)
			{
				Debug.LogWarning(" no particle system found on car to generate smoke particles", base.gameObject);
			}
			else
			{
				this.skidParticles.Stop();
			}
			this.m_WheelCollider = base.GetComponent<WheelCollider>();
			this.m_AudioSource = base.GetComponent<AudioSource>();
			this.PlayingAudio = false;
			if (WheelEffects.skidTrailsDetachedParent == null)
			{
				WheelEffects.skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		public void EmitTyreSmoke()
		{
			this.skidParticles.transform.position = base.transform.position - base.transform.up * this.m_WheelCollider.radius;
			this.skidParticles.Emit(1);
			if (!this.skidding)
			{
				base.StartCoroutine(this.StartSkidTrail());
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CD05 File Offset: 0x0000AF05
		public void PlayAudio()
		{
			this.m_AudioSource.Play();
			this.PlayingAudio = true;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000CD19 File Offset: 0x0000AF19
		public void StopAudio()
		{
			this.m_AudioSource.Stop();
			this.PlayingAudio = false;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		public IEnumerator StartSkidTrail()
		{
			this.skidding = true;
			this.m_SkidTrail = Object.Instantiate<Transform>(this.SkidTrailPrefab);
			while (this.m_SkidTrail == null)
			{
				yield return null;
			}
			this.m_SkidTrail.parent = base.transform;
			this.m_SkidTrail.localPosition = -Vector3.up * this.m_WheelCollider.radius;
			yield break;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public void EndSkidTrail()
		{
			if (!this.skidding)
			{
				return;
			}
			this.skidding = false;
			this.m_SkidTrail.parent = WheelEffects.skidTrailsDetachedParent;
			Object.Destroy(this.m_SkidTrail.gameObject, 10f);
		}

		// Token: 0x040002AC RID: 684
		public Transform SkidTrailPrefab;

		// Token: 0x040002AD RID: 685
		public static Transform skidTrailsDetachedParent;

		// Token: 0x040002AE RID: 686
		public ParticleSystem skidParticles;

		// Token: 0x040002B1 RID: 689
		private AudioSource m_AudioSource;

		// Token: 0x040002B2 RID: 690
		private Transform m_SkidTrail;

		// Token: 0x040002B3 RID: 691
		private WheelCollider m_WheelCollider;
	}
}

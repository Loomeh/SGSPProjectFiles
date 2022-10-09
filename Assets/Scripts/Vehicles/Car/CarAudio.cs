using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000086 RID: 134
	[RequireComponent(typeof(CarController))]
	public class CarAudio : MonoBehaviour
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		private void StartSound()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_HighAccel = this.SetUpEngineAudioSource(this.highAccelClip);
			if (this.engineSoundStyle == CarAudio.EngineAudioOptions.FourChannel)
			{
				this.m_LowAccel = this.SetUpEngineAudioSource(this.lowAccelClip);
				this.m_LowDecel = this.SetUpEngineAudioSource(this.lowDecelClip);
				this.m_HighDecel = this.SetUpEngineAudioSource(this.highDecelClip);
			}
			this.m_StartedSound = true;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BD2C File Offset: 0x00009F2C
		private void StopSound()
		{
			AudioSource[] components = base.GetComponents<AudioSource>();
			for (int i = 0; i < components.Length; i++)
			{
				Object.Destroy(components[i]);
			}
			this.m_StartedSound = false;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BD60 File Offset: 0x00009F60
		private void Update()
		{
			float sqrMagnitude = (Camera.main.transform.position - base.transform.position).sqrMagnitude;
			if (this.m_StartedSound && sqrMagnitude > this.maxRolloffDistance * this.maxRolloffDistance)
			{
				this.StopSound();
			}
			if (!this.m_StartedSound && sqrMagnitude < this.maxRolloffDistance * this.maxRolloffDistance)
			{
				this.StartSound();
			}
			if (this.m_StartedSound)
			{
				float num = CarAudio.ULerp(this.lowPitchMin, this.lowPitchMax, this.m_CarController.Revs);
				num = Mathf.Min(this.lowPitchMax, num);
				if (this.engineSoundStyle == CarAudio.EngineAudioOptions.Simple)
				{
					this.m_HighAccel.pitch = num * this.pitchMultiplier * this.highPitchMultiplier;
					this.m_HighAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
					this.m_HighAccel.volume = 1f;
					return;
				}
				this.m_LowAccel.pitch = num * this.pitchMultiplier;
				this.m_LowDecel.pitch = num * this.pitchMultiplier;
				this.m_HighAccel.pitch = num * this.highPitchMultiplier * this.pitchMultiplier;
				this.m_HighDecel.pitch = num * this.highPitchMultiplier * this.pitchMultiplier;
				float num2 = Mathf.Abs(this.m_CarController.AccelInput);
				float num3 = 1f - num2;
				float num4 = Mathf.InverseLerp(0.2f, 0.8f, this.m_CarController.Revs);
				float num5 = 1f - num4;
				num4 = 1f - (1f - num4) * (1f - num4);
				num5 = 1f - (1f - num5) * (1f - num5);
				num2 = 1f - (1f - num2) * (1f - num2);
				num3 = 1f - (1f - num3) * (1f - num3);
				this.m_LowAccel.volume = num5 * num2;
				this.m_LowDecel.volume = num5 * num3;
				this.m_HighAccel.volume = num4 * num2;
				this.m_HighDecel.volume = num4 * num3;
				this.m_HighAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_LowAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_HighDecel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_LowDecel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C014 File Offset: 0x0000A214
		private AudioSource SetUpEngineAudioSource(AudioClip clip)
		{
			AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.volume = 0f;
			audioSource.loop = true;
			audioSource.time = Random.Range(0f, clip.length);
			audioSource.Play();
			audioSource.minDistance = 5f;
			audioSource.maxDistance = this.maxRolloffDistance;
			audioSource.dopplerLevel = 0f;
			return audioSource;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C083 File Offset: 0x0000A283
		private static float ULerp(float from, float to, float value)
		{
			return (1f - value) * from + value * to;
		}

		// Token: 0x04000268 RID: 616
		public CarAudio.EngineAudioOptions engineSoundStyle = CarAudio.EngineAudioOptions.FourChannel;

		// Token: 0x04000269 RID: 617
		public AudioClip lowAccelClip;

		// Token: 0x0400026A RID: 618
		public AudioClip lowDecelClip;

		// Token: 0x0400026B RID: 619
		public AudioClip highAccelClip;

		// Token: 0x0400026C RID: 620
		public AudioClip highDecelClip;

		// Token: 0x0400026D RID: 621
		public float pitchMultiplier = 1f;

		// Token: 0x0400026E RID: 622
		public float lowPitchMin = 1f;

		// Token: 0x0400026F RID: 623
		public float lowPitchMax = 6f;

		// Token: 0x04000270 RID: 624
		public float highPitchMultiplier = 0.25f;

		// Token: 0x04000271 RID: 625
		public float maxRolloffDistance = 500f;

		// Token: 0x04000272 RID: 626
		public float dopplerLevel = 1f;

		// Token: 0x04000273 RID: 627
		public bool useDoppler = true;

		// Token: 0x04000274 RID: 628
		private AudioSource m_LowAccel;

		// Token: 0x04000275 RID: 629
		private AudioSource m_LowDecel;

		// Token: 0x04000276 RID: 630
		private AudioSource m_HighAccel;

		// Token: 0x04000277 RID: 631
		private AudioSource m_HighDecel;

		// Token: 0x04000278 RID: 632
		private bool m_StartedSound;

		// Token: 0x04000279 RID: 633
		private CarController m_CarController;

		// Token: 0x020000E9 RID: 233
		public enum EngineAudioOptions
		{
			// Token: 0x0400049D RID: 1181
			Simple,
			// Token: 0x0400049E RID: 1182
			FourChannel
		}
	}
}

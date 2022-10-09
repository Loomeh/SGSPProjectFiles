using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000091 RID: 145
	public class AeroplaneAudio : MonoBehaviour
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		private void Awake()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_EngineSoundSource = base.gameObject.AddComponent<AudioSource>();
			this.m_EngineSoundSource.playOnAwake = false;
			this.m_WindSoundSource = base.gameObject.AddComponent<AudioSource>();
			this.m_WindSoundSource.playOnAwake = false;
			this.m_EngineSoundSource.clip = this.m_EngineSound;
			this.m_WindSoundSource.clip = this.m_WindSound;
			this.m_EngineSoundSource.minDistance = this.m_AdvancedSetttings.engineMinDistance;
			this.m_EngineSoundSource.maxDistance = this.m_AdvancedSetttings.engineMaxDistance;
			this.m_EngineSoundSource.loop = true;
			this.m_EngineSoundSource.dopplerLevel = this.m_AdvancedSetttings.engineDopplerLevel;
			this.m_WindSoundSource.minDistance = this.m_AdvancedSetttings.windMinDistance;
			this.m_WindSoundSource.maxDistance = this.m_AdvancedSetttings.windMaxDistance;
			this.m_WindSoundSource.loop = true;
			this.m_WindSoundSource.dopplerLevel = this.m_AdvancedSetttings.windDopplerLevel;
			this.Update();
			this.m_EngineSoundSource.Play();
			this.m_WindSoundSource.Play();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		private void Update()
		{
			float t = Mathf.InverseLerp(0f, this.m_Plane.MaxEnginePower, this.m_Plane.EnginePower);
			this.m_EngineSoundSource.pitch = Mathf.Lerp(this.m_EngineMinThrottlePitch, this.m_EngineMaxThrottlePitch, t);
			this.m_EngineSoundSource.pitch += this.m_Plane.ForwardSpeed * this.m_EngineFwdSpeedMultiplier;
			this.m_EngineSoundSource.volume = Mathf.InverseLerp(0f, this.m_Plane.MaxEnginePower * this.m_AdvancedSetttings.engineMasterVolume, this.m_Plane.EnginePower);
			float magnitude = this.m_Rigidbody.velocity.magnitude;
			this.m_WindSoundSource.pitch = this.m_WindBasePitch + magnitude * this.m_WindSpeedPitchFactor;
			this.m_WindSoundSource.volume = Mathf.InverseLerp(0f, this.m_WindMaxSpeedVolume, magnitude) * this.m_AdvancedSetttings.windMasterVolume;
		}

		// Token: 0x040002C0 RID: 704
		[SerializeField]
		private AudioClip m_EngineSound;

		// Token: 0x040002C1 RID: 705
		[SerializeField]
		private float m_EngineMinThrottlePitch = 0.4f;

		// Token: 0x040002C2 RID: 706
		[SerializeField]
		private float m_EngineMaxThrottlePitch = 2f;

		// Token: 0x040002C3 RID: 707
		[SerializeField]
		private float m_EngineFwdSpeedMultiplier = 0.002f;

		// Token: 0x040002C4 RID: 708
		[SerializeField]
		private AudioClip m_WindSound;

		// Token: 0x040002C5 RID: 709
		[SerializeField]
		private float m_WindBasePitch = 0.2f;

		// Token: 0x040002C6 RID: 710
		[SerializeField]
		private float m_WindSpeedPitchFactor = 0.004f;

		// Token: 0x040002C7 RID: 711
		[SerializeField]
		private float m_WindMaxSpeedVolume = 100f;

		// Token: 0x040002C8 RID: 712
		[SerializeField]
		private AeroplaneAudio.AdvancedSetttings m_AdvancedSetttings = new AeroplaneAudio.AdvancedSetttings();

		// Token: 0x040002C9 RID: 713
		private AudioSource m_EngineSoundSource;

		// Token: 0x040002CA RID: 714
		private AudioSource m_WindSoundSource;

		// Token: 0x040002CB RID: 715
		private AeroplaneController m_Plane;

		// Token: 0x040002CC RID: 716
		private Rigidbody m_Rigidbody;

		// Token: 0x020000EC RID: 236
		[Serializable]
		public class AdvancedSetttings
		{
			// Token: 0x040004A5 RID: 1189
			public float engineMinDistance = 50f;

			// Token: 0x040004A6 RID: 1190
			public float engineMaxDistance = 1000f;

			// Token: 0x040004A7 RID: 1191
			public float engineDopplerLevel = 1f;

			// Token: 0x040004A8 RID: 1192
			[Range(0f, 1f)]
			public float engineMasterVolume = 0.5f;

			// Token: 0x040004A9 RID: 1193
			public float windMinDistance = 10f;

			// Token: 0x040004AA RID: 1194
			public float windMaxDistance = 100f;

			// Token: 0x040004AB RID: 1195
			public float windDopplerLevel = 1f;

			// Token: 0x040004AC RID: 1196
			[Range(0f, 1f)]
			public float windMasterVolume = 0.5f;
		}
	}
}

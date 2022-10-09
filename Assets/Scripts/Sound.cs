using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[Serializable]
public class Sound
{
	// Token: 0x04000050 RID: 80
	public string name;

	// Token: 0x04000051 RID: 81
	public AudioClip clip;

	// Token: 0x04000052 RID: 82
	[Range(0f, 1f)]
	public float volume;

	// Token: 0x04000053 RID: 83
	[Range(0f, 2f)]
	public float pitch;

	// Token: 0x04000054 RID: 84
	[HideInInspector]
	public AudioSource source;
}

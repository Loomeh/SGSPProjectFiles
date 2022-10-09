using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[ExecuteInEditMode]
public class FPSParticleSystemScaler : MonoBehaviour
{
	// Token: 0x06000012 RID: 18 RVA: 0x00002642 File Offset: 0x00000842
	private void Start()
	{
		this.oldScale = this.particlesScale;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000255D File Offset: 0x0000075D
	private void Update()
	{
	}

	// Token: 0x04000016 RID: 22
	public float particlesScale = 1f;

	// Token: 0x04000017 RID: 23
	private float oldScale;
}

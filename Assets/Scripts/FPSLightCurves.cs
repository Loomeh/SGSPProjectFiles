using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class FPSLightCurves : MonoBehaviour
{
	// Token: 0x0600000E RID: 14 RVA: 0x0000256F File Offset: 0x0000076F
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002598 File Offset: 0x00000798
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000025AC File Offset: 0x000007AC
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float intensity = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = intensity;
		}
		if (num >= this.GraphTimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x04000010 RID: 16
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000011 RID: 17
	public float GraphTimeMultiplier = 1f;

	// Token: 0x04000012 RID: 18
	public float GraphIntensityMultiplier = 1f;

	// Token: 0x04000013 RID: 19
	private bool canUpdate;

	// Token: 0x04000014 RID: 20
	private float startTime;

	// Token: 0x04000015 RID: 21
	private Light lightSource;
}

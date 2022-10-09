using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class StressReceiver : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x00002A2C File Offset: 0x00000C2C
	private void Update()
	{
		float num = Mathf.Pow(this._trauma, this.TraumaExponent);
		if (num > 0f)
		{
			Vector3 lastRotation = this._lastRotation;
			Vector3 lastPosition = this._lastPosition;
			this._lastPosition = new Vector3(this.MaximumTranslationShake.x * (Mathf.PerlinNoise(0f, Time.time * 25f) * 2f - 1f), this.MaximumTranslationShake.y * (Mathf.PerlinNoise(1f, Time.time * 25f) * 2f - 1f), this.MaximumTranslationShake.z * (Mathf.PerlinNoise(2f, Time.time * 25f) * 2f - 1f)) * num;
			this._lastRotation = new Vector3(this.MaximumAngularShake.x * (Mathf.PerlinNoise(3f, Time.time * 25f) * 2f - 1f), this.MaximumAngularShake.y * (Mathf.PerlinNoise(4f, Time.time * 25f) * 2f - 1f), this.MaximumAngularShake.z * (Mathf.PerlinNoise(5f, Time.time * 25f) * 2f - 1f)) * num;
			base.transform.localPosition += this._lastPosition - lastPosition;
			base.transform.localRotation = Quaternion.Euler(base.transform.localRotation.eulerAngles + this._lastRotation - lastRotation);
			this._trauma = Mathf.Clamp01(this._trauma - Time.deltaTime);
			return;
		}
		if (this._lastPosition == Vector3.zero && this._lastRotation == Vector3.zero)
		{
			return;
		}
		base.transform.localPosition -= this._lastPosition;
		base.transform.localRotation = Quaternion.Euler(base.transform.localRotation.eulerAngles - this._lastRotation);
		this._lastPosition = Vector3.zero;
		this._lastRotation = Vector3.zero;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002C87 File Offset: 0x00000E87
	public void InduceStress(float Stress)
	{
		this._trauma = Mathf.Clamp01(this._trauma + Stress);
	}

	// Token: 0x0400002F RID: 47
	private float _trauma;

	// Token: 0x04000030 RID: 48
	private Vector3 _lastPosition;

	// Token: 0x04000031 RID: 49
	private Vector3 _lastRotation;

	// Token: 0x04000032 RID: 50
	[Tooltip("Exponent for calculating the shake factor. Useful for creating different effect fade outs")]
	public float TraumaExponent = 1f;

	// Token: 0x04000033 RID: 51
	[Tooltip("Maximum angle that the gameobject can shake. In euler angles.")]
	public Vector3 MaximumAngularShake = Vector3.one * 5f;

	// Token: 0x04000034 RID: 52
	[Tooltip("Maximum translation that the gameobject can receive when applying the shake effect.")]
	public Vector3 MaximumTranslationShake = Vector3.one * 0.75f;
}

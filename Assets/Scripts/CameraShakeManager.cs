using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class CameraShakeManager : MonoBehaviour
{
	// Token: 0x06000068 RID: 104 RVA: 0x00003D66 File Offset: 0x00001F66
	private void Awake()
	{
		CameraShakeManager.instance = this;
		this.fpsCamera = base.GetComponent<Camera>();
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003D7A File Offset: 0x00001F7A
	public static CameraShakeManager Get()
	{
		return CameraShakeManager.instance;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003D81 File Offset: 0x00001F81
	public void ShakeScreen(float duration, float magnitude)
	{
		base.StartCoroutine(this.Shake(duration, magnitude));
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003D92 File Offset: 0x00001F92
	private IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = base.transform.localPosition;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			float x = (float)Random.Range(-1, 1) * magnitude;
			float y = (float)Random.Range(-1, 1) * magnitude;
			base.transform.localPosition = new Vector3(x, y, originalPos.z);
			elapsed += Time.deltaTime;
			yield return null;
		}
		base.transform.localPosition = originalPos;
		yield break;
	}

	// Token: 0x04000063 RID: 99
	private static CameraShakeManager instance;

	// Token: 0x04000064 RID: 100
	private Camera fpsCamera;
}

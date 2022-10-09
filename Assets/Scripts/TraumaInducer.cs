using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class TraumaInducer : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x00002CD9 File Offset: 0x00000ED9
	private IEnumerator Explosion()
	{
		yield return new WaitForSeconds(this.Delay);
		this.PlayParticles();
		GameObject[] array = Object.FindObjectsOfType<GameObject>();
		for (int i = 0; i < array.Length; i++)
		{
			StressReceiver component = array[i].GetComponent<StressReceiver>();
			if (!(component == null))
			{
				float num = Vector3.Distance(base.transform.position, array[i].transform.position);
				if (num <= this.Range)
				{
					float f = Mathf.Clamp01(num / this.Range);
					float stress = (1f - Mathf.Pow(f, 2f)) * this.MaximumStress;
					component.InduceStress(stress);
				}
			}
		}
		yield break;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002CE8 File Offset: 0x00000EE8
	private void PlayParticles()
	{
		ParticleSystem[] componentsInChildren = base.transform.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		if (component != null)
		{
			component.Play();
		}
	}

	// Token: 0x04000035 RID: 53
	[Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
	public float Delay = 1f;

	// Token: 0x04000036 RID: 54
	[Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
	public float MaximumStress = 0.6f;

	// Token: 0x04000037 RID: 55
	[Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
	public float Range = 45f;
}

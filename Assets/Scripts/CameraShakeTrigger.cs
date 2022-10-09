using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class CameraShakeTrigger : MonoBehaviour
{
	// Token: 0x0600006D RID: 109 RVA: 0x00003DAF File Offset: 0x00001FAF
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			base.GetComponent<TraumaInducer>().StartCoroutine("Explosion");
		}
	}
}

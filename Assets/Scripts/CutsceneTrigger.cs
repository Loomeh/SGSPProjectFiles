using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class CutsceneTrigger : MonoBehaviour
{
	// Token: 0x0600007A RID: 122 RVA: 0x00003E68 File Offset: 0x00002068
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.Get().LoadLevel(Level.SQUIDWARDSHOUSE);
		}
	}
}

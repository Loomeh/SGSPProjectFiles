using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class MrKrabsTalkTrigger : MonoBehaviour
{
	// Token: 0x060000C9 RID: 201 RVA: 0x000050B1 File Offset: 0x000032B1
	private void Start()
	{
		this.mrKrabs = base.GetComponentInParent<MrKrabs>();
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000050BF File Offset: 0x000032BF
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.mrKrabs.canTalk = true;
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000050DA File Offset: 0x000032DA
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.mrKrabs.canTalk = false;
		}
	}

	// Token: 0x040000D0 RID: 208
	private MrKrabs mrKrabs;
}

using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class PrefabManager : MonoBehaviour
{
	// Token: 0x06000106 RID: 262 RVA: 0x00006C55 File Offset: 0x00004E55
	private void Awake()
	{
		PrefabManager.instance = this;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00006C5D File Offset: 0x00004E5D
	public static PrefabManager Get()
	{
		return PrefabManager.instance;
	}

	// Token: 0x04000133 RID: 307
	private static PrefabManager instance;
}

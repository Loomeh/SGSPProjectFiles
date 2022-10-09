using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class WarpManager : MonoBehaviour
{
	// Token: 0x0600015D RID: 349 RVA: 0x00007A48 File Offset: 0x00005C48
	private void Awake()
	{
		WarpManager.instance = this;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00007A50 File Offset: 0x00005C50
	public static WarpManager Get()
	{
		return WarpManager.instance;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00007A58 File Offset: 0x00005C58
	public Transform GetPlayerInitialSpawn(Level prevLevel)
	{
		Warp warp2 = Array.Find<Warp>(this.warps, (Warp warp) => warp.previousLevel == prevLevel);
		if (warp2 == null)
		{
			Debug.Log("Warp for level " + prevLevel + " not found.");
			return null;
		}
		return warp2.warpPosition;
	}

	// Token: 0x04000173 RID: 371
	public static WarpManager instance;

	// Token: 0x04000174 RID: 372
	public Warp[] warps;
}

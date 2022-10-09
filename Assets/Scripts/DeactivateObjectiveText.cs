using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class DeactivateObjectiveText : MonoBehaviour
{
	// Token: 0x06000080 RID: 128 RVA: 0x00003EB2 File Offset: 0x000020B2
	public void DeactivateObjectiveTextHUD()
	{
		HUDManager.Get().objectiveText.gameObject.SetActive(false);
	}
}

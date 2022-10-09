using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000031 RID: 49
public class HUDManager : MonoBehaviour
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00004A59 File Offset: 0x00002C59
	private void Awake()
	{
		HUDManager.instance = this;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00004A61 File Offset: 0x00002C61
	public static HUDManager Get()
	{
		return HUDManager.instance;
	}

	// Token: 0x040000AD RID: 173
	private static HUDManager instance;

	// Token: 0x040000AE RID: 174
	public TextMeshProUGUI ineractText;

	// Token: 0x040000AF RID: 175
	public TextMeshProUGUI itemGetText;

	// Token: 0x040000B0 RID: 176
	public GameObject hitMarkers;

	// Token: 0x040000B1 RID: 177
	public Image bloodOverlay;

	// Token: 0x040000B2 RID: 178
	public GameObject crosshair;

	// Token: 0x040000B3 RID: 179
	public TextMeshProUGUI objectiveText;
}

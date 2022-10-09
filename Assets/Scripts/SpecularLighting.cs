using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
[RequireComponent(typeof(WaterBase))]
[ExecuteInEditMode]
public class SpecularLighting : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x0000353C File Offset: 0x0000173C
	public void Start()
	{
		this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003560 File Offset: 0x00001760
	public void Update()
	{
		if (!this.waterBase)
		{
			this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
		}
		if (this.specularLight && this.waterBase.sharedMaterial)
		{
			this.waterBase.sharedMaterial.SetVector("_WorldLightDir", this.specularLight.transform.forward);
		}
	}

	// Token: 0x04000044 RID: 68
	public Transform specularLight;

	// Token: 0x04000045 RID: 69
	private WaterBase waterBase;
}

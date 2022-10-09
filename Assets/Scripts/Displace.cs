using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[ExecuteInEditMode]
[RequireComponent(typeof(WaterBase))]
public class Displace : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x00002D56 File Offset: 0x00000F56
	public void Awake()
	{
		if (base.enabled)
		{
			this.OnEnable();
			return;
		}
		this.OnDisable();
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002D6D File Offset: 0x00000F6D
	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002D83 File Offset: 0x00000F83
	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
	}
}

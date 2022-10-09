using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
[ExecuteInEditMode]
public class WaterBase : MonoBehaviour
{
	// Token: 0x06000040 RID: 64 RVA: 0x000035E4 File Offset: 0x000017E4
	public void UpdateShader()
	{
		if (this.waterQuality > WaterQuality.Medium)
		{
			this.sharedMaterial.shader.maximumLOD = 501;
		}
		else if (this.waterQuality > WaterQuality.Low)
		{
			this.sharedMaterial.shader.maximumLOD = 301;
		}
		else
		{
			this.sharedMaterial.shader.maximumLOD = 201;
		}
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.edgeBlend = false;
		}
		if (this.edgeBlend)
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_ON");
			Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
			if (Camera.main)
			{
				Camera.main.depthTextureMode |= DepthTextureMode.Depth;
				return;
			}
		}
		else
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
			Shader.DisableKeyword("WATER_EDGEBLEND_ON");
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000036A4 File Offset: 0x000018A4
	public void WaterTileBeingRendered(Transform tr, Camera currentCam)
	{
		if (currentCam && this.edgeBlend)
		{
			currentCam.depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000036C4 File Offset: 0x000018C4
	public void Update()
	{
		if (this.sharedMaterial)
		{
			this.UpdateShader();
		}
	}

	// Token: 0x0400004A RID: 74
	public Material sharedMaterial;

	// Token: 0x0400004B RID: 75
	public WaterQuality waterQuality = WaterQuality.High;

	// Token: 0x0400004C RID: 76
	public bool edgeBlend = true;
}

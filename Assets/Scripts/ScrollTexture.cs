using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class ScrollTexture : MonoBehaviour
{
	// Token: 0x0600011C RID: 284 RVA: 0x00007032 File Offset: 0x00005232
	private void Start()
	{
		this.objRenderer = base.GetComponent<Renderer>();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00007040 File Offset: 0x00005240
	private void Update()
	{
		this.objRenderer.material.mainTextureOffset = new Vector2(Time.time * this.xSpeed, Time.time * this.ySpeed);
	}

	// Token: 0x04000140 RID: 320
	private Renderer objRenderer;

	// Token: 0x04000141 RID: 321
	public float xSpeed;

	// Token: 0x04000142 RID: 322
	public float ySpeed;
}

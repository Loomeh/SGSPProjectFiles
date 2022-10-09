using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class TitleScreenBubble : MonoBehaviour
{
	// Token: 0x0600014C RID: 332 RVA: 0x00007844 File Offset: 0x00005A44
	private void Start()
	{
		this.upPos = base.transform.position + new Vector3(0f, 10f, 0f);
		this.downPos = base.transform.position - new Vector3(0f, 10f, 0f);
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000078A8 File Offset: 0x00005AA8
	private void FixedUpdate()
	{
		if (this.delay < 0f)
		{
			this.delay -= Time.deltaTime;
			return;
		}
		if (this.up)
		{
			base.transform.position = base.transform.position + new Vector3(0f, 0.1f, 0f);
			if (base.transform.position.y >= this.upPos.y)
			{
				this.up = false;
				return;
			}
		}
		else
		{
			base.transform.position = base.transform.position - new Vector3(0f, 0.1f, 0f);
			if (base.transform.position.y <= this.downPos.y)
			{
				this.up = true;
			}
		}
	}

	// Token: 0x04000168 RID: 360
	public bool up;

	// Token: 0x04000169 RID: 361
	public float delay;

	// Token: 0x0400016A RID: 362
	private Vector3 upPos;

	// Token: 0x0400016B RID: 363
	private Vector3 downPos;
}

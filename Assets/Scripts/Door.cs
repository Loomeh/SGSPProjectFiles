using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class Door : MonoBehaviour
{
	// Token: 0x06000082 RID: 130 RVA: 0x0000255D File Offset: 0x0000075D
	private void Start()
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003ECC File Offset: 0x000020CC
	private void Update()
	{
		if (this.use)
		{
			if (this.open)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.openTransform.position, 3f * Time.deltaTime);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.openTransform.rotation, 3f * Time.deltaTime);
				if (base.transform == this.openTransform)
				{
					this.use = false;
					return;
				}
			}
			else
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.closedTransform.position, 3f * Time.deltaTime);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.closedTransform.rotation, 3f * Time.deltaTime);
				if (base.transform == this.closedTransform)
				{
					this.use = false;
				}
			}
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003FEC File Offset: 0x000021EC
	public void Use()
	{
		this.use = true;
		this.open = !this.open;
		if (this.open)
		{
			AudioManager.Get().Play("MetalHingeOpen");
			this.openInteractTrigger.SetActive(false);
			this.closeInteractTrigger.SetActive(true);
			return;
		}
		this.openInteractTrigger.SetActive(true);
		this.closeInteractTrigger.SetActive(false);
		AudioManager.Get().Play("MetalHingeClose");
	}

	// Token: 0x04000074 RID: 116
	public Transform closedTransform;

	// Token: 0x04000075 RID: 117
	public Transform openTransform;

	// Token: 0x04000076 RID: 118
	public GameObject openInteractTrigger;

	// Token: 0x04000077 RID: 119
	public GameObject closeInteractTrigger;

	// Token: 0x04000078 RID: 120
	public bool use;

	// Token: 0x04000079 RID: 121
	public bool open;

	// Token: 0x0400007A RID: 122
	public float moveSpeed;

	// Token: 0x0400007B RID: 123
	public float rotSpeed;
}

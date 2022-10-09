using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[ExecuteInEditMode]
public class WaterTile : MonoBehaviour
{
	// Token: 0x06000044 RID: 68 RVA: 0x000036EF File Offset: 0x000018EF
	public void Start()
	{
		this.AcquireComponents();
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000036F8 File Offset: 0x000018F8
	private void AcquireComponents()
	{
		if (!this.reflection)
		{
			if (base.transform.parent)
			{
				this.reflection = base.transform.parent.GetComponent<PlanarReflection>();
			}
			else
			{
				this.reflection = base.transform.GetComponent<PlanarReflection>();
			}
		}
		if (!this.waterBase)
		{
			if (base.transform.parent)
			{
				this.waterBase = base.transform.parent.GetComponent<WaterBase>();
				return;
			}
			this.waterBase = base.transform.GetComponent<WaterBase>();
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003794 File Offset: 0x00001994
	public void OnWillRenderObject()
	{
		if (this.reflection)
		{
			this.reflection.WaterTileBeingRendered(base.transform, Camera.current);
		}
		if (this.waterBase)
		{
			this.waterBase.WaterTileBeingRendered(base.transform, Camera.current);
		}
	}

	// Token: 0x0400004D RID: 77
	public PlanarReflection reflection;

	// Token: 0x0400004E RID: 78
	public WaterBase waterBase;
}

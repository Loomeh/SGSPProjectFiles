using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class FPSRandomRotateAngle : MonoBehaviour
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002663 File Offset: 0x00000863
	private void Awake()
	{
		this.t = base.transform;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002674 File Offset: 0x00000874
	private void OnEnable()
	{
		Vector3 zero = Vector3.zero;
		if (this.RotateX)
		{
			zero.x = (float)Random.Range(0, 360);
		}
		if (this.RotateY)
		{
			zero.y = (float)Random.Range(0, 360);
		}
		if (this.RotateZ)
		{
			zero.z = (float)Random.Range(0, 360);
		}
		this.t.Rotate(zero);
	}

	// Token: 0x04000018 RID: 24
	public bool RotateX;

	// Token: 0x04000019 RID: 25
	public bool RotateY;

	// Token: 0x0400001A RID: 26
	public bool RotateZ = true;

	// Token: 0x0400001B RID: 27
	private Transform t;
}

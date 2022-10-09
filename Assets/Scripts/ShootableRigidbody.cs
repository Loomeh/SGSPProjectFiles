using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class ShootableRigidbody : MonoBehaviour
{
	// Token: 0x06000123 RID: 291 RVA: 0x000070F8 File Offset: 0x000052F8
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007108 File Offset: 0x00005308
	public void Shot()
	{
		if (this.rb.isKinematic)
		{
			this.rb.isKinematic = false;
		}
		Vector3 vector = base.transform.position - Camera.main.transform.position;
		this.rb.AddForce(vector.normalized * this.force);
	}

	// Token: 0x04000146 RID: 326
	public float force;

	// Token: 0x04000147 RID: 327
	private Rigidbody rb;
}

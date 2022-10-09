using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class Ladder : MonoBehaviour
{
	// Token: 0x060000B0 RID: 176 RVA: 0x00004C75 File Offset: 0x00002E75
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.touching = true;
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00004C8B File Offset: 0x00002E8B
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.touching = false;
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00004CA4 File Offset: 0x00002EA4
	private void Update()
	{
		if (this.touching && Input.GetKey(KeyCode.Space))
		{
			PlayerMovement component = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
			component.velocity = new Vector3(component.velocity.x, 6f, component.velocity.z);
		}
	}

	// Token: 0x040000BC RID: 188
	public bool touching;
}

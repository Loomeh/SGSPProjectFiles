using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class Bounce : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x00003D10 File Offset: 0x00001F10
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<PlayerMovement>().velocity.y = Mathf.Sqrt(this.height * -2f * other.GetComponent<PlayerMovement>().gravity);
			AudioManager.Get().Play("Boing");
		}
	}

	// Token: 0x04000062 RID: 98
	public float height;
}

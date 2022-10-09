using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000033 RID: 51
public class LSDTrigger : MonoBehaviour
{
	// Token: 0x060000AB RID: 171 RVA: 0x00004C0B File Offset: 0x00002E0B
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && this.inRange && !this.used)
		{
			this.used = true;
			base.StartCoroutine(this.LSDEffect());
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00004C3A File Offset: 0x00002E3A
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = true;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00004C50 File Offset: 0x00002E50
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.inRange = false;
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00004C66 File Offset: 0x00002E66
	private IEnumerator LSDEffect()
	{
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		AudioSource music = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>();
		camera.GetComponent<PostProcessVolume>().enabled = true;
		camera.GetComponent<PostProcessLayer>().enabled = true;
		music.pitch = 1.1f;
		yield return new WaitForSeconds(25f);
		camera.GetComponent<PostProcessVolume>().enabled = false;
		camera.GetComponent<PostProcessLayer>().enabled = false;
		music.pitch = 1f;
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040000BA RID: 186
	private bool inRange;

	// Token: 0x040000BB RID: 187
	private bool used;
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class Gary : MonoBehaviour
{
	// Token: 0x060000A0 RID: 160 RVA: 0x00004A3B File Offset: 0x00002C3B
	public void Shot()
	{
		base.StartCoroutine(this.GaryShot());
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00004A4A File Offset: 0x00002C4A
	private IEnumerator GaryShot()
	{
		GameObject.Find("Music").GetComponent<AudioSource>().Stop();
		this.meow.Play();
		PlayerMovement.canControl = false;
		this.shootPanel.SetActive(true);
		yield return new WaitForSeconds(3f);
		this.shootText.gameObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		GameManager.Get().LoadLevel(Level.SPONGEBOBSHOUSE);
		yield break;
	}

	// Token: 0x040000AA RID: 170
	public GameObject shootPanel;

	// Token: 0x040000AB RID: 171
	public TextMeshProUGUI shootText;

	// Token: 0x040000AC RID: 172
	public AudioSource meow;
}

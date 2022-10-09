using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000022 RID: 34
public class CreditsManager : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x00003DCF File Offset: 0x00001FCF
	private void Start()
	{
		base.StartCoroutine(this.PlayCredits());
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003DDE File Offset: 0x00001FDE
	private IEnumerator PlayCredits()
	{
		this.aGameByMixMorrisText.SetActive(true);
		yield return new WaitForSeconds(8f);
		this.aGameByMixMorrisText.SetActive(false);
		yield return new WaitForSeconds(4f);
		this.youtubeText.SetActive(true);
		this.twitterText.SetActive(true);
		yield return new WaitForSeconds(8f);
		this.youtubeText.SetActive(false);
		this.twitterText.SetActive(false);
		yield return new WaitForSeconds(4f);
		this.creditsToYouText.SetActive(true);
		yield return new WaitForSeconds(8f);
		this.creditsToYouText.SetActive(false);
		yield return new WaitForSeconds(4f);
		this.krustyKrabPizzaText.SetActive(true);
		yield return new WaitForSeconds(8f);
		this.krustyKrabPizzaText.SetActive(false);
		yield return new WaitForSeconds(5f);
		float startVolume = this.music.volume;
		while (this.music.volume > 0f)
		{
			this.music.volume -= startVolume * Time.deltaTime / 3f;
			yield return new WaitForEndOfFrame();
		}
		SceneManager.LoadScene("TitleScreen");
		yield break;
	}

	// Token: 0x04000065 RID: 101
	public AudioSource music;

	// Token: 0x04000066 RID: 102
	public GameObject aGameByMixMorrisText;

	// Token: 0x04000067 RID: 103
	public GameObject youtubeText;

	// Token: 0x04000068 RID: 104
	public GameObject twitterText;

	// Token: 0x04000069 RID: 105
	public GameObject creditsToYouText;

	// Token: 0x0400006A RID: 106
	public GameObject krustyKrabPizzaText;
}

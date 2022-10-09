using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000024 RID: 36
public class CutsceneManager : MonoBehaviour
{
	// Token: 0x06000072 RID: 114 RVA: 0x00003DED File Offset: 0x00001FED
	private void Awake()
	{
		CutsceneManager.instance = this;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003DF5 File Offset: 0x00001FF5
	public static CutsceneManager Get()
	{
		return CutsceneManager.instance;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003DFC File Offset: 0x00001FFC
	public void PlayCutscene(Cutscene cutscene)
	{
		base.StartCoroutine(CutsceneManager.cutsceneTable[cutscene]);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003E10 File Offset: 0x00002010
	private IEnumerator SquidwardEnterCutscene()
	{
		this.cutscenePlaying = true;
		GameObject bloodySpongeBob = GameObject.FindGameObjectWithTag("BloodySpongeBob");
		bloodySpongeBob.SetActive(false);
		GameObject.FindGameObjectWithTag("Player").SetActive(false);
		PauseManager.Get().canPause = false;
		this.cutsceneCamera.GetComponent<Animator>().SetTrigger("squidwardEnter");
		AudioManager.Get().Play("DoorCreak");
		this.cutsceneCamera.SetActive(true);
		PlayerMovement.health = 100f;
		HUDManager.Get().crosshair.SetActive(false);
		this.bars.SetActive(true);
		FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		AudioManager.Get().Play("TheRedMistIsComing");
		PauseManager.Get().canPause = true;
		yield return new WaitForSeconds(14f);
		GameObject.FindGameObjectWithTag("SpongeBobCutscene").GetComponent<Animator>().SetTrigger("pullGun");
		yield return new WaitForSeconds(5f);
		GameObject.FindGameObjectWithTag("RedMist").GetComponent<Animator>().SetTrigger("lookAtSpongeBob");
		yield return new WaitForSeconds(2f);
		this.cover.SetActive(true);
		GameObject.FindGameObjectWithTag("SpongeBobCutscene").GetComponent<Animator>().SetTrigger("idle");
		yield return new WaitForSeconds(0.25f);
		AudioManager.Get().Play("GlockFire");
		yield return new WaitForSeconds(0.25f);
		AudioManager.Get().Play("GlockFire");
		yield return new WaitForSeconds(0.25f);
		AudioManager.Get().Play("GlockFire");
		yield return new WaitForSeconds(3f);
		GameObject.FindGameObjectWithTag("SpongeBobCutscene").SetActive(false);
		bloodySpongeBob.SetActive(true);
		this.cover.SetActive(false);
		yield return new WaitForSeconds(4.8f);
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		this.cutscenePlaying = false;
		ObjectiveManager.currentObjective = 3;
		SaveManager.Save();
		GameManager.Get().LoadLevel(Level.BIKINIBOTTOM);
		yield break;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003E1F File Offset: 0x0000201F
	private IEnumerator EndCutscene()
	{
		this.cutscenePlaying = true;
		this.bars.SetActive(true);
		GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Stop();
		PlayerMovement.canControl = false;
		AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
		FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
		GameObject.FindGameObjectWithTag("MrKrabsTrueForm").SetActive(false);
		this.cutsceneCamera.SetActive(true);
		GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
		GameObject.FindGameObjectWithTag("Breeze").GetComponent<AudioSource>().Play();
		HUDManager.Get().crosshair.SetActive(false);
		HUDManager.Get().bloodOverlay.gameObject.SetActive(false);
		HUDManager.Get().ineractText.gameObject.SetActive(false);
		GameObject bloodySpongeBob = GameObject.FindGameObjectWithTag("BloodySpongeBob");
		GameObject src = GameObject.FindGameObjectWithTag("CutsceneSrc");
		GameObject dst = GameObject.FindGameObjectWithTag("CutsceneDst");
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(30f);
		bloodySpongeBob.transform.position = src.transform.position;
		bloodySpongeBob.transform.rotation = src.transform.rotation;
		bloodySpongeBob.GetComponent<Animator>().SetTrigger("walk");
		base.StartCoroutine(this.MoveSpongeBob(bloodySpongeBob.transform, src.transform, dst.transform));
		yield return new WaitForSeconds(8f);
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		FadeManager.Get().PlayFadeIn(FadeManager.defaultSpeed);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		this.cutscenePlaying = false;
		SceneManager.LoadScene("Credits");
		yield break;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003E2E File Offset: 0x0000202E
	private IEnumerator MoveSpongeBob(Transform spongebob, Transform src, Transform dst)
	{
		while (spongebob.position != dst.position)
		{
			spongebob.position = Vector3.MoveTowards(spongebob.position, dst.position, 10f * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400006E RID: 110
	private static CutsceneManager instance;

	// Token: 0x0400006F RID: 111
	private static Dictionary<Cutscene, string> cutsceneTable = new Dictionary<Cutscene, string>
	{
		{
			Cutscene.SQUIDWARDENTER,
			"SquidwardEnterCutscene"
		},
		{
			Cutscene.END,
			"EndCutscene"
		}
	};

	// Token: 0x04000070 RID: 112
	public GameObject bars;

	// Token: 0x04000071 RID: 113
	public GameObject cover;

	// Token: 0x04000072 RID: 114
	public GameObject cutsceneCamera;

	// Token: 0x04000073 RID: 115
	public bool cutscenePlaying;
}

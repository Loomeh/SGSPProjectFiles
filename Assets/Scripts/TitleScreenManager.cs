using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000054 RID: 84
public class TitleScreenManager : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x00007984 File Offset: 0x00005B84
	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		AudioManager.Get().MaxVolume();
		SaveManager.CheckSave();
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000079A1 File Offset: 0x00005BA1
	public void PlayButton()
	{
		base.StartCoroutine(this.Play());
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000079B0 File Offset: 0x00005BB0
	public void ControlsButton()
	{
		this.controlsPanel.SetActive(true);
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000079BE File Offset: 0x00005BBE
	public void ControlsPanelClick()
	{
		this.controlsPanel.SetActive(false);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000079CC File Offset: 0x00005BCC
	public void ClearSaveDataButton()
	{
		this.clearSaveConfirmDialog.SetActive(true);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000079DA File Offset: 0x00005BDA
	public void ClearSaveDataYesButton()
	{
		SaveManager.ClearSave();
		this.clearSaveConfirmDialog.SetActive(false);
		this.saveDataClearedDialog.SetActive(true);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000079F9 File Offset: 0x00005BF9
	public void ClearSaveDataNoButton()
	{
		this.clearSaveConfirmDialog.SetActive(false);
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00007A07 File Offset: 0x00005C07
	public void SaveDataClearedDialogClick()
	{
		this.saveDataClearedDialog.SetActive(false);
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00007A15 File Offset: 0x00005C15
	public void QuitButton()
	{
		Application.Quit();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00007A1C File Offset: 0x00005C1C
	private IEnumerator Play()
	{
		FadeManager.Get().PlayFadeIn(FadeManager.defaultSpeed);
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		SceneManager.LoadScene(2);
		GameManager.prevLevel = Level.NONE;
		GameManager.level = Level.SPONGEBOBSHOUSE;
		yield break;
	}

	// Token: 0x0400016C RID: 364
	public GameObject clearSaveConfirmDialog;

	// Token: 0x0400016D RID: 365
	public GameObject saveDataClearedDialog;

	// Token: 0x0400016E RID: 366
	public GameObject controlsPanel;
}

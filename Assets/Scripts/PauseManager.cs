using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200003F RID: 63
public class PauseManager : MonoBehaviour
{
	// Token: 0x060000E4 RID: 228 RVA: 0x00005A79 File Offset: 0x00003C79
	private void Awake()
	{
		PauseManager.instance = this;
		this.canPause = true;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00005A88 File Offset: 0x00003C88
	public static PauseManager Get()
	{
		return PauseManager.instance;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00005A90 File Offset: 0x00003C90
	private void Start()
	{
		this.musicSlider.value = PauseManager.musicVolume;
		this.soundSlider.value = PauseManager.soundvolume;
		this.sensitivitySlider.value = MouseLook.mouseSensitivity;
		this.autoSprintToggle.isOn = PlayerMovement.autoSprint;
		this.resolutions = Screen.resolutions;
		this.resolutionDropdown.ClearOptions();
		List<string> list = new List<string>();
		int value = 0;
		for (int i = 0; i < this.resolutions.Length; i++)
		{
			string item = this.resolutions[i].width + " x " + this.resolutions[i].height;
			list.Add(item);
			if (this.resolutions[i].width == Screen.currentResolution.width && this.resolutions[i].height == Screen.currentResolution.height)
			{
				value = i;
			}
		}
		this.resolutionDropdown.AddOptions(list);
		this.resolutionDropdown.value = value;
		this.resolutionDropdown.RefreshShownValue();
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005BB9 File Offset: 0x00003DB9
	private void Update()
	{
		if (!this.canPause || CutsceneManager.Get().cutscenePlaying)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.paused)
			{
				this.Unpause();
				return;
			}
			this.Pause();
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00005BEE File Offset: 0x00003DEE
	public void AssignMixerValues()
	{
		this.mainMixer.SetFloat("Music", PauseManager.musicVolume);
		this.mainMixer.SetFloat("Sound", PauseManager.soundvolume);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00005C1C File Offset: 0x00003E1C
	private void Pause()
	{
		PlayerMovement.canControl = false;
		this.paused = true;
		this.pauseMenu.SetActive(true);
		this.mainMenu.SetActive(true);
		this.optionsMenu.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00005C70 File Offset: 0x00003E70
	private void Unpause()
	{
		PlayerMovement.canControl = true;
		this.transition = false;
		this.paused = false;
		this.pauseMenu.SetActive(false);
		this.mainMenu.SetActive(false);
		this.optionsMenu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00005CCB File Offset: 0x00003ECB
	public void ContinueButton()
	{
		this.Unpause();
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00005CD3 File Offset: 0x00003ED3
	public void OptionsButton()
	{
		if (!this.transition)
		{
			this.mainMenu.GetComponent<Animator>().SetTrigger("scrollOff");
			this.optionsMenu.GetComponent<Animator>().SetTrigger("scrollIn");
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00005D07 File Offset: 0x00003F07
	public void BackButton()
	{
		if (!this.transition)
		{
			this.mainMenu.GetComponent<Animator>().SetTrigger("scrollIn");
			this.optionsMenu.GetComponent<Animator>().SetTrigger("scrollOff");
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00005D3C File Offset: 0x00003F3C
	public void SetResolution(int resolutionIndex)
	{
		if (this.paused)
		{
			Resolution resolution = this.resolutions[resolutionIndex];
			Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00005D76 File Offset: 0x00003F76
	public void FullscreenToggle()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00005D85 File Offset: 0x00003F85
	public void MusicVolumeChanged(float volume)
	{
		this.mainMixer.SetFloat("Music", volume);
		PauseManager.musicVolume = volume;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00005D9F File Offset: 0x00003F9F
	public void SoundVolumeChanged(float volume)
	{
		this.mainMixer.SetFloat("Sound", volume);
		PauseManager.soundvolume = volume;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00005DB9 File Offset: 0x00003FB9
	public void OptionsSensitivityChanged(float sensitivity)
	{
		MouseLook.mouseSensitivity = sensitivity;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00005DC1 File Offset: 0x00003FC1
	public void AutoSprintButton(bool value)
	{
		PlayerMovement.autoSprint = value;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00005DC9 File Offset: 0x00003FC9
	public void ExitButton()
	{
		base.StartCoroutine(this.Exit());
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00005DD8 File Offset: 0x00003FD8
	private IEnumerator Exit()
	{
		Time.timeScale = 1f;
		FadeManager.Get().PlayFadeIn(FadeManager.defaultSpeed);
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		GameManager.prevLevel = Level.NONE;
		SceneManager.LoadScene("TitleScreen");
		yield break;
	}

	// Token: 0x040000FA RID: 250
	private static PauseManager instance;

	// Token: 0x040000FB RID: 251
	public bool canPause;

	// Token: 0x040000FC RID: 252
	public GameObject pauseMenu;

	// Token: 0x040000FD RID: 253
	public GameObject mainMenu;

	// Token: 0x040000FE RID: 254
	public GameObject optionsMenu;

	// Token: 0x040000FF RID: 255
	public TextMeshProUGUI currentobjective;

	// Token: 0x04000100 RID: 256
	public AudioMixer mainMixer;

	// Token: 0x04000101 RID: 257
	private Resolution[] resolutions;

	// Token: 0x04000102 RID: 258
	public TMP_Dropdown resolutionDropdown;

	// Token: 0x04000103 RID: 259
	public Slider musicSlider;

	// Token: 0x04000104 RID: 260
	public Slider soundSlider;

	// Token: 0x04000105 RID: 261
	public Slider sensitivitySlider;

	// Token: 0x04000106 RID: 262
	public Toggle autoSprintToggle;

	// Token: 0x04000107 RID: 263
	public static float musicVolume;

	// Token: 0x04000108 RID: 264
	public static float soundvolume;

	// Token: 0x04000109 RID: 265
	public bool paused;

	// Token: 0x0400010A RID: 266
	private bool transition;
}
